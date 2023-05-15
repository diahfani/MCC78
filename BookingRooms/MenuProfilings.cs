using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms;

public class MenuProfilings
{
    private readonly static string connectionString =
"Data Source=DIAH;Database=db_booking_rooms_mcc;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

    public static int InsertProfilings(Employees employee, Educations education)
    {
        int result = 0;
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO tb_m_profilings VALUES (@employee_id, @education_id)";
            command.Transaction = transaction;

            var pEmployeeId = new SqlParameter
            {
                ParameterName = "@employee_id",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = employee.Id
            };

            var pEducationId = new SqlParameter();
            pEmployeeId.ParameterName = "@education_id";
            pEmployeeId.SqlDbType = System.Data.SqlDbType.Int;
            pEmployeeId.Value = education.Id;

            command.Parameters.Add(pEmployeeId);
            command.Parameters.Add(pEducationId);

            result = command.ExecuteNonQuery();
            transaction.Commit();
            return result;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            transaction.Rollback();
        }
        finally
        {
            connection.Close();
        }
        // return new List<Universities>();
        return result;

    }
}

