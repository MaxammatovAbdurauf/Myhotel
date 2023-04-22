﻿using Microsoft.AspNetCore.Mvc;
using MyhotelApi.Objects.Models;
using MyhotelApi.Services;
using MyhotelApi.Services.IServices;

namespace MyhotelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService reservationService;
    public ReservationController(ReservationService reservationService)
    {
        this.reservationService = reservationService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> AddReservationAsync(CreateReservationDto createReservationDto)
    {
        var reservationId = await reservationService.AddReservationAsync(createReservationDto);
        return Ok(reservationId);
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetReservationByIdAsync(Guid reservationId)
    {
        var reservation = await reservationService.GetReservationByIdAsync(reservationId);
        return Ok(reservation);
    }

    [HttpGet("all")]
    public async ValueTask<IActionResult> GetReservationsAsync(ReservationFilterDto? reservationFilterDto = null)
    {
        var reservations = await reservationService.GetReservationsAsync(reservationFilterDto);
        return Ok(reservations);
    }

    [HttpPost]
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
}