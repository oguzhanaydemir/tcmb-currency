using Protein.Tech.TCMB.Core.Application.Interfaces.Repositories;
using Protein.Tech.TCMB.Core.Domain.Entities;
using Protein.Tech.TCMB.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Protein.Tech.TCMB.Infrastructure.Persistence.Repositories
{
    public class CurrencyRateRepository : Repository<CurrencyRate>, ICurrencyRateRepository
    {
        public CurrencyRateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<CurrencyRate> GetRateByCurrencyId(Guid currencyId)
        {
            return _dbSet.Where(r => r.CurrencyId == currencyId).AsEnumerable();
        }
    }
}
