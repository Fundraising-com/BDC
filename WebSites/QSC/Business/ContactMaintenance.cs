using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	/// <summary>
	/// Summary description for ContactMaintenance.
	/// </summary>
	public class ContactMaintenance : QBusinessObject
	{
		#region Class Members

		private DAL.ContactDataAccess aTable;

		private bool	ValidGUIM;
		///<summary>Gets or sets value indicating if the CA has passsed user interface level validation</summary>
		public bool		ValidGUI	{ get{ return this.ValidGUIM; } set{this.ValidGUIM = value; } }

		private bool	ValidBIM;
		///<summary>Gets or sets value indicating if the CA has passsed biz intelligence level validation</summary>
		public bool		ValidBI		{ get{ return this.ValidBIM;  } set{this.ValidBIM  = value; } }

		private string	ErrorGUIM;
		///<summary>Gets or sets error string associated with user interface level validation</summary>
		public string	ErrorGUI	{ get{ return this.ErrorGUIM; } set{this.ErrorGUIM = value; } }

		private string	ErrorBIM;
		///<summary>Gets or sets error string associated with biz intelligence level validation</summary>
		public string	ErrorBI		{ get{ return this.ErrorBIM;  } set{this.ErrorBIM  = value; } }

		private System.Collections.ArrayList ContactsM = new System.Collections.ArrayList(20);

		///<summary>Method to Add a Contact Record to the maintenance group</summary>
		///<param name="Input">a Business.CampaignProgramBrochure instance</param>
		public void AddContact(Business.Contact Input)
		{
			ContactsM.Add(Input);
		}
		#endregion  Class Members

		#region Constructors
		public ContactMaintenance()
		{
			aTable = new DAL.ContactDataAccess();
		}
		#endregion Constructors

		#region ValidateAndSave
		///<summary>check it then submit it</summary>
		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
				//this.ErrorBI = "this.Validate() returned true, lets try a save !<br>" + this.ErrorBI;
				return this.Save();
			}
			else
			{
				return false;
			}
		}
		///<summary>Check for compliance with biz rules</summary>
		public bool Validate()
		{
			/* setup variables to track validation */
			bool blValid = true;
			//string stError = "";

			return blValid;
		}
		///<summary>Save a Contact to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		public bool Save()
		{
			return ( Save_ContactMaintenance() );
		}

		private bool Save_ContactMaintenance()
		{
			return false;
		}

		///<summary>Save the Contact to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		private bool Save_Contact()
		{
			bool blSave = false;
//			this.ErrorBI += "this.Save_Contact() ContactIDM (before):" + this.ContactIDM.ToString() + "\r\n";
//			if(this.ContactIDM == -1)
//			{
//			}
//			else
//			{
//			}
			return blSave;
		}
		#endregion ValidateAndSave

		#region Populate Functions
		public System.Collections.ArrayList GetContactsByAccountID(int AccountID)
		{
			DataTable DT = aTable.GetContactsByAccountID(AccountID);
			ConvertDataTableToContacts(DT);
			return ContactsM;
		}

		//private vs public, this is un-used and "undocumented"
		private System.Collections.ArrayList GetContactsByContactList(int ContactListID)
		{
			DataTable DT = aTable.GetContactsByContactList(ContactListID);
			ConvertDataTableToContacts(DT);
			return ContactsM;
		}

		private void ConvertDataTableToContacts(DataTable DT)
		{
			Business.Contact aContact;
			for (int i = 0; i < DT.Rows.Count; i++)
			{
				aContact = new Business.Contact();
				aContact.ContactID			= Convert.ToInt32( DT.Rows[i]["ContactID"]);
				aContact.ContactListID		= Convert.ToInt32( DT.Rows[i]["ContactListID"]);
				aContact.CAccountID			= Convert.ToInt32( DT.Rows[i]["CAccountID"]);
				aContact.Title				= Convert.ToString(DT.Rows[i]["Title"]);
				aContact.FirstName			= Convert.ToString(DT.Rows[i]["FirstName"]);
				aContact.LastName			= Convert.ToString(DT.Rows[i]["LastName"]);
				//aContact.TypeId				= Convert.ToInt32( DT.Rows[i]["TypeId"]);
				aContact.Function			= Convert.ToString(DT.Rows[i]["Function"]);
				aContact.Email				= Convert.ToString(DT.Rows[i]["Email"]);
				aContact.AddressID			= Convert.ToInt32( DT.Rows[i]["AddressID"]);
				aContact.PhoneListID		= Convert.ToInt32( DT.Rows[i]["PhoneListID"]);
				this.AddContact(aContact);
			}
		}
		#endregion Populate Functions
	}
}
