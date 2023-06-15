using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Model;
using WebAPI.Utility;

namespace WebAPI.ViewModels.Employees;

public class EmployeeVM
{
    public Guid Guid { get; set; }
    [NIKEmailPhoneValidation(nameof(Nik))]
    public string Nik { get; set; }
    public string FirstName { get; set; }
    // (?) atau mark question berarti nullable
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }
    public DateTime HiringDate { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }

}
