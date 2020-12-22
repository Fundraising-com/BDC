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

namespace EFundraisingCRMWeb.Components.User
{
	/// <summary>
	/// Summary description for PickDay1.
	/// </summary>
	public partial class PickDay1 : EFundraisingCRMWebBasePage
	{
		public string TextFieldName;
		protected string script;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				SetCaption();
				try
				{
					SelectDateOnCalendar(Request["date"]);
				}
				catch(Exception)
				{
					SelectDateOnCalendar(DateTime.Today);
				}

				TextFieldName = Request["name"];
				ViewState["FieldName"] = TextFieldName;

				script = @"<script language='javascript'>
				if (document.frames != null){
					try{						
						var oitem = window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "Calendarframe") + @"');
						var obody = window.frames.parent.document.body;
						oitem.style.width = window.document.getElementById('cadre').offsetWidth+2; 
						oitem.style.height = window.document.getElementById('cadre').offsetHeight+2;
						if((oitem.style.posHeight+oitem.style.posTop)>obody.offsetHeight)
							oitem.style.posTop=(obody.offsetHeight-oitem.style.posHeight);
						}
						catch(err)
						{
						}
				}
				</script>";
				DataBind();

				BuildMonths();
				BuildYears();
			}
			else
				TextFieldName = (string)ViewState["FieldName"];
		}

		private void SetCaption()
		{
			TodayButton.Text = "Today";
			SelectButton.Text = "Select";
			ClearButton.Text = "Clear";
			CancelButton.Text = "Cancel";
		}

		private void BuildMonths()
		{
			string[] months = new string[12];

			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(HttpContext.Current.);

			for(int i=1; i<=12; i++)
			{
				months[i-1] = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetMonthName(i);
			}

			MonthDropDownList.DataSource = months;
			MonthDropDownList.DataBind();

			MonthDropDownList.Items[Calendar1.VisibleDate.Month-1].Selected = true;
		}

		private void BuildYears()
		{
			for(int i=1930; i<2100; i++)
			{
				YearDropDownList.Items.Add(i.ToString());
			}
			try
			{
				YearDropDownList.Items.FindByText(Calendar1.VisibleDate.Year.ToString()).Selected = true;
			}
			catch(Exception)
			{
				YearDropDownList.Items.FindByText(DateTime.Today.Year.ToString()).Selected = true;
			}
		}

		/// <summary>
		/// SEt the selected date on calendar
		/// </summary>
		/// <param name="date">date string in format YYYY/MM/DD</param>
		private void SelectDateOnCalendar(string date)
		{
			string[] arr = date.Split(new char[]{'/'});
			if(date != null && date != string.Empty)
				Calendar1.SelectedDate = new DateTime(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]));
			else
				Calendar1.SelectedDate = DateTime.Today;

			Calendar1.VisibleDate = Calendar1.SelectedDate;
		}

		private void SelectDateOnCalendar(DateTime date)
		{
			Calendar1.SelectedDate = date;
			Calendar1.VisibleDate = Calendar1.SelectedDate;
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
			this.Calendar1.VisibleMonthChanged += new System.Web.UI.WebControls.MonthChangedEventHandler(this.Calendar1_VisibleMonthChanged);

		}
		#endregion

		protected void Calendar1_SelectionChanged(object sender, System.EventArgs e)
		{
			this.script = @"<script language='javascript'>
					window.frames.parent.document.getElementById('" + TextFieldName + "').value = '" + PickDay.GetDateString(Calendar1.SelectedDate) + @"';
					window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "dateTimeHidden") + "').value = '" + 
				Calendar1.SelectedDate.Date.Year + "/" + Calendar1.SelectedDate.Date.Month + "/" + Calendar1.SelectedDate.Date.Day + @"';
					window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "Calendarframe") + @"').style.display = 'none'; 
		</script>";
			DataBind();
		}

		protected void TodayButton_Click(object sender, System.EventArgs e)
		{
			SelectDateOnCalendar(DateTime.Today);
			script = @"<script language='javascript'>
					window.frames.parent.document.getElementById('" + TextFieldName + "').value = '" + PickDay.GetDateString(Calendar1.SelectedDate) + @"';
					window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "dateTimeHidden") + "').value = '" + 
				Calendar1.SelectedDate.Date.Year + "/" + Calendar1.SelectedDate.Date.Month + "/" + Calendar1.SelectedDate.Date.Day + @"';
					window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "Calendarframe") + @"').style.display = 'none'; 
			</script>";
			DataBind();
		}

		protected void CancelButton_Click(object sender, System.EventArgs e)
		{
			script = @"<script language='javascript'>
					window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "Calendarframe") + @"').style.display = 'none'; 
		</script>";
			DataBind();
		}

		private void SelectButton_Click(object sender, System.EventArgs e)
		{
			script = @"<script language='javascript'>
					window.frames.parent.document.getElementById('" + TextFieldName + "').value = '" + PickDay.GetDateString(Calendar1.SelectedDate) + @"';
					window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "dateTimeHidden") + "').value = '" + 
				Calendar1.SelectedDate.Date.Year + "/" + Calendar1.SelectedDate.Date.Month + "/" + Calendar1.SelectedDate.Date.Day + @"';
					window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "Calendarframe") + @"').style.display = 'none'; 
		</script>";
			DataBind();
		}

		protected void MonthDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Calendar1.VisibleDate  = new DateTime(Calendar1.SelectedDate.Year, MonthDropDownList.SelectedIndex+1, 1);
			Calendar1.VisibleDate  = new DateTime(Calendar1.VisibleDate.Year, MonthDropDownList.SelectedIndex+1, 1);
		}

		protected void YearDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Calendar1.VisibleDate  = new DateTime(int.Parse(YearDropDownList.SelectedValue), Calendar1.VisibleDate.Month, 1);
		}

		private void Calendar1_VisibleMonthChanged(object sender, System.Web.UI.WebControls.MonthChangedEventArgs e)
		{
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CurrentEnActUser.Language);
			MonthDropDownList.SelectedIndex = Calendar1.VisibleDate.Month-1;

			YearDropDownList.SelectedItem.Selected = false;
			try
			{
				YearDropDownList.Items.FindByText(Calendar1.VisibleDate.Year.ToString()).Selected = true;
			}
			catch(Exception)
			{
				YearDropDownList.Items.FindByText(DateTime.Today.Year.ToString()).Selected = true;
			}
		}

		public string GetScript()
		{
			return script;
		}

		private void ClearButton_Click(object sender, System.EventArgs e)
		{
			script = @"<script language='javascript'>
					window.frames.parent.document.getElementById('" + TextFieldName + @"').value = '';
					window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "dateTimeHidden") + @"').value = '';
					window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "Calendarframe") + @"').style.display = 'none'; 
			</script>";
			DataBind();
		}
	}
}
