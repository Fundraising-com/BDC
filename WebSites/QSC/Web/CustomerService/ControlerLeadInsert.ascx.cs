namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;

	/// <summary>
	///		Summary description for ContolerLeadInsert.
	/// </summary>
	public partial class ControlerLeadInsert : CustomerServiceControl
	{
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected LeadTable Table = new LeadTable();
		private const string MSG_INSERT_HEADER = "Please fill in the information and click save.";
		private const string MSG_INSERT_HEADER_SUCCESS = "<font color=blue>Information has been saved successfuly.</font><br><br>Please fill in the information and click save.";

		protected void Page_Load(object sender, System.EventArgs e)
		{
				if(!IsPostBack)
					this.lblMessage.Text = MSG_INSERT_HEADER;
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
				
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Instance !=0)
				{
					Update();

				}
				else
				{
					Insert();
					this.lblMessage.Text = MSG_INSERT_HEADER_SUCCESS;
					ClearValue();
				}
			}
			catch(ExceptionFulf ex)
			{
				this.lblMessage.Text = MSG_INSERT_HEADER;
				this.Page.SetPageError(ex);
			}
		}
		private void Update()
		{
			this.Page.BusLead.SelectOne(Table,Instance);
			
			if(Table.Rows.Count !=0)
			{
				DataRow row= Table.Rows[0];
		
				GetValueUpdate(row);


				this.Page.BusLead.Update(Table);
				Instance = Convert.ToInt32(Table.Rows[0][LeadTable.FLD_INSTANCE]);
			}
		}
		private void Insert()
		{
			DataRow row= Table.NewRow();
			
			GetValueInsert(row);
			Table.Rows.Add(row);

			this.Page.BusLead.Insert(Table);
			


			
		}

		private void GetValueUpdate(DataRow row)
		{
			Insert(row,LeadTable.FLD_CITYTOWN,this.tbxCity.Text);
			Insert(row,LeadTable.FLD_CONTACTEMAIL,this.tbxContactEMail.Text);
			Insert(row,LeadTable.FLD_CONTACTFAXNUMBER,this.tbxContactFaxNumber.Text);
			Insert(row,LeadTable.FLD_CONTACTHOMEPHONENUMBER,this.tbxContactHomePhoneNumber.Text);
			Insert(row,LeadTable.FLD_CONTACTNAME,this.tbxContactName.Text);
			Insert(row,LeadTable.FLD_CONTACTWORKPHONENUMBER,this.tbxContactWorkPhoneNumber.Text);
			//Insert(row,LeadTable.FLD_DATESENT,this.tbxStreet2.Text);
			Insert(row,LeadTable.FLD_FMID,"");
			Insert(row,LeadTable.FLD_INTERESTEDINWHAT,this.tbxIntersedinWhat.Text);
			Insert(row,LeadTable.FLD_PROVINCE,this.ddlProvince.SelectedItem.Value);
			Insert(row,LeadTable.FLD_SCHOOLGROUP,this.tbxSchoolGroup.Text);
			Insert(row,LeadTable.FLD_USERID,this.Page.UserID);
			Insert(row,LeadTable.FLD_WHEREHEARABOUTQSP,this.tbxWhereHearAboutQSP.Text);
			Insert(row,LeadTable.FLD_COMMENTS,this.tbxComments.Text);
		
		}
		private void GetValueInsert(DataRow row)
		{
			Insert(row,LeadTable.FLD_CITYTOWN,this.tbxCity.Text);
			Insert(row,LeadTable.FLD_CONTACTEMAIL,this.tbxContactEMail.Text);
			Insert(row,LeadTable.FLD_CONTACTFAXNUMBER,this.tbxContactFaxNumber.Text);
			Insert(row,LeadTable.FLD_CONTACTHOMEPHONENUMBER,this.tbxContactHomePhoneNumber.Text);
			Insert(row,LeadTable.FLD_CONTACTNAME,this.tbxContactName.Text);
			Insert(row,LeadTable.FLD_CONTACTWORKPHONENUMBER,this.tbxContactWorkPhoneNumber.Text);
			//Insert(row,LeadTable.FLD_DATESENT,this.tbxStreet2.Text);
			Insert(row,LeadTable.FLD_FMID,"");
			Insert(row,LeadTable.FLD_INTERESTEDINWHAT,this.tbxIntersedinWhat.Text);
			Insert(row,LeadTable.FLD_PROVINCE,this.ddlProvince.SelectedItem.Value);
			Insert(row,LeadTable.FLD_SCHOOLGROUP,this.tbxSchoolGroup.Text);
			Insert(row,LeadTable.FLD_USERID,this.Page.UserID);
			Insert(row,LeadTable.FLD_WHEREHEARABOUTQSP,this.tbxWhereHearAboutQSP.Text);
			Insert(row,LeadTable.FLD_COMMENTS,this.tbxComments.Text);
		
		}
		
		private int Instance
		{
			get
			{
				if(ViewState["Instance"] == null)
					return 0;

				return Convert.ToInt32(ViewState["Instance"]);
			}
			set
			{
				ViewState["Instance"] =value;
			}
		}
		private void ClearValue()
		{
			this.tbxCity.Text = "";
			this.tbxContactEMail.Text= "";
			this.tbxContactFaxNumber.Text= "";
			this.tbxContactHomePhoneNumber.Text= "";
			this.tbxContactName.Text= "";
			this.tbxContactWorkPhoneNumber.Text= "";
			this.ddlProvince.SelectedIndex = -1;
			this.tbxIntersedinWhat.Text= "";
			this.tbxPostalCode.Text="";
			this.ddlProvince.SelectedItem.Value= "";
			this.tbxSchoolGroup.Text= "";
			this.tbxWhereHearAboutQSP.Text= "";
			this.tbxComments.Text = "";
			this.Instance = 0;
		}
	}
}