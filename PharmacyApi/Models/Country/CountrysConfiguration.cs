using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PharmacyApi.Models.Country
{
    public class CountrysConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
           builder.ToTable(nameof(Country));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PersianName) 
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CountryCode)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength (10)
                .HasDefaultValue("Active");

            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

        }
    }
}
