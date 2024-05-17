using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Api.Extensions;
using Project.Data;
using Project.Data.Configuration;
using Project.Services.Interfaces;
using Project.Services.Repositories;
using System.Text.Json.Serialization;
using Core.Crud.Filters.Binder;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.RegisterDbContext();
        // Add services to the container.
        builder.Services.AddControllersWithViews(config =>
        {
            config.ModelBinderProviders.Insert(0, new FilterInfoModelBinderProvider());
        });
        builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        //builder.RegisterAuthentication();

        builder.Services.AddAuthorization();
        builder.Services.AddIdentityApiEndpoints<IdentityUser>(opt =>
        {
            opt.Password.RequiredLength = 8;
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
            opt.SignIn.RequireConfirmedEmail = true;
        })
            .AddEntityFrameworkStores<DataContextBase>();
        builder.Services.ConfigureApplicationCookie(options => { options.Cookie.SameSite = SameSiteMode.None; });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddTransient<IOrderService, OrderService>();
        builder.Services.AddTransient<IOrderStatusService, OrderStatusService>();

        builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
        

        builder.Services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-XSRF-TOKEN";
        });
        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("allowedOrigin", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            opt.AddPolicy("localhost", builder => builder.WithOrigins("http://localhost:4200/"));
        });

        var app = builder.Build();
        //app.ExecuteMigrations();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || true)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.ConfigureExceptionHandler();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapIdentityApi<IdentityUser>();
        app.UseCors("allowedOrigin");

        app.MapControllers();

        var services = app.Services.CreateScope().ServiceProvider;
        var context = services.GetRequiredService<DataContextBase>();
        //if (context.Database.GetPendingMigrations().Any())
        //    context.Database.Migrate();

        app.Run();
    }
}