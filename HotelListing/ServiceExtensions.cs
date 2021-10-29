using HotelListing.Data;
using HotelListing.Services.IRepository;
using HotelListing.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
		}
	}
}
