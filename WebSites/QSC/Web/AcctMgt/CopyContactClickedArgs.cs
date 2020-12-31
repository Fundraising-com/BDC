using System;
using Business.Objects;
using QSPFulfillment.AcctMgt.Control;

namespace QSPFulfillment.AcctMgt
{
	/// <summary>
	/// Summary description for SelectContactClickedArgs.
	/// </summary>
	public class CopyContactClickedArgs:System.EventArgs
	{
		private Contact contact;
		private ContactSelectionType type;

		public CopyContactClickedArgs(Contact oContact, ContactSelectionType oType)
		{
			this.contact = oContact;
			this.type = oType;
		}
		
		public Contact oContact
		{
			get{return this.contact;}
		}

		public ContactSelectionType oType 
		{
			get{return this.type;}
		}
	}
}
