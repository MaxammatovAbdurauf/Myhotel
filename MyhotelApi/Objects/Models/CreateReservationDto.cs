namespace MyhotelApi.Objects.Models;

public class CreateReservationDto
{
    public Guid HouseId { get; set; } // Identifier for the house that the room(s) reserved is belong to
    public DateTime? CheckInDate { get; set; } // Date and time of check-in
    public DateTime? CheckOutDate { get; set; } // Date and time of check-out
    public int? NumGuests { get; set; } // Number of guests included in the reservation
    public decimal? TotalPrice { get; set; } // Total price for the reservation
    public bool? IsPaid { get; set; } // Indicates whether the reservation has been paid for
}