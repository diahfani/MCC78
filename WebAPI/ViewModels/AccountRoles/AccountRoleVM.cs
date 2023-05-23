﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.ViewModels.AccountRoles;

public class AccountRoleVM
{
    public Guid Guid { get; set; }
    public Guid AccountGuid { get; set; }
    [Column("role_guid")]
    public Guid RoleGuid { get; set; }
}
