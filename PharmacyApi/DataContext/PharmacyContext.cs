using Microsoft.EntityFrameworkCore;
using PharmacyApi.Models.City;
using PharmacyApi.Models.Country;

namespace PharmacyApi.DataContext
{
    public class PharmacyContext:DbContext
    {
        public PharmacyContext(DbContextOptions options)
            :base(options) 
        {               
        }
        public DbSet <Country> Countrys { get; set; }
        public DbSet<City> Citys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PharmacyContext).Assembly);
        }
    }
}
