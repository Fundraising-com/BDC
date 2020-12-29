using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using GA.BDC.Core.EnterpriseComponents;

namespace GA.BDC.Core.Web.UI.InputControls
{
	/*
	protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
	protected System.Web.UI.WebControls.RangeValidator RangeValidator1;
	protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
	*/

	/// <summary>
	/// Summary description for DecimalTextBox.
	/// </summary>
	[DesignerAttribute(typeof(DateTextBoxDesigner), typeof(IDesigner))]
	[DefaultEvent("Click")]
	public class DateTextBox : BaseInputControl {
		public DateTextBox() {
			
		}

		private void SetCalendarStyle(Calendar calendar)
		{
			if (calendar == null)
				return;
			calendar.CssClass = "Calendar";
			calendar.BorderColor = Color.White;
			calendar.ShowGridLines = true;

			calendar.DayHeaderStyle.CssClass = "DayHeaderStyle";
//			calendar.DayHeaderStyle.BackColor = Color.FromArgb(Convert.ToInt32(byte.Parse("AB", System.Globalization.NumberStyles.HexNumber)),
//				Convert.ToInt32(byte.Parse("C0", System.Globalization.NumberStyles.HexNumber)),
//				Convert.ToInt32(byte.Parse("E7", System.Globalization.NumberStyles.HexNumber)));
			calendar.DayStyle.CssClass = "DayStyle";
			calendar.TodayDayStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
			calendar.NextPrevStyle.ForeColor = Color.White;
//			calendar.TitleStyle.Font.Size = new System.Web.UI.WebControls.FontUnit("11px");
//			calendar.TitleStyle.Font.Bold = true;
//			calendar.TitleStyle.ForeColor = Color.White;
			calendar.TitleStyle.CssClass = "TitleStyle";

			
		}

		protected override void LoadControl(object sender, EventArgs e) {
			base.LoadControl (sender, e);

			Calendar calendar = new Calendar();
			calendar.ID = ID + "MyCalendar";
			calendar.SelectionChanged += new EventHandler(calendar_SelectionChanged);
			calendar.VisibleMonthChanged += new MonthChangedEventHandler(calendar_VisibleMonthChanged);
			SetCalendarStyle(calendar);
			if(Text != null) {
                if (Text != "") {
                    DateTime date = DateTime.Now;
                    try {
                        date = DateTime.Parse(Text);
                    } catch { }
                    calendar.SelectedDate = date;
                }
			}
			
			HtmlInputButton inputButton = new HtmlInputButton();
			inputButton.Value = "Cancel";
			inputButton.Attributes.Add("class", "calendarText");

			HtmlInputText inputText = new HtmlInputText();
			inputText.Value = " ";
			inputText.Attributes.Add("class", "calendarText");


			HtmlGenericControl calDiv =
				new HtmlGenericControl("div");
			calDiv.Controls.Add(calendar);
			calDiv.Controls.Add(inputText);
			calDiv.Controls.Add(inputButton);

			calDiv.Attributes.Add("style", "DISPLAY: none; VISIBILITY: hidden;");
			calDiv.Attributes.Add("id", "CalendarDiv" + ID);

			Controls.Add(calDiv);
			string javascript = 
				"<script language=\"javascript\">\r\n" +
				"function showCalendar" + ID + "() {\r\n" +
				"	document.getElementById('CalendarDiv" + ID + "').style.visibility = 'visible';\r\n" +
				"	document.getElementById('CalendarDiv" + ID + "').style.display = 'block'; \r\n" +
				"}\r\n" +
				"</script>\r\n";

			Page.RegisterClientScriptBlock("scriptz" + ID, javascript);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "scriptz", javascript);

			TextBox textBox = (TextBox)FindControl("InnertTextBox");
			textBox.Attributes.Add("onfocus", "showCalendar" + ID + "()");
			string tmp = "	document.getElementById('CalendarDiv" + ID + "').style.visibility = 'hidden';document.getElementById('CalendarDiv" + ID + "').style.display = 'none';";
			inputButton.Attributes.Add("onclick", tmp);
		}

		private void calendar_SelectionChanged(object sender, EventArgs e) {
			Calendar cal = (Calendar)FindControl(ID + "MyCalendar");
			innerTextBox.Text = cal.SelectedDate.ToLongDateString();
		}

		private void calendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e) {
			string javascript = 
				"<script language=\"javascript\">\r\n" +
				"if (typeof window.onload != 'function') {	// firefox\r\n" +
				"	window.onload = showCalendar" + ID + ";\r\n" +
				"} else {	// ie\r\n" +
				"	document.body.onload = function() {\r\n" +
				"		showCalendar" + ID + "();\r\n" +
				"	}\r\n" +
				"}\r\n" +
				"</script>\r\n";

			Page.RegisterClientScriptBlock("scriptzx" + ID, javascript);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "scriptzx", javascript);
		}

	}

	/// <summary>
	/// This class is used by the Visual Studio at Design Time.
	/// You can set control properties behavior in the ControlDesigner class.
	/// </summary>
	public class DateTextBoxDesigner : ControlDesigner {
		public DateTextBoxDesigner() {

		}

		// return the html display we want the UI control to see at design time
		public override string GetDesignTimeHtml() {
			return "<input type=\"text\" value=\"" + ID + "\" />";
		}
	}
}
