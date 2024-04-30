using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PharmacyApi.Models.City
{
    public class CityConfigurattion : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable(nameof(City));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PersianName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CityCode)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(30)
                ;

            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("Active");

            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

        }
    }
}
