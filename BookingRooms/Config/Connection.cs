using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Config;

public class Connection
{
    private readonly static string connectionString =
       "Data Source=DIAH;Database=db_booking_rooms_mcc;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}
