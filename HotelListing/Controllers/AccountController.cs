using AutoMapper;
using HotelListing.Data;
using HotelListing.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : Controller
	{
		private readonly UserManager<ApiUser> _userManager;
		//private readonly SignInManager<ApiUser> _signInManager;
		private readonly ILogger<AccountController> _logger;
		private readonly IMapper _mapper;

		public AccountController(IMapper mapper, SignInManager<ApiUser> signInManager, UserManager<ApiUser> userManager, ILogger<AccountController> logger)
		{
			_mapper = mapper;
			//_signInManager = signInManager;
			_userManager = userManager;
			_logger = logger;
		}


		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
		{
			_logger.LogInformation($"Registration Attempt for {userDTO.Email}");
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var user = _mapper.Map<ApiUser>(userDTO);
				user.UserName = userDTO.Email;
				var res = await _userManager.CreateAsync(user);
				if (!res.Succeeded)
				{
					foreach (var error in res.Errors)
					{
						ModelState.AddModelError(error.Code, error.Description);
					} 
					return BadRequest(ModelState);
				}
				await _userManager.AddToRolesAsync(user, userDTO.Roles);
				return Accepted();

			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Something went wrong {nameof(Register)}");
				return Problem(e.Message, $"Something went wrong{nameof(Register)}", 500);
			}
		}

		/*[HttpPost]
		[Route("login")]		
		public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
		{
			_logger.LogInformation($"Login Attempt for {loginDTO.Email}");
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var res = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
				if (!res.Succeeded)
				{
					return Unauthorized(loginDTO);
				}
				return Ok();
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Something went wrong {nameof(Login)}");
				return Problem(e.Message, $"Something went wrong{nameof(Login)}", 500);
			}
			return View();
		}
*/
	}
}
