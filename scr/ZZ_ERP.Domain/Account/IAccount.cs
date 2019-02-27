using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Domain.Account
{
    public interface IAccount
    {
        string Id { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
    }
}
