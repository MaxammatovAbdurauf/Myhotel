using Microsoft.AspNetCore.Mvc;
using MyhotelApi.Helpers.Exceptions;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Options;
using MyhotelApi.Services;
using MyhotelApi.Services.IServices;
using StackExchange.Redis;

namespace MyhotelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService roomService;
    private object jwtService;

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
    [Role(RoleType.Admin)]
    public async ValueTask<IActionResult> GetRoomsAsync( [FromQuery] RoomFilterDto? RoomFilterDto = null)
    {
        var rooms = await roomService.GetRoomsAsync(RoomFilterDto);
        return Ok(rooms);
    }

    [HttpPut]
    [Role(RoleType.Manager, RoleType.Owner)]
    public async ValueTask<IActionResult> UpdateRoomAsync(UpdateRoomDto updateRoomDto)
    {
        var updatedRoom = await roomService.UpdateRoomAsync(updateRoomDto);
        return Ok(updatedRoom);
    }

    [HttpDelete]
    [Role(RoleType.Manager, RoleType.Owner)]
    public async ValueTask<IActionResult> DeleteRoomAsync(Guid roomId)
    {
        var deletedRoom = await roomService.DeleteRoomAsync(roomId);
        return Ok(deletedRoom);
    }

    /*private async Task<Tuple<Guid, string>> CheckTokenData(string token, Guid? roomId = null)
    {
        var principal = jwtService.GetPrincipal(token);
        var userId = Guid.Parse(principal!.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var role = principal!.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;

        if (roomId != null)
        {
            var room = await roomService.GetRoomByIdAsync(roomId!.Value);

            if (room.UserId != userId || role != RoleType.Creator || role != RoleType.Admin)
            {
                throw new BadRequestException("you have no access");
            }
        }
        return new Tuple<Guid, string>(userId, role);
    }*/
}