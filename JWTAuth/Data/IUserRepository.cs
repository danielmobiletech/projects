using System;
using JWTAuth.Models;

namespace JWTAuth.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        public User GetUserByEmail(string email);
        public User GetUserById(int id);
    }
}
