using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;

namespace MyhotelApi.Services.IServices;

public interface IAmenityService
{
    ValueTask<Guid> AddAmenityAsync(CreateAmenityDto createAmenityDto);
    ValueTask<List<AmenityView>> GetAmenitysAsync();
    ValueTask<AmenityView> GetAmenityByIdAsync(Guid amenityId);
    ValueTask<AmenityView> UpdateAmenityAsync(UpdateAmenityDto updateAmenityDto);
    ValueTask DeleteAmenityAsync(Guid amenityId, bool deleteFromDataBase = false);
}