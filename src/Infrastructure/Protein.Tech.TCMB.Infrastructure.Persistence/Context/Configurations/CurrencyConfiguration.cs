using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Protein.Tech.TCMB.Core.Domain.Entities;
using System;

namespace Protein.Tech.TCMB.Infrastructure.Persistence.Context.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey("Id");
            builder.Property(x => x.Code).IsRequired().HasMaxLength(3);
            builder.HasData(
                new Currency { Id = new Guid("D9EEFED9-EAD1-47E9-B045-A3357BE50128"), Code = "USD" },
                new Currency { Id = new Guid("44223EA3-1955-4E38-BBE5-4C918F643255"), Code = "EUR" },
                new Currency { Id = new Guid("75D1A8C3-A054-4A1A-857E-AEF623BA8644"), Code = "GBP" },
                new Currency { Id = new Guid("2A16F857-5B8A-445C-9013-705553367267"), Code = "CHF" },
                new Currency { Id = new Guid("E55FF64E-558C-4B79-85C8-1EBDDED0806F"), Code = "KWD" },
                new Currency { Id = new Guid("632EE9F7-3633-4333-95A7-36FD395758D7"), Code = "SAR" },
                new Currency { Id = new Guid("116251BF-8993-45CC-9AB8-91E6A28340B6"), Code = "RUB" }
            );
            builder.ToTable("Currencies");
        }
    }
}
