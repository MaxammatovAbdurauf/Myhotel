using Myhotel.Objects.Enums;
using Myhotel.Objects.Views;
using Myhotel.Objects.Options;

namespace Myhotel.Objects.Models;

public class HouseFilterDto : PaginationParams
{
    public EHouseType? Type { get; set; } // Type of the house (like hotel, motel and others)
    public EHouseStatus? Status { get; set; } // Status of the house (like active, deleted and others)
    public EHouseSorting? SortingType { get; set; }
    public string? City { get; set; } // City where the house is located
    public string? Region { get; set; } // Region where the house is located (like a state)
    public string? Country { get; set; } // Country where the house is located
    public uint? Stars { get; set; }  // How many starts house have (out of 5), always for hotels
    public decimal? Rating { get; set; } // Rating of the house (out of 5)
    public decimal? PricePerNight { get; set; } // Price per night for a room
    public List<AmenityView>? Amenities { get; set; }
}