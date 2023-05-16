using BookingRooms.Config;
using System.Data.SqlClient;

namespace BookingRooms.Model;
public class Profiling
{
    public Guid EmployeeId { get; set; }
    public int EducationId { get; set; }

    public int InsertProfilings(Profiling profilings)
    {
        int result = 0;
        using SqlConnection connection = Connection.GetConnection();
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO tb_tr_profilings VALUES (@employee_id, @education_id)";
            command.Transaction = transaction;

            var pEmployeeId = new SqlParameter
            {
                ParameterName = "@employee_id",
                SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                Value = profilings.EmployeeId
            };

            var pEducationId = new SqlParameter();
            pEducationId.ParameterName = "@education_id";
            pEducationId.SqlDbType = System.Data.SqlDbType.Int;
            pEducationId.Value = profilings.EducationId;

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

    public List<Profiling> GetProfilings()
    {
        var profilings = new List<Profiling>();
        using SqlConnection connection = Connection.GetConnection();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from tb_tr_profilings";
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var prof = new Profiling();
                    prof.EmployeeId = reader.GetGuid(0);
                    prof.EducationId = reader.GetInt32(1);
                    profilings.Add(prof);
                }
                return profilings;
            }


        }
        catch (Exception ex)
        {

        }
        finally
        {
            connection.Close();
        }
        return new List<Profiling>();
    }

}


