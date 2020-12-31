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
	public class PhoneTable: CommonTable
	{
		public const string TBL_PHONE= "Phone";
		public const string FLD_ID= "ID";
		public const string FLD_TYPE= "Type";
		public const string FLD_PHONELISTID= "PhoneListID";
		public const string FLD_PHONENUMBER= "PhoneNumber";
		public const int FLD_PHONENUMBER_LENGTH= 50;
		public const string FLD_BESTTIMETOCALL= "BestTimeToCall";
		public const int FLD_BESTTIMETOCALL_LENGTH= 2000;
		public const string FLD_CANADA_OM_PHONELISTID= "Canada_OM_PhoneListID";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public PhoneTable()
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
		public PhoneTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:Phone//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the Phone
			//
			this.TableName =TBL_PHONE;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_ID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_TYPE, typeof(Int32));
			columns.Add(FLD_PHONELISTID, typeof(Int32));
			columns.Add(FLD_PHONENUMBER, typeof(string));
			columns.Add(FLD_BESTTIMETOCALL, typeof(string));
			columns.Add(FLD_CANADA_OM_PHONELISTID, typeof(Int32));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_ID]};
		}
	}
}