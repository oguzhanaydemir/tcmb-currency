using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Protein.Tech.TCMB.Core.Application.Constants;
using Protein.Tech.TCMB.Core.Application.Dtos;
using Protein.Tech.TCMB.Core.Application.Features.Queries.GetAllCurrencies;
using Protein.Tech.TCMB.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protein.Tech.TCMB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The currency list", typeof(ServiceResponse<List<CurrencyDTO>>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If there is no currency data", typeof(ServiceResponse<List<CurrencyDTO>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "If there is occurred error during the request time", typeof(ServiceResponse<List<CurrencyDTO>>))]
        public async Task<IActionResult> Get([FromQuery] string sortType = Defaults.SortAsc, [FromQuery] string sortBy = Defaults.SortByCode)
        {
            var response = await _mediator.Send(new GetAllCurrenciesRequest
            {
                SortBy = sortBy,
                SortType = sortType,
            });

            if (!response.IsSuccess)
                return BadRequest(response);

            if (!response.Data?.Any() ?? true)
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet("{code}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The currency details", typeof(ServiceResponse<List<CurrencyDetailDTO>>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "There is no currency details", typeof(ServiceResponse<List<CurrencyDetailDTO>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "If there is occurred error during the request time", typeof(ServiceResponse<List<CurrencyDetailDTO>>))]
        public async Task<IActionResult> Get(string code)
        {
            var response = await _mediator.Send(new GetCurrencyDetailByCodeRequest
            {
                Code = code
            });

            if (!response.IsSuccess)
                return BadRequest(response);

            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }
    }
}
