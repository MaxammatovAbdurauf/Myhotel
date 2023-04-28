namespace MyhotelApi.Objects.Models;

public class SignInUserDto
{
    public string? Email { get; set; }
    public string UserName { get; set; }
    public string? Password { get; set; }
    public long? PhoneNumber { get; set; }
}