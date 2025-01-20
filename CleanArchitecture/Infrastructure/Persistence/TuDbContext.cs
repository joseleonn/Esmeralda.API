using Domain.Entities;
using Infrastructure.Bootstrap;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class TuDbContext : DbContext
    {
        public TuDbContext(DbContextOptions<TuDbContext> options) : base(options)
        {
        }

        // Aquí puedes agregar DbSets de prueba
        public DbSet<TestEntity> TestEntities { get; set; }
    }
}

