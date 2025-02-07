using System.ComponentModel.DataAnnotations;

namespace PB202_Pronia.ViewModels;

public class LoginVM
{
    [Required]
    public string EmailOrUsername { get; set; } = null!;
    [Required]

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}

public class RegisterVM
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}
