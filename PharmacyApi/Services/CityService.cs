using Microsoft.EntityFrameworkCore;
using PharmacyApi.DataContext;
using PharmacyApi.Models.City;
using PharmacyApi.Models.Country;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyApi.Services
{
    public class CityService : ICityService
    {
        private readonly PharmacyContext _context;

        public CityService(PharmacyContext context)
        {
            _context = context;
        }

        public async Task<bool> CityExistAsynk(int id)
        {
            return await _context.Citys.AnyAsync(c => c.Id == id);
        }

        public async Task DeleteCityAsync(int id, bool isHide)
        {
            var city = await _context.Citys.FindAsync(id);
            if (city != null)
            {
                if (isHide)
                {
                    city.IsDeleted = true;
                }
                else
                {
                    _context.Citys.Remove(city);
                }
                _context.Entry(city).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<City>> GetAllCitiesAsync()
        {
            return await _context.Citys.ToListAsync();
        }

        public async Task<City> GetCityByIdAsync(int id)
        {
            return await _context.Citys.FindAsync(id);
        }

        public async Task<City> InsertCityAsync(City city)
        {
            _context.Citys.Add(city);
            await _context.SaveChangesAsync();
            return city;
        }

        public async Task UpdateCityAsync(City city)
        {
            _context.Entry(city).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
