using HotelListing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Services
{
	public interface IAuthManager
	{
		Task<bool> ValidateUser(LoginDTO loginDTO);
	
		Task<string> CreateToken();
	}
}
