using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.V1.Automations.CreateAutomation
{
    public class CreateAutomationRequest : IRequest<Response<CreateAutomationRequest>>
    {
        public string ClerkId { get; set; }
        public Guid AutomationId { get; set; }

        public CreateAutomationRequest(string clerkId, Guid automationId)
        {
            ClerkId = clerkId;
            AutomationId = automationId;
        }
    }
}
