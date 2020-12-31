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
	public class ProblemCodeTable: CommonTable
	{
		public const string TBL_PROBLEMCODE= "ProblemCode";
		public const string FLD_INSTANCE= "Instance";
		public const string FLD_DESCRIPTION= "Description";
		public const int FLD_DESCRIPTION_LENGTH= 50;
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public ProblemCodeTable()
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
		public ProblemCodeTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:ProblemCode
		//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the ProblemCode
			//
			this.TableName =TBL_PROBLEMCODE;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_INSTANCE, typeof(Int32));
			Column.AllowDBNull = true;
			Column.DefaultValue = 0;
			columns.Add(FLD_DESCRIPTION, typeof(string));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_INSTANCE]};
		}
	}
}