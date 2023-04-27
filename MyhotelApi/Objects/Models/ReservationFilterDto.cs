using MyhotelApi.Objects.Enums;

namespace MyhotelApi.Objects.Models;

public class ReservationFilterDto
{
    public Guid? UserId { get; set; } // Unique identifier for the user who reserved the room(s)
    public Guid? HouseId { get; set; } // Identifier for the house that the room(s) reserved is belong to
    public DateTime? CheckInDate { get; set; } // Date and time of check-in
    public DateTime? CheckOutDate { get; set; } // Date and time of check-out
    public EReservationStatus? Status { get; set; }
    public DateTime? CreatedDate { get; set; } // Date and time of the creation of  the reservation
    public int? NumGuests { get; set; } // Number of guests included in the reservation
    public decimal? TotalPrice { get; set; } // Total price for the reservation
    public bool? IsPaid { get; set; } // Indicates whether the reservation has been paid for
}