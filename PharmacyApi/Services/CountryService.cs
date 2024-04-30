using Microsoft.EntityFrameworkCore;
using PharmacyApi.DataContext;
using PharmacyApi.Models.Country;

namespace PharmacyApi.Services
{
    public class CountryService : ICountryService
    {
        private readonly PharmacyContext _context;
        public CountryService(PharmacyContext context)
        {
            _context = context;
        }
        public async Task<bool> CountryExistAsynk(int id)
        {
            return await _context.Countrys.AllAsync(x => x.Id == id);
        }   
        /// <summary>
        /// Delete one Record by CountryId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteCountryAsync(int id)
        {
            _context.Remove(new Country { Id = id });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Return All Countries 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Country>> GetAllAsync()
        {
            return await _context.Countrys.ToListAsync();
        }
        public async Task DeleteCountryWithDeleteModeAsync(int id, bool IsHide)
        {
            var country = await _context.Countrys.FindAsync(id);
            if (country != null)
            {
                if (IsHide)
                {
                    country.IsDeleted = true;
                }
                _context.Entry(country).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }


        public async Task<Country> GetByIdAsync(int id)
        {
           return await _context.Countrys.FirstOrDefaultAsync(X => X.Id == id);
        }

        public async Task<Country> InsertCountryAsync(Country country)
        {
            await _context.AddAsync(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task UpdateCountryAsync(Country country)
        {
            _context.Entry(country).State= EntityState.Modified;
            await _context.SaveChangesAsync();
        }


    }
}
