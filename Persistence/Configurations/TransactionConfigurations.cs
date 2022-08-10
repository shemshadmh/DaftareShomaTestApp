using Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TransactionConfigurations:IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasIndex(transaction => transaction.Id)
                .IsUnique();

            builder.Property(transaction => transaction.Id)
                .ValueGeneratedNever(); ;

            builder.Property(transaction => transaction.Date)
                .HasColumnType(ModelConstants.Shared.SmallDatetimeColumnType)
                .IsRequired();

            builder.Property(transaction => transaction.Price)
                .IsRequired();

            // Relations
            builder.HasOne(transaction => transaction.Person)
                .WithMany(person => person.Transactions)
                .HasForeignKey(transaction => transaction.PersonId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted
        }
    }
}
