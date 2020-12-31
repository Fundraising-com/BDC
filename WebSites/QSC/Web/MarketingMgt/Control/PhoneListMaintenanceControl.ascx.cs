namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for AddressListMaintenanceControl.
	/// </summary>
	public partial class PhoneListMaintenanceControl : MarketingMgtControl
	{

		private DataTable PhoneTable = new DataTable("Phone");

		protected void PhoneListMaintenanceControl_Init(object sender, EventArgs e)
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			try 
			{
				foreach(string ID in PhoneControlIDCollection) 
				{
					ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) LoadControl("PhoneMaintenanceControl.ascx");
					ctrlPhoneMaintenanceControl.ID = ID;
					this.plhPhoneList.Controls.Add(ctrlPhoneMaintenanceControl);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

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
			this.Init += new System.EventHandler(this.PhoneListMaintenanceControl_Init);

		}
		#endregion

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			try 
			{
				ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) LoadControl("PhoneMaintenanceControl.ascx");
				this.plhPhoneList.Controls.Add(ctrlPhoneMaintenanceControl);
				ctrlPhoneMaintenanceControl.ID = "ctrlPhoneMaintenanceControl" + (this.plhPhoneList.Controls.Count + 1);
				ctrlPhoneMaintenanceControl.PhoneID = 0;
				ctrlPhoneMaintenanceControl.DataBind();

				this.PhoneControlIDCollection.Add(ctrlPhoneMaintenanceControl.ID);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public int PhoneListID 
		{
			get 
			{
				if(this.ViewState["PhoneListID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["PhoneListID"]);
			}
			set 
			{
				this.ViewState["PhoneListID"] = value;
			}
		}

		private ArrayList PhoneControlIDCollection 
		{
			get 
			{
				if(Session[this.ClientID + "PhoneControlIDCollection"] == null)
					Session[this.ClientID + "PhoneControlIDCollection"] = new ArrayList();

				return (ArrayList) Session[this.ClientID + "PhoneControlIDCollection"];
			}
		}

		public void CreateControls() 
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			this.PhoneControlIDCollection.Clear();
			this.plhPhoneList.Controls.Clear();
			if(this.PhoneListID != 0) 
			{
				this.Page.BusCatalog.SelectAllPhones(PhoneTable, this.PhoneListID);

				foreach(DataRow row in PhoneTable.Rows) 
				{
					ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) LoadControl("PhoneMaintenanceControl.ascx");
					this.plhPhoneList.Controls.Add(ctrlPhoneMaintenanceControl);
					ctrlPhoneMaintenanceControl.ID = "ctrlPhoneMaintenanceControl" + (this.plhPhoneList.Controls.Count + 1);
					ctrlPhoneMaintenanceControl.PhoneID = Convert.ToInt32(row["ID"]);

					this.PhoneControlIDCollection.Add(ctrlPhoneMaintenanceControl.ID);
				}
			}
		}

		public override void DataBind()
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			CreateControls();

			if(this.PhoneListID != 0) 
			{
				foreach(Control ctrl in this.plhPhoneList.Controls) 
				{
					if(ctrl is PhoneMaintenanceControl) 
					{
						ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) ctrl;
						ctrlPhoneMaintenanceControl.DataSource = this.PhoneTable;
						ctrlPhoneMaintenanceControl.DataBind();
					}
				}
			}
		}

		public void Save() 
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			foreach(Control ctrl in this.plhPhoneList.Controls) 
			{
				if(ctrl is PhoneMaintenanceControl) 
				{
					ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) ctrl;

					ctrlPhoneMaintenanceControl.PhoneListID = this.PhoneListID;
					ctrlPhoneMaintenanceControl.Save();
				}
			}
		}
	}
}
