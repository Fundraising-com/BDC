using System;
using System.Data;

namespace QSPForm.Common
{
	/// <summary>
	/// Summary description for QSPFormMessage.
	/// This class i used to Manage all Message for exception, including
	/// the validation message that we need to provide to the presentation Layer
	/// </summary>
	public class QSPFormMessage
	{
		public const String VALMSG_GENERAL = "Some fields are invalid, the operation cannot be performed.";
		public const String VALMSG_REQUIRED_FIELD = "The field [var1] is mandatory.";
		public const String VALMSG_MAX_LENGTH = "Invalid Length for the field [var1], it must be [var2] character(s).";
		public const String VALMSG_INTEGRITY = "The delete operation cannot be performed, because the [var1] is associated with at least one [var2].";
		public const String VALMSG_UNICITY = "[var1] already exist for this [var2].";
		public const String VALMSG_UNICITY_ORG = "A QSP Organization, at this address, is already in the system.  Please go to the Organization List in the Directory to find it.";
		public const String VALMSG_MAX_REACHED = "The maximum number ([var1]) of [var2] have been reached for this campaign.";
		public const String VALMSG_HEADER_TEXT = "Correct the following error to proceed."; 
		public const String SYSMSG_HEADER_TEXT = "System error"; 
		public const String IMPORT_GENERAL_ERR_MSG = "An error occured during the Import, please verify the requirements.";
		public const String IMPORT_OLEDB_ERR_MSG = "No Sheet have the name of QSPForm, please verify the requirements.";
		public const String IMPORT_REQUIRED_FIELD = "The Import file doesn't contains the column(s) : [var1]. ";
		public const String IMPORT_EMPTY_SHEET = "The Import file doesn't contains any row.";
		public const String IMPORT_MAX_OVER = "The importation cannot be performed cause the number of [var1] that the system have to insert " +
			"and the number of [var1] already defined in the current campaign will go over the maximum of " + 
			"[var2] for each campaign.";
		
		public const String ERRMSG_NO_REC_AFF = "The modification have not been made.  Probably there is someone who have delete this record before. " +
			"Please refresh the list to verify the if this record exist.";
		public const string ERRMSG_SYSTEM = "The server is unable to provide the requested information. Please retry.";
		public const string DATA_ADMIN_CLEAR = "To be able to clear the Table [var1], you must clear also the following tables: [var2]";
		public const String CONCURENT_RECORD_DELETED = "The modification cannot be made.  Another user has deleted this [var1].";
		public const String CONCURENT_RECORD_MODIFIED = "The modification cannot be made.  Another user has modified this [var1].";
		public const String CONCURENT_NEW_VERSION = "The modification cannot be made.  Another user has created a new version of this [var1].";
		
		
		private String errorMessage = "";
		private String errorHTMLMessage = "";
		private string headerText = VALMSG_HEADER_TEXT;
		private QSPFormExceptionType exType = QSPFormExceptionType.Unknown;
		
		public QSPFormMessage()
		{
			//
			// TODO: Add constructor logic here
			//
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

		public QSPFormExceptionType ValidationExceptionType
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


		public String HeaderText
		{
			get
			{
				return headerText;
			}
			set
			{
				headerText = value;
			}
						
		}

		public void SetErrorMessage(DataTable dtTable)
		{
			
			String TextErrorMessage = "";
			String HTMLErrorMessage = "";
			//Init the error message by the header
			HTMLErrorMessage = headerText + "<BR>";
			TextErrorMessage = headerText + "/n";

			if (dtTable.HasErrors)
			{				
				HTMLErrorMessage = HTMLErrorMessage + "<ul>";
				TextErrorMessage = TextErrorMessage + " /n ";
				foreach(DataRow row in dtTable.GetErrors())
				{		
					foreach(DataColumn dc in row.GetColumnsInError())
					{
						HTMLErrorMessage = HTMLErrorMessage + "<li>" + row.GetColumnError(dc) + @"</li>";
						TextErrorMessage = TextErrorMessage + "-" + row.GetColumnError(dc) + " /n ";
					}
				}
				HTMLErrorMessage = HTMLErrorMessage + @"</ul>";	
			}

			errorMessage = TextErrorMessage;
			errorHTMLMessage = HTMLErrorMessage;
			
		
		}
		
		public void SetErrorMessage(String ErrorMessage)
		{
			
			String TextErrorMessage = "";
			String HTMLErrorMessage = "";
			//Init the error message by the header
			HTMLErrorMessage = headerText + "<BR>";
			TextErrorMessage = headerText + "/n";

			HTMLErrorMessage = HTMLErrorMessage + "<ul>";
			HTMLErrorMessage = HTMLErrorMessage + "<li>" + ErrorMessage + @"</li>";
			HTMLErrorMessage = HTMLErrorMessage + @"</ul>";	

			TextErrorMessage = TextErrorMessage + "-" + ErrorMessage + "/n";

			errorMessage = TextErrorMessage;
			errorHTMLMessage = HTMLErrorMessage;
		
		}

		public void SetSystemErrorMessage(String ErrorMessage)
		{
			
			String TextErrorMessage = "";
			String HTMLErrorMessage = "";
			//Init the error message by the header
			HTMLErrorMessage = SYSMSG_HEADER_TEXT + "<BR>";
			TextErrorMessage = SYSMSG_HEADER_TEXT + "/n";

			HTMLErrorMessage = HTMLErrorMessage + "<ul>";
			HTMLErrorMessage = HTMLErrorMessage + "<li>" + ErrorMessage + @"</li>";
			HTMLErrorMessage = HTMLErrorMessage + @"</ul>";	

			TextErrorMessage = TextErrorMessage + "-" + ErrorMessage + "/n";

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
	}
}
