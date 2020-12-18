using System;
using System.Collections.Generic;
using System.Text;

namespace QSP.SqlCodeGen.Business
{
	public class Schema
	{
		#region Constructors
		public Schema()
		{

		} 
		#endregion

		#region Fields
		protected string name = "";
		protected List<Table> tables = new List<Table>(); 
		#endregion

		#region Properties
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public List<Table> Tables
		{
			get { return tables; }
			set { tables = value; }
		}
		#endregion
		
	}
}
