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
	public class CampaignProgramTable: CommonTable
	{
		public const string TBL_CAMPAIGNPROGRAM= "CampaignProgram";
		public const string FLD_CAMPAIGNID= "CampaignID";
		public const string FLD_PROGRAMID= "ProgramID";
		public const string FLD_ISPRECOLLECT= "IsPreCollect";
		public const int FLD_ISPRECOLLECT_LENGTH= 1;
		public const string FLD_GROUPPROFIT= "GroupProfit";
		public const string FLD_DELETEDTF= "DeletedTF";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public CampaignProgramTable()
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
		public CampaignProgramTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:CampaignProgram//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the CampaignProgram
			//
			this.TableName =TBL_CAMPAIGNPROGRAM;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_CAMPAIGNID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_PROGRAMID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_ISPRECOLLECT, typeof(string));
			columns.Add(FLD_GROUPPROFIT, typeof(Decimal));
			columns.Add(FLD_DELETEDTF, typeof(bool));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_CAMPAIGNID],Columns[FLD_PROGRAMID]};
		}
	}
}