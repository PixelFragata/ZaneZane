using System;

namespace ZZ_ERP.Infra.CrossCutting.DTO.IdentityDTO
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
