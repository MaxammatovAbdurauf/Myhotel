namespace Myhotel.Objects.Views;

public class AmenityView
{
    public Guid? Id { get; set; } // Unique identifier for the amenity
    public Guid? RoomId { get; set; } //Id of the room that amenity is related to
    public Guid? HouseId { get; set; } // Identifier for the house that the room(s) reserved is belong to
    public string? Name { get; set; } // Name of the amenity (e.g. "Swimming Pool", "Fitness Center")
    public decimal? AdditionalFee { get; set; }
    public bool? IsFree { get; set; } // Indicates whether the amenity is free or requires an additional fee
}