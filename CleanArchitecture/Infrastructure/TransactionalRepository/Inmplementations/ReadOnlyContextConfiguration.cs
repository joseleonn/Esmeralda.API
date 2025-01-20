using Infrastructure.TransactionalRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TransactionalRepository.Inmplementations
{
    public class ReadOnlyContextConfiguration : IReadOnlyContextConfiguration
    {
        protected readonly DbContext _context;

        public ReadOnlyContextConfiguration(DbContext context)
        {
            _context = context;
        }

        public TContext GetContext<TContext>() where TContext : DbContext
        {
            return (TContext)_context;
        }
    }
}
