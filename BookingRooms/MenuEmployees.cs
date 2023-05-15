using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms;

public class MenuEmployees
{
    private readonly static string connectionString =
    "Data Source=DIAH;Database=db_booking_rooms_mcc;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

    public static int InsertEmployee(Employees employee)
    {
        int result = 0;
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "INSERT INTO tb_m_employees VALUES (default, @nik, @first_name, @last_name, @birthdate, @gender, @hiring_date, @email, @phone_number, @department_id)",
                Transaction = transaction
            };

            var pNik = new SqlParameter
            {
                ParameterName = "@nik",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 10,
                Value = employee.Nik
            };

            var pFirstName = new SqlParameter
            {
                ParameterName = "@first_name",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 50,
                Value = employee.FirstName
            };

            var pLastName = new SqlParameter
            {
                ParameterName = "@last_name",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 50,
                Value = employee.LastName
            };

            var pBirthdate = new SqlParameter
            {
                ParameterName = "@birthdate",
                SqlDbType = System.Data.SqlDbType.DateTime,
                Value = employee.Birthdate
            };

            var pGender = new SqlParameter
            {
                ParameterName = "@gender",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 50,
                Value = employee.Gender
            };

            var pHiringDate = new SqlParameter
            {
                ParameterName = "@hiring_date",
                SqlDbType = System.Data.SqlDbType.DateTime,
                Size = 50,
                Value = employee.HiringDate
            };

            var pEmail = new SqlParameter
            {
                ParameterName = "@email",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 50,
                Value = employee.Email
            };

            var pPhoneNumber = new SqlParameter
            {
                ParameterName = "@phone_number",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = employee.PhoneMumber
            };

            var pDepartmentID = new SqlParameter
            {
                ParameterName = "@department_id",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = employee.DepartmentId
            };


            command.Parameters.Add(pNik);
            command.Parameters.Add(pFirstName);
            command.Parameters.Add(pLastName);
            command.Parameters.Add(pBirthdate);
            command.Parameters.Add(pGender);
            command.Parameters.Add(pHiringDate);
            command.Parameters.Add(pEmail);
            command.Parameters.Add(pPhoneNumber);
            command.Parameters.Add(pDepartmentID);

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

    //GET ID EMPLOYEE BY NIK
    public static List<Employees> GetIdByNikEmployee(Employees employee)
    {
        var result = new List<Employees>();
        using SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Select id from tb_m_employees where nik = @nik";

            var pNik = new SqlParameter
            {
                ParameterName = "@nik",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 10,
                Value = employee.Nik
            };


            command.Parameters.Add(pNik);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    /*Console.WriteLine("Id : " + reader.GetInt32(0));
                    Console.WriteLine("Nama : " + reader.GetString(1));*/
                    var Employee = new Employees
                    {
                        Id = reader.GetGuid(0)
                    };
                    result.Add(Employee);
                }
                return result;
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
        return new List<Employees>();
    }

}
