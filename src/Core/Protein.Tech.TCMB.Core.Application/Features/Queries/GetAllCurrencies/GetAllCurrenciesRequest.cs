using MediatR;
using Protein.Tech.TCMB.Core.Application.Constants;
using Protein.Tech.TCMB.Core.Application.Dtos;
using Protein.Tech.TCMB.Core.Application.Wrappers;
using System.Collections.Generic;

namespace Protein.Tech.TCMB.Core.Application.Features.Queries.GetAllCurrencies
{
    public class GetAllCurrenciesRequest : IRequest<ServiceResponse<List<CurrencyDTO>>>
    {
        public string SortBy { get; set; }
        public string SortType { get; set; }

        public GetAllCurrenciesRequest()
        {
            SortBy = Defaults.SortByCode;
            SortType = Defaults.SortAsc;
        }
        public GetAllCurrenciesRequest(string sortBy, string sortType)
        {
            SortBy = sortBy;
            SortType = sortType;
        }
    }
}
