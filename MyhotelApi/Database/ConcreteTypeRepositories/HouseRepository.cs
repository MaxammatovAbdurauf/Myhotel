using MyhotelApi.Database.Repositories;
using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database.ConcreteTypeRepositories;

public class HouseRepository : GenericRepository<House>, IHouseRepository
{
    public HouseRepository(AppDbContext context) : base(context) { }
}