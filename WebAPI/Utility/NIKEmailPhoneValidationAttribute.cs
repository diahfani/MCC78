using System.ComponentModel.DataAnnotations;
using WebAPI.Contracts;

namespace WebAPI.Utility;

public class NIKEmailPhoneValidationAttribute : ValidationAttribute
{
    private readonly string _propertyName;

    public NIKEmailPhoneValidationAttribute(string propertyName)
    {
        _propertyName = propertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)  return new ValidationResult($"{_propertyName} is required");
        var employeeRepository = validationContext.GetService(typeof(IEmployeeRepository)) as IEmployeeRepository;
        var checkEmailandPhone = employeeRepository.CheckEmailAndPhoneAndNIK(value.ToString());
        if (checkEmailandPhone) return new ValidationResult($"{_propertyName} '{value}' already exists.");
        return ValidationResult.Success;
    }
}
