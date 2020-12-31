namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for ControlerFM.
	/// </summary>
	public partial class ControlerFM : CustomerServiceControl
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
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

		public override void DataBind()
		{
			LoadData();
			SetValue();
			base.DataBind ();
		}
		private void LoadData()
		{
			try
			{
			
				DataSource = new DataTable();
				this.Page.BusAccount.SelectFieldManager(DataSource,this.Page.OrderInfo.CampaignID);
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		private void SetValue()
		{
			if(DataSource.Rows.Count != 0)
			{
				DataRow row = DataSource.Rows[0];
				this.lblFMID.Text= row["FMID"].ToString();
				this.LblLastName.Text = row["LastName"].ToString();
				this.lblFirstName.Text = row["FirstName"].ToString();
			}
		}

	}
}
