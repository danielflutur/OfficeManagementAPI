using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeManagementAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace OfficeManagementAPI.Controllers
{
    [Authorize(Roles = Role.Administrator)]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RequestsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select RequestNo, EmployeeName, RemotePercent, RequestMsg, ReqStatus from dbo.Requests
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
        public JsonResult Post(Requests req)
        {
            string query = @"
                            insert into dbo.Requests values(
                            @EmployeeName, @RemotePercent, @RequestMsg, @ReqStatus
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
                    myCommand.Parameters.AddWithValue("@EmployeeName", req.EmployeeName);
                    myCommand.Parameters.AddWithValue("@RemotePercent", req.RemotePercent);
                    myCommand.Parameters.AddWithValue("@RequestMsg", req.RequestMsg);
                    myCommand.Parameters.AddWithValue("@ReqStatus", req.ReqStatus);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added succesfuly");
        }
        [HttpPut]
        public JsonResult Put(Requests req)
        {
            string query = @"
                            update dbo.Requests
                            set
                            EmployeeName = @EmployeeName,
                            RemotePercent = @RemotePercent,
                            RequestMsg = @RequestMsg,
                            ReqStatus = @ReqStatus
                            where RequestNo = @RequestNo
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName", req.EmployeeName);
                    myCommand.Parameters.AddWithValue("@RemotePercent", req.RemotePercent);
                    myCommand.Parameters.AddWithValue("@RequestMsg", req.RequestMsg);
                    myCommand.Parameters.AddWithValue("@ReqStatus", req.ReqStatus);
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
                            delete from dbo.Requests where RequestNo = @RequestNo
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@RequestNo", id);
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
