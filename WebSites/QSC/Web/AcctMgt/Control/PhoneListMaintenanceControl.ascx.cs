namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Business.Objects;
	using Common;
	using Common.TableDef;

	/// <summary>
	///		Summary description for AddressListMaintenanceControl.
	/// </summary>
	public partial class PhoneListMaintenanceControl : AcctMgtControl
	{
		private Phone ph;

		protected void PhoneListMaintenanceControl_Init(object sender, EventArgs e)
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			foreach(string ID in PhoneControlIDCollection) 
			{
				ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) LoadControl("PhoneMaintenanceControl.ascx");
				ctrlPhoneMaintenanceControl.ID = ID;
				this.plhPhoneList.Controls.Add(ctrlPhoneMaintenanceControl);
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
			AddNewPhone();
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

		public Phone oPhone 
		{
			get 
			{
				return ph;
			}
			set 
			{
				ph = value;
			}
		}

		public bool Required 
		{
			get
			{
				if(this.ViewState["Required"] == null)
					return true;

				return Convert.ToBoolean(this.ViewState["Required"]);
			}
			set 
			{
				this.ViewState["Required"] = value;
				SetRequired();
			}
		}

		public bool Enabled 
		{
			get 
			{
				if(this.ViewState["Enabled"] == null)
					return true;

				return Convert.ToBoolean(this.ViewState["Enabled"]);
			}
			set 
			{
				this.ViewState["Enabled"] = value;
				SetEnabled();
			}
		}

		public bool ShowOne 
		{
			get 
			{
				bool showOne = false;

				if(ViewState["ShowOne"] != null) 
				{
					showOne = Convert.ToBoolean(ViewState["ShowOne"]);
				}

				return showOne;
			}
			set 
			{
				ViewState["ShowOne"] = value;
				SetVisible();
			}
		}

		public bool ShowMainPhoneReadOnly 
		{
			get 
			{
				bool showMainPhoneReadOnly = false;

				if(ViewState["ShowMainPhoneReadOnly"] != null) 
				{
					showMainPhoneReadOnly = Convert.ToBoolean(ViewState["ShowMainPhoneReadOnly"]);
				}

				return showMainPhoneReadOnly;
			}
			set 
			{
				ViewState["ShowMainPhoneReadOnly"] = value;
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

		public void AddNewPhone() 
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) LoadControl("PhoneMaintenanceControl.ascx");
			this.plhPhoneList.Controls.Add(ctrlPhoneMaintenanceControl);
			ctrlPhoneMaintenanceControl.ID = "ctrlPhoneMaintenanceControl" + (this.plhPhoneList.Controls.Count + 1);
			ctrlPhoneMaintenanceControl.PhoneID = 0;
			ctrlPhoneMaintenanceControl.DeleteButtonVisible = !ShowOne;
			ctrlPhoneMaintenanceControl.Required = Required;
			ctrlPhoneMaintenanceControl.ShowMainPhoneReadOnly = ShowMainPhoneReadOnly;
			ctrlPhoneMaintenanceControl.DataBind();

			this.PhoneControlIDCollection.Add(ctrlPhoneMaintenanceControl.ID);
		}

		public void CreateControls() 
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			this.PhoneControlIDCollection.Clear();
			this.plhPhoneList.Controls.Clear();
			if(this.PhoneListID != 0) 
			{
				oPhone = new Phone(this.PhoneListID, this.Page.CurrentTransaction);

				if(!ShowOne) 
				{
					foreach(PhoneDataSet.PhoneRow row in oPhone.dataSet.Phone.Rows) 
					{
						ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) LoadControl("PhoneMaintenanceControl.ascx");
						this.plhPhoneList.Controls.Add(ctrlPhoneMaintenanceControl);
						ctrlPhoneMaintenanceControl.ID = "ctrlPhoneMaintenanceControl" + (this.plhPhoneList.Controls.Count + 1);
						ctrlPhoneMaintenanceControl.PhoneID = row.ID;
						ctrlPhoneMaintenanceControl.ShowMainPhoneReadOnly = ShowMainPhoneReadOnly;

						this.PhoneControlIDCollection.Add(ctrlPhoneMaintenanceControl.ID);
					}
				} 
				else if(oPhone.dataSet.Phone.Count >= 1)
				{
					ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) LoadControl("PhoneMaintenanceControl.ascx");
					this.plhPhoneList.Controls.Add(ctrlPhoneMaintenanceControl);
					ctrlPhoneMaintenanceControl.ID = "ctrlPhoneMaintenanceControl" + (this.plhPhoneList.Controls.Count + 1);
					ctrlPhoneMaintenanceControl.PhoneID = oPhone.dataSet.Phone[0].ID;
					ctrlPhoneMaintenanceControl.DeleteButtonVisible = !ShowOne;
					ctrlPhoneMaintenanceControl.ShowMainPhoneReadOnly = ShowMainPhoneReadOnly;

					this.PhoneControlIDCollection.Add(ctrlPhoneMaintenanceControl.ID);
				} 
				else 
				{
					AddNewPhone();
				}
			}

			SetRequired();
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
						ctrlPhoneMaintenanceControl.oPhone = oPhone;
						ctrlPhoneMaintenanceControl.DataBind();
					}
				}
			}
			else if(!IsPostBack && ShowOne) 
			{
				AddNewPhone();
			}
		}

		private void SetRequired() 
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			foreach(System.Web.UI.Control ctrl in plhPhoneList.Controls) 
			{
				ctrlPhoneMaintenanceControl = ctrl as PhoneMaintenanceControl;

				if(ctrlPhoneMaintenanceControl != null) 
				{
					ctrlPhoneMaintenanceControl.Required = this.Required;
				}
			}
		}

		private void SetEnabled() 
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			foreach(System.Web.UI.Control ctrl in this.plhPhoneList.Controls) 
			{
				ctrlPhoneMaintenanceControl = ctrl as PhoneMaintenanceControl;

				if(ctrlPhoneMaintenanceControl != null) 
				{
					ctrlPhoneMaintenanceControl.Enabled = this.Enabled;
				}
			}

			this.btnAddNew.Visible = this.Enabled;
		}

		private void SetVisible() 
		{
			this.btnAddNew.Visible = !this.ShowOne;
		}

		public void Save() 
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			if(this.PhoneListID != 0) 
			{
				oPhone = new Phone(this.PhoneListID, this.Page.CurrentTransaction);

				foreach(Control ctrl in this.plhPhoneList.Controls) 
				{
					if(ctrl is PhoneMaintenanceControl) 
					{
						ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) ctrl;

						ctrlPhoneMaintenanceControl.oPhone = oPhone;
						ctrlPhoneMaintenanceControl.PhoneListID = this.PhoneListID;

						if(!this.Visible) 
						{
							ctrlPhoneMaintenanceControl.Visible = false;
						}

						ctrlPhoneMaintenanceControl.Save();
					}
				}

				oPhone.Save();
			} 
			else 
			{
				SaveNew();
			}
		}

		public void SaveNew() 
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			oPhone = new Phone(this.Page.CurrentTransaction);

			foreach(Control ctrl in this.plhPhoneList.Controls) 
			{
				if(ctrl is PhoneMaintenanceControl) 
				{
					ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) ctrl;
					
					ctrlPhoneMaintenanceControl.oPhone = oPhone;
					ctrlPhoneMaintenanceControl.PhoneListID = this.PhoneListID;
					ctrlPhoneMaintenanceControl.PhoneID = 0;

					if(!this.Visible) 
					{
						ctrlPhoneMaintenanceControl.Visible = false;
					}

					ctrlPhoneMaintenanceControl.Save();
				}
			}

			if(oPhone.dataSet.Phone.Count > 0) 
			{
				oPhone.Save();

				this.PhoneListID = oPhone.dataSet.Phone[0].PhoneListID;
			}
		}

		public void UnbindIDs() 
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			this.PhoneListID = 0;

			foreach(Control ctrl in this.plhPhoneList.Controls) 
			{
				if(ctrl is PhoneMaintenanceControl) 
				{
					ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) ctrl;

					ctrlPhoneMaintenanceControl.PhoneListID = 0;
					ctrlPhoneMaintenanceControl.PhoneID = 0;
				}
			}
		}
	}
}
