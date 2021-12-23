using System;
using JWTAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Data
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> opt):base(opt)
        {
            
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>(ent => {


                ent.HasIndex(e => e.Email).IsUnique();



                });
        }
    }
}
