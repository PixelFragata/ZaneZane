using System.Collections.Generic;

namespace ZZ_ERP.Infra.CrossCutting.DTO.IdentityDTO
{
    public class MatrizRolePermission
    {
        public string RoleName { get; set; }
        public List<string> Telas { get; set; }
        public List<string> Permissoes { get; set; }
        public bool[][] TelasPermissoes { get; set; }

    }
}
