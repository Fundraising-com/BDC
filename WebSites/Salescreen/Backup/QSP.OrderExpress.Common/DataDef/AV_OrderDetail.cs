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
	public class AV_OrderDetailTable : DataTable, System.Collections.IEnumerable
	{		
		
		public const string TBL_ORDER_DETAIL = "AV_OrderDetail";
		//public const string FLD_PKID = "OrderID";
		public const string FLD_ACCOUNTTRACK_UNIQUE_ID = "AccountTrackUniqueID";
		public const string FLD_AS_400_NUMBER = "AS400Number";
		public const string FLD_ORDER_STATUS = "OrderStatus";
		public const string FLD_PERSONALIZATION_ID = "PersonalizationID";
		public const string FLD_DETAIL_ID = "DetailID";
        public const string FLD_PRODUCT_CODE = "ProductCode";
		public const string FLD_FSM_ID = "FSM_ID";
		
		public AV_OrderDetailTable() : 
			base(TBL_ORDER_DETAIL) 
		{
			this.InitClass();
		}
		    
		public AV_OrderDetailTable(DataTable table) : 
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
		public AV_OrderDetailTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		

		public System.Collections.IEnumerator GetEnumerator() 
		{
			return this.Rows.GetEnumerator();
		}
        
		public override DataTable Clone() 
		{
			AV_OrderDetailTable cln = ((AV_OrderDetailTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
        
		protected override DataTable CreateInstance() 
		{
			return new AV_OrderDetailTable();
		}
        
		internal void InitVars() 
		{
            
		}
        
		private void InitClass() 
		{			
			//
			// Create the Users table
			//
			this.TableName = TBL_ORDER_DETAIL;
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
			columns.Add(FLD_ACCOUNTTRACK_UNIQUE_ID,typeof(System.Int32));
			columns.Add(FLD_AS_400_NUMBER,typeof(System.String));
			columns.Add(FLD_ORDER_STATUS,typeof(System.Int32));
			columns.Add(FLD_PERSONALIZATION_ID,typeof(System.Int32));
			columns.Add(FLD_DETAIL_ID, typeof(System.Int32));
			columns.Add(FLD_FSM_ID, typeof(System.String));
		}

		
	}
}
