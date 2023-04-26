using Mapster;
using Myhotel.Services;
using MyhotelApi.Database.ConcreteTypeRepositories;
using MyhotelApi.Database.IRepositories;
using MyhotelApi.Helpers.AddServiceFromAttribute;
using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Enums;
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
        var room   = createRoomDto.Adapt<Room>();
        room.Id    = roomId;

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
        var query = unitOfWork.roomRepository.GetAll();
        
        if (roomFilterDto != null)
        {

        }

        var rooms = await query.ToPagedListAsync(roomFilterDto);

        return rooms.Select(r => r.Adapt<RoomView>()).ToList();
    }

    public async ValueTask<RoomView> UpdateRoomAsync(UpdateRoomDto updateRoomDto)
    {
        var room = updateRoomDto.Adapt<Room>();

        if (updateRoomDto.Name != null) room.Name = updateRoomDto.Name;
        if (updateRoomDto.Capacity != null) room.Capacity = updateRoomDto.Capacity;
        if (updateRoomDto.PricePerNight != null) room.Name = updateRoomDto.Name;
        if (updateRoomDto.ReservationId != null) room.ReservationId = updateRoomDto.ReservationId;
        if (updateRoomDto.RoomAvatarPath!= null) room.RoomAvatarPath = updateRoomDto.RoomAvatarPath;
        if (updateRoomDto.Gallery != null) room.Gallery = updateRoomDto.Gallery;

        if (updateRoomDto.Status != null &&
            updateRoomDto.Status != ERoomStatus.deleted) room.Status = updateRoomDto.Status;

        var updatedRoom = await unitOfWork.roomRepository.UpdateAsync(room);
        return updateRoomDto.Adapt<RoomView>();
    }

    public async ValueTask<RoomView> DeleteRoomAsync(Guid roomId, bool deleteFromDataBase = false)
    {
        var room = await unitOfWork.roomRepository.GetAsync(roomId);

        if (deleteFromDataBase)
        {
            var deletedRoom = await unitOfWork.roomRepository.RemoveAsync(room);
            return deletedRoom.Adapt<RoomView>();
        }
        else
        {
            room.Status = ERoomStatus.deleted;
            var deletedRoom = await unitOfWork.roomRepository.UpdateAsync(room);
            return deletedRoom.Adapt<RoomView>();
        }
    }
}