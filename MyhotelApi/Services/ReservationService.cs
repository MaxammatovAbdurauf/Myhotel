using Mapster;
using Microsoft.EntityFrameworkCore;
using MyhotelApi.Database.ConcreteTypeRepositories;
using MyhotelApi.Database.IRepositories;
using MyhotelApi.Helpers.AddServiceFromAttribute;
using MyhotelApi.Helpers.Exceptions;
using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Enums;
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

    public async ValueTask<ReservationView> AddReservationAsync(CreateReservationDto createReservationDto)
    {
        var reservationId = Guid.NewGuid();
        var reservation   = createReservationDto.Adapt<Reservation>();

        reservation.Id = reservationId;
        reservation.Status = EReservationStatus.created;
        reservation.CreatedDate = DateTime.UtcNow;
        reservation.CheckOutDate = DateTime.UtcNow;
        reservation.CheckInDate = DateTime.UtcNow;

        var newReservation = await unitOfWork.reservationRepository.AddAsync(reservation);

        return newReservation.Adapt<ReservationView>();
    }

    public async ValueTask<ReservationView> GetReservationByIdAsync(Guid reservationId)
    {
        var reservation = await unitOfWork.reservationRepository.GetAsync(reservationId);

        if (reservation is null) throw new NotFoundException<Reservation>();
        else 
           return reservation.Adapt<ReservationView>();
    }

    public async ValueTask<List<ReservationView>> GetReservationsAsync(ReservationFilterDto? reservationFilterDto = null)
    {
        var query = unitOfWork.reservationRepository.GetAll();

        if (reservationFilterDto != null)
        {
            if (reservationFilterDto.NumGuests != null) 
                query = query.Where(res => res.NumGuests == reservationFilterDto.NumGuests);

            if (reservationFilterDto.IsPaid != null)
                query = query.Where(res => res.IsPaid == reservationFilterDto.IsPaid);

            if (reservationFilterDto.UserId != null)
                query = query.Where(res => res.UserId == reservationFilterDto.UserId);

            if (reservationFilterDto.CheckInDate != null)
                query = query.Where(res => res.CheckInDate >= reservationFilterDto.CheckInDate);

            if (reservationFilterDto.CheckOutDate != null)
                query = query.Where(res => res.CheckOutDate <= reservationFilterDto.CheckOutDate);

            if (reservationFilterDto.HouseId != null)
                query = query.Where(res => res.HouseId == reservationFilterDto.HouseId);

            if (reservationFilterDto.TotalPrice != null)
                query = query.Where(res => res.TotalPrice == reservationFilterDto.TotalPrice);

            if (reservationFilterDto.CreatedDate != null)
                query = query.Where(res => res.CreatedDate == reservationFilterDto.CreatedDate);

            if (reservationFilterDto.Status != null)
            {
                query = reservationFilterDto.Status switch
                {
                    EReservationStatus.created  => query.Where(res => res.Status == EReservationStatus.created),
                    EReservationStatus.inactive => query.Where(res => res.Status == EReservationStatus.inactive),
                    EReservationStatus.active   => query.Where(res => res.Status == EReservationStatus.active),
                    EReservationStatus.deleted  => query.Where(res => res.Status == EReservationStatus.deleted),
                    _ => query
                };
            }
        }

        var reservations = await query.ToListAsync();

        return reservations.Select(r => r.Adapt<ReservationView>()).ToList();
    }

    public async ValueTask<ReservationView> UpdateReservationAsync(UpdateReservationDto updateReservationDto)
    {
        var reservation = await unitOfWork.reservationRepository.GetAsync(updateReservationDto.Id!.Value);

        if (reservation is null) throw new NotFoundException<Reservation>();
        else
        {
            var updatedReservation = await unitOfWork.reservationRepository.UpdateAsync(reservation);
            // code for the updating properties
            return updatedReservation!.Adapt<ReservationView>();
        }
    }

    public async ValueTask<ReservationView> DeleteReservationAsync(Guid reservationId, bool deleteFromDataBase = false)
    {
        var reservation = await unitOfWork.reservationRepository.GetAsync(reservationId);

        if (reservation is null) throw new NotFoundException<Reservation>();
        else
        {
            if (deleteFromDataBase)
            {
                var deletedReseravtion = await unitOfWork.reservationRepository.RemoveAsync(reservation);
                return deletedReseravtion!.Adapt<ReservationView>();
            }
            else
            {
                reservation.Status = EReservationStatus.deleted; ;
                var updatedReservation = await unitOfWork.reservationRepository.UpdateAsync(reservation);
                return updatedReservation!.Adapt<ReservationView>();
            }
        }      
    }
}