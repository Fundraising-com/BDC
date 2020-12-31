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
	public class INVOICETable: CommonTable
	{
		public const string TBL_INVOICE= "INVOICE";
		public const string FLD_INVOICE_ID= "INVOICE_ID";
		public const string FLD_ACCOUNT_ID= "ACCOUNT_ID";
		public const string FLD_ACCOUNT_TYPE_ID= "ACCOUNT_TYPE_ID";
		public const string FLD_ORDER_ID= "ORDER_ID";
		public const string FLD_INVOICE_DATE= "INVOICE_DATE";
		public const string FLD_INVOICE_DUE_DATE= "INVOICE_DUE_DATE";
		public const string FLD_INVOICE_AMOUNT= "INVOICE_AMOUNT";
		public const string FLD_FIRST_PRINT_DATE= "FIRST_PRINT_DATE";
		public const string FLD_NOTE_TO_PRINT= "NOTE_TO_PRINT";
		public const int FLD_NOTE_TO_PRINT_LENGTH= 100;
		public const string FLD_DATETIME_CREATED= "DATETIME_CREATED";
		public const string FLD_DATETIME_MODIFIED= "DATETIME_MODIFIED";
		public const string FLD_LAST_UPDATED_BY= "LAST_UPDATED_BY";
		public const int FLD_LAST_UPDATED_BY_LENGTH= 30;
		public const string FLD_COUNTRY_CODE= "COUNTRY_CODE";
		public const int FLD_COUNTRY_CODE_LENGTH= 10;
		public const string FLD_IS_PRINTED= "IS_PRINTED";
		public const int FLD_IS_PRINTED_LENGTH= 10;
		public const string FLD_DATETIME_APPROVED= "DATETIME_APPROVED";
		public const string FLD_INVOICE_EFFECTIVE_DATE= "INVOICE_EFFECTIVE_DATE";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public INVOICETable()
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
		public INVOICETable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:INVOICE//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the INVOICE
			//
			this.TableName =TBL_INVOICE;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_INVOICE_ID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_ACCOUNT_ID, typeof(Int32));
			columns.Add(FLD_ACCOUNT_TYPE_ID, typeof(Int32));
			columns.Add(FLD_ORDER_ID, typeof(Int32));
			columns.Add(FLD_INVOICE_DATE, typeof(DateTime));
			columns.Add(FLD_INVOICE_DUE_DATE, typeof(DateTime));
			columns.Add(FLD_INVOICE_AMOUNT, typeof(Decimal));
			columns.Add(FLD_FIRST_PRINT_DATE, typeof(DateTime));
			columns.Add(FLD_NOTE_TO_PRINT, typeof(string));
			columns.Add(FLD_DATETIME_CREATED, typeof(DateTime));
			columns.Add(FLD_DATETIME_MODIFIED, typeof(DateTime));
			columns.Add(FLD_LAST_UPDATED_BY, typeof(string));
			columns.Add(FLD_COUNTRY_CODE, typeof(string));
			columns.Add(FLD_IS_PRINTED, typeof(string));
			columns.Add(FLD_DATETIME_APPROVED, typeof(DateTime));
			columns.Add(FLD_INVOICE_EFFECTIVE_DATE, typeof(DateTime));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_INVOICE_ID]};
		}
	}
}