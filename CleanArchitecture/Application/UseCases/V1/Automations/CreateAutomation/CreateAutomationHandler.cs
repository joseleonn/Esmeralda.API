using Application.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.V1.Automations.CreateAutomation
{
    public class CreateAutomationHandler : IRequestHandler<CreateAutomationRequest, Response<CreateAutomationRequest>>
    {

        private readonly ITransactionalRepository _repository;
        public CreateAutomationHandler(ITransactionalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<CreateAutomationRequest>> Handle(CreateAutomationRequest request, CancellationToken cancellationToken)
        {
            // Buscar el usuario por su ClerkId
            var user = (await _repository.GetByConditionAsync<User>(u => u.ClerkId == request.ClerkId));

            if (user == null)
            {
                return Response<CreateAutomationRequest>.NotFound("Usuario no encontrado.");
            }

            // Crear la nueva automatización
            var automation = new Automation
            {
                Id = request.AutomationId,
                UserId = user.Id 
            };

           
            _repository.Insert(automation);
            await _repository.SaveChangeAsync();
         
            return Response<CreateAutomationRequest>.Success(new CreateAutomationRequest(user.ClerkId, automation.Id));
        }
    }
    }

