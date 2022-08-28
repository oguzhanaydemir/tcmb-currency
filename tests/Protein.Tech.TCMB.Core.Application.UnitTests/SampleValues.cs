using Protein.Tech.TCMB.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Protein.Tech.TCMB.Core.Application.UnitTests
{
    public static class SampleValues
    {
        public static IEnumerable<Currency> GetCurrencies()
        {
            return new List<Currency>
            {
                new Currency
                {
                    Id = new Guid("029B1748-F9F9-4740-BE87-36987AF66EFB"),
                    Code = "USD",
                    CreateDate = DateTime.Now,
                },
                new Currency
                {
                    Id = new Guid("57B148BE-21FB-4911-A45A-EC81C2CF0239"),
                    Code = "EUR",
                    CreateDate = DateTime.Now,
                },
            };
        }

        public static IEnumerable<CurrencyRate> GetCurrencyRates()
        {
            return new List<CurrencyRate>
            {
                new CurrencyRate
                {
                    Id = Guid.NewGuid(),
                    CurrencyId = new Guid("029B1748-F9F9-4740-BE87-36987AF66EFB"),
                    Rate = 10M,
                    LastUpdated = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                    Changes = -4M,
                },
                new CurrencyRate
                {
                    Id = Guid.NewGuid(),
                    CurrencyId = new Guid("029B1748-F9F9-4740-BE87-36987AF66EFB"),
                    Rate = 11M,
                    LastUpdated = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 11),
                    Changes = 10M,
                },
                new CurrencyRate
                {
                    Id = Guid.NewGuid(),
                    CurrencyId = new Guid("029B1748-F9F9-4740-BE87-36987AF66EFB"),
                    Rate = 10M,
                    LastUpdated = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 12),
                    Changes = -9M,
                },
            };
        }
    }
}
