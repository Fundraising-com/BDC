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
	public class SwitchLetterBatchTable: CommonTable
	{
		public const string TBL_SWITCHLETTERBATCH= "SwitchLetterBatch";
		public const string FLD_INSTANCE= "Instance";
		public const string FLD_PRODUCTCODE= "ProductCode";
		public const int FLD_PRODUCTCODE_LENGTH= 20;
		public const int FLD_PRODUCTNAME_LENGTH= 50;
		public const string FLD_DATECREATED= "DateCreated";
		public const string FLD_USERID= "UserID";
		public const string FLD_LANGUAGECODE = "LanguageCode";
		public const int FLD_USERID_LENGTH= 4;
		public const string FLD_ISPRINTED= "IsPrinted";
		public const string FLD_DATEPRINTED= "DatePrinted";
		public const string FLD_ISLOCKED= "IsLocked";



		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public SwitchLetterBatchTable()
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
		public SwitchLetterBatchTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:SwitchLetterBatch//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the SwitchLetterBatch
			//
			this.TableName =TBL_SWITCHLETTERBATCH;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_INSTANCE, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_PRODUCTCODE, typeof(string));
			columns.Add(FLD_DATECREATED, typeof(DateTime));
			columns.Add(FLD_USERID, typeof(string));
			columns.Add(FLD_LANGUAGECODE,typeof(string));
			columns.Add(FLD_ISPRINTED,typeof(string));
			columns.Add(FLD_DATEPRINTED,typeof(DateTime));
			columns.Add(FLD_ISLOCKED,typeof(string));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_INSTANCE]};
		}
	}
}