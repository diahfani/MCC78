using WebAPI.Model;
using WebAPI.ViewModels.Bookings;

namespace WebAPI.Contracts;

public interface IBookingRepository : IGenericRepository<Booking>
{
    IEnumerable<BookingRoomVM> GetBookingLength();
}
