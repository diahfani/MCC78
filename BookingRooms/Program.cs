using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq.Expressions;
using BookingRooms.Config;
using BookingRooms.Model;
using BookingRooms.View;
using Microsoft.VisualBasic;

namespace BookingRooms;

public class Program
{
    // pake store procedure
    public static int InsertAllAtOnce(Employee employee, University universities, Education educations)
    {
        int result = 0;
        using SqlConnection connection = Connection.GetConnection();
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "exec sp_insert_once_booking_rooms @nik, @first_name, @last_name, @birthdate, @gender, @hiring_date, @email, @phone_number, @department_id, @major, @degree, @gpa, @university_name",
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

            var pMajor= new SqlParameter
            {
                ParameterName = "@major",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = educations.Major
            };

            var pDegree = new SqlParameter
            {
                ParameterName = "@degree",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = educations.Degree
            };

            var pGpa = new SqlParameter
            {
                ParameterName = "@gpa",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = educations.Gpa
            };

            var pNameUniv = new SqlParameter
            {
                ParameterName = "@university_name",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = universities.Name
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
            command.Parameters.Add(pMajor);
            command.Parameters.Add(pDegree);
            command.Parameters.Add(pGpa);
            command.Parameters.Add(pNameUniv);
            

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

    public static void Main()
    {
        var getMenu = new MenuView();
        getMenu.GetMenu();

    }


}


