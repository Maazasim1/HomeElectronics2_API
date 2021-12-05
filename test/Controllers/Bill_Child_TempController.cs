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
        // GET: api/<Bill_Child_Temp>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Bill_Child_Temp>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Bill_Child_Temp>
        [HttpPost]
        public void Post(Bill_Child_Temp bct)
        {
            string Add_ChildTemp = "Insert into Bill_Child_Temp values('" + bct.ItemSKU + "','" + bct.ItemBrand + "','" + bct.ItemType + "'," + bct.ItemPrice + "," + bct.Quantity;
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
