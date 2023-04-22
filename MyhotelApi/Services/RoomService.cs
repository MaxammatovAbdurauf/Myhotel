using Mapster;
using MyhotelApi.Database.ConcreteTypeRepositories;
using MyhotelApi.Database.IRepositories;
using MyhotelApi.Helpers.AddServiceFromAttribute;
using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;
using MyhotelApi.Services.IServices;

namespace MyhotelApi.Services;

[Scoped]
public class RoomService : IRoomService
{
    private readonly IUnitOfWork unitOfWork;

    public RoomService (UnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Guid> AddRoomAsync(CreateRoomDto createRoomDto)
    {
        var roomId = Guid.NewGuid();
        var room = createRoomDto.Adapt<Room>();
        room.Id = roomId;
        await unitOfWork.roomRepository.AddAsync(room);

        return roomId;
    }

    public async ValueTask<RoomView> GetRoomByIdAsync(Guid roomId)
    {
        var review = await unitOfWork.roomRepository.GetAsync(roomId);
        return review.Adapt<RoomView>();
    }

    public async ValueTask<ICollection<RoomView>> GetRoomsAsync(RoomFilterDto? roomFilterDto = null)
    {
        var rooms = await unitOfWork.roomRepository.GetAllAsync();
        return rooms.Select(r => r.Adapt<RoomView>()).ToList();
    }

    public async ValueTask<RoomView> UpdateRoomAsync(UpdateRoomDto updateRoomDto)
    {
        var room = updateRoomDto.Adapt<Room>();
        var updatedRoom = await unitOfWork.roomRepository.UpdateAsync(room);
        return updateRoomDto.Adapt<RoomView>();
    }

    public async ValueTask<RoomView> DeleteRoomAsync(Guid roomId)
    {
        var room = await unitOfWork.roomRepository.GetAsync(roomId);
        var deletedRoom = await unitOfWork.roomRepository.RemoveAsync(room);
        return deletedRoom.Adapt<RoomView>();
    }

}