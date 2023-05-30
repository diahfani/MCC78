using System.ComponentModel.DataAnnotations;
using WebAPI.Utility;

namespace WebAPI.ViewModels.Accounts;

public class RegisterVM
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }

    public DateTime HiringDate { get; set; }
    [EmailAddress]
    [NIKEmailPhoneValidation(nameof(Email))]
    // validation bisa duplikat
    public string Email { get; set; }
    [Phone]
    [NIKEmailPhoneValidation(nameof(PhoneNumber))]
    public string PhoneNumber { get; set; }

    public string Major { get; set; }

    public string Degree { get; set; }
    [Range(0,4, ErrorMessage = "Value is ")]
    public float GPA { get; set; }

    //public Guid UniversityGuid { get; set; }

    public string UniversityCode { get; set; }

    public string UniversityName { get; set; }

    [PasswordValidation(ErrorMessage = "Password must contain minimal 1 uppercase, 1 symbol, 1 number and have 6 characters")]
    public string Password { get; set; }

    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    // public University? University { get; set; }

}
