using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Data;

namespace HotelListing.Model
{
	public class HotelDto
	{
		public int Id { get; set; }
		[Required]
		[StringLength(maximumLength: 50, ErrorMessage = "Country name is too long")]
		public string Name { get; set; }
		
		[Required]
		public string Address { get; set; }
		
		[Required]
		[StringLength(maximumLength: 50, ErrorMessage = "Country name is too long")]
		public double Rating { get; set; }
		
		public int CountryId { get; set; }
		public Country Country { get; set; }
	}
}
