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
using Common;

namespace QSPFulfillment.Admin
{
	///<summary>base page for personnel directory lookups</summary>
	public class pdirPage : QSPFulfillment.CommonWeb.QSPPage
	{
		///<summary>default constructor</summary>
		public pdirPage(){}
	
		#region item declarations
		protected HtmlInputText		FirstName;
		protected HtmlInputText		LastName;
		protected HtmlInputButton		Reset1;
		protected HtmlInputButton		Submit1;
		protected Repeater				Repeater1;
		protected Label				EmptySearch_lbl;
		protected Label				email_lbl;
		protected CheckBox				displaytime;
		protected Label				BadSearch_lbl;
		#endregion item declarations

		#region Page Load stuff
		///<summary>
		/// set labels and repeater off, turn on later in correct scenarios
		/// then do search as long as it isnt the first load of the page.
		///</summary>
		///<param name="mode"></param>
		protected void BASE_Page_Load(string mode)
		{
			//string FMID = getFMID(); //if this fails, the user wasn't logged in properly
			string FMID = aUserProfile.FMID;
			EmptySearch_lbl.Visible = false;
			email_lbl.Visible = false;
			BadSearch_lbl.Visible = false;
			Repeater1.Visible = false;
			if(Page.IsPostBack) { BindRepeater(mode); }
		}
		#endregion Page Load stuff

		#region repeater stuff
		///<summary>grab data from DB, hook into page</summary>
		///<param name="mode"></param>
		private void BindRepeater(string mode)
		{
			if ((Request.Form["FirstName"] == "") &&
				(Request.Form["LastName"] == "") ) 
			{
				EmptySearch_lbl.Visible = true;
				return;
			}
			email_lbl.Visible = true;

			string first = Request.Form["FirstName"].ToString().Trim();
			string last  = Request.Form["LastName"].ToString().Trim();

			Response.Write(first + " " + last + " NAME!!");

			try
			{
				DAL.PersonnelDirectoryDataAccess pdirDAL = new DAL.PersonnelDirectoryDataAccess();
				//DataTable dt = pdirDAL.GetDirectoryData(mode,first, last);
				DataTable dt = null;
				if(mode == "Personnel") 
				{ 
					dt = pdirDAL.GetPersonnelDirectoryData(first,last);
				} 
				else if(mode == "Meridian") 
				{ 
					dt = pdirDAL.GetMeridianDirectoryData(first,last);
				}
				else 
				{ 
					string errorStr = "Unrecognized PersonnelDirectoryDataAccess mode";
					throw new ArgumentException(errorStr, mode); 
				}
				
				if (dt.Rows.Count < 1) 
				{
					Repeater1.Visible = false;
					BadSearch_lbl.Visible = true;
					email_lbl.Visible = false;
				}
				else
				{
					Repeater1.Visible = true;
					Repeater1.DataSource = dt;
					Repeater1.DataBind();
				}
			}
			catch(Exception ex)
			{
				Response.Write( "<span style=\"error\">Error getting the data</span><hr />" );
				Response.Write("Error: " + ex.Message + "<hr />");
				Repeater1.Visible = false;
			}
		}

		
		/// <summary>
		/// converts the TimeZone code to the actual time in that zone, 
		/// by calling theirTime() 
		/// </summary>
		/// <param name="src"></param>
		/// <param name="e"></param>
		protected void Repeater1_ItemDataBound(object src, RepeaterItemEventArgs e)
		{
			System.Web.UI.WebControls.Label         time = new Label();
			System.Web.UI.HtmlControls.HtmlTableRow row  = new HtmlTableRow();
			System.Web.UI.WebControls.Literal       dst  = new Literal();
			
			if(e.Item.ItemType == ListItemType.Item)
			{
				if(displaytime.Checked == true)
				{
					time = (Label) e.Item.FindControl("time");
					dst = (Literal) e.Item.FindControl("lt_dst"); 
					try   { time.Text = theirTime(Convert.ToInt16(time.Text), Convert.ToBoolean(dst.Text)); }
					catch { time.Text = "Error computing listing's localtime"; }
					dst.Text = "";
				}
				else
				{
					row  = (HtmlTableRow) e.Item.FindControl("timerow");
					row.Visible = false;
				}
			
				
			}
			else if (e.Item.ItemType == ListItemType.AlternatingItem )
			{
				if(displaytime.Checked == true)
				{
					time = (Label) e.Item.FindControl("time_A");
					dst = (Literal) e.Item.FindControl("lt_dst_A"); 
					try   { time.Text = theirTime(Convert.ToInt16(time.Text), Convert.ToBoolean(dst.Text)); }
					catch { time.Text = "Error computing listing's localtime"; }
					dst.Text = "";
				}
				else
				{
					row  = (HtmlTableRow) e.Item.FindControl("timerow_A");
					row.Visible = false;
				}
			}
		}
		#endregion repeater stuff

		#region date time stuff
		/// <summary>localtime and localzone wrapper</summary>
		/// <remarks>ASSUMES IT IS RUNNING ON A GMT-5 (EST), DST observant SERVER.</remarks>
		/// <param name="zone">int: input Timezone, hours behind GMT</param>
		/// <param name="DST">bool: Area observes Day Light Savings ?</param>
		/// <returns>Text string with the local time</returns>
		private string theirTime(int zone, bool DST)
		{
			System.DateTime NOW = new DateTime();
			//NOW = DateTime.Now;
			NOW = QSPDateTime.localtime(zone, DST, DateTime.Now);
			return NOW.ToString("t") + " " + QSPDateTime.localzone(zone, DST, NOW);
		}
		#endregion date time stuff
	}
}
