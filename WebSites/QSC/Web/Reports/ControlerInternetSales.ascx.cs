namespace QSPFulfillment.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Text;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.CommonWeb;
	/// <summary>
	///		Summary description for GenerateSwitchLetter.
	/// </summary>
	public class ControlerInternetSales : QSPFulfillment.CustomerService.CustomerServiceControl
	{
		private const int REPORT_TIMEOUT = 180000;

		private System.Text.StringBuilder sb = new System.Text.StringBuilder();
		protected System.Web.UI.WebControls.Button btnPreview;
		
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryTo;
		protected System.Web.UI.WebControls.Label lblDateFrom;
		protected System.Web.UI.WebControls.Label lblDateTo;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label Label1;
		protected QSP.WebControl.DropDownListSearch ddlFieldManager;
		protected System.Web.UI.WebControls.Label lblFieldManager;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationInternetSales;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryFrom;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack) 
			{
				this.SetValueDropDownList();
				
				if(QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999") 
				{
					SetValueFieldManager();
				}
			}
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
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			ParameterValueCollection parameterValues;

			if(Validate())
			{
				if(QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999") 
				{
					SetValueFieldManager();
				}

				parameterValues = GetParameterValueCollection();
				rsGenerationInternetSales.Generate(parameterValues, REPORT_TIMEOUT);
			}
			else
			{
				this.Page.MessageManager.PrepareErrorMessage();
				this.Page.SetPageError();
			}
		}

		private void SetValueFieldManager() 
		{
			this.ddlFieldManager.SelectedIndex = this.ddlFieldManager.Items.IndexOf(this.ddlFieldManager.Items.FindByValue(QSPPage.aUserProfile.FMID));
			this.lblFieldManager.Text = this.ddlFieldManager.SelectedItem.Text;

			this.ddlFieldManager.Visible = false;
			this.lblFieldManager.Visible = true;
		}

		private void SetValueDropDownList()
		{
			try
			{
				this.SetValueDDLFieldManager();
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}

		private void SetValueDDLFieldManager() 
		{
			DataTable dtbFieldManager = new DataTable();
			ListItem sel = new ListItem("", "");

			sel.Selected = true;
			this.ddlFieldManager.Items.Add(sel);

			this.Page.BusAccount.SelectAllFieldManager(dtbFieldManager);

			foreach(DataRow dtrFieldManager in dtbFieldManager.Rows)
			{
				this.ddlFieldManager.Items.Add(new ListItem(dtrFieldManager["LastName"].ToString() + ", " + dtrFieldManager["FirstName"].ToString() + " (" + dtrFieldManager["FMID"].ToString() + ")", dtrFieldManager["FMID"].ToString()));
			}
		}

		public override bool Validate()
		{	
			bool isValid = ValidFromTo(ctrlDateEntryFrom.Date, ctrlDateEntryTo.Date);

			return isValid;
		}

		private bool ValidFromTo(string From,string To)
		{
			if(From != "" || To != "")
			{
				if(From == "" || To == "")
				{
					
					this.Page.MessageManager.Add (this.Page.MessageManager.FormatErrorMessage(Message.ERRMSG_SEARCH_PROVIDE_FROM_TO_1,"Incident ID"));
					return false;
				}
			}
			return true;
		}

		public ParameterValueCollection GetParameterValueCollection()
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;
			
			foreach(System.Web.UI.Control ctrl in this.Controls)
			{
				if(ctrl is QSP.WebControl.InternalTextBoxSearch || ctrl is QSP.WebControl.DropDownListSearch ||ctrl is QSPFulfillment.CommonWeb.UC.DateEntry || ctrl is QSP.WebControl.DropDownListProvince ||ctrl is QSP.WebControl.RadioButtonSearch || ctrl is QSP.WebControl.PostalCode)
				{
					QSP.WebControl.ISearch iSearch =(QSP.WebControl.ISearch)ctrl;
					parameterValue = new ParameterValue();
					parameterValue.Name = iSearch.ParameterName;
					
					if(iSearch.Value != String.Empty)
					{
						parameterValue.Value = iSearch.Value;
					} 
					else
					{
						switch(iSearch.ContentType) 
						{
							case "int" :
								parameterValue.Value = "0";
								break;
							case "string" :
								parameterValue.Value = String.Empty;
								break;
							case "bool" :
								parameterValue.Value = "false";
								break;
							case "DateTime" : 
								parameterValue.Value = "01-01-1995";
								break;
							default :
								goto case "string";
						}
					}

					parameterValues.Add(parameterValue);
				}
			}

			return parameterValues;
		}
	}
}
