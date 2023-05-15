using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace BookingRooms;

public class Program
{
    private readonly static string connectionString =
        "Data Source=DIAH;Database=db_booking_rooms_mcc;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
    public static void GetMenu()
    {
        var university = new Universities();
        var education = new Educations();
        var employee = new Employees();
        Console.WriteLine("Selamat datang, Admin!");
        Console.WriteLine("Silahkan pilih data yang ingin dimasukkan :");
        Console.WriteLine("1. University\n2. Education\n3. Insert Data Sekaligus");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("Silahkan pilih menu :");
                Console.WriteLine("1. Tambah data Universitas\n2. Get All Universitas\n3. Get By ID Universitas\n4. Update Nama Universitas\n5. Delete Universitas");
                var newChoice = Console.ReadLine();
                if (newChoice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Masukkan nama Universitas yang ingin ditambah :");
                    university.Name = Console.ReadLine();
                    Console.WriteLine("Nama Universitas yang di input: " + university.Name);
                    var resultInsertUniv = MenuUniversities.InsertUniversity(university);
                    if (resultInsertUniv > 0)
                    {
                        Console.WriteLine("Insert success!");
                    }
                    else
                    {
                        Console.WriteLine("Insert failed!");
                    }
                }
                if (newChoice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Get All Universities");

                    var resultsUniv = MenuUniversities.GetUniversities();
                    foreach (var results in resultsUniv)
                    {
                        Console.WriteLine("Id : " + results.Id);
                        Console.WriteLine("Nama : " + results.Name);
                    }
                    
                }
                if (newChoice == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Get University By Id");
                    var id = Console.ReadLine();
                    university.Id = Convert.ToInt32(id);
                    Console.WriteLine("Id yang dicari : " + university.Id);
                    MenuUniversities.GetByIdUniversity(university);
                }
                if (newChoice == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Update Universities By Id");
                    Console.Write("Masukkan ID Universitas yang ingin di update: ");
                    var id = Console.ReadLine();
                    Console.Write("Masukkan nama universitas yang terbaru: ");
                    university.Name = Console.ReadLine();
                    university.Id = Convert.ToInt32(id);
                    Console.WriteLine("ID yang mau diubah :" + university.Id);
                    Console.WriteLine("Nama universitas terbaru :" + university.Name);
                    var resultUpdateUniv = MenuUniversities.UpdateByIdUniversities(university);
                    if (resultUpdateUniv > 0)
                    {
                        Console.WriteLine("Update success!");
                    }
                    else
                    {
                        Console.WriteLine("Update failed!");
                    }
                }
                if (newChoice == "5")
                {
                    Console.Clear();
                    Console.WriteLine("Delete Universities By Id");
                    Console.Write("Masukkan ID Universitas yang ingin di hapus: ");
                    var id = Console.ReadLine();
                    university.Id = Convert.ToInt32(id);
                    Console.WriteLine("ID yang mau dihapus :" + university.Id);
                    var resultDelUniv = MenuUniversities.DeleteByIdUniversities(university);
                    if (resultDelUniv > 0)
                    {
                        Console.WriteLine("Delete success!");
                    }
                    else
                    {
                        Console.WriteLine("Delete failed!");
                    }
                }
                break;

            case "2":
                Console.Clear();
                Console.WriteLine("Silahkan pilih menu :");
                Console.WriteLine("1. Tambah data education\n2. Get All Educations\n3. Get By ID Education\n4. Update Educations\n5. Delete Education");
                var newChoiceSecond = Console.ReadLine();
                if (newChoiceSecond == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Masukkan major :");
                    education.Major = Console.ReadLine();
                    Console.WriteLine("Masukkan degree :");
                    education.Degree = Console.ReadLine();
                    Console.WriteLine("Masukkan GPA :");
                    education.Gpa = Console.ReadLine();
                    Console.WriteLine("Masukkan University ID :");
                    var universityId = Console.ReadLine();
                    if (universityId == null)
                    {
                        education.UniversityId = null;
                    }
                    education.UniversityId = Convert.ToInt32(universityId);
                    Console.WriteLine("Major yang di input: " + education.Major);
                    Console.WriteLine("Degree yang di input: " + education.Degree);
                    Console.WriteLine("GPA yang di input: " + education.Gpa);
                    Console.WriteLine("ID Universitas yang di input: " + education.UniversityId);
                    var resultInsertEdu = MenuEducation.InsertEducation(education);
                    if (resultInsertEdu > 0)
                    {
                        Console.WriteLine("Insert success!");
                    }
                    else
                    {
                        Console.WriteLine("Insert failed!");
                    }
                }
                if (newChoiceSecond == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Get All Universities");

                    var resultsEdu = MenuEducation.GetEducations();
                    foreach (var resultsE in resultsEdu)
                    {
                        Console.WriteLine("Id : " + resultsE.Id);
                        Console.WriteLine("Major : " + resultsE.Major);
                        Console.WriteLine("Degree : " + resultsE.Degree);
                        Console.WriteLine("GPA : " + resultsE.Gpa);
                        Console.WriteLine("ID University : "+ resultsE.UniversityId);
                    }

                }
                if (newChoiceSecond == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Get Education By Id");
                    var id = Console.ReadLine();
                    education.Id = Convert.ToInt32(id);
                    Console.WriteLine("Id yang dicari : " + education.Id);
                    MenuEducation.GetByIdEducation(education);
                }
                if (newChoiceSecond == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Update Education By Id");
                    Console.Write("Masukkan ID Education yang ingin di update: ");
                    var id = Console.ReadLine();
                    education.Id = Convert.ToInt32(id);
                    Console.WriteLine("Masukkan major yang baru :");
                    education.Major = Console.ReadLine();
                    Console.WriteLine("Masukkan degree yang baru :");
                    education.Degree = Console.ReadLine();
                    Console.WriteLine("Masukkan GPA yang baru :");
                    education.Gpa = Console.ReadLine();
                    Console.WriteLine("Masukkan University ID yang baru:");
                    var universityId = Console.ReadLine();
                    if (universityId == null)
                    {
                        education.UniversityId = null;
                    }
                    education.UniversityId = Convert.ToInt32(universityId);
                    Console.WriteLine("ID yang diperbarui: " + education.Id);
                    Console.WriteLine("Major yang diperbarui: " + education.Major);
                    Console.WriteLine("Degree yang diperbarui: " + education.Degree);
                    Console.WriteLine("GPA yang diperbarui: " + education.Gpa);
                    Console.WriteLine("ID Universitas yang diperbarui: " + education.UniversityId);
                    var resultUpdateEdu = MenuEducation.UpdateByIdEducation(education);
                    if (resultUpdateEdu > 0)
                    {
                        Console.WriteLine("Update success!");
                    }
                    else
                    {
                        Console.WriteLine("Update failed!");
                    }
                }
                if (newChoiceSecond == "5")
                {
                    Console.Clear();
                    Console.WriteLine("Delete Education By Id");
                    Console.Write("Masukkan ID Education yang ingin di hapus: ");
                    var id = Console.ReadLine();
                    education.Id = Convert.ToInt32(id);
                    Console.WriteLine("ID yang mau dihapus :" + education.Id);
                    var resultDelEdu = MenuEducation.DeleteByIdEducation(education);
                    if (resultDelEdu > 0)
                    {
                        Console.WriteLine("Delete success!");
                    }
                    else
                    {
                        Console.WriteLine("Delete failed!");
                    }
                }
                break;

            case "3":
                Console.Clear();
                /*var resultEmployees = MenuEmployees.GetEmployees();*/
                /*var getDepartmentId = from e in resultEmployees
                                      where e.DepartmentId == "1A1"
                                      select e;*/
                /*var getDepartmentId = resultEmployees.Where(e => e.DepartmentId.Contains("1A1")).ToList();
                foreach (var getName in getDepartmentId)
                {
                    Console.WriteLine(getName.FirstName);
                    Console.WriteLine(getName.DepartmentId);
                }*/
                /*var resultEmployees = MenuEmployees.GetEmployees();
                foreach (var resultEmp in resultEmployees)
                {
                    Console.WriteLine("ID            : " + resultEmp.Id);
                    Console.WriteLine("First Name    : " + resultEmp.FirstName);
                    Console.WriteLine("Last Name     : " + resultEmp.LastName);
                    Console.WriteLine("Birthdate     : " + resultEmp.Birthdate);
                    Console.WriteLine("Gender        : " + resultEmp.Gender);
                    Console.WriteLine("Hiring Date   : " + resultEmp.HiringDate);
                    Console.WriteLine("Email         : " + resultEmp.Email);
                    Console.WriteLine("Phone Number  : " + resultEmp.PhoneMumber);
                    Console.WriteLine("Department ID : " + resultEmp.DepartmentId);
                }*/
                Console.WriteLine("Berikut adalah list nama department: ");
                var resultsDepart = MenuDepartments.GetDepartments();
                foreach (var resultDep in resultsDepart)
                {
                    Console.WriteLine("Id : " + resultDep.Id);
                    Console.WriteLine("Nama : " + resultDep.Name);
                }
                Console.WriteLine("================================");
                Console.WriteLine("Berikut adalah list nama universitas: ");
                var resultsuniv = MenuUniversities.GetUniversities();
                foreach (var resultUnivall in resultsuniv)
                {
                    Console.WriteLine("Id : " + resultUnivall.Id);
                    Console.WriteLine("Nama : " + resultUnivall.Name);
                }
                Console.WriteLine("================================");
                Console.WriteLine("Masukkan data-data berikut: ");
                Console.Write("NIK         : ");
                var nik = Console.ReadLine();
                employee.Nik = nik;
                Console.Write("First Name  : ");
                employee.FirstName = Console.ReadLine();
                Console.Write("Last Name   : ");
                employee.LastName = Console.ReadLine();
                Console.Write("Birthdate   : ");
                var birthdate = Console.ReadLine();
                employee.Birthdate = DateTime.Parse(birthdate);
                Console.Write("Gender  : (lk/pr)");
                employee.Gender = Console.ReadLine();
                Console.Write("Hiring Date : ");
                var hiringDate = Console.ReadLine();
                employee.HiringDate = DateTime.Parse(hiringDate);
                Console.Write("Email   : ");
                employee.Email = Console.ReadLine();
                Console.Write("Phone Number   : ");
                employee.PhoneMumber = Console.ReadLine();
                Console.Write("Department ID  :");
                employee.DepartmentId = Console.ReadLine();
                Console.Write("Major        : ");
                education.Major = Console.ReadLine();
                Console.Write("Degree       : ");
                education.Degree = Console.ReadLine();
                Console.Write("GPA          : ");
                education.Gpa = Console.ReadLine();
                Console.Write("University Name   :");
                university.Name = Console.ReadLine();

                InsertByQuery(employee, university, education);
                /*var result = InsertAllAtOnce(employee, university, education);
                if (result > 0)
                {
                    Console.WriteLine("Insert success!");
                }
                else
                {
                    Console.WriteLine("Insert failed!");
                }*/
                break;
        }
            

    }

    public static void InsertByQuery(Employees employees, Universities universities, Educations educations)
    {
        var emp = new Employees();
        emp.FirstName = employees.FirstName;
        emp.LastName = employees.LastName;
        emp.Birthdate = employees.Birthdate;
        emp.PhoneMumber = employees.PhoneMumber;
        emp.HiringDate = employees.HiringDate;
        emp.Gender = employees.Gender;
        emp.Email = employees.Email;
        emp.DepartmentId = employees.DepartmentId;
        emp.Nik = employees.Nik;

        var univ = new Universities();
        univ.Name = universities.Name;

        var edu = new Educations();
        edu.Major = educations.Major;
        edu.Degree = educations.Degree;
        edu.Gpa = educations.Gpa;

        var prof = new Profilings();

        MenuEmployees.InsertEmployee(employees);
        var cekNameUniv = MenuUniversities.GetIDByNameUniversity(universities);
        // cek apakah nama univ sudah ada
        foreach (var cekName in cekNameUniv)
        {
            Console.WriteLine(cekName.Id);
            if (cekName.Id != 0)
            {
                univ.Id = cekName.Id;
                edu.UniversityId = univ.Id;
                // insert ke education dengan id univ yg sudah ada
                MenuEducation.InsertEducation(edu);

            }
        }
        var getIdEdu = MenuEducation.GetIdByMajorEducation(educations);
        var getEmployeeId = MenuEmployees.GetIdByNikEmployee(employees);
        foreach (var idEdu in getIdEdu)
        {
            edu.Id = idEdu.Id;
            prof.EducationId = edu.Id;
        }
        foreach (var idEmployee in getEmployeeId)
        {
            emp.Id = idEmployee.Id;
            prof.EmployeeId = emp.Id;
        }
        MenuProfilings.InsertProfilings(prof);
        /*var getIdUniv = MenuUniversities.GetIDByNameUniversity(universities);
        foreach (var idUniv in getIdUniv)
        {
            if (idUniv.Id != 0)
            {
                univ.Id = idUniv.Id;
                edu.UniversityId = univ.Id;
                // insert ke education dengan id univ yg sudah ada
                Console.WriteLine(edu.UniversityId);
                MenuEducation.InsertEducation(edu);
                Console.WriteLine(univ.Id);
            }
        }*/




        /*var univ = new Universities();*/
        /*if (cekNameUniv != null)
        {
            foreach(var iduniv in cekNameUniv)
            {
                univ.Id = iduniv.Id;
                MenuUniversities.InsertUniversity(univ);
            }
        }
        MenuUniversities.InsertUniversity(universities);
        MenuEducation.InsertEducation(educations);
        var getIdEdu = MenuEducation.GetIdByMajorEducation(educations);
        if (getIdEdu != null)
        {
            foreach(var idEdu in getIdEdu)
            {
                edu.Id = idEdu.Id;
            }
        }
        var getEmployee = MenuEmployees.GetIdByNikEmployee(employees);
        if (getEmployee != null)
        {
            foreach (var idEmployee in getEmployee)
            {
                emp.Id = idEmployee.Id;
            }
        }
        MenuProfilings.InsertProfilings(emp, edu);*/
    }

    public static int InsertAllAtOnce(Employees employee, Universities universities, Educations educations)
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
        GetMenu();

    }


}


