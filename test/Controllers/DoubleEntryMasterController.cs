using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using test.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoubleEntryMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DoubleEntryMasterController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        // GET: api/<DoubleEntryMasterController>
        [HttpGet]
        public JsonResult Get()
        {
            string query = "select * from DoubleEntryMaster";
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

            // GET api/<DoubleEntryMasterController>/5
            [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = "select * from DoubleEntryMaster where DoubleEntryID="+id;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HomeElectronicsAppCon");
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

        // POST api/<DoubleEntryMasterController>
        [HttpPost]
        public JsonResult Post([FromBody] DoubleEntryMaster value)
        {
            string AddDE = "Insert into DoubleEntryMaster values('" + value.TransactionDate + "','" + value.CreatedBy + "'," + value.CreditAccount + "," + value.CreditAmount + "," + value.DebitAccount + "," + value.DebitAmount + ",'" + value.Narration + "')";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HomeElectronicsAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(AddDE, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                   
                    myReader.Close();
                    myCon.Close();
                }


            }
            return new JsonResult("Added!!!");
        }

        // PUT api/<DoubleEntryMasterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoubleEntryMasterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
