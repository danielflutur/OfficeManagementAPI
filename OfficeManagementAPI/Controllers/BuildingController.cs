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
    public class BuildingController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BuildingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select BuildingName, NumberOfFloors, BuildingAddress from dbo.Building
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
        public JsonResult Post(Building bld)
        {
            string query = @"
                            insert into dbo.Building values(
                            @BuildingName, @NumberOfFloors, @BuildingAddress
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
                    myCommand.Parameters.AddWithValue("@BuildingName", bld.BuildingName);
                    myCommand.Parameters.AddWithValue("@NumberOfFloors", bld.NumberOfFloors);
                    myCommand.Parameters.AddWithValue("@BuildingAddress", bld.BuildingAddress);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added succesfuly");
        }
        [HttpPut]
        public JsonResult Put(Building bld)
        {
            string query = @"
                            update dbo.Building
                            set
                            NumberOfFloors = @NumberOfFloors,
                            BuildingAddress = @BuildingAddress 
                            where BuildingName = @BuildingName
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@BuildingName", bld.BuildingName);
                    myCommand.Parameters.AddWithValue("@NumberOfFloors", bld.NumberOfFloors);
                    myCommand.Parameters.AddWithValue("@BuildingAddress", bld.BuildingAddress);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated succesfuly");
        }
        [HttpDelete("{name}")]
        public JsonResult Delete(string name)
        {
            string query = @"
                            delete from dbo.Building where BuildingName = @BuildingName
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@BuildingName", name);
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
