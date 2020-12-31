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
	public class GiftCardOutputTable: CommonTable
	{
		public const string TBL_GIFTCARDOUTPUT= "GiftCardOutput";
		public const string FLD_ID= "ID";
		public const string FLD_FILENAME= "FileName";
		public const int FLD_FILENAME_LENGTH= 50;
		public const string FLD_DATE= "Date";
		public const string FLD_FILECONTENT= "FileContent";
		public const int FLD_FILECONTENT_LENGTH= 16;
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public GiftCardOutputTable()
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
		public GiftCardOutputTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:GiftCardOutput//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the GiftCardOutput
			//
			this.TableName =TBL_GIFTCARDOUTPUT;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_ID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_FILENAME, typeof(string));
			columns.Add(FLD_DATE, typeof(DateTime));
			columns.Add(FLD_FILECONTENT, typeof(string));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_ID]};
		}
	}
}