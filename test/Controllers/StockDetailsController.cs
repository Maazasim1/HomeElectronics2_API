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
    public class StockDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StockDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select ID,Item_ModelNumber,WareHouseID,Quantity,StockAdd_Datetime from dbo.StockDetails";
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

        public JsonResult Post(StockDetails std)
        {
            string DateToday = DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;
            string itemDetailsCheck_query = "Select count(*) from ItemDetails where  ModelNumber like '" + std.Item_ModelNumber + "'";
            //string itemDetails_query = @"insert into dbo.ItemDetails (ItemType,ItemBrand,ModelNumber,UnitPrice) values ('" + std.ItemType + @"','" + std.ItemBrand + @"','" + std.Item_ModelNumber + @"','" + std.UnitPrice + @"')";
            //  string query = @"insert into dbo.StockDetails (Item_ModelNumber,WareHouseID,Quantity,StockAdd_Datetime) values ('" + std.Item_ModelNumber + @"','" + std.WareHouseID + @"','" + std.Quantity + @"'," + DateToday + @")";


            string query = "Insert into StockDetails values('" + std.Item_ModelNumber + "'," + std.WareHouseID + "," + std.UnitPrice + "," + DateToday + "," + std.UnitPrice + ")";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HomeElectronicsAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                using (SqlCommand myCommand = new SqlCommand(itemDetailsCheck_query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    if (table.Rows[0][0].ToString() == "0")
                    {
                        return new JsonResult("SKU-Number does not exist");
                    }
                    else
                    {
                        string StockCheckQuery = "Select count(*) from StockDetails where Item_ModelNumber like '" + std.Item_ModelNumber + "' and WareHouseID=" + std.WareHouseID + "";
                        using (SqlCommand myCommand4 = new SqlCommand(StockCheckQuery, myCon))
                        {

                            SqlDataReader myReader4;
                            DataTable table4 = new DataTable();
                            myReader4 = myCommand4.ExecuteReader();
                            table4.Load(myReader4);
                            if (table4.Rows[0][0].ToString() == "0")
                            {
                                using (SqlCommand myCommand3 = new SqlCommand(query, myCon))
                                {

                                    SqlDataReader myReader3;
                                    DataTable table3 = new DataTable();
                                    myReader3 = myCommand3.ExecuteReader();
                                    table3.Load(myReader3);
                                    myReader3.Close();

                                }
                            }
                            else
                            {
                                string StockQuantity = "Select Quantity from StockDetails where Item_ModelNumber like '" + std.Item_ModelNumber + "' and WareHouseID=" + std.WareHouseID;
                                using (SqlCommand myCommand5 = new SqlCommand(StockQuantity, myCon))
                                {

                                    SqlDataReader myReader5;
                                    DataTable table5 = new DataTable();
                                    myReader5 = myCommand5.ExecuteReader();
                                    table5.Load(myReader5);
                                    int currentquantity = Convert.ToInt32(table5.Rows[0][0].ToString());
                                    currentquantity = currentquantity + std.Quantity;
                                    string updateQuery = "Update StockDetails set quantity=" + currentquantity + " where Item_ModelNumber like '" + std.Item_ModelNumber + "' and WareHouseID=" + std.WareHouseID;
                                    using (SqlCommand myCommand8 = new SqlCommand(updateQuery, myCon))
                                    {

                                        SqlDataReader myReader8;
                                        DataTable table8 = new DataTable();
                                        myReader8 = myCommand8.ExecuteReader();
                                        table8.Load(myReader8);
                                        myReader8.Close();

                                    }
                                    myReader5.Close();

                                }
                            }
                            myReader4.Close();

                        }

                        myCon.Close();
                    }
                    return new JsonResult("Added!!!!!");
                }
                myReader.Close();

            }
        

        }

        [HttpPut("{id}")]

        public JsonResult Put(StockDetails std,int id)
        {
            string query = @"update dbo.StockDetails set Item_ModelNumber = '" + std.Item_ModelNumber + @"',WareHouseID = '" + std.WareHouseID + @"',Quantity = '" + std.Quantity + @"',StockAdd_Datetime = '" + DateTime.Now + @"' where ID = '" + id + @"'";
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
            string query = @"delete from dbo.StockDetails where ID='" + id + @"' ";
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