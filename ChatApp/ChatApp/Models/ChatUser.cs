using System;
namespace ChatApp.Models
{
    public class ChatUser
    {        
        public User User { get; set; }
        public string UserId { get; set; }
        public Chat Chat { get; set; }
        public int ChatId { get; set; }
        public UserRole Role { get; set; }        
    }
}
