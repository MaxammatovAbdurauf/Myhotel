﻿using Microsoft.AspNetCore.Mvc;
using MyhotelApi.Helpers.Exceptions;
using MyhotelApi.Objects.Models;
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
    [Role("user")]
    public async Task<IActionResult> AddHouseAsync(CreateHouseDto createHouseDto)
    {
        var houseId = await houseService.AddHouseAsync(createHouseDto);
        return Ok(houseId);
    }

    [HttpGet]
    public async Task<IActionResult> GetHouseByIdAsync(Guid houseId)
    {
        var house = await houseService.GetHouseByIdAsync(houseId);
        return Ok(house);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetHousesAsync(HouseFilterDto? houseFilterDto = null)
    {
        var houses = await houseService.GetHousesAsync(houseFilterDto);
        return Ok(houses);
    }

    [HttpPut]
    [Role("user")]
    public async Task<IActionResult> UpdateHouseAsync(UpdateHouseDto updateHouseDto)
    {
        var updatedHouse = await houseService.UpdateHouseAsync(updateHouseDto);
        return Ok(updatedHouse);
    }

    [HttpDelete]
    [Role("user")]
    public async Task<IActionResult> DeleteHouseAsync(Guid houseId)
    {
        var role = (await CheckTokenData(HttpContext.Request.Headers.Authorization, houseId)).Item2;

        if (role != "owner" || role != "admin")
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