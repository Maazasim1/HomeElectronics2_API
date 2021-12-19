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
            string query = @"select * from dbo.BillMaster_POS";
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


        [HttpPost]

        public JsonResult Post(BillMaster_POS billm)
        {
            BillChild_POS i = new BillChild_POS();
            // string query = @"insert into dbo.BillMaster_POS (BillCreatedBy,BillCreatedOn,BillModifiedOn,CustomerName,CustomerPhoneNumber,CustomerAddress,DeliveryCharges,InstallationChares,totalAmount) values ('" + billm.BillCreatedBy + @"','" + DateTime.Now + @"','"  + @"','" + billm.BillModifiedOn + @"','" + billm.CustomerName + @"','" + billm.CustomerPhoneNumber + @"','" + billm.CustomerAddress + @"','" + billm.DeliveryCharges + @"','" + billm.InstallationChares + @" ," + billm.totalAmount + @"')";
            string query = "Insert into BillMaster_POS values('"+billm.BillCreatedBy+"','"+billm.BillCreatedOn+"',NULL,'"+billm.CustomerCNIC+"','"+billm.DeliveryCharges+","+billm.InstallationChares+","+billm.totalAmount+")";
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
            string fetchFromBillTemp = "select * from Bill_Child_Temp";
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                int BillMasterID;
                DataTable table2 = new DataTable();
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(fetchFromBillTemp, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table2.Load(myReader); 
                    myReader.Close();
                    
                }

                string BillMaster = "select BillMasterNo from BillMaster_POS where CustomerName like '" + billm.CustomerCNIC + "' And BillCreatedBy like '" + billm.BillCreatedBy + "'";
                DataTable table3 = new DataTable();
                using (SqlCommand myCommand = new SqlCommand(BillMaster, myCon))
                {
                    
                    myReader = myCommand.ExecuteReader();
                    table3.Load(myReader);
                    BillMasterID = Convert.ToInt32(table3.Rows[0][0]);
                    myReader.Close();
                    
                }

                if (table2.Rows!=null)
                {
                    
                    int count = table2.Rows.Count;
                    int count2 = table2.Columns.Count;
                    for(int j=0;j< count; j++)
                    {

                      
                        
                        
                        i.BillMasterID = BillMasterID;  i.ItemSKU = table2.Rows[j][1].ToString();   i.ItemBrand = table2.Rows[j][2].ToString(); i.ItemType = table2.Rows[j][3].ToString();  i.ItemPrice = Convert.ToInt32(table2.Rows[j][4]);   i.ItemQuantity = Convert.ToInt32(table2.Rows[j][5]);
                        string Add_to_BillChildPOS = "Insert into BillChild_POS values(" + BillMasterID + ",'" + i.ItemSKU + "',"+i.ItemPrice+","+i.ItemQuantity+")";


                        using (SqlCommand myCommand = new SqlCommand(Add_to_BillChildPOS, myCon))
                        {
                            
                            myReader = myCommand.ExecuteReader();
                            
                           
                            myReader.Close();

                        }


                    }
                    string truncate_temp = "Truncate table Bill_Child_Temp";
                    using (SqlCommand myCommand = new SqlCommand(truncate_temp, myCon))
                    {

                        myReader = myCommand.ExecuteReader();


                        myReader.Close();

                    }
                }


                myCon.Close();
            }
            
            
                return new JsonResult("Added!!!!!");

        }

        [HttpPut]

        public JsonResult Put(BillMaster_POS billm)
        {
            string query = @"update dbo.BillMaster_POS set BillMasterNO = '" + billm.BillMasterNO + @"',BillCreatedBy = '" + billm.BillCreatedBy +  @"',BillModifiedOn = '" + DateTime.Now + @"',CustomerName='" + billm.CustomerName + @"',CustomerPhoneNumber='" + billm.CustomerPhoneNumber + @"',CustomerAddress='" + billm.CustomerAddress + @"',DeliveryCharges='" + billm.DeliveryCharges + @"',InstallationChares='" + billm.InstallationChares + @"' where BillMasterNO = '" + billm.BillMasterNO + @"'";
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
                    table.Load(myReader); 
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted!!!!!");

        }
    }
}