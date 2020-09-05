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
		public SqlUpdate(string connStr)
		{
			this.conn = new SqlConnection(connStr);
		}

		public List<T> Updates<T>(string query)
		{
			var messages = new List<T>();
			using (var cmd = new SqlCommand(query, conn))
			{
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				var dependency = new SqlDependency(cmd);
				SqlDependency.Start(conn.ConnectionString);
				dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
				DataSet data = new DataSet();
				adapter.Fill(data);
				DataTable table = data.Tables[0];
				SqlDeserializer deserializer = new SqlDeserializer(table);
				messages = deserializer.Get<T>();
			}
			return messages;
		}

		void dependency_OnChange(object sender, SqlNotificationEventArgs e)
		{
			if (e.Type == SqlNotificationType.Change)
				MonitorHub.SendMessages();
		}

	}
}