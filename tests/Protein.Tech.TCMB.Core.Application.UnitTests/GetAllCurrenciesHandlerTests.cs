using Moq;
using Protein.Tech.TCMB.Core.Application.Features.Queries.GetAllCurrencies;
using Protein.Tech.TCMB.Core.Application.Interfaces.Repositories;
using Protein.Tech.TCMB.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Protein.Tech.TCMB.Core.Application.UnitTests
{
    public class GetAllCurrenciesHandlerTests
    {
        private readonly Mock<ICurrencyRepository> _currencyRepository;
        private readonly Mock<ICurrencyRateRepository> _currencyRateRepository;

        public GetAllCurrenciesHandlerTests()
        {
            _currencyRepository = new Mock<ICurrencyRepository>();
            _currencyRateRepository = new Mock<ICurrencyRateRepository>();
        }

        [Fact]
        public async void GetCurrencyList_ShouldReturn_FilledCurrencyList()
        {
            //Arrange
            
            _currencyRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(SampleValues.GetCurrencies()));
            _currencyRateRepository.Setup(x => x.GetRateByCurrencyId(It.IsAny<Guid>())).Returns(SampleValues.GetCurrencyRates());

            var handler = new GetAllCurrenciesHandler(_currencyRepository.Object, _currencyRateRepository.Object);
            //Act
            var response = await handler.Handle(new GetAllCurrenciesRequest(), new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response.Data);
            Assert.True(response.IsSuccess);
        }
        [Fact]
        public async void GetCurrencyList_ShouldReturn_EmptyCurrencyList()
        {
            //Arrange
            _currencyRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(((IEnumerable<Currency>)new List<Currency>())));
            var handler = new GetAllCurrenciesHandler(_currencyRepository.Object, _currencyRateRepository.Object);
            //Act
            var response = await handler.Handle(new GetAllCurrenciesRequest(), new System.Threading.CancellationToken());

            //Assert
            Assert.True(response.IsSuccess);
            Assert.Null(response.Data);
        }

    }
}


