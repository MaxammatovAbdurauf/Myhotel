using MyhotelApi.Database.ConcreteTypeRepositories;

namespace MyhotelApi.Database;

public interface IGenericRepository<TEntity> where TEntity : class
{

    //Add:C
    ValueTask AddAsync(TEntity entity);
    ValueTask AddRangeAsync(IEnumerable<TEntity> entity);


    //Get:R
    ValueTask<TEntity?> GetAsync(int Id);
    ValueTask<TEntity?> GetAsync(Guid id);
    ValueTask<IEnumerable<TEntity>> GetAllAsync(); //IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);


    //Update:U
    ValueTask<TEntity?> UpdateAsync(TEntity entity);
    ValueTask UpdateRangeAsync(IEnumerable<TEntity> entities);


    //Delete:D
    ValueTask<TEntity?> RemoveAsync(TEntity entity);
    ValueTask RemoveRangeAsync(IEnumerable<TEntity> entities);
}