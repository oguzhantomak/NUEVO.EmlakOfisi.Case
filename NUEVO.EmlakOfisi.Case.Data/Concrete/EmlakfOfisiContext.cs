using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUEVO.EmlakOfisi.Case.Entity;

namespace NUEVO.EmlakOfisi.Case.Data.Concrete
{
    public class EmlakfOfisiContext : IdentityDbContext<User, Role, int>
    {
        public EmlakfOfisiContext(DbContextOptions<EmlakfOfisiContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("Default");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        public DbSet<Company> Companies { get; set; }
    }
}
