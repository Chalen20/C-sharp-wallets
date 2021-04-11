using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BudgetsWPF
{
    public class AuthenticationService
    {
        private static List<DBUser> Users = new List<DBUser> { new DBUser("1", "1", "1", "1", "1") };
        public User Authenticate(AuthenticationUser authUser)
        {
            Thread.Sleep(2000);
            if (String.IsNullOrWhiteSpace(authUser.Login) || String.IsNullOrWhiteSpace(authUser.Password)) 
                throw new ArgumentException("Login or/and Password is Empty");
            var dbuser = Users.FirstOrDefault(user => user.Login == authUser.Login && user.Password == authUser.Password);
            if (dbuser == null)
                throw new Exception("Wrong Login or Password");
            return new User(dbuser.Guid, dbuser.FirstName, dbuser.LastName, dbuser.Email, dbuser.Password);
        }

        public bool RegisterUser(RegistrationUser regUser)
        {
            var dbuser = Users.FirstOrDefault(user => user.Login == regUser.Login);
            if (dbuser != null)
                throw new Exception("User already exists");
            if (String.IsNullOrWhiteSpace(regUser.Login) || String.IsNullOrWhiteSpace(regUser.Password) ||
                String.IsNullOrWhiteSpace(regUser.LastName) || String.IsNullOrWhiteSpace(regUser.FirstName) ||
                String.IsNullOrWhiteSpace(regUser.Email))
                throw new ArgumentException("Login, Password, FirstName, LastName or Email is Empty");
            dbuser = new DBUser(regUser.FirstName, regUser.LastName, regUser.Email, regUser.Login, regUser.Password);
            Users.Add(dbuser);
            return true;
        }
    }
}
