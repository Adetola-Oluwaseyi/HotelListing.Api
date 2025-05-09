using HotelListing.Api.Models.Countries;

namespace HotelListing.Api.Models.Hotels
{
    public class GetHotelsIdDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public GetCountryDto? Country { get; set; }
    }
}
