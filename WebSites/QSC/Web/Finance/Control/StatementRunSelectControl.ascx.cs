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
	public partial class StatementRunSelectControl : QSPUserControl, QSP.WebControl.ISearch
	{
        protected StatementRun statementRun;

		protected string mParameterName;
		protected string mContentType = "";
		protected bool bValidation = false;

        protected bool showOnlyCompletedStatementRuns = true;

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
            if (this.ddlStatementRun.Items.Count == 0)
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
            get { return this.ddlStatementRun.SelectedValue; }
		}

        [Bindable(true), Category("SqlQuery"), DefaultValue("")]
        public string Text
        {
            get { return this.ddlStatementRun.SelectedItem.Text;  }
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
            this.ddlStatementRun.DataTextField = statementRun.dataSet.StatementRun.StatementRunDateColumn.ColumnName;
            this.ddlStatementRun.DataValueField = statementRun.dataSet.StatementRun.StatementRunIDColumn.ColumnName;
		}

		protected void SetDataSource()
		{
            this.ddlStatementRun.DataSource = statementRun.dataSet;
            this.ddlStatementRun.DataMember = statementRun.dataSet.StatementRun.TableName;
		}

		protected void CreateControl()
		{
			statementRun = new StatementRun();
			LoadData();
			SetDataField();
			SetDataSource();
            this.ddlStatementRun.DataBind();
            this.ddlStatementRun.SelectedIndex = this.ddlStatementRun.Items.Count - 1;
		}

		protected void LoadData() 
		{
            if (showOnlyCompletedStatementRuns)
                statementRun.GetAllCompleted();
            else
                statementRun.GetAll();
		}

		public override void DataBind()
		{
			CreateControl();
		}

		/// <summary>
		/// gets current fiscal year
		/// </summary>
		/// <returns></returns>
		/*private string GetLatestStatementRun()
		{
            DateTime latestStatementRun = new DateTime();

            foreach (ListItem sr in this.ddlStatementRun.Items)
            {
                if
            }
			int currentYear = DateTime.Now.Year;

			if(DateTime.Now.Month >= 7)
			{
				return Convert.ToString(currentYear + 1);
			}
			else
			{
				return currentYear.ToString();
			}
		}*/
	}
}
