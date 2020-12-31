namespace Business.Objects
{
	using System;
	using System.Data;
	using Business.Rules;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.ContactDataSet;
	using dataAccessRef = DAL.ContactData;

	public enum ContactTypeID
	{
		Default = -5,
		Secretary = 1,
		Teacher = 2,
		Principal = 3,
		Parent = 5,
		FundraisingCoordinator = 8,
		Primary = 10
	}

	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class Contact : BusinessSystem
	{
		internal const string DEFAULT_CONTACT_FIRST_NAME = "Fundraising";
		internal const string DEFAULT_CONTACT_LAST_NAME = "Coordinator";
		internal const string DEFAULT_FRENCH_CONTACT_FIRST_NAME = "Coordonnateur(trice)";
		internal const string DEFAULT_FRENCH_CONTACT_LAST_NAME = "de levées de fonds";

		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public Contact() : base()
		{
			this.dtsDataSet =  new dataSetRef();
			CreateRulesCollection();
		}

		public Contact(Message messageManager) : base(messageManager) 
		{
			this.dtsDataSet =  new dataSetRef();
			CreateRulesCollection();
		}

		public Contact(Transaction currentTransaction) : this()
		{
			this.CurrentTransaction = currentTransaction;
		}

		public Contact(Message messageManager, Transaction currentTransaction) : this(messageManager) 
		{
			this.CurrentTransaction = currentTransaction;
		}

		public Contact(int accountID) : this()
		{
			GetAllByAccountID(accountID);
		}

		public Contact(int accountID, Message messageManager) : this(messageManager) 
		{
			GetAllByAccountID(accountID);
		}

		public Contact(int accountID, Transaction currentTransaction) : this(currentTransaction) 
		{
			GetAllByAccountID(accountID);
		}

		public Contact(int accountID, Message messageManager, Transaction currentTransaction) : this(messageManager, currentTransaction) 
		{
			GetAllByAccountID(accountID);
		}

		public dataSetRef dataSet 
		{
			get 
			{
				return this.dtsDataSet;
			}
		}

		internal override DataSet baseDataSet
		{
			get
			{
				return (DataSet) this.dtsDataSet;
			}
		}

		public override string DefaultTableName
		{
			get
			{
				return this.dtsDataSet.Contact.TableName;
			}
		}


		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}
		
		public void GetAllByAccountID(int AccountID)
		{
			try
			{
				dataAccess.SelectAllByAccountID(dtsDataSet, DefaultTableName, AccountID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetOneByID(int ID) 
		{
			try 
			{
				dataAccess.SelectOne(dtsDataSet, DefaultTableName, ID);
			} 
			catch(Exception ex)
			{
				ManageError(ex);
			}
		}

		public void GetLastByAccountID(int accountID) 
		{
			GetLastByAccountID(accountID, false, String.Empty);
		}

		public void GetLastByAccountID(int accountID, bool forceDefault, string language) 
		{
			try 
			{
				dataAccess.SelectOneLastByAccountID(dtsDataSet, DefaultTableName, accountID);

				if(dataSet.Contact.Count == 0 && forceDefault) 
				{
					AddDefaultCampaignContact(language);
				}
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		public bool Save()
		{			
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return base.UpdateBatch();
		}

		public bool SaveWithoutValidation() 
		{
			try 
			{
				ContactToPhoneListIntegrity contactToPhoneListIntegrity = new ContactToPhoneListIntegrity(CurrentMessageManager);
				DataView dataView = new DataView(this.dataSet.Contact, String.Empty, String.Empty, DataViewRowState.Added | DataViewRowState.ModifiedCurrent);

				contactToPhoneListIntegrity.CurrentTransaction = this.CurrentTransaction;

				foreach(DataRowView dataRowView in dataView) 
				{
					contactToPhoneListIntegrity.Validate(dataRowView.Row);
				}

				return (Convert.ToBoolean(DataAccessReference.UpdateBatch(baseDataSet, DefaultTableName)));
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
				return false;
			}
		}

		public bool Validate() 
		{
			return base.ValidateUpdateBatch();
		}

		public int Clone(int ID) 
		{
			return Clone(ID, -1);
		}

		public int Clone(int ID, int AccountID) 
		{
			int NewContactID = 0;
			bool bIsSuccess = true;
			DataView dv;
			dataSetRef.ContactRow rowFrom;
			dataSetRef.ContactRow rowTo = null;

			Address oAddress;
			PhoneList oPhoneList;

			try 
			{
				dv = new DataView(this.dataSet.Contact, "Id = " + ID.ToString(), "", DataViewRowState.CurrentRows);

				if(dv.Count != 0)
				{
					rowFrom = (dataSetRef.ContactRow) dv[0].Row;
					rowTo = this.dataSet.Contact.NewContactRow();

					CopyContactRow(rowFrom, rowTo);

					if(AccountID != -1) 
					{
						rowTo.CAccountID = AccountID;
						rowTo.ContactListID = 0;
					}

					oAddress = new Address();
					if(this.CurrentTransaction != null) 
					{
						oAddress.CurrentTransaction = this.CurrentTransaction;
					}

					if(rowFrom.AddressID != 0) 
					{
						oAddress.GetOneByID(rowFrom.AddressID);
					} 
					else 
					{
						rowFrom.AddressID = oAddress.AddDefaultCampaignContactAddress().address_id;
					}

					oPhoneList = new PhoneList();
					if(this.CurrentTransaction != null) 
					{
						oPhoneList.CurrentTransaction = this.CurrentTransaction;
					}

					if(rowFrom.PhoneListID != 0) 
					{
						oPhoneList.GetOneByID(rowFrom.PhoneListID);
					}

					rowTo.AddressID = oAddress.Clone(rowFrom.AddressID);
					rowTo.PhoneListID = oPhoneList.Clone(rowFrom.PhoneListID);

					if(rowTo.AddressID != 0 && rowTo.PhoneListID != 0 && rowTo != null) 
					{
						this.dataSet.Contact.AddContactRow(rowTo);

						bIsSuccess &= base.Insert();
					} 
					else 
					{
						bIsSuccess = false;
					}
				} 

				if(bIsSuccess && rowTo != null) 
				{
					NewContactID = rowTo.Id;
				} 
				else 
				{
					NewContactID = 0;
				}
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}

			return NewContactID;
		}

		private void CopyContactRow(dataSetRef.ContactRow rowFrom, dataSetRef.ContactRow rowTo) 
		{
			rowTo.ContactListID = rowFrom.ContactListID;
			rowTo.CAccountID = rowFrom.CAccountID;
			rowTo.Title = rowFrom.Title;
			rowTo.FirstName = rowFrom.FirstName;
			rowTo.LastName = rowFrom.LastName;
			rowTo.MiddleInitial = rowFrom.MiddleInitial;
			rowTo.Function = rowFrom.Function;
			rowTo.Email = rowFrom.Email;
			rowTo.DeletedTF = rowFrom.DeletedTF;
			rowTo.DateChanged = DateTime.Now;
		}

		public bool LinkToCampaign(int CampaignID, int ShipToContactID, int BillToContactID) 
		{
			bool IsValid = false;
			Campaign ca;

			try 
			{
				ca = new Campaign();
				if(this.CurrentTransaction != null) 
				{
					ca.CurrentTransaction = this.CurrentTransaction;
				}

				ca.GetOneByID(CampaignID);

				if(ca.dataSet.Campaign.Rows.Count == 1) 
				{
					if(dataSet.Contact.FindById(ShipToContactID) != null &&
						dataSet.Contact.FindById(BillToContactID) != null) 
					{
						ca.dataSet.Campaign[0].ShipToCampaignContactID = ShipToContactID;
						ca.dataSet.Campaign[0].BillToCampaignContactID = BillToContactID;

						ca.Save();
						IsValid = true;
					}
				}

				if(IsValid) 
				{
					SaveCampaignToAccount(ca.dataSet.Campaign[0].ShipToAccountID);
				}

				if(!IsValid) 
				{
					throw new Exception();
				}
			} 
			catch (Exception ex) 
			{
				ManageError(ex);
			}

			return IsValid;
		}

		public void SaveCampaignToAccount(int AccountID) 
		{
			Phone oPhone;
			dataSetRef.ContactRow rowAccountContact;
			Contact oContact = new Contact();
			if(this.CurrentTransaction != null) 
			{
				oContact.CurrentTransaction = this.CurrentTransaction;
			}

			oContact.GetAllByAccountID(AccountID);

			if(this.dataSet.Contact.Count == 1 || this.dataSet.Contact.Count == 2) 
			{
				foreach(dataSetRef.ContactRow rowCampaignContact in this.dataSet.Contact) 
				{
					rowAccountContact = oContact.Find(rowCampaignContact);

					if(rowAccountContact != null) 
					{
						CopyContactRow(rowCampaignContact, rowAccountContact);
				
						rowAccountContact.ContactListID = 0;
						rowAccountContact.CAccountID = AccountID;

						oContact.Save();

						oPhone = new Phone();
						if(this.CurrentTransaction != null) 
						{
							oPhone.CurrentTransaction = this.CurrentTransaction;
						}

						oPhone.GetAllByPhoneListID(rowCampaignContact.PhoneListID);
						oPhone.CompareUpdate(rowAccountContact.PhoneListID);
					} 
					else
					{
						oContact.GetOneByID(rowCampaignContact.Id);
						oContact.Clone(rowCampaignContact.Id, AccountID);
					}
				}
			}
		}

		public dataSetRef.ContactRow Find(dataSetRef.ContactRow rowSearch) 
		{
			dataSetRef.ContactRow rowFound = null;

			foreach(dataSetRef.ContactRow row in this.dataSet.Contact) 
			{
				if(rowFound == null) 
				{
					if(row.FirstName == rowSearch.FirstName && row.LastName == rowSearch.LastName) 
					{
						rowFound = row;
					}
				}
			}

			return rowFound;
		}

		public ContactDataSet.ContactRow AddDefaultCampaignContact(string language) 
		{
			return dataSet.Contact.AddContactRow(0, 0, String.Empty, GetDefaultContactFirstName(language), GetDefaultContactLastName(language), String.Empty, Convert.ToInt32(ContactTypeID.Default), String.Empty, String.Empty, 0, 0, false, DateTime.Now);
		}

		public static string GetDefaultContactFirstName(string language) 
		{
			string defaultContactFirstName = String.Empty;

			if(language == "FR") 
			{
				defaultContactFirstName = DEFAULT_FRENCH_CONTACT_FIRST_NAME;
			} 
			else 
			{
				defaultContactFirstName = DEFAULT_CONTACT_FIRST_NAME;
			}

			return defaultContactFirstName;
		}

		public static string GetDefaultContactLastName(string language) 
		{
			string defaultContactLastName = String.Empty;

			if(language == "FR") 
			{
				defaultContactLastName = DEFAULT_FRENCH_CONTACT_LAST_NAME;
			} 
			else 
			{
				defaultContactLastName = DEFAULT_CONTACT_LAST_NAME;
			}

			return defaultContactLastName;
		}
	}
}