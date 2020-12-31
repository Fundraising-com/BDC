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
	public class CouponTable: CommonTable
	{
		public const string TBL_COUPON= "Coupon";
		public const string FLD_ID= "ID";
		public const int FLD_ID_LENGTH= 50;
		public const string FLD_COUPONSETID= "CouponSetID";
		public const string FLD_ISUSED= "IsUsed";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public CouponTable()
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
		public CouponTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:Coupon//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the Coupon
			//
			this.TableName =TBL_COUPON;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_ID, typeof(string));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_COUPONSETID, typeof(Int32));
			columns.Add(FLD_ISUSED, typeof(bool));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_ID]};
		}
	}
}