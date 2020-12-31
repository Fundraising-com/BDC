using System;
using System.Data;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for DataViewObject.
	/// </summary>
	public class DataViewObject:DataView
	{
		public DataViewObject()
		{
			
		}

		public int GetPosition(string Field,int Value)
		{
		
			int index = 0;
			foreach(DataRow row in this.Table.Rows)
			{
				if(Convert.ToInt32(row[Field]) == Value)
					return index+1;

				index++;
			}
			return 0;
		}
	}
}
