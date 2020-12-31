namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.PhoneListDataSet;
	using dataAccessRef = DAL.PhoneListData;
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class PhoneList : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public PhoneList() 
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public PhoneList(Transaction CurrentTransaction) : this() 
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public PhoneList(int ID, Transaction CurrentTransaction) : this(CurrentTransaction) 
		{
			GetOneByID(ID);
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
				return this.dtsDataSet.PhoneList.TableName;
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

		public void GetOneByID(Int32 ID)
		{
			try
			{
				dataAccess.SelectOne(dtsDataSet, DefaultTableName, ID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public int Clone(int PhoneListID) 
		{
			int NewPhoneListID = 0;
			bool bIsSuccess = true;
			PhoneDataSet.PhoneRow rowTo = null;
			dataSetRef.PhoneListRow rowPhoneList = null;
			int PhoneCount = 0;

			Phone oPhoneFrom;
			Phone oPhoneTo;

			try 
			{
				oPhoneFrom = new Phone(this.CurrentTransaction);

				if(PhoneListID != 0) 
				{
					oPhoneFrom.GetAllByPhoneListID(PhoneListID);
				} 
				else 
				{
					oPhoneFrom.AddDefaultCampaignContactPhone();
				}

				PhoneCount = oPhoneFrom.dataSet.Phone.Count;
				if(PhoneCount != 0) 
				{
					oPhoneTo = new Phone(this.CurrentTransaction);

					for(int i = 0; i < PhoneCount; i++) 
					{
						rowTo = oPhoneTo.dataSet.Phone.NewPhoneRow();

						CopyPhoneRow(oPhoneFrom.dataSet.Phone[i], rowTo);

						oPhoneTo.dataSet.Phone.AddPhoneRow(rowTo);
					}

					bIsSuccess &= oPhoneTo.Save();

					if(bIsSuccess && rowTo != null) 
					{
						NewPhoneListID = rowTo.PhoneListID;
					} 
					else 
					{
						NewPhoneListID = 0;
					}
				} 
				else 
				{
					rowPhoneList = this.dataSet.PhoneList.AddPhoneListRow(DateTime.Now, false);
					bIsSuccess &= base.Insert();

					if(bIsSuccess) 
					{
						NewPhoneListID = rowPhoneList.ID;
					} 
					else 
					{
						NewPhoneListID = 0;
					}
				}
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}

			return NewPhoneListID;
		}

		private void CopyPhoneRow(PhoneDataSet.PhoneRow rowFrom, PhoneDataSet.PhoneRow rowTo) 
		{
			rowTo.Type = rowFrom.Type;
			rowTo.PhoneListID = 0;
			rowTo.PhoneNumber = rowFrom.PhoneNumber;
			rowTo.BestTimeToCall = rowFrom.BestTimeToCall;
		}
	}
}