using HotelListing.Api.Models.Hotels;

namespace HotelListing.Api.Models.Countries
{
    public class GetCountryIdDto
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public IList<GetHotelsDto>? Hotels { get; set; }
    }
}
