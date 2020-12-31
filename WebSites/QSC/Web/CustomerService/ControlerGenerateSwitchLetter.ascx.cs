namespace QSPFulfillment.CustomerService
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
	///		Summary description for ControlerGenerateSwitchLetter.
	/// </summary>
	public class ControlerGenerateSwitchLetter : CustomerServiceControl
	{
		private const int REPORT_TIMEOUT = 600000; // This report can take time: 10 minutes

		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationSwitchLetter;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Label lblCodeDescription;
		protected System.Web.UI.WebControls.Button btnPreview;
		protected System.Web.UI.WebControls.TextBox tbxTitleCode;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.HtmlControls.HtmlAnchor A1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Button btnGenerate;
		protected QSP.WebControl.TextBoxSearch tbxRemitBatchID;
		protected System.Web.UI.WebControls.DropDownList ddlReason;
		
		private DataTable Table;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryTo;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator5;
		protected System.Web.UI.WebControls.Label lblDirections;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryFrom;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
				SetValueDDL();
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
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void btnGenerate_Click(object sender, System.EventArgs e)
		{
			StringBuilder sb = new System.Text.StringBuilder();

			int switchLetterBatchInstance = 0;
			
			try
			{
				if(Validate())
				{
					
					this.Page.BusSwitchLetterBatch.ValidateGenerateSwitchLetterBatch(GetRemitBatchID(),GetTitleCode(),GetReason(),ctrlDateEntryFrom.Date,ctrlDateEntryTo.Date);
					switchLetterBatchInstance = this.Page.BusSwitchLetterBatch.GenerateSwitchLetterBatch(GetTitleCode(),GetRemitBatchID(),GetReason(),ctrlDateEntryFrom.Date,ctrlDateEntryTo.Date,this.Page.UserID);
					GenerateReport(switchLetterBatchInstance, "pr_SwitchLetterSelectReport");
					
					/*sb.Append(System.Configuration.ConfigurationSettings.AppSettings["RSDefaultUrl"] + "/QSPCanadaFinance/SwitchLetter&rs:Command=Render&rs:Format=PDF");
					sb.Append(GetParameterQueryString());
					sb.Append("&sReport=pr_SwitchLetterSelectReport&iSwitchLetterBatchID="+switchLetterBatchInstance+"&iCustomerOrderHeaderInstance=0&iTransID=0");
					
					/*
					//temp revert back to old style link to eliminate timeouts
					sb.Append("../Common/RSDirect.aspx?rpt=SwitchLetter");
					sb.Append(GetParameterQueryString());
					sb.Append("&sReport=pr_SwitchLetterSelectReport");
					sb.Append("&iSwitchLetterBatchID="+(int)this.Page.BusSwitchLetterBatch.ResultSetReturned);
					sb.Append("&iCustomerOrderHeaderInstance=0");
					sb.Append("&iTransID=0");
					
					this.Page.RegisterClientScriptBlock("open","<script>window.open('"+sb.ToString()+"','',\"toolbar = yes,status=yes,scrollbars=yes,resizable=yes, width=800, height=550\");</script>");
					*/				
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


		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			StringBuilder sb = new System.Text.StringBuilder();

			try
			{
				if(Validate())
				{
					this.Page.BusSwitchLetterBatch.ValidateGenerateSwitchLetterBatch(GetRemitBatchID(),GetTitleCode(),GetReason(),ctrlDateEntryFrom.Date,ctrlDateEntryTo.Date);
					GenerateReport(0, "pr_SwitchLetterSelectReportPreview");
					/*
					sb.Append(System.Configuration.ConfigurationSettings.AppSettings["RSDefaultUrl"] + "/QSPCanadaFinance/SwitchLetter&rs:Command=Render&rs:Format=PDF");
					sb.Append(GetParameterQueryString());
					sb.Append("&sReport=pr_SwitchLetterSelectReportPreview&iSwitchLetterBatchID=0&iCustomerOrderHeaderInstance=0&iTransID=0");
					this.Page.RegisterClientScriptBlock("open","<script>window.open('"+sb.ToString()+"','',\"toolbar = yes,status=yes,scrollbars=yes,resizable=yes, width=800, height=550\");</script>");
					*/
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
				if(    ctrl is QSP.WebControl.InternalTextBoxSearch 
					|| ctrl is QSP.WebControl.DropDownListSearch 
					|| ctrl is QSPFulfillment.CommonWeb.UC.DateEntry 
					|| ctrl is QSP.WebControl.DropDownListProvince 
					|| ctrl is QSP.WebControl.RadioButtonSearch
					|| ctrl is QSP.WebControl.PostalCode)
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

		private string GetTitleCode()
		{
			if(this.tbxTitleCode.Text == String.Empty)
				return "";

			return this.tbxTitleCode.Text;
		}
		private int GetRemitBatchID()
		{
			if(this.tbxRemitBatchID.Text == String.Empty)
				return 0;

			return Convert.ToInt32(this.tbxRemitBatchID.Text);
		
		}
		private int GetReason()
		{

			return Convert.ToInt32(this.ddlReason.SelectedItem.Value);
		}

		
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
		public override bool Validate()
		{	
			//return true server will validate
			if(GetRemitBatchID() == 0 && ctrlDateEntryFrom.Date == DateTime.MinValue && ctrlDateEntryTo.Date == DateTime.MinValue)
			{
				return true;	
			}
			
			return ValidFromTo(ctrlDateEntryFrom.Date,ctrlDateEntryTo.Date);
		}

		private void GenerateReport(int switchLetterBatchID, string reportName) 
		{
			
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "iRemitBatchID";
			if(this.tbxRemitBatchID.Text.Length > 0)
			{
				parameterValue.Value = this.tbxRemitBatchID.Text;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "dFrom";
			if(this.ctrlDateEntryFrom.Value.Length > 0)
			{
				parameterValue.Value = this.ctrlDateEntryFrom.Value;
			}
			else
			{
				parameterValue.Value = "01/01/1955";
			}
			parameterValues.Add(parameterValue);
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "dTo";
			if(this.ctrlDateEntryTo.Value.Length > 0)
			{
				parameterValue.Value = this.ctrlDateEntryTo.Value;
			}
			else
			{
				parameterValue.Value = "01/01/1955";
			}
			parameterValues.Add(parameterValue);
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "sTitleCode";
			if(this.tbxTitleCode.Text.Length > 0)
			{
				parameterValue.Value = this.tbxTitleCode.Text;
			}
			else
			{
				parameterValue.Value = "0";
			}
			parameterValues.Add(parameterValue);
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "sReport";
			parameterValue.Value = reportName;
			parameterValues.Add(parameterValue);
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "iSwitchLetterBatchID";
			parameterValue.Value = switchLetterBatchID.ToString();
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "iCustomerOrderHeaderInstance";
			parameterValue.Value = "0";
			parameterValues.Add(parameterValue);
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "iTransID";
			parameterValue.Value = "0";
			parameterValues.Add(parameterValue);				
			
			rsGenerationSwitchLetter.Generate(parameterValues, REPORT_TIMEOUT);
		}

	
	}
}
