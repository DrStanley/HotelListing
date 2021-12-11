using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelListing.Data;
using HotelListing.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Services.Repository
{
	public class GenericRepo<T>:IGenericRepo<T> where T:class
	{
		private readonly DatabaseContext _context;
		private readonly DbSet<T> _dbSet;
		public GenericRepo(DatabaseContext databaseContext)
		{
			_context = databaseContext;
			_dbSet = _context.Set<T>();
		}
		public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable> orderBy = null, List<string> includes = null)
		{
			IQueryable<T> queryable = _dbSet;
			if (includes!=null)
			{
				queryable = _dbSet.Where(expression);
			}
			if (includes != null)
			{
				foreach (var include in includes)
				{
					queryable = queryable.Include(include);
				}
			}

			if (orderBy!=null)
			{
				queryable = (IQueryable<T>) orderBy(queryable);
			}

			return await queryable.AsNoTrackingWithIdentityResolution().ToListAsync();
		}

		public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
		{
			IQueryable<T> queryable = _dbSet;
			if (includes!=null)
			{
				foreach (var include in includes)
				{
					queryable = queryable.Include(include);
				}
			}

			return await queryable.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(expression);
		}

		public async Task Insert(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task InsertRange(IEnumerable<T> entities)
		{
		await _dbSet.AddRangeAsync(entities);
		}

		public async Task Delete(int id)
		{
			var entity = await _dbSet.FindAsync(id);
			_dbSet.Remove(entity);
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
		}

		public void Update(T entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;

		}
	}
}
