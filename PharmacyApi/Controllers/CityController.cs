using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.City;
using PharmacyApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCity = await _cityService.InsertCityAsync(city);
            return CreatedAtAction(nameof(GetCityById), new { id = createdCity.Id }, createdCity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            await _cityService.UpdateCityAsync(city);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id, [FromBody] bool isHide)
        {
            await _cityService.DeleteCityAsync(id, isHide);
            return NoContent();
        }
    }
}
