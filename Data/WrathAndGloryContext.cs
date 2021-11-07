using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrathAndGloryModels;

namespace WrathAndGloryAPI.Data
{
    public class WrathAndGloryContext : DbContext
    {
        //
        public WrathAndGloryContext(DbContextOptions<WrathAndGloryContext> options) : base(options) { }

        public DbSet<SyncModel> SyncModels { get; set; }
    }
}
