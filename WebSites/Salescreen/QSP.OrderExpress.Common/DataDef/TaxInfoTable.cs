using System;
using System.Data;
using System.Runtime.Serialization;
    

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of Registry Table.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type RegistryTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class TaxInfoTable : DataTable
	{		
		//
		//User constants
		//
		/// <value>The constant used for Registry table. </value>
		public const String TBL_TAX_INFO = "tax_info";
		/// <value>The constant used for PKId field in the Registry table. </value>
		public const String FLD_LEVEL_ID = "tax_level";
		/// <value>The constant used for fm_id field in the Registry table. </value>
		public const String FLD_RATE = "tax_rate";
		/// <value>The constant used for role field in the Registry table. </value>
		public const String FLD_IS_EXEMPT = "tax_exempt";
		/// <value>The constant used for user_id field in the Registry table. </value>
		public const String FLD_IS_VALID = "tax_valid";		
			
		

		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public TaxInfoTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		
        
		/// <summary>
		///     Constructor for UserData.  
		///     <remarks>Initialize a UserData instance by building the table schema.</remarks> 
		/// </summary>
		public TaxInfoTable()
		{
			//
			// Create the tables in the dataset
			//
			BuildDataTable();
		}
                
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable: Periods
		//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the Periods table
			//
			this.TableName = TBL_TAX_INFO;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_LEVEL_ID, typeof(System.Int32));
            
			//Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.DefaultValue = 0;

            columns.Add(FLD_RATE, typeof(System.Decimal));
			columns.Add(FLD_IS_EXEMPT, typeof(System.Boolean));			
			columns.Add(FLD_IS_VALID, typeof(System.Boolean));
					

			
		}

		public bool IsTaxExempted
		{
			get
			{
				DataView dv = new DataView(this);
				dv.RowFilter = FLD_IS_EXEMPT + " = TRUE";

				return (dv.Count != 0);
			}
		}
	}
}
