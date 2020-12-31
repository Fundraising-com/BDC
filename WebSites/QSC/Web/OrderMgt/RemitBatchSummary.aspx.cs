using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business;
using QSPFulfillment.CommonWeb.UC;

namespace QSPFulfillment.OrderMgt
{
	///<summary>Summary description for RemitBatchSummary</summary>
	public partial class RemitBatchSummary : QSPFulfillment.CommonWeb.QSPPage
	{
		//protected skmMenu.Menu Menu1;
		protected CodeDetailDropDown aDropDown;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			aDropDown.CodeHeader=(int)CodeHeader.CampaignStatus;
		}

		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			aDropDown = (CodeDetailDropDown) FindControl("Code");
			aDropDown.CodeHeader = (int)CodeHeader.MagazineStatus;
			aDropDown.LoadList();
		}

		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{

		}
		#endregion auto-generated code

		private void ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{

		}
	}
}
