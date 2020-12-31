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
	public class GiftCardRemitBatchTable: CommonTable
	{
		public const string TBL_GIFTCARDREMITBATCH= "GiftCardRemitBatch";
		public const string FLD_GIFTCARDOUTPUTID= "GiftCardOutputId";
		public const string FLD_REMITBATCHID= "RemitBatchId";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public GiftCardRemitBatchTable()
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
		public GiftCardRemitBatchTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:GiftCardRemitBatch//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the GiftCardRemitBatch
			//
			this.TableName =TBL_GIFTCARDREMITBATCH;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_GIFTCARDOUTPUTID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			Column = columns.Add(FLD_REMITBATCHID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_GIFTCARDOUTPUTID],Columns[FLD_REMITBATCHID]};
		}
	}
}