
using Microsoft.AspNetCore.Identity;

namespace RetroGamingCatalog.Dao;

public class User
{
    public Guid Id { get; set; }
    public DateTime RegistrationDate { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public UserTypeEnum UserType { get; set; }
    public DateTime LastConnectionDate { get; set; }
    public bool Locked { get; set; }
    public enum UserTypeEnum
    {
        Default,
        Admin
    }
}
