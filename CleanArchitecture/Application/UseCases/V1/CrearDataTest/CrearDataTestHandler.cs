using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.V1.CrearDataTest
{
    public class CrearDataTestHandler : IRequestHandler<CrearDataTest, Response<CrearDataTest>>
    {
        public async Task<Response<CrearDataTest>> Handle(CrearDataTest request, CancellationToken cancellationToken)
        {
            return Response<CrearDataTest>.Created(request);
        }


    }
}
