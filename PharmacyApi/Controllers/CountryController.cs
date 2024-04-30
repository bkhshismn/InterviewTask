using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.Country;
using PharmacyApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var country = await _countryService.GetByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _countryService.GetAllAsync();
            return Ok(countries);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCountry = await _countryService.InsertCountryAsync(country);
            return CreatedAtAction(nameof(GetCountryById), new { id = createdCountry.Id }, createdCountry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            await _countryService.UpdateCountryAsync(country);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id, [FromBody] bool isHide)
        {
            await _countryService.DeleteCountryWithDeleteModeAsync(id, isHide);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _countryService.DeleteCountryAsync(id);
            return NoContent();
        }
    }
}
