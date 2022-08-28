using MediatR;
using Protein.Tech.TCMB.Core.Application.Constants;
using Protein.Tech.TCMB.Core.Application.Dtos;
using Protein.Tech.TCMB.Core.Application.Interfaces.Repositories;
using Protein.Tech.TCMB.Core.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using Protein.Tech.TCMB.Core.Application.Extensions;

namespace Protein.Tech.TCMB.Core.Application.Features.Queries.GetAllCurrencies
{
    public class GetAllCurrenciesHandler : IRequestHandler<GetAllCurrenciesRequest, ServiceResponse<List<CurrencyDTO>>>
    {

        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICurrencyRateRepository _currencyRateRepository;

        public GetAllCurrenciesHandler(ICurrencyRepository currencyRepository, ICurrencyRateRepository currencyRateRepository)
        {
            _currencyRepository = currencyRepository;
            _currencyRateRepository = currencyRateRepository;
        }
        public async Task<ServiceResponse<List<CurrencyDTO>>> Handle(GetAllCurrenciesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var currencies = await _currencyRepository.GetAllAsync();
                if (!currencies?.Any() ?? true)
                    return new ServiceResponse<List<CurrencyDTO>>(true, "Currencies not found");
                var response = new List<CurrencyDTO>();
                foreach (var currency in currencies)
                {
                    var rate = _currencyRateRepository.GetRateByCurrencyId(currency.Id).OrderByDescending(r => r.LastUpdated).FirstOrDefault();
                    if (rate == null)
                        continue;
                    
                    response.Add(new CurrencyDTO
                    {
                        Currency = $"{currency.Code}-{Defaults.Currency}",
                        Rate = rate.Rate,
                        LastUpdated = rate.LastUpdated
                    });
                }
                response = Sort(response, asc: request.SortType == Defaults.SortAsc).ToList();
                return new ServiceResponse<List<CurrencyDTO>>(response);
            }
            catch (System.Exception ex)
            {
                return new ServiceResponse<List<CurrencyDTO>>(ex.Message);
            }
        }

        private List<CurrencyDTO> Sort(List<CurrencyDTO> data, string sortBy = Defaults.SortByCode, bool asc = true)
        {
            switch (sortBy)
            {
                case Defaults.SortByCode:
                    data = asc ? data.OrderBy(r => r.Currency).ToList() : data.OrderByDescending(r => r.Currency).ToList();
                    break;
                case Defaults.SortByRate:
                    data = asc ? data.OrderBy(r => r.Rate).ToList() : data.OrderByDescending(r => r.Rate).ToList();
                    break;
                default:
                    break;
            }
            return data;
        }
    }
}
