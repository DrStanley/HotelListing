using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Configurations.Entities;
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
			modelBuilder.ApplyConfiguration(new HotelConfig());
			modelBuilder.ApplyConfiguration(new CountryConfig());
			modelBuilder.ApplyConfiguration(new RoleConfiguration());
		}

	}
}
