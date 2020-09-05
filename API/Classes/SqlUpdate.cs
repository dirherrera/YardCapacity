using API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using API.Classes;

namespace API.Classes
{

	public class SqlUpdate
	{
		private SqlConnection conn;
		private string query;
		private T t;

		public SqlUpdate(string connStr, string query)
		{
			this.conn = new SqlConnection(connStr);
			this.query = query;
		}

		public List<T> Updates<T>()
		{
			var list = new List<T>();
			using (var cmd = new SqlCommand(query, conn))
			{
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				var dependency = new SqlDependency(cmd);
				dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
				DataSet data = new DataSet();
				adapter.Fill(data);
				DataTable table = data.Tables[0];
				SqlDeserializer deserializer = new SqlDeserializer(table);
				list = deserializer.Get<T>();
			}
			return list;
		}

		public List<T> Get<T>(string query)
		{
			var list = new List<T>();
			using (var cmd = new SqlCommand(query, conn))
			{
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				DataSet data = new DataSet();
				adapter.Fill(data);
				DataTable table = data.Tables[0];
				SqlDeserializer deserializer = new SqlDeserializer(table);
				list = deserializer.Get<T>();
			}
			return list;
		}

		void dependency_OnChange(object sender, SqlNotificationEventArgs e)
		{
			if (e.Type == SqlNotificationType.Change && e.Info == SqlNotificationInfo.Update)
				MonitorHub.SendMessages();
			Updates<>();
		}

	}
}