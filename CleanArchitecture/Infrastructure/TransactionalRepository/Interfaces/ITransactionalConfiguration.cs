using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Infrastructure.TransactionalRepository.Interfaces
{
    public interface ITransactionalConfiguration : IReadOnlyContextConfiguration
    {
        ILogger<Infrastructure.TransactionalRepository.Inmplementations.TransactionalRepository> Logger { get; set; }
    }
}
