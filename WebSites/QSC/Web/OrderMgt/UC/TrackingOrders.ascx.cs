namespace QSPFulfillment.OrderMgt
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	/// <summary>
	///		Summary description for TrackingOrders.
	/// </summary>
	public partial class TrackingOrders : System.Web.UI.UserControl
	{
		DataView dvOrdInFiles = new DataView();

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
			this.dgOrdersInFile.TemplateSelection += new DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventHandler(this.dgOrdersInFile_TemplateSelection);
			this.dgOrdersInFile.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgOrdersInFile_PageIndexChanged);
			this.DataBinding += new System.EventHandler(this.dgOrdersInFile_DataBinding);

		}
		#endregion

		protected void dgOrdersInFile_DataBinding(object sender, EventArgs e)
		{
			if(IsNested) 
			{
				DataGridItem dgi = (DataGridItem) BindingContainer;
									
				if(!(dgi.DataItem is DataSet))
					throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the HierarGrid declaration");

				DataSet ds = (DataSet) dgi.DataItem;
				
				DataSource = ds;
			}
			BindChildGrid();
		}

		private void BindChildGrid() 
		{
			dgOrdersInFile.DataSource = DataSource;
			dgOrdersInFile.DataMember = "TrackingOrders";
			dgOrdersInFile.DataBind();
		}
		private string SESSIONKEY_DATASOURCE
		{
			get { return this.UniqueID + "_DataSource"; }
		}

		public DataSet DataSource 
		{
			get 
			{
				return (DataSet) Session[SESSIONKEY_DATASOURCE];
			}
			set 
			{
				Session[SESSIONKEY_DATASOURCE] = value;
			}
		}
		public bool IsNested 
		{
			get 
			{
				bool isNested = true;

				if(ViewState["IsNested"] != null) 
				{
					isNested = Convert.ToBoolean(ViewState["IsNested"]);
				}

				return isNested;
			}
			set 
			{
				ViewState["IsNested"] = value;
			}
		}
		private void dgOrdersInFile_TemplateSelection(object sender, DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventArgs e)
		{
			e.TemplateFilename = "UC\\TrackingOrderDetail.ascx";
			
		}

		private void dgOrdersInFile_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgOrdersInFile.CurrentPageIndex = e.NewPageIndex;
			BindChildGrid();
		}
	}
}
