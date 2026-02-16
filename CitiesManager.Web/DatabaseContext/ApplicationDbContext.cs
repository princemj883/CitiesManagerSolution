using CitiesManager.Web.Model;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Web.DatabaseContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public ApplicationDbContext()
    {
        
    }
    
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<City>().HasData(new City()
        {
            CityId = Guid.Parse("a0437da5-0508-417d-bdcd-7e6b7a2896b9"),
            CityName = "New York"
        });
        modelBuilder.Entity<City>().HasData(new City()
        {
            CityId = Guid.Parse("0333dca2-65c3-40cc-b9d3-a5ab9818b4e8"),
            CityName = "London"
        });
    }
}
