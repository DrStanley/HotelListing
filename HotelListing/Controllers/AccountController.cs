using AutoMapper;
using HotelListing.Data;
using HotelListing.Model;
using HotelListing.Services;
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
		private readonly IAuthManager _authManager;


		public AccountController(IMapper mapper,
			UserManager<ApiUser> userManager, ILogger<AccountController> logger, IAuthManager authManager)
		{
			_mapper = mapper;
			//_signInManager = signInManager;
			_userManager = userManager;
			_logger = logger;
			_authManager = authManager;
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
				if(userDTO.Roles.Count>0)
					await _userManager.AddToRolesAsync(user, userDTO.Roles);
				return Accepted();

			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Something went wrong {nameof(Register)}");
				return Problem(e.Message, $"Something went wrong{nameof(Register)}", 500);
			}
		}

		[HttpPost]
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
				if (!await _authManager.ValidateUser(loginDTO))
				{
					return Unauthorized(new {Message="Unauthorized User", User = loginDTO });
				}
				return Accepted(new { Token = await _authManager.CreateToken() });
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Something went wrong {nameof(Login)}");
				return Problem(e.Message, $"Something went wrong{nameof(Login)}", 500);
			}
			return View();
		}

	}
}
