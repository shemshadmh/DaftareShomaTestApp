
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Application
{
    public interface IApplicationDbContext
    {

        public DbSet<Person> Persons { get;}
        public DbSet<Transaction> Transactions { get;}

        public ValueTask DisposeAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    }
}
