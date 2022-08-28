using Microsoft.EntityFrameworkCore;
using Protein.Tech.TCMB.Core.Application.Interfaces.Repositories;
using Protein.Tech.TCMB.Core.Domain.Entities;
using Protein.Tech.TCMB.Infrastructure.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Protein.Tech.TCMB.Infrastructure.Persistence.Repositories
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Currency> GetByCodeAsync(string code)
        {
            return await _dbSet.Where(c => c.Code.ToLower() == code.ToLower()).FirstOrDefaultAsync();
        }
    }
}
