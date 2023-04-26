using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;

namespace MyhotelApi.Services.IServices;

public interface IRoomService
{
    ValueTask<Guid> AddRoomAsync(CreateRoomDto createRoomDto);
    ValueTask<RoomView> GetRoomByIdAsync(Guid roomId);
    ValueTask<ICollection<RoomView>> GetRoomsAsync(RoomFilterDto? roomFilterDto = null);
    ValueTask<RoomView> UpdateRoomAsync(UpdateRoomDto updateRoomDto);
    ValueTask<RoomView> DeleteRoomAsync(Guid roomId, bool deleteFromDataBase = false);
}