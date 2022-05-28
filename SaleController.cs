using InventryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace InventryManagementSystem.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Index()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            List<Sale> list = new List<Sale>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("usp_GetSaleDetails",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    Sale s = new Sale();
                    s.id = Convert.ToInt32(rdr["id"]);
                    s.Sale_Product = Convert.ToString(rdr["Sale_Product"]);
                    s.Sale_Qnty = Convert.ToInt32(rdr["Sale_Qnty"]);
                    s.Sale_Date = Convert.ToDateTime(rdr["Sale_Date"]);
                    list.Add(s);
                }
            }
            return View("SaleList", list);
        }

        public ActionResult Edit(int id)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Sale sale = new Sale();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("usp_GetSaleDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    sale.id = Convert.ToInt32(rdr["id"]);
                    sale.Sale_Product = Convert.ToString(rdr["Sale_Product"]);
                    sale.Sale_Qnty = Convert.ToInt32(rdr["Sale_Qnty"]);
                    sale.Sale_Date = Convert.ToDateTime(rdr["Sale_Date"]);
                    
                }
            }
            return View(sale);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_post(int id)
        {
            Sale s = new Sale();
            UpdateModel(s);
            string connectionstring = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Sale sale = new Sale();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("usp_UpdateSaleDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Sale_Product", s.Sale_Product);
                cmd.Parameters.AddWithValue("@Sale_Qnty", s.Sale_Qnty);
                cmd.Parameters.AddWithValue("@Sale_Date", s.Sale_Date);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Sale sale = new Sale();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("usp_GetSaleDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    sale.id = Convert.ToInt32(rdr["id"]);
                    sale.Sale_Product = Convert.ToString(rdr["Sale_Product"]);
                    sale.Sale_Qnty = Convert.ToInt32(rdr["Sale_Qnty"]);
                    sale.Sale_Date = Convert.ToDateTime(rdr["Sale_Date"]);

                }
            }
            return View(sale);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_post()
        {
            Sale s = new Sale();
            UpdateModel(s);
            string connectionstring = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Sale sale = new Sale();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("usp_InsertSaleDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Sale_Product", s.Sale_Product);
                cmd.Parameters.AddWithValue("@Sale_Qnty", s.Sale_Qnty);
                cmd.Parameters.AddWithValue("@Sale_Date", s.Sale_Date);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
            
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Sale sale = new Sale();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
               
                SqlCommand cmd = new SqlCommand("usp_DeleteSaleDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}