using Microsoft.EntityFrameworkCore;
using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database;

public class AppDbContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<House> Hotels { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Amenity> Amenities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        /*builder.Entity<Category>()
                .HasData(new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "default category",
                    Description = "this category is used as default",
                    IsPublic = true,
                    Templates = new List<Template>(),
                });*/
    }
}