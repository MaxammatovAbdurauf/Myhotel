﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyhotelApi.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyhotelApi.Objects.Entities;

public class House
{
    public Guid Id { get; set; } // Unique identifier for the house
    public Guid? UserId { get; set; } // Id of the owner of the house
    public string? Name { get; set; } // Name of the house
    public string? Brand { get; set; }
    public EHouseType? Type { get; set; } // Type of the house (like hotel, motel and others)
    public EHouseStatus? Status { get; set; } // Status of the house (like active, deleted and others)
    public string? Address { get; set; } // Address of the house
    public string? City { get; set; } // City where the house is located
    public string? Region { get; set; } // Region where the house is located (like a state)
    public string? Country { get; set; } // Country where the house is located
    public string? ZipCode { get; set; } // ZIP code of the house's location
    public uint? Stars { get; set; }  // How many starts house have (out of 5), always for hotels
    public decimal? Rating { get; set; } // Rating of the house (out of 5)
    public decimal? PricePerNight { get; set; } // Price per night for a room
    public List<string>? GalleryPaths { get; set; } // List of images belong to the house
    public string? HouseAvatarPath { get; set; } // the path of image which is chosen as avatar for the house
    public DateTime? CreatedDate { get; set; } // when this house is added to system 
    public DateTime? UpdatedDate { get; set; } // when this house is updated in system

    public virtual List<Reservation>? Reservations { get; set; } // List of reservations made for the room
    public virtual List<Room>? Rooms { get; set; } // List of rooms available in the house
    public virtual List<Amenity>? Amenities { get; set; } // List of amenities offered by the house
    public virtual List<Review>? Reviews { get; set; } // List of reviews for the house

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }
}