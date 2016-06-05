using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionCQRS.Core.DomainModels;
using OnionCQRS.Data.Helper;

namespace OnionCQRS.Data.Mappings
{
    public class DepartamentMapping : EntityMappingConfiguration<Departament>
    {
        public override void Map(EntityTypeBuilder<Departament> builder)
        {

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(x => x.Employees)
                .WithOne(x => x.Departament);
        }
    }
}
