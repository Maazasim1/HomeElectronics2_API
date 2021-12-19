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
    public class BillChild_POSController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public BillChild_POSController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.BillChild_POS";
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

        public JsonResult Post(BillChild_POS bill)
        {
            string query = @"insert into dbo.BillChild_POS (ItemSKU,ItemPrice,ItemQuantity,BillMasterID) values ('"+bill.ItemSKU+"','"+bill.ItemPrice+"','"+bill.ItemQuantity+"','"+bill.BillMasterID+")";
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

        [HttpPut("{id}")]

        public JsonResult Put(BillChild_POS bill,int id)
        {
            string query = @"update dbo.BillChild_POS set ItemSKU = '"+bill.ItemSKU+"',BillMasterID="+bill.BillMasterID+",ItemPrice="+bill.ItemPrice+",ItemQuantity="+bill.ItemQuantity+" where BillChildID="+id;
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
            string query = @"delete from dbo.BillChild_POS where BillMasterID='"+ id +@"' ";
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