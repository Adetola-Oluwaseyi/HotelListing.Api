using HotelListing.Api.Models;
using HotelListing.Api.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Api.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> RegisterUser(UserDto userDto);
        Task<ResponseDto?> Login(LoginDto loginDto);
        //Task<string> GenerateToken(UserDto userDto);
    }
}
