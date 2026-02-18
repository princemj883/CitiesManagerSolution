using Asp.Versioning;
using CitiesManager.Web.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Web.Controllers.v2;
[ApiVersion("2.0")]
public class CitiesController(ApplicationDbContext context) : CustomControllerBase
{
    // GET: api/Cities
    /// <summary>
    /// To get list of cities(including only cityName) from the database
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string?>>> GetCities()
    {
        var cities =  await context.Cities
            .OrderBy(temp => temp.CityName)
            .Select(temp => temp.CityName)
            .ToListAsync();
        return cities;
    }
}