using System;
using System.Data;
using QSPFulfillment.DataAccess.Common;
using System.ComponentModel;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.MarketingMgt.Control
{
	/// <summary>
	/// Summary description for CustomerServiceControl.
	/// </summary>
	public class MarketingMgtControl : QSPUserControl
	{
		private DataTable  dtbMain;
		private System.Text.RegularExpressions.Regex rgxValidCharacter = new System.Text.RegularExpressions.Regex("%");


		public  new MarketingMgtPage Page
		{
			get
			{
				return (MarketingMgtPage)base.Page;
			}
			set
			{
				base.Page = value;
			}
		}
		
		public DataTable DataSource
		{
			get
			{
				return dtbMain;
			}
			set
			{
				dtbMain = value;
			}
		}
		public virtual bool Validate()
		{
				return true;
		}
		protected bool ValidFromTo(DateTime From,DateTime To)
		{
			if(From.Date != System.DateTime.MinValue || To.Date != System.DateTime.MinValue)
			{
				if(From.Date == System.DateTime.MinValue || To.Date == System.DateTime.MinValue)
				{
					
					this.Page.MessageManager.Add (this.Page.MessageManager.FormatErrorMessage(Message.ERRMSG_SEARCH_PROVIDE_FROM_TO_1,"Date"));
					return false;
				}
			}
			return true;
		}
		protected virtual void AddJavaScript()
		{
			
		}
		protected string ReplaceValue(string Value)
		{
			return Value.Replace("'","''");
		}
		protected void ValidValueSearch(string Value)
		{
			if(rgxValidCharacter.IsMatch(Value))
			{
                this.Page.MessageManager.Add(this.Page.MessageManager.FormatErrorMessage(Message.ERRMSG_INVALID_CHARACTER_SEARCH_1,"%"));
				throw new ExceptionFulf(this.Page.MessageManager);
			}
			
			
		}
		protected void AddParameterValue(System.Web.UI.ControlCollection Controls,ParameterValueList List,string StartParameterName)
		{

			foreach(System.Web.UI.Control ctrl in Controls)
			{
				if(ctrl is QSP.WebControl.InternalTextBoxSearch || ctrl is QSP.WebControl.DropDownListSearch ||ctrl is QSPFulfillment.CommonWeb.UC.DateEntry || ctrl is QSP.WebControl.DropDownListProvince ||ctrl is QSP.WebControl.RadioButtonSearch)
				{
					QSP.WebControl.ISearch iSearch =(QSP.WebControl.ISearch)ctrl;

					
					if(iSearch.Value != String.Empty)
					{
						if(iSearch.Validation) 
						{
							ValidValueSearch(iSearch.Value);
						}
						List.Add(new QSPFulfillment.DataAccess.ParameterValue(StartParameterName+ iSearch.ParameterName,ReplaceValue(iSearch.Value)));
					}
				}
			}

			
		}
		protected void Insert(DataRow row,string Field,string Value)
		{
			if(Value != String.Empty)
			{
				row[Field] = Value;
			
				
			}
			else
			{
				row[Field] = System.DBNull.Value;
				
			}
		}
		protected void Insert(DataRow row,string Field,int Value)
		{
			if(Value > 0)
			{

				row[Field] = Value;
				
			}
			else
			{
				row[Field] = System.DBNull.Value;
			}
		}


	}
}
