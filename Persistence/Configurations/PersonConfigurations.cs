
using Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PersonConfigurations: IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasIndex(person => person.Id)
                .IsUnique();

            builder.Property(person => person.Id)
                .ValueGeneratedNever(); ;

            builder.Property(person => person.Name)
                .HasMaxLength(ModelConstants.Person.NameMaxLength)
                .IsRequired();

            builder.Property(person => person.Family)
                .HasMaxLength(ModelConstants.Person.FamilyMaxLength)
                .IsRequired();
        }
    }
}
