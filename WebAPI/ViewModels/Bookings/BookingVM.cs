using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Utility;

namespace WebAPI.ViewModels.Booking;

public class BookingVM
{
    [BookingDateValidation]
    public DateTime StartDate { get; set; }
    [BookingDateValidation]
    public DateTime EndDate { get; set; }
    public StatusLevel Status { get; set; }
    public string Remarks { get; set; }
    [Required]
    public Guid RoomGuid { get; set; }
    public Guid? EmployeeGuid { get; set; }

}
