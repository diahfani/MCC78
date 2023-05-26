using WebAPI.Model;
using WebAPI.ViewModels.Bookings;

namespace WebAPI.Contracts;

public interface IBookingRepository : IGenericRepository<Booking>
{
    IEnumerable<BookingDurationVM> GetBookingDuration();
    BookingDetailVM GetBookingDetailByGuid(Guid guid);
    IEnumerable<BookingDetailVM> GetAllBookingDetail();
}
