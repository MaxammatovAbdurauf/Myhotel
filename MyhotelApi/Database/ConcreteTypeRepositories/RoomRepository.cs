using MyhotelApi.Database.Repositories;
using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database.ConcreteTypeRepositories;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    public RoomRepository(AppDbContext context) : base(context)  {   }
}