using Application.Common;
using Application.UseCases.V1.ShowWeather;
using Asp.Versioning;
using CleanArchitecture.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ApiControllerBase
    {

        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<IEnumerable<WeatherForecast>>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Result(await Mediator.Send(new GetWeather()));

        }
    }
}
