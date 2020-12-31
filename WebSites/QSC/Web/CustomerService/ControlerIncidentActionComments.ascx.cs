namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for ControlerIncidentActionComments.
	/// </summary>
	public partial class ControlerIncidentActionComments : CustomerServiceControl
    {
		protected IncidentActionTable Table;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadData();
				if(Table.Rows.Count !=0)
				{
					this.tbxComments.Text = Table.Rows[0][IncidentActionTable.FLD_COMMENTS].ToString();
				}
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
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

		private void LoadData()
		{
			try
			{
				Table = new IncidentActionTable();
				if(GetInstance() !=0)
				{
					this.Page.BusIncidentAction.SelectOne(Table,GetInstance());
				
				}
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
			
		}

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				LoadData();
				Update();
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}

		}
		private int GetInstance()
		{
			if(Context.Request.QueryString["Instance"]!= null)
				return Convert.ToInt32(Context.Request.QueryString["Instance"]);
			
			return 0;
		}
		private void GetValue(DataRow Row)
		{
			Row[IncidentActionTable.FLD_COMMENTS] = this.tbxComments.Text;

		}
		private void Update()
		{
			
				if(Table.Rows.Count !=0)
				{
					GetValue(Table.Rows[0]);
					this.Page.BusIncidentAction.Update(Table);
					AddScriptReloadClose();
				}
			
		
		}
		private void AddScriptClose()
		{
			
			if(!this.Page.IsStartupScriptRegistered("CloseEditComments"))
			{
				this.Page.RegisterStartupScript("CloseEditComments","<script language=\"javascript\">self.close(); </script>");
			}
			
			
		}
		protected void AddScriptReloadClose()
		{
			
			if(!this.Page.IsStartupScriptRegistered("ConfirmCloseReload"))
			{
				this.Page.RegisterStartupScript("ConfirmCloseReload","<script language=\"javascript\"> window.opener.pleasewait(); window.opener.Refresh(); self.close(); </script>");
			}
			
			
		}
		protected override void AddJavaScript()
		 {
			this.btnCancel.Attributes.Add("onclick","javascript:self.close();");
		 }

	}
}
