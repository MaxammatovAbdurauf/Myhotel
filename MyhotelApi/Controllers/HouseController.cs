using Microsoft.AspNetCore.Mvc;
using MyhotelApi.Helpers.Exceptions;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Options;
using MyhotelApi.Services;
using MyhotelApi.Services.IServices;
using System.Security.Claims;

namespace MyhotelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HouseController : ControllerBase
{
    private readonly IHouseService houseService;
    private readonly IJwtService jwtService;

    public HouseController(HouseService houseService, JwtService jwtService)
    {
        this.houseService = houseService;
        this.jwtService = jwtService;
    }

    [HttpPost]
    [Role(RoleType.All)]
    public async Task<IActionResult> AddHouseAsync(CreateHouseDto createHouseDto)
    {
        var userId = (await CheckTokenData(HttpContext.Request.Headers.Authorization)).Item1;
        createHouseDto.UserId = userId;

        var house = await houseService.AddHouseAsync(createHouseDto);
        return Ok(house);
    }

    [HttpGet]
    public async Task<IActionResult> GetHouseByIdAsync(Guid houseId)
    {
        var house = await houseService.GetHouseByIdAsync(houseId);
        return Ok(house);
    }

    [HttpGet("all")]
    [Role(RoleType.Creator, RoleType.Admin)]
    public async Task<IActionResult> GetHousesAsync([FromQuery]HouseFilterDto? houseFilterDto = null)
    {
        var houses = await houseService.GetHousesAsync(houseFilterDto);
        return Ok(houses);
    }

    [HttpPut]
    [Role(RoleType.Manager,RoleType.Owner, RoleType.Creator)]
    public async Task<IActionResult> UpdateHouseAsync(UpdateHouseDto updateHouseDto)
    {
        var updatedHouse = await houseService.UpdateHouseAsync(updateHouseDto);
        return Ok(updatedHouse);
    }

    [HttpDelete]
    [Role(RoleType.Owner, RoleType.Creator)]
    public async Task<IActionResult> DeleteHouseAsync(Guid houseId)
    {
        var role = (await CheckTokenData(HttpContext.Request.Headers.Authorization, houseId)).Item2;

        if (role != RoleType.Creator|| role != RoleType.Admin)
        {
            var updatedHouse = await houseService.DeleteHouseAsync(houseId);
            return Ok(updatedHouse);
        }
        else
        {
            var deletedHouse = await houseService.DeleteHouseAsync(houseId, deleteFromDataBase: true);
            return Ok(deletedHouse);
        }
    }

    private async Task<Tuple<Guid, string>> CheckTokenData(string token, Guid? houseId = null)
    {
        var principal = jwtService.GetPrincipal(token);
        var userId = Guid.Parse(principal!.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var role = principal!.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;

        if (houseId != null)
        {
            var house = await houseService.GetHouseByIdAsync(houseId!.Value);

            if (house.UserId != userId || role != "admin" || role != "owner")
            {
                throw new BadRequestException("you have no access");
            }
        }
        return new Tuple<Guid, string>(userId, role);
    }
}