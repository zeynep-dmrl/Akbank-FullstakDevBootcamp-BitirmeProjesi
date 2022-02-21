using Microsoft.AspNetCore.Mvc;

namespace DAL.Model
{
    public class Login
    {
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
    public class LoginDto
    {
        [FromHeader]
        public string Email { get; set; } = string.Empty;
        [FromHeader]
        public string Password { get; set; } = string.Empty;
    }
    //The instance of APIAuthority table in the database.
    public class APIAuthority //landlord
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }

    }
}
