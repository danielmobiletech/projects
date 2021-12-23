using System;
namespace JWTAuth.Dtos
{
    public class LoginDto
    {
        public LoginDto()
        {
        }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string Name { get; set; }
    }
}
