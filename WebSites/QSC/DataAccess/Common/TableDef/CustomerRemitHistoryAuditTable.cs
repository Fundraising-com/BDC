using System;
using System.Data;
using System.Runtime.Serialization;
namespace QSPFulfillment.DataAccess.Common.TableDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of CampaignPrizeData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type CampaignPrizeData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class CustomerRemitHistoryAuditTable: CommonTable
	{
		public const string TBL_CUSTOMERREMITHISTORYAUDIT= "CustomerRemitHistoryAudit";
		public const string FLD_AUDITDATE= "AuditDate";
		public const string FLD_REMITBATCHID= "RemitBatchID";
		public const string FLD_INSTANCE= "Instance";
		public const string FLD_CUSTOMERINSTANCE= "CustomerInstance";
		public const string FLD_STATUSINSTANCE= "StatusInstance";
		public const string FLD_LASTNAME= "LastName";
		public const int FLD_LASTNAME_LENGTH= 50;
		public const string FLD_FIRSTNAME= "FirstName";
		public const int FLD_FIRSTNAME_LENGTH= 50;
		public const string FLD_ADDRESS1= "Address1";
		public const int FLD_ADDRESS1_LENGTH= 50;
		public const string FLD_ADDRESS2= "Address2";
		public const int FLD_ADDRESS2_LENGTH= 50;
		public const string FLD_CITY= "City";
		public const int FLD_CITY_LENGTH= 50;
		public const string FLD_STATE= "State";
		public const int FLD_STATE_LENGTH= 10;
		public const string FLD_ZIP= "Zip";
		public const int FLD_ZIP_LENGTH= 20;
		public const string FLD_ZIPPLUSFOUR= "ZipPlusFour";
		public const int FLD_ZIPPLUSFOUR_LENGTH= 4;
		public const string FLD_DATEMODIFIED= "DateModified";
		public const string FLD_USERIDMODIFIED= "UserIDModified";
		public const int FLD_USERIDMODIFIED_LENGTH= 4;
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public CustomerRemitHistoryAuditTable()
		{
			//
			// Create the tables in the dataset
			//
			BuildDataTable();
		}
		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public CustomerRemitHistoryAuditTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:CustomerRemitHistoryAudit//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the CustomerRemitHistoryAudit
			//
			this.TableName =TBL_CUSTOMERREMITHISTORYAUDIT;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_AUDITDATE, typeof(DateTime));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_REMITBATCHID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_INSTANCE, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_CUSTOMERINSTANCE, typeof(Int32));
			columns.Add(FLD_STATUSINSTANCE, typeof(Int32));
			columns.Add(FLD_LASTNAME, typeof(string));
			columns.Add(FLD_FIRSTNAME, typeof(string));
			columns.Add(FLD_ADDRESS1, typeof(string));
			columns.Add(FLD_ADDRESS2, typeof(string));
			columns.Add(FLD_CITY, typeof(string));
			columns.Add(FLD_STATE, typeof(string));
			columns.Add(FLD_ZIP, typeof(string));
			columns.Add(FLD_ZIPPLUSFOUR, typeof(string));
			columns.Add(FLD_DATEMODIFIED, typeof(DateTime));
			columns.Add(FLD_USERIDMODIFIED, typeof(string));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_AUDITDATE],Columns[FLD_REMITBATCHID],Columns[FLD_INSTANCE]};
		}
	}
}