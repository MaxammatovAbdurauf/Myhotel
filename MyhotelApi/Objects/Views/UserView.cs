﻿namespace MyhotelApi.Objects.Views;

public class UserView
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string? Password { get; set; }
    public long? PhoneNumber { get; set; }
}