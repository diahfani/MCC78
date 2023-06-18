﻿using System.ComponentModel.DataAnnotations;
using Client.Utilities;
namespace Client.Models;

public class Employee
{
    public Guid Guid { get; set; }
    public string Nik { get; set; }
    public string FirstName { get; set; }
    // (?) atau mark question berarti nullable
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }
    public DateTime HiringDate { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    public DateTime ModifiedDate { get; set; }

}
