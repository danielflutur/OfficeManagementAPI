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
    public class OfficesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OfficesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select OfficeID, OfficeName, BuildingName, FloorNo,
                        OfficeAdminName, TotalDesksCount, UsableDesksCount, OccupiedDesksCount
                        from dbo.Offices
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
        public JsonResult Post(Offices office)
        {
            string query = @"
                        insert into dbo.Offices values(
                        @OfficeName, @BuildingName, @FloorNo, @OfficeAdminName, @TotalDesksCount,
                        @UsableDesksCount, @OccupiedDesksCount
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
                    myCommand.Parameters.AddWithValue("@FloorNo", office.FloorNo);
                    myCommand.Parameters.AddWithValue("@OfficeAdminName", office.OfficeAdminName);
                    myCommand.Parameters.AddWithValue("@TotalDesksCount", office.TotalDesksCount);
                    myCommand.Parameters.AddWithValue("@UsableDesksCount", office.UsableDesksCount);
                    myCommand.Parameters.AddWithValue("@OccupiedDesksCount", office.OccupiedDesksCount);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added succesfuly");
        }
        [HttpPut]
        public JsonResult Put(Offices office)
        {
            string query = @"
                        update dbo.Offices
                        set
                        OfficeName = @OfficeName,
                        BuildingName = @BuildingName,
                        FloorNo = @FloorNo,
                        OfficeAdminName = @OfficeAdminName,
                        TotalDesksCount = @TotalDesksCount,
                        UsableDesksCount = @UsableDesksCount,
                        OccupiedDesksCount = @OccupiedDesksCount
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
                    myCommand.Parameters.AddWithValue("@FloorNo", office.FloorNo);
                    myCommand.Parameters.AddWithValue("@OfficeAdminName", office.OfficeAdminName);
                    myCommand.Parameters.AddWithValue("@TotalDesksCount", office.TotalDesksCount);
                    myCommand.Parameters.AddWithValue("@UsableDesksCount", office.UsableDesksCount);
                    myCommand.Parameters.AddWithValue("@OccupiedDesksCount", office.OccupiedDesksCount);
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
                        delete from dbo.Offices where OfficeID = @OfficeID
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
