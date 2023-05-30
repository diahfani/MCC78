using WebAPI.Utility;

namespace WebAPI.ViewModels.Accounts;

public class AccountVM
{
    public Guid Guid { get; set; }
    [PasswordValidation]
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredDate { get; set; }
}
