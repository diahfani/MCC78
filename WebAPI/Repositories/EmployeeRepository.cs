using Microsoft.EntityFrameworkCore;
using System;
using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Employees;

namespace WebAPI.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(BookingRoomsDBContext context) : base(context)
    {
    }

    public bool CheckEmailAndPhoneAndNIK(string value)
    {
        return _context.Employees.Any(e => e.Email == value || e.nik == value || e.PhoneNumber == value);
    }
    public int CreateWithValidate(Employee employee)
    {
        try
        {
            bool ExistsByEmail = _context.Employees.Any(e => e.Email == employee.Email);
            if (ExistsByEmail)
            {
                return 1;
            }

            bool ExistsByPhoneNumber = _context.Employees.Any(e => e.PhoneNumber == employee.PhoneNumber);
            if (ExistsByPhoneNumber)
            {
                return 2;
            }

            Create(employee);
            return 3;

        }
        catch
        {
            return 0;
        }

    }

    public Guid? FindGuidByEmail(string email)
    {
        try
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Email == email);
            if (employee == null)
            {
                return null;
            }
            return employee.Guid;
        }
        catch
        {
            return null;
        }

    }

    public IEnumerable<MasterEmployeeVM> GetAllMasterEmployee()
    {
        var employees = GetAll();
        var educations = _context.Educations.ToList();
        var universities = _context.Universities.ToList();

        var employeeEducations = new List<MasterEmployeeVM>();

        foreach (var employee in employees)
        {
            var education = educations.FirstOrDefault(e => e.Guid == employee?.Guid);
            var university = universities.FirstOrDefault(u => u.Guid == education?.UniversityGuid);

            if (education != null && university != null)
            {
                var employeeEducation = new MasterEmployeeVM
                {
                    Guid = employee.Guid,
                    NIK = employee.nik,
                    FullName = employee.FirstName + " " + employee.LastName,
                    BirthDate = employee.BirthDate,
                    Gender = employee.Gender.ToString(),
                    HiringDate = employee.HiringDate,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Major = education.Major,
                    Degree = education.Degree,
                    GPA = education.Gpa,
                    UniversityName = university.Name
                };

                employeeEducations.Add(employeeEducation);
            }
        }

        return employeeEducations;
    }

    public EmployeeVM GetByEmail(string email)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.Email == email);
        var data = new EmployeeVM
        {
            Guid = employee.Guid,
            Nik = employee.nik,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender.ToString(),
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        };
        return data;
    }

    public MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid)
    {
        var employee = GetByGuid(guid);
        var educations = _context.Educations.Find(guid);
        var universities = _context.Universities.Find(educations.UniversityGuid);

        var data = new MasterEmployeeVM
        {
            Guid = employee.Guid,
            NIK = employee.nik,
            FullName = employee.FirstName + " " + employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender.ToString(),
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Major = educations.Major,
            Degree = educations.Degree,
            GPA = educations.Gpa,
            UniversityName = universities.Name
        };

        return data;

    }
}
