namespace QSPFulfillment.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Text;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.CommonWeb;
	using Common;
	using Common.TableDef;
	using Business.Objects;
	/// <summary>
	///		Summary description for GenerateSwitchLetter.
	/// </summary>
	public partial class CampaignBatchReportsControl : QSPFulfillment.AcctMgt.Control.AcctMgtControl
	{
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteStartDate;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteEndDate;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteApprovedStatusDateFrom;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteApprovedStatusDateTo;

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

		}
		#endregion

		protected void btnPreview_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999") 
				{
					SetValueFieldManager();
				}

				RenderReport();
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		#region Fields

		private int CampaignID 
		{
			get 
			{
				int iCampaignID = 0;

				try 
				{
					iCampaignID = Convert.ToInt32(this.tbxCampaignID.Text);
				} 
				catch { }

				return iCampaignID;
			}
			set 
			{
				this.tbxCampaignID.Text = value.ToString();
			}
		}

		private int AccountID 
		{
			get 
			{
				int iAccountID = 0;

				try 
				{
					iAccountID = Convert.ToInt32(this.tbxAccountID.Text);
				} 
				catch { }

				return iAccountID;
			}
			set 
			{
				this.tbxAccountID.Text = value.ToString();
			}
		}

		private string FMID 
		{
			get 
			{
				return this.ddlFieldManager.SelectedValue;
			}
			set 
			{
				this.ddlFieldManager.SelectedIndex = this.ddlFieldManager.Items.IndexOf(this.ddlFieldManager.Items.FindByValue(value.ToString()));
			}
		}

		private DateTime StartDate 
		{
			get 
			{
				DateTime dtStartDate = new DateTime(1995, 1, 1);

				if(this.dteStartDate.Value != String.Empty) 
				{
					dtStartDate = this.dteStartDate.Date;
				}

				return dtStartDate;
			}
			set 
			{
				this.dteStartDate.Date = value;
			}
		}

		private DateTime EndDate 
		{
			get 
			{
				DateTime dtEndDate = new DateTime(1995, 1, 1);

				if(this.dteEndDate.Value != String.Empty) 
				{
					dtEndDate = this.dteEndDate.Date;
				}

				return dtEndDate;
			}
			set 
			{
				this.dteEndDate.Date = value;
			}
		}

		private DateTime ApprovedStatusDateFrom 
		{
			get 
			{
				DateTime dtApprovedStatusDateFrom = new DateTime(1995, 1, 1);

				if(this.dteApprovedStatusDateFrom.Value != String.Empty) 
				{
					dtApprovedStatusDateFrom = this.dteApprovedStatusDateFrom.Date;
				}

				return dtApprovedStatusDateFrom;
			}
			set 
			{
				this.dteApprovedStatusDateFrom.Date = value;
			}
		}

		private DateTime ApprovedStatusDateTo 
		{
			get 
			{
				DateTime dtApprovedStatusDateTo = new DateTime(1995, 1, 1);

				if(this.dteApprovedStatusDateTo.Value != String.Empty) 
				{
					dtApprovedStatusDateTo = this.dteApprovedStatusDateTo.Date;
				}

				return dtApprovedStatusDateTo;
			}
			set 
			{
				this.dteApprovedStatusDateTo.Date = value;
			}
		}

		#endregion

		public override void DataBind()
		{
			LoadDataDDL();
				
			if(QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999") 
			{
				SetValueFieldManager();
			}
		}

		private void LoadDataDDL() 
		{
			LoadDataDDLFieldManager();
		}

		private void LoadDataDDLFieldManager() 
		{
			FieldManager oFieldManager = new FieldManager();
			oFieldManager.GetAll();

			this.ddlFieldManager.DataSource = oFieldManager.dataSet;
			this.ddlFieldManager.DataMember = oFieldManager.dataSet.FieldManager.TableName;
			this.ddlFieldManager.DataTextField = oFieldManager.dataSet.FieldManager.ListNameColumn.ColumnName;
			this.ddlFieldManager.DataValueField = oFieldManager.dataSet.FieldManager.FMIDColumn.ColumnName;

			this.ddlFieldManager.DataBind();
		}

		private void SetValueFieldManager() 
		{
			FMID = QSPPage.aUserProfile.FMID;
			this.lblFieldManager.Text = this.ddlFieldManager.SelectedItem.Text;

			this.ddlFieldManager.Visible = false;
			this.lblFieldManager.Visible = true;
		}

		private void RenderReport() 
		{
			byte[] result;

			CampaignBatchReport oCampaignBatchReport = new CampaignBatchReport();
			result = oCampaignBatchReport.Generate(CampaignID, AccountID, FMID, StartDate, EndDate, ApprovedStatusDateFrom, ApprovedStatusDateTo);

			Response.ClearContent();
			Response.AppendHeader("content-length", result.Length.ToString());
			Response.ContentType = "application/pdf";
			Response.BinaryWrite(result);
			Response.Flush();
			Response.Close();
		}
	}
}
