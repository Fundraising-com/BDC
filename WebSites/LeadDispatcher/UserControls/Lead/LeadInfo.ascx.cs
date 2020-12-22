namespace CRMWeb.UserControls.Lead
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using GA.BDC.Core.EnterpriseComponents;

	/// <summary>
	///		Summary description for LeadInfo1.
	/// </summary>
	public partial class LeadInfo1 : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		
			
		

			
		}


		public void Refresh(){
			int leadID = 0;
			try{
				
				if (Session[Global.SessionVariables.CURRENT_LEAD_ID] != null){
					leadID = Convert.ToInt32(Session[Global.SessionVariables.CURRENT_LEAD_ID]);
			
			
					DataTable dt = DatabaseObjects.GetLeadInfo(leadID);
					if (dt.Rows.Count > 0){
				
						//lead
						lblFirstName.Text = IFNULL_S(dt.Rows[0]["first_name"],"");
						lblLastName.Text = IFNULL_S(dt.Rows[0]["last_name"],"");
						lblTitle.Text = IFNULL_S(dt.Rows[0]["title"],"");
						lblSalutation.Text = IFNULL_S(dt.Rows[0]["salutation"],"");
						lblGroupType.Text = IFNULL_S(dt.Rows[0]["group_type"],"");
						lblGroupWebSite.Text = IFNULL_S(dt.Rows[0]["group_web_site"],"");
						lblGroupWebSite.NavigateUrl = FixUrl(IFNULL_S(dt.Rows[0]["group_web_site"],""));
						lblOrg2.Text = IFNULL_S(dt.Rows[0]["organization"],"");
						lblOrgType.Text = IFNULL_S(dt.Rows[0]["org_type"],"");
						lblLeadID.Text = IFNULL_S(dt.Rows[0]["lead_id"],"");
				
						//address
				
						lblState.Text = IFNULL_S(dt.Rows[0]["state"],"");
						lblZip.Text = IFNULL_S(dt.Rows[0]["zip_code"],"");
						lblAddress.Text = IFNULL_S(dt.Rows[0]["street_address"],"");
						lblCity.Text = IFNULL_S(dt.Rows[0]["city"],"");
						lblCountry.Text = IFNULL_S(dt.Rows[0]["country"],"");
				
						//phone
						lblOtherPhone.Text = IFNULL_S(dt.Rows[0]["other_phone"],"");
						lblDayPhone.Text = IFNULL_S(dt.Rows[0]["day_phone"],"") + " " + IFNULL_S(dt.Rows[0]["day_phone_ext"],"");
						lblEvePhone.Text = IFNULL_S(dt.Rows[0]["evening_phone"],"") + " " + IFNULL_S(dt.Rows[0]["evening_phone_ext"],"");
						lblFax.Text = IFNULL_S(dt.Rows[0]["fax"],"");
						lblBestTime.Text = IFNULL_S(dt.Rows[0]["best_time_to_call"],"");
						lblEmail.Text = IFNULL_S(dt.Rows[0]["email"],"");
				
						//tracking
				
						lblAssignmentDate.Text = IFNULL_D(dt.Rows[0]["lead_assignment_date"],"");
						lblChannel.Text = IFNULL_S(dt.Rows[0]["channel2"],"");
						lblConsultant.Text = IFNULL_S(dt.Rows[0]["consultant"],"");
						lblFM.Text = IFNULL_S(dt.Rows[0]["FM"],"");
						lblFromWebSite.Text = IFNULL_S(dt.Rows[0]["from_web_site"],"");
						lblFromWebSite.NavigateUrl = FixUrl(IFNULL_S(dt.Rows[0]["from_web_site"],""));
						lblPromotion.Text = IFNULL_S(dt.Rows[0]["promotion"],"");
						lblPromoType.Text = IFNULL_S(dt.Rows[0]["promotion_type"],"");
						lblEntryDate.Text = IFNULL_D(dt.Rows[0]["lead_entry_date"],"");
						lblStatus.Text = IFNULL_S(dt.Rows[0]["status"],"");
						lblAssigner.Text = IFNULL_S(dt.Rows[0]["assigner"],"");

						//campaign
						lblNbPart.Text = IFNULL_S(dt.Rows[0]["participant_count"],"");
						lblReason.Text = IFNULL_S(dt.Rows[0]["campaign_reason"],"");
						lblHeard.Text = IFNULL_S(dt.Rows[0]["hear_about_us"],"");
						lblGoal.Text = Helper.FormatCurrency(IFNULL_S(dt.Rows[0]["fund_raising_goal"],"0.00")); // added by Jason IT (APR 2016) fix formatting error
						lblStartDate.Text = IFNULL_D(dt.Rows[0]["fund_raiser_start_date"],"");
						lblkit.Text = IFNULL_S(dt.Rows[0]["kit_type"],"");

						Classes.LeadInfo_Basic li = new Classes.LeadInfo_Basic();
						li.LEAD_NAME = IFNULL_S(dt.Rows[0]["first_name"],"") + " " +  IFNULL_S(dt.Rows[0]["last_name"],"");
						li.GROUP_NAME = IFNULL_S(dt.Rows[0]["organization"],"");
						li.PHONE = IFNULL_S(dt.Rows[0]["day_phone"],"") + " " + IFNULL_S(dt.Rows[0]["day_phone_ext"],"");
						li.EMAIL = IFNULL_S(dt.Rows[0]["email"],"");
						Session[Global.SessionVariables.LEAD_INFO] = li;
			
						dt = DatabaseObjects.GetLeadComments(leadID);
						
						dgComments.DataSource = dt;
						dgComments.DataBind();
						


						

					}
				}
			}catch(Exception ex){
				throw new Global.CRMException("Lead:" + leadID,ex,0,"LeadInfo.Refresh");
			}

		}

		private string FixUrl(string url){
			try{
				if (url != "" && url.Length > 4){
					if (url.Substring(0,4) != "http"){
						url = "http://" + url;
					}
				}
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"LeadInfo.FixUrl");
			}
			return url;
		}

		//if null -- returns strings
		private string IFNULL_S(Object obj, string replaceBy){
		
			if (obj == DBNull.Value){
				return replaceBy;
			}else{
				return obj.ToString();
			}
		}

		private string IFNULL_D(Object obj, string replaceBy){
		
			if (obj == DBNull.Value){
				return replaceBy;
			}else{
				return Convert.ToDateTime(obj).ToShortDateString();
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

		}
		#endregion
	}
}
