using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.Interfaces
{
    public  class IPolicies
    {
        public  List<IPolicy> MyPolicies { get; set; }
        public static IPolicies Instance { get; }
    }
}
