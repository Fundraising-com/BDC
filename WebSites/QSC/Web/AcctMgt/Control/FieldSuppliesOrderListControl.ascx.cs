namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Business.Objects;
	using Common.TableDef;
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Summary description for CampaignListControl.
	/// </summary>
	public partial class FieldSuppliesOrderListControl : AcctMgtControl
	{

		private Business.Objects.FieldSuppliesOrderList oFSOrderList;

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

		public int CampaignID 
		{
			get 
			{
				int iCampaignID = 0;

				try 
				{
					iCampaignID = Convert.ToInt32(this.ViewState["CampaignID"]);
				} 
				catch { }

				return iCampaignID;
			}
			set 
			{
				this.ViewState["CampaignID"] = value;
			}
		}

		public override void DataBind()
		{
			if(this.CampaignID != 0) 
			{
				LoadData();

				this.dtgMain.DataSource = oFSOrderList.dataSet;
				this.dtgMain.DataMember = oFSOrderList.dataSet.FieldSuppliesOrderList.TableName;
				this.dtgMain.DataBind();

				this.dtgMain.Visible = true;
			} 
			else 
			{
				this.dtgMain.Visible = false;
			}
		}

		private void LoadData() 
		{
			oFSOrderList = new Business.Objects.FieldSuppliesOrderList(this.CampaignID, this.Page.CurrentTransaction);
		}
	}
}
