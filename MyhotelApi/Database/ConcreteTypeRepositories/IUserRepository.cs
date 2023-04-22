using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database.ConcreteTypeRepositories;

public interface IUserRepository : IGenericRepository<AppUser>
{
    ValueTask<AppUser?> CheckEmailExistAsync(string email);
}