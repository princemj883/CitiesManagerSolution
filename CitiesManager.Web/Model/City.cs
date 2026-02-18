using System.ComponentModel.DataAnnotations;

namespace CitiesManager.Web.Model;

public class City
{
    [Key]
    public Guid CityId { get; set; }
    [Required(ErrorMessage = "City name can not be blank")]
    public string? CityName { get; set; }
}