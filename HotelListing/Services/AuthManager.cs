using HotelListing.Data;
using HotelListing.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Services
{
	public class AuthManager : IAuthManager
	{
		private readonly UserManager<ApiUser> _userManager;
		private readonly IConfiguration _configuration;
		private ApiUser _apiUser;
		public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<string> CreateToken()
		{
			var signinCred = GetSignInCred();
			var claims = await GetClaims();
			var tokenOpt = GeneratTokenOption(signinCred, claims);

			return new JwtSecurityTokenHandler().WriteToken(tokenOpt);
		}

		private JwtSecurityToken GeneratTokenOption(SigningCredentials signinCred, List<Claim> claims)
		{
			var jwtset = _configuration.GetSection("JWT");
			var exp = DateTime.Now.AddHours(Convert.ToDouble(jwtset.GetSection("lifetime").Value));
			var token = new JwtSecurityToken(
				issuer: jwtset.GetSection("Issuer").Value,
				claims: claims,
				expires:exp,
				signingCredentials: signinCred
				);
			return token;
		}

		private async Task<List<Claim>> GetClaims()
		{
			var claims = new List<Claim>
			{
				new Claim (ClaimTypes.Name,_apiUser.UserName)
			};
			var roles = await _userManager.GetRolesAsync(_apiUser);

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));

			}
			return claims;
		}

		private SigningCredentials GetSignInCred()
		{
			var key = _configuration.GetSection("JWT").GetSection("SecretKey").Value;
			//"h0t3l71s7i4g" ,"xecretKeywqejane"
			//var key = Environment.GetEnvironmentVariable("KEY");
			var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}
		 
		public async Task<bool> ValidateUser(LoginDTO loginDTO)
		{
			_apiUser = await _userManager.FindByNameAsync(loginDTO.Email);
			return (_apiUser != null && await _userManager.CheckPasswordAsync(_apiUser, loginDTO.Password));
		}
	}
}
