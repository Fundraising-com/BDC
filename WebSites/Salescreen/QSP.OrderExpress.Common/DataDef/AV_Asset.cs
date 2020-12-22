using System;
using System.Data;
using System.Runtime.Serialization;
    

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of UserData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type UserData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class AV_AssetTable : DataTable, System.Collections.IEnumerable
	{		
		
		public const string TBL_ASSET = "AV_Asset";
		public const string FLD_ASSET_ID = "AssetID";
		public const string FLD_ASSET_PATH= "AssetPath";
		public const string FLD_ASSET_URL= "AssetURL";
		public const string FLD_ASSET_HR_FILE_EXTENSION= "AssetHRFileExtension";
		public const string FLD_ASSET_NAME = "AssetName";
		public const string FLD_ASSET_DESCRIPTION = "AssetDescription";
		public const string FLD_ASSET_CATEGORY = "AssetCategory";
		
		public AV_AssetTable() : 
			base(TBL_ASSET) 
		{
			this.InitClass();
		}
		    
		public AV_AssetTable(DataTable table) : 
			base(table.TableName) 
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
		public AV_AssetTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		

		public System.Collections.IEnumerator GetEnumerator() 
		{
			return this.Rows.GetEnumerator();
		}
        
		public override DataTable Clone() 
		{
			AV_AssetTable cln = ((AV_AssetTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
        
		protected override DataTable CreateInstance() 
		{
			return new AV_AssetTable();
		}
        
		internal void InitVars() 
		{
            
		}
        
		private void InitClass() 
		{			
			//
			// Create the Users table
			//
			this.TableName = TBL_ASSET;
			DataColumnCollection columns = this.Columns;
        
			/* NO PKID
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;            
			*/

			//columns.Add(FLD_PKID,typeof(System.Int32));
			columns.Add(FLD_ASSET_ID,typeof(System.Int32));
			columns.Add(FLD_ASSET_PATH,typeof(System.String));
			columns.Add(FLD_ASSET_URL,typeof(System.String));
			columns.Add(FLD_ASSET_HR_FILE_EXTENSION,typeof(System.String));
			columns.Add(FLD_ASSET_NAME,typeof(System.String));
			columns.Add(FLD_ASSET_DESCRIPTION,typeof(System.String));
			columns.Add(FLD_ASSET_CATEGORY,typeof(System.Int32));
		}

		
	}
}
