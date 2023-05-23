namespace BookingRooms.Model;
public class Account
{
    public int EmployeeId { get; set; }
    public string? Password { get; set; }
    public bool IsDeleted { get; set; }
    public string Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }
}


