using Microsoft.AspNetCore.Mvc;
using MyhotelApi.Objects.Models;
using MyhotelApi.Services;
using MyhotelApi.Services.IServices;

namespace MyhotelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService roomService;

    public RoomController(RoomService roomService)
    {
        this.roomService = roomService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> AddRoomAsync(CreateRoomDto createRoomDto)
    {
        var roomId = await roomService.AddRoomAsync(createRoomDto);
        return Ok(roomId);
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetRoomByIdAsync(Guid roomId)
    {
        var room = await roomService.GetRoomByIdAsync(roomId);
        return Ok(room);
    }

    [HttpGet("all")]
    public async ValueTask<IActionResult> GetRoomsAsync(RoomFilterDto? RoomFilterDto = null)
    {
        var rooms = await roomService.GetRoomsAsync(RoomFilterDto);
        return Ok(rooms);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateRoomAsync(UpdateRoomDto updateRoomDto)
    {
        var updatedRoom = await roomService.UpdateRoomAsync(updateRoomDto);
        return Ok(updatedRoom);
    }

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteRoomAsync(Guid roomId)
    {
        var deletedRoom = await roomService.DeleteRoomAsync(roomId);
        return Ok(deletedRoom);
    }
}