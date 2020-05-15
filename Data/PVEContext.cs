using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PVE.Models;

namespace PVE.Data
{
    public class PVEContext : DbContext
    {
        public PVEContext (DbContextOptions<PVEContext> options)
            : base(options)
        {
        }

        public DbSet<PveData> PveData { get; set; }

        public DbSet<Signal> Signal { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PveData>()
                .HasIndex(u => u.VIN)
                .IsUnique();
        }
    }
}
