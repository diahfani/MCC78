using System.ComponentModel.DataAnnotations;
using WebAPI.Model;

namespace WebAPI.ViewModels.Universities;

public class UniversityVM
{
    public Guid? Guid {  get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }

}
