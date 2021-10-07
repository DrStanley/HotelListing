using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Model
{
	public class CreateCountryDto
	{
		
		[Required]
		[StringLength(maximumLength:50,ErrorMessage = "Country name is too long")]
		public string Name { get; set; }
		
		[Required]
		public string Description { get; set; }
		
		[Required]
		[Display(Name = "Short Name")]
		[StringLength(maximumLength:5,ErrorMessage = "Short name too long")]
		public string ShortName { get; set; }
	}

	public class CountryDto: CreateCountryDto
	{
		public int Id { get; set; }
		public virtual IList<HotelDto> Hotels { get; set; }

	}
}
