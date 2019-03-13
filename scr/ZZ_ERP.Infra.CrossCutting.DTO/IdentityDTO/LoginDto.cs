using System.ComponentModel.DataAnnotations;

namespace ZZ_ERP.Infra.CrossCutting.DTO.IdentityDTO
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
