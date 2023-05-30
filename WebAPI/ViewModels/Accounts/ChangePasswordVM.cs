using System.ComponentModel.DataAnnotations;
using WebAPI.Utility;

namespace WebAPI.ViewModels.Accounts;

public class ChangePasswordVM
{
    [Required]
    public string Email { get; set; }
    public int OTP { get; set; }
    [PasswordValidation]
    public string NewPassword { get; set; }
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; }
}
