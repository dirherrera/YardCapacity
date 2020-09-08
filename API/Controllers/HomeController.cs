using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Description;
using System.Web.Mvc;
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
            
            
            string query = $"SELECT * FROM Yard WHERE YardId = {yardId}";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDependency.Start(connStr);
            SqlDependency dependency = new SqlDependency(cmd);
            dependency.OnChange += new OnChangeEventHandler(DBNotificatoion.Notify);

            conn.Open();

            List<Yard> yards = IDataTable.To<Yard>(DAO.Exec(cmd));
            Yard yard = yards.Find(y => y.YardId == yardId);
            
            
            ViewBag.yard = (yard != null) ? yard : new Yard();
            ViewBag.Title = $"Yarda {yard}";
            
            
            return View();
        }

    }
}
