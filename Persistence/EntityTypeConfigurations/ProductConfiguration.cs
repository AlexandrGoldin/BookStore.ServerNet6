using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) 
        {
            builder.HasKey(product => product.Id);
            builder.HasIndex(product => product.Id).IsUnique();
            builder.Property(product => product.Title).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Author).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Genre).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Price).IsRequired();
        }
    }
}
