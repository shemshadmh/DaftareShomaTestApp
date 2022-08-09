
using System.Reflection;
using Application.Interfaces.Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationDbContext:DbContext,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }


        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Transaction> Transactions => Set<Transaction>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
