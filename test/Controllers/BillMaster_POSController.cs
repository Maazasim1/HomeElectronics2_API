using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using test.Models;
namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillMaster_POSController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public BillMaster_POSController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select BillMasterNO,BillCreatedBy,BillCreatedOn,BillChildID,BillModifiedOn,CustomerName,CustomerPhoneNumber,CustomerAddress,DeliveryCharges,InstallationChares from dbo.BillMaster_POS";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HomeElectronicsAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


        [HttpPost]

        public JsonResult Post(BillMaster_POS billm)
        {
            string query = @"insert into dbo.BillMaster_POS (BillMasterNO,BillCreatedBy,BillCreatedOn,BillChildID,BillModifiedOn,CustomerName,CustomerPhoneNumber,CustomerAddress,DeliveryCharges,InstallationChares) values ('" + billm.BillMasterNO + @"','" + billm.BillCreatedBy + @"','" + DateTime.Now + @"','" + billm.BillChildID + @"','" + billm.BillModifiedOn + @"','" + billm.CustomerName + @"','" + billm.CustomerPhoneNumber + @"','" + billm.CustomerAddress + @"','" + billm.DeliveryCharges + @"','" + billm.InstallationChares + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HomeElectronicsAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added!!!!!");

        }

        [HttpPut]

        public JsonResult Put(BillMaster_POS billm)
        {
            string query = @"update dbo.BillMaster_POS set BillMasterNO = '" + billm.BillMasterNO + @"',BillCreatedBy = '" + billm.BillCreatedBy + @"',BillChildID = '" + billm.BillChildID + @"',BillModifiedOn = '" + DateTime.Now + @"',CustomerName='" + billm.CustomerName + @"',CustomerPhoneNumber='" + billm.CustomerPhoneNumber + @"',CustomerAddress='" + billm.CustomerAddress + @"',DeliveryCharges='" + billm.DeliveryCharges + @"',InstallationChares='" + billm.InstallationChares + @"' where BillMasterNO = '" + billm.BillMasterNO + @"'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HomeElectronicsAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated!!!!!");

        }

        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.BillMaster_POS where BillMasterNO='" + id + @"' ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HomeElectronicsAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted!!!!!");

        }
    }
}