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
	public class CodeDetailTable: CommonTable
	{
		public const string TBL_CODEDETAIL= "CodeDetail";
		public const string FLD_INSTANCE= "Instance";
		public const string FLD_CODEHEADERINSTANCE= "CodeHeaderInstance";
		public const string FLD_DESCRIPTION= "Description";
		public const int FLD_DESCRIPTION_LENGTH= 64;
		public const string FLD_GROSS= "Gross";
		public const string FLD_ADPCODE= "ADPCode";
		public const int FLD_ADPCODE_LENGTH= 6;
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public CodeDetailTable()
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
		public CodeDetailTable(SerializationInfo info, StreamingContext context) : base(info, context) 
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
			this.TableName =TBL_CODEDETAIL;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_INSTANCE, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_CODEHEADERINSTANCE, typeof(Int32));
			columns.Add(FLD_DESCRIPTION, typeof(string));
			columns.Add(FLD_GROSS, typeof(bool));
			columns.Add(FLD_ADPCODE, typeof(string));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_INSTANCE]};
		}
	}
}