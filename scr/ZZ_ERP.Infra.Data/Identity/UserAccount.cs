using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;

namespace ZZ_ERP.Infra.Data.Identity
{
    public class UserAccount : IdentityUser , IAccount
    {

    }
}
