using System;
using System.Collections.Generic;
using System.Text;

namespace QSP.SqlCodeGen.Business
{
	public class Table
	{
		#region Constructors
		public Table()
		{

		} 
		#endregion

		#region Fields
		protected string name = "";
		protected bool map = true;
		protected string entityName = "";
		protected string entityNamespace = "";
		protected string entityInherits = "";
		protected List<Column> columns = new List<Column>();
		protected string tableType = "BASE TABLE";
		#endregion

		#region Properties
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public bool Map
		{
			get { return this.map; }
			set { this.map = value; }
		}

		public string EntityName
		{
			get { return this.entityName; }
			set { this.entityName = value; }
		}

		public string EntityNamespace
		{
			get { return this.entityNamespace; }
			set { this.entityNamespace = value; }
		}

		public string EntityInherits
		{
			get { return this.entityInherits; }
			set { this.entityInherits = value; }
		}

		public List<Column> Columns
		{
			get { return this.columns; }
			set { this.columns = value; }
		}

		public string TableType
		{
			get { return this.tableType; }
			set { this.tableType = value; }
		}

		public bool IsCompositeKey
		{
			get
			{
				if (this.columns == null)
					return false;
				else
				{
					int count = 0;
					foreach (Column c in this.columns)
					{
						if (c.PrimaryKey)
							count++;
					}

					if (count > 1)
						return true;
					else
						return false;
				}
			}
		}

		public bool IsKeyAssigned
		{
			get
			{
				if (this.columns == null)
					return false;
				else
				{
					foreach (Column c in this.columns)
					{
						if (c.PrimaryKey && ! c.Identity)
							return true;
					}
					return false;
				}
			}
		}
		#endregion

		#region Methods
		
		public List<Column> GetPrimaryKeys()
		{
			List<Column> keys = new List<Column>();
			if (this.columns != null)
			{
				foreach (Column c in this.columns)
				{
					if (c.PrimaryKey)
						keys.Add(c);
				}
			}
			return keys;
		}
		#endregion
	}
}
