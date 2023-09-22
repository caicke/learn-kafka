using KafkaConsumer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KafkaConsumer.Domain.Helpers.DataContext
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<Topic> Topic { get; set; }
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

    }
}
