namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Text;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.CommonWeb;
	using Business.Objects;
	using Common;

	/// <summary>
	///		Summary description for ControlerSwitchLetter.
	/// </summary>
	public class InactiveMagazineLetterBatchMaintenanceControl : LetterBatchMaintenanceControl
	{
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.DataGrid dtgMain;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationSwitchLetter;
		protected System.Web.UI.WebControls.Label Label6;
		protected QSP.WebControl.DropDownListInteger ddlLetterBatchTypeSearch;
		protected QSP.WebControl.TextBoxInteger tbxRemitBatchIDFromSearch;
		protected QSP.WebControl.TextBoxInteger tbxRemitBatchIDToSearch;
		protected QSP.WebControl.DropDownListInteger ddlIsPrintedSearch;
		protected QSP.WebControl.DropDownListInteger ddlIsLockedSearch;
		protected QSPFulfillment.CustomerService.LetterTemplateSelectionDropDownList ddlLetterTemplateSearch;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateCreatedFromSearch;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateCreatedToSearch;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateFromSearch;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label10;
		protected QSP.WebControl.DropDownListInteger ddlReason;
		protected System.Web.UI.HtmlControls.HtmlAnchor A1;
		protected QSP.WebControl.TextBoxSearch tbxTitleCode;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateToSearch;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.ddlLetterTemplateSearch.SelectedIndexChanged += new EventHandler(ddlLetterTemplateSearch_SelectedIndexChanged);
			InitializeComponent();
			base.OnInit(e, dtgMain);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try 
			{
				DataBind();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ddlLetterTemplateSearch_SelectedIndexChanged(object sender, EventArgs e)
		{
			try 
			{
				SelectedTemplateChangedArgs args = new SelectedTemplateChangedArgs(ddlLetterTemplateSearch.SelectedLetterTemplateItem);

				OnSelectedTemplateChanged(args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected override string ExtendedTable
		{
			get
			{
				return "InactiveMagazine";
			}
		}

		#region Fields

		protected override int LetterTemplateIDSearch
		{
			get
			{
				return ddlLetterTemplateSearch.Value;
			}
			set
			{
				ddlLetterTemplateSearch.Value = value;
			}
		}

		protected override DateTime DateCreatedFromSearch
		{
			get
			{
				return dteDateCreatedFromSearch.Date;
			}
			set
			{
				dteDateCreatedFromSearch.Date = value;
			}
		}

		protected override DateTime DateCreatedToSearch
		{
			get
			{
				return dteDateCreatedToSearch.Date;
			}
			set
			{
				dteDateCreatedToSearch.Date = value;
			}
		}

		protected override LetterBatchType LetterBatchTypeSearch
		{
			get
			{
				return (LetterBatchType) ddlLetterBatchTypeSearch.Value;
			}
			set
			{
				ddlLetterBatchTypeSearch.Value = Convert.ToInt32(value);
			}
		}

		protected override DateTime DateFromSearch
		{
			get
			{
				return dteDateFromSearch.Date;
			}
			set
			{
				dteDateFromSearch.Date = value;
			}
		}

		protected override DateTime DateToSearch
		{
			get
			{
				return dteDateToSearch.Date;
			}
			set
			{
				dteDateToSearch.Date = value;
			}
		}

		protected override int RunIDFromSearch
		{
			get
			{
				return tbxRemitBatchIDFromSearch.Value;
			}
			set
			{
				tbxRemitBatchIDFromSearch.Value = value;
			}
		}

		protected override int RunIDToSearch
		{
			get
			{
				return tbxRemitBatchIDToSearch.Value;
			}
			set
			{
				tbxRemitBatchIDToSearch.Value = value;
			}
		}

		protected override BooleanNullable IsPrintedSearch
		{
			get
			{
				return new BooleanNullable(ddlIsPrintedSearch.Value);
			}
			set
			{
				ddlIsPrintedSearch.Value = value.IntValue;
			}
		}

		protected override BooleanNullable IsLockedSearch
		{
			get
			{
				return new BooleanNullable(ddlIsLockedSearch.Value);
			}
			set
			{
				ddlIsLockedSearch.Value = value.IntValue;
			}
		}

		protected int Reason
		{
			get
			{
				return this.ddlReason.Value;
			}
			set
			{
				ddlReason.Value = value;
			}
		}

		protected string TitleCode
		{
			get
			{
				return this.tbxTitleCode.Value;
			}
		}

		#endregion

		public override void DataBind()
		{
			InactiveMagazineLetterBatch inactiveMagazineLetterBatch = LoadData() as InactiveMagazineLetterBatch;
			this.dtgMain.DataSource = inactiveMagazineLetterBatch.dataSet;
			this.dtgMain.DataMember = inactiveMagazineLetterBatch.dataSet.InactiveMagazineLetterBatch.TableName;
			this.dtgMain.DataBind();
		}

		protected override LetterBatch LoadData() 
		{
			InactiveMagazineLetterBatch inactiveMagazineLetterBatch = new InactiveMagazineLetterBatch();

			inactiveMagazineLetterBatch.GetAll(GetLetterBatchSearchCriteria());

			return inactiveMagazineLetterBatch;
		}

		protected override LetterBatchSearchCriteria GetLetterBatchSearchCriteria()
		{
			InactiveMagazineLetterBatchSearchCriteria inactiveMagazineLetterBatchSearchCriteria = new InactiveMagazineLetterBatchSearchCriteria();
			
			inactiveMagazineLetterBatchSearchCriteria.fill(LetterTemplateIDSearch, DateCreatedFromSearch, DateCreatedToSearch, LetterBatchTypeSearch, DateFromSearch, DateToSearch, RunIDFromSearch, RunIDToSearch, IsPrintedSearch, IsLockedSearch, TitleCode, Reason);
			
			return inactiveMagazineLetterBatchSearchCriteria;
		}

		public override void DataBindStatelessData()
		{
			ddlLetterTemplateSearch.DataBind();
		}

		public override void DataBindInitialData()
		{
			DataBindDDL();
		}

		private void DataBindDDL() 
		{
			DataBindDDLLetterBatchTypeSearch();
		}

		private void DataBindDDLLetterBatchTypeSearch() 
		{
			CodeDetail codeDetail = new CodeDetail(CodeHeaderInstance.LetterBatchType);

			ddlLetterBatchTypeSearch.DataSource = codeDetail.dataSet;
			ddlLetterBatchTypeSearch.DataMember = codeDetail.dataSet.CodeDetail.TableName;
			ddlLetterBatchTypeSearch.DataTextField = codeDetail.dataSet.CodeDetail.DescriptionColumn.ColumnName;
			ddlLetterBatchTypeSearch.DataValueField = codeDetail.dataSet.CodeDetail.InstanceColumn.ColumnName;
			ddlLetterBatchTypeSearch.DataBind();
		}

		protected int GetTitleCode(DataGridItem e) 
		{
			return Convert.ToInt32(((Label) e.FindControl("lblTitleCode")).Text);
		}

		protected int GetReason(DataGridItem e) 
		{
			return Convert.ToInt32(((Label) e.FindControl("lblReason")).Text);
		}

	}
}
