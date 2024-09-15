using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
        }


        //POST: /api/Auth/Register
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {

            var identyResult = await _userManager.CreateAsync(
                                                                new IdentityUser
                                                                {
                                                                    UserName = registerRequestDto.UserName,
                                                                    Email = registerRequestDto.UserName
                                                                },
                                                                registerRequestDto.Password);

            if (identyResult.Succeeded)
            {
                if (registerRequestDto.Roles != null)
                {
                    var user = await _userManager.FindByNameAsync(registerRequestDto.UserName);
                    //Add Roles to this user
                    identyResult = await _userManager.AddToRolesAsync(user, registerRequestDto.Roles);

                    return Ok("User was register");
                }


            }

            return BadRequest("Something went wrong");

        }


        //POST: /api/Auth/Login
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            //Mira si el usuario existe
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Usuario);

            if ((user != null))
            {
                //Checkeo de pass, devuelve true si el password es correcto
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    //Create Token
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        LoginResponseDto loginResponseDto = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(loginResponseDto);
                    }

                }

            }

            return BadRequest("Something went wrong");

        }

    }
}
