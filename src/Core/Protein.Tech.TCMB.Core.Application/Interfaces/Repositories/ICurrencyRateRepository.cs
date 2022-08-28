using Protein.Tech.TCMB.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Protein.Tech.TCMB.Core.Application.Interfaces.Repositories
{
    public interface ICurrencyRateRepository : IBaseRepositoryAsync<CurrencyRate>
    {
        IEnumerable<CurrencyRate> GetRateByCurrencyId(Guid currencyId);
    }
}