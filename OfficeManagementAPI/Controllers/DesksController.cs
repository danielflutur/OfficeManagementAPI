using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using OfficeManagementAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace OfficeManagementAPI.Controllers
{
    [Authorize(Roles = Role.Administrator)]
    [Route("api/[controller]")]
    [ApiController]
    public class DesksController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DesksController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select DeskID, EmployeeName, DeskNo, OfficeName, OfficeAdmin, BuildingName from dbo.Desks
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
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Desks dsk)
        {
            string query = @"
                        insert into dbo.Desks values(
                        @EmployeeName, @DeskNo, @OfficeName, @OfficeAdmin, @BuildingName
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
                    myCommand.Parameters.AddWithValue("@EmployeeName", dsk.EmployeeName);
                    myCommand.Parameters.AddWithValue("@DeskNo", dsk.DeskNo);
                    myCommand.Parameters.AddWithValue("@OfficeName", dsk.OfficeName);
                    myCommand.Parameters.AddWithValue("@OfficeAdmin", dsk.OfficeAdmin);
                    myCommand.Parameters.AddWithValue("@BuildingName", dsk.BuildingName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added succesfuly");
        }
        [HttpPut]
        public JsonResult Put(Desks dsk)
        {
            string query = @"
                        update dbo.Desks
                        set EmployeeName = @EmployeeName,
                        DeskNo = @DeskNo,
                        OfficeName = @OfficeName,
                        OfficeAdmin = @OfficeAdmin,
                        BuildingName = @BuildingName
                        where DeskID = @DeskID
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DeskID", dsk.DeskID);
                    myCommand.Parameters.AddWithValue("@EmployeeName", dsk.EmployeeName);
                    myCommand.Parameters.AddWithValue("@DeskNo", dsk.DeskNo);
                    myCommand.Parameters.AddWithValue("@OfficeName", dsk.OfficeName);
                    myCommand.Parameters.AddWithValue("@OfficeAdmin", dsk.OfficeAdmin);
                    myCommand.Parameters.AddWithValue("@BuildingName", dsk.BuildingName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated succesfuly");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        delete from dbo.Desks where DeskID = @DeskID
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DeskID", id);
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
