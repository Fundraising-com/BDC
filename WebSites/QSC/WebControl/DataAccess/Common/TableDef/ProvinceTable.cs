using System;
using System.Data;
using System.Runtime.Serialization;
namespace QSP.WebControl.DataAccess.Common.TableDef
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
	internal class ProvinceTable: CommonTable
	{
		public const string TBL_PROVINCE= "Province";
		public const string FLD_COUNTRY_CODE= "COUNTRY_CODE";
		public const int FLD_COUNTRY_CODE_LENGTH= 10;
		public const string FLD_PROVINCE_CODE= "PROVINCE_CODE";
		public const int FLD_PROVINCE_CODE_LENGTH= 10;
		public const string FLD_PROVINCE_NAME= "PROVINCE_NAME";
		public const int FLD_PROVINCE_NAME_LENGTH= 50;
		public const string FLD_LAPSE_DAYS_DELIVERY= "LAPSE_DAYS_DELIVERY";
		public const string FLD_TAX_BACKOUT_FUNCTION= "TAX_BACKOUT_FUNCTION";
		public const int FLD_TAX_BACKOUT_FUNCTION_LENGTH= 50;
		public const string FLD_LAPSE_DAYS_FIELD_SUPPLY_PREP= "LAPSE_DAYS_FIELD_SUPPLY_PREP";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public ProvinceTable()
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
		public ProvinceTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:Province//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the Province
			//
			this.TableName =TBL_PROVINCE;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_COUNTRY_CODE, typeof(string));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			Column = columns.Add(FLD_PROVINCE_CODE, typeof(string));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_PROVINCE_NAME, typeof(string));
			columns.Add(FLD_LAPSE_DAYS_DELIVERY, typeof(Int32));
			columns.Add(FLD_TAX_BACKOUT_FUNCTION, typeof(string));
			columns.Add(FLD_LAPSE_DAYS_FIELD_SUPPLY_PREP, typeof(Int32));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_COUNTRY_CODE],Columns[FLD_PROVINCE_CODE]};
		}
	}
}