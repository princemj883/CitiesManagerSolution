using System.ComponentModel.DataAnnotations;

namespace CitiesManager.Web.Model;

public class City
{
    [Key]
    public Guid CityId { get; set; }
    public string? CityName { get; set; }
}