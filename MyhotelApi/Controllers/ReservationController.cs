using Microsoft.AspNetCore.Mvc;
using MyhotelApi.Helpers.Exceptions;
using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Options;
using MyhotelApi.Services;
using MyhotelApi.Services.IServices;
using System.Security.Claims;

namespace MyhotelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Role(RoleType.All)]
public class ReservationController : ControllerBase
{
    private readonly IReservationService reservationService;
    private readonly IJwtService jwtService;

    public ReservationController(ReservationService reservationService, JwtService jwtService)
    {
        this.reservationService = reservationService;
        this.jwtService = jwtService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> AddReservationAsync(CreateReservationDto createReservationDto)
    {
        var userId = (await CheckTokenData(HttpContext.Request.Headers.Authorization)).Item1;
        createReservationDto.UserId = userId;

        var newReservation = await reservationService.AddReservationAsync(createReservationDto);

        return Ok(newReservation);
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetReservationByIdAsync(Guid reservationId)
    {
        var reservation = await reservationService.GetReservationByIdAsync(reservationId);
        return Ok(reservation);
    }

    [HttpGet("all")]
    public async ValueTask<IActionResult> GetReservationsAsync([FromQuery]ReservationFilterDto? reservationFilterDto = null)
    {
        var reservations = await reservationService.GetReservationsAsync(reservationFilterDto);
        return Ok(reservations);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateReservationAsync(UpdateReservationDto updateReservationDto)
    {
        var updatedReservation = await reservationService.UpdateReservationAsync(updateReservationDto);
        return Ok(updatedReservation);
    }

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteReservationAsync(Guid reservationId)
    {
        var deletedReservation = await reservationService.DeleteReservationAsync(reservationId);
        return Ok(deletedReservation);
    }

    private async Task<Tuple<Guid, string>> CheckTokenData(string token, Guid? reservationId = null)
    {
        var principal = jwtService.GetPrincipal(token);
        var userId = Guid.Parse(principal!.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var role = principal!.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;

        if (reservationId != null)
        {
            var reservation = await reservationService.GetReservationByIdAsync(reservationId!.Value);

            if (reservation.UserId != userId)
            {
                throw new BadRequestException("you have no access");
            }
        }
        return new Tuple<Guid, string>(userId, role);
    }
}