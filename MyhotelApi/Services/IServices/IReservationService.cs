using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;

namespace MyhotelApi.Services.IServices;

public interface IReservationService
{
    ValueTask<ReservationView> AddReservationAsync(Guid userId, CreateReservationDto createReservationDto);
    ValueTask<ReservationView> GetReservationByIdAsync(Guid reservationId);
    ValueTask<List<ReservationView>> GetReservationsAsync(ReservationFilterDto? reservationFilterDto = null);
    ValueTask<ReservationView> UpdateReservationAsync(UpdateReservationDto updateReservationDto);
    ValueTask<ReservationView> DeleteReservationAsync(Guid reservationId, bool deleteFromDataBase = false);
}