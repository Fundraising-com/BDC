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
	public class FulfillmentHouseContactProductTable: CommonTable
	{
		public const string TBL_FULFILLMENTHOUSECONTACT_PRODUCT = "FulfillmentHouseContact_Product";
		public const string FLD_ID= "ID";
		public const string FLD_FULFILLMENTHOUSECONTACTID= "FulfillmentHouseContactID";
		public const string FLD_PRODUCT_CODE= "Product_Code";
		public const int FLD_PRODUCT_CODE_LENGTH= 20;
		public const string FLD_DATECREATED= "DateCreated";
		public const string FLD_USERIDCREATED= "UserIDCreated";
		public const string FLD_DATECHANGED= "DateChanged";
		public const string FLD_USERIDCHANGED= "UserIDChanged";
		public const string FLD_DELETEDTF= "DeletedTF";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public FulfillmentHouseContactProductTable()
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
		public FulfillmentHouseContactProductTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:CodeDetail//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the CodeDetail
			//
			this.TableName =TBL_FULFILLMENTHOUSECONTACT_PRODUCT;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_ID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_FULFILLMENTHOUSECONTACTID, typeof(Int32));
			columns.Add(FLD_PRODUCT_CODE, typeof(string));
			columns.Add(FLD_DATECREATED, typeof(DateTime));
			columns.Add(FLD_USERIDCREATED, typeof(int));
			columns.Add(FLD_DATECHANGED, typeof(DateTime));
			columns.Add(FLD_USERIDCHANGED, typeof(int));
			columns.Add(FLD_DELETEDTF, typeof(bool));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_ID]};
		}
	}
}