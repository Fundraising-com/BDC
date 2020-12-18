using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing MDR school information.
	///     <remarks>
	///         This class is used to define the schema of the data used from the CMDRSchool table.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type CMDRTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class CMDRTable : DataTable
	{

		/// <value>The constant used for CMDRSchool table. </value>
		public const String TBL_CMDR = "CMDRSchool";
		/// <value>The constant used for primary key in the CMDRSchool table. </value>
		public const String FLD_PKID = "PID";
		/// <value>The constant used for "Name" field in the CMDRSchool table. </value>
		public const String FLD_NAME = "Name";
		/// <value>The constant used for "Address" field in the CMDRSchool table. </value>
		public const String FLD_ADDR  = "Address";
		/// <value>The constant used for "City" field in the CMDRSchool table. </value>
		public const String FLD_CITY  = "City";
		/// <value>The constant used for "City" field in the CMDRSchool table. </value>
		public const String FLD_COUNTY  = "County";
		/// <value>The constant used for "State" field in the CMDRSchool table. </value>
		public const String FLD_STATE  = "State";
		/// <value>The constant used for "Zip" field in the CMDRSchool table. </value>
		public const String FLD_POSTAL_CODE  = "Zip";
		/// <value>The constant used for "Zip" field in the CMDRSchool table. </value>
		public const String FLD_PHONE_NUMBER  = "PhoneNumber";
		/// <value>The constant used for "Zip" field in the CMDRSchool table. </value>
		public const String FLD_TYPE  = "Type";
		/// <value>The constant used for "Zip" field in the CMDRSchool table. </value>
		public const String FLD_LOW_GRADE  = "LowGrade";
		/// <value>The constant used for "Zip" field in the CMDRSchool table. </value>
		public const String FLD_HIGH_GRADE  = "HighGrade";
		/// <value>The constant used for "Principal Name" field in the CMDRSchool table. </value>
		public const String FLD_PRINCIPAL_NAME  = "PrincipalName";
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_TYPE_ID = "organization_type_id";		
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_LEVEL_ID = "organization_level_id";	
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORGANIZATION_ID = "organization_id";
		

		#region constructors
		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public CMDRTable (SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}

		/// <summary>
		///     Constructor for CMDRTable.  
		///     <remarks>Initialize a CMDRTable instance by building the table schema.</remarks> 
		/// </summary>
		public CMDRTable()
		{
			//
			// Create the tables 
			//
			BuildDataTable();
		}
		#endregion constructors

		#region BuildDataTable
		///<summary>Creates the following datatable: CMDRTable</summary>
		///<remarks>
		///  The information provided is from the CMDRSchool table
		///  of the QSPCommon Database
		///</remarks>
		private void BuildDataTable()
		{
			//
			// Create the CMDRSchool table
			//
			this.TableName = TBL_CMDR;
			DataColumnCollection columns = this.Columns;

			DataColumn Column = columns.Add(FLD_PKID, typeof(System.String));

			//Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			//Column.DefaultValue = 0;

			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_ADDR, typeof(System.String));
			columns.Add(FLD_CITY, typeof(System.String));
			columns.Add(FLD_COUNTY, typeof(System.String));
			columns.Add(FLD_STATE, typeof(System.String));
			columns.Add(FLD_POSTAL_CODE, typeof(System.String));
			columns.Add(FLD_PHONE_NUMBER, typeof(System.String));
			columns.Add(FLD_TYPE, typeof(System.String));
			columns.Add(FLD_LOW_GRADE, typeof(System.String));
			columns.Add(FLD_HIGH_GRADE, typeof(System.String));
			columns.Add(FLD_PRINCIPAL_NAME, typeof(System.String));
			columns.Add(FLD_ORG_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ORG_LEVEL_ID, typeof(System.Int32));
			columns.Add(FLD_ORGANIZATION_ID, typeof(System.Int32));
		}
		#endregion BuildDataTable
	}
}
