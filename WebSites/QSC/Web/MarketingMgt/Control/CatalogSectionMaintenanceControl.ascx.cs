namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class CatalogSectionMaintenanceControl : MarketingMgtControl
	{
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.HtmlControls.HtmlAnchor A1;

		public event SelectCatalogSectionEventHandler CatalogSectionSaved;
		public event System.EventHandler CatalogSectionCancelled;

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
			this.ddlSectionType.SelectedIndexChanged += new EventHandler(ddlSectionType_SelectedIndexChanged);
		}
		#endregion

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			SelectCatalogSectionClickedArgs args;
		
			try 
			{
				SaveCatalogSectionInformations();

				args = new SelectCatalogSectionClickedArgs(new CatalogSection(this.Page.CatalogSectionInfo.CatalogSectionID, Type, TypeDescription, Name, FSProgramID));
				
				if(CatalogSectionSaved != null)
					CatalogSectionSaved(this, args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}		
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(CatalogSectionCancelled != null)
					CatalogSectionCancelled(sender, e);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ddlSectionType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try 
			{
				if(Type == CatalogSectionType.FieldSupplies) 
				{
					SetValueDDLFSProgram();

					if(this.Page.CatalogSectionInfo != null) 
					{
						FSProgramID = this.Page.CatalogSectionInfo.FSProgramID;
					} 
					else 
					{
						FSProgramID = 0;
					}

					ShowFSProgram = true;
				} 
				else 
				{
					FSProgramID = 0;
					ShowFSProgram = false;
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected virtual bool ShowFSProgram 
		{
			get 
			{
				return this.trFSProgram.Visible;
			}
			set 
			{
				this.trFSProgram.Visible = value;
			}
		}

		#region Fields

		protected virtual string Name 
		{
			get 
			{
				return this.tbxSectionName.Text;
			}
			set 
			{
				this.tbxSectionName.Text = value;
			}
		}

		protected virtual CatalogSectionType Type 
		{
			get 
			{
				return (CatalogSectionType) this.ddlSectionType.Value;
			}
			set 
			{
				this.ddlSectionType.Value = Convert.ToInt32(value);
			}
		}

		protected virtual string TypeDescription 
		{
			get 
			{
				return this.ddlSectionType.SelectedItem.Text;
			}
			set 
			{
				this.ddlSectionType.SelectedIndex = this.ddlSectionType.Items.IndexOf(this.ddlSectionType.Items.FindByText(value));
			}
		}

		protected virtual int FSProgramID 
		{
			get 
			{
				int fsProgramID = 0;

				if(this.ddlFSProgram.Items.Count > 0) 
				{
					return this.ddlFSProgram.Value;
				}

				return fsProgramID;
			}
			set 
			{
				if(this.ddlFSProgram.Items.Count > 0) 
				{
					this.ddlFSProgram.Value = value;
				}
			}
		}

		#endregion

		public override void DataBind()
		{
			SetValueDDL();

			if(this.Page.CatalogSectionInfo != null) 
			{
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}
		}

		private void SetValue() 
		{
			Name = this.Page.CatalogSectionInfo.Name;
			Type = this.Page.CatalogSectionInfo.Type;

			if(Type == CatalogSectionType.FieldSupplies) 
			{
				FSProgramID = this.Page.CatalogSectionInfo.FSProgramID;
				ShowFSProgram = true;
			} 
			else 
			{
				FSProgramID = 0;
				ShowFSProgram = false;
			}
		}

		private void SetValueEmpty() 
		{
			Name = String.Empty;
			Type = CatalogSectionType.None;
			FSProgramID = 0;
			ShowFSProgram = false;
		}

		private void SetValueDDL() 
		{
			SetValueDDLSectionType();

			if(this.Page.CatalogSectionInfo != null && this.Page.CatalogSectionInfo.Type == CatalogSectionType.FieldSupplies) 
			{
				SetValueDDLFSProgram();
			}
		}

		private void SetValueDDLSectionType() 
		{
			if(this.ddlSectionType.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusCatalogSection.SelectAllCatalogSectionTypes(table);

				this.ddlSectionType.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));
						
				foreach(DataRow row in table.Rows)
				{
					this.ddlSectionType.Items.Add(new ListItem(row["Description"].ToString(), row["ID"].ToString()));
				}	
			}
		}

		private void SetValueDDLFSProgram() 
		{
			DataTable table;

			if(this.ddlFSProgram.Items.Count == 0) 
			{
				table = new DataTable();
				this.Page.BusProgram.SelectAll(table);

				this.ddlFSProgram.DataSource = table;
				this.ddlFSProgram.DataTextField = "Name";
				this.ddlFSProgram.DataValueField = "ID";
				this.ddlFSProgram.DataBind();
			}
		}

		private void SaveCatalogSectionInformations() 
		{
			if(this.Page.CatalogSectionInfo != null && this.Page.CatalogSectionInfo.CatalogSectionID != 0) 
			{
				this.Page.BusCatalogSection.Update(this.Page.CatalogSectionInfo.CatalogSectionID, this.Page.CatalogInfo.CatalogCode, Type, Name, FSProgramID, this.Page.UserID);
			} 
			else 
			{
				this.Page.CatalogSectionInfo = new CatalogSection();
				this.Page.CatalogSectionInfo.CatalogSectionID = this.Page.BusCatalogSection.Insert(this.Page.CatalogInfo.CatalogID, this.Page.CatalogInfo.CatalogCode, Type, Name, FSProgramID, this.Page.UserID);
			}

			this.Page.CatalogSectionInfo.Name = Name;
			this.Page.CatalogSectionInfo.Type = Type;
			this.Page.CatalogSectionInfo.TypeDescription = TypeDescription;
			this.Page.CatalogSectionInfo.FSProgramID = FSProgramID;
		}
	}
}
