using MediatR;
using Protein.Tech.TCMB.Core.Application.Dtos;
using Protein.Tech.TCMB.Core.Application.Wrappers;
using System.Collections.Generic;

namespace Protein.Tech.TCMB.Core.Application.Features.Queries.GetAllCurrencies
{
    public class GetCurrencyDetailByCodeRequest : IRequest<ServiceResponse<List<CurrencyDetailDTO>>>
    {
        public string Code { get; set; }

        public GetCurrencyDetailByCodeRequest()
        {
        }

        public GetCurrencyDetailByCodeRequest(string code)
        {
            Code = code;
        }
    }
}
