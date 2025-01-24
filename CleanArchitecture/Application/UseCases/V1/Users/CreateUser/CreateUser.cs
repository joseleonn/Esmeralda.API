using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.V1.Users.CreateUser
{
    public class CreateUser : IRequest<Response<CreateUser>>
    {
        public string ClerkId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public CreateUser(string clerkId, string firstname, string lastname, string email)
        {
            ClerkId = clerkId;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }
    }
}
