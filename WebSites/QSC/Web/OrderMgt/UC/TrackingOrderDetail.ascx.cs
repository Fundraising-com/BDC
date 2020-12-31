namespace QSPFulfillment.OrderMgt
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for TrackingOrderDetail.
	/// </summary>
	public partial class TrackingOrderDetail : QSPFulfillment.CommonWeb.QSPUserControl
	{
		private const int IMAGE_DATE_COLUMN_ID = 1;
		private const int EDIT_DATE_COLUMN_ID  = 4;
		private const int VERIFY_DATE_COLUMN_ID= 3;

		
		
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
			this.DataBinding += new System.EventHandler(this.dgOrderDetail_DataBinding);

		}
		#endregion
		protected void dgOrderDetail_DataBinding(object sender, EventArgs e)
		{
			//if(IsNested) 
		{
			DataGridItem dgi = (DataGridItem) BindingContainer;
					
			if(!(dgi.DataItem is DataSet))
				throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the HierarGrid declaration");

			DataSet ds1 = (DataSet) dgi.DataItem;
			DataSource = ds1;
		}
			BindOrderDetailGrid();
		}

		private bool ImageColumnVisible 
		{
			get 
			{
				return this.dgOrderDetail.Columns[IMAGE_DATE_COLUMN_ID].Visible;
			}
			set 
			{
				this.dgOrderDetail.Columns[IMAGE_DATE_COLUMN_ID].Visible = value;
			}
		}
		private bool EditColumnVisible 
		{
			get 
			{
				return this.dgOrderDetail.Columns[EDIT_DATE_COLUMN_ID].Visible;
			}
			set 
			{
				this.dgOrderDetail.Columns[EDIT_DATE_COLUMN_ID].Visible = value;
			}
		}
		private bool VerifyColumnVisible 
		{
			get 
			{
				return this.dgOrderDetail.Columns[VERIFY_DATE_COLUMN_ID].Visible;
			}
			set 
			{
				this.dgOrderDetail.Columns[VERIFY_DATE_COLUMN_ID].Visible = value;
			}
		}

		private void BindOrderDetailGrid() 
		{
			dgOrderDetail.DataSource = DataSource;
			dgOrderDetail.DataMember = "OrderDetail";
			dgOrderDetail.DataBind();

			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999") 
			{
				ImageColumnVisible =false;
				EditColumnVisible  =false;
				VerifyColumnVisible=false;
			}
		
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
	}
}
