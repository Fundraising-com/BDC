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
	public class SwitchLetterBatchCustomerOrderDetailTable: CommonTable
	{
		public const string TBL_SWITCHBATCHLETTERCUSTOMERORDERDETAIL= "SwitchLetterBatchCustomerOrderDetail";
		public const string FLD_SWITCHLETTERBATCHINSTANCE= "SwitchLetterBatchInstance";
		public const string FLD_CUSTOMERORDERHEADERINSTANCE= "CustomerOrderHeaderInstance";
		public const string FLD_TRANSID= "TransID";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public SwitchLetterBatchCustomerOrderDetailTable()
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
		public SwitchLetterBatchCustomerOrderDetailTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:SwitchBatchLetterCustomerOrderDetail//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the SwitchBatchLetterCustomerOrderDetail
			//
			this.TableName = TBL_SWITCHBATCHLETTERCUSTOMERORDERDETAIL;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_SWITCHLETTERBATCHINSTANCE, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			Columns.Add(FLD_CUSTOMERORDERHEADERINSTANCE, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_TRANSID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_SWITCHLETTERBATCHINSTANCE],Columns[FLD_CUSTOMERORDERHEADERINSTANCE],Columns[FLD_TRANSID]};
		}
	}
}