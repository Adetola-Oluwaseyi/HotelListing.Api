using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.Models;
using HotelListing.Api.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HotelListing.Api.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<IEnumerable<IdentityError>> RegisterUser(UserDto userDto)
        {
            var _user = _mapper.Map<ApiUser>(userDto);
            _user.UserName = userDto.Email;
            var result = await _userManager.CreateAsync(_user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }

            return result.Errors;
        }

        public async Task<ResponseDto> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var tryLogin = await _userManager.CheckPasswordAsync(user, login.Password);
            var response = new ResponseDto();
            if (user == null)
            {
                response.error = "User not found!";
                return response;
            }

            if (tryLogin is false)
            {
                response.error = "Invalid password!";
                return response;
            }

            return await GenerateToken(user);

        }

        private async Task<ResponseDto?> GenerateToken(ApiUser user)
        {
            var response = new ResponseDto();
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var roles = await _userManager.GetRolesAsync(user);

                var roleClaim = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }.Union(roleClaim);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: credentials
                    );

                response.token = new JwtSecurityTokenHandler().WriteToken(token);

                return response;
            }

            catch (ArgumentOutOfRangeException e)
            {
                response.exception = $"Ensure key length is greater than 32 bytes. Error: {e.Message}";
                return response;

            }

            catch (Exception e)
            {
                response.exception ??= "";
                response.exception += "\n" + e.Message;
                return response;
            }



        }


    }
}
