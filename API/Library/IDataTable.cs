using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;
using System.Web;

namespace API.Classes
{
	public class IDataTable
	{

		public static List<T> To<T>(DataTable table)
		{
			List<T> list = new List<T>();
			PropertyInfo[] properties = typeof(T).GetProperties();

			foreach (DataRow row in table.Rows)
			{
				var item = Activator.CreateInstance<T>();
				foreach (DataColumn col in table.Columns)
				{
					foreach (PropertyInfo property in properties)
					{
						if (property.Name == col.ColumnName) {
							property.SetValue(item, row[col.ColumnName], null);
							break;
						}
					}
				}
				list.Add(item);
			}

			return list;
		}

	}
}