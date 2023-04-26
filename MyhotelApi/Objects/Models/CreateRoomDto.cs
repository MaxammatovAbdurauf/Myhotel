using MyhotelApi.Objects.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyhotelApi.Objects.Models;

public class CreateRoomDto
{
    public Guid? HouseId { get; set; } // Unique identifier for the house that includes this room
    public string? Name { get; set; } // Name or type of the room (e.g. "Standard Room", "Suite")
    public int Capacity { get; set; } // Maximum number of guests that can be accommodated in the room
    public decimal PricePerNight { get; set; } // Price per night for the room
    public string? RoomAvatarPath { get; set; } // the path of image which is chosen as avatar for the room
    public List<string>? Gallery { get; set; } // List of images belong to the room
}