using MediatR;
using Protein.Tech.TCMB.Core.Application.Constants;
using Protein.Tech.TCMB.Core.Application.Dtos;
using Protein.Tech.TCMB.Core.Application.Interfaces.Repositories;
using Protein.Tech.TCMB.Core.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Protein.Tech.TCMB.Core.Application.Features.Queries.GetAllCurrencies
{
    public class GetCurrencyDetailHandler : IRequestHandler<GetCurrencyDetailByCodeRequest, ServiceResponse<List<CurrencyDetailDTO>>>
    {
        private readonly ICurrencyRateRepository _currencyRateRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public GetCurrencyDetailHandler(ICurrencyRepository currencyRepository, ICurrencyRateRepository currencyRateRepository)
        {
            _currencyRateRepository = currencyRateRepository;
            _currencyRepository = currencyRepository;
        }
        public async Task<ServiceResponse<List<CurrencyDetailDTO>>> Handle(GetCurrencyDetailByCodeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var currency = await _currencyRepository.GetByCodeAsync(request.Code);
                if (currency == null)
                    return new ServiceResponse<List<CurrencyDetailDTO>>("Currency not found");

                var rates = _currencyRateRepository.GetRateByCurrencyId(currency.Id).ToList();
                if (!rates?.Any() ?? true)
                    return new ServiceResponse<List<CurrencyDetailDTO>>(true, "There is no record for this currency");

                var response = new List<CurrencyDetailDTO>();

                var data =  rates.Select(r => new CurrencyDetailDTO
                {
                    Changes = r.Changes == 0M ? "-" : $"{(r.Changes > 0 ? "+" : string.Empty)}{r.Changes.ToString("N2").Replace(',', '.')}%",
                    Currency = $"{currency.Code}-{Defaults.Currency}",
                    Rate = r.Rate,
                    Date = r.CreateDate
                }).OrderBy(r => r.Date).ToList();

                return new ServiceResponse<List<CurrencyDetailDTO>>(data);

            }
            catch (System.Exception ex)
            {
                return new ServiceResponse<List<CurrencyDetailDTO>>(ex.Message);
            }
        }
    }
}
