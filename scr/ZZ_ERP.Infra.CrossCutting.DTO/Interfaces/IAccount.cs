using System;
using System.Collections.Generic;

namespace ZZ_ERP.Infra.CrossCutting.DTO.Interfaces
{
    public interface IAccount
    {
        string Id { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        bool IsActive { get; set; }
        DateTime CreateDate { get; set; }
        DateTime DeactivationDate { get; set; }
    }
}
