using Mapster;
using Myhotel.Services;
using MyhotelApi.Database.ConcreteTypeRepositories;
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

    public HouseService(UnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Guid> AddHouseAsync(CreateHouseDto createHouseDto)
    {
        var houseId = Guid.NewGuid();
        var house = createHouseDto.Adapt<House>();

        house.Id = houseId;
        house.Rating = 0;
        house.CreatedDate = DateTime.Now;
        house.UpdatedDate = DateTime.Now;
        house.Status = EHouseStatus.active;

        await unitOfWork.houseRepository.AddAsync(house);

        return houseId;
    }

    public async ValueTask<HouseView> GetHouseByIdAsync(Guid houseId)
    {
        var house = await unitOfWork.houseRepository.GetAsync(houseId);
        return house.Adapt<HouseView>();
    }

    public async ValueTask<List<HouseView>> GetHousesAsync(HouseFilterDto? houseFilterDto = null)
    {
        var query = unitOfWork.houseRepository.GetAll();

        if (houseFilterDto != null)
        {
            if (houseFilterDto.Country != null) query = query.Where(h => h.Country == houseFilterDto.Country);
            if (houseFilterDto.Region  != null) query = query.Where(h => h.Region == houseFilterDto.Region);
            if (houseFilterDto.City    != null) query = query.Where(h => h.City == houseFilterDto.City);
            if (houseFilterDto.Stars   != null) query = query.Where(h => h.Stars == houseFilterDto.Stars);
            if (houseFilterDto.PricePerNight != null) query = query.Where(h => h.PricePerNight == houseFilterDto.PricePerNight);
            if (houseFilterDto.Rating != null)        query = query.Where(h => h.Rating == houseFilterDto.Rating);

            if (houseFilterDto.Amenities != null)
            {
                query = query.Where(h =>
                    !houseFilterDto.Amenities.All(amenity =>
                        !h.Amenities!.Contains(amenity)
                    )
                );
            }

            if (houseFilterDto.Status != null)
            {
                query = houseFilterDto.Status switch
                {
                    EHouseStatus.created  => query = query.Where(h => h.Status == EHouseStatus.created),
                    EHouseStatus.active   => query = query.Where(h => h.Status == EHouseStatus.active),
                    EHouseStatus.inactive => query = query.Where(h => h.Status == EHouseStatus.inactive),
                    EHouseStatus.deleted  => query = query.Where(h => h.Status == EHouseStatus.deleted),
                    _ =>  query
                };
            }

            if (houseFilterDto.SortingType != null)
            {
                query = houseFilterDto.SortingType switch
                {
                    EHouseSorting.Name => query.OrderByDescending(p => p.Name),
                    EHouseSorting.Price => query.OrderByDescending(p => p.PricePerNight),
                    EHouseSorting.Rating => query.OrderByDescending(p => p.Rating),
                    _ => query,
                };
            }
        }

        var houses = await query.ToPagedListAsync(houseFilterDto);

        return houses.Select(h => h.Adapt<HouseView>()).ToList();
    }

    public async ValueTask<HouseView> UpdateHouseAsync(UpdateHouseDto updateHouseDto)
    {
        var house = await unitOfWork.houseRepository.GetAsync(updateHouseDto.Id);

        house!.Id = updateHouseDto.Id;
        house.UpdatedDate = DateTime.Now;

        if (updateHouseDto.Name != null) house.Name = updateHouseDto.Name;
        if (updateHouseDto.Type != null) house.Type = updateHouseDto.Type;
        if (updateHouseDto.Address != null) house.Address = updateHouseDto.Address;
        if (updateHouseDto.City != null) house.City = updateHouseDto.City;
        if (updateHouseDto.Region != null) house.Region = updateHouseDto.Region;
        if (updateHouseDto.Country != null) house.Country = updateHouseDto.Country;
        if (updateHouseDto.Stars != null) house.Stars = updateHouseDto.Stars;
        if (updateHouseDto.ZipCode != null) house.ZipCode = updateHouseDto.ZipCode;

        if (updateHouseDto.PricePerNight != null) house.PricePerNight = updateHouseDto.PricePerNight;
        if (updateHouseDto.GalleryPaths != null) house.GalleryPaths = updateHouseDto.GalleryPaths;
        if (updateHouseDto.HouseAvatarPath != null) house.HouseAvatarPath = updateHouseDto.HouseAvatarPath;

        if (updateHouseDto.Status != null &&
            updateHouseDto.Status != EHouseStatus.created &&
            updateHouseDto.Status != EHouseStatus.deleted) house.Status = updateHouseDto.Status;

        var updatedHouse = await unitOfWork.houseRepository.UpdateAsync(house);

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
            house.Status = EHouseStatus.deleted;
            var updatedHouse = await unitOfWork.houseRepository.UpdateAsync(house);
            return updatedHouse.Adapt<HouseView>();
        }
    }
}