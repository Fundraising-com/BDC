using System;
using efundraising.Core;


namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for EFundraisingCRMObject.
	/// </summary>
	public abstract class EFundraisingCRMObject : BusinessBase, IComparable
	{
		public EFundraisingCRMObject()
		{
			
		}

		// check if the object is the exact same object type
		protected virtual bool CheckObjectIntegrity(object obj, Type mtype) {
			return (obj.GetType().AssemblyQualifiedName == mtype.AssemblyQualifiedName);
		}
		
		
		#region IComparable Members

		public virtual int CompareTo(object obj)
		{
			// TODO:  Add BusinessBase.CompareTo implementation
			return DoCompare(obj);
		}

		#endregion
	}
}
