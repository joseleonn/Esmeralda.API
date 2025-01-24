using Application.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UseCases.V1.Users.UpdateSubscription
{
    public class Handler : IRequestHandler<UpdateSubscription, Response<UpdateSubscription>>
    {
        private readonly ITransactionalRepository _repository;

        public Handler(ITransactionalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<UpdateSubscription>> Handle(UpdateSubscription request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByConditionAsync<User>(u => u.ClerkId == request.ClerkId,
                                                                      query => query.Include(u => u.Subscription));

            if (user?.Subscription == null)
                return Response<UpdateSubscription>.NotFound("Usuario o suscripción no encontrada");

            // Actualizar solo los campos proporcionados
            if (request.CustomerId != null)
                user.Subscription.CustomerId = request.CustomerId;

            //WIP:
            //FALTA AGREGAR ENUM Y VERIFICAR
            if (request.Plan.Length > 0)
                user.Subscription.Plan = request.Plan;

            _repository.Update(user);
            await _repository.SaveChangeAsync();

            return Response<UpdateSubscription>.Success(new UpdateSubscription(
                user.ClerkId,
                user.Subscription.CustomerId,
                user.Subscription.Plan
            ));
        }
    }
}

