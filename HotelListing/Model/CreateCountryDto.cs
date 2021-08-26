using System.ComponentModel.DataAnnotations;

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
		[StringLength(maximumLength:5,ErrorMessage = "Short name too")]
		public string ShortName { get; set; }
	}
}