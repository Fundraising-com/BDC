using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of EmailEntityTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type EmailEntityTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class EntityTypeTable : DataTable, System.Collections.IEnumerable
	{
		public const String TBL_EMAIL_ENTITY = "entity_type";
		
		public const String FLD_ENTITY_TYPE_ID = "entity_type_id";				

		public const String FLD_ENTITY_TYPE_NAME = "entity_type_name";

		public System.Collections.IEnumerator GetEnumerator() 
		{
			return this.Rows.GetEnumerator();
		}
	}
	
}
