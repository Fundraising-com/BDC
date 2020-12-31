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
	public class LeadTable: CommonTable
	{
		public const string FLD_INSTANCE = "Instance";
		public const string TBL_LEAD= "Lead";
		public const string FLD_USERID= "UserID";
		public const string FLD_DATE= "Date";
		public const string FLD_DATESENT = "DateSent";
		public const string FLD_CONTACTNAME= "ContactName";
		public const int FLD_CONTACTNAME_LENGTH= 100;
		public const string FLD_CONTACTHOMEPHONENUMBER= "ContactHomePhoneNumber";
		public const int FLD_CONTACTHOMEPHONENUMBER_LENGTH= 14;
		public const string FLD_CONTACTWORKPHONENUMBER= "ContactWorkPhoneNumber";
		public const int FLD_CONTACTWORKPHONENUMBER_LENGTH= 14;
		public const string FLD_CONTACTFAXNUMBER= "ContactFaxNumber";
		public const int FLD_CONTACTFAXNUMBER_LENGTH= 14;
		public const string FLD_CONTACTEMAIL= "ContactEMail";
		public const int FLD_CONTACTEMAIL_LENGTH= 50;
		public const string FLD_SCHOOLGROUP= "SchoolGroup";
		public const int FLD_SCHOOLGROUP_LENGTH= 50;
		public const string FLD_CITYTOWN= "CityTown";
		public const int FLD_CITYTOWN_LENGTH= 50;
		public const string FLD_PROVINCE= "Province";
		public const int FLD_PROVINCE_LENGTH= 20;
		public const string FLD_INTERESTEDINWHAT= "InterestedInWhat";
		public const int FLD_INTERESTEDINWHAT_LENGTH= 250;
		public const string FLD_WHEREHEARABOUTQSP= "WhereHearAboutQSP";
		public const int FLD_WHEREHEARABOUTQSP_LENGTH= 250;
		public const string FLD_COMMENTS = "Comments";
		public const int FLD_COMMENTS_LENGTH = 250;
		public const string FLD_FMID= "FMID";
		public const int FLD_FMID_LENGTH= 4;
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public LeadTable()
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
		public LeadTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:Lead//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the Lead
			//
			this.TableName =TBL_LEAD;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_INSTANCE,typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;

			columns.Add(FLD_USERID, typeof(Int32));
			columns.Add(FLD_DATE, typeof(DateTime));
			columns.Add(FLD_DATESENT, typeof(DateTime));
			columns.Add(FLD_CONTACTNAME, typeof(string));
			columns.Add(FLD_CONTACTHOMEPHONENUMBER, typeof(string));
			columns.Add(FLD_CONTACTWORKPHONENUMBER, typeof(string));
			columns.Add(FLD_CONTACTFAXNUMBER, typeof(string));
			columns.Add(FLD_CONTACTEMAIL, typeof(string));
			columns.Add(FLD_SCHOOLGROUP, typeof(string));
			columns.Add(FLD_CITYTOWN, typeof(string));
			columns.Add(FLD_PROVINCE, typeof(string));
			columns.Add(FLD_INTERESTEDINWHAT, typeof(string));
			columns.Add(FLD_WHEREHEARABOUTQSP, typeof(string));
			columns.Add(FLD_COMMENTS, typeof(string));
			columns.Add(FLD_FMID, typeof(string));
			
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_INSTANCE]};
		}
	}
}