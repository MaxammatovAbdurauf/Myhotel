using Mapster;
using MyhotelApi.Database.ConcreteTypeRepositories;
using MyhotelApi.Database.IRepositories;
using MyhotelApi.Helpers.AddServiceFromAttribute;
using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;
using MyhotelApi.Services.IServices;

namespace MyhotelApi.Services;

[Scoped]
public class ReservationService : IReservationService
{
    private readonly IUnitOfWork unitOfWork;

    public ReservationService (UnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;   
    }

    public async ValueTask<Guid> AddReservationAsync(CreateReservationDto createReservationDto)
    {
        var reservationId = Guid.NewGuid();
        var reservation = createReservationDto.Adapt<Reservation>();
        reservation.Id = reservationId;
        await unitOfWork.reservationRepository.AddAsync(reservation);

        return reservationId;
    }

    public async ValueTask<ReservationView> DeleteReservationAsync(Guid reservationId)
    {
        var reservation = await unitOfWork.reservationRepository.GetAsync(reservationId);
        var deletedReseravtion  = await unitOfWork.reservationRepository.RemoveAsync(reservation);

        return deletedReseravtion.Adapt<ReservationView>();
    }

    public async ValueTask<ReservationView> GetReservationByIdAsync(Guid reservationId)
    {
        var reservations = await unitOfWork.reservationRepository.GetAsync(reservationId);
        return reservations.Adapt<ReservationView>();
    }

    public async ValueTask<ICollection<ReservationView>> GetReservationsAsync(ReservationFilterDto? reservationFilterDto = null)
    {
        var reservations = await unitOfWork.reservationRepository.GetAllAsync();
        return reservations.Select(r => r.Adapt<ReservationView>()).ToList();
    }

    public async ValueTask<ReservationView> UpdateReservationAsync(UpdateReservationDto updateReservationDto)
    {
        var reservation = updateReservationDto.Adapt<Reservation>();
        var updatedReservation = await unitOfWork.reservationRepository.UpdateAsync(reservation);
        return updatedReservation.Adapt<ReservationView>();
    }
}