using PharmacyApi.Models.Country;

namespace PharmacyApi.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(int id);
        Task<Country> InsertCountryAsync(Country country);
        Task UpdateCountryAsync(Country country);
        Task DeleteCountryAsync(int id);
        Task DeleteCountryWithDeleteModeAsync(int id,bool IsHide);
        Task<bool> CountryExistAsynk(int id);
    }
}
