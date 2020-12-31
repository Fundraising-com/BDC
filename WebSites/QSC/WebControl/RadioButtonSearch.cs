using System;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for RadioButtonSearch.
	/// </summary>
	public class RadioButtonSearch:RadioButton,ISearch
	{

		private string mParameterName ="";
		private string mContentType = "";
		private bool bValidation = true;

		public RadioButtonSearch()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		
		[Bindable(true),Category("SqlQuery"),DefaultValue("")]
		public string ParameterName
		{
			get
			{
				return this.mParameterName;
			}
			set
			{	
				
				this.mParameterName = value;	
			}
		}
		
		[Bindable(true),Category("SqlQuery"),DefaultValue("")]
		public string Value
		{
			get
			{
				if(mContentType == "int") 
				{
					return Convert.ToInt32(base.Checked).ToString();
				} 
				else 
				{
					return base.Checked.ToString();
				}
			}
		}
		
		[Bindable(true), Category("SqlQuery"), DefaultValue("")]
		public string ContentType
		{
			get 
			{
				return this.mContentType;
			}
			set  
			{
				this.mContentType = value;
			}
		}

		[Bindable(true), Category("SqlQuery"), DefaultValue(true)]
		public bool Validation
		{
			get
			{
				return this.bValidation;
			}
			set 
			{
				this.bValidation = value;
			}
		}
	}
}

