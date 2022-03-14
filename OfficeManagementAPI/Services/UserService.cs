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
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
        //private List<Employees> _users = new List<Employees>
        //{
        //    new Employees { ID = 1, FirstName = "Admin", LastName = "User", Email= "testmail@gmail.com", Passw = "admin", EmpRole = Role.Administrator,
        //        Gender = "Male", BirthDate = DateTime.Now, Nationality = "RO", EmpStatus="active", DeskNo = 2, OfficeName = "B2",
        //        FloorNo = 2, BuildingName = "B", WorkRemote = "NO"},
        //    new Employees { ID = 2, FirstName = "OfficeAdmin", LastName = "User", Email= "testmail1@gmail.com", Passw = "officeadmin", EmpRole = Role.OfficeAdministrator,
        //        Gender = "Male", BirthDate = DateTime.Now, Nationality = "RO", EmpStatus="active", DeskNo = 3, OfficeName = "B2",
        //        FloorNo = 2, BuildingName = "B", WorkRemote = "NO"},
        //    new Employees { ID = 3, FirstName = "Employee", LastName = "User", Email= "testmail2@gmail.com", Passw = "officeadmin", EmpRole = Role.User,
        //        Gender = "Male", BirthDate = DateTime.Now, Nationality = "RO", EmpStatus="inactive", DeskNo = 4, OfficeName = "B2",
        //        FloorNo = 2, BuildingName = "B", WorkRemote = "NO"}
        //};
        private readonly IConfiguration _configuration;

        private readonly AppSettings _appSettings;
        DataTable table;
        private List<Employees> _users = new List<Employees>();
        public UserService(IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;
        

        
            string query = @"
                            select ID,FirstName,LastName,Email,Passw,EmpRole,Gender,BirthDate,Nationality,
                            EmpStatus,DeskNo,OfficeName,FloorNo,BuildingName,WorkRemote from dbo.Employees
                            ";
            table = new DataTable();
            table.Columns.Add("ID", typeof(Int32));
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Passw", typeof(string));
            table.Columns.Add("EmpRole", typeof(string));
            table.Columns.Add("Gender", typeof(string));
            table.Columns.Add("BirthDate", typeof(DateTime));
            table.Columns.Add("Nationality", typeof(string));
            table.Columns.Add("EmpStatus", typeof(string));
            table.Columns.Add("DeskNo", typeof(Int32));
            table.Columns.Add("OfficeName", typeof(string));
            table.Columns.Add("FloorNo", typeof(Int32));
            table.Columns.Add("BuiuldingName", typeof(string));
            table.Columns.Add("WorkRemote", typeof(string));
            table.Columns.Add("Token", typeof(string));
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    
                    myReader = myCommand.ExecuteReader();
                    //table.Rows.Add(myReader);
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            _users = (from DataRow dr in table.Rows
                      select new Employees()
                      {
                          ID = int.Parse(dr["ID"].ToString()),
                          FirstName = dr["FirstName"].ToString(),
                          LastName = dr["LastName"].ToString(),
                          Email = dr["Email"].ToString(),
                          Passw = dr["Passw"].ToString(),
                          EmpRole = dr["EmpRole"].ToString(),
                          Gender = dr["Gender"].ToString(),
                          BirthDate = Convert.ToDateTime(dr["BirthDate"].ToString()),
                          Nationality = dr["Nationality"].ToString(),
                          EmpStatus = dr["EmpStatus"].ToString(),
                          DeskNo = int.Parse(dr["DeskNo"].ToString()),
                          OfficeName = dr["OfficeName"].ToString(),
                          FloorNo = int.Parse(dr["FloorNo"].ToString()),
                          BuildingName = dr["BuildingName"].ToString(),
                          WorkRemote = dr["WorkRemote"].ToString(),
                          Token = dr["Token"].ToString()
                      }).ToList();
            //if (table != null)
            
            //return new JsonResult(table);
            //else return new JsonResult("failed");
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

            //INCEARCA SA FACI UN PUT CU TOKEN_UL CA SA SE SCHIMBE SI IN BAZA DE DATE
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<Employees> GetAll()
        {
            return _users;//.WithoutPasswords();
        }

        public Employees GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.ID == id);
            return user.WithoutPassword();
        }
    }
}
