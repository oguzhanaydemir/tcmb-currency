using MediatR;
using Protein.Tech.TCMB.Core.Application.Interfaces.Repositories;
using Protein.Tech.TCMB.Core.Application.Wrappers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Protein.Tech.TCMB.Core.Application.Features.Commands.AddCurrencyRate
{
    public class AddCurrencyRateCommandHandler : IRequestHandler<AddCurrencyRateCommandRequest, ServiceResponse<bool>>
    {
        private readonly ICurrencyRateRepository _currencyRateRepository;

        public AddCurrencyRateCommandHandler(ICurrencyRateRepository currencyRateRepository)
        {
            _currencyRateRepository = currencyRateRepository;
        }

        public async Task<ServiceResponse<bool>> Handle(AddCurrencyRateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var rate = _currencyRateRepository.GetRateByCurrencyId(request.CurrencyId)
                    .OrderByDescending(r => r.LastUpdated)
                    .FirstOrDefault();

                if (rate != null && (request.Date - rate.LastUpdated).Days < 1)
                    return new ServiceResponse<bool>(false, false, "The rate is added just once in a day");

                var change = rate == null ? 0M : CalcChange(request.Rate, rate.Rate);

                await _currencyRateRepository.AddAsync(new Domain.Entities.CurrencyRate
                {
                    CurrencyId = request.CurrencyId,
                    LastUpdated = request.Date,
                    Rate = request.Rate,
                    Changes = change,
                });

                return new ServiceResponse<bool>(data: true, "Successfully");
            }
            catch(System.Exception ex)
            {
                return new ServiceResponse<bool>(ex.Message);
            }
        }

        private decimal CalcChange(decimal newRate, decimal oldRate)
        {
            return (newRate - oldRate) * 100M / oldRate;
        }
    }
}
