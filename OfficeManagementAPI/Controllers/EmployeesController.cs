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

namespace OfficeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ID,FName,LName,Email,Passw,EmpRole,Gender,BirthDate,Nationality,
                            EmpStatus,DeskNr,OfficeName,FloorNr,BuildingName,WorkRemote from dbo.Employees
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Employees emp)
        {
            string query = @"
                            insert into dbo.Employees values(
                            @FName,@LName,@Email,@Passw,@EmpRole,@Gender,@BirthDate,@Nationality,
                            @EmpStatus,@DeskNr,@OfficeName,@FloorNr,@BuildingName,@WorkRemote
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
                    myCommand.Parameters.AddWithValue("@FName",emp.FName);
                    myCommand.Parameters.AddWithValue("@LName", emp.LName);
                    myCommand.Parameters.AddWithValue("@Email", emp.Email);
                    myCommand.Parameters.AddWithValue("@Passw", emp.Passw);
                    myCommand.Parameters.AddWithValue("@EmpRole", emp.EmpRole);
                    myCommand.Parameters.AddWithValue("@Gender", emp.Gender);
                    myCommand.Parameters.AddWithValue("@BirthDate", emp.BirthDate);
                    myCommand.Parameters.AddWithValue("@Nationality", emp.Nationality);
                    myCommand.Parameters.AddWithValue("@EmpStatus", emp.EmpStatus);
                    myCommand.Parameters.AddWithValue("@DeskNr", emp.DeskNr);
                    myCommand.Parameters.AddWithValue("@OfficeName", emp.OfficeName);
                    myCommand.Parameters.AddWithValue("@FloorNr", emp.FloorNr);
                    myCommand.Parameters.AddWithValue("@BuildingName", emp.BuildingName);
                    myCommand.Parameters.AddWithValue("@WorkRemote", emp.WorkRemote);
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
                            set FName = @FName, LName = @LName,
                            Email = @Email, Passw = @Passw,
                            EmpRole = @EmpRole, Gender = @Gender,
                            BirthDate = @BirthDate, Nationality = @Nationality,
                            EmpStatus = @EmpStatus, DeskNr = @DeskNr,
                            OfficeName = @OfficeName, FloorNr = @FloorNr,
                            BuildingName = @BuildingName, WorkRemote = @WorkRemote where ID = @ID
                            
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
                    myCommand.Parameters.AddWithValue("@FName", emp.FName);
                    myCommand.Parameters.AddWithValue("@LName", emp.LName);
                    myCommand.Parameters.AddWithValue("@Email", emp.Email);
                    myCommand.Parameters.AddWithValue("@Passw", emp.Passw);
                    myCommand.Parameters.AddWithValue("@EmpRole", emp.EmpRole);
                    myCommand.Parameters.AddWithValue("@Gender", emp.Gender);
                    myCommand.Parameters.AddWithValue("@BirthDate", emp.BirthDate);
                    myCommand.Parameters.AddWithValue("@Nationality", emp.Nationality);
                    myCommand.Parameters.AddWithValue("@EmpStatus", emp.EmpStatus);
                    myCommand.Parameters.AddWithValue("@DeskNr", emp.DeskNr);
                    myCommand.Parameters.AddWithValue("@OfficeName", emp.OfficeName);
                    myCommand.Parameters.AddWithValue("@FloorNr", emp.FloorNr);
                    myCommand.Parameters.AddWithValue("@BuildingName", emp.BuildingName);
                    myCommand.Parameters.AddWithValue("@WorkRemote", emp.WorkRemote);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated succesfuly");
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
            return new JsonResult("Deleted succesfuly");
        }
    }
}
