using AutoMapper;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Countries;
using HotelListing.Api.Models.Hotels;
using HotelListing.Api.Models.Users;

namespace HotelListing.Api.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<GetCountryDto, Country>().ReverseMap();
            CreateMap<GetCountryIdDto, Country>().ReverseMap();

            CreateMap<Hotel, GetHotelsDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, BaseHotelDto>().ReverseMap();
            CreateMap<Hotel, GetHotelsIdDto>().ReverseMap();

            CreateMap<ApiUser, UserDto>().ReverseMap();

        }
    }
}
