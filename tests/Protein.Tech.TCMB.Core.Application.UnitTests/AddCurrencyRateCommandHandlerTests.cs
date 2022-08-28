using Moq;
using Protein.Tech.TCMB.Core.Application.Features.Commands.AddCurrencyRate;
using Protein.Tech.TCMB.Core.Application.Interfaces.Repositories;
using System;
using Xunit;

namespace Protein.Tech.TCMB.Core.Application.UnitTests
{
    public  class AddCurrencyRateCommandHandlerTests
    {
        private readonly Mock<ICurrencyRateRepository> _currencyRateRepository;
        public AddCurrencyRateCommandHandlerTests()
        {
            _currencyRateRepository = new Mock<ICurrencyRateRepository>();
        }

        [Fact]
        public async void AddRate_ShouldReturnAs_Added()
        {
            //Arrange
            _currencyRateRepository.Setup(x => x.GetRateByCurrencyId(It.IsAny<Guid>())).Returns(SampleValues.GetCurrencyRates());

            var handler = new AddCurrencyRateCommandHandler(_currencyRateRepository.Object);
            var request = new AddCurrencyRateCommandRequest()
            {
                CurrencyId = new Guid("029B1748-F9F9-4740-BE87-36987AF66EFB"),
                Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 13),
                Rate = 10M
            };
            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.True(response.Data);
        }

        [Fact]
        public async void AddRate_ShouldReturnAs_NotAdded()
        {
            //Arrange
            _currencyRateRepository.Setup(x => x.GetRateByCurrencyId(It.IsAny<Guid>())).Returns(SampleValues.GetCurrencyRates());

            var handler = new AddCurrencyRateCommandHandler(_currencyRateRepository.Object);
            var request = new AddCurrencyRateCommandRequest()
            {
                CurrencyId = new Guid("029B1748-F9F9-4740-BE87-36987AF66EFB"),
                Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                Rate = 10M
            };
            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.False(response.Data);
        }
    }
}
