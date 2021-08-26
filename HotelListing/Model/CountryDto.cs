using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Model
{
	public class CountryDto
	{
		public int Id { get; set; }
		
		[Required]
		[StringLength(maximumLength:50,ErrorMessage = "Country name is too long")]
		public string Name { get; set; }
		
		[Required]
		public string Description { get; set; }
		
		[Required]
		[Display(Name = "Short Name")]
		[StringLength(maximumLength:5,ErrorMessage = "Short name too")]
		public string ShortName { get; set; }
	}
}
