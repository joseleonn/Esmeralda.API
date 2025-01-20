using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TransactionalRepository.Interfaces
{
    public interface IReadOnlyContextConfiguration
    {
        TContext GetContext<TContext>() where TContext : DbContext;
    }
}
