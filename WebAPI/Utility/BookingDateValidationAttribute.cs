using System.ComponentModel.DataAnnotations;

namespace WebAPI.Utility;

public class BookingDateValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var date = (DateTime)value;
        if (date == null) return false;
        return date >= DateTime.Now;
    }
}
