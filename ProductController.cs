using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventryManagementSystem.Models;

namespace InventryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            List<Prod> list = new List<Prod>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetProductDetails",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    Prod p = new Prod();
                    
                    p.id = Convert.ToInt32(rdr["id"]);

                    p.product_name = Convert.ToString(rdr["product_name"]);
                    
                    p.product_qnty = Convert.ToString(rdr["product_qnty"]);
                    list.Add(p);
                }
            }
            return View("ProductList",list);
        }


        /// <summary>
        /// fetch product Details on id
        /// </summary>
        /// <param name="id">product id parameter</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Prod prod = new Prod();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetProductDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                { 
                    prod.id = Convert.ToInt32(rdr["id"]);

                    prod.product_name = Convert.ToString(rdr["product_name"]);

                    prod.product_qnty = Convert.ToString(rdr["product_qnty"]);
                    
                }
            }
            return View(prod);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id)
        {
            Prod p = new Prod();
            UpdateModel(p);
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Prod prod = new Prod();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_UpdateProductDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@product_name", p.product_name);
                cmd.Parameters.AddWithValue("@product_qnty", p.product_qnty);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Prod prod = new Prod();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetProductDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    prod.id = Convert.ToInt32(rdr["id"]);

                    prod.product_name = Convert.ToString(rdr["product_name"]);

                    prod.product_qnty = Convert.ToString(rdr["product_qnty"]);

                }
            }
            return View(prod);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            Prod p = new Prod();
            UpdateModel(p);

            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Prod prod = new Prod();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_InsertProductDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@product_name", p.product_name);
                cmd.Parameters.AddWithValue("@product_qnty", p.product_qnty);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            Prod prod = new Prod();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_DeleteProductDetailsBasedOnId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public JsonResult IsProductNameisExist(string product_name)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbProd"].ConnectionString;
            List<Prod> list = new List<Prod>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetProductDetails", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Prod p = new Prod();

                    p.id = Convert.ToInt32(rdr["id"]);

                    p.product_name = Convert.ToString(rdr["product_name"]);

                    p.product_qnty = Convert.ToString(rdr["product_qnty"]);
                    list.Add(p);
                }
            }
            Prod productdata = list.FirstOrDefault(x => x.product_name == product_name);
            if(productdata != null)
            
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}