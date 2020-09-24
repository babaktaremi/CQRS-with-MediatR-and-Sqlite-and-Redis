using MediatRCqrs.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MediatRCqrs.Infrastructure
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }


        public DbSet<People> People { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<People>(p => p.Property(p => p.Id).UseHiLo("peopleSequence"));
            //modelBuilder.HasSequence("peopleSequence").StartsAt(1).IncrementsBy(10);
            base.OnModelCreating(modelBuilder);
        }
    }
}
