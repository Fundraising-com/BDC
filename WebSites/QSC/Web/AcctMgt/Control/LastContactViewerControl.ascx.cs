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
	///		Summary description for LastContactViewerControl.
	/// </summary>
	public partial class LastContactViewerControl : AcctMgtControl
	{

		private Contact contact;

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

		public int ContactID 
		{
			get 
			{
				if(this.ViewState["ContactID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["ContactID"]);
			}
			set 
			{
				this.ViewState["ContactID"] = value;
			}
		}

		public Contact oContact 
		{
			get 
			{
				return contact;
			}
		}

		public override void DataBind() 
		{
			LoadData();

			if((this.AccountID != 0 || this.ContactID != 0) && oContact.dataSet.Contact.Count > 0) 
			{
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}

			SetVisible();
		}

		private void LoadData() 
		{
			contact = new Contact(this.Page.CurrentTransaction);

			if(this.AccountID != 0) 
			{
				oContact.GetLastByAccountID(this.AccountID);
			} 
			else if(this.ContactID != 0) 
			{
				oContact.GetOneByID(this.ContactID);
			}
		}

		private void SetValue() 
		{
			ContactDataSet.ContactRow row = oContact.dataSet.Contact[0];

			this.lblCompleteName.Text = row.Title + " " + row.FirstName + " " + ((row.MiddleInitial != "") ? (row.MiddleInitial + " ") : "") + row.LastName;
			this.lblFunction.Text = row.Function;
		}

		private void SetValueEmpty() 
		{
			this.lblCompleteName.Text = "";
			this.lblFunction.Text = "";
		}

		private void SetVisible() 
		{
			this.Visible = ((this.AccountID != 0 || this.ContactID != 0) && oContact.dataSet.Contact.Count > 0);
		}
	}
}
