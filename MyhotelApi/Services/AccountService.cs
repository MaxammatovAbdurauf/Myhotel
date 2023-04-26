using Mapster;
using MyhotelApi.Database.ConcreteTypeRepositories;
using MyhotelApi.Database.IRepositories;
using MyhotelApi.Helpers.AddServiceFromAttribute;
using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Options;
using MyhotelApi.Objects.Views;
using MyhotelApi.Services.IServices;
using StackExchange.Redis;

namespace MyhotelApi.Services;

[Scoped]
public class AccountService : IAccountService
{
    private readonly IUnitOfWork unitOfWork;

    public AccountService(UnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Guid> AddUserAsync(SignInUserDto signInUserDto)
    {
        var user = signInUserDto.Adapt<User>();
        var userId = Guid.NewGuid();
        user.Id = userId;
        user.Role = RoleType.Creator;

        await unitOfWork.userRepository.AddAsync(user);
        return userId;
    }

    public async ValueTask<UserView> GetUserByIdAsync(Guid userId)
    {
        var user = await unitOfWork.userRepository.GetAsync(userId);
        return user.Adapt<UserView>();
    }

    public async ValueTask<List<UserView>> GetUsersAsync()
    {
        var templates = await unitOfWork.userRepository.GetAllAsync();
        return templates.Select(u => u.Adapt<UserView>()).ToList();
    }

    public async ValueTask<UserView> UpdateUserAsync(Guid userId, UpdateUserDto updateUserDto)
    {
        var user = updateUserDto.Adapt<User>();
        user.Id = userId;
        var updatedUser = await unitOfWork.userRepository.UpdateAsync(user);
        return updatedUser.Adapt<UserView>();
    }

    public async ValueTask DeleteUserAsync(Guid userId, bool fullyDelete = false)
    {
        var currentUser = await unitOfWork.userRepository.GetAsync(userId);
        if (fullyDelete) await unitOfWork.userRepository.RemoveAsync(currentUser);
        else
        {
            var user = new User
            {
                Id = userId,
                //Status = EUserStatus.deleted
            };
            await unitOfWork.userRepository.UpdateAsync(user);
        };

    }

    public async ValueTask<User?> CheckEmailExistAsync(string email)
    {
        var user = await unitOfWork.userRepository.CheckEmailExistAsync(email);
        return user;
    }
}