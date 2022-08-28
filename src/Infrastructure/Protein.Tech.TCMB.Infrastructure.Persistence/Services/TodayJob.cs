using MediatR;
using Microsoft.Extensions.Configuration;
using Protein.Tech.TCMB.Core.Application.Features.Commands.AddCurrencyRate;
using Protein.Tech.TCMB.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Protein.Tech.TCMB.Infrastructure.Persistence.Services
{
    public class TodayJob : ITodayJob
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICurrencyRateRepository _currencyRateRepository;
        public TodayJob(IMediator mediator, IConfiguration configuration, ICurrencyRepository currencyRepository, ICurrencyRateRepository currencyRateRepository)
        {
            _mediator = mediator;
            _configuration = configuration;
            _currencyRepository = currencyRepository;
            _currencyRateRepository = currencyRateRepository;
        }

        public async Task<bool> RunAsync()
        {
            try
            {
                var xml = XDocument.Load(_configuration.GetSection("CurrencyServiceUrl").Value);
                DateTime.TryParseExact(xml.Root.Attribute("Tarih").Value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
                foreach (var currency in await _currencyRepository.GetAllAsync())
                {
                    if (!_currencyRateRepository.GetRateByCurrencyId(currency.Id).Any(r => r.LastUpdated == date))
                    {

                        var el = xml.Descendants("Currency").Where(x => x.Attribute("Kod").Value == currency.Code).FirstOrDefault();
                        var rate = Convert.ToDecimal(el.Element("ForexSelling").Value.Replace('.', ','));

                        await _mediator.Send(new AddCurrencyRateCommandRequest
                        {
                            CurrencyId = currency.Id,
                            Date = date,
                            Rate = rate
                        });
                    }
                }

                return true;
            }
            catch
            {

            }
            return false;
        }
    }
}
