using API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using API.Classes;
using System.Web.Caching;

namespace API.Classes
{

	public class DAO
	{

		public static DataTable Exec(SqlCommand cmd)
		{
			DataTable table;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			DataSet data = new DataSet();
			adapter.Fill(data);
			table = data.Tables[0];
			return table;
		}

	}
}