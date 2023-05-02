using Myhotel.Objects.Enums;
using Myhotel.Objects.Options;

namespace Myhotel.Objects.Models;

public class RoomFilterDto : PaginationParams
{
    public Guid? HouseId { get; set; } // Unique identifier for the house that includes this room
    public Guid? ReservationId { get; set; }
    public ERoomStatus? Status { get; set; }
    public int? Capacity { get; set; } // Maximum number of guests that can be accommodated in the room
    public decimal? PricePerNight { get; set; } // Price per night for the room
}