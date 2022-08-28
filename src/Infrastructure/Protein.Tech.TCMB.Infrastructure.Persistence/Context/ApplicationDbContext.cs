using Microsoft.EntityFrameworkCore;
using Protein.Tech.TCMB.Core.Domain.Entities;
using System.Reflection;

namespace Protein.Tech.TCMB.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> Rates { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
