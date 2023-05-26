using WebAPI.Model;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Contracts;

public interface IRoomRepository : IGenericRepository<Room>
{
    IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);
    IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();
    IEnumerable<RoomBookedTodayVM> GetRoomByDate();
}
