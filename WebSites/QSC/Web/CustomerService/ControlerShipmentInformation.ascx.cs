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
	///		Summary description for ControlerShipmentInformation.
	/// </summary>
	public partial class ControlerShipmentInformation : CustomerServiceControl
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
			DataSource = new DataTable("ShipInfo");
		
			this.Page.BusShipmentOrder.SelectShipmentInformation(DataSource,this.Page.OrderInfo.ShipmentID);

			
		}
		private void SetValue()
		{
			DataRow row = DataSource.Rows[0];
			this.lblShipmentID.Text = row["ShipmentID"].ToString();
			this.lblCarrierName.Text = row["carriername"].ToString();
			this.lblExpectedDelevryDate.Text = row["expecteddeliverydate"].ToString();
			this.lblNote.Text= row["note"].ToString();
			this.lblnumberboxes.Text= row["numberboxes"].ToString();
			this.lblNumberKids.Text= row["numberskids"].ToString();
			this.lblOperatorName.Text= row["OperatorName"].ToString();
			this.lblShipmentDate.Text= row["ShipmentDate"].ToString();
			this.lblwaybillno.Text= row["waybillno"].ToString();
			this.lblWeight.Text= row["Weight"].ToString();
			this.lblWeightUnitMeasure.Text= row["weightunitofmeasure"].ToString();
			
		}

	}
}
