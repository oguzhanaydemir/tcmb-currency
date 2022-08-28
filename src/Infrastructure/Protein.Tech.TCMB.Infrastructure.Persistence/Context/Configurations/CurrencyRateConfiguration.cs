using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Protein.Tech.TCMB.Core.Domain.Entities;
using System;

namespace Protein.Tech.TCMB.Infrastructure.Persistence.Context.Configurations
{
    public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.HasKey("Id");
            builder.Property(x => x.Rate).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Changes).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.LastUpdated).HasColumnType("datetime");
            builder.HasOne(x => x.Currency).WithMany(c => c.Rates).HasForeignKey(x => x.CurrencyId);
            builder.ToTable("Rates");
        }
    }
}