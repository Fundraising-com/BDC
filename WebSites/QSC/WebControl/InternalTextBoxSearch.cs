using System;
using System.Web.UI.WebControls;
using System.ComponentModel;


namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for InternalTextBoxSearch.
	/// </summary>
	public abstract class InternalTextBoxSearch: System.Web.UI.WebControls.TextBox,ISearch
	{
		private string mParameterName ="";
		private string mContentType = "";
		private bool bValidation = true;
	
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			
					
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
			get{return base.Text;}
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

