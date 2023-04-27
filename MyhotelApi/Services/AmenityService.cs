using MyhotelApi.Database.ConcreteTypeRepositories;
using MyhotelApi.Database.IRepositories;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;
using MyhotelApi.Services.IServices;

namespace MyhotelApi.Services;

public class AmenityService : IAmenityService
{
    private readonly IUnitOfWork unitOfWork;

    public AmenityService(UnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public ValueTask<Guid> AddAmenityAsync(CreateAmenityDto createAmenityDto)
    {
        throw new NotImplementedException();
    }

    public ValueTask DeleteAmenityAsync(Guid amenityId, bool deleteFromDataBase = false)
    {
        throw new NotImplementedException();
    }

    public ValueTask<AmenityView> GetAmenityByIdAsync(Guid amenityId)
    {
        throw new NotImplementedException();
    }

    public ValueTask<List<AmenityView>> GetAmenitysAsync()
    {
        throw new NotImplementedException();
    }

    public ValueTask<AmenityView> UpdateAmenityAsync(UpdateAmenityDto updateAmenityDto)
    {
        throw new NotImplementedException();
    }
}