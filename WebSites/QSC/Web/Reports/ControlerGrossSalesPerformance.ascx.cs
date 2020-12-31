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
	public class ControlerGrossSalesPerformance : QSPFulfillment.CustomerService.CustomerServiceControl
	{
		private const int REPORT_TIMEOUT = -1;

		private System.Text.StringBuilder sb = new System.Text.StringBuilder();
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Button btnPreview;
		
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryTo;
		protected System.Web.UI.WebControls.Label lblFMId;
		protected System.Web.UI.WebControls.Label lblDateFrom;
		protected System.Web.UI.WebControls.Label lblDateTo;
		protected System.Web.UI.WebControls.Label lblProvince;
		protected System.Web.UI.WebControls.Label lblCity;
		protected System.Web.UI.WebControls.Label lblPostalCode;
		protected System.Web.UI.WebControls.Label lblGroupClassCode;
		protected System.Web.UI.WebControls.Label lblGroupCodeName;
		protected System.Web.UI.WebControls.Label lblStaffIndicator;
		protected System.Web.UI.WebControls.Label lblCampaignLanguage;
		protected System.Web.UI.WebControls.Label lblProgramsCA;
		protected System.Web.UI.WebControls.Label lblIncentivesPrograms;
		protected System.Web.UI.WebControls.Label lblCatalogueCode;
		protected QSP.WebControl.DropDownListProvince ddlProvince;
		protected QSP.WebControl.TextBoxSearch tbxCity;
		protected QSP.WebControl.PostalCode tbxPostalCode;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected QSP.WebControl.DropDownListSearch ddlGroupClassCode;
		protected QSP.WebControl.DropDownListSearch ddlGroupCodeName;
		protected QSP.WebControl.DropDownListSearch ddlStaffIndicator;
		protected QSP.WebControl.DropDownListSearch ddlCampaignLanguage;
		protected QSP.WebControl.DropDownListSearch ddlProgramsFromCampaign;
		protected QSP.WebControl.DropDownListSearch ddlIncentivesPrograms;
		protected QSP.WebControl.DropDownListSearch ddlCatalogCode;
		protected QSP.WebControl.DropDownListSearch ddlFieldManager;
		protected System.Web.UI.WebControls.Label lblInternetOrders;
		protected System.Web.UI.WebControls.RadioButtonList rblInternetOrders;
		protected System.Web.UI.WebControls.Label lblFieldManager;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationGrossSalesPerformance;
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
			this.ddlGroupClassCode.SelectedIndexChanged += new System.EventHandler(this.ddlGroupClassCode_SelectedIndexChanged);
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			ParameterValueCollection parameterValues;
			ParameterValue parameterValue;

			if(Validate())
			{
				if(QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999") 
				{
					SetValueFieldManager();
				}

				parameterValues = GetParameterValueCollection();
				
				parameterValue = new ParameterValue();
				parameterValue.Name = "InternetOrders";
				parameterValue.Value = "1";
				parameterValues.Add(parameterValue);

				rsGenerationGrossSalesPerformance.Generate(parameterValues, REPORT_TIMEOUT);
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
				this.SetValueDDLGroupClassCode();
				this.SetValueDDLStaffIndicator();
				this.SetValueDDLCampaignLanguage();
				this.SetValueDDLProgramsFromCampaign();
				this.SetValueDDLIncentivesPrograms();
				this.SetValueDDLCatalogCode();
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

		private void SetValueDDLGroupClassCode()
		{
			ListItem sel = new ListItem("", "");

			sel.Selected = true;
			this.ddlGroupClassCode.Items.Add(sel);
			this.ddlGroupClassCode.Items.Add(new ListItem("School", "Sc"));
			this.ddlGroupClassCode.Items.Add(new ListItem("Sports/Clubs/Affinities", "Sp"));
			this.ddlGroupClassCode.Items.Add(new ListItem("Non School", "NSc"));

			ddlGroupClassCode_SelectedIndexChanged();
		}

		private void SetValueDDLStaffIndicator() 
		{
			ListItem sel = new ListItem("", "2");

			sel.Selected = true;
			this.ddlStaffIndicator.Items.Add(sel);
			this.ddlStaffIndicator.Items.Add(new ListItem("Yes", "1"));
			this.ddlStaffIndicator.Items.Add(new ListItem("No", "0"));
		}

		private void SetValueDDLCampaignLanguage() 
		{
			ListItem sel = new ListItem("", "");

			sel.Selected = true;
			this.ddlCampaignLanguage.Items.Add(sel);
			this.ddlCampaignLanguage.Items.Add(new ListItem("English", "EN"));
			this.ddlCampaignLanguage.Items.Add(new ListItem("French", "FR"));
		}

		private void SetValueDDLProgramsFromCampaign() 
		{
			DataTable dtbProgram = new DataTable();
			ListItem sel = new ListItem("", "");

			sel.Selected = true;
			this.ddlProgramsFromCampaign.Items.Add(sel);

			this.Page.BusAccount.SelectAllProgramFundraising(dtbProgram);

			foreach(DataRow dtrProgram in dtbProgram.Rows)
			{
				this.ddlProgramsFromCampaign.Items.Add(new ListItem(dtrProgram["Name"].ToString(), dtrProgram["ID"].ToString()));
			}
		}

		private void SetValueDDLIncentivesPrograms() 
		{
			DataTable dtbProgram = new DataTable();
			ListItem sel = new ListItem("", "");

			sel.Selected = true;
			this.ddlIncentivesPrograms.Items.Add(sel);

			this.Page.BusAccount.SelectAllProgramIncentives(dtbProgram);

			foreach(DataRow dtrProgram in dtbProgram.Rows)
			{
				this.ddlIncentivesPrograms.Items.Add(new ListItem(dtrProgram["Name"].ToString(), dtrProgram["ID"].ToString()));
			}
		}

		private void SetValueDDLCatalogCode() 
		{
			DataTable dtbCatalogCode = new DataTable();
			ListItem sel = new ListItem("", "");

			sel.Selected = true;
			this.ddlCatalogCode.Items.Add(sel);

			this.Page.BusAccount.SelectAllCatalogCode(dtbCatalogCode);

			foreach(DataRow dtrCatalogCode in dtbCatalogCode.Rows)
			{
				this.ddlCatalogCode.Items.Add(new ListItem(dtrCatalogCode["Content_Catalog_Code"].ToString(), dtrCatalogCode["Content_Catalog_Code"].ToString()));
			}
		}


/*		
		private void SetValueDDL()
		{
			LoadData();
			if(Table.Rows.Count != 0)
			{
				this.ddlReason.DataSource = Table;
				DataRow dtrow = Table.NewRow();
				dtrow[CodeDetailTable.FLD_DESCRIPTION]= "Select";
				dtrow[CodeDetailTable.FLD_INSTANCE] = 0;
				Table.Rows.InsertAt(dtrow,0);
				this.ddlReason.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				this.ddlReason.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlReason.DataBind();
			}
		
		}
		private void LoadData()
		{
			try
			{
				
				Table = new DataTable("CodeDetail");
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table,1000);
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		*/
		
		public override bool Validate()
		{	
			bool isValid = ValidRequiredFields();
			isValid &= ValidFromTo(ctrlDateEntryFrom.Date, ctrlDateEntryTo.Date);

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
			if(this.ddlFieldManager.SelectedIndex != 0 || (this.ctrlDateEntryFrom.Value != String.Empty && this.ctrlDateEntryTo.Value != String.Empty) || this.ddlProvince.SelectedIndex != 0 || this.tbxCity.Text != String.Empty || this.tbxPostalCode.Text != String.Empty || this.ddlGroupClassCode.SelectedIndex != 0 || this.ddlGroupCodeName.SelectedIndex != 0 || this.ddlProgramsFromCampaign.SelectedIndex != 0 || this.ddlIncentivesPrograms.SelectedIndex != 0 || this.ddlCatalogCode.SelectedIndex != 0)
			{
				return true;
			} 
			else 
			{
				this.Page.MessageManager.Add (this.Page.MessageManager.FormatErrorMessage(Message.ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0,""));
				return false;
			}
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

		private void ddlGroupClassCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ddlGroupClassCode_SelectedIndexChanged();
		}

		private void ddlGroupClassCode_SelectedIndexChanged() 
		{
			ListItem Sel = new ListItem("","");
			Sel.Selected = true;
			if(this.ddlGroupClassCode.SelectedValue == "Sc")
			{
				this.ddlGroupCodeName.Items.Clear();
				this.ddlGroupCodeName.Items.Add(Sel);
				this.ddlGroupCodeName.Items.Add(new ListItem("Elementary","Sc1"));
				this.ddlGroupCodeName.Items.Add(new ListItem("High School","Sc2"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Junior High School","Sc3"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Middle School","Sc4"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Cegep","Sc5"));
				this.ddlGroupCodeName.Items.Add(new ListItem("College","Sc6"));
				this.ddlGroupCodeName.Items.Add(new ListItem("University","Sc7"));
				this.ddlGroupCodeName.Items.Add(new ListItem("School Board","Sc8"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Adult","Sc9"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Vocational","Sc10"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Other","Sc11"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Combined","Sc12"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Pre-School","Sc13"));
			}
			else if(this.ddlGroupClassCode.SelectedValue == "Sp")
			{
				this.ddlGroupCodeName.Items.Clear();
				this.ddlGroupCodeName.Items.Add(Sel);
				this.ddlGroupCodeName.Items.Add(new ListItem("Ice Skating","Sp1"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Hockey","Sp2"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Bowling","Sp3"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Soccer","Sp4"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Baseball","Sp5"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Volleyball","Sp6"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Gymnastics","Sp7"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Basketball","Sp8"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Travel","Sp9"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Music Band","Sp10"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Theater","Sp11"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Athletics","Sp12"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Dance","Sp13"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Karaté","Sp14"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Curling","Sp15"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Equestrian","Sp16"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Aqua/Swim","Sp17"));
			}
			else if(this.ddlGroupClassCode.SelectedValue == "NSc")
			{
				this.ddlGroupCodeName.Items.Clear();
				this.ddlGroupCodeName.Items.Add(Sel);
				this.ddlGroupCodeName.Items.Add(new ListItem("Daycare","NSc1"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Gym","NSc2"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Scouts/Guides","NSc3"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Company","NSc4"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Church","NSc5"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Lodge","NSc6"));
				this.ddlGroupCodeName.Items.Add(new ListItem("Other","NSc7"));
			}
			else
			{
				this.ddlGroupCodeName.Items.Clear();
				this.ddlGroupCodeName.Items.Add(Sel);
			}
		}
	}
}
