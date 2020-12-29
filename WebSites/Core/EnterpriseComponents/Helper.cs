using System;
using System.IO;
using System.Web.Mail;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;


namespace GA.BDC.Core.EnterpriseComponents {
	/// <summary>
	/// Summary description for Helper.
	/// </summary>
	public class Helper {

		#region Constructor
		public Helper() {
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region Integer Validation Functions
		/// <summary>
		/// return true if integer, false if null or contains alpha value
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsInteger(string strInteger) {

			try 
			{
				int.Parse(strInteger);
			} 
			catch 
			{
				return false;
			}

			return true;
			/*
			if(strInteger == null)
				return false;

			if(strInteger == string.Empty)
				return false;

			// checks if there something else than 0 to 9 and the minus sign
			Regex containsAlpha = new Regex("-[^0-9]");
			// checks if theres a number inserted
			Regex containsNumeric = new Regex("0*[0-9][0-9]*");
			
			// return the match found
			// checks for non alpha and numeric in case we miss special characters
			return (!containsAlpha.IsMatch(strInteger) &&
				containsNumeric.IsMatch(strInteger));
				*/
		}

		#endregion Integer Functions

		#region Miscellaneous Validation Functions

		/// <summary>
		/// Checks if an email has the required specs.
		/// </summary>
		/// <param name="emailAddress"></param>
		/// <returns></returns>
		public static bool IsValidEmail(string emailAddress) { 
			Regex regEx = new Regex(@"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z0-9]{1,6}$");
			return regEx.IsMatch(emailAddress);
		}

		/// <summary>
		/// Get a value from the WebConfig.
		/// </summary>
		/// <param name="webConfigValue">Attribute Name</param>
		/// <returns>The Web Config Value or string.Empty if not set</returns>
		public static string GetWebConfigValue(string webConfigAttribute) {
			string val = "";
			try {
                val = System.Web.Configuration.WebConfigurationManager.AppSettings[webConfigAttribute].Trim();//ConfigurationSettings.AppSettings[webConfigAttribute];
            } catch {}
            return val;
		}

       //check if a string of character is numeric
		public static bool IsNumeric(object value) { 
			try { 
				double d = System.Double.Parse(value.ToString(), System.Globalization.NumberStyles.Any); 
				return true; 
			} 
			catch (FormatException) { 
				return false; 
			} 
		}

		#endregion

		#region File Functions
		/// <summary>
		/// Append text to a file
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="message">The message to insert into the file</param>
		public static void AppendToFile(string fileName, string message) {
			FileStream logSystemFile = null;
			StreamWriter streamWriterLogSystem = null;

			try {
				logSystemFile = new FileStream(fileName, System.IO.FileMode.Append);
				streamWriterLogSystem = new StreamWriter(logSystemFile);
				streamWriterLogSystem.WriteLine(message);
				streamWriterLogSystem.Close();
				logSystemFile.Close();
			} catch(System.Exception ex) {
				throw(ex);
			}
		}
		#endregion

        #region Other Colorful Functions

		//when the user presses enter, the submit button is invoked  //vines
		public static void SetOnKeyPressBehavior(TextBox txt, String SubmitCtrlRef) {
			String strJavaScript = "if (event.keyCode == 13){ window.document.all('" + SubmitCtrlRef +"').click();return false;}";
			txt.Attributes.Add("onKeyPress", strJavaScript);	
		
		}

		//format in currency  
		public static string FormatCurrency(object num) {
			Decimal mynum = Convert.ToDecimal(num);
			System.Globalization.NumberFormatInfo fmt = new System.Globalization.NumberFormatInfo(); 
			fmt.CurrencyDecimalSeparator = "."; 
			fmt.CurrencyGroupSeparator ="," ; 
			fmt.CurrencySymbol ="$"; 
			string someCurrency = String.Format(fmt, "{0:c}", mynum); 
			return someCurrency; 
		} 

      

		public static int BusinessDateDiff(DateTime startDate, DateTime endDate){		
		
			int incr;
			int diff = 0;
			// incr can be +1 or -1
			if (startDate < endDate){
				incr = 1;
			}else{
				incr = -1;
			}
    
			while(startDate < endDate){
				// skip to previous or next day
				startDate = startDate.AddDays(incr);
				if (startDate.DayOfWeek != DayOfWeek.Sunday && (startDate.DayOfWeek != DayOfWeek.Saturday)) {
					// if it's a weekday add/subtract one to the result
					diff  = diff + incr;
				}
			}
			// when the loop is exited the function name contains the correct result     
		return (diff);
		}


		public static DateTime AddBusinessDays(DateTime startDate, int businessDays){		
		
		   int holyDays = 0, weekDays = 0;
		   DateTime dateTemp = startDate;

			while(weekDays < businessDays){
				// skip to the next day
			    dateTemp = dateTemp.AddDays(1);
				if (dateTemp.DayOfWeek == DayOfWeek.Sunday || (dateTemp.DayOfWeek == DayOfWeek.Saturday)) {
					holyDays = holyDays + 1;
				}else{
					weekDays = weekDays + 1;
				}

			}
			
			startDate = startDate.AddDays(holyDays + weekDays);
			return (startDate);
		}

		
		//** US - CA - ALL **//
		public static void GetStateList(DropDownList cboState, string state)
		{
			try 
			{
				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@strCountryCode";
				parameters[0].DataType = DbType.String;
				parameters[0].ParamDirection = ParameterDirection.Input;
				if (state != "ALL")
				{
					parameters[0].Value = state;
				}
				else
				{
					parameters[0].Value = DBNull.Value;
				}
				

				DatabaseInterface dbi = new DatabaseInterface(); 

				DataTable dt = dbi.ExecuteFetchDataTable("get_all_states_provinces", CommandType.StoredProcedure,parameters); 
		
				cboState.DataSource = dt;
				cboState.DataTextField = "state_code";
				cboState.DataValueField = "state_code";
				cboState.DataBind();

			}
			catch(Exception e) 
			{
				//Debug.Write(e.Message.ToString());
			}

		
		}
		public static bool IsDate(String date)
		{
			try
			{
				DateTime dt = Convert.ToDateTime(date);
				return true;
			}
			catch(Exception e)
			{
				return false;   
			}
		}

		//if null -- returns strings
		public static string IFNULL_S(Object obj, string replaceBy)
		{
		
			if (obj == DBNull.Value)
			{
				return replaceBy;
			}
			else
			{
				return obj.ToString();
			}
		}
	
  #endregion

		#region Color Methods
		/// <summary>
		/// Convert a hex string to a .NET Color object.
		/// </summary>
		/// <param name="hexColor">a hex string: "FFFFFF", "#000000"</param>
		public static Color HexStringToColor(string hexColor) {
			string hc = ExtractHexDigits(hexColor);
			if (hc.Length != 6) {
				// you can choose whether to throw an exception
				//throw new ArgumentException("hexColor is not exactly 6 digits.");
				return Color.Empty;
			}
			string r = hc.Substring(0, 2);
			string g = hc.Substring(2, 2);
			string b = hc.Substring(4, 2);
			Color color = Color.Empty;
			try {
				int ri 
					= Int32.Parse(r, System.Globalization.NumberStyles.HexNumber);
				int gi 
					= Int32.Parse(g, System.Globalization.NumberStyles.HexNumber);
				int bi 
					= Int32.Parse(b, System.Globalization.NumberStyles.HexNumber);
				color = Color.FromArgb(ri, gi, bi);
			}
			catch {
				// you can choose whether to throw an exception
				//throw new ArgumentException("Conversion failed.");
				return Color.Empty;
			}
			return color;
		}
		/// <summary>
		/// Extract only the hex digits from a string.
		/// </summary>
		public static string ExtractHexDigits(string input) {
			// remove any characters that are not digits (like #)
			Regex isHexDigit 
				= new Regex("[abcdefABCDEF\\d]+", RegexOptions.Compiled);
			string newnum = "";
			foreach (char c in input) {
				if (isHexDigit.IsMatch(c.ToString()))
					newnum += c.ToString();
			}
			return newnum;
		}
		#endregion

	}
	
	}
