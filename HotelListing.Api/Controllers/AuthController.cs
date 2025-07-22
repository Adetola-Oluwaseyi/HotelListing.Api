using HotelListing.Api.Contracts;
using HotelListing.Api.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register([FromBody] UserDto user)
        {
            var errors = await _authManager.RegisterUser(user);

            if (errors.Any())
            {
                foreach (var item in errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login([FromBody] LoginDto login)
        {
            var isValid = await _authManager.Login(login);

            if (isValid.error is not null)
            {
                return Unauthorized(new { error = isValid.error }); //for unauthenticated requests
                //return Forbid(); //for unathourized requests
            }

            if (isValid.exception is not null)
            {
                return StatusCode(500, new { error = isValid.exception });
            }

            return Ok(new { Token = isValid });


        }
    }
}
