using MyhotelApi.Database.Repositories;
using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database.ConcreteTypeRepositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository (AppDbContext context) : base (context) {   }
}