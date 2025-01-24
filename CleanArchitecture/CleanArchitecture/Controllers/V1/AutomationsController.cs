using Application.Common;
using Application.UseCases.V1.Automations.CreateAutomation;
using Application.UseCases.V1.CrearDataTest;
using Asp.Versioning;
using CleanArchitecture.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AutomationsController : ApiControllerBase
    {
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<CreateAutomationRequest>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAutomationRequest automation)
        {
            return Result(await Mediator.Send(new CreateAutomationRequest(automation.ClerkId, automation.AutomationId)));

        }
    }
}
