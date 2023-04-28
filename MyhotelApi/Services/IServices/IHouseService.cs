using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;

namespace MyhotelApi.Services.IServices;

public interface IHouseService
{
    ValueTask<HouseView> AddHouseAsync(CreateHouseDto createHouseDto);
    ValueTask<HouseView> GetHouseByIdAsync(Guid houseId);
    ValueTask<List<HouseView>> GetHousesAsync(HouseFilterDto? houseFilterDto = null);
    ValueTask<HouseView> UpdateHouseAsync(UpdateHouseDto updateHouseDto);
    ValueTask<HouseView> DeleteHouseAsync(Guid houseId, bool deleteFromDataBase = false);
}