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
	/// Summary description for PrintMagQueue.
	/// </summary>
	public partial class PrintMagQueue  :   QSPFulfillment.CommonWeb.QSPPage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Server.ScriptTimeout = 60;
			// Put user code to initialize the page here
			if (!IsPostBack) 
			{
				PopulateDG();						
			}
		}

		
		private void PopulateDG()
		{
				
			DAL.PickListDataAccess oPickDA = new DAL.PickListDataAccess();

			DataGrid1.DataSource = oPickDA.GetMagQueueOrders();
			DataGrid1.DataBind();
	
		}

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
				Response.Redirect("PrintMagQueueChoose.aspx", true);
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
