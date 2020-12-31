using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPFulfillment.CommonWeb;



namespace QSPFulfillment.OrderMgt
{
	/// <summary>
	/// Summary description for OrderStageTracking.
	/// </summary>
	public class OrderStageTracking : QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.Label lblOrderStageTracking;
		protected System.Web.UI.WebControls.Button pbReset;
		protected System.Web.UI.WebControls.Label lblSearch;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ucDateFrom,ucDateTo;
		protected System.Web.UI.WebControls.Label lblGroupId;
		protected System.Web.UI.WebControls.Label lblGroupname;
		protected System.Web.UI.WebControls.Label lblCAId;
		protected System.Web.UI.WebControls.TextBox tbGroupId;
		protected System.Web.UI.WebControls.TextBox tbGroupName;
		protected System.Web.UI.WebControls.TextBox tbCAId;
		protected System.Web.UI.WebControls.Label lblFM;
		protected System.Web.UI.WebControls.Label lblDateTo;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.DropDownList ddlStagingStatus;
      protected System.Web.UI.WebControls.DropDownList ddlProductType;
      protected System.Web.UI.WebControls.Button pbSearch;
		protected QSPFulfillment.CommonWeb.UC.FieldManagerDDL		ucFMddl;
		protected DBauer.Web.UI.WebControls.HierarGrid dgTrackingFiles;
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected System.Web.UI.WebControls.Label lblOrdId;
		protected System.Web.UI.WebControls.TextBox tbOrderId;
		protected System.Web.UI.WebControls.Label lblDateFrom;
		protected System.Web.UI.WebControls.Label lblLoggedFMId;
		DataView dvTrackingFiles = new DataView();
        protected QSPFulfillment.OrderMgt.UC.OrderQualifier ucOrderQualifier;
        protected System.Web.UI.WebControls.CheckBox ShowOrdersPastStageCheckBox;

		public string dgSortfield = "CampaignId";
		string SeasonStart;
		string SeasonEnd;

				
		protected string strStartDate, strEndDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			// If a FM login disable DDL and show only the FMId
			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999")
			{
				ucFMddl.Visible=false;
				this.lblLoggedFMId.Text = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID;
			}	
			if (!IsPostBack)
			{
				dgTrackingFiles.Visible = false;
				populate_DDList();
				dgTrackingFiles_bind();
			}
			//dgTrackingFiles.Visible = false;
			lblErrorMessage.Text =" ";
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ddlStagingStatus.SelectedIndexChanged += new System.EventHandler(this.ddlStagingStatus_SelectedIndexChanged);
         this.ddlProductType.SelectedIndexChanged += new System.EventHandler(this.ddlProductType_SelectedIndexChanged);
         this.pbSearch.Click += new System.EventHandler(this.pbSearch_Click);
			this.pbReset.Click += new System.EventHandler(this.pbReset_Click);
			this.dgTrackingFiles.TemplateSelection += new DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventHandler(this.dgTrackingFiles_TemplateSelection);
			this.dgTrackingFiles.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgTrackingFiles_PageIndexChanged);
			this.dgTrackingFiles.SelectedIndexChanged += new System.EventHandler(this.dgTrackingFiles_SelectedIndexChanged);
			this.dgTrackingFiles.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgTrackingFiles_ItemDataBound);
			this.dgTrackingFiles.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgTrackingFiles_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void populate_DDList()
		{
			this.ucFMddl.Bind(1); //mode = 1

			DataSet OrderStageStatusds = new DataSet();
			DAL.CodeDetailDataAccess CodeDetailData =	new DAL.CodeDetailDataAccess();
			OrderStageStatusds = CodeDetailData.GetCodeDesc(59000); //OrderTrackingStages
			ddlStagingStatus.DataSource= OrderStageStatusds;
			ddlStagingStatus.DataBind();
			ddlStagingStatus.Items.Insert(0, new ListItem("All", String.Empty));

			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999") 
			{
				
				ddlStagingStatus.Items.RemoveAt(2);
				ddlStagingStatus.Items.RemoveAt(3);
				ddlStagingStatus.Items.RemoveAt(3);
			}

         this.ucOrderQualifier.Bind();

         DataSet orderStageProductLineds = new DataSet();
         DAL.CodeDetailDataAccess CodeDetailProductLineData = new DAL.CodeDetailDataAccess();
         orderStageProductLineds = CodeDetailData.GetCodeDesc(46000);
         ddlProductType.DataSource = orderStageProductLineds;
         ddlProductType.DataBind();
         ddlProductType.Items.Insert(0, new ListItem("All", String.Empty));
		}

		private void dgTrackingFiles_bind()

		{
			GetSeasonStartAndEnd();

			//Start
			this.dgTrackingFiles.RowExpanded.CollapseAll();
			DataSet TrackingFilesds = new DataSet();
			Business.OrderStageTracking ordTrack = new Business.OrderStageTracking();
			
			if (tbGroupId.Text=="")
			{ordTrack.AccountID= 0;}
			else
			{ordTrack.AccountID=Convert.ToInt32(tbGroupId.Text);}
		
			ordTrack.Account= this.tbGroupName.Text;

			//Need validation
			if (this.tbCAId.Text=="")
			{ordTrack.Campaign= 0;}
			else
			{ordTrack.Campaign=Convert.ToInt32(tbCAId.Text);}

			//required
			if (this.ucDateFrom.Date == System.DateTime.MinValue )
			{
				//{ordTrack.FromDate=Convert.ToDateTime("01/01/1995");}
				//this.ucDateFrom.Date =System.DateTime.Now;

				this.ucDateFrom.Date = Convert.ToDateTime(SeasonStart);
				ordTrack.FromDate=Convert.ToDateTime(SeasonStart);
			}
				
			else
			{ordTrack.FromDate= this.ucDateFrom.Date;}
			
			if (this.ucDateTo.Date == System.DateTime.MinValue )
			{
				//this.ucDateTo.Date =System.DateTime.Now;
				//ordTrack.ToDate=System.DateTime.Now;

				this.ucDateTo.Date = Convert.ToDateTime(SeasonEnd);
				ordTrack.ToDate= Convert.ToDateTime(SeasonEnd);
			}
			else
			{ordTrack.ToDate= this.ucDateTo.Date;}
			
			//From date less than to date
			if (this.ucDateFrom.Date > this.ucDateTo.Date)
			{
				lblErrorMessage.Text = "Invalid Order From/To Date, please correct ";
				dgTrackingFiles.Visible = false;
			}

			if (tbOrderId.Text=="")
			{ordTrack.OrderId= 0;}
			else
			{ordTrack.OrderId=Convert.ToInt32(tbOrderId.Text);}

            ordTrack.ShowOrdersPastStage = this.ShowOrdersPastStageCheckBox.Checked;
                
            ordTrack.OrderQualifierID = this.ucOrderQualifier.SelectedValue;
			
			if (ddlStagingStatus.SelectedValue == "")
			{ ordTrack.Status="ALL";}
			else
			{ordTrack.Status= ddlStagingStatus.SelectedValue;}

         if (ddlProductType.SelectedValue == "")
         { ordTrack.ProductType = 0; }
         else
         { ordTrack.ProductType = Convert.ToInt32(ddlProductType.SelectedValue); }

			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999") 
			{
				ordTrack.FieldManager = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID;
			}
			else
				// if not FM
			{
				if (ucFMddl.SelectedValue == "")
				{
				{ ordTrack.FieldManager="";}
				}
				else
				{
				{ordTrack.FieldManager= this.ucFMddl.SelectedValue;}
				
				}
			}
			// disabled for Only logged on Fm if it is FM
			//if (this.ucFMddl.SelectedValue =="")
			//{ ordTrack.FieldManager="";}
			//else
			//{ordTrack.FieldManager= this.ucFMddl.SelectedValue;}

			
			TrackingFilesds = ordTrack.GetTrackingFilesDataSet();
			
			//Filter stages for FMs
			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999") 
			{
				//for (int i=0; i < TrackingFilesds.Tables[2].Rows.Count; i++)
				//{
				//	string ab = Convert.ToString(TrackingFilesds.Tables[2].Rows[i]["Stage"]);
				//	if (ab =="59001" || ab =="59003" || ab =="59004")
				//	{
				//		TrackingFilesds.Tables[2].Rows[i].Delete();
						
				//	}

				//}
				//for (int i=0; i < TrackingFilesds.Tables[1].Rows.Count; i++)
				//{
				//	string ab = Convert.ToString(TrackingFilesds.Tables[1].Rows[i]["Stage"]);
				//	if (ab =="59001" || ab =="59003" || ab =="59004")
				//	{
				//		TrackingFilesds.Tables[1].Rows[i].Delete();
						
				//	}

				//}
			}
		
			//Relations
			DataColumn dc1;
			DataColumn dc2;
			DataColumn dc3;
			DataColumn dc4;
			//DataColumn dc5;
			//DataColumn dc6;

			//Relation Files to orders on transmissionSeq# column
			dc1 = TrackingFilesds.Tables[0].Columns["CampaignId"];
			dc2 = TrackingFilesds.Tables[1].Columns["CampaignId"];
			DataRelation dr = new DataRelation("Files_Order", dc1, dc2, false);
			TrackingFilesds.Relations.Add(dr);

			
			//Relation Order to order detail on OrderId column only when user is not FM
			dc3 = TrackingFilesds.Tables[1].Columns["OrderId"];
			dc4 =  TrackingFilesds.Tables[2].Columns["OrderId"];
            
			DataRelation dr1 = new DataRelation("Order_OrderDetail", dc3, dc4, false);
			TrackingFilesds.Relations.Add(dr1);

			dvTrackingFiles = TrackingFilesds.Tables["Files"].DefaultView;
			dvTrackingFiles.Sort = dgSortfield;
			dgTrackingFiles.DataSource = dvTrackingFiles;
			
			try
			{
				dgTrackingFiles.DataBind();
			}
			catch( Exception e)
			{
				// If Numbers of records filtered are less than the current page index reset Page index
				if (dgTrackingFiles.CurrentPageIndex> (dvTrackingFiles.Count/dgTrackingFiles.PageSize)  )
				{
					dgTrackingFiles.CurrentPageIndex = 0;
					dgTrackingFiles.DataBind();
				}
				string a =e.Message;
			}
				
		}
				

		private void pbSearch_Click(object sender, System.EventArgs e)
		{
			dgTrackingFiles.Visible = true;
			dgTrackingFiles_bind();
		}

		private void ddlStagingStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			dgTrackingFiles_bind();
		}

      private void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         dgTrackingFiles_bind();
      }

		private void dgTrackingFiles_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgTrackingFiles.CurrentPageIndex = e.NewPageIndex;
			dgTrackingFiles_bind();
		}

		private void dgTrackingFiles_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			dgSortfield = e.SortExpression;
			dgTrackingFiles_bind();
		}
		private void dgTrackingFiles_TemplateSelection(object sender, DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventArgs e)
		{
			e.TemplateFilename = "UC\\TrackingOrders.ascx";
		}

		private void dgTrackingFiles_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			dgTrackingFiles_bind();
		}

		private void pbReset_Click(object sender, System.EventArgs e)
		{
			GetSeasonStartAndEnd();
			lblErrorMessage.Text =" ";
			tbGroupId.Text="";
			tbGroupName.Text="";
			tbCAId.Text="";
			ucDateFrom.Date = Convert.ToDateTime(SeasonStart);
			ucDateTo.Date = Convert.ToDateTime(SeasonEnd);
			tbOrderId.Text="";
			ddlStagingStatus.SelectedValue = "";
						
			dgTrackingFiles.Visible = false;
		}

		public void GetSeasonStartAndEnd()
		{
			//Get season start end dates and use it for search if date not provided
			DataSet Seasonds = new DataSet();
			DAL.SeasonData SeasonData =	new DAL.SeasonData();
			SeasonData.SelectCurrentSeasonStartAndEnd(Seasonds,"SeasonDataSet"); 
				
			SeasonStart =  Convert.ToString(Seasonds.Tables[0].Rows[0]["StartDate"]);
			SeasonEnd =     Convert.ToString(Seasonds.Tables[0].Rows[0]["EndDate"]);
		}

		private void dgTrackingFiles_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			ParameterValueCollection parameterValues;
			ParameterValue parameterValue;

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label lblCampaignID	= (Label)e.Item.FindControl("CAID");
				Label lblCAStart	= (Label)e.Item.FindControl("CAStart");
				Label lblCAEnd	    = (Label)e.Item.FindControl("CAEnd");
				Label lblinternet	= (Label)e.Item.FindControl("lblOnline");

			
				RSGenerationLinkButton rsGenerationOnlineStatementReport = (RSGenerationLinkButton) e.Item.FindControl("rsGenerationOnlineStatementReport");

				if(rsGenerationOnlineStatementReport != null) 
				{
					rsGenerationOnlineStatementReport.Mode = FilePageMode.PopUp;
					//only if CA has online Orders
					if (lblinternet.Text == "Y")
					{
						rsGenerationOnlineStatementReport.Text = "Online Statement";
					}
					else
					{
						rsGenerationOnlineStatementReport.Text = "";
					}

					rsGenerationOnlineStatementReport.ReportName =  "OnlineProgramProfitStatement";

					parameterValues = new ParameterValueCollection();

					parameterValue = new ParameterValue();
					parameterValue.Name = "CampaignID";
					parameterValue.Value = lblCampaignID.Text.Trim();
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "DateFrom";
					parameterValue.Value = SeasonStart;//lblCAStart.Text.Trim(); 
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "DateTo";
					parameterValue.Value = SeasonEnd; // lblCAEnd.Text.Trim(); 
					parameterValues.Add(parameterValue);

					rsGenerationOnlineStatementReport.ParameterValues = parameterValues;
					
				}
			}
		}

		
		


	}
}
