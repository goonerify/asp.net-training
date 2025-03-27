using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController(
		UserManager<IdentityUser> userManager,
		ITokenRepository tokenRepository,
		ILogger<RegionsController> logger) : ControllerBase
	{
		// POST: https://localhost:portnumber/api/auth/register
		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
		{
			var user = new IdentityUser
			{
				UserName = registerRequestDto.Username,
				Email = registerRequestDto.Username
			};

			var identityResult = await userManager.CreateAsync(user, registerRequestDto.Password);

			if (identityResult.Succeeded)
			{
				// Add roles to user
				if (registerRequestDto.Roles != null && registerRequestDto.Roles.Length != 0)
				{
					identityResult = await userManager.AddToRolesAsync(user, registerRequestDto.Roles);

					if (identityResult.Succeeded)
					{
						return Created();
					}
				}

			}

			logger.LogWarning($"Register Error: Bad Request");
			return BadRequest("Something went wrong");
		}

		// POST: https://localhost:portnumber/api/auth/login
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
		{
			var user = await userManager.FindByNameAsync(loginRequestDto.Username);

			if (user != null && await userManager.CheckPasswordAsync(user, loginRequestDto.Password))
			{
				// Get roles for user
				var roles = await userManager.GetRolesAsync(user);

				if (roles != null)
				{
					// Generate token
					var token = tokenRepository.CreateJWTToken(user, [.. roles]);
					var response = new LoginResponseDto()
					{
						JwtToken = token
					};

					return Ok(response);
				}
			}

			return Unauthorized();
		}
	}
}
