using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Project.Data;

namespace Project.Api.Extensions
{
    public static class DbContextExtensions
    {
        public static WebApplicationBuilder RegisterDbContext(this WebApplicationBuilder builder)
        {
            bool isDevelopment =Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            var connectionString = isDevelopment ? "ConnectionString" : "ProductionConnectionString";
            builder.Services.AddDbContextFactory<DataContextBase>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetValue<string>(connectionString));
                opt.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
            }, ServiceLifetime.Scoped);
            
            builder.Services.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<DataContextBase>>().CreateDbContext());
            return builder;
        }
        public static void ExecuteMigrations(this WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();

            serviceScope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContextBase>>().CreateDbContext().Database.Migrate();
        }
    }
}
