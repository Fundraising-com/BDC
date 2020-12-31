namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.AddressDataSet;
	using dataAccessRef = DAL.AddressData;

	public enum AddressType 
	{
		Undefined = 54000,
		ShipTo = 54001,
		BillTo = 54002,
		Secondary = 54003,
		Home = 54004,
		SupplyAddress = 54005,
		ContactAddress = 54006
	}

	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class Address : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		private CodeDetail oCodeDetailType;
		private Province oProvince;

		public Address() 
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public Address(Transaction CurrentTransaction) : this()
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public Address(int AddressListID, Transaction CurrentTransaction) : this(CurrentTransaction)
		{
			this.GetAllByAddressListID(AddressListID);
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
				return this.dtsDataSet.Address.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public CodeDetail CodeDetailType 
		{
			get 
			{
				return this.oCodeDetailType;
			}
		}

		public Province AddressProvince 
		{
			get 
			{
				return this.oProvince;
			}
		}

		public bool Save()
		{			
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return base.UpdateBatch();
		}

		public void GetAllByAddressListID(int AddressListID)
		{
			try
			{
				dataAccess.SelectAllByAddressListID(dtsDataSet, DefaultTableName,AddressListID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetOneByID(int AddressID)
		{
			try
			{
				dataAccess.SelectOne(dataSet, DefaultTableName, AddressID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetOneByIDWithChildren(int AddressID)
		{
			dataSetRef.AddressRow rowSelected;

			try
			{
				dataAccess.SelectOne(dtsDataSet, DefaultTableName, AddressID);
				
				rowSelected = dataSet.Address.FindByaddress_id(AddressID);

				if(rowSelected != null) 
				{
					oCodeDetailType = new CodeDetail();
					oCodeDetailType.GetOneByID(rowSelected.address_type);

					oProvince = new Province(rowSelected.stateProvince);
				}
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public dataSetRef.AddressRow GetOneByType(AddressType addressType) 
		{
			DataView dv;
			dataSetRef.AddressRow row;

			dv = new DataView(this.dtsDataSet.Address, "address_type = " + ((int) addressType).ToString(), "", DataViewRowState.CurrentRows);
			
			if(dv.Count == 0) 
			{
				row = this.dtsDataSet.Address.NewAddressRow();
			} 
			else 
			{
				row = (dataSetRef.AddressRow) dv[0].Row;
			}

			return row;
		}

		public int Clone(int ID) 
		{
			int NewAddressID = 0;
			bool bIsSuccess = true;
			DataView dv;
			dataSetRef.AddressRow rowFrom;
			dataSetRef.AddressRow rowTo = null;

			try 
			{
				dv = new DataView(this.dataSet.Address, "address_id = " + ID.ToString(), "", DataViewRowState.CurrentRows);

				if(dv.Count != 0) 
				{
					rowFrom = (dataSetRef.AddressRow) dv[0].Row;
					rowTo = this.dataSet.Address.NewAddressRow();

					CopyAddressRow(rowFrom, rowTo);

					this.dataSet.Address.AddAddressRow(rowTo);

					bIsSuccess &= base.Insert();
				}

				if(bIsSuccess && rowTo != null) 
				{
					NewAddressID = rowTo.address_id;
				} 
				else 
				{
					NewAddressID = 0;
				} 
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}

			return NewAddressID;
		}

		private void CopyAddressRow(dataSetRef.AddressRow rowFrom, dataSetRef.AddressRow rowTo) 
		{
			rowTo.street1 = rowFrom.street1;
			rowTo.street2 = rowFrom.street2;
			rowTo.city = rowFrom.city;
			rowTo.stateProvince = rowFrom.stateProvince;
			rowTo.postal_code = rowFrom.postal_code;
			rowTo.zip4 = rowFrom.zip4;
			rowTo.country = rowFrom.country;
			rowTo.address_type = rowFrom.address_type;
			rowTo.AddressListID = rowFrom.AddressListID;
		}

		public AddressDataSet.AddressRow AddDefaultCampaignContactAddress() 
		{
			return dataSet.Address.AddAddressRow(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "CA", Convert.ToInt32(AddressType.ContactAddress), 0);
		}
	}
}