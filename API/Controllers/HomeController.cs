using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;
using API.Classes;
using API.Models;
using Microsoft.Ajax.Utilities;

namespace API.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            int yardId = Convert.ToInt32(HttpContext.Request["yardid"]);

            List<Yard> yards = new List<Yard>();
            string connStr = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            SqlUpdate update = new SqlUpdate(connStr);
            string query = $"SELECT * FROM Yard WHERE YardId = {yardId}";
            yards = update.Get<Yard>(query);
            Yard yard = yards.Find(y => y.YardId == yardId);

            ViewBag.yard = (yard != null) ? yard : new Yard();

            ViewBag.Title = $"Yarda {yard}";

            return View();
        }

        public JsonResult GetUpdate(int id)
		{
            List<Yard> yards = new List<Yard>();
            string connStr = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            SqlDependency.Start(connStr);
            SqlUpdate update = new SqlUpdate(connStr);
            string query = $"SELECT * FROM Yard WHERE YardId = {id}";
            yards = update.Updates<Yard>(query);
            return Json(yards, JsonRequestBehavior.AllowGet);
		}

    }
}
