using System;
using System.Data;
using Common;
using Business.Objects;
using Common.TableDef;

namespace Business.Rules
{
	/// <summary>
	/// Validates required fields
	/// </summary>
	public class ContactPrimaryUnicity : RulesBase
	{
		public ContactPrimaryUnicity(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			bool IsValid = true;
			ContactDataSet.ContactRow contactRow = row as ContactDataSet.ContactRow;
			Business.Objects.Contact accountContacts = null;

			if(contactRow != null && contactRow.CAccountID != 0 && contactRow.TypeId == Convert.ToInt32(ContactTypeID.Primary))
			{
				accountContacts = new Business.Objects.Contact(contactRow.CAccountID, this.CurrentTransaction);

				if(accountContacts.dataSet.Contact.Select("TypeId = " + ((int) ContactTypeID.Primary).ToString() + " AND Id <> " + contactRow.Id.ToString()).Length > 0) 
				{
					CurrentMessageManager.Add(Message.ERRMSG_CONTACT_PRIMARY_UNICITY_0);
					IsValid = false;
				}
			}

			return IsValid;
		}
	}
}
