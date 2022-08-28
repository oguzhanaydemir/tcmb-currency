using Protein.Tech.TCMB.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Protein.Tech.TCMB.Core.Application.Interfaces.Repositories
{
    public interface ICurrencyRepository : IBaseRepositoryAsync<Currency>
    {
        Task<Currency> GetByCodeAsync(string code);
    }
}