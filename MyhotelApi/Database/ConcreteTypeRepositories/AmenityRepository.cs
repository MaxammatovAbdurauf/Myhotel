using MyhotelApi.Database.Repositories;
using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database.ConcreteTypeRepositories;

public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
{
    public AmenityRepository(AppDbContext context) : base(context) { }
}