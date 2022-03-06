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

namespace OfficeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OfficeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select OfficeID, OfficeName, BuildingName, FloorNr,
                        NrOfDesks, NrOfFreeDesks, OfficeAdminName
                        from dbo.Office
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
        public JsonResult Post(Office office)
        {
            string query = @"
                        insert into dbo.Office values(
                        @OfficeName, @BuildingName, @FloorNr, @NrOfDesks,
                        @NrOfFreeDesks, @OfficeAdminName
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
                    myCommand.Parameters.AddWithValue("@OfficeName", office.OfficeName);
                    myCommand.Parameters.AddWithValue("@BuildingName", office.BuildingName);
                    myCommand.Parameters.AddWithValue("@FloorNr", office.FloorNr);
                    myCommand.Parameters.AddWithValue("@NrOfDesks", office.NrOfDesks);
                    myCommand.Parameters.AddWithValue("@NrOfFreeDesks", office.NrOfFreeDesks);
                    myCommand.Parameters.AddWithValue("@OfficeAdminName", office.OfficeAdminName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added succesfuly");
        }
        [HttpPut]
        public JsonResult Put(Office office)
        {
            string query = @"
                        update dbo.Office
                        set
                        OfficeName = @OfficeName,
                        BuildingName = @BuildingName,
                        FloorNr = @FloorNr,
                        NrOfDesks = @NrOfDesks,
                        NrOfFreeDesks = @NrOfFreeDesks,
                        OfficeAdminName = @OfficeAdminName
                        where OfficeID = @OfficeID
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@OfficeID", office.OfficeID);
                    myCommand.Parameters.AddWithValue("@OfficeName", office.OfficeName);
                    myCommand.Parameters.AddWithValue("@BuildingName", office.BuildingName);
                    myCommand.Parameters.AddWithValue("@FloorNr", office.FloorNr);
                    myCommand.Parameters.AddWithValue("@NrOfDesks", office.NrOfDesks);
                    myCommand.Parameters.AddWithValue("@NrOfFreeDesks", office.NrOfFreeDesks);
                    myCommand.Parameters.AddWithValue("@OfficeAdminName", office.OfficeAdminName);
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
                        delete from dbo.Office where OfficeID = @OfficeID
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@OfficeID", id);
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
