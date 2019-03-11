using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class MatrizRolePermission
    {
        public string RoleName { get; set; }
        public List<string> Telas { get; set; }
        public List<string> Permissoes { get; set; }
        public bool[][] TelasPermissoes { get; set; }

    }
}
