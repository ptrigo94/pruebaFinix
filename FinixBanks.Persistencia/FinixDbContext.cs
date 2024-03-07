using FinixBanks.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinixBanks.Persistence
{
    public class FinixDbContext : DbContext
    {
        public FinixDbContext(DbContextOptions<FinixDbContext>options ) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder optionsBuilder)
        {
            base.OnModelCreating(optionsBuilder);
        }
        public DbSet<Bank> Banks => Set<Bank>();
    }
}
