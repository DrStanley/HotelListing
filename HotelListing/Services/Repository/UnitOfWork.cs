using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Data;
using HotelListing.Services.IRepository;

namespace HotelListing.Services.Repository
{
	public class UnitOfWork :IUnitOfWork
	{
		private readonly DatabaseContext _context;
		private IGenericRepo<Country> _countriesRepo;
		private IGenericRepo<Hotel> _hotelsRepo;
		public UnitOfWork(DatabaseContext context)
		{
			_context = context;
		}
		public void Dispose()
		{
		_context.Dispose();
		GC.SuppressFinalize(this);
		}

		public IGenericRepo<Country> CountriesRepo=>_countriesRepo??=new GenericRepo<Country>(_context);
		public IGenericRepo<Hotel> HotelsRepo => _hotelsRepo ??= new GenericRepo<Hotel>(_context);

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}
	}
}
