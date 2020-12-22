using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;

namespace QSPForm.Common.DataDef
{

	//public delegate void PostalAddressEntityRowChangeEventHandler(object sender, PostalAddressEntityRowChangeEvent e);
        

	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of PostalAddressEntityTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type PostalAddressEntityTable to be remoted.
	///     </remarks>
	/// </summary>
	[Serializable]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("Code")]
	//[SerializableAttribute] 
	public class PostalAddressEntityTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Postal Address Entity table. </value>
		public const String TBL_POSTAL_ADDRESS_ENTITY = "postal_address_entity";
		/// <value>The constant used for PKId field in the Postal Address Entity table. </value>
		public const String FLD_PKID = "postal_address_entity_id";
		/// <value>The constant used for Address Name field in the Postal Address Entity table. </value>
		public const String FLD_ADDRESS_ID = "postal_address_id";
		/// <value>The constant used for entity field in the Postal Address Entity table. </value>
		public const String FLD_ENTITY_TYPE_ID = "entity_type_id";				
		/// <value>The constant used for entity field in the Postal Address Entity table. </value>
		public const String FLD_ENTITY_ID = "entity_id";		
		/// <value>The constant used for Type field field in the Postal Entity Address table. </value>
		public const String FLD_TYPE = "postal_address_type_id";
		/// <value>The constant used for Type field field in the Postal Entity Address table. </value>
		public const String FLD_TYPE_NAME = "postal_address_type_name";
		/// <value>The constant used for Address Title field in the Postal Address table. </value>
		public const String FLD_TITLE = "TitleAddress";
		/// <value>The constant used for Address Name field in the Postal Address table. </value>
		public const String FLD_NAME = "name";		
		/// <value>The constant used for Address Name field in the Postal Address table. </value>
		public const String FLD_FIRST_NAME = "first_name";
		/// <value>The constant used for Address Name field in the Postal Address table. </value>
		public const String FLD_LAST_NAME = "last_name";
		/// <value>The constant used for Address1 field in the Postal Address table. </value>
		public const String FLD_ADDRESS1 = "address1";		
		/// <value>The constant used for Address2 field field in the Postal Address table. </value>
		public const String FLD_ADDRESS2 = "address2";
		/// <value>The constant used for City field in the Postal Address table. </value>
		public const String FLD_CITY = "city";
		/// <value>The constant used for County field in the Postal Address table. </value>
		public const String FLD_COUNTY = "county";
		/// <value>The constant used for Zip field in the Postal Address table. </value>
		public const String FLD_ZIP = "zip";
		/// <value>The constant used for Subdivion Code field in the Postal Address table. </value>
		public const String FLD_SUBDIVISION_CODE = "subdivision_code";	
		/// <value>The constant used for Subdivion Name field in the Postal Address table. </value>
		public const String FLD_SUBDIVISION_NAME_1 = "subdivision_name_1";
		/// <value>The constant used for Country Code field in the Postal Address table. </value>
		public const String FLD_COUNTRY_CODE = "country_code";
		/// <value>The constant used for Country Name field in the Postal Address table. </value>
		public const String FLD_COUNTRY_NAME = "country_name";
		/// <value>The constant used for the order "Source ID" field in the Order table. </value>
		public const String FLD_RESIDENTIAL_AREA = "residential_area";	
		


		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";


            
		public PostalAddressEntityTable() : base(TBL_POSTAL_ADDRESS_ENTITY) 
		{
			 this.InitClass();
		}
        
		public PostalAddressEntityTable(DataTable table) : base(table.TableName) 
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
		public PostalAddressEntityTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}	
            
            
		public System.Collections.IEnumerator GetEnumerator() 
		{
			return this.Rows.GetEnumerator();
		}
            
		public override DataTable Clone() 
		{
			PostalAddressEntityTable cln = ((PostalAddressEntityTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
            
		protected override DataTable CreateInstance() 
		{
			return new PostalAddressEntityTable();
		}
            


		internal void InitVars() 
		{
			
		}
            
		private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_POSTAL_ADDRESS_ENTITY;
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
			
			columns.Add(FLD_ADDRESS_ID, typeof(System.Int32));
			columns.Add(FLD_ENTITY_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ENTITY_ID, typeof(System.Int32));	
			columns.Add(FLD_TYPE, typeof(System.Int32));
			columns.Add(FLD_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_TITLE, typeof(System.String));
			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_FIRST_NAME, typeof(System.String));
			columns.Add(FLD_LAST_NAME, typeof(System.String));
			columns.Add(FLD_ADDRESS1, typeof(System.String));	
			columns.Add(FLD_ADDRESS2, typeof(System.String));
			columns.Add(FLD_CITY, typeof(System.String));
			columns.Add(FLD_COUNTY, typeof(System.String));
			columns.Add(FLD_ZIP, typeof(System.String));
			columns.Add(FLD_SUBDIVISION_CODE, typeof(System.String));
			columns.Add(FLD_SUBDIVISION_NAME_1, typeof(System.String));
			columns.Add(FLD_COUNTRY_CODE, typeof(System.String));
			columns.Add(FLD_COUNTRY_NAME, typeof(System.String));
			columns.Add(FLD_RESIDENTIAL_AREA, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));


            this.PrimaryKey = new DataColumn[] { this.Columns[FLD_PKID], this.Columns[FLD_ENTITY_ID], this.Columns[FLD_ENTITY_TYPE_ID], this.Columns[FLD_TYPE]};


		}

	}
}
