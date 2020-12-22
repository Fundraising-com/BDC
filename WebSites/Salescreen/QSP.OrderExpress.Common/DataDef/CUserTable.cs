using System;
using System.Data;
using System.Runtime.Serialization;
    

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of CUserTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type CUserTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class CUserTable : DataTable
	{		
		//
		//User constants
		//
		/// <value>The constant used for CUser table. </value>
		public const String TBL_CUSERS = "CuserProfile";
		/// <value>The constant used for PK "Instance" field in the CUser table. </value>
		public const String FLD_PKID = "Instance";
		/// <value>The constant used for User Name field in the CUser table. </value>
		public const String FLD_USER_NAME = "UserName";
		/// <value>The constant used for Password field in the CUser table. </value>
		public const String FLD_PASSWORD  = "Password";
		/// <value>The constant used for title field in the CUser table. </value>
		public const String FLD_FIRST_NAME = "FirstName";
		/// <value>The constant used for Last Name field in the CUser table. </value>		
		public const String FLD_LAST_NAME = "LastName";
		/// <value>The constant used for "Region" field in the CUser table. </value>
		public const String FLD_EMAIL = "Email";
		/// <value>The constant used for "CorporateEmail" field in the CUser table. </value>
		public const String FLD_CORP_EMAIL = "CorporateEmail";
		/// <value>The constant used for "CUserNumber" field in the CUser table. </value>
		public const String FLD_FM_ID  = "FMNumber";
		/// <value>The constant used for "CUserNumber" field in the CUser table. </value>
		public const String FLD_FSM_NAME  = "FM_Name";
		/// <value>The constant used for "MailAddress1" field in the CUser table. </value>
		public const String FLD_ADDR1  = "MailAddress1";
		/// <value>The constant used for "MailAddress2" field in the CUser table. </value>
		public const String FLD_ADDR2  = "MailAddress2";
		/// <value>The constant used for "MailCity" field in the CUser table. </value>
		public const String FLD_CITY  = "MailCity";
		/// <value>The constant used for "MailState" field in the CUser table. </value>
		public const String FLD_STATE  = "MailState";
		/// <value>The constant used for "MailPostalCode" field in the CUser table. </value>
		public const String FLD_POSTAL_CODE  = "MailPostalCode";
		/// <value>The constant used for "VoiceMailExt" field in the CUser table. </value>
		public const String FLD_VOICE_MAIL_EXT  = "VoiceMailExt";
		/// <value>The constant used for "HomePhone" field in the CUser table. </value>
		public const String FLD_HOME_PHONE  = "HomePhone";
		/// <value>The constant used for "WorkPhone" field in the CUser table. </value>
		public const String FLD_WORK_PHONE  = "WorkPhone";
		/// <value>The constant used for "FaxPhone" field in the CUser table. </value>
		public const String FLD_FAX_PHONE  = "FaxPhone";
		/// <value>The constant used for "TollFreePhone" field in the CUser table. </value>
		public const String FLD_TOLL_FREE_PHONE  = "TollFreePhone";
		/// <value>The constant used for "MobilePhone" field in the CUser table. </value>
		public const String FLD_MOBILE_PHONE  = "MobilePhone";
		/// <value>The constant used for "PagerPhone" field in the CUser table. </value>
		public const String FLD_PAGER_PHONE  = "PagerPhone";
		/// <value>The constant used for "AreaManager" field in the CUser table. </value>
		public const String FLD_AREA_MGR  = "AreaManager";			

		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public CUserTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		
        
		/// <summary>
		///     Constructor for UserData.  
		///     <remarks>Initialize a UserData instance by building the table schema.</remarks> 
		/// </summary>
		public CUserTable()
		{
			//
			// Create the tables 
			//
			BuildDataTable();
		}
                
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable: CUserTable
		//	 The information provide from the CUserProfile Table
		//   of the QSPCommon Database
		//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the CUser table
			//
			this.TableName = TBL_CUSERS;
			DataColumnCollection columns = this.Columns;

			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.DefaultValue = 0;
           
			columns.Add(FLD_USER_NAME, typeof(System.String));
			columns.Add(FLD_PASSWORD, typeof(System.String));
			columns.Add(FLD_FIRST_NAME, typeof(System.String));
			columns.Add(FLD_LAST_NAME, typeof(System.String));
			columns.Add(FLD_EMAIL, typeof(System.String));
			columns.Add(FLD_CORP_EMAIL, typeof(System.String));
			columns.Add(FLD_FM_ID, typeof(System.String));
			columns.Add(FLD_FSM_NAME, typeof(System.String));
			//Mail Address field
			columns.Add(FLD_ADDR1, typeof(System.String));
			columns.Add(FLD_ADDR2, typeof(System.String));
			columns.Add(FLD_CITY, typeof(System.String));
			columns.Add(FLD_STATE, typeof(System.String));
			columns.Add(FLD_POSTAL_CODE, typeof(System.String));
			//Phone Phone
			columns.Add(FLD_VOICE_MAIL_EXT, typeof(System.String));
			columns.Add(FLD_HOME_PHONE, typeof(System.String));
			columns.Add(FLD_WORK_PHONE, typeof(System.String));
			columns.Add(FLD_FAX_PHONE, typeof(System.String));
			columns.Add(FLD_TOLL_FREE_PHONE, typeof(System.String));
			columns.Add(FLD_MOBILE_PHONE, typeof(System.String));
			columns.Add(FLD_PAGER_PHONE, typeof(System.String));
			//Area where the CUser belong
			columns.Add(FLD_AREA_MGR, typeof(System.String));

		}
	}
}
