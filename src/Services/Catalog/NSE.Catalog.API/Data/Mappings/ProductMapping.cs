using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Catalog.API.Models;

namespace NSE.Catalog.API.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("VARCHAR(255)");

            builder.Property(x => x.Description)
                .HasColumnType("VARCHAR(600)");

            builder.Property(x => x.PictureUrl)
                .IsRequired()
                .HasColumnType("VARCHAR(255)");

            builder.ToTable("Products");
        }
    }
}
