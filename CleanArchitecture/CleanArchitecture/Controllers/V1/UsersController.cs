using Application.Common;
using Application.UseCases.V1.CrearDataTest;
using Application.UseCases.V1.ShowWeather;
using Application.UseCases.V1.Users.CreateUser;
using Asp.Versioning;
using CleanArchitecture.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ApiControllerBase
    {

        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CreateUser>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUser user)
        {
            return Result(await Mediator.Send(new CreateUser(user.ClerkId, user.FirstName, user.LastName, user.Email)));

        }
    }
}

