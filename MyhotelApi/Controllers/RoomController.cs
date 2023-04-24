using Microsoft.AspNetCore.Mvc;
using MyhotelApi.Helpers.Exceptions;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Options;
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
    [Role(RoleType.Manager, RoleType.Owner)]
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
    [Role(RoleType.Creator, RoleType.Admin)]
    public async ValueTask<IActionResult> GetRoomsAsync(RoomFilterDto? RoomFilterDto = null)
    {
        var rooms = await roomService.GetRoomsAsync(RoomFilterDto);
        return Ok(rooms);
    }

    [HttpPut]
    [Role(RoleType.Manager, RoleType.Owner, RoleType.Creator)]
    public async ValueTask<IActionResult> UpdateRoomAsync(UpdateRoomDto updateRoomDto)
    {
        var updatedRoom = await roomService.UpdateRoomAsync(updateRoomDto);
        return Ok(updatedRoom);
    }

    [HttpDelete]
    [Role(RoleType.Manager, RoleType.Owner,RoleType.Creator)]
    public async ValueTask<IActionResult> DeleteRoomAsync(Guid roomId)
    {
        var deletedRoom = await roomService.DeleteRoomAsync(roomId);
        return Ok(deletedRoom);
    }
}