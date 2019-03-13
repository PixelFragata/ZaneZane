using System.ComponentModel.DataAnnotations;

namespace ZZ_ERP.Infra.CrossCutting.DTO.IdentityDTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
