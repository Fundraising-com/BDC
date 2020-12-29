using System;
using GA.BDC.Core.BusinessBase;


namespace GA.BDC.Core.eFundraisingStore
{
	/// <summary>
	/// Summary description for EFundraisingCRMObject.
	/// </summary>
    public class eFundraisingStoreObject : GA.BDC.Core.BusinessBase.BusinessBase
	{
		public eFundraisingStoreObject()
		{
			
		}

		// check if the object is the exact same object type
		protected virtual bool CheckObjectIntegrity(object obj, Type mtype) {
			return (obj.GetType().AssemblyQualifiedName == mtype.AssemblyQualifiedName);
		}
	}
}
