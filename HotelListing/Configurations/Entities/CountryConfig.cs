using HotelListing.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations.Entities
{
	public class CountryConfig : IEntityTypeConfiguration<Country>
	{
		public void Configure(EntityTypeBuilder<Country> modelBuilder)
		{
			modelBuilder.HasData(
						new Country
						{

							Id = 1,
							Name = "Nigeria",
							ShortName = "Nig",
							Description = "Corruption is Free"
						}, new Country
						{
							Id = 2,
							Name = "Ghana",
							ShortName = "Ghn",
							Description = "Tech is Free"
						}, new Country
						{
							Id = 3,
							Name = "Jamaica",
							ShortName = "Jam",
							Description = "Smoking is Free"
						}, new Country
						{
							Id = 4,
							Name = "South Africa",
							ShortName = "SA",
							Description = "Dance is Free"
						});
		}
	}
}
