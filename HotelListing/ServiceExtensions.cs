using HotelListing.Data;
using HotelListing.Services;
using HotelListing.Services.IRepository;
using HotelListing.Services.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing
{
	public static class ServiceExtensions
	{
		public static void ConfigureIdentity(this IServiceCollection service)
		{
			var builder = service.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail=true);
			
			builder = new IdentityBuilder(builder.UserType,typeof(IdentityRole),service);
			builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
		}
		public static void InjectClasses(this IServiceCollection service)
		{
			service.AddTransient<IUnitOfWork, UnitOfWork>();
			service.AddTransient<IAuthManager, AuthManager>();
			/*service.AddTransient<SignInManager<ApiUser>>();
			service.AddTransient<UserManager<ApiUser>>();*/
		}
		public static void ConfigJWT(this IServiceCollection service,IConfiguration configuration)
		{
			var jwtset = configuration.GetSection("JWT");
			var key = Environment.GetEnvironmentVariable("KEY");
			service.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(opt =>
			{
				opt.TokenValidationParameters = new TokenValidationParameters {
					ValidateIssuer =true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey =true,
					ValidIssuer = jwtset.GetSection ("Issuer").Value,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
				};

			});

		}
	}
}
