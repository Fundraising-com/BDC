using System;
using System.Data;
using System.Web.UI;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.TableDef;


namespace QSPFulfillment.DataAccess.Common
{
	/// <summary>
	/// Summary description for QCAPMessage.
	/// This class i used to Manage all Message for exception, including
	/// the validation message that we need to provide to the presentation Layer
	/// </summary>
	public class Message
	{
		public readonly static String VALMSG_GENERAL_VAR_0 = "Some fields are invalid, the operation cannot be performed.";
		public readonly static String VALMSG_REQUIRED_FIELD_VAR_1 = "The field [var1] is mandatory.";
		public readonly static String VALMSG_MAX_LENGTH_VAR_2 = "Invalid length for the field [var1], it must be [var2] character(s).";
		public readonly static String VALMSG_LENGTH_RANGE_VAR_3 = "Invalid length for the field [var1], it must be between [var2] and [var3] character(s).";
		public readonly static String VALMSG_INTEGRITY_VAR_2 = "The delete operation cannot be performed, because the [var1] is associated with at least one [var2].";
		public readonly static String VALMSG_UNICITY_VAR_2 = "[var1] already exists for this [var2].";
		public readonly static String VALMSG_HEADER_TEXT_VAR_0 = "Error Summary, please correct the following error(s):"; 
		public readonly static String SYSMSG_HEADER_TEXT_VAR_0 = "System error"; 
		//public const String IMPORT_GENERAL_ERR_MSG_VAR_0 = "An error occured during the Import, please verify the requirements.";
		//public const String IMPORT_EMPTY_SHEET_VAR_0 = "The Import file doesn't contains any row.";
		
		public readonly static String ERRMSG_NO_REC_AFF_VAR_0 = "The modification have not been made.  Probably there is someone who have delete this record before. " +
			"Please refresh the list to verify the presence of this record.";
		public readonly static string ERRMSG_SYSTEM_VAR_0 = "The server is unable to provide the requested information. Please retry.";
		//public const string DATA_ADMIN_CLEAR_VAR_2 = "To be able to clear the Table [var1], you must clear also the folowing tables: [var2]";
		public readonly static string ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0 = "Please provide at least one field.";
		public readonly static string ERRMSG_SEARCH_PROVIDE_FROM_TO_1 = "Please provide From and To [var1].";
		public readonly static string ERRMSG_INSTANCE_DO_NOT_EXIST_1 = "The [var1] that you have entered is invalid.";
		//public const string ERRMSG_ACTION_INSTANCE_UNIQUE = "This operation can be operated only time.";
		//public const string ERRMSG_ACTION_STATUS_ERROR_0 = "You cannot do this action because of the current status of the subscription.";
		public readonly static string ERRMSG_TITLE_CODE_STATUS_INVALID_0 = "The Magazine has to be inactive in order to generate a switch letter.";
		public readonly static string ERRMSG_TITLE_CODE_REMIT_BATCH_ID_COMBINATION_0 = "This remit batch and title code combination has NO eligible subscription for Switch Letter Generation.";
		public readonly static string ERRMSG_TITLE_CODE_SWITCH_LETTER_0 = "This Title has NO eligible subscription for Switch Letter Generation.";
		public readonly static string ERRMSG_REQUEST_NO_ROW_AFFECT_0 = "You'r request worked succefuly but no modification has been made.";
		public readonly static string ERRMSG_SWITCH_LETTER_SUB_EXIST_0 = "There is already a Switch Letter generated for this Subscription.";
		public readonly static string ERRMSG_SWITCH_LETTER_SUB_NOT_EXIST_0 = "There is no Switch Letter generated for this Subscription.";
		public readonly static string ERRMSG_SWITCH_LETTER_BATCH_AT_LEAST_0 = "Please provide at least Remit Batch or From/To Date.";
		public readonly static string ERRMSG_SWITCH_LETTER_BATCH_NOT_BOTH_0 = "Please provide Remit Batch or From/To Date.";
		public readonly static string ERRMSG_TITLE_CODE_FROM_TO_COMBINATION_0 = "This title code and From/To combination has NO eligible subscription fro Switch Letter Generation.";
		public readonly static string ERRMSG_COUPON_INVALID= "The certificate does not exist.";
		public readonly static string ERRMSG_COUPON_ALREADY_USE = "The coupon is already used.";
		public readonly static string ERRMSG_CERTIFICATE_ALREADY_USED = "The certificate is already in use.<br><br>Are you sure you wish to continue?";
		public readonly static string ERRMSG_VALID_CREDIT_CARD_FORMAT = "The credit card number is not in the right format. Ex:(1111 1111 1111 1111).";
		public readonly static string ERRMSG_INVALID_CHARACTER_SEARCH_1 = "This character <font color=blue>[var1]</font> is not accepted in the search field";
		public readonly static string ERRMSG_CANNOT_BE_HIGHER_2 = "The value in the field [var1] cannot be higher than the value of the field [var2].";
		public readonly static string ERRMSG_CREDIT_CARD_REJECTED_1 = "The credit card has been rejected for the following reason: [var1]";
		public readonly static string ERRMSG_STATE_PROVINCE_0 = "The selected State / Province is not in the Country you selected.";
		public readonly static string ERRMSG_POSTALCODE_INVALID_0 = "The field Postal Code is invalid. Ex: H1H 1H1";
		public readonly static string ERRMSG_ZIP_INVALID_0 = "The field Zip Code is invalid. Ex: 11111 or 11111-1111";
		public readonly static string ERRMSG_PRODUCT_CODE_ALREADY_EXISTS = "The Product Code already exists.";
		public readonly static string ERRMSG_GSTWITHOUTHST_0 = "GST Registration Number cannot be entered without the HST Registration Number.";
		public readonly static string ERRMSG_HSTWITHOUTGST_0 = "HST Registration Number cannot be entered without the GST Registration Number.";
		public readonly static string ERRMSG_FIELD_SUPPLIES_WITHOUT_PROGRAM_0 = "A Field Supplies catalog section needs to be mapped to a program";
		public readonly static string ERRMSG_CANNOT_DELETE_CONTRACT_0 = "The contract cannot be deleted. There are orders that have been placed for it.";
		public readonly static string ERRMSG_INVALID_PRODUCT_CODE_0 = "The selected Title Code is not part of the list.";
		public readonly static string ERRMSG_PRODUCT_CODE_ALREADY_SELECTED_1 = "The contact [var1] is already responsible for this title.";
		public readonly static string ERRMSG_PRODUCT_AT_LEAST_ONE_0 = "The contact needs to be linked to at least one title.";
		public readonly static string ERRMSG_CANNOT_DELETE_SECTION_0 = "The section cannot be deleted. There are orders that have been placed for its contracts.";
		public readonly static string ERRMSG_CANNOT_DELETE_CATALOG_0 = "The catalog cannot be deleted. There are orders that have been placed for its contracts.";
		public readonly static string ERRMSG_CANNOT_DELETE_PRODUCT_0 = "The product cannot be deleted. There are orders that have been placed for its contracts.";
		public readonly static string ERRMSG_INVALID_ORDER_QUALIFIER = "The order qualifier must be of a type that allows free items as you have selected at least one free item.";
		
		private static bool IsSet = false;
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
		private static QSPFulfillment.DataAccess.Business.ErrorMessageBusiness bus;
		private static QSPFulfillment.DataAccess.Common.TableDef.ErrorMessageTable Table;
		
		public Message(bool IsMessageBox)
		{
			if(!IsSet)
			{
				try
				{
					LoadErrorMessage();
					if(Table.Rows.Count !=0)
					{
						SetValueMessage();
					}
					IsSet= true;
					
				}
				catch
				{
					
				}
			}	

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
						HTMLErrorMessage = HTMLErrorMessage + ListItem + row.GetColumnError(dc) + ListItemEnd + NewLine;
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
					
						HTMLErrorMessage = HTMLErrorMessage + ListItem + ienum.Current.ToString() + ListItemEnd+ NewLine;;
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
		private void LoadErrorMessage()
		{
			
				bus =new QSPFulfillment.DataAccess.Business.ErrorMessageBusiness();
				Table = new QSPFulfillment.DataAccess.Common.TableDef.ErrorMessageTable();

				bus.SelectAll(Table);
			

		}
		private void SetValueMessage()
		{
			SetValueField("VALMSG_GENERAL_VAR_0",VALMSG_GENERAL_VAR_0);
			SetValueField("VALMSG_REQUIRED_FIELD_VAR_1",VALMSG_REQUIRED_FIELD_VAR_1);
			SetValueField("VALMSG_MAX_LENGTH_VAR_2",VALMSG_MAX_LENGTH_VAR_2);
			SetValueField("VALMSG_INTEGRITY_VAR_2",VALMSG_INTEGRITY_VAR_2);
			SetValueField("VALMSG_UNICITY_VAR_2",VALMSG_UNICITY_VAR_2);
			SetValueField("VALMSG_HEADER_TEXT_VAR_0",VALMSG_HEADER_TEXT_VAR_0);
			SetValueField("SYSMSG_HEADER_TEXT_VAR_0",SYSMSG_HEADER_TEXT_VAR_0);
	
			SetValueField("ERRMSG_NO_REC_AFF_VAR_0",ERRMSG_NO_REC_AFF_VAR_0);
			
			SetValueField("ERRMSG_SYSTEM_VAR_0",ERRMSG_SYSTEM_VAR_0);
		
			SetValueField("ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0",ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0);
			SetValueField("ERRMSG_SEARCH_PROVIDE_FROM_TO_DATE_0",ERRMSG_SEARCH_PROVIDE_FROM_TO_1);
			SetValueField("ERRMSG_INSTANCE_DO_NOT_EXIST_1",ERRMSG_INSTANCE_DO_NOT_EXIST_1);
		
			SetValueField("ERRMSG_TITLE_CODE_STATUS_INVALID_0",ERRMSG_TITLE_CODE_STATUS_INVALID_0);
			SetValueField("ERRMSG_TITLE_CODE_REMIT_BATCH_ID_COMBINATION_0",ERRMSG_TITLE_CODE_REMIT_BATCH_ID_COMBINATION_0);
			SetValueField("ERRMSG_TITLE_CODE_SWITCH_LETTER_0",ERRMSG_TITLE_CODE_SWITCH_LETTER_0);
			SetValueField("ERRMSG_REQUEST_NO_ROW_AFFECT_0",ERRMSG_REQUEST_NO_ROW_AFFECT_0);
			SetValueField("ERRMSG_SWITCH_LETTER_SUB_EXIST_0",ERRMSG_SWITCH_LETTER_SUB_EXIST_0);
			SetValueField("ERRMSG_SWITCH_LETTER_SUB_NOT_EXIST_0",ERRMSG_SWITCH_LETTER_SUB_NOT_EXIST_0);
			SetValueField("ERRMSG_SWITCH_LETTER_BATCH_AT_LEAST_0",ERRMSG_SWITCH_LETTER_BATCH_AT_LEAST_0);
			SetValueField("ERRMSG_SWITCH_LETTER_BATCH_NOT_BOTH_0",ERRMSG_SWITCH_LETTER_BATCH_NOT_BOTH_0);
			SetValueField("ERRMSG_TITLE_CODE_FROM_TO_COMBINATION_0",ERRMSG_TITLE_CODE_FROM_TO_COMBINATION_0);
			SetValueField("ERRMSG_COUPON_INVALID",ERRMSG_COUPON_INVALID);
			SetValueField("ERRMSG_COUPON_ALREADY_USE",ERRMSG_COUPON_ALREADY_USE);
			SetValueField("ERRMSG_VALID_CREDIT_CARD_FORMAT",ERRMSG_VALID_CREDIT_CARD_FORMAT);
			SetValueField("ERRMSG_INVALID_CHARACTER_SEARCH_1",ERRMSG_INVALID_CHARACTER_SEARCH_1);

			
		}
		private void SetValueField(string ID,string Variable)
		{

			DataRow[] row = Table.Select("ID = '"+ID+"'");

			if(row.Length !=0)
			{
				Variable= row[0][ErrorMessageTable.FLD_DESCRIPTION].ToString();
			}
			
		}
	}
}
