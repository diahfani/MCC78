using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.Roles;

public class RoleVM
{
    public Guid Guid { get; set; }
    [Required]
    public string Name { get; set; }

}
