using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Data.Models;

namespace Project.Data
{

    public class DataContextBase : IdentityDbContext<IdentityUser> 
    {

        private readonly IConfiguration _config;
        public DataContextBase(DbContextOptions<DataContextBase> options, IConfiguration config) : base(options)
        {
            _config = config;
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config.GetValue<string>("ConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    //public class ContextFactory : IDesignTimeDbContextFactory<DataContextBase>
    //{
    //    public ContextFactory()
    //    {
            
    //    }
    //    public DataContextBase CreateDbContext(string[] args)
    //    {
    //        var basePath = Directory.GetCurrentDirectory();
    //        var environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
    //        Trace.WriteLine($"BasePath: {basePath}");
    //        var configuration = new ConfigurationBuilder()
    //           .SetBasePath(basePath)
    //           .AddJsonFile("appsettings.json")
    //           .Build();
    //        var connectionString = configuration.GetValue<string>("DefaultConnection");
    //        var optionsBuilder = new DbContextOptionsBuilder<DataContextBase>();
    //        optionsBuilder.UseSqlServer(connectionString);

    //        return new DataContextBase(optionsBuilder.Options, configuration);
    //    }
    //}
}