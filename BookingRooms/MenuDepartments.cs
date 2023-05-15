using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms;

public class MenuDepartments
{
    private readonly static string connectionString =
        "Data Source=DIAH;Database=db_booking_rooms_mcc;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
    public static List<Departments> GetDepartments()
    {
        var departments = new List<Departments>();
        using SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "Select * from tb_m_departments"
            };
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var department = new Departments
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1)
                    };
                    departments.Add(department);
                }
                return departments;
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }


        return new List<Departments>();
    }
}
