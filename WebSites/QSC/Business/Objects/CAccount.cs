namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.CAccountDataSet;
	using dataAccessRef = DAL.CAccountData;

	public enum CAccountStatus 
	{
		Active = 35001,
		Inactive = 35002,
		Pending = 35003
	}

	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class CAccount : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		private Address oAddress;

		public CAccount() 
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public CAccount(Transaction CurrentTransaction) : this() 
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public CAccount(int ID, Transaction CurrentTransaction) : this(CurrentTransaction)
		{
			GetOneById(ID);
		}

		public dataSetRef dataSet 
		{
			get 
			{
				return this.dtsDataSet;
			}
		}

		public Address AddressList 
		{
			get 
			{
				return this.oAddress;
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
				return this.dtsDataSet.CAccount.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public bool Save()
		{			
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return base.UpdateBatch();
		}

		public void GetOneById(Int32 Id)
		{
			try
			{
				dataAccess.SelectOne(dataSet, DefaultTableName, Id);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetOneByIdWithChildren(Int32 Id)
		{
			dataSetRef.CAccountRow rowSelected;

			try
			{
				dataAccess.SelectOne(dataSet, DefaultTableName, Id);

				rowSelected = dataSet.CAccount.FindById(Id);

				if(rowSelected != null) 
				{
					oAddress = new Address(rowSelected.AddressListID, this.CurrentTransaction);
				}
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void PopulatePayLaterAccount() 
		{
			try 
			{
				dataAccess.PopulatePayLaterAccount(dataSet.CAccount[0].Id);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}
	}
}