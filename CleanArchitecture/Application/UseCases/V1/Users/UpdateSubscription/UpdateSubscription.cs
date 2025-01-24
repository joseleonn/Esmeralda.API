using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.V1.Users.UpdateSubscription
{  
    public record UpdateSubscription([Required] string ClerkId, string? CustomerId, string? Plan) : IRequest<Response<UpdateSubscription>>;
    
}
