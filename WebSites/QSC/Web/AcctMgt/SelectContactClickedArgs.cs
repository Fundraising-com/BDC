using System;
using Business.Objects;
using QSPFulfillment.AcctMgt.Control;

namespace QSPFulfillment.AcctMgt
{
	/// <summary>
	/// Summary description for SelectContactClickedArgs.
	/// </summary>
	public class SelectContactClickedArgs:System.EventArgs
	{
		private Contact contact;

		public SelectContactClickedArgs(Contact oContact)
		{
			this.contact = oContact;
		}
		
		public Contact oContact
		{
			get{return this.contact;}
		}
	}
}
