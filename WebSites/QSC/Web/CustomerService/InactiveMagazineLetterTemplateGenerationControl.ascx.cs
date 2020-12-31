namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Business.Objects;
	using QSPFulfillment.CommonWeb;
	using System.Data;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.TableDef;


	/// <summary>
	///		Summary description for ControlerGenerateSwitchLetter.
	/// </summary>
	public partial class InactiveMagazineLetterTemplateGenerationControl : LetterTemplateGenerationControl
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateFrom;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateTo;
		private DataTable Table;

		protected void Page_Load(object sender, EventArgs e)
		{
			AddJavaScript();
			
			//Run first time page is displayed (isPostback won't work in this case)
			if (ddlReason.Items.Count == 0)
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

		}
		#endregion

		#region Fields

		public override int RunID
		{
			get 
			{
				if (rbRemitBatch.Checked)
					return this.tbxRunID.Value;
				else
					return -1;
			}
			set 
			{
				this.tbxRunID.Value = value;
			}
		}

		public override DateTime DateFrom
		{
			get 
			{
				if (rbDateRange.Checked)
					return this.dteDateFrom.Date;
				else
					return Convert.ToDateTime("1995/01/01");
			}
			set 
			{
				this.dteDateFrom.Date = value;
			}
		}

		public override DateTime DateTo
		{
			get 
			{
				if (rbDateRange.Checked)
					return this.dteDateTo.Date;
				else
					return Convert.ToDateTime("1995/01/01");
			}
			set 
			{
				this.dteDateTo.Date = value;
			}
		}

		public string TitleCode
		{
			get 
			{
				if(this.tbxTitleCode.Text == String.Empty)
					return "";
				return this.tbxTitleCode.Value;
			}
		}

		/*public string CodeDescription
		{
			get 
			{
				return this.lblCodeDescription.Value;
			}
			set 
			{
				this.lblCodeDescription.Value = value;
			}
		}*/

		public int Reason
		{
			get 
			{
				return Convert.ToInt32(this.ddlReason.SelectedItem.Value);
			}
			set 
			{
				this.ddlReason.SelectedItem.Value = Convert.ToString(value);
			}
		}

		#endregion

		#region JavaScript

		private new void AddJavaScript()
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "	function SetEnabledLetterBatchType()\n";
			script += "	{\n";
			script += "		var isRemitBatch = document.getElementById(\"" + rbRemitBatch.ClientID + "\").checked;\n";
			script += "		document.getElementById(\"" + tbxRunID.ClientID + "\").disabled = !isRemitBatch;\n";
			script += "		document.getElementById(\"" + dteDateFrom.ClientID + "_tb_DATE\").disabled = isRemitBatch;\n";
			script += "		document.getElementById(\"" + dteDateTo.ClientID + "_tb_DATE\").disabled = isRemitBatch;\n";
			script += "	}\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("SetEnabledLetterBatchType", script);
			this.Page.RegisterStartupScript("SetEnabledLetterBatchTypeStartup", "<script language=\"javascript\">SetEnabledLetterBatchType();</script>\n");
		}

		#endregion

		private void SetValueDDL()
		{
			LoadData();
			if(Table.Rows.Count != 0)
			{
				this.ddlReason.DataSource = Table;
				DataRow dtrow = Table.NewRow();
				dtrow[CodeDetailTable.FLD_DESCRIPTION]= "Select";
				dtrow[CodeDetailTable.FLD_INSTANCE] = 1;
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

		protected override LetterBatchItem GetLetterBatchItem()
		{
			InactiveMagazineLetterBatchItem inactiveMagazineLetterBatchItem = (InactiveMagazineLetterBatchItem) base.GetLetterBatchItem ();

			inactiveMagazineLetterBatchItem.ProductCode = TitleCode;
			inactiveMagazineLetterBatchItem.Reason = Reason;

			return (LetterBatchItem) inactiveMagazineLetterBatchItem;
		}
	}
}
