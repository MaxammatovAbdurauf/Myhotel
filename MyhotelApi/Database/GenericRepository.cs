using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyhotelApi.Database.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext context;
    private readonly DbSet<TEntity> DbSet;

    public GenericRepository(AppDbContext _context)
    {
        context = _context;
        DbSet = context.Set<TEntity>();
    }

    //Add:C
    public async ValueTask AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async ValueTask AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }

    //Get:R
    public async ValueTask<TEntity?> GetAsync(int Id) => await DbSet.FindAsync(Id);

    public async ValueTask<TEntity?> GetAsync(Guid Id) => await DbSet.FindAsync(Id);

    public async ValueTask<List<TEntity>> GetAllAsync() => await DbSet.ToListAsync();
    
    public IQueryable<TEntity> GetAll() => DbSet;
    
    public IEnumerable<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression) 
        => DbSet.Where(expression);


    //Update:U
    public async ValueTask<TEntity?> UpdateAsync(TEntity entity)
    {
        var updatedEntity = DbSet.Update(entity).Entity;
        await context.SaveChangesAsync();

        return updatedEntity;
    }

    public async ValueTask UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
        await context.SaveChangesAsync();
    }


    //Delete:D
    public async ValueTask<TEntity?> RemoveAsync(TEntity entity)
    {
        var deletedEntity = DbSet.Remove(entity).Entity;
        await context.SaveChangesAsync();

        return deletedEntity;
    }

    public async ValueTask RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
        await context.SaveChangesAsync();
    }
}