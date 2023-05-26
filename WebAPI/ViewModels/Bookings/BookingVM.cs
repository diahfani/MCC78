using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Utility;

namespace WebAPI.ViewModels.Booking;

public class BookingVM
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public StatusLevel Status { get; set; }
    public string Remarks { get; set; }
    public Guid RoomGuid { get; set; }
    public Guid? EmployeeGuid { get; set; }

}
