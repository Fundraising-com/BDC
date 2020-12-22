using System;
using efundraising.EFundraisingCRM;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using efundraising.Diagnostics;


namespace EFundraisingCRMWeb.Components.Server.DataGrid.Leads
{
	/// <summary>
	/// Summary description for LeadVisitsDataGrid.
	/// </summary>
	public class LeadVisitsDataGrid
	{
		public LeadVisitsDataGrid()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		//Buid a dataTable for displaying all the lad visits
		public static DataTable CreateDataTableLeadVisits(efundraising.EFundraisingCRM.LeadVisit[] leadVisits)
		{
			DataTable dt = new DataTable();
			try
			{
				// Build our data table for binding
				dt.Columns.Add("LeadID");
				dt.Columns.Add("Promo");
				dt.Columns.Add("Channel");
				dt.Columns.Add("Date");
					
				//for every visits
				for (int i=0; i< leadVisits.Length; i++)
				{
					DataRow dr = dt.NewRow();
					
					LeadVisit visit = leadVisits[i];
					dr["LeadID"] = visit.LeadID;
					//go get promo w/ promo_id
					Promotion p = Promotion.GetPromotionByID(visit.PromotionID); 
					if (p != null)
					{
						dr["Promo"] = p.Description;
					}
										
					dr["Channel"] = visit.ChannelCode;
										
					dr["Date"] = visit.VisitDate.ToShortDateString();
									
					dt.Rows.Add(dr);
				}
				
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in CreateDataTableProductPackage", ex);
			}

			return dt;
		}
	}
}
