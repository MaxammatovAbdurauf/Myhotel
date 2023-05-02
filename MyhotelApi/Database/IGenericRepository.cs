using MyhotelApi.Database.ConcreteTypeRepositories;
using System.Linq.Expressions;

namespace MyhotelApi.Database;

public interface IGenericRepository<TEntity> where TEntity : class
{

    //Add:C
    ValueTask<TEntity?> AddAsync(TEntity entity);
    ValueTask AddRangeAsync(IEnumerable<TEntity> entity);


    //Get:R
    ValueTask<TEntity?> GetAsync(int Id);
    ValueTask<TEntity?> GetAsync(Guid id);
    ValueTask<List<TEntity>> GetAllAsync();
    IQueryable<TEntity> GetAll();
    IEnumerable<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression);

    //Update:U
    ValueTask<TEntity?> UpdateAsync(TEntity entity);
    ValueTask UpdateRangeAsync(IEnumerable<TEntity> entities);


    //Delete:D
    ValueTask<TEntity?> RemoveAsync(TEntity entity);
    ValueTask RemoveRangeAsync(IEnumerable<TEntity> entities);
}