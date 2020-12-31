namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSP.WebControl;
	using Business.Objects;
	using Common;

	public enum ContactSelectionType 
	{
		ShipTo,
		BillTo
	}

	public delegate void CopyContactEventHandler(object sender, CopyContactClickedArgs e);
	public delegate void SelectContactEventHandler(object sender, SelectContactClickedArgs e);

	/// <summary>
	///		Summary description for AccountListControl.
	/// </summary>
	public partial class ContactListControl : AcctMgtControl
	{

		public event CopyContactEventHandler CopyContactClicked;
		public event SelectContactEventHandler DeleteContactClicked;
		public event SelectContactEventHandler SelectContactClicked;

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
			this.dtgMain.PageIndexChanged += new DataGridPageChangedEventHandler(dtgMain_PageIndexChanged);
			this.dtgMain.ItemCommand += new DataGridCommandEventHandler(dtgMain_ItemCommand);
		}
		#endregion

		private void dtgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			if(e.NewPageIndex >= 0 && e.NewPageIndex < this.dtgMain.PageCount) 
			{
				this.dtgMain.CurrentPageIndex = e.NewPageIndex;
				DataBind();
			}
		}

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			Contact oContact;

			if(e.CommandName == "CopyContact") 
			{
				CopyContactClickedArgs args;
				ContactSelectionType oType;

				oContact = new Contact(this.Page.CurrentTransaction);

				oContact.GetOneByID(GetID(e.Item));

				oType = (ContactSelectionType) Enum.Parse(typeof(ContactSelectionType), e.CommandArgument.ToString(), true);

				args = new CopyContactClickedArgs(oContact, oType);

				if(CopyContactClicked != null)
					CopyContactClicked(source, args);
			} 
			else if(e.CommandName == "Delete") 
			{
				SelectContactClickedArgs args;

				oContact = new Contact(this.Page.CurrentTransaction);

				oContact.GetOneByID(GetID(e.Item));

				args = new SelectContactClickedArgs(oContact);

				if(DeleteContactClicked != null)
					DeleteContactClicked(source, args);
			}
			else if(e.CommandName == "Select") 
			{
				SelectContactClickedArgs args;

				oContact = new Contact(this.Page.CurrentTransaction);

				oContact.GetOneByID(GetID(e.Item));

				args = new SelectContactClickedArgs(oContact);

				if(SelectContactClicked != null)
					SelectContactClicked(source, args);
			}
		}

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

		public bool ShowCopyTo 
		{
			get 
			{
				return this.dtgMain.Columns[8].Visible;
			}
			set 
			{
				this.dtgMain.Columns[8].Visible = value;
				this.dtgMain.Columns[9].Visible = value;
			}
		}

		public bool ShowDelete
		{
			get 
			{
				return this.dtgMain.Columns[10].Visible;
			}
			set 
			{
				this.dtgMain.Columns[10].Visible = value;
			}
		}

		public bool ShowSelect 
		{
			get 
			{
				return this.dtgMain.Columns[11].Visible;
			}
			set 
			{
				this.dtgMain.Columns[11].Visible = value;
			}
		}

		public override void DataBind()
		{
			try 
			{
				if(this.AccountID != 0) 
				{
					LoadData();
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void LoadData() 
		{
			Contact oContact = new Contact(this.AccountID, this.Page.CurrentTransaction);

			this.dtgMain.DataSource = oContact.dataSet;
			this.dtgMain.DataMember = oContact.dataSet.Contact.TableName;
			this.dtgMain.DataBind();
		}

		private int GetID(DataGridItem e) 
		{
			int iID = 0;

			try 
			{
				iID = Convert.ToInt32(((Label) e.FindControl("lblID")).Text);
			} 
			catch { }

			return iID;
		}
	}
}