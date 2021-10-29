﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Model
{
	public class UserDTO:LoginDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
		
	}
	public class LoginDTO
	{

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[StringLength(1)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
