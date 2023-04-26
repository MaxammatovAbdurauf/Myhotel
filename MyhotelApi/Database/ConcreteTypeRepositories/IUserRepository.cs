using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database.ConcreteTypeRepositories;

public interface IUserRepository : IGenericRepository<User>
{
    ValueTask<User?> CheckEmailExistAsync(string email);
}