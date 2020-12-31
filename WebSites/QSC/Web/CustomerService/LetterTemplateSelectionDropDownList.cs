using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Common.TableDef;
using Business.Objects;
using QSP.WebControl;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for LetterTemplateSelectionDropDownList.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:LetterTemplateSelectionDropDownList runat=server></{0}:LetterTemplateSelectionDropDownList>")]
	public class LetterTemplateSelectionDropDownList : QSP.WebControl.DropDownListInteger
	{
		private const string CACHED_DATASOURCE_NAME = "LetterTemplateDataSet";
		LetterTemplate letterTemplate = new LetterTemplate();

		private LetterTemplateDataSet LetterTemplateDataSet 
		{
			get 
			{
				return (LetterTemplateDataSet) DataSource;
			}
		}

		protected override bool EnableCache
		{
			get
			{
				return true;
			}
			set { }
		}

		protected override string CachedDataSourceName
		{
			get
			{
				return CACHED_DATASOURCE_NAME;
			}
			set { }
		}

		public override DateTime AbsoluteExpiration
		{
			get
			{
				return DateTime.Now.AddHours(12);
			}
			set { }
		}

		public override string DataMember
		{
			get
			{
				return LetterTemplateDataSet.LetterTemplate.TableName;
			}
			set { }
		}

		public override string DataTextField
		{
			get
			{
				return LetterTemplateDataSet.LetterTemplate.NameColumn.ColumnName;
			}
			set { }
		}

		public override string DataValueField
		{
			get
			{
				return LetterTemplateDataSet.LetterTemplate.IDColumn.ColumnName;
			}
			set { }
		}

		protected override object LoadCachedData()
		{
			letterTemplate.GetAll();

			return letterTemplate.dataSet;
		}

		public LetterTemplateItem SelectedLetterTemplateItem
		{
			get 
			{
				LetterTemplateItem item = null;

				if(Value != Convert.ToInt32(InitialValue))
				{
					item = letterTemplate.GetOneByID(LetterTemplateDataSet, Value);
				}

				return item;
			}
		}
	}
}
