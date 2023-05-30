using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace WebAPI.Utility;

public class PasswordValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var password = (string)value;
        if (password == null) return false;

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[0-9]+");
        var hasMinimum6Chars = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasLowerChar.IsMatch(password)
            && hasSymbols.IsMatch(password) && hasMinimum6Chars.IsMatch(password);


        return isValidated;
    }
}
