using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MKidz.Models.Database
{

    public class RecordsDBContext : DbContext
    {
        public RecordsDBContext(DbContextOptions<RecordsDBContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Records> Records { get; set; }
    }
}
