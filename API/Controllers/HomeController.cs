using System;
using System.Collections.Generic;
using System.Configuration;
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

            YardCapacityDBContext db = new YardCapacityDBContext();

            Yard yard = db.Yard.SingleOrDefault(m => m.YardId == yardId);

            ViewBag.yard = (yard != null) ? yard : new Yard();

            ViewBag.Title = $"Yarda {yard}";

            return View();
        }

        public JsonResult GetUpdate(int id)
		{
            List<Yard> yards = new List<Yard>();
            string connStr = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            SqlUpdate update = new SqlUpdate(connStr);
            string query = $"SELECT * FROM Yard WHERE YardId = {id}";
            yards = update.Updates<Yard>(query);
            return Json(yards, JsonRequestBehavior.AllowGet);
		}

    }
}
