using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Models.Countries
{
    public class CreateCountryDto
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
