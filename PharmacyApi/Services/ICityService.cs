using PharmacyApi.Models.City;

namespace PharmacyApi.Services
{
    public interface ICityService
    {
        Task<City> GetCityByIdAsync(int id);
        Task<List<City>> GetAllCitiesAsync();
        Task<City> InsertCityAsync(City city);
        Task UpdateCityAsync(City city);
        Task DeleteCityAsync(int id, bool isHide);
        Task<bool> CityExistAsynk(int id);
    }
}
