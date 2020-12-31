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
	public class IncidentTable: CommonTable
	{
		public const string TBL_INCIDENT= "Incident";
		public const string FLD_INCIDENTINSTANCE= "IncidentInstance";
		public const string FLD_PROBLEMCODEINSTANCE= "ProblemCodeInstance";
		public const string FLD_CUSTOMERORDERHEADERINSTANCE= "CustomerOrderHeaderInstance";
		public const string FLD_TRANSID= "TransID";
		public const string FLD_COMMUNICATIONCHANNELINSTANCE= "CommunicationChannelInstance";
		public const string FLD_COMMUNICATIONSOURCEINSTANCE= "CommunicationSourceInstance";
		public const string FLD_STATUSINSTANCE= "StatusInstance";
		public const string FLD_REFERTOINCIDENTINSTANCE= "ReferToIncidentInstance";
		public const string FLD_COMMENTS= "Comments";
		public const int FLD_COMMENTS_LENGTH= 500;
		public const string FLD_USERIDCREATED= "UserIDCreated";
		public const int FLD_USERIDCREATED_LENGTH= 4;
		public const string FLD_DATECREATED= "DateCreated";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public IncidentTable()
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
		public IncidentTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:Incident//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the Incident
			//
			this.TableName =TBL_INCIDENT;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_INCIDENTINSTANCE, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_PROBLEMCODEINSTANCE, typeof(Int32));
			columns.Add(FLD_CUSTOMERORDERHEADERINSTANCE, typeof(Int32));
			columns.Add(FLD_TRANSID, typeof(Int32));
			columns.Add(FLD_COMMUNICATIONCHANNELINSTANCE, typeof(Int32));
			columns.Add(FLD_COMMUNICATIONSOURCEINSTANCE, typeof(Int32));
			columns.Add(FLD_STATUSINSTANCE, typeof(Int32));
			columns.Add(FLD_REFERTOINCIDENTINSTANCE, typeof(Int32));
			columns.Add(FLD_COMMENTS, typeof(string));
			columns.Add(FLD_USERIDCREATED, typeof(string));
			columns.Add(FLD_DATECREATED, typeof(DateTime));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_INCIDENTINSTANCE]};
		}
	}
}