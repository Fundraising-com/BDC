namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;
	using System.Web.Mail;
	using System.Text;

	/// <summary>
	///		Summary description for ControlerPhoneList.
	/// </summary>
	public class ControlerLead : QSPFulfillment.CustomerService.CustomerServiceControlDataGrid
	{
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected DataTable TableManager;
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		private void dtgMain_PreRender(object sender, EventArgs e)
		{
			this.dtgMain.ShowFooter = false;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			this.dtgMain.ItemDataBound +=new DataGridItemEventHandler(dtgMain_ItemDataBound);
			this.dtgMain.PreRender +=new EventHandler(dtgMain_PreRender);
			this.dtgMain.ItemCommand +=new DataGridCommandEventHandler(dtgMain_ItemCommand);
			base.OnInit(e,dtgMain,lblMessage);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected override void LoadData()
		{
			
			DataSource = new LeadTable();
			Page.BusLead.SelectAll(DataSource,QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID);
		}
		protected override void Insert(DataGridCommandEventArgs e)
		{
			DataRow row = DataSource.NewRow();
			GetValueInsert(e,row);
			DataSource.Rows.Add(row);
			this.Page.BusLead.Insert((LeadTable)DataSource);
			NewIDInserted = Convert.ToInt32(row[PhoneTable.FLD_ID]);
		}
		protected override void Update(DataGridCommandEventArgs e)
		{
			DataSource = new LeadTable();
			this.Page.BusLead.SelectOne(DataSource,GetInstance(e));
			
			if(DataSource.Rows.Count != 0)
			{
				GetValueUpdate(e,DataSource.Rows[0]);
				this.Page.BusLead.Update((LeadTable)DataSource);
			}
			
			
		}

		private void dtgMain_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			
			if(e.Item.ItemType == ListItemType.EditItem)
			{
				SetValueDropDownList((DropDownList)e.Item.FindControl("ddlFieldManager"));
				e.Item.DataBind();
			}
			
		}
		
		private void SetValueDropDownList(DropDownList ddl)
		{
			if(ddl.Items.Count == 0)
			{
				if(TableManager == null)
					LoadFieldManager();

				ddl.Items.Insert(0,new ListItem("Select",""));
				foreach(DataRow row in TableManager.Rows)
				{
					ddl.Items.Add(new ListItem(row["LastName"].ToString()+"," +row["FirstName"].ToString(),row["FMID"].ToString()));
				}
			}
		
			
			
		
		}

		private void LoadFieldManager()
		{
			try
			{
				TableManager = new DataTable();
				this.Page.BusAccount.SelectFieldManager(TableManager,0);
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}

		}
		protected int GetIndex(string Value)
		{
			if(TableManager == null)
				LoadFieldManager();

			int index =0;
			foreach(DataRow row in TableManager.Rows)
			{
				if(row["fmid"].ToString() == Value)
					return index+1;

				index++;

			}
			return 0;
		}
		private void GetValueUpdate(DataGridCommandEventArgs e,DataRow row)
		{
			Insert(row,LeadTable.FLD_FMID,GetFieldManagerID(e.Item));
			Insert(row,LeadTable.FLD_USERID,this.Page.UserID);
			

		}
		private void GetValueInsert(DataGridCommandEventArgs e,DataRow row)
		{
				
		}
		private int GetInstance(DataGridCommandEventArgs e)
		{
			return Convert.ToInt32(e.CommandArgument);
		}
		private string GetFieldManagerID(DataGridItem e)
		{
			return ((DropDownList)e.FindControl("ddlFieldManager")).SelectedItem.Value;
		}

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == "send")
			{
				DataSource = new LeadTable();
				this.Page.BusLead.SelectOne(DataSource,Convert.ToInt32(e.CommandArgument.ToString().Split(';')[1]));
				
				SendMail(e);
				try
				{
					UpdateDateSent();
					this.DataBind();
				}
				catch(ExceptionFulf ex)
				{
					this.Page.SetPageError(ex);
				}

			}
			else if(e.CommandName == "Edit")
			{
				this.dtgMain.Columns[this.dtgMain.Columns.Count-1].Visible = false;
			}
			else if(e.CommandName == "Cancel")
			{
				this.dtgMain.Columns[this.dtgMain.Columns.Count-1].Visible = true;
			}
			else if(e.CommandName == "Update")
			{
				this.dtgMain.Columns[this.dtgMain.Columns.Count-1].Visible = true;
			}
		}
		
		private void SendMail(DataGridCommandEventArgs e)
		{
			string fmid = e.CommandArgument.ToString().Split(';')[0];

			MailMessage Message = new MailMessage();

         Message.From = System.Configuration.ConfigurationSettings.AppSettings["LeadEmailFromAddress"];

			/*string dmid = GetEmailAddressDM(fmid);
			if (dmid.Length > 0)
				Message.From = dmid;
			else
				Message.From = "qsp_it@rd.com";*/
			
			Message.BodyFormat = MailFormat.Html;
			
			Message.To = GetEmailAddressFM(fmid);
			
			Message.Subject = "NEW LEAD";

			StringBuilder SB = new StringBuilder();
			CreateBody(e.Item,SB, fmid);
			Message.Body = SB.ToString();

			SmtpMail.SmtpServer = QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ErrorWebSmtp;
			try
			{
				System.Web.Mail.SmtpMail.Send(Message);
			}
			catch(Exception ex)
			{
				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);
			}
		}
		private void CreateBody(DataGridItem e,StringBuilder SB, string fmid)
		{
				
			SB.Append("ContactName: " + DataSource.Rows[0][LeadTable.FLD_CONTACTNAME].ToString()+"<br>");
			
			SB.Append("Contact Home Phone Number: "+DataSource.Rows[0][LeadTable.FLD_CONTACTHOMEPHONENUMBER].ToString()+"<br>");
			SB.Append("Contact Work Phone Number: "+DataSource.Rows[0][LeadTable.FLD_CONTACTWORKPHONENUMBER].ToString()+"<br>");
			SB.Append("Contact Fax Number: "+DataSource.Rows[0][LeadTable.FLD_CONTACTFAXNUMBER].ToString()+"<br>");
			SB.Append("Contact EMail: <a href=\"mailto:"+DataSource.Rows[0][LeadTable.FLD_CONTACTEMAIL].ToString()+"\">" + DataSource.Rows[0][LeadTable.FLD_CONTACTEMAIL].ToString() + "</a><br>");
			SB.Append("School Group: "+DataSource.Rows[0][LeadTable.FLD_SCHOOLGROUP].ToString()+"<br>");
			SB.Append("City Town: "+DataSource.Rows[0][LeadTable.FLD_CITYTOWN].ToString()+"<br>");
			SB.Append("Province: "+DataSource.Rows[0][LeadTable.FLD_PROVINCE].ToString()+"<br>");
			SB.Append("Interested In What?: "+DataSource.Rows[0][LeadTable.FLD_INTERESTEDINWHAT].ToString()+"<br>");
			SB.Append("Heard About QSP From Where?: "   +  DataSource.Rows[0][LeadTable.FLD_WHEREHEARABOUTQSP].ToString()+"<br>");
			SB.Append("Comments: "   +  DataSource.Rows[0][LeadTable.FLD_COMMENTS].ToString()+"<br><br>");
			//SB.Append("IF YOU NEED TO REPLY TO THIS EMAIL PLEASE USE THIS ADDRESS: <a href=\"mailto:" + GetEmailAddressDM(fmid) + "\">" + GetEmailAddressDM(fmid)+ "<br>");
		}
		private void UpdateDateSent()
		{
			
					
			if(DataSource.Rows.Count != 0)
			{
				Insert(DataSource.Rows[0],LeadTable.FLD_DATESENT,System.DateTime.Now.ToString());
				this.Page.BusLead.Update((LeadTable)DataSource);
			}
		}

		private string GetEmailAddressFM(string FMID)
		{
			try
			{
				if(TableManager == null) 
				{
					GetFMInformation();
				}

				DataRow[] row = TableManager.Select("FMID = '" + FMID + "'");
				
				if(row.Length != 0)
					return row[0]["Email"].ToString();
				
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
			return "";
		}

		private string GetEmailAddressDM(string FMID)
		{
			try
			{
				if(TableManager == null) 
				{
					GetFMInformation();
				}

				DataRow[] row = TableManager.Select("FMID = '" + FMID + "'");
				
				if(row.Length != 0) 
				{
					return GetEmailAddressFM(row[0]["DMID"].ToString());
				}
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
			return "";
		}

		private void GetFMInformation() 
		{
			TableManager = new DataTable();
			this.Page.BusAccount.SelectFieldManager(TableManager,0);
		}
	}
}
