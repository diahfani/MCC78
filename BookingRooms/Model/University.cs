﻿using BookingRooms.Config;
using System.Data.SqlClient;

namespace BookingRooms.Model;
public class University
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Get All Universities
    public List<University> GetUniversities()
    {
        // generic type universities diambil dari class universities
        // buat instance list untuk menampung data dari database
        var universities = new List<University>();
        // buat instance connection menggunakan using agar sifatnya disposable
        using SqlConnection connection = Connection.GetConnection(); 
        try
        {
            // buat instance sql command yang mana command ini digunakan untuk memanggil database atau membuat query
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Select * from tb_m_universities";
            connection.Open();

            // buat instance sqldatareader yang fungsinya untuk menampilkan row2 yg ada di database
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //Console.WriteLine(reader.GetInt32(0));
                    //Console.WriteLine(reader.GetString(1));
                    var university = new University();
                    university.Id = reader.GetInt32(0);
                    university.Name = reader.GetString(1);
                    universities.Add(university);
                }
                return universities;
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


        return new List<University>();
    }
    // Get by Id universities
    public void GetByIdUniversity(University universities)
    {
        // var university = new List<Universities>();
        using SqlConnection connection = Connection.GetConnection();
        try
        {
            // buat instance command dan query select by id
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Select * from tb_m_universities where id = @id";

            // buat instance parameter untuk memanggil universities by id
            var pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = universities.Id;

            // tambahkan parameter yang sudah dibuat ke dalam command
            command.Parameters.Add(pId);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id : " + reader.GetInt32(0));
                    Console.WriteLine("Nama : " + reader.GetString(1));
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
        // return new List<Universities>();
    }

    // Get by Name Universities
    public List<University> GetIDByNameUniversity(University universities)
    {
        var university = new List<University>();
        using SqlConnection connection = Connection.GetConnection();
        try
        {
            // buat instance command dan query select by id
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Select id from tb_m_universities where name = @name";

            // buat instance parameter untuk memanggil universities by id
            var pName = new SqlParameter
            {
                ParameterName = "@name",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = universities.Name
            };

            // tambahkan parameter yang sudah dibuat ke dalam command
            command.Parameters.Add(pName);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                // idUniv = reader.GetInt32(0);
                while (reader.Read())
                {

                    var Univ = new University
                    {
                        Id = reader.GetInt32(0)
                    };
                    university.Add(Univ);
                }
                return university;
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
        return new List<University>();
        // return new List<Universities>();
    }

    // Insert into Universities
    public int InsertUniversity(University universities)
    {
        int result = 0;
        using SqlConnection connection = Connection.GetConnection();
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO tb_m_universities(name) VALUES (@name)";
            command.Transaction = transaction;

            var pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Size = 100;
            pName.Value = universities.Name;

            command.Parameters.Add(pName);

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

    // Update Universities (by id)
    public int UpdateByIdUniversities(University universities)
    {
        // var university = new List<Universities>();
        int result = 0;
        using SqlConnection connection = Connection.GetConnection();
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE tb_m_universities SET name = @name WHERE id = @id";
            command.Transaction = transaction;

            var pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = universities.Id;

            var pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Size = 100;
            pName.Value = universities.Name;

            command.Parameters.Add(pId);
            command.Parameters.Add(pName);

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

    // delete universities by id
    public int DeleteByIdUniversities(University universities)
    {
        int result = 0;
        using SqlConnection connection = Connection.GetConnection();
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM tb_m_universities WHERE id = @id";
            command.Transaction = transaction;

            var pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = universities.Id;

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


