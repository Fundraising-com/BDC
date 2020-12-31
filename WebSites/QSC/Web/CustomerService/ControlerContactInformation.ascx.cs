namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;

	/// <summary>
	///		Summary description for ControlerContactInformation.
	/// </summary>
	public partial class ControlerContactInformation : CustomerServiceControl
	{

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
			if(DataSource.Rows.Count !=0)
				SetValue();
		}
		private void LoadData()
		{
			DataSource = new DataTable("ContactInfo");
			
			this.Page.BusShipmentOrder.SelectContactInformation(DataSource,this.Page.OrderInfo.OrderID);

			
		}
		private void SetValue()
		{
			DataRow row = DataSource.Rows[0];
			this.lblLastName.Text = row["LastName"].ToString();
			this.lblEmail.Text = row["email"].ToString();
			this.lblFaxeNumber.Text= row["fax"].ToString();
			this.lblFirstName.Text= row["firstname"].ToString();
			this.lblPhoneNumber.Text= row["phone"].ToString();
		}
	}
}
