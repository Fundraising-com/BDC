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
	/// <summary>
	/// Summary description for ProducePickList.
	/// </summary>
	public partial class ProducePickList :   QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.CheckBox cbSubReport1;
		protected System.Web.UI.WebControls.CheckBox cbSubReport2;
		protected System.Web.UI.WebControls.CheckBox cbSubReport3;
		protected System.Web.UI.WebControls.CheckBox cbSubReport4;
		protected System.Web.UI.WebControls.CheckBox cbSubReport5;
		protected System.Web.UI.WebControls.CheckBox cbSubReport6;
		protected System.Web.UI.WebControls.CheckBox cbSubReport7;
		protected System.Web.UI.WebControls.CheckBox cbSubReport8;
		protected System.Web.UI.WebControls.CheckBox cbSubReport9;
		protected System.Web.UI.WebControls.CheckBox cbSubReport10;
		protected System.Web.UI.WebControls.CheckBox cbSubReport11;
		protected System.Web.UI.WebControls.CheckBox cbSubReport12;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Server.ScriptTimeout = 180;

			if (!IsPostBack) 
			{
				PopulateDC();
				PopulateDG();
				//Button1.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to produce pick reports?')");
			}
			else
			{
				
			}
			
			
		}

		private void PopulateDC()
		{
			Business.DistributionCenter oDC = new Business.DistributionCenter();
			ddDC.DataSource = oDC.GetAllDistributionCenters();
			ddDC.DataBind();
		}

		private void PopulateDG()
		{
				
			DAL.PickListDataAccess oPickDA = new DAL.PickListDataAccess();

			DataGrid1.DataSource = oPickDA.GetUnpickedOrders(Convert.ToInt32(null));
			DataGrid1.DataBind();
	
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			CheckBox chkSelected;
			System.Web.UI.HtmlControls.HtmlInputHidden oHidden;
			Session["QSPF_OrderList"] = "";
			Int32 iCount = 0;

			foreach (DataGridItem dgItem in DataGrid1.Items)
			{
				chkSelected = (CheckBox)dgItem.FindControl("Checkbox1");
				if (chkSelected.Checked) 
				{
					iCount = iCount + 1;
					oHidden = (System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("HOrderId");
					if (Session["QSPF_OrderList"].ToString() == "") 
					{
						Session["QSPF_OrderList"] = oHidden.Value;
					}
					else
					{
						Session["QSPF_OrderList"] = Session["QSPF_OrderList"] + "," + oHidden.Value;
					}
					
				}

			}

			if (iCount == 0) 
			{
				// Throw error
			}
			else
			{
				Session["QSPF_DistributionCenterId"] = ddDC.SelectedValue;
				Response.Redirect("ProducePickListChoose.aspx", true);
			}

		}
	}
}
