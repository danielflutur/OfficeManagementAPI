using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OfficeManagementAPI.Models;
using OfficeManagementAPI.Helpers;

namespace OfficeManagementAPI.Services
{
    public interface IUserService
    {
        Employees Authenticate(string email, string password);
        IEnumerable<Employees> GetAll();
        Employees GetById(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Employees> _users = new List<Employees>
        {
            new Employees { ID = 1, FirstName = "Admin", LastName = "User", Email= "testmail@gmail.com", Passw = "admin", EmpRole = Role.Administrator,
                Gender = "Male", BirthDate = DateTime.Now, Nationality = "RO", EmpStatus="active", DeskNo = 2, OfficeName = "B2",
                FloorNo = 2, BuildingName = "B", WorkRemote = "NO"},
            new Employees { ID = 2, FirstName = "OfficeAdmin", LastName = "User", Email= "testmail1@gmail.com", Passw = "officeadmin", EmpRole = Role.OfficeAdministrator,
                Gender = "Male", BirthDate = DateTime.Now, Nationality = "RO", EmpStatus="active", DeskNo = 3, OfficeName = "B2",
                FloorNo = 2, BuildingName = "B", WorkRemote = "NO"},
            new Employees { ID = 3, FirstName = "Employee", LastName = "User", Email= "testmail2@gmail.com", Passw = "officeadmin", EmpRole = Role.User,
                Gender = "Male", BirthDate = DateTime.Now, Nationality = "RO", EmpStatus="inactive", DeskNo = 4, OfficeName = "B2",
                FloorNo = 2, BuildingName = "B", WorkRemote = "NO"}
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Employees Authenticate(string email, string password)
        {
            var user = _users.SingleOrDefault(x => x.Email == email && x.Passw == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.EmpRole)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<Employees> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public Employees GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.ID == id);
            return user.WithoutPassword();
        }
    }
}
