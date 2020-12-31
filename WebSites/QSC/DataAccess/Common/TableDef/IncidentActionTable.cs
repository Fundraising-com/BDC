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
	public class IncidentActionTable: CommonTable
	{
		public const string FLD_INSTANCE = "Instance";
		public const string TBL_INCIDENTACTION= "IncidentAction";
		public const string FLD_INCIDENTINSTANCE= "IncidentInstance";
		public const string FLD_ACTIONINSTANCE= "ActionInstance";
		public const string FLD_COMMENTS= "Comments";
		public const int FLD_COMMENTS_LENGTH= 500;
		public const string FLD_USERIDCREATED= "UserIDCreated";
		public const string FLD_CUSTOMERREMITHISTORYINSTANCE ="CustomerRemitHistoryInstance";
		public const int FLD_USERIDCREATED_LENGTH= 4;
		public const string FLD_DATECREATED= "DateCreated";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public IncidentActionTable()
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
		public IncidentActionTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:IncidentAction//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the IncidentAction
			//
			this.TableName =TBL_INCIDENTACTION;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_INSTANCE,typeof(int));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_ACTIONINSTANCE, typeof(Int32));
			columns.Add(FLD_COMMENTS, typeof(string));
			columns.Add(FLD_USERIDCREATED, typeof(string));
			columns.Add(FLD_DATECREATED, typeof(DateTime));
			columns.Add(FLD_INCIDENTINSTANCE, typeof(Int32));
			columns.Add(FLD_CUSTOMERREMITHISTORYINSTANCE,typeof(Int32));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_INSTANCE]};
		}
	}
}