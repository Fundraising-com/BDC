namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Common;
	using Common.TableDef;
	using Business.Objects;

	/// <summary>
	///		Summary description for FieldSuppliesMaintenanceControl.
	/// </summary>
	public partial class FieldSuppliesMaintenanceControl : AcctMgtControl
	{
      private const int SHIP_TO_SUPPLIES_ID_SCHOOL = 63002;
      private const int SHIP_TO_SUPPLIES_ID_OTHER = 63003;
		private const string EDIT_FS_ORDER_URL = "FieldSuppliesOrderList.aspx?IsNewWindow=true&CampaignID=";


		private Campaign campaign;
		private Contact oContact;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDeliveryDate;
		protected QSPFulfillment.AcctMgt.Control.ContactMaintenanceControl ctrlShipToFSContactMaintenanceControl;

		private CampaignDataSet.CampaignRow row;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			 Ajax.Utility.RegisterTypeForAjax(typeof(FieldSuppliesMaintenanceControl));
			if(hidDataBind.Value=="1")
			{
				hidDataBind.Value="0";
				DataBind();
			}
			
		}
		[Ajax.AjaxMethod()]
		public string GenerateFieldSupplyForCampaign(int nCampaignID)
		{			
			//string x = new string("Field Supply generation started");
			try
			{
				AccountCampaignList acList = new AccountCampaignList();

				acList.GenerateFieldSupplies(0,nCampaignID, "", "", "","");
				
			}
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
				
			}
			return "FS Generation completed";
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

		protected void ddlShipSuppliesTo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetVisibleShipToFSContact();
		}

		public Campaign oCampaign 
		{
			get 
			{
				return this.campaign;
			}
			set 
			{
				this.campaign = value;
				//set the hidden one as well - used during GenerateFS
				//hidCampaignID.Value=this.oCampaign.dataSet.Campaign[0].ID.ToString();
			}
		}

		public bool EditMode 
		{
			get 
			{
				if(this.ViewState["EditMode"] == null)
					return true;

				return Convert.ToBoolean(this.ViewState["EditMode"]);
			}
			set 
			{
				this.ViewState["EditMode"] = value;
			}
		}

		#region Fields

		public int CampaignID 
		{
			get 
			{
				if(this.ViewState["CampaignID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["CampaignID"]);
			}
			set 
			{
				this.ViewState["CampaignID"] = value;
			}
		}


		public bool FieldSuppliesRequired 
		{
			get 
			{
				return this.chkRequired.Checked;
			}
			set 
			{
				this.chkRequired.Checked = value;
			}
		}

		private bool FieldSuppliesExtraRequired 
		{
			get 
			{
				return this.chkExtraRequired.Checked;
			}
			set 
			{
				this.chkExtraRequired.Checked = value;
			}
		}

		public DateTime DeliveryDate 
		{
			get 
			{
				DateTime dtDeliveryDate = new DateTime(1995, 1, 1);

				if(!this.EditMode) 
				{
					try 
					{
						dtDeliveryDate = Convert.ToDateTime(this.lblDeliveryDate.Text);
					} 
					catch { }
				}
				else 
				{
					if(dteDeliveryDate.Date != DateTime.MinValue) 
					{
						dtDeliveryDate = this.dteDeliveryDate.Date;
					}
				}

				return dtDeliveryDate;
			}
			set 
			{
				if(!this.EditMode) 
				{
					this.lblDeliveryDate.Text = value.ToString("MM-dd-yyyy");
				} 
				else 
				{
					if(value != new DateTime(1995, 1, 1)) 
					{
						this.dteDeliveryDate.Date = value;
					} 
					else 
					{
						this.dteDeliveryDate.ClearDate();
					}
				}
			}
		}

		private int ShipSuppliesTo
		{
			get 
			{
				int iShipSuppliesTo = 0;

				if(!this.EditMode) 
				{
					iShipSuppliesTo = Convert.ToInt32(this.ddlShipSuppliesTo.Items.FindByText(this.lblShipSuppliesTo.Text).Value);
				} 
				else 
				{
					iShipSuppliesTo = Convert.ToInt32(this.ddlShipSuppliesTo.SelectedValue);
				}

				return iShipSuppliesTo;
			}
			set 
			{
				if(!this.EditMode) 
				{
					try 
					{
						this.lblShipSuppliesTo.Text = this.ddlShipSuppliesTo.Items.FindByValue(value.ToString()).Text;
					} 
					catch { }
				} 
				else 
				{
					this.ddlShipSuppliesTo.SelectedIndex = this.ddlShipSuppliesTo.Items.IndexOf(this.ddlShipSuppliesTo.Items.FindByValue(value.ToString()));
				}
			}
		}

		#endregion

		#region JavaScript

		private void AddJavaScriptEditOrder() 
		{
			if(row != null) 
			{
				this.btnEditFSOrder.Attributes["onclick"] = "OpenBig(\"" + EDIT_FS_ORDER_URL + row.ID.ToString() + "\");";
			}
		}

		#endregion

		public override void DataBind()
		{
			LoadData();

			if(this.oCampaign != null) 
			{
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}

			SetVisible();
			SetEnabled();
		}

		private void LoadData() 
		{
			LoadDataDDL();

			if(this.oCampaign != null) 
			{
				row = this.oCampaign.dataSet.Campaign[0];
			}

			DataBindShipToFSContact();
		}

		private void DataBindShipToFSContact() 
		{
			if(oCampaign != null && row != null && row.SuppliesShipToCampaignContactID == SHIP_TO_SUPPLIES_ID_OTHER && row.SuppliesCampaignContactID != 0) 
			{
				LoadDataShipToFSContact();
				SetValueShipToFSContact();
			} 
			else 
			{
				SetValueEmptyShipToFSContact();
			}

			this.ctrlShipToFSContactMaintenanceControl.DataBind();
		}

		private void LoadDataShipToFSContact() 
		{
			this.oContact = new Contact(this.Page.CurrentTransaction);
			this.oContact.GetOneByID(row.SuppliesCampaignContactID);
		}

		private void SetValueShipToFSContact() 
		{
			this.ctrlShipToFSContactMaintenanceControl.AccountID = 0;
			this.ctrlShipToFSContactMaintenanceControl.ContactID = row.SuppliesCampaignContactID;
			this.ctrlShipToFSContactMaintenanceControl.oContact = this.oContact;
		}

		private void SetValueEmptyShipToFSContact() 
		{
			this.ctrlShipToFSContactMaintenanceControl.AccountID = 0;
			this.ctrlShipToFSContactMaintenanceControl.ContactID = 0;
			this.ctrlShipToFSContactMaintenanceControl.oContact = null;
		}

		private void LoadDataDDL() 
		{
			LoadDataDDLShipSuppliesTo();
		}

		private void LoadDataDDLShipSuppliesTo() 
		{
			if(this.ddlShipSuppliesTo.Items.Count == 0) 
			{
				CodeDetail cd = new CodeDetail(CodeHeaderInstance.ShipSuppliesTo);

				this.ddlShipSuppliesTo.DataSource = cd.dataSet;
				this.ddlShipSuppliesTo.DataMember = cd.dataSet.CodeDetail.TableName;
				this.ddlShipSuppliesTo.DataTextField = cd.dataSet.CodeDetail.DescriptionColumn.ColumnName;
				this.ddlShipSuppliesTo.DataValueField = cd.dataSet.CodeDetail.InstanceColumn.ColumnName;

            this.ddlShipSuppliesTo.DataBind();
			}
		}

		private void SetValue() 
		{
			this.EditMode = !row.FSOrderRecCreated;

			this.FieldSuppliesRequired = row.FSRequired;
			this.FieldSuppliesExtraRequired = row.FSExtraRequired;
			this.DeliveryDate = row.SuppliesDeliveryDate;
			this.ShipSuppliesTo = row.SuppliesShipToCampaignContactID;

			AddJavaScriptEditOrder();
		}

		private void SetValueEmpty() 
		{
			this.EditMode = true;

			this.FieldSuppliesRequired = false;
			this.FieldSuppliesExtraRequired = false;
			this.DeliveryDate = new DateTime(1995, 1, 1);
			this.ShipSuppliesTo = SHIP_TO_SUPPLIES_ID_SCHOOL;

			this.btnEditFSOrder.Attributes.Remove("onclick");
		}

		private void SetVisible()
		{

			this.lblFieldSuppliesGenerated.Visible = !this.EditMode;
			this.lblFieldSuppliesInstructions.Visible = this.EditMode;
			this.lblDeliveryDate.Visible = !this.EditMode;
			this.dteDeliveryDate.Visible = this.EditMode;
			this.lblShipSuppliesTo.Visible = !this.EditMode;
			this.ddlShipSuppliesTo.Visible = this.EditMode;
			this.btnEditFSOrder.Visible = !this.EditMode;

			if(this.lblFieldSuppliesGenerated.Visible == true || QSPFulfillment.CommonWeb.QSPPage.aUserProfile.HasRole("HomeOffice") != true)
				tdGenerateFS.Visible= false;

			SetVisibleShipToFSContact();
		}

		private void SetEnabled() 
		{
			this.chkRequired.Enabled = this.EditMode;
			this.chkExtraRequired.Enabled = this.EditMode;
			this.ctrlShipToFSContactMaintenanceControl.Enabled = this.EditMode;
		}

		private void SetVisibleShipToFSContact() 
		{
			if(this.EditMode) 
			{
				this.ctrlShipToFSContactMaintenanceControl.Visible = (this.ddlShipSuppliesTo.SelectedValue == SHIP_TO_SUPPLIES_ID_OTHER.ToString());
			} 
			else 
			{
				this.ctrlShipToFSContactMaintenanceControl.Visible = (row.SuppliesShipToCampaignContactID == SHIP_TO_SUPPLIES_ID_OTHER);
			}
		}

		public void Save() 
		{
			if(this.EditMode) 
			{
				if(this.oCampaign != null) 
				{
					row = this.oCampaign.dataSet.Campaign[0];

					row.FSRequired = this.FieldSuppliesRequired;
					row.FSExtraRequired = this.FieldSuppliesExtraRequired;
					row.SuppliesDeliveryDate = this.DeliveryDate;

					if(this.ddlShipSuppliesTo.SelectedValue == SHIP_TO_SUPPLIES_ID_OTHER.ToString() || (row.SuppliesShipToCampaignContactID == SHIP_TO_SUPPLIES_ID_OTHER && row.SuppliesCampaignContactID != 0)) 
					{
						LoadDataShipToFSContact();
						SetValueShipToFSContact();
						this.ctrlShipToFSContactMaintenanceControl.Save();

						oContact.Save();

						if(oContact.dataSet.Contact.Count == 1) 
						{
							row.SuppliesCampaignContactID = oContact.dataSet.Contact[0].Id;
						} 
						else 
						{
							row.SetSuppliesCampaignContactIDNull();
						}
					}

					row.SuppliesShipToCampaignContactID = this.ShipSuppliesTo;
				}
			}
		}

		
	}
}
