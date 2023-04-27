using Microsoft.EntityFrameworkCore;
using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<House> Houses { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<Reservation> Reservations { get; set; }
    public virtual DbSet<Amenity> Amenities { get; set; }
}