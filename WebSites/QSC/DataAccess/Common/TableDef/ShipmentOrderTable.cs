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
	public class ShipmentOrderTable: CommonTable
	{
		public const string TBL_SHIPMENTORDER= "ShipmentOrder";
		public const string FLD_SHIPMENTID= "ShipmentID";
		public const string FLD_ORDERID= "OrderID";
		public const string FLD_SHIPMENTBATCHID= "ShipmentBatchID";
		public const string FLD_ISSHIPMENTBATCHCREATED= "IsShipmentBatchCreated";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public ShipmentOrderTable()
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
		public ShipmentOrderTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:ShipmentOrder//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the ShipmentOrder
			//
			this.TableName =TBL_SHIPMENTORDER;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_SHIPMENTID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			Column = columns.Add(FLD_ORDERID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_SHIPMENTBATCHID, typeof(Int32));
			columns.Add(FLD_ISSHIPMENTBATCHCREATED, typeof(bool));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_SHIPMENTID],Columns[FLD_ORDERID]};
		}
	}
}