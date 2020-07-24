using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Models
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt)
        {
        }

        
       public  DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
