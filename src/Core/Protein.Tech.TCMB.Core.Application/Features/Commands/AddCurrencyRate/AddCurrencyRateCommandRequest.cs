using MediatR;
using Protein.Tech.TCMB.Core.Application.Wrappers;
using System;

namespace Protein.Tech.TCMB.Core.Application.Features.Commands.AddCurrencyRate
{
    public class AddCurrencyRateCommandRequest : IRequest<ServiceResponse<bool>>
    {
        public Guid CurrencyId { get; set; }
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
    }
}
