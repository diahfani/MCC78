using BookingRooms.Config;
using System.Data.SqlClient;

namespace BookingRooms.Model;
public class Department
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Department> GetDepartments()
    {
        var departments = new List<Department>();
        using SqlConnection connection = Connection.GetConnection();
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
                    var department = new Department
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


        return new List<Department>();
    }
}
