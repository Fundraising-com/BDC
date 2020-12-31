namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess;
	using System.Text;
	using QSPFulfillment.CommonWeb;
	using ParameterValue = QSPFulfillment.CommonWeb.ParameterValue;

	/// <summary>
	///		Summary description for ControlerIncidentsManagementReport.
	/// </summary>
	public class ControlerIncidentsManagementReport : CustomerServiceControl
	{
		private const int REPORT_TIMEOUT = 600000; // This report can take time: 10 minutes
		private const string REPORT_NAME = "IncidentsManagement";

		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationSwitchLetter;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.HyperLink hypFindProblemCode;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator5;
		protected QSP.WebControl.TextBoxSearch tbxProblemCode;
		protected QSP.WebControl.TextBoxSearch tbxIncidentIDFrom;
		protected QSP.WebControl.TextBoxSearch tbxIncidentIDTo;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator1;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator2;
		protected QSP.WebControl.DropDownListSearch ddlIncidentStatus;
		protected QSP.WebControl.DropDownListSearch ddlLoggedByUser;
		protected QSP.WebControl.DropDownListSearch ddlFulfillmentHouse;
		protected QSP.WebControl.DropDownListSearch ddlPublisher;
		protected System.Web.UI.WebControls.Label Label1;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateLoggedFrom;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateLoggedTo;
		protected QSP.WebControl.DropDownListSearch ddlAction;
		protected System.Web.UI.WebControls.Button btnSubmit;
		private DataTable dtbAction;
		protected QSP.WebControl.TextBoxSearch tbxTitleCode;
		protected System.Web.UI.HtmlControls.HtmlAnchor A1;
		private DataTable dtbFulfillmentHouse;
		private DataTable dtbPublisher;
		protected QSP.WebControl.RadioButtonSearch rabPrintAllExceptNPW;
		protected QSP.WebControl.RadioButtonSearch rabJustFlaggedNPW;
		protected QSP.WebControl.RadioButtonSearch rabIndividualAction;
		protected System.Web.UI.WebControls.Label lblActionCodes;
		protected System.Web.UI.WebControls.Label Label3;
		protected QSP.WebControl.TextBoxInteger tbxOrderIDFrom;
		protected System.Web.UI.WebControls.Label Label4;
		protected QSP.WebControl.TextBoxInteger tbxOrderIDTo;
		protected System.Web.UI.WebControls.Label Label6;
		protected QSP.WebControl.CheckBoxSearch chkRemoveAutomated;
		private DataTable dtbUser;
      protected QSP.WebControl.TextBoxInteger tbxCampaignID;
      protected QSP.WebControl.TextBoxInteger tbxAccountID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{				
				this.SetValueDropDownList();

			} 
			else
			{
				SetDDLActionEnabled();
				
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
			this.ddlFulfillmentHouse.SelectedIndexChanged += new System.EventHandler(this.ddlFulfillmentHouse_SelectedIndexChanged);
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void SetDDLActionEnabled() 
		{
			if (rabIndividualAction.Checked) 
			{
				this.ddlAction.Enabled = true;
			}
		}
		private void SetValueDDLStatus()
		{
			if(ddlIncidentStatus.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusIncidentStatus.SelectAll(Table);
						
				foreach(DataRow row in Table.Rows)
				{
					ddlIncidentStatus.Items.Add(new ListItem(row[IncidentStatusTable.FLD_DESCRIPTION].ToString(),row[IncidentStatusTable.FLD_INSTANCE].ToString()));
				}
						
				
			}
		}
		public override bool Validate()
		{	
			bool isValid = ValidRequiredFields();
			isValid &= ValidFromTo(this.ctrlDateLoggedFrom.Date, this.ctrlDateLoggedTo.Date);
			isValid &= ValidFromTo(this.tbxIncidentIDFrom.Text, this.tbxIncidentIDTo.Text);
			isValid &= ValidFromTo(this.tbxOrderIDFrom.Text, this.tbxOrderIDTo.Text);
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

		private bool ValidRequiredFields() 
		{
          if ((this.tbxIncidentIDFrom.Text != String.Empty && this.tbxIncidentIDTo.Text != String.Empty) || (this.ctrlDateLoggedFrom.Value != String.Empty && this.ctrlDateLoggedTo.Value != String.Empty) || (this.tbxOrderIDFrom.Value != 0 && this.tbxOrderIDTo.Value != 0) || this.ddlLoggedByUser.SelectedIndex != 0 || this.tbxProblemCode.Text != String.Empty || this.ddlFulfillmentHouse.SelectedIndex != 0 || this.ddlPublisher.SelectedIndex != 0 || this.tbxTitleCode.Text != String.Empty || this.tbxCampaignID.Text != String.Empty || this.tbxAccountID.Text != String.Empty) 
			{
				return true;
			} 
			else 
			{
				this.Page.MessageManager.Add (this.Page.MessageManager.FormatErrorMessage(Message.ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0,""));
				return false;
			}
		}

		private void SetValueDDLAction()
		{
	
			if(ddlAction.Items.Count == 0)
			{
				dtbAction = new DataTable();
				this.Page.BusAction.SelectAll(dtbAction,0,0, false);
				DataRow rRow = dtbAction.NewRow();
				rRow[ActionTable.FLD_DESCRIPTION] = "All";
				dtbAction.Rows.InsertAt(rRow,0);
				

				foreach(DataRow row in dtbAction.Rows)
				{
					this.ddlAction.Items.Add(new ListItem(row[ActionTable.FLD_DESCRIPTION].ToString(),row[ActionTable.FLD_INSTANCE].ToString()));	
						
				}
			}
			
		}
		private void SetValueDDLFulfillmentHouse()
		{
	
			if(ddlFulfillmentHouse.Items.Count == 0)
			{
				dtbFulfillmentHouse = new DataTable();
				dtbFulfillmentHouse.Rows.Add(dtbFulfillmentHouse.NewRow());
				this.Page.BusAccount.SelectAllFulfillmentHouse(dtbFulfillmentHouse);

				foreach(DataRow row in dtbFulfillmentHouse.Rows)
				{
					this.ddlFulfillmentHouse.Items.Add(new ListItem(row["ful_name"].ToString(),row["ful_nbr"].ToString()));	
				 		
				
				}
			}
			
		}
		private void SetValueDDLUser()
		{
	
			if(ddlLoggedByUser.Items.Count == 0)
			{
				dtbUser = new DataTable();
				dtbUser.Rows.Add(dtbUser.NewRow());
				this.Page.BusAccount.SelectAllIncludInIncident(dtbUser);

				foreach(DataRow row in dtbUser.Rows)
				{
					
					string Seperator = ",";
					if(row["lastname"].ToString()== String.Empty && row["firstname"].ToString()== String.Empty)
						Seperator = "";

					this.ddlLoggedByUser.Items.Add(new ListItem(row["lastname"].ToString()+Seperator+row["firstname"].ToString(),row["instance"].ToString()));	
				 		
				
				}
			}
			
		}
		private void SetValueDDLPublisher()
		{
	
			if(ddlPublisher.Items.Count == 0)
			{
				dtbPublisher = new DataTable();
				dtbPublisher.Rows.Add(dtbPublisher.NewRow());
				this.Page.BusAccount.SelectAllPublisher(dtbPublisher,FulfillmentHouseID);

				foreach(DataRow row in dtbPublisher.Rows)
				{
					this.ddlPublisher.Items.Add(new ListItem(row["pub_name"].ToString(),row["pub_nbr"].ToString()));	
				 		
				
				}
			}
			
		}


		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			StringBuilder sb = new System.Text.StringBuilder();

			try
			{
				if(Validate())
				{
					GenerateReport();
					/*sb.Append(System.Configuration.ConfigurationSettings.AppSettings["RSDefaultUrl"] + "/Customer Service Reports/IncidentsManagement&rs:Command=Render&rs:Format=PDF");
					sb.Append(GetParameterQueryString());

					if(this.tbxOrderIDFrom.Value != 0 && this.tbxOrderIDTo.Value != 0) 
					{
						sb.Append("&OrderIDFrom=" + this.tbxOrderIDFrom.Text + "&OrderIDTo=" + this.tbxOrderIDTo.Text);
					}

					this.Page.RegisterClientScriptBlock("open","<script>window.open('"+sb.ToString()+"','',\"toolbar = yes,status=yes,scrollbars=yes,resizable=yes, width=800, height=550\");</script>");*/
					
				}
				else
				{
					this.Page.MessageManager.PrepareErrorMessage();
					this.Page.SetPageError();
				}
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}

		public string GetParameterQueryString()
		{
			StringBuilder sb = new StringBuilder();

			foreach(System.Web.UI.Control ctrl in this.Controls)
			{
				if(ctrl is QSP.WebControl.InternalTextBoxSearch || ctrl is QSP.WebControl.DropDownListSearch ||ctrl is QSPFulfillment.CommonWeb.UC.DateEntry || ctrl is QSP.WebControl.DropDownListProvince ||ctrl is QSP.WebControl.RadioButtonSearch || ctrl is QSP.WebControl.CheckBoxSearch || ctrl is QSP.WebControl.PostalCode)
				{
					QSP.WebControl.ISearch iSearch =(QSP.WebControl.ISearch)ctrl;

					
					if(iSearch.Value != String.Empty)
					{
						sb.Append("&"+iSearch.ParameterName+"="+iSearch.Value);
					} 
					else
					{
						switch(iSearch.ContentType) 
						{
							case "int" :
								sb.Append("&" + iSearch.ParameterName + "=0");
								break;
							case "string" :
								goto case "int";
							case "bool" :
								sb.Append("&" + iSearch.ParameterName + "=false");
								break;
							case "DateTime" : 
								sb.Append("&" + iSearch.ParameterName + "=01-01-1955");
								break;
							default :
								goto case "int";
						}
					}
				}

			}

			return sb.ToString();
			
		}
		protected override void AddJavaScript()
		{
			this.hypFindProblemCode.Attributes.Add("onclick","javascript:Open('ProblemCode.aspx?IsNewWindow=true&ID=true');");
			this.rabIndividualAction.Attributes.Add("onclick","javascript:OnClickAction('"+this.rabIndividualAction.UniqueID.Replace("$","_")+"');");
			this.rabJustFlaggedNPW.Attributes.Add("onclick","javascript:OnClickAction('"+this.rabIndividualAction.UniqueID.Replace("$","_")+"');");
			this.rabPrintAllExceptNPW.Attributes.Add("onclick","javascript:OnClickAction('"+this.rabIndividualAction.UniqueID.Replace("$","_")+"');");
		}
		private void SetValueDropDownList()
		{
			try
			{
				this.SetValueDDLAction();
				this.SetValueDDLStatus();
				this.SetValueDDLFulfillmentHouse();
				this.SetValueDDLUser();
				this.SetValueDDLPublisher();
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
			
		}

		private void ddlFulfillmentHouse_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ddlPublisher.Items.Clear();
			SetValueDDLPublisher();
		}
		private int FulfillmentHouseID
		{
			get
			{
				if(this.ddlFulfillmentHouse.SelectedIndex < 1)
					return 0;

				return Convert.ToInt32(this.ddlFulfillmentHouse.SelectedItem.Value);
			}
		}

		private void GenerateReport() 
		{			
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			QSPFulfillment.CommonWeb.ParameterValue parameterValue;
				
			//IncidentStatusInstance
			parameterValue = new QSPFulfillment.CommonWeb.ParameterValue();
			parameterValue.Name = "IncidentStatusInstance";
			if(this.ddlIncidentStatus.SelectedValue.Length > 0)
			{
				parameterValue.Value = this.ddlIncidentStatus.SelectedValue;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);

			//IncidentIDFrom
			parameterValue = new ParameterValue();
			parameterValue.Name = "IncidentIDFrom";
			if(this.tbxIncidentIDFrom.Text.Length > 0)
			{
				parameterValue.Value = this.tbxIncidentIDFrom.Text;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);
				
			//IncidentIDTo
			parameterValue = new ParameterValue();
			parameterValue.Name = "IncidentIDTo";
			if(this.tbxIncidentIDTo.Text.Length > 0)
			{
				parameterValue.Value = this.tbxIncidentIDTo.Text;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);

			//DateIncidentLoggedFrom
			parameterValue = new ParameterValue();
			parameterValue.Name = "DateIncidentLoggedFrom";
			if(this.ctrlDateLoggedFrom.Value.Length > 0)
			{
				parameterValue.Value = this.ctrlDateLoggedFrom.Value;
			}
			else
			{
				parameterValue.Value = "01/01/1955";
			}
			parameterValues.Add(parameterValue);
				
			//DateIncidentLoggedTo
			parameterValue = new ParameterValue();
			parameterValue.Name = "DateIncidentLoggedTo";
			if(this.ctrlDateLoggedTo.Value.Length > 0)
			{
				parameterValue.Value = this.ctrlDateLoggedTo.Value;
			}
			else
			{
				parameterValue.Value = "01/01/1955";
			}
			parameterValues.Add(parameterValue);

			if(this.tbxOrderIDFrom.Value != 0 && this.tbxOrderIDTo.Value != 0) 
			{
				//OrderIDFrom
				parameterValue = new ParameterValue();
				parameterValue.Name = "OrderIDFrom";
				if(this.tbxOrderIDFrom.Text.Length > 0)
				{
					parameterValue.Value = this.tbxOrderIDFrom.Text;
				}
				else
				{
					parameterValue.Value = "0";
				}
				parameterValues.Add(parameterValue);
				
				//OrderIDTo
				parameterValue = new ParameterValue();
				parameterValue.Name = "OrderIDTo";
				if(this.tbxOrderIDTo.Text.Length > 0)
				{
					parameterValue.Value = this.tbxOrderIDTo.Text;
				}
				else
				{
					parameterValue.Value = "0";
				}
				parameterValues.Add(parameterValue);
			}

			//LoggedByInstance
			parameterValue = new ParameterValue();
			parameterValue.Name = "LoggedByInstance";
			if(this.ddlLoggedByUser.SelectedValue.Length > 0)
			{
				parameterValue.Value = this.ddlLoggedByUser.SelectedValue;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);

			//ProblemCodeInstance
			parameterValue = new ParameterValue();
			parameterValue.Name = "ProblemCodeInstance";
			if(this.tbxProblemCode.Text.Length > 0)
			{
				parameterValue.Value = this.tbxProblemCode.Text;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);

			//FulfillmentHouseInstance
			parameterValue = new ParameterValue();
			parameterValue.Name = "FulfillmentHouseInstance";
			if(this.ddlFulfillmentHouse.SelectedValue.Length > 0)
			{
				parameterValue.Value = this.ddlFulfillmentHouse.SelectedValue;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);

			//PublisherInstance
			parameterValue = new ParameterValue();
			parameterValue.Name = "PublisherInstance";
			if(this.ddlPublisher.SelectedValue.Length > 0)
			{
				parameterValue.Value = this.ddlPublisher.SelectedValue;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);

			//TitleCodeInstance
			parameterValue = new ParameterValue();
			parameterValue.Name = "TitleCodeInstance";
			if(this.tbxTitleCode.Text.Length > 0)
			{
				parameterValue.Value = this.tbxTitleCode.Text;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);

			//PrintAll
			parameterValue = new ParameterValue();
			parameterValue.Name = "PrintAll";
			parameterValue.Value = this.rabPrintAllExceptNPW.Checked.ToString();
			parameterValues.Add(parameterValue);

			//JustOne
			parameterValue = new ParameterValue();
			parameterValue.Name = "JustOne";
			parameterValue.Value = this.rabJustFlaggedNPW.Checked.ToString();
			parameterValues.Add(parameterValue);

			//ByIndividual
			parameterValue = new ParameterValue();
			parameterValue.Name = "ByIndividual";
			parameterValue.Value = this.rabIndividualAction.Checked.ToString();
			parameterValues.Add(parameterValue);

			//ActionInstance
			parameterValue = new ParameterValue();
			parameterValue.Name = "ActionInstance";
			if(this.ddlAction.SelectedValue.Length > 0)
			{
				parameterValue.Value = this.ddlAction.SelectedValue;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);

			//RemoveAutomated
			parameterValue = new ParameterValue();
			parameterValue.Name = "RemoveAutomated";
			parameterValue.Value = this.chkRemoveAutomated.Checked.ToString();
			parameterValues.Add(parameterValue);

         //CampaignID
         parameterValue = new ParameterValue();
         parameterValue.Name = "CampaignID";
         if (this.tbxCampaignID.Text.Length > 0)
         {
             parameterValue.Value = this.tbxCampaignID.Text;
         }
         else
         {
             parameterValue.Value = "0";
         }
         parameterValues.Add(parameterValue);


         //AccountID
         parameterValue = new ParameterValue();
         parameterValue.Name = "AccountID";
         if (this.tbxAccountID.Text.Length > 0)
         {
             parameterValue.Value = this.tbxAccountID.Text;
         }
         else
         {
             parameterValue.Value = "0";
         }
         parameterValues.Add(parameterValue);

			rsGenerationSwitchLetter.Generate(REPORT_NAME, parameterValues, REPORT_TIMEOUT);
		}
	}
}