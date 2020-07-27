using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class RadioContext:DbContext
    {
        public DbSet<Employer> Employers { get; set; }
        public DbSet<ReclameBlock> ReclameBlocks { get; set; }
        public DbSet<Release> Releases { get; set; }
        public RadioContext(DbContextOptions<RadioContext> options):base(options) {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employer>()
        //        .HasMany(e=>e.)
        //        .WithOne(e => e.Company);
        //}
    }
}
