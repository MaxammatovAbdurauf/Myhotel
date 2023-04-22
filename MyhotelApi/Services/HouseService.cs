using Mapster;
using MyhotelApi.Database.IRepositories;
using MyhotelApi.Helpers.AddServiceFromAttribute;
using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Enums;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;
using MyhotelApi.Services.IServices;

namespace MyhotelApi.Services;

[Scoped]
public class HouseService : IHouseService
{
    private readonly IUnitOfWork unitOfWork;

    public HouseService (IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Guid> AddHouseAsync(CreateHouseDto createHouseDto)
    {
        var houseId = Guid.NewGuid();
        var house   = createHouseDto.Adapt<House>();

        house.Id    = houseId;
        house.Rating = 0;
        house.CreatedDate = DateTime.Now;
        house.Status = HouseStatus.active;

        //bunday namedagi house bor yoki yo`qligini validationda tekshir
        await unitOfWork.houseRepository.AddAsync(house);

        return houseId;
    }

    public async ValueTask<HouseView> GetHouseByIdAsync(Guid houseId)
    {
       var house = await unitOfWork.houseRepository.GetAsync(houseId);
       return house.Adapt<HouseView>();
    }

    public async ValueTask<ICollection<HouseView>> GetHousesAsync(HouseFilterDto? houseFilterDto = null)
    {
        var houses = await unitOfWork.houseRepository.GetAllAsync();
        return houses.Select(h => h.Adapt<HouseView>()).ToList();
    }

    public async ValueTask<HouseView> UpdateHouseAsync(UpdateHouseDto updateHouseDto)
    {
        var house = await unitOfWork.houseRepository.GetAsync(updateHouseDto.Id);

        house!.Id = updateHouseDto.Id;
        house.UpdatedDate = DateTime.Now;

        if (updateHouseDto.Name != null)    house.Name    = updateHouseDto.Name;
        if (updateHouseDto.Type != null)    house.Type    = updateHouseDto.Type;
        if (updateHouseDto.Address != null) house.Address = updateHouseDto.Address;
        if (updateHouseDto.City != null)    house.City    = updateHouseDto.City;
        if (updateHouseDto.Region != null)  house.Region  = updateHouseDto.Region;
        if (updateHouseDto.Country!= null)  house.Country = updateHouseDto.Country;
        if (updateHouseDto.Stars != null)   house.Stars   = updateHouseDto.Stars;
        if (updateHouseDto.ZipCode != null) house.ZipCode = updateHouseDto.ZipCode;

        if (updateHouseDto.PricePerNight != null)   house.PricePerNight = updateHouseDto.PricePerNight;
        if (updateHouseDto.GalleryPaths != null)    house.GalleryPaths  = updateHouseDto.GalleryPaths;
        if (updateHouseDto.HouseAvatarPath != null) house.HouseAvatarPath = updateHouseDto.HouseAvatarPath;

        if (updateHouseDto.Status != null && 
            updateHouseDto.Status != HouseStatus.created &&
            updateHouseDto.Status != HouseStatus.deleted) house.Status = updateHouseDto.Status;

        var updatedHouse  = await unitOfWork.houseRepository.UpdateAsync(house);

        return updateHouseDto.Adapt<HouseView>();
    }

    public async ValueTask<HouseView> DeleteHouseAsync(Guid houseId, bool deleteFromDataBase = false)
    {
        var house = await unitOfWork.houseRepository.GetAsync(houseId);

        if (deleteFromDataBase)
        {
            var deletedHouse = await unitOfWork.houseRepository.RemoveAsync(house);
            return deletedHouse.Adapt<HouseView>();
        }
        else
        {
            house.Status = HouseStatus.deleted;
            var updatedHouse = await unitOfWork.houseRepository.UpdateAsync(house);
            return updatedHouse.Adapt<HouseView>();
        }     
    }
}