	using System;
	using System.Data;
	using System.Runtime.Serialization;
	namespace QSP.WebControl.DataAccess.Common.TableDef
	{
		/// <summary>
		/// Summary description for CommonTable.
		/// </summary>
		/// 
		[System.ComponentModel.DesignerCategory("Code")]
		[SerializableAttribute] 
		internal class CommonTable:DataTable
		{
			public CommonTable()
			{
				BuildDataTable();
			}
			public CommonTable(SerializationInfo info, StreamingContext context) : base(info, context) 
			{	
			}
			//----------------------------------------------------------------
			// Sub BuildDataTable:
			//   Creates the following datatable: Collection Days
			//----------------------------------------------------------------
			private void BuildDataTable()
			{
			}
		}
	}