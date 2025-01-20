using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.V1.CrearDataTest
{
    public class CrearDataTest : IRequest<Response<CrearDataTest>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CrearDataTest(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
