namespace MyhotelApi.Objects.Entities;

public class AppUser
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string? Password { get; set; }
}  