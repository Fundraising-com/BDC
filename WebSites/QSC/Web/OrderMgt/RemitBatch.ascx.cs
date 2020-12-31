namespace QSPFulfillment.OrderMgt
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using DBauer.Web.UI.WebControls;

	/// <summary>
	///		Summary description for RemitBatch.
	/// </summary>
	public partial  class RemitBatch : System.Web.UI.UserControl
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.hidGrid.TemplateSelection += new DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventHandler(this.HG1_TemplateSelection);
			this.DataBinding += new System.EventHandler(this.List_DataBinding);

		}
		#endregion

		protected void List_DataBinding(object sender, System.EventArgs e)
		{
			DataGridItem dgi = (DataGridItem) this.BindingContainer;
			DataSet ds = (DataSet) dgi.DataItem;
			hidGrid.DataSource = ds;
			hidGrid.DataMember = "RemitBatch";
			hidGrid.DataBind();
		}

		private void HG1_TemplateSelection(object sender, HierarGridTemplateSelectionEventArgs e)
		{
			switch(e.Row.Table.TableName)
			{
				case "CustomerOrderDetailRemitHistory":
					e.TemplateFilename = "CustomerOrderDetailRemitHistory.ascx";
					break;
				default:
					throw new NotImplementedException("Unexpected child row in TemplateSelection event");
			}
		}

	}
}
