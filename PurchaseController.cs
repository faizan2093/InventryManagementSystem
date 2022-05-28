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
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            List<Purc> list = new List<Purc>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetPurchaseDetails",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    Purc pu = new Purc();
                    pu.id = Convert.ToInt32(rdr["id"]);
                    pu.Purchase_Product = Convert.ToString(rdr["Purchase_Product"]);
                    pu.Purchase_Qnty = Convert.ToInt32(rdr["Purchase_Qnty"]);
                    pu.Date_Of_Purchase = Convert.ToDateTime(rdr["Date_Of_Purchase"]);
                    list.Add(pu);
                }
            }
           
            return View("PurchaseList",list);
        }

        public ActionResult Edit(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Purc purc = new Purc();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetPurchaseDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    purc.id = Convert.ToInt32(rdr["id"]);
                    purc.Purchase_Product = Convert.ToString(rdr["Purchase_Product"]);
                    purc.Purchase_Qnty = Convert.ToInt32(rdr["Purchase_Qnty"]);
                    purc.Date_Of_Purchase = Convert.ToDateTime(rdr["Date_Of_Purchase"]);
                }
            }
            return View(purc);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id)
        {
            Purc pu = new Purc();
            UpdateModel(pu);
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Purc purc = new Purc();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_UpdatePurchaseDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Purchase_Product", pu.Purchase_Product);
                cmd.Parameters.AddWithValue("@Purchase_Qnty", pu.Purchase_Qnty);
                cmd.Parameters.AddWithValue("@Date_Of_Purchase", pu.Date_Of_Purchase);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Purc purc = new Purc();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetPurchaseDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    purc.id = Convert.ToInt32(rdr["id"]);
                    purc.Purchase_Product = Convert.ToString(rdr["Purchase_Product"]);
                    purc.Purchase_Qnty = Convert.ToInt32(rdr["Purchase_Qnty"]);
                    purc.Date_Of_Purchase = Convert.ToDateTime(rdr["Date_Of_Purchase"]);
                }
            }
            return View(purc);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            Purc pu = new Purc();
            UpdateModel(pu);
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Purc purc = new Purc();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_InsertPurchaseDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Purchase_Product", pu.Purchase_Product);
                cmd.Parameters.AddWithValue("@Purchase_Qnty", pu.Purchase_Qnty);
                cmd.Parameters.AddWithValue("@Date_Of_Purchase", pu.Date_Of_Purchase);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Purc purc = new Purc();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_DeletePurchaseDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);      
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}