using Application.Common;
using Application.UseCases.V1.CrearDataTest;
using Application.UseCases.V1.ShowWeather;
using Asp.Versioning;
using CleanArchitecture.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DataTestController : ApiControllerBase
    {

        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<IEnumerable<WeatherForecast>>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearDataTest dataTest)
        {
            return Result(await Mediator.Send(new CrearDataTest(dataTest.Id, dataTest.Name)));

        }
    }
}

