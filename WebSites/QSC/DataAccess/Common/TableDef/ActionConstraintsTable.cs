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
	public class ActionConstraintsTable: CommonTable
	{
		public const string TBL_ACTIONCONSTRAINTS= "ActionConstraints";
		public const string FLD_INSTANCE= "Instance";
		public const string FLD_ACTIONINSTANCETOBEPERFORMED= "ActionInstanceToBePerformed";
		public const string FLD_CURRENTSTATUS = "CurrentStatus";
		public const string FLD_ERRORMESSAGE = "ErrorMessage";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public ActionConstraintsTable()
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
		public ActionConstraintsTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:ActionConstraints//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the ActionConstraints
			//
			this.TableName =TBL_ACTIONCONSTRAINTS;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_INSTANCE, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_ACTIONINSTANCETOBEPERFORMED, typeof(Int32));
			columns.Add(FLD_CURRENTSTATUS, typeof(Int32));
			columns.Add(FLD_ERRORMESSAGE,typeof(string));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_INSTANCE]};
		}
	}
}