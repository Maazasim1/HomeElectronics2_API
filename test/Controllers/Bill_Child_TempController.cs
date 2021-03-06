using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using test.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Bill_Child_TempController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Bill_Child_TempController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet("{SalesmanName}")]
        public JsonResult Get(String SalesmanName)
        {
            string Query = "Select * from Bill_Child_Temp where SalesmanName like '%"+SalesmanName+"%'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HomeElectronicsAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(Query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
           
        }

        // GET: api/<Bill_Child_Temp>
        [HttpGet]
        public JsonResult Get()
        {
            string query ="Select * from Bill_Child_Temp";
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

        // GET api/<Bill_Child_Temp>/5


        // POST api/<Bill_Child_Temp>
        [HttpPost]
        public void Post(Bill_Child_Temp bct)
        {

            string Add_ChildTemp = "Insert into Bill_Child_Temp values('" + bct.ItemSKU + "'," + bct.ItemPrice + "," + bct.ItemQuatity+",'"+bct.SalesmanName+"')";
            string sqlDataSource = _configuration.GetConnectionString("HomeElectronicsAppCon");
            SqlDataReader myReader;
            DataTable table = new DataTable();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(Add_ChildTemp, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }


        }

        // PUT api/<Bill_Child_Temp>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Bill_Child_Temp>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
