
namespace PharmacyApi.Models.City
{

    using Microsoft.EntityFrameworkCore;
    using PharmacyApi.Models.Country;

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PersianName { get; set; }
        public string CityCode { get; set; }
        public string Status { get; set; } // Active/Deactivate
        public bool IsDeleted { get; set; } // True/False
        public string Type { get; set; } // Urban - Rural - SmallCity

        // Foreign key
        public int CountryId { get; set; }
        public Country Country { get; set; } // Use 'Country' as type name
    }


}
