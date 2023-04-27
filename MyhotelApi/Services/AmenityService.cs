using Mapster;
using Microsoft.EntityFrameworkCore;
using MyhotelApi.Database.ConcreteTypeRepositories;
using MyhotelApi.Database.IRepositories;
using MyhotelApi.Objects.Entities;
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

    public async ValueTask<Guid> AddAmenityAsync(CreateAmenityDto createAmenityDto)
    {
        var amenityId = Guid.NewGuid();
        var amenity = createAmenityDto.Adapt<Amenity>();

        amenity.Id = amenityId;

        await unitOfWork.amenityRepository.AddAsync(amenity);

        return amenityId;
    }

    public async ValueTask<AmenityView> GetAmenityByIdAsync(Guid amenityId)
    {
        var amenity = await unitOfWork.amenityRepository.GetAsync(amenityId);
        return amenity.Adapt<AmenityView>();
    }

    public async ValueTask<List<AmenityView>> GetAmenitysAsync(AmenityFilterDto? amenityFilterDto = null)
    {
        var query = unitOfWork.amenityRepository.GetAll();

        if (amenityFilterDto != null)
        {
            if (amenityFilterDto.IsFree != null)
                query = query.Where(a => a.IsFree == amenityFilterDto.IsFree);

            if (amenityFilterDto.AdditionalFee != null)
                query = query.Where(a => a.AdditionalFee == amenityFilterDto.AdditionalFee);

            if (amenityFilterDto.HouseId != null)
                query = query.Where(a => a.HouseId == amenityFilterDto.HouseId);

            if (amenityFilterDto.RoomId != null)
                query = query.Where(a => a.RoomId == amenityFilterDto.RoomId);
        }

        var amenities = await query.ToListAsync();
        return amenities.Select(a => a.Adapt<AmenityView>()).ToList();
    }

    public async ValueTask<AmenityView> UpdateAmenityAsync(UpdateAmenityDto updateAmenityDto)
    {
        var amenity = await unitOfWork.amenityRepository.GetAsync(updateAmenityDto.Id);

        if (updateAmenityDto.Name != null) amenity.Name = updateAmenityDto.Name;
        if (updateAmenityDto.IsFree != null) amenity.IsFree = updateAmenityDto.IsFree;
        // bu yer atayin shunday yozilgan 3 xil holat bor 1) tekin 2) pulli asosisyni ichida 3) pulli alohida agarda tekin qilmoqchi bolsa additional feeni kiritmaydi  va isfree ni true qilib qoyadi.\
        // agarda additional fee ni qoshib va yana isfree ni tekin qilib qoysgan holda, addational fee ga narx assign qilinadi va joy tekin bo`lmagani uchun  isfree false o`zgaradi.*/
        if (updateAmenityDto.AdditionalFee != null)
        {
            if (updateAmenityDto.AdditionalFee > 0)
            {
                amenity.AdditionalFee = updateAmenityDto.AdditionalFee;
                amenity.IsFree = false;
            }
        }

        var updatedAmenity = await unitOfWork.amenityRepository.UpdateAsync(amenity);
        return updatedAmenity.Adapt<AmenityView>();
    }

    public async ValueTask<AmenityView> DeleteAmenityAsync(Guid amenityId)
    {
        var amenity = await unitOfWork.amenityRepository.GetAsync(amenityId);

        var deletedAmenity = await unitOfWork.amenityRepository.RemoveAsync(amenity);

        return deletedAmenity.Adapt<AmenityView>();
    }
}