using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Protein.Tech.TCMB.Api.Controllers;
using Protein.Tech.TCMB.Core.Application.Dtos;
using Protein.Tech.TCMB.Core.Application.Features.Queries.GetAllCurrencies;
using Protein.Tech.TCMB.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Protein.Tech.TCMB.Presentation.Api.UnitTests
{
    public class CurrencyControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        public CurrencyControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async void GetCurrencyList_ShouldReturn_OK()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetAllCurrenciesRequest>(), It.IsAny<CancellationToken>())).Returns(GetCurrencyList_OK());

            //Act
            CurrencyController currencyController = new CurrencyController(_mediator.Object);
            var response = await currencyController.Get();

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, (response as ObjectResult).StatusCode);
        }
        [Fact]
        public async void GetCurrencyList_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetAllCurrenciesRequest>(), It.IsAny<CancellationToken>())).Returns(GetCurrencyList_NotFound());

            //Act
            CurrencyController currencyController = new CurrencyController(_mediator.Object);
            var response = await currencyController.Get();

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, (response as ObjectResult).StatusCode);
        }
        [Fact]
        public async void GetCurrencyList_ShouldReturn_BadRequest()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetAllCurrenciesRequest>(), It.IsAny<CancellationToken>())).Returns(GetCurrencyList_BadRequest());

            //Act
            CurrencyController currencyController = new CurrencyController(_mediator.Object);
            var response = await currencyController.Get();

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, (response as ObjectResult).StatusCode);
        }

        [Fact]
        public async void GetCurrencyDetails_ShouldReturn_CurrencyList()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetCurrencyDetailByCodeRequest>(), It.IsAny<CancellationToken>())).Returns(GetCurrencyDetails_OK());

            //Act
            CurrencyController currencyController = new CurrencyController(_mediator.Object);
            var response = await currencyController.Get("usd");

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, (response as ObjectResult).StatusCode);
        }
        [Fact]
        public async void GetCurrencyDetails_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetCurrencyDetailByCodeRequest>(), It.IsAny<CancellationToken>())).Returns(GetCurrencyDetails_NotFound());

            //Act
            CurrencyController currencyController = new CurrencyController(_mediator.Object);
            var response = await currencyController.Get("usd");

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, (response as ObjectResult).StatusCode);
        }
        [Fact]
        public async void GetCurrencyDetails_ShouldReturn_BadRequest()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetCurrencyDetailByCodeRequest>(), It.IsAny<CancellationToken>())).Returns(GetCurrencyDetails_BadRequest());

            //Act
            CurrencyController currencyController = new CurrencyController(_mediator.Object);
            var response = await currencyController.Get("usd");

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, (response as ObjectResult).StatusCode);
        }



        private static Task<ServiceResponse<List<CurrencyDTO>>> GetCurrencyList_OK()
        {
            return Task.FromResult(new ServiceResponse<List<CurrencyDTO>>
            {
                IsSuccess = true,
                Message = "succesfully",
                Data = new List<CurrencyDTO>
            {
                new CurrencyDTO
                {
                    Currency = "USD-TRY",
                    LastUpdated = System.DateTime.Now,
                    Rate = 18.15M
                },
                new CurrencyDTO
                {
                    Currency = "EUR-TRY",
                    LastUpdated = System.DateTime.Now,
                    Rate = 18.10M
                },
                new CurrencyDTO
                {
                    Currency = "GBP-TRY",
                    LastUpdated = System.DateTime.Now,
                    Rate = 21.34M
                },
            }
            });
        }
        private static Task<ServiceResponse<List<CurrencyDTO>>> GetCurrencyList_NotFound()
        {
            return Task.FromResult(new ServiceResponse<List<CurrencyDTO>>
            {
                IsSuccess = true,
                Message = "not found",
                Data = null
            });
        }
        private static Task<ServiceResponse<List<CurrencyDTO>>> GetCurrencyList_BadRequest()
        {
            return Task.FromResult(new ServiceResponse<List<CurrencyDTO>>());
        }


        private static Task<ServiceResponse<List<CurrencyDetailDTO>>> GetCurrencyDetails_OK()
        {
            return Task.FromResult(new ServiceResponse<List<CurrencyDetailDTO>>
            {
                IsSuccess = true,
                Message = "succesfully",
                Data = new List<CurrencyDetailDTO>
                {
                    new CurrencyDetailDTO
                    {
                        Currency = "USD-TRY",
                        Date = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                        Changes = "+1%",
                        Rate = 18.15M
                    },
                    new CurrencyDetailDTO
                    {
                        Currency = "USD-TRY",
                        Date = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month, 11),
                        Changes = "-10%",
                        Rate = 16.30M
                    },
                    new CurrencyDetailDTO
                    {
                        Currency = "USD-TRY",
                        Date = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month, 12),
                        Changes = "+5%",
                        Rate = 17.10M
                    },
                }
            });
        }
        private static Task<ServiceResponse<List<CurrencyDetailDTO>>> GetCurrencyDetails_NotFound()
        {
            return Task.FromResult(new ServiceResponse<List<CurrencyDetailDTO>>
            {
                IsSuccess = true,
                Message = "succesfully",
            });
        }
        private static Task<ServiceResponse<List<CurrencyDetailDTO>>> GetCurrencyDetails_BadRequest()
        {
            return Task.FromResult(new ServiceResponse<List<CurrencyDetailDTO>>());
        }
    }
}
