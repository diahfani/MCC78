using BookingRooms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Controllers;

public class EmployeeController
{
    public void InsertEmployeeC(Employee employees, University universities, Education educations)
    {
        var emp = new Employee();
        emp.FirstName = employees.FirstName;
        emp.LastName = employees.LastName;
        emp.Birthdate = employees.Birthdate;
        emp.PhoneMumber = employees.PhoneMumber;
        emp.HiringDate = employees.HiringDate;
        emp.Gender = employees.Gender;
        emp.Email = employees.Email;
        emp.DepartmentId = employees.DepartmentId;
        emp.Nik = employees.Nik;

        var univ = new University();
        univ.Name = universities.Name;

        var edu = new Education();
        edu.Major = educations.Major;
        edu.Degree = educations.Degree;
        edu.Gpa = educations.Gpa;

        var prof = new Profiling();

        emp.InsertEmployee(employees);
        var cekNameUniv = univ.GetIDByNameUniversity(universities);
        // cek apakah nama univ sudah ada
        foreach (var cekName in cekNameUniv)
        {
            Console.WriteLine(cekName.Id);
            if (cekName.Id != 0)
            {
                univ.Id = cekName.Id;
                edu.UniversityId = univ.Id;
                // insert ke education dengan id univ yg sudah ada
                edu.InsertEducation(edu);

            }
        }
        var getIdEdu = edu.GetIdByMajorEducation(educations);
        /*var getEmployeeId = emp.GetIdByNikEmployee(employees);*/
        foreach (var idEdu in getIdEdu)
        {
            edu.Id = idEdu.Id;
            prof.EducationId = edu.Id;
        }
        /*foreach (var idEmployee in getEmployeeId)
        {
            emp.Id = idEmployee.Id;
            prof.EmployeeId = emp.Id;
        }*/
        prof.InsertProfilings(prof);
    }

}
