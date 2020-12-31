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

	/// <summary>
	///		Summary description for AddressMaintenanceControl.
	/// </summary>
	public partial class PhoneMaintenanceControl : AcctMgtControl
	{


		private Phone ph;
		private PhoneDataSet.PhoneRow row;

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

		}
		#endregion

		protected void btnRemove_Click(object sender, System.EventArgs e)
		{
			this.Visible = false;
		}

		public int PhoneID 
		{
			get 
			{
				if(this.ViewState["PhoneID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["PhoneID"]);
			}
			set 
			{
				this.ViewState["PhoneID"] = value;
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

		public bool DeleteButtonVisible 
		{
			get 
			{
				return this.trRemove.Visible;
			}
			set 
			{
				this.trRemove.Visible = value;
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

		#region Fields

		private int PhoneType 
		{
			get 
			{
				int iPhoneType = 0;

				try 
				{
					iPhoneType = Convert.ToInt32(this.ddlType.SelectedValue);
				} 
				catch { }

				return iPhoneType;
			}
			set 
			{
				this.ddlType.SelectedIndex = this.ddlType.Items.IndexOf(this.ddlType.Items.FindByValue(value.ToString()));
				this.lblType.Text = this.ddlType.SelectedItem.Text;
			}
		}

		private string PhoneNumber 
		{
			get 
			{
				return this.tbxPhoneNumber.Text;
			}
			set 
			{
				this.tbxPhoneNumber.Text = value;
			}
		}

		private string BestTimeToCall
		{
			get 
			{
				return this.tbxBestTimeToCall.Text;
			}
			set 
			{
				this.tbxBestTimeToCall.Text = value;
			}
		}

		#endregion

		public override void DataBind()
		{
			LoadData();
		}

		private void LoadData() 
		{
			LoadDataDDL();

			if(this.PhoneID != 0 && oPhone != null) 
			{
				row = oPhone.dataSet.Phone.FindByID(this.PhoneID);

				if(row != null) 
				{
					SetValue();
					SetVisible();
				} 
				else 
				{
					SetValueEmpty();
				}
			} 
			else 
			{
				SetValueEmpty();
			}
		}

		private void SetValue() 
		{
			try 
			{
				this.PhoneType = row.Type;
				this.PhoneNumber = row.PhoneNumber;
				this.BestTimeToCall = row.BestTimeToCall;
			} 
			catch (Exception ex) 
			{
				ApplicationError.ManageError(ex);
			}
		}

		private void SetValueEmpty() 
		{
			this.PhoneType = 0;
			this.PhoneNumber = "";
			this.BestTimeToCall = "";
		}

		private void LoadDataDDL() 
		{
			LoadDataDDLType();
		}

		private void LoadDataDDLType() 
		{
			if(this.ddlType.Items.Count == 0) 
			{
				try 
				{
					CodeDetail cd = new CodeDetail(CodeHeaderInstance.PhoneType);

					this.ddlType.DataSource = cd.dataSet;
					this.ddlType.DataMember = cd.dataSet.CodeDetail.TableName;
					this.ddlType.DataTextField = cd.dataSet.CodeDetail.DescriptionColumn.ColumnName;
					this.ddlType.DataValueField = cd.dataSet.CodeDetail.InstanceColumn.ColumnName;
					this.ddlType.DataBind();
				} 
				catch (MessageException ex) 
				{
					this.Page.SetPageError(ex);
				}
			}
		}

		private void SetVisible() 
		{
			bool bIsMainPhone;

			try 
			{
				bIsMainPhone = (ShowMainPhoneReadOnly && row.Type == 30505);

				this.ddlType.Visible = !bIsMainPhone;
				this.lblType.Visible = bIsMainPhone;
				this.btnRemove.Visible = !bIsMainPhone;
			} 
			catch (Exception ex) 
			{
				ApplicationError.ManageError(ex);
			}
		}

		private void SetRequired() 
		{
			this.ddlType.Required = this.Required;
			this.tbxPhoneNumber.Required = this.Required;
		}

		private void SetEnabled() 
		{
			this.ddlType.Enabled = this.Enabled;
			this.tbxPhoneNumber.ReadOnly = !this.Enabled;
			this.tbxBestTimeToCall.ReadOnly = !this.Enabled;
			this.btnRemove.Visible = this.Enabled;
		}

		public void Save() 
		{
			if(this.Visible) 
			{
				if(this.PhoneID != 0) 
				{
					row = oPhone.dataSet.Phone.FindByID(this.PhoneID);

					row.Type = this.PhoneType;
					row.PhoneNumber = this.PhoneNumber;
					row.BestTimeToCall = this.BestTimeToCall;
				} 
				else 
				{
					oPhone.dataSet.Phone.AddPhoneRow(this.PhoneType, this.PhoneListID, this.PhoneNumber, this.BestTimeToCall);
				}
			} 
			else 
			{
				if(this.PhoneID != 0) 
				{
					row = oPhone.dataSet.Phone.FindByID(this.PhoneID);

					if(row != null) 
					{
						row.Delete();
					}
				}
			}
		}
	}
}
