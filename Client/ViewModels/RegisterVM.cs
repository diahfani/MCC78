﻿using Client.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Client.ViewModels;

public class RegisterVM
{
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }

    public DateTime HiringDate { get; set; }
    [EmailAddress]
    // validation bisa duplikat
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }

    public string Major { get; set; }

    public string Degree { get; set; }
    [Range(0, 4)]
    public float GPA { get; set; }

    //public Guid UniversityGuid { get; set; }

    public string UniversityCode { get; set; }

    public string UniversityName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
