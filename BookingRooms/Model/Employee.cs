using BookingRooms.Config;
using System.Data.SqlClient;

namespace BookingRooms.Model;
public class Employee
{
    public Guid Id { get; set; }
    public string Nik { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public string Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneMumber { get; set; }
    public string DepartmentId { get; set; }

    public int InsertEmployee(Employee employee)
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

    public List<Employee> GetEmployees()
    {
        var employees = new List<Employee>();
        using SqlConnection connection = Connection.GetConnection();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Select * from tb_m_employees";
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var emp = new Employee
                    {
                        Id = reader.GetGuid(0),
                        Nik = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        LastName = reader.GetString(3),
                        Birthdate = reader.GetDateTime(4),
                        Gender = reader.GetString(5),
                        HiringDate = reader.GetDateTime(6),
                        Email = reader.GetString(7),
                        PhoneMumber = reader.GetString(8),
                        DepartmentId = reader.GetString(9),
                    };
                    employees.Add(emp);
                }
                return employees;
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


        return new List<Employee>();
    }

    //GET ID EMPLOYEE BY NIK
    public List<Employee> GetIdByNikEmployee(Employee employee)
    {
        var result = new List<Employee>();
        using SqlConnection connection = Connection.GetConnection();
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
                    var Employee = new Employee
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
        return new List<Employee>();
    }

}


