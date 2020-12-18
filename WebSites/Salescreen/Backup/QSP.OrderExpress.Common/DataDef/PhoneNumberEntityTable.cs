using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of PhoneNumberEntityTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type PhoneNumberEntityTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class PhoneNumberEntityTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Postal Address Entity table. </value>
		public const String TBL_PHONE_NUMBER_ENTITY = "phone_number_entity";
		/// <value>The constant used for PKId field in the Postal Address Entity table. </value>
		public const String FLD_PKID = "phone_number_entity_id";
		/// <value>The constant used for Address Name field in the Postal Address Entity table. </value>
		public const String FLD_PHONE_NUMBER_ID = "phone_number_id";
		/// <value>The constant used for entity field in the Postal Address Entity table. </value>
		public const String FLD_ENTITY_TYPE_ID = "entity_type_id";				
		/// <value>The constant used for entity field in the Postal Address Entity table. </value>
		public const String FLD_ENTITY_ID = "entity_id";		
		/// <value>The constant used for Type field field in the Postal Entity Address table. </value>
		public const String FLD_TYPE = "phone_number_type_id";
		/// <value>The constant used for Type field field in the Postal Entity Address table. </value>
		public const String FLD_TYPE_NAME = "phone_number_type_name";
		/// <value>The constant used for Phone Number field in the Postal Address table. </value>
		public const String FLD_PHONE_NUMBER = "phone_number";
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";



		public PhoneNumberEntityTable() : base(TBL_PHONE_NUMBER_ENTITY) 
		{
			 this.InitClass();
		}
        
		public PhoneNumberEntityTable(DataTable table) : base(table.TableName) 
		{
			if ((table.CaseSensitive != table.DataSet.CaseSensitive)) 
			{
				this.CaseSensitive = table.CaseSensitive;
			}
			if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) 
			{
				this.Locale = table.Locale;
			}
			if ((table.Namespace != table.DataSet.Namespace)) 
			{
				this.Namespace = table.Namespace;
			}
			this.Prefix = table.Prefix;
			this.MinimumCapacity = table.MinimumCapacity;
			this.DisplayExpression = table.DisplayExpression;
		}

		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public PhoneNumberEntityTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}	
            
            
		public System.Collections.IEnumerator GetEnumerator() 
		{
			return this.Rows.GetEnumerator();
		}
            
		public override DataTable Clone() 
		{
			PhoneNumberEntityTable cln = ((PhoneNumberEntityTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
            
		protected override DataTable CreateInstance() 
		{
			return new PhoneNumberEntityTable();
		}
            


		internal void InitVars() 
		{
			
		}
            
		private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_PHONE_NUMBER_ENTITY;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID >= 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			//Column.DefaultValue = 0;
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;


			columns.Add(FLD_PHONE_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_ENTITY_TYPE_ID, typeof(System.Int32));
            columns.Add(FLD_ENTITY_ID, typeof(System.Int32));	
			columns.Add(FLD_TYPE, typeof(System.Int32));
			columns.Add(FLD_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_PHONE_NUMBER, typeof(System.String));
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));
            //this.PrimaryKey = new DataColumn[] { this.Columns[FLD_PHONE_NUMBER_ID] };
  			this.PrimaryKey = new DataColumn[] {this.Columns[FLD_PKID], this.Columns[FLD_ENTITY_ID], this.Columns[FLD_ENTITY_TYPE_ID], this.Columns[FLD_TYPE]};
		}

		
	}

	
}
