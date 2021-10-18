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
			//Country Mapping 
			CreateMap<Country, CountryDto>().ReverseMap();
			CreateMap<Country, CreateCountryDto>().ReverseMap();

			//Hotel Mapping
			CreateMap<Hotel, HotelDto>().ReverseMap();
			CreateMap<Hotel, CreateHotelDto>().ReverseMap();
		}
	}
}
