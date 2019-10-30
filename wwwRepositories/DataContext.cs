using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using wwwModel;

namespace wwwRepositories
{
    public class DataContext : DbContext
    {
        private IConfiguration _configuration { get; }

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_configuration.GetConnectionString("wwwWebDb"));
        }

        public DbSet<WwwDescribe> Describes { get; set; }

    }
}
