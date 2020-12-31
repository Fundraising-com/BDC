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

namespace QSPFulfillment.OrderMgt
{
	///<summary>Shipment Pick Lists</summary>
	public partial class ConfirmShipment : QSPFulfillment.CommonWeb.QSPPage
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{    

		}
		#endregion auto-generated code


		#region item declarations
		#endregion item declarations
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ArrayList AL = new ArrayList(5);
			AL.Add(5);
			AL.Add(2);
			AL.Add(1979);
			OrdersToShipDataGrid.DataSource = AL;
			OrdersToShipDataGrid.DataBind();
		}
	}
}
