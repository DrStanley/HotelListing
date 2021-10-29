using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
	public class DatabaseContext: IdentityDbContext<ApiUser>
	{
		//Bridge btw Db and application
		public DatabaseContext(DbContextOptions options):base(options)
		{
			
		}

		public DbSet<Country> Countries { get; set; }
		public DbSet<Hotel> Hotels { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Country>().HasData(
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

			modelBuilder.Entity<Hotel>().HasData(
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
