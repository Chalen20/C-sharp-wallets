using BusinessLayer.Users;
using DataStorage;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetsWPF
{
    public class AuthenticationService
    {
        private FileDataStorage<DBUser> _storage = new FileDataStorage<DBUser>();
        public async Task<User> Authenticate(AuthenticationUser authUser)
        {
            return await Task.Run(async () =>
            {
                Thread.Sleep(2000);
                if (String.IsNullOrWhiteSpace(authUser.Login) || String.IsNullOrWhiteSpace(authUser.Password))
                    throw new ArgumentException("Login or/and Password is Empty");
                var users = await _storage.GetAllAsync();
                var dbuser = users.FirstOrDefault(user => user.Login == authUser.Login && user.Password == PasswordHash(authUser.Password));
                if (dbuser == null)
                    throw new Exception("Wrong Login or Password");
                return new User(dbuser.Guid, dbuser.FirstName, dbuser.LastName, dbuser.Email, dbuser.Password);
            });
        }

        public async Task<bool> RegisterUser(RegistrationUser regUser)
        {
            return await Task.Run(async () =>
            {
                Thread.Sleep(2000);
                var users = await _storage.GetAllAsync();
                var dbuser = users.FirstOrDefault(user => user.Login == regUser.Login);
                if (dbuser != null)
                    throw new Exception("User already exists");
                if (String.IsNullOrWhiteSpace(regUser.Login) || String.IsNullOrWhiteSpace(regUser.Password) ||
                    String.IsNullOrWhiteSpace(regUser.LastName) || String.IsNullOrWhiteSpace(regUser.FirstName) ||
                    String.IsNullOrWhiteSpace(regUser.Email))
                    throw new ArgumentException("Login, Password, FirstName, LastName or Email is Empty");
                dbuser = new DBUser(Guid.NewGuid(), regUser.FirstName, regUser.LastName, regUser.Email, regUser.Login, PasswordHash(regUser.Password));
                await _storage.AddOrUpdateAsync(dbuser);
                return true;
            });
        }


        public string PasswordHash(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }
    }
}
