using FitnessApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data.Repository;

public class BaseRepository<TType, TId> : IRepository<TType, TId>
	where TType : class
{
	private readonly ApplicationDbContext _context;
	private readonly DbSet<TType> _dbSet;

	public BaseRepository(ApplicationDbContext context)
	{
		_context = context;
		_dbSet = context.Set<TType>();
	}

	public TType GetById(TId id)
	{
		TType entity = _dbSet
			.Find(id);

		return entity;
	}

	public async Task<TType> GetByIdAsync(TId id)
	{
		TType entity = await _dbSet
			.FindAsync(id);

		return entity;
	}

	public IEnumerable<TType> GetAll()
	{
		return _dbSet.ToList();
	}

	public async Task<IEnumerable<TType>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	public IEnumerable<TType> GetAllAttached()
	{
		return _dbSet.AsQueryable();
	}

	public void Add(TType entity)
	{
		_dbSet.Add(entity);
		_context.SaveChanges();
	}

	public async Task AddAsync(TType entity)
	{
		await _dbSet.AddAsync(entity);
		await _context.SaveChangesAsync();
	}

	public bool Delete(TId id)
	{
		TType entity = GetById(id);
		if (entity == null)
		{
			return false;
		}

		_context.Remove(entity);
		_context.SaveChanges();

		return true;
	}

	public async Task<bool> DeleteAsync(TId id)
	{
		TType entity = await GetByIdAsync(id);
		if (entity == null)
		{
			return false;
		}

		_context.Remove(entity);
		await _context.SaveChangesAsync();

		return true;
	}

	public bool Update(TType entity)
	{
		try
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			_context.SaveChanges();

			return true;
		}
		catch (Exception e)
		{
			return false;
		}
	}

	public async Task<bool> UpdateAsync(TType entity)
	{
		try
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return true;
		}
		catch (Exception e)
		{
			return false;
		}
	}
}