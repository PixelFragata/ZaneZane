using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class LoginResultDto
    {
        public bool Authenticated { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
    }
}
