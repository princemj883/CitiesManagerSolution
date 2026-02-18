using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.Web.DatabaseContext;
using CitiesManager.Web.Model;

namespace CitiesManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController(ApplicationDbContext context) : ControllerBase
    {
        // GET: api/Cities
        /// <summary>
        /// To get list of cities(including cityId and cityName) from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await context.Cities.ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{cityId}")]
        public async Task<ActionResult<City>> GetCity(Guid cityId)
        {
            var city = await context.Cities.FindAsync(cityId);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        [HttpPut("{cityId}")]
        public async Task<IActionResult> PutCity(Guid cityId,[Bind(nameof(City.CityId),nameof(City.CityName))] City city)
        {
            if (cityId != city.CityId)
            {
                return BadRequest();
            }
            var cityToUpdate = await context.Cities.FindAsync(cityId);
            if (cityToUpdate == null)
            {
                return NotFound(); //HTTP 404 
            }
            cityToUpdate.CityName = city.CityName;
            
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(cityId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cities
        [HttpPost]
        public async Task<ActionResult<City>> PostCity([Bind(nameof(City.CityId),nameof(City.CityName))] City city)
        {
            context.Cities.Add(city);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { cityId = city.CityId }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{cityId}")]
        public async Task<IActionResult> DeleteCity(Guid cityId)
        {
            var city = await context.Cities.FindAsync(cityId);
            if (city == null)
            {
                return NotFound();
            }

            context.Cities.Remove(city);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(Guid cityId)
        {
            return context.Cities.Any(e => e.CityId == cityId);
        }
    }
}
