using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionCQRS.Core.DomainModels;
using OnionCQRS.Data.Helper;

namespace OnionCQRS.Data.Mappings
{
    public class EmployeeMapping : EntityMappingConfiguration<Employee>
    {

        public override void Map(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired();

            builder.Property(t => t.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.LastName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
