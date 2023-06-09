﻿using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Utility;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Login;

namespace WebAPI.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    public AccountRepository(BookingRoomsDBContext context, IUniversityRepository universityRepository, IEmployeeRepository employeeRepository, IEducationRepository educationRepository) : base(context)
    {
        _universityRepository = universityRepository;
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
    }

    public int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM)
    {
        var account = new Account();
        account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
        if (account == null || account.OTP != changePasswordVM.OTP)
        {
            return 2;
        }
        // Cek apakah OTP sudah digunakan
        if (account.IsUsed)
        {
            return 3;
        }
        // Cek apakah OTP sudah expired
        if (account.ExpiredDate < DateTime.Now)
        {
            return 4;
        }
        // Cek apakah NewPassword dan ConfirmPassword sesuai
        if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
        {
            return 5;
        }
        // Update password
        account.Password = Hashing.HashPassword(changePasswordVM.NewPassword);
        /*var hashPassword = Hashing.HashPassword(account.Password);*/
        account.IsUsed = true;
        try
        {
            var updatePassword = Update(account);
            if (!updatePassword)
            {
                return 0;
            }
            return 1;
        }
        catch
        {
            return 0;
        }
    }

    public IEnumerable<string> GetRoles(Guid guid)
    {
        var account = GetByGuid(guid);
        if (account is null) return Enumerable.Empty<string>();
        var query = from accRole in _context.AccountRoles
                    join role in _context.Roles
                    on accRole.RoleGuid equals role.Guid
                    where accRole.AccountGuid == guid
                    select role.Name;
        return query.ToList();
    }

    public LoginVM Login(LoginVM loginVM)
    {
        var account = GetAll();
        var employee = _context.Employees.ToList();
        var query = from emp in employee
                    join acc in account
                    on emp.Guid equals acc.Guid
                    where emp.Email == loginVM.Email
                    select new LoginVM
                    {
                        Email = emp.Email,
                        Password = acc.Password
                    };

        return query.FirstOrDefault();
    }

    public int Register(RegisterVM registerVM)
    {
        try
        {
            var university = new University
            {
                Code = registerVM.UniversityCode,
                Name = registerVM.UniversityName

            };
            _universityRepository.CreateWithValidate(university);

            var employee = new Employee
            {
                Nik = GenerateNIK(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Gender = registerVM.Gender,
                HiringDate = registerVM.HiringDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
            };
            var result = _employeeRepository.Create(employee);

            var education = new Education
            {
                Guid = employee.Guid,
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.GPA,
                UniversityGuid = university.Guid
            };
            _educationRepository.Create(education);

            var hashPassword = Hashing.HashPassword(registerVM.Password);
            var account = new Account
            {
                Guid = employee.Guid,
                Password = hashPassword,
                IsDeleted = false,
                IsUsed = true,
                OTP = 0
            };
            Create(account);

            var accountRole = new AccountRole
            {
                RoleGuid = Guid.Parse("a0082ab9-4cde-4c07-ca74-08db60bf4a3f"),
                AccountGuid = employee.Guid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            _context.AccountRoles.Add(accountRole);
            _context.SaveChanges();

            return 3;

        }
        catch
        {
            return 0;
        }
    }

    public int UpdateOTP(Guid? employeeId)
    {
        /*var account = new Account();*/
        var account = _context.Accounts.FirstOrDefault(a => a.Guid == employeeId);
        //Generate OTP
        Random rnd = new Random();
        var getOtp = rnd.Next(100000, 999999);
        account.OTP = getOtp;

        //Add 5 minutes to expired time
        account.ExpiredDate = DateTime.Now.AddMinutes(5);
        account.IsUsed = false;
        try
        {
            var check = Update(account);


            if (!check)
            {
                return 0;
            }
            return getOtp;
        }
        catch
        {
            return 0;
        }

    }

    private string GenerateNIK()
    {
        var lastNik = _context.Employees.ToList().OrderByDescending(e => int.Parse(e.Nik)).FirstOrDefault(); 

        if (lastNik != null)
        {
            int lastNikNumber;
            if (int.TryParse(lastNik.Nik, out lastNikNumber))
            {
                return (lastNikNumber + 1).ToString();
            }
        }

        return "100000";
    }
}
