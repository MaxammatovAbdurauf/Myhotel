using MyhotelApi.Database.Repositories;
using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database.ConcreteTypeRepositories;

public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(AppDbContext context) : base(context) { }
}