using System;
using Common.TableDef;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for LetterTemplateItem.
	/// </summary>
	[Serializable]
	public class LetterTemplateItem
	{
		private int id = 0;
		private string name = String.Empty;
		private string description = String.Empty;
		private int status = 0;
		private string reportName = String.Empty;
		private string viewName = String.Empty;
		private string extendedTable = String.Empty;

		public LetterTemplateItem() { }

		public LetterTemplateItem(int id, string name, string description, int status, string reportName, string viewName, string extendedTable) : this() 
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.status = status;
			this.reportName = reportName;
			this.viewName = viewName;
			this.extendedTable = extendedTable;
		}

		public int ID 
		{
			get 
			{
				return id;
			}
			set 
			{
				id = value;
			}
		}

		public string Name 
		{
			get 
			{
				return name;
			}
			set 
			{
				name = value;
			}
		}

		public string Description 
		{
			get 
			{
				return description;
			}
			set 
			{
				description = value;
			}
		}

		public int Status 
		{
			get 
			{
				return status;
			}
			set 
			{
				status = value;
			}
		}

		public string ReportName 
		{
			get 
			{
				return reportName;
			}
			set 
			{
				reportName = value;
			}
		}

		public string ViewName 
		{
			get 
			{
				return viewName;
			}
			set 
			{
				viewName = value;
			}
		}

		public string ExtendedTable
		{
			get 
			{
				return extendedTable;
			}
			set 
			{
				extendedTable = value;
			}
		}

		public static LetterTemplateItem GetFromRow(LetterTemplateDataSet.LetterTemplateRow row) 
		{
			return new LetterTemplateItem(row.ID, row.Name, row.Description, row.Status, row.ReportName, row.ViewName, row.ExtendedTable);
		}
	}
}
