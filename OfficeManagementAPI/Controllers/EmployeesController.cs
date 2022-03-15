using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using OfficeManagementAPI.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace OfficeManagementAPI.Controllers
{
    [Authorize(Roles = Role.Administrator)]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //[AllowAnonymous]
        //[HttpGet("{role}")]
        //public JsonResult GetForAll(string role)
        //{
        //    string query = @"
        //                    select ID,FirstName,LastName,DeskNo,OfficeName,BuildingName,WorkRemote from dbo.Employees
        //                    ";
        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
        //    SqlDataReader myReader;
        //    using(SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using(SqlCommand myCommand = new SqlCommand(query, myCon))
        //        {
        //            myReader = myCommand.ExecuteReader();
        //            table.Load(myReader);
        //            myReader.Close();
        //            myCon.Close();
        //        }
        //    }
        //    return new JsonResult(table);
            
        //}
        [HttpGet]
        public JsonResult Get()
        {
            string role = ;
            string query1 = @"
                            select ID,FirstName,LastName,DeskNo,OfficeName,BuildingName,WorkRemote from dbo.Employees
                            ";
            string query = @"
                            select ID,FirstName,LastName,Email,Passw,EmpRole,Gender,BirthDate,Nationality,
                            EmpStatus,DeskNo,OfficeName,FloorNo,BuildingName,WorkRemote, Token from dbo.Employees
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            //if (table != null)
            //Console.WriteLine(table);
            return new JsonResult(table);
            //else return new JsonResult("failed");
        }

        [HttpPost]
        public JsonResult Post(Employees emp)
        {
            string query = @"
                            insert into dbo.Employees values(
                            @FirstName,@LastName,@Email,@Passw,@EmpRole,@Gender,@BirthDate,@Nationality,
                            @EmpStatus,@DeskNo,@OfficeName,@FloorNo,@BuildingName,@WorkRemote, @Token
                            )
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName",emp.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", emp.LastName);
                    myCommand.Parameters.AddWithValue("@Email", emp.Email);
                    myCommand.Parameters.AddWithValue("@Passw", emp.Passw);
                    myCommand.Parameters.AddWithValue("@EmpRole", emp.EmpRole);
                    myCommand.Parameters.AddWithValue("@Gender", emp.Gender);
                    myCommand.Parameters.AddWithValue("@BirthDate", emp.BirthDate);
                    myCommand.Parameters.AddWithValue("@Nationality", emp.Nationality);
                    myCommand.Parameters.AddWithValue("@EmpStatus", emp.EmpStatus);
                    myCommand.Parameters.AddWithValue("@DeskNo", emp.DeskNo);
                    myCommand.Parameters.AddWithValue("@OfficeName", emp.OfficeName);
                    myCommand.Parameters.AddWithValue("@FloorNo", emp.FloorNo);
                    myCommand.Parameters.AddWithValue("@BuildingName", emp.BuildingName);
                    myCommand.Parameters.AddWithValue("@WorkRemote", emp.WorkRemote);
                    myCommand.Parameters.AddWithValue("@Token", emp.Token);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added succesfuly");
        }
       
        [HttpPut]
        public JsonResult Put(Employees emp)
        {
            string query = @"
                            update dbo.Employees 
                            set FirstName = @FirstName, LastName = @LastName,
                            Email = @Email, Passw = @Passw,
                            EmpRole = @EmpRole, Gender = @Gender,
                            BirthDate = @BirthDate, Nationality = @Nationality,
                            EmpStatus = @EmpStatus, DeskNo = @DeskNo,
                            OfficeName = @OfficeName, FloorNo = @FloorNo,
                            BuildingName = @BuildingName, WorkRemote = @WorkRemote
                            where ID = @ID
                            
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", emp.ID);
                    myCommand.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", emp.LastName);
                    myCommand.Parameters.AddWithValue("@Email", emp.Email);
                    myCommand.Parameters.AddWithValue("@Passw", emp.Passw);
                    myCommand.Parameters.AddWithValue("@EmpRole", emp.EmpRole);
                    myCommand.Parameters.AddWithValue("@Gender", emp.Gender);
                    myCommand.Parameters.AddWithValue("@BirthDate", emp.BirthDate);
                    myCommand.Parameters.AddWithValue("@Nationality", emp.Nationality);
                    myCommand.Parameters.AddWithValue("@EmpStatus", emp.EmpStatus);
                    myCommand.Parameters.AddWithValue("@DeskNo", emp.DeskNo);
                    myCommand.Parameters.AddWithValue("@OfficeName", emp.OfficeName);
                    myCommand.Parameters.AddWithValue("@FloorNo", emp.FloorNo);
                    myCommand.Parameters.AddWithValue("@BuildingName", emp.BuildingName);
                    myCommand.Parameters.AddWithValue("@WorkRemote", emp.WorkRemote);
                    //myCommand.Parameters.AddWithValue("@Token", emp.Token);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated successfuly");
        }
        
        [HttpDelete ("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Employees where ID = @ID
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted successfuly");
        }

        [HttpPut ("{status},{id}")]
        public JsonResult PutStatus(string status, int id)
        {
            string query = @"
                            update dbo.Employees 
                            set EmpStatus = @EmpStatus where ID = @ID
                            
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", id);
                   
                    if (status == "inactive")
                        myCommand.Parameters.AddWithValue("@EmpStatus", "inactive");
                    else if (status == "active")
                        myCommand.Parameters.AddWithValue("@EmpStatus", "active");
                    else
                        myCommand.Parameters.AddWithValue("@EmpStatus", "error");


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated successfuly");
        }
    }
}
