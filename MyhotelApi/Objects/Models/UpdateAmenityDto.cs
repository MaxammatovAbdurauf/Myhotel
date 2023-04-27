namespace MyhotelApi.Objects.Models;

public class UpdateAmenityDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; } // Name of the amenity (e.g. "Swimming Pool", "Fitness Center")
    public decimal? AdditionalFee { get; set; }
    public bool? IsFree { get; set; } // Indicates whether the amenity is free or requires an additional fee
}