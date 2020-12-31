namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Business.Objects;
	using Common;
	using Common.TableDef;
	using QSPFulfillment.CommonWeb;
    using Business;

	/// <summary>
	///		Summary description for AccountGeneralInformationControl.
	/// </summary>
	public partial class AccountGeneralInformationControl : AcctMgtControl
	{
		protected AddressListMaintenanceControl ctrlAddressListMaintenanceControl;
		protected LastContactViewerControl ctrlLastContactViewerControl;
		private CAccount a;
        protected System.Web.UI.WebControls.TextBox tbxCountry;
        //protected System.Web.UI.WebControls.DropDownList ddlDistCenter;
		
		private CAccountClassCode acc = new CAccountClassCode();
		
        

		protected void Page_Load(object sender, System.EventArgs e)
		{
			AddJavaScript();
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
			this.ddlGroupClass.SelectedIndexChanged += new System.EventHandler(this.ddlGroupClass_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected void ddlGroupClass_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadDataDDLGroupCode();
		}

		#region Fields
		private string Name 
		{
			get 
			{
				return this.tbxName.Text;
			}
			set 
			{
				this.tbxName.Text = value;
			}
		}

		private string Language 
		{
			get 
			{
				return this.ddlLanguage.SelectedValue;
			}
			set 
			{
				this.ddlLanguage.SelectedIndex = this.ddlLanguage.Items.IndexOf(this.ddlLanguage.Items.FindByValue(value));
			}
		}

		private int Status 
		{
			get 
			{
				int iStatus = 0;

				try 
				{
					iStatus = Convert.ToInt32(this.ddlStatus.SelectedValue);
				} 
				catch { }

				return iStatus;
			}
			set 
			{
				this.ddlStatus.SelectedIndex = this.ddlStatus.Items.IndexOf(this.ddlStatus.Items.FindByValue(value.ToString()));
			}
		}

		private string GroupClass 
		{
			get 
			{
				return this.ddlGroupClass.SelectedValue;
			}
			set 
			{
				this.ddlGroupClass.SelectedIndex = this.ddlGroupClass.Items.IndexOf(this.ddlGroupClass.Items.FindByValue(value));
			}
		}

		private string GroupCode
		{
			get 
			{
				return this.ddlGroupCode.SelectedValue;
			}
			set 
			{
				this.ddlGroupCode.SelectedIndex = this.ddlGroupCode.Items.IndexOf(this.ddlGroupCode.Items.FindByValue(value));
			}
		}
		//
		/*private string FMID
		{
			get 
			{
				return this.ddlFM.SelectedValue;
			}
			set 
			{
				this.ddlFM.SelectedIndex = this.ddlFM.Items.IndexOf(this.ddlFM.Items.FindByValue(value));
			}
		}*/

		private bool PrivateOrganization 
		{
			get 
			{
				return Convert.ToBoolean(this.rblPrivateOrganization.SelectedValue);
			}
			set 
			{
				this.rblPrivateOrganization.SelectedIndex = this.rblPrivateOrganization.Items.IndexOf(this.rblPrivateOrganization.Items.FindByValue(value.ToString()));
			}
		}

		private int Enrollment 
		{
			get 
			{
				try 
				{
					return Convert.ToInt32(this.tbxEnrollment.Text);
				} 
				catch 
				{
					return 0;
				}
			}
			set 
			{
				this.tbxEnrollment.Text = value.ToString();
			}
		}

		private string Email 
		{
			get 
			{
				return this.tbxEmail.Text;
			}
			set  
			{
				this.tbxEmail.Text = value;
			}
		}
		private int ParentGroupID 
		{
			get 
			{
				try 
				{
					return Convert.ToInt32(this.tbxParentGroupID.Text);
				} 
				catch 
				{
					return 0;
				}
			}
			set 
			{
				if(value != 0) 
				{
					this.tbxParentGroupID.Text = value.ToString();
				} 
				else 
				{
					this.tbxParentGroupID.Text = "";
				}
			}
		}
		private string Comments 
		{
			get 
			{
				return this.tbxComments.Text;
			}
			set 
			{
				this.tbxComments.Text = value;
			}
		}
      private string ProfitChequePayee
        {
         get
         {
               return this.tbxProfitChequePayee.Text;
         }
         set
         {
               this.tbxProfitChequePayee.Text = value;
         }
      }
      private string Country 
		{
			get 
			{
				return this.tbxCountry.Text;
			}
			set 
			{
				this.tbxCountry.Text = value;
			}
		}

		/*private int Inventory
		{
			
			get 
			{
				int iInventory =0;
				try
				{
					iInventory= Convert.ToInt32(this.ddlDistCenter.SelectedValue);
				}
				catch { }
				return iInventory;
			}
			set 
			{
				this.ddlDistCenter.SelectedIndex = this.ddlDistCenter.Items.IndexOf(this.ddlDistCenter.Items.FindByValue(value.ToString()));
			}
		
		}*/

        private int BusinessUnitID
        {
            get
            {
                return 1;
            }
        }

		#endregion

		public int AccountID 
		{
			get 
			{
				if(this.ViewState["AccountID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["AccountID"]);
			}
			set 
			{
				this.ViewState["AccountID"] = value;
			}
		}

		public CAccount oCAccount 
		{
			get 
			{
				return a;
			}
		}

		protected override void AddJavaScript()
		{
			this.btnParentIDSearch.Attributes.Add("onclick", "OpenBig('AccountList.aspx?IsNewWindow=true&AccountIDSearch=' + document.getElementById('" + this.tbxParentGroupID.ClientID + "').value + '&ParentControlName=" + this.tbxParentGroupID.ClientID + "');");
				
			base.AddJavaScript ();
		}

		private void AddJavaScriptEditContacts() 
		{
			btnEditContacts.Attributes["onclick"] = "javascript: OpenCustom(\"AccountContactListMaintenance.aspx?IsNewWindow=true&AccountId=" + this.AccountID + "\", 890, 500);";
		}

		public override void DataBind()
		{
			try 
			{
				LoadData();

				this.ctrlLastContactViewerControl.DataBind();
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void LoadData() 
		{
			LoadDataDDL();


			if(this.AccountID != 0) 
			{
				a = new CAccount(AccountID, this.Page.CurrentTransaction);
				

				SetValue();
			}
			else 
			{
				SetValueEmpty();
			}

			SetEnabled();
		}

		private void SetValue() 
		{
			
			this.Name = a.dataSet.CAccount[0].Name;
			this.Language = a.dataSet.CAccount[0].Lang;
			this.Status = a.dataSet.CAccount[0].StatusID;
			this.GroupClass = a.dataSet.CAccount[0].CAccountCodeClass;
			LoadDataDDLGroupCode();
			//this.FMID=a.dataSet.CAccount[0].FMID;
			this.GroupCode = a.dataSet.CAccount[0].CAccountCodeGroup;
			this.PrivateOrganization = a.dataSet.CAccount[0].IsPrivateOrg;
			this.Enrollment = a.dataSet.CAccount[0].Enrollment;
			this.Email = a.dataSet.CAccount[0].EMail;
			this.ParentGroupID = a.dataSet.CAccount[0].ParentID;
			this.Comments = a.dataSet.CAccount[0].Comment;
			this.ProfitChequePayee = a.dataSet.CAccount[0].ProfitChequePayee;
			//this.Inventory= a.dataSet.CAccount[0].InventoryCode;
			//this.Country = a.dataSet.CAccount[0].Country;
			

			this.ctrlLastContactViewerControl.AccountID = this.AccountID;
		}

		private void SetValueEmpty() 
		{
			this.Name = "";
			this.Language = "";
			this.Status = Convert.ToInt32(CAccountStatus.Active);
			this.GroupClass = "";
			this.GroupCode = "";
			//this.FMID="";
			this.PrivateOrganization = false;
			this.tbxEnrollment.Text = "";
			this.Email = "";
			this.tbxParentGroupID.Text = "";
			this.Comments = "";
			this.ProfitChequePayee = "";
			//this.Country="";
			//this.Inventory=0;
			/*if(QSPPage.aUserProfile.IsFM) 
			{
				this.FMID = QSPPage.aUserProfile.FMID;
			} */
			//else 
			//{
			//	this.FMID = this.LastFMIDState;
			//}

			this.ctrlLastContactViewerControl.AccountID = 0;
		}

		public void SetEnabled() 
		{
			if(this.AccountID != 0) 
			{
				this.btnEditContacts.Disabled = false;
				AddJavaScriptEditContacts();
			} 
			else 
			{
				this.btnEditContacts.Disabled = true;
			}
		}

		private void LoadDataDDL() 
		{
			LoadDataDDLStatus();
			LoadDataDDLGroupClass();
			//LoadDataDDLFM(); 
			//LoadDataDDLDistCenter();
		}

		/*private void LoadDataDDLDistCenter() 
		{
			try 
			{
				if(this.ddlDistCenter.Items.Count == 0) 
				{
					DistributionCenter DistCenter = new DistributionCenter();
					DistCenter.GetAll();
					this.ddlDistCenter.DataSource = DistCenter.dataSet;
					this.ddlDistCenter.DataMember = DistCenter.dataSet.DistributionCenter.TableName;
					this.ddlDistCenter.DataTextField = DistCenter.dataSet.DistributionCenter.NameColumn.ColumnName;
					this.ddlDistCenter.DataValueField = DistCenter.dataSet.DistributionCenter.IdColumn.ColumnName;

					this.ddlDistCenter.DataBind();
					ddlDistCenter.Items.Insert(0, new ListItem("Please Select", String.Empty));
				}
			}

			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}*/
		/*private void LoadDataDDLFM() 
		{
			try 
			{
				if(this.ddlFM.Items.Count == 0) 
				{
					FieldManager fm = new FieldManager();
					//fm.GetAllByCountryCode("CA");
					fm.GetAll();
					this.ddlFM.DataSource = fm.dataSet;
					this.ddlFM.DataMember = fm.dataSet.FieldManager.TableName;
					this.ddlFM.DataTextField = fm.dataSet.FieldManager.ListNameColumn.ColumnName;
					this.ddlFM.DataValueField = fm.dataSet.FieldManager.FMIDColumn.ColumnName;

					this.ddlFM.DataBind();
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}*/

		private void LoadDataDDLStatus() 
		{
			try 
			{
				if(this.ddlStatus.Items.Count == 0) 
				{
					Business.Objects.CodeDetail cd = new Business.Objects.CodeDetail(CodeHeaderInstance.CAccountStatus);

					this.ddlStatus.DataSource = cd.dataSet;
					this.ddlStatus.DataMember = cd.dataSet.CodeDetail.TableName;
					this.ddlStatus.DataTextField = cd.dataSet.CodeDetail.DescriptionColumn.ColumnName;
					this.ddlStatus.DataValueField = cd.dataSet.CodeDetail.InstanceColumn.ColumnName;

					this.ddlStatus.DataBind();
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void LoadDataDDLGroupClass() 
		{
			try 
			{
				if(this.ddlGroupClass.Items.Count == 0) 
				{
					if(acc.dataSet.CAccountClass.Rows.Count == 0) 
					{
						acc.CurrentTransaction = this.Page.CurrentTransaction;
						acc.GetAll();
					}

					this.ddlGroupClass.DataSource = acc.dataSet;
					this.ddlGroupClass.DataMember = acc.dataSet.CAccountClass.TableName;
					this.ddlGroupClass.DataTextField = acc.dataSet.CAccountClass.NameColumn.ColumnName;
					this.ddlGroupClass.DataValueField = acc.dataSet.CAccountClass.AccountClassColumn.ColumnName;

               this.ddlGroupClass.DataBind();

               this.ddlGroupClass.Items.Insert(0, new ListItem("Please select...", "0"));
            }
         } 
			catch (MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void LoadDataDDLGroupCode() 
		{
			try 
			{
				if(acc.dataSet.CAccountClass.Rows.Count == 0) 
				{
					acc.CurrentTransaction = this.Page.CurrentTransaction;
					acc.GetAll();
				}

				acc.dataSet.CAccountClass.DefaultView.RowFilter = "AccountClass = '" + this.GroupClass + "'";
				if(acc.dataSet.CAccountClass.DefaultView.Count != 0) 
				{
					this.ddlGroupCode.DataSource = acc.dataSet.CAccountClass.DefaultView[0].Row.GetChildRows("CAccountClassCAccountCode");
					this.ddlGroupCode.DataTextField = acc.dataSet.CAccountCode.NameColumn.ColumnName;
					this.ddlGroupCode.DataValueField = acc.dataSet.CAccountCode.AccountCodeColumn.ColumnName;

					this.ddlGroupCode.DataBind();

				} 
				else 
				{
               this.ddlGroupCode.Items.Clear();
					this.ddlGroupCode.DataBind();
				}
			} 
			catch (MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}

		}

		public void Save() 
		{
			CAccountDataSet.CAccountRow row;

			if(this.AccountID != 0) 
			{
				a = new CAccount(AccountID, this.Page.CurrentTransaction);

				row = a.dataSet.CAccount[0];
				FillCAccountRow(row);

				//a.Save();
			} 
			else 
			{
				SaveNew();
			}
		}

		public void SaveNew() 
		{
			CAccountDataSet.CAccountRow row;

			a = new CAccount(this.Page.CurrentTransaction);

			row = a.dataSet.CAccount.NewCAccountRow();
			FillCAccountRow(row);

			a.dataSet.CAccount.AddCAccountRow(row);
			//a.Save();
		}

		private void FillCAccountRow(CAccountDataSet.CAccountRow row) 
		{
			row.Name = this.Name;
			row.Lang = this.Language;
			row.StatusID = this.Status;
			row.CAccountCodeClass = this.GroupClass;
			row.CAccountCodeGroup = this.GroupCode;
			//row.FMID=this.FMID;
			row.IsPrivateOrg = this.PrivateOrganization;
			row.Enrollment = this.Enrollment;
			row.EMail = this.Email;
			row.ParentID = this.ParentGroupID;
			row.Comment = this.Comments;
			row.ProfitChequePayee = this.ProfitChequePayee;
			row.UserIDModified = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.Instance;
			row.DateUpdated = DateTime.Now;
			//row.Country="CA";
			//row.InventoryCode=this.Inventory;
            row.BusinessUnitID = this.BusinessUnitID;
		}

		/*private void ddlFM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadDataDDLFM();
		}*/
	}
}
