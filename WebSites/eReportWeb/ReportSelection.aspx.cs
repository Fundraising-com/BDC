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

using efundraising.eReport;

namespace efundraising.eReportWeb
{
	/// <summary>
	/// Summary description for Reports.
	/// </summary>
	public partial class ReportSelection : eReportWebBasePage
	{
		// LabelLogin should be removed when created by VS IDE

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				//ReportCollection reports = Report.LoadReports();

				//foreach (Report report in reports)
				//	ReportsListBox.Items.Add(new ListItem(report.Label, report.ReportID.ToString()));

				ReportCollection reports = GetReports();
				if (reports != null)
				{
					int i = 0;
					foreach (Report report in reports)
					{
						i++;
						string reportID = report.ReportID.ToString();
						ReportsListBox.Items.Add(new ListItem(report.Label, report.ReportID.ToString()));
					}

					if (ReportsListBox != null && ReportsListBox.Items.Count > 0)
						ReportsListBox.SelectedIndex = 0;
				}
			}
		}

		private ReportCollection GetReports()
		{

			return Report.LoadReports();

			//if (userLogin != null)
			//{
			//	if (Components.Server.Config.IsExternal)
			//		return efundraising.eReport.Report.GetReportsByUserName(userLogin.UserName);
			//	else
			//	{
			//		Hashtable hashTmp = new Hashtable();
			//		ReportCollection result = new ReportCollection();
			//		if (userLogin.Roles != null && userLogin.Roles.Length > 0)
			//		{
			//			for (int i= 0; i < userLogin.Roles.Length; i++)
			//			{
			//				string grpName = userLogin.Roles[i].Trim();
			//				if (grpName != string.Empty)
			//				{
			//					Group grp = Group.GetGroupByName(grpName);
			//					if (grp != null)
			//					{
			//						string stmp = Global.GroupAccessAllReports();
			//						if (grp.GroupId == 0 || grp.IsGroupInList(stmp))
			//						{
			//							return Report.LoadReports();
			//						}
			//						ReportCollection rptCol = null;
			//						rptCol = Report.GetReportsByGroupName(grpName, Session);
			//						for (int k=0; k < rptCol.Count; k++)
			//						{
			//							if (hashTmp[rptCol[k].ReportID] == null)
			//							{
			//								hashTmp[rptCol[k].ReportID] = rptCol[k].ReportID;
			//								result.Add(rptCol[k]);
			//							}
			//						}
			//					}
			//				}
			//			}
			//		}
			//		return result;
			//	}
			//}
			//else
			//{
			//	return null;
			//}
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

		}
		#endregion

		protected void ViewReportButton_Click(object sender, System.EventArgs e)
		{
			// Access report by ID
			if(ReportsListBox.SelectedItem != null)
			{
				string reportID = ReportsListBox.SelectedValue;
				Response.Redirect("ReportSummary.aspx?rid=" + reportID);
			}
		}
	}
}
