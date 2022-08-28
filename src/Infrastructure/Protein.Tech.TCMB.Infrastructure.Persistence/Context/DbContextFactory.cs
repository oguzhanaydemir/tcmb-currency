using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Protein.Tech.TCMB.Infrastructure.Persistence.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            IConfigurationBuilder builder =
                new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json");

            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("Default");

            Console.WriteLine($"DesignTimeDbContextFactory: using base path = {path}");
            Console.WriteLine($"DesignTimeDbContextFactory: using connection string = {connectionString}");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Could not find connection string named 'Default'");
            }

            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString);
            
            return new ApplicationDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
