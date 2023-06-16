﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Client.Models;
public class University
{
    public Guid Guid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}