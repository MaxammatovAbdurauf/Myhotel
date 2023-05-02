using Myhotel.Objects.Enums;

namespace Myhotel.Objects.Models;

public class CreateHouseDto
{
    public string? Name { get; set; } // Name of the house
    public string? Brand { get; set; }
    public Guid? UserId { get; set; } // Id of the owner of the house
    public EHouseType? Type { get; set; } // Type of the house (like hotel, motel and others)
    public string? Address { get; set; } // Address of the house
    public string? City { get; set; } // City where the house is located
    public string? Region { get; set; } // Region where the house is located (like a state)
    public string? Country { get; set; } // Country where the house is located
    public string? ZipCode { get; set; } // ZIP code of the house's location
    public uint? Stars { get; set; }  // How many starts house have (out of 5), always for hotels
    public decimal? PricePerNight { get; set; } // Price per night for a room
    public List<string>? GalleryPaths { get; set; } // List of images belong to the house
    public string? HouseAvatarPath { get; set; } //the image for avatar of the house
}