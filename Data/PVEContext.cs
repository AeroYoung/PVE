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

        public DbSet<PVE.Models.PveData> PveData { get; set; }
    }
}
