using System.ComponentModel.DataAnnotations.Schema;

namespace MyhotelApi.Objects.Entities;

public class Reservation
{
    public Guid Id { get; set; } // Unique identifier for the reservation
    public Guid UserId { get; set; } // Unique identifier for the user who reserved the room(s)
    public Guid HouseId { get; set; } // Identifier for the house that the room(s) reserved is belong to
    public DateTime CheckInDate { get; set; } // Date and time of check-in
    public DateTime CheckOutDate { get; set; } // Date and time of check-out
    public int NumGuests { get; set; } // Number of guests included in the reservation
    public decimal TotalPrice { get; set; } // Total price for the reservation
    public bool IsPaid { get; set; } // Indicates whether the reservation has been paid for
    public virtual List<Room>? Rooms { get; set; } // List of the rooms reserved

    [ForeignKey(nameof(HouseId))]
    public virtual House? House { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual AppUser? User { get; set; }
}