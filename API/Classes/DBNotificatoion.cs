using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API.Classes
{
	public class DBNotificatoion
	{

		public static void Notify(object sender, SqlNotificationEventArgs e)
		{
			SqlDependency dependency = (SqlDependency)sender;
			dependency.OnChange -= Notify;

			if (e.Type == SqlNotificationType.Change)
			{
				MonitorHub.SendMessages();
			}

		}

	}
}