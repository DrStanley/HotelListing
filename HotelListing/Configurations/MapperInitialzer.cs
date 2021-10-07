using AutoMapper;
using HotelListing.Data;
using HotelListing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations
{
	public class MapperInitialzer:Profile
	{
		public MapperInitialzer()
		{
			CreateMap<Country, CountryDto>().ReverseMap();
			CreateMap<Country, CreateCountryDto>().ReverseMap();

			CreateMap<Hotel, HotelDto>().ReverseMap();
			CreateMap<Hotel, CreateHotelDto>().ReverseMap();
		}
	}
}
