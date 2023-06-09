﻿using BookingRooms.Config;
using System.Data.SqlClient;

namespace BookingRooms.Model;
public class Education
{
    public int Id { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public string Gpa { get; set; }
    public int? UniversityId { get; set; }

    // GET ALL EDUCATIONS
    public List<Education> GetEducations()
    {
        var educations = new List<Education>();
        using SqlConnection connection = Connection.GetConnection();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Select * from tb_m_educations";
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var education = new Education
                    {
                        Id = reader.GetInt32(0),
                        Major = reader.GetString(1),
                        Degree = reader.GetString(2),
                        Gpa = reader.GetString(3),
                        UniversityId = reader.GetInt32(4)
                    };
                    educations.Add(education);
                }
                return educations;
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


        return new List<Education>();
    }

    // GET BY ID EDUCATION
    public void GetByIdEducation(Education education)
    {
        using SqlConnection connection = Connection.GetConnection(); 
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Select * from tb_m_educations where id = @id";

            var pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = education.Id;

            command.Parameters.Add(pId);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("ID : " + reader.GetInt32(0));
                    Console.WriteLine("Major : " + reader.GetString(1));
                    Console.WriteLine("Degree : " + reader.GetString(2));
                    Console.WriteLine("GPA : " + reader.GetString(3));
                    Console.WriteLine("University ID : " + reader.GetInt32(4));
                }
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
    }

    // GET ID BY NAME EDUCATION
    public List<Education> GetIdByMajorEducation(Education education)
    {
        var educations = new List<Education>();
        using SqlConnection connection = Connection.GetConnection();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Select id from tb_m_educations where major = @major and degree = @degree and gpa = @gpa";

            var pMajor = new SqlParameter
            {
                ParameterName = "@major",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = education.Major
            };

            var pDegree = new SqlParameter
            {
                ParameterName = "@degree",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = education.Degree
            };

            var pGpa = new SqlParameter
            {
                ParameterName = "@gpa",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = education.Gpa
            };

            /* var pUniversityId = new SqlParameter
             {
                 ParameterName = "@university_id",
                 SqlDbType = System.Data.SqlDbType.Int,
                 Value = education.UniversityId
             };*/

            command.Parameters.Add(pMajor);
            command.Parameters.Add(pDegree);
            command.Parameters.Add(pGpa);
            /*command.Parameters.Add(pUniversityId);*/
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    /*Console.WriteLine("Id : " + reader.GetInt32(0));
                    Console.WriteLine("Nama : " + reader.GetString(1));*/
                    var Edu = new Education
                    {
                        Id = reader.GetInt32(0)
                    };
                    educations.Add(Edu);
                }
                return educations;
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
        return educations;
    }


    // INSERT EDUCATION
    public int InsertEducation(Education education)
    {
        int result = 0;
        using SqlConnection connection = Connection.GetConnection(); ;
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO tb_m_educations VALUES (@major, @degree, @gpa, @university_id)";
            command.Transaction = transaction;

            var pMajor = new SqlParameter
            {
                ParameterName = "@major",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = education.Major
            };
            var pDegree = new SqlParameter
            {
                ParameterName = "@degree",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 10,
                Value = education.Degree
            };
            var pGpa = new SqlParameter
            {
                ParameterName = "@gpa",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 5,
                Value = education.Gpa
            };
            var pIdUniv = new SqlParameter
            {
                ParameterName = "@university_id",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = education.UniversityId
            };

            command.Parameters.Add(pMajor);
            command.Parameters.Add(pDegree);
            command.Parameters.Add(pGpa);
            command.Parameters.Add(pIdUniv);

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


    // UPDATE BY ID EDUCATION
    public int UpdateByIdEducation(Education education)
    {
        // var university = new List<Universities>();
        int result = 0;
        using SqlConnection connection = Connection.GetConnection(); 
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "UPDATE tb_m_educations SET major = @major, degree = @degree, gpa = @gpa, university_id = @university_id WHERE id = @id",
                Transaction = transaction
            };

            var pId = new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = education.Id
            };

            var pMajor = new SqlParameter
            {
                ParameterName = "@major",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100,
                Value = education.Major
            };
            var pDegree = new SqlParameter
            {
                ParameterName = "@degree",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 10,
                Value = education.Degree
            };
            var pGpa = new SqlParameter
            {
                ParameterName = "@gpa",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 5,
                Value = education.Gpa
            };
            var pIdUniv = new SqlParameter
            {
                ParameterName = "@university_id",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = education.UniversityId
            };

            command.Parameters.Add(pId);
            command.Parameters.Add(pMajor);
            command.Parameters.Add(pDegree);
            command.Parameters.Add(pGpa);
            command.Parameters.Add(pIdUniv);

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


    // DELETE BY ID EDUCATION
    public int DeleteByIdEducation(Education educations)
    {
        int result = 0;
        using SqlConnection connection = Connection.GetConnection(); 
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM tb_m_educations WHERE id = @id";
            command.Transaction = transaction;

            var pId = new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = educations.Id
            };

            command.Parameters.Add(pId);

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

