using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuth.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }
    }
}
