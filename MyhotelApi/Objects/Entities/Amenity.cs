using System.ComponentModel.DataAnnotations.Schema;

namespace MyhotelApi.Objects.Entities;

public class Amenity
{
    public Guid Id { get; set; } // Unique identifier for the amenity
    public Guid RoomId { get; set; } //Id of the room that amenity is related to
    public Guid HouseId { get; set; } // Identifier for the house that the room(s) reserved is belong to
    public string? Name { get; set; } // Name of the amenity (e.g. "Swimming Pool", "Fitness Center")
    public bool IsFree { get; set; } // Indicates whether the amenity is free or requires an additional fee

    [ForeignKey(nameof(HouseId))]
    public virtual House? House { get; set; }

    [ForeignKey(nameof(RoomId))]
    public virtual Room? Room { get; set; }
}