using Microsoft.EntityFrameworkCore;
using MyhotelApi.Database.Repositories;
using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database.ConcreteTypeRepositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AppDbContext context;
    public UserRepository(AppDbContext context) : base(context)
    {
        this.context = context;
    }

    public async ValueTask<User?> CheckEmailExistAsync(string email)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null) return user;
        else return null;
    }
}