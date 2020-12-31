using System;
using System.Data;

namespace QSP.WebControl.DataAccess.Common
{
	/// <summary>
	/// Summary description for QCAPMessage.
	/// This class i used to Manage all Message for exception, including
	/// the validation message that we need to provide to the presentation Layer
	/// </summary>
	internal class Message
	{
		public const String VALMSG_GENERAL_VAR_0 = "Some fields are invalid, the operation cannot be performed.";
		public const String VALMSG_REQUIRED_FIELD_VAR_1 = "The field [var1] is mandatory.";
		public const String VALMSG_MAX_LENGTH_VAR_2 = "Invalid Length for the field [var1], it must be [var2] character(s).";
		public const String VALMSG_INTEGRITY_VAR_2 = "The delete operation cannot be performed, because the [var1] is associated with at least one [var2].";
		public const String VALMSG_UNICITY_VAR_2 = "[var1] already exist for this [var2].";
		public const String VALMSG_HEADER_TEXT_VAR_0 = "Error Summary, please correct the following error(s):"; 
		public const String SYSMSG_HEADER_TEXT_VAR_0 = "System error"; 
		public const String IMPORT_GENERAL_ERR_MSG_VAR_0 = "An error occured during the Import, please verify the requirements.";
		public const String IMPORT_REQUIRED_FIELDVAR_1 = "The Import file doesn't contains the column(s) : [var1]. ";
		public const String IMPORT_EMPTY_SHEET_VAR_0 = "The Import file doesn't contains any row.";
		public const String IMPORT_MAX_OVER_VAR_2 = "The importation cannot be performed cause the number of [var1] that the system have to insert " +
			"and the number of [var1] already defined in the current campaign will go over the maximum of " + 
			"[var2] for each campaign.";
		
		public const String ERRMSG_NO_REC_AFF_VAR_0 = "The modification have not been made.  Probably there is someone who have delete this record before. " +
			"Please refresh the list to verify the presence of this record.";
		public const string ERRMSG_SYSTEM_VAR_0 = "The server is unable to provide the requested information. Please retry.";
		public const string DATA_ADMIN_CLEAR_VAR_2 = "To be able to clear the Table [var1], you must clear also the folowing tables: [var2]";
		public const string ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0 = "Please provide at least one field.";
		public const string ERRMSG_SEARCH_PROVIDE_FROM_TO_DATE_0 = "Please provide From and To date.";
		public const string ERRMSG_INSTANCE_DO_NOT_EXIST_1 = "The [var1] that you have entered is invalid.";
		private String errorMessage = "";
		private String errorHTMLMessage = "";
		private ExceptionType exType = ExceptionType.Unknown;
		
		private bool bIsMessageBox = false; 
		
		private string NewLine = "<br>";

		private string UnorderList = "<UL>";
		private string ListItem = "<LI>";
		private string ListItemEnd = "</LI>";
		private string UnorderListEnd = "</UL>";
		private System.Collections.ArrayList alErrorMsg = new System.Collections.ArrayList();

		public Message(bool IsMessageBox)
		{
			bIsMessageBox = IsMessageBox;
		}

		public String ErrorMessage
		{
			get
			{
				return errorMessage;
			}
						
		}
		

		public String ErrorHTMLMessage
		{
			get
			{
				return errorHTMLMessage;
			}
			
		}

		public ExceptionType ValidationExceptionType
		{
			get
			{
				return exType;
			}
			set
			{
				exType = value;
			}
		
		}


		public void SetErrorMessage(DataTable dtTable)
		{
			
			String TextErrorMessage = "";
			String HTMLErrorMessage = "";
			//Init the error message by the header
			HTMLErrorMessage = VALMSG_HEADER_TEXT_VAR_0 + NewLine;
			TextErrorMessage = VALMSG_HEADER_TEXT_VAR_0 + NewLine;

			if (dtTable.HasErrors)
			{				
				HTMLErrorMessage = HTMLErrorMessage + UnorderList;
				TextErrorMessage = TextErrorMessage + " /n ";
				foreach(DataRow row in dtTable.GetErrors())
				{		
					foreach(DataColumn dc in row.GetColumnsInError())
					{
						HTMLErrorMessage = HTMLErrorMessage + ListItem + row.GetColumnError(dc) + ListItemEnd + NewLine;;
						TextErrorMessage = TextErrorMessage + "-" + row.GetColumnError(dc) + NewLine;
						
					}
				}
				HTMLErrorMessage = HTMLErrorMessage + UnorderListEnd;	
			}

			errorMessage = TextErrorMessage;
			errorHTMLMessage = HTMLErrorMessage;
			
		
		}
		
		public void SetErrorMessage(String ErrorMessage)
		{
			
			String TextErrorMessage = "";
			String HTMLErrorMessage = "";
			//Init the error message by the header
			HTMLErrorMessage = VALMSG_HEADER_TEXT_VAR_0 + NewLine;
			TextErrorMessage = VALMSG_HEADER_TEXT_VAR_0 + "/n";

			HTMLErrorMessage = HTMLErrorMessage + UnorderList;
			HTMLErrorMessage = HTMLErrorMessage + ListItem + ErrorMessage + ListItemEnd;
			HTMLErrorMessage = HTMLErrorMessage + UnorderListEnd;	

			TextErrorMessage = TextErrorMessage + "-" + ErrorMessage + NewLine;

			errorMessage = TextErrorMessage;
			errorHTMLMessage = HTMLErrorMessage;
		
		}

		public void SetSystemErrorMessage(String ErrorMessage)
		{
			
			String TextErrorMessage = "";
			String HTMLErrorMessage = "";
			//Init the error message by the header
			HTMLErrorMessage = SYSMSG_HEADER_TEXT_VAR_0 + NewLine;
			TextErrorMessage = SYSMSG_HEADER_TEXT_VAR_0 + "/n";

			HTMLErrorMessage = HTMLErrorMessage + UnorderList;
			HTMLErrorMessage = HTMLErrorMessage + ListItem + ErrorMessage + ListItemEnd;
			HTMLErrorMessage = HTMLErrorMessage + UnorderListEnd;	

			TextErrorMessage = TextErrorMessage + "-" + ErrorMessage + "/n";

			errorMessage = TextErrorMessage;
			errorHTMLMessage = HTMLErrorMessage;
		
		}

		/// <summary>
		/// Prepare Error with the list of error added
		/// </summary>
		public void PrepareErrorMessage()
		{
			String TextErrorMessage = "";
			String HTMLErrorMessage = "";
			//Init the error message by the header
			HTMLErrorMessage = VALMSG_HEADER_TEXT_VAR_0 + NewLine;
			TextErrorMessage = VALMSG_HEADER_TEXT_VAR_0 + NewLine;

			if ( alErrorMsg.Count != 0)
			{				
				HTMLErrorMessage = HTMLErrorMessage + UnorderList;
				TextErrorMessage = TextErrorMessage + " /n ";
				
				System.Collections.IEnumerator ienum = alErrorMsg.GetEnumerator();
				 

				while(ienum.MoveNext())
				{		
					
						HTMLErrorMessage = HTMLErrorMessage + ListItem + ienum.Current.ToString() + ListItemEnd;
						TextErrorMessage = TextErrorMessage + "-" + ienum.Current.ToString() + NewLine;
					
				}
				HTMLErrorMessage = HTMLErrorMessage + UnorderListEnd;	
			}

			errorMessage = TextErrorMessage;
			errorHTMLMessage = HTMLErrorMessage;
		}
		public String FormatErrorMessage(String PredefinedMsg, String[] Var)
		{
			int iIndex  = 1;
			String ErrorMessage = PredefinedMsg;
			foreach(String var in Var)
			{
				String textToFind = "[var" + iIndex.ToString() + "]";
				int iIndexFound = ErrorMessage.IndexOf(textToFind);
				if (iIndexFound != -1)
				{
					ErrorMessage = ErrorMessage.Replace(textToFind, var);
				}
				iIndex = iIndex + 1;

			}
			return ErrorMessage;
		
		}

		public String FormatErrorMessage(String PredefinedMsg, String Var)
		{
			return FormatErrorMessage(PredefinedMsg, new String[] {Var});
		}
		private void ChangeTag()
		{
				NewLine = "<br>";
				UnorderList = "<UL>";
				ListItem = "<LI>";
				ListItemEnd = "</LI>";
				UnorderListEnd = "</UL>";
		}
		public void Add(string Value)
		{
			this.alErrorMsg.Add(Value);
		}
	}
}
