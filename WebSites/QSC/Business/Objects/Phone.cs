namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.PhoneDataSet;
	using dataAccessRef = DAL.PhoneData;
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class Phone : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public Phone() 
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public Phone(Transaction CurrentTransaction) : this() 
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public Phone(int PhoneListID, Transaction CurrentTransaction) : this(CurrentTransaction) 
		{
			this.GetAllByPhoneListID(PhoneListID);
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
				return this.dtsDataSet.Phone.TableName;
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

		public void GetAllByPhoneListID(int PhoneListID)
		{
			try
			{
				dataAccess.SelectAllByPhoneListID(dtsDataSet, DefaultTableName, PhoneListID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void CompareUpdate(int PhoneListIDTo) 
		{
			dataSetRef.PhoneRow row;
			Phone oPhone = new Phone();
			if(this.CurrentTransaction != null) 
			{
				oPhone.CurrentTransaction = this.CurrentTransaction;
			}

			oPhone.GetAllByPhoneListID(PhoneListIDTo);

			if(oPhone.dataSet.Phone.Count > this.dataSet.Phone.Count) 
			{
				for(int i = this.dataSet.Phone.Count; i < oPhone.dataSet.Phone.Count; i++) 
				{
					oPhone.dataSet.Phone[i].Delete();
				}
			}

			for(int i = 0; i < this.dataSet.Phone.Count; i++) 
			{
				if(oPhone.dataSet.Phone.Count > i) 
				{
					row = oPhone.dataSet.Phone[i];
				} 
				else 
				{
					row = oPhone.dtsDataSet.Phone.NewPhoneRow();
				}

				CopyPhoneRow(this.dataSet.Phone[i], row);

				if(oPhone.dataSet.Phone.Count <= i) 
				{
					row.PhoneListID = PhoneListIDTo;

					oPhone.dataSet.Phone.AddPhoneRow(row);
				}
			}

			oPhone.Save();
		}

		private void CopyPhoneRow(dataSetRef.PhoneRow rowFrom, dataSetRef.PhoneRow rowTo) 
		{
			rowTo.Type = rowFrom.Type;
			rowTo.PhoneNumber = rowFrom.PhoneNumber;
			rowTo.BestTimeToCall = rowFrom.BestTimeToCall;
		}

		public PhoneDataSet.PhoneRow AddDefaultCampaignContactPhone() 
		{
			return dataSet.Phone.AddPhoneRow(Convert.ToInt32(dataSet.Phone.TypeColumn.DefaultValue), 0, String.Empty, String.Empty);
		}
	}
}