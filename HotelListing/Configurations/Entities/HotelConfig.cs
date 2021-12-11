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
	public class HotelConfig : IEntityTypeConfiguration<Hotel>
	{
		public void Configure(EntityTypeBuilder<Hotel> modelBuilder)
		{
			modelBuilder.HasData(
						new Hotel
						{
							Id = 1,
							Name = "Top Rank",
							CountryId = 1,
							Address = "Enugu",
							Rating = 3
						},
						new Hotel
						{
							Id = 2,
							Name = "Eko Hotel",
							CountryId = 1,
							Address = "Lagos",
							Rating = 4
						},
						new Hotel
						{
							Id = 3,
							Name = "Sandals Resort and Spa",
							CountryId = 3,
							Address = "Negril",
							Rating = 4.5
						});
		}
	}
}
