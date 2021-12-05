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
    public class StockTransferController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StockTransferController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select ItemModelNumber,From_LocationID,To_LocationID,Stock_TransferDetailsID from dbo.StockTransfer";
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

        public StatusCodeResult Post(StockTransfer trf)
        {
            string query = @"insert into dbo.StockTransfer (ItemModelNumber,From_LocationID,To_LocationID) values ('" + trf.ItemModelNumber + @"','" + trf.From_LocationID + @"','" + trf.To_LocationID + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HomeElectronicsAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                int currentQuantityFromTo;
                myCon.Open();
                string CheckStockFr = "Select Quantity from StockDetails where Item_ModelNumber like '" + trf.ItemModelNumber + "' and WareHouseID=" + trf.From_LocationID;
                string CheckStockTo = "Select Quantity from StockDetails where Item_ModelNumber like '" + trf.ItemModelNumber + "' and WareHouseID=" + trf.To_LocationID;
                string StockCheckQuery = "Select count(*) from StockDetails where Item_ModelNumber like '" + trf.ItemModelNumber + "' and WareHouseID=" + trf.To_LocationID + "";
                int StockCheck_ForTo;
                int StockCheck_ForFrom;
                using (SqlCommand myCommand4 = new SqlCommand(StockCheckQuery, myCon))
                {

                    SqlDataReader myReader4;
                    DataTable table4 = new DataTable();
                    myReader4 = myCommand4.ExecuteReader();
                    table4.Load(myReader4);
                     StockCheck_ForTo = Convert.ToInt32(table4.Rows[0][0]);

                    string StockCheckQuery2 = "Select count(*) from StockDetails where Item_ModelNumber like '" + trf.ItemModelNumber + "' and WareHouseID=" + trf.From_LocationID + "";
                    using (SqlCommand myCommand9 = new SqlCommand(StockCheckQuery2, myCon))
                    {

                        SqlDataReader myReader9;
                        DataTable table9 = new DataTable();
                        myReader9 = myCommand9.ExecuteReader();
                        table9.Load(myReader9);
                        StockCheck_ForFrom = Convert.ToInt32(table9.Rows[0][0]);
                    }
                }
                if(StockCheck_ForFrom==0)
                {
                    return StatusCode(416);
                    //return new JsonResult("Item not  in From Location");
                }
                else if(StockCheck_ForTo==0)
                {
                    
                }
                    using (SqlCommand myCommand3 = new SqlCommand(CheckStockTo, myCon))
                    {
                        DataTable table3 = new DataTable();
                        SqlDataReader myReader3;
                        myReader3 = myCommand3.ExecuteReader();
                        table3.Load(myReader3);
                        currentQuantityFromTo = Convert.ToInt32(table3.Rows[0][0]);
                        myReader3.Close();
                    }

                    using (SqlCommand myCommand = new SqlCommand(CheckStockFr, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        int currentQuantityFromWr = Convert.ToInt32(table.Rows[0][0]);

                        if ((currentQuantityFromWr - trf.Quantity) < 0)
                        {
                            myReader.Close();
                            myCon.Close();
                            return StatusCode(418);
                            //return new JsonResult("Stock less than available");
                        }
                        else
                        {
                            using (SqlCommand myCommand2 = new SqlCommand(query, myCon))
                            {

                                DataTable table2 = new DataTable();
                                SqlDataReader myReader2;
                                myReader2 = myCommand2.ExecuteReader();
                                table2.Load(myReader2);
                                myReader2.Close();
                                string UpdateStock = "update StockDetails set Quantity=" + (currentQuantityFromTo + trf.Quantity) + " where WareHouseID=" + trf.To_LocationID + " and Item_ModelNumber like '" + trf.ItemModelNumber + "'";
                                using (SqlCommand myCommand40 = new SqlCommand(UpdateStock, myCon))
                                {
                                    DataTable table40 = new DataTable();
                                    SqlDataReader myReader40;
                                    myReader40 = myCommand40.ExecuteReader();
                                    table40.Load(myReader40);

                                    myReader40.Close();
                                }
                                string UpdateStockFrom = "update StockDetails set Quantity=" + (currentQuantityFromWr - trf.Quantity) + " where WareHouseID=" + trf.From_LocationID + " and Item_ModelNumber like '" + trf.ItemModelNumber + "'";
                                using (SqlCommand myCommand5 = new SqlCommand(UpdateStockFrom, myCon))
                                {
                                    DataTable table5 = new DataTable();
                                    SqlDataReader myReader5;
                                    myReader5 = myCommand5.ExecuteReader();
                                    table5.Load(myReader5);

                                    myReader5.Close();
                                }
                                myCon.Close();
                                //return new JsonResult("Added!!!!!");
                            }   return StatusCode(200);
                        }


                    }






                
            }
            

        }

        [HttpPut]

        public JsonResult Put(StockTransfer trf)
        {
            string query = @"update dbo.StockTransfer set ItemModelNumber = '" + trf.ItemModelNumber + @"',From_LocationID = '" + trf.From_LocationID + @"',To_LocationID = '" + trf.To_LocationID + @"',ItemPrice = '" + trf.Stock_TrasferDetailsID + @"'";
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
            string query = @"delete from dbo.StockTransfer where ItemModelNumber='" + id + @"' ";
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