namespace MyhotelApi.Objects.Entities;

public class Reservation
{
    public Guid Id { get; set; } // Unique identifier for the reservation
    public int RoomId { get; set; } // Identifier for the room reserved
    public DateTime CheckInDate { get; set; } // Date and time of check-in
    public DateTime CheckOutDate { get; set; } // Date and time of check-out
    public int NumGuests { get; set; } // Number of guests included in the reservation
    public decimal TotalPrice { get; set; } // Total price for the reservation
    public bool IsPaid { get; set; } // Indicates whether the reservation has been paid for
}