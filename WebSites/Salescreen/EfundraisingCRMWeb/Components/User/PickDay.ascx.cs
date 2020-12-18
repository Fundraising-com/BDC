namespace EFundraisingCRMWeb.Components.User
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for PickDay.
	/// </summary>
	public partial class PickDay : System.Web.UI.UserControl
	{
	
		public static string DefaultDateFormat
		{
			get
			{
				return "yyyy-MM-dd";
			}
		}

		public void SetDateNotEmpty(bool bflag)
		{
			TextBox1RFV.Enabled = true;
		}

		public event EventHandler TextChanged;

		public string Text
		{
			get { return TextBox1.Text; }
			//set { TextBox1.Text = value; }
		}

		private DateTime _Date;

		public static string GetDateString (object data)
		{
			return GetDateString (data, DefaultDateFormat);
		}
		private static string GetDateString (object data, string selectedFormat)
		{
			string dateStr = "";
			if (data != null)
			{
				System.Data.SqlTypes.SqlDateTime sqlDate = System.Data.SqlTypes.SqlDateTime.Null;
				try
				{
					switch (data.GetType().ToString())
					{
						case "System.DateTime":
							sqlDate = new System.Data.SqlTypes.SqlDateTime((DateTime)data);
							break;
						case "System.Data.SqlTypes.SqlDateTime":
							sqlDate = (System.Data.SqlTypes.SqlDateTime)data;
							break;
						case "System.String":
							sqlDate = System.Data.SqlTypes.SqlDateTime.Parse((string) data);
							break;
						default:
							break;
					}
					if (!sqlDate.IsNull)
					{
						//string selectedFormat = CurrentEnActUser.Attributes[EnActUser.AttributNames.DefaultDateFormat].ToString();
						switch (selectedFormat)
						{
							case "M/d/yyyy":
							case "M/d/yy":
							case "MM/dd/yy":
							case "MM/dd/yyyy":
							case "yy/MM/dd":
							case "yyyy-MM-dd":
							case "dd-MMM-yy":
							case "dddd, MMMM dd, yyyy":
							case "MMMM dd, yyyy":
							case "dddd, dd MMMM, yyyy":
							case "dd MMMM, yyyy":
								dateStr = sqlDate.Value.ToString(selectedFormat);
								break;
							default:
								dateStr = sqlDate.Value.ToString("yyyy-MM-dd");
								break;
						}

						string[] arrWeekDays = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.DayNames;
						System.Collections.Hashtable weekdays = new System.Collections.Hashtable();
						weekdays.Add(DayOfWeek.Sunday, arrWeekDays[0]);
						weekdays.Add(DayOfWeek.Monday, arrWeekDays[1]);
						weekdays.Add(DayOfWeek.Tuesday, arrWeekDays[2]);
						weekdays.Add(DayOfWeek.Wednesday, arrWeekDays[3]);
						weekdays.Add(DayOfWeek.Thursday, arrWeekDays[4]);
						weekdays.Add(DayOfWeek.Friday, arrWeekDays[5]);
						weekdays.Add(DayOfWeek.Saturday, arrWeekDays[6]);

						string[] months = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames;
						string month = months[sqlDate.Value.Month-1];
						string day = (string)weekdays[sqlDate.Value.DayOfWeek];
						string[] arr = dateStr.Split(new char[]{' ', ','});
						switch (selectedFormat)
						{
							case "MMMM dd, yyyy":
								dateStr = month + " " + arr[1] + ", " + arr[3];
								break;
							case "dddd, MMMM dd, yyyy":
								dateStr = day + ", " + month + " " + arr[3] + ", " + arr[5];
								break;
							case "dddd, dd MMMM, yyyy":
								dateStr = day + ", " + arr[2] + " " + month + ", " + arr[5];
								break;
							case "dd MMMM, yyyy":
								dateStr = arr[0] + " " + month + ", " + arr[3];
								break;
							case "dd-MMM-yy":
								arr = dateStr.Split(new char[] {'-'});
								months = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
								month = months[sqlDate.Value.Month-1];
								dateStr = arr[0] + "-" + month + "-" + arr[2];
								break;
							default:
								break;
						}
					}
				}
				catch
				{
				}
			}
			return dateStr;
		}

		public DateTime Date
		{
			get 
			{
				//return this._Date;
				if (dateTimeHidden.Value == "")
				{
					return DateTime.MinValue;
				}
				else
				{
				   return Convert.ToDateTime(dateTimeHidden.Value);	
				}
				
			}
			set
			{
				this._Date = value;
				if (value.Equals(DateTime.MinValue))
					dateTimeHidden.Value = "";
				else
					dateTimeHidden.Value = value.Year.ToString() + "/" + value.Month.ToString() + "/" + value.Day.ToString();
				TextBox1.Text = GetDateString(value);
			}
		}

		private bool _ReadOnly;
		public bool ReadOnly
		{
			get { return _ReadOnly; }
			set 
			{
				_ReadOnly = value;
				if (value)
				{
					TextBox1.ReadOnly = true;
					calendarImage.Visible = false;
					imgTD.Width = "0";
				}
				else
				{
					TextBox1.ReadOnly = false;
					imgTD.Width = "20";
					calendarImage.Visible = true;
				}
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void TextBox1_TextChanged(object sender, System.EventArgs e)
		{
			if (dateTimeHidden.Value == null || dateTimeHidden.Value.Trim() == "")
				this._Date = DateTime.MinValue;
			else
			{
				string[] arr = dateTimeHidden.Value.Split(new char[]{'/'});   //format: YYYY/MM/DD
				this._Date = new DateTime(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]));
			}
			if(TextChanged != null)
				TextChanged(sender, e);
		}

		protected override void OnPreRender(EventArgs e)
		{
			
			this.Page.RegisterStartupScript("VarPickdayVariables",string.Format("<script>var thePath='{0}';</script>", 
				(this.Page as EFundraisingWebBasePage).appPath ));
			base.OnPreRender (e);
		}

	}
}
