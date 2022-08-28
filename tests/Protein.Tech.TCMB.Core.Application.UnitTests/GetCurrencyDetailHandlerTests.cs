using Moq;
using Protein.Tech.TCMB.Core.Application.Features.Queries.GetAllCurrencies;
using Protein.Tech.TCMB.Core.Application.Interfaces.Repositories;
using Protein.Tech.TCMB.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Protein.Tech.TCMB.Core.Application.UnitTests
{
    public class GetCurrencyDetailHandlerTests
    {
        private readonly Mock<ICurrencyRepository> _currencyRepository;
        private readonly Mock<ICurrencyRateRepository> _currencyRateRepository;

        public GetCurrencyDetailHandlerTests()
        {
            _currencyRepository = new Mock<ICurrencyRepository>();
            _currencyRateRepository = new Mock<ICurrencyRateRepository>();
        }

        [Fact]
        public async void GetCurrencyList_ShouldReturn_FilledRates()
        {
            //Arrange
            var currency = SampleValues.GetCurrencies().FirstOrDefault(p => p.Code == "USD");
            _currencyRepository.Setup(x => x.GetByCodeAsync(It.IsAny<string>())).Returns(Task.FromResult(currency));
            _currencyRateRepository.Setup(x => x.GetRateByCurrencyId(It.IsAny<Guid>())).Returns(SampleValues.GetCurrencyRates());
            var handler = new GetCurrencyDetailHandler(_currencyRepository.Object, _currencyRateRepository.Object);

            //Act
            var response = await handler.Handle(new GetCurrencyDetailByCodeRequest(currency.Code), new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response.Data);
            Assert.True(response.IsSuccess);
        }
        [Fact]
        public async void GetCurrencyList_ShouldReturn_RatesAsNull()
        {
            //Arrange
            var currency = SampleValues.GetCurrencies().FirstOrDefault(p => p.Code == "EUR");
            _currencyRepository.Setup(x => x.GetByCodeAsync(It.IsAny<string>())).Returns(Task.FromResult(currency));
            _currencyRateRepository.Setup(x => x.GetRateByCurrencyId(It.IsAny<Guid>())).Returns(new List<CurrencyRate>());
            var handler = new GetCurrencyDetailHandler(_currencyRepository.Object, _currencyRateRepository.Object);

            //Act
            var response = await handler.Handle(new GetCurrencyDetailByCodeRequest(currency.Code), new System.Threading.CancellationToken());

            //Assert
            Assert.Null(response.Data);
            Assert.True(response.IsSuccess);
        }
        [Fact]
        public async void GetCurrencyList_ShouldReturn_NotFoundCode()
        {
            //Arrange
            Currency currency = null;
            _currencyRepository.Setup(x => x.GetByCodeAsync(It.IsAny<string>())).Returns(Task.FromResult(currency));
            var handler = new GetCurrencyDetailHandler(_currencyRepository.Object, _currencyRateRepository.Object);

            //Act
            var response = await handler.Handle(new GetCurrencyDetailByCodeRequest("GBP"), new System.Threading.CancellationToken());

            //Assert
            Assert.Null(response.Data);
            Assert.False(response.IsSuccess);
        }

    }
}


