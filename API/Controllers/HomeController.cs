using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.WebPages;
using API.Classes;
using API.Models;
using Microsoft.Ajax.Utilities;

namespace API.Controllers
{
    public class HomeController : Controller
    {

        public string connStr = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            int yardId = Convert.ToInt32(HttpContext.Request["yardid"]);
            
            
            string query = $"SELECT [YardId],[Name],[cur_equipment],[cur_units],[max_equipment],[max_units],[update] FROM Yard WHERE YardId = {yardId}";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            List<Yard> yards = IDataTable.To<Yard>(DAO.Exec(cmd));
            conn.Close();

            SqlDependencyService service = new SqlDependencyService();
            service.Config();

            Yard yard = yards.Find(y => y.YardId == yardId);
            
            ViewBag.yard = (yard != null) ? yard : new Yard();
            string Name = (yard == null)?"":yard.Name;
            ViewBag.Title = $"Yarda {Name}";
            
            
            return View();
        }

        public JsonResult GetUpdate(int id)
		{
            string query = $"SELECT * FROM Yard WHERE YardId = {id}";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            List<Yard> yards = IDataTable.To<Yard>(DAO.Exec(cmd));
            conn.Close();
            Yard yard = yards.Find(y => y.YardId == id);
            return Json(yard, JsonRequestBehavior.AllowGet);
        }

    }
}
