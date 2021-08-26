using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Data;

namespace HotelListing.Services.IRepository
{
	public interface IUnitOfWork:IDisposable
	{
		IGenericRepo<Country> CountriesRepo { get;  }
		IGenericRepo<Hotel> HotelsRepo { get; }
		Task Save();
	}
}
