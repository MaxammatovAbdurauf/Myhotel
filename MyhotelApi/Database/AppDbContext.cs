using Microsoft.EntityFrameworkCore;
using MyhotelApi.Objects.Entities;

namespace MyhotelApi.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<House> Hotels { get; set; }
    public virtual DbSet<AppUser> Users { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
}