using Application.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.V1.Users.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUser, Response<CreateUser>>
    {
        private readonly ITransactionalRepository _repository;

        public CreateUserHandler(ITransactionalRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<CreateUser>> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            //Posibles mejoras:
                //Agregar notificacion por mail.
                
            var user = await _repository.GetByConditionAsync<User>(u => u.Email == request.Email || u.ClerkId == request.ClerkId);

            if (user != null) return Response<CreateUser>.BadRequest("El usuario ya existe");

            _repository.Insert(new User
            {
                ClerkId = request.ClerkId,
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Email = request.Email,
                Subscription = new Subscription() 
            });

            await _repository.SaveChangeAsync();

            return Response<CreateUser>.Created(request);
        }
    }
}
