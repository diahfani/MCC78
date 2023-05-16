namespace BookingRooms.Model;
public class Booking
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Remarks { get; set; }
    public int RoomId { get; set; }
    public int EmployeeId { get; set; }

}

