using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DAL;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment
{
	///<summary>QSPFulfillment Homepage.</summary>
	public partial class Home : QSPFulfillment.CommonWeb.QSPPage
	{
		protected string connString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];

		private HomePageDataAccess aHomePageDataAccess;

		public Home()
		{
			aHomePageDataAccess = new HomePageDataAccess();
		}

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			
		}
		
		private void InitializeComponent()
		{    

		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadNews();
				LoadDates();

				SetValueFMWelcome();
			}
		}

		private void LoadNews()
		{
			foreach(DataRow row in aHomePageDataAccess.GetHomePageItems(1).Rows)
			{
				AddNewsItem(row.ItemArray);
			}
		}

		private void AddNewsItem(object[] item)
		{
			bool redColor = item[2].ToString().Equals("red");
			tdNew.InnerHtml += "&nbsp;";
			if(redColor)
				tdNew.InnerHtml += "<img src=\"Images/rarrow_r1.gif\" width=\"9\" height=\"9\">&nbsp;<font color=\"red\">";
			else
				tdNew.InnerHtml += "<img src=\"Images/rarrow_b1.gif\" width=\"9\" height=\"9\">&nbsp;<font color=\"blue\">";
			tdNew.InnerHtml += item[0] + "</font>&nbsp;" + item[1] + "<br />";
		}

		private void LoadDates()
		{
			bool redColor = true;

			foreach(DataRow row in aHomePageDataAccess.GetHomePageItems(0).Rows)
			{
				AddDate(row.ItemArray, redColor);
				redColor = !redColor;
			}
		}

		private void AddDate(object []columns, bool redColor)
		{
			HtmlTableRow newRow = new HtmlTableRow();
			HtmlTableCell tdDate = new HtmlTableCell("td");
			HtmlTableCell tdInfo = new HtmlTableCell("td");
			HtmlTableCell tdSep = new HtmlTableCell("td");
			tdSep.InnerText = "-";

			//Format the date.  Day. DD/MM or DD/MM-DD/MM based on presence whether date is a range or one day.
			DateTime dStart = (DateTime)columns[0];
			tdDate.InnerText += dStart.Month.ToString() + "/" + dStart.Day.ToString();
			if(columns[1] is DBNull || columns[1].Equals(columns[0]))
			{
				tdDate.InnerText = dStart.DayOfWeek.ToString().Substring(0, 3) + ". " + tdDate.InnerText;
			}
			else
			{
				DateTime dEnd = (DateTime)columns[1];
				tdDate.InnerText += "-" + dEnd.Month.ToString() + "/" + dEnd.Day.ToString();
			}
			tdDate.NoWrap = true;

			//Easier than using <font>, add the style attribute.
			if(redColor)
				tdInfo.Style.Add("color", "red");
			else
				tdInfo.Style.Add("color", "blue");
			tdInfo.InnerHtml = (string)columns[2] + "<br />";

			//Add our new cells to the row.
			newRow.Cells.Add(tdDate);
			newRow.Cells.Add(tdSep);
			newRow.Cells.Add(tdInfo);
			tblDates.Rows.Add(newRow);

			redColor = !redColor;
		}

		private void SetValueFMWelcome() 
		{
			this.lblFMWelcome.Visible = QSPPage.aUserProfile.IsFM;
		}
	}
}
