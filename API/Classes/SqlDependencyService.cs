using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API.Classes
{
	public interface IDatabaseChangeNotificationService
	{
		void Config();
	}
	public class SqlDependencyService : IDatabaseChangeNotificationService
	{
		private string connStr = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

		public SqlDependencyService()
		{
		}

		public void Config()
		{
			SubscribeDatabaseChange();
		}

		private void SubscribeDatabaseChange()
		{
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				conn.Open();
				using (SqlCommand comm = new SqlCommand(@"SELECT [update] FROM [dbo].Yard", conn))
				{
					comm.Notification = null;
					SqlDependency dependency = new SqlDependency(comm);
					dependency.OnChange += UpdateChange;
					SqlDependency.Start(connStr);
					comm.ExecuteReader();
				}
			}
		}

		private void UpdateChange(object sender, SqlNotificationEventArgs e)
		{
			if (e.Type == SqlNotificationType.Change)
			{
				MonitorHub.Send("Dependency", "UpdateChange");
			}
			SubscribeDatabaseChange();
		}

	}
}