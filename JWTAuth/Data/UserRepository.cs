using System;
using System.Linq;
using JWTAuth.Models;

namespace JWTAuth.Data
{
    public class UserRepository:IUserRepository
    {
        private readonly UserContext userContext;
        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }
        public User Create(User user)
        {
            userContext.Users.Add(user);
            user.Id=userContext.SaveChanges();
            return user;

        }

        public User GetUserByEmail(string email)
        {
             if(!userContext.Users.Any(x => x.Email == email))
            {
                return null;
            }
            var vals = userContext.Users.FirstOrDefault(x => x.Email == email);

            return new User() { Email = vals.Email,Id=vals.Id,Name=vals.Name,Password=vals.Password };
        }

        public User GetUserById(int id)
        {

            if (!userContext.Users.Any(x => x.Id == id))
            {
                return null;
            }
            var vals = userContext.Users.FirstOrDefault(x => x.Id == id);

            return new User() { Email = vals.Email, Id = vals.Id, Name = vals.Name, Password = vals.Password };
        }
    }
}
