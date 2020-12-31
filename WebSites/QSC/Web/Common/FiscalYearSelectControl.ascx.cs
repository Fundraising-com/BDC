namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Business.Objects;
	using QSPFulfillment.CommonWeb;
	using System.ComponentModel;

	/// <summary>
	/// pre-bound select fiscal year control
	/// </summary>
	/// <remarks>
	/// Created: Saitakhmetova Madina, 2006-August-10
	/// </remarks>
	public partial class FiscalYearSelectControl : QSPUserControl, QSP.WebControl.ISearch
	{

		protected Season s;
		protected string mParameterName;
		protected string mContentType = "";
		protected bool bValidation = false;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			if(this.ddlFiscalYear.Items.Count == 0)
			{
				CreateControl();
			}
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

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
			get{return this.ddlFiscalYear.SelectedValue;}
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

        public string GetDDLClientId()
        {
            string result = "";

            result = this.ClientID;

            return result;
        }

		protected void SetDataField()
		{
			this.ddlFiscalYear.DataTextField = s.dataSet.Season.FiscalYearColumn.ColumnName;
			this.ddlFiscalYear.DataValueField = s.dataSet.Season.FiscalYearColumn.ColumnName;
		}

		protected void SetDataSource()
		{
			this.ddlFiscalYear.DataSource = s.dataSet;
			this.ddlFiscalYear.DataMember = s.dataSet.Season.TableName;
		}

		protected void CreateControl()
		{
			s = new Season();
			LoadData();
			SetDataField();
			SetDataSource();
			this.ddlFiscalYear.DataBind();
			this.ddlFiscalYear.SelectedIndex = this.ddlFiscalYear.Items.IndexOf(this.ddlFiscalYear.Items.FindByValue(GetCurrentFiscalYear()));
		}

		protected void LoadData() 
		{
			s.GetAllFiscalYears();
            //s.GetOneByDate
		}

		public override void DataBind()
		{
			CreateControl();
		}

		/// <summary>
		/// gets current fiscal year
		/// </summary>
		/// <returns></returns>
		private string GetCurrentFiscalYear()
		{
			int currentYear = DateTime.Now.Year;

			if(DateTime.Now.Month >= 7)
			{
				return Convert.ToString(currentYear + 1);
			}
			else
			{
				return currentYear.ToString();
			}
		}
	}
}
