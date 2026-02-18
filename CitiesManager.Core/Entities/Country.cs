using System.ComponentModel.DataAnnotations;

namespace CitiesManager.Core.Entities;

public class Country
{
    [Key]
    public Guid CountryId { get; set; }
    public string? CountryName { get; set; }
}