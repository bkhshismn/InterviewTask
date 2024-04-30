namespace PharmacyApi.Models.Country
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PersianName { get; set; }
        public string CountryCode { get; set; }
        public string Status { get; set; } // Active/Deactivate
        public bool IsDeleted { get; set; } // True/False

    }

}
