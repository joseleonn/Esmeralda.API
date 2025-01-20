using Infrastructure.TransactionalRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TransactionalRepository.Inmplementations
{
    public class TransactionalConfiguration : ReadOnlyContextConfiguration, ITransactionalConfiguration, IReadOnlyContextConfiguration
    {
        public ILogger<TransactionalRepository> Logger { get; set; }

        public TransactionalConfiguration(ILogger<TransactionalRepository> logger, DbContext context)
            : base(context)
        {
            Logger = logger;
        }
    }
}
