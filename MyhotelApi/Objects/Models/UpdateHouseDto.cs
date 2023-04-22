using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Enums;

namespace MyhotelApi.Objects.Models;

public class UpdateHouseDto
{
    public Guid Id { get; set; } // Unique identifier for the house
    public string? Name { get; set; } // Name of the house
    public HouseType? Type { get; set; } // Type of the house (like hotel, motel and others)
    public HouseStatus? Status { get; set; } // Status of the house (like active, deleted and others)
    public string? Address { get; set; } // Address of the house
    public string? City { get; set; } // City where the house is located
    public string? Region { get; set; } // Region where the house is located (like a state)
    public string? Country { get; set; } // Country where the house is located
    public string? ZipCode { get; set; } // ZIP code of the house's location
    public uint? Stars { get; set; }  // How many starts house have (out of 5), always for hotels
    public decimal? PricePerNight { get; set; } // Price per night for a room
    public ICollection<string>? GalleryPaths { get; set; } // List of images belong to the house
    public string? HouseAvatarPath { get; set; } // the path of image which is chosen as avatar for the house 
}