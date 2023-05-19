using BookingRooms.Controllers;
using BookingRooms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.View;

public class MenuView
{
    public void GetMenu()
    {
        var university = new University();
        var education = new Education();
        var employee = new Employee();
        var profiling = new Profiling();
        var department = new Department();
        var newChoice = "";
        Console.WriteLine("Selamat datang, Admin!");
        Console.WriteLine("Silahkan pilih data yang ingin dimasukkan :");
        Console.WriteLine("1. University\n2. Education\n3. Insert data all at once\n4. Get Data Profilings\n5. Get Data Employee");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.Clear();
                GetMenuUniv(university, newChoice);
                break;

            case "2":
                Console.Clear();
                GetMenuEdu(education, newChoice);
                break;

            case "3":
                Console.Clear();
                GetMenuInsertAll(university, department, employee, education);
                break;


            case "4":
                Console.Clear();
                GetMenuProfilings(profiling);
                break;

            case "5":
                Console.Clear();
                GetMenuEmployeeWithLinq(profiling, education, university, employee);
                break;
        }


    }

    public void GetMenuUniv(University university, string secondChoice)
    {
        Console.WriteLine("Silahkan pilih menu :");
        Console.WriteLine("1. Tambah data Universitas\n2. Get All Universitas\n3. Get By ID Universitas\n4. Update Nama Universitas\n5. Delete Universitas");
        secondChoice = Console.ReadLine();
        if (secondChoice == "1")
        {
            Console.Clear();
            Console.WriteLine("Masukkan nama Universitas yang ingin ditambah :");
            university.Name = Console.ReadLine();
            Console.WriteLine("Nama Universitas yang di input: " + university.Name);
            var resultInsertUniv = university.InsertUniversity(university);
            if (resultInsertUniv > 0)
            {
                Console.WriteLine("Insert success!");
            }
            else
            {
                Console.WriteLine("Insert failed!");
            }
        }
        if (secondChoice == "2")
        {
            Console.Clear();
            Console.WriteLine("Get All Universities");

            var resultsUniv = university.GetUniversities();
            foreach (var results in resultsUniv)
            {
                Console.WriteLine("Id : " + results.Id);
                Console.WriteLine("Nama : " + results.Name);
            }

        }
        if (secondChoice == "3")
        {
            Console.Clear();
            Console.WriteLine("Get University By Id");
            var id = Console.ReadLine();
            university.Id = Convert.ToInt32(id);
            Console.WriteLine("Id yang dicari : " + university.Id);
            university.GetByIdUniversity(university);
        }
        if (secondChoice == "4")
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
            var resultUpdateUniv = university.UpdateByIdUniversities(university);
            if (resultUpdateUniv > 0)
            {
                Console.WriteLine("Update success!");
            }
            else
            {
                Console.WriteLine("Update failed!");
            }
        }
        if (secondChoice == "5")
        {
            Console.Clear();
            Console.WriteLine("Delete Universities By Id");
            Console.Write("Masukkan ID Universitas yang ingin di hapus: ");
            var id = Console.ReadLine();
            university.Id = Convert.ToInt32(id);
            Console.WriteLine("ID yang mau dihapus :" + university.Id);
            var resultDelUniv = university.DeleteByIdUniversities(university);
            if (resultDelUniv > 0)
            {
                Console.WriteLine("Delete success!");
            }
            else
            {
                Console.WriteLine("Delete failed!");
            }
        }
    }

    public void GetMenuEdu(Education education, string secondChoice)
    {
        Console.WriteLine("Silahkan pilih menu :");
        Console.WriteLine("1. Tambah data education\n2. Get All Educations\n3. Get By ID Education\n4. Update Educations\n5. Delete Education");
        secondChoice = Console.ReadLine();
        if (secondChoice == "1")
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
            var resultInsertEdu = education.InsertEducation(education);
            if (resultInsertEdu > 0)
            {
                Console.WriteLine("Insert success!");
            }
            else
            {
                Console.WriteLine("Insert failed!");
            }
        }
        if (secondChoice == "2")
        {
            Console.Clear();
            Console.WriteLine("Get All Universities");

            var resultsEdu = education.GetEducations();
            foreach (var resultsE in resultsEdu)
            {
                Console.WriteLine("Id : " + resultsE.Id);
                Console.WriteLine("Major : " + resultsE.Major);
                Console.WriteLine("Degree : " + resultsE.Degree);
                Console.WriteLine("GPA : " + resultsE.Gpa);
                Console.WriteLine("ID University : " + resultsE.UniversityId);
            }

        }
        if (secondChoice == "3")
        {
            Console.Clear();
            Console.WriteLine("Get Education By Id");
            var id = Console.ReadLine();
            education.Id = Convert.ToInt32(id);
            Console.WriteLine("Id yang dicari : " + education.Id);
            education.GetByIdEducation(education);
        }
        if (secondChoice == "4")
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
            var resultUpdateEdu = education.UpdateByIdEducation(education);
            if (resultUpdateEdu > 0)
            {
                Console.WriteLine("Update success!");
            }
            else
            {
                Console.WriteLine("Update failed!");
            }
        }
        if (secondChoice == "5")
        {
            Console.Clear();
            Console.WriteLine("Delete Education By Id");
            Console.Write("Masukkan ID Education yang ingin di hapus: ");
            var id = Console.ReadLine();
            education.Id = Convert.ToInt32(id);
            Console.WriteLine("ID yang mau dihapus :" + education.Id);
            var resultDelEdu = education.DeleteByIdEducation(education);
            if (resultDelEdu > 0)
            {
                Console.WriteLine("Delete success!");
            }
            else
            {
                Console.WriteLine("Delete failed!");
            }
        }
    }

    public void GetMenuInsertAll(University university, Department department, Employee employee, Education education)
    {
        Console.WriteLine("Berikut adalah list nama department: ");
        var resultsDepart = department.GetDepartments();
        foreach (var resultDep in resultsDepart)
        {
            Console.WriteLine("Id : " + resultDep.Id);
            Console.WriteLine("Nama : " + resultDep.Name);
        }
        Console.WriteLine("================================");
        Console.WriteLine("Berikut adalah list nama universitas: ");
        var resultsuniv = university.GetUniversities();
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

        var insert = new EmployeeController();
        insert.InsertEmployeeC(employee, university, education);

    }

    public void GetMenuProfilings(Profiling profiling)
    {
        var resultProfilings = profiling.GetProfilings();
        foreach (var resultsProf in resultProfilings)
        {
            Console.WriteLine("Employee ID : " + resultsProf.EmployeeId);
            Console.WriteLine("Education ID : " + resultsProf.EducationId);
        }

    }

    public void GetMenuEmployeeWithLinq(Profiling profiling, Education education, University university, Employee employee)
    {

        Console.WriteLine("===========================");
        Console.WriteLine("       Data Employee        ");
        Console.WriteLine("===========================");
        var getProfilings = profiling.GetProfilings();
        var getEmployee = employee.GetEmployees();
        var getEducation = education.GetEducations();
        var getUniversity = university.GetUniversities();
        var showEmployee = from prof in getProfilings
                           join emp in getEmployee on prof.EmployeeId equals emp.Id
                           join edu in getEducation on prof.EducationId equals edu.Id
                           join univ in getUniversity on edu.UniversityId equals univ.Id
                           select new
                           {
                               nik = emp.Nik,
                               full_name = emp.FirstName + " " + emp.LastName,
                               birthdate = emp.Birthdate,
                               gender = emp.Gender,
                               hiringdate = emp.HiringDate,
                               email = emp.Email,
                               phonenumber = emp.PhoneMumber,
                               major = edu.Major,
                               degree = edu.Degree,
                               gpa = edu.Gpa,
                               univName = univ.Name
                           };
        foreach (var i in showEmployee)
        {
            Console.WriteLine($"NIK                 : {i.nik}");
            Console.WriteLine($"Fullname            : {i.full_name}");
            Console.WriteLine($"Birthdate           : {i.birthdate}");
            Console.WriteLine($"Gender              : {i.gender}");
            Console.WriteLine($"Hiring Date         : {i.hiringdate}");
            Console.WriteLine($"Email               : {i.email}");
            Console.WriteLine($"Phone Number        : {i.phonenumber}");
            Console.WriteLine($"Major               : {i.major}");
            Console.WriteLine($"Degree              : {i.degree}");
            Console.WriteLine($"GPA                 : {i.gpa}");
            Console.WriteLine($"University Name     : {i.univName}");
            Console.WriteLine("========================================");
        }

    }
}
