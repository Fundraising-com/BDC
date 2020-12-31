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
	/// Summary description for CatalogSection.
	/// </summary>
	public class BankDeposit : QBusinessObject
	{
		#region Class Members
		// Column fields

		protected string IDM ;
		[DAL.DataColumn("Bank_Deposit_Id")]
		public string Bank_Deposit_Id
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}
		
		protected string DepositDateM;
		[DAL.DataColumn("Deposit_Date")]
		public string DepositDate
		{
			get{ return this.DepositDateM; }
			set{ this.DepositDateM = value; }
		}
		protected string DepositDateToM;
		[DAL.DataColumn("Deposit_Date")]
		public string DepositDateTo
		{
			get{ return this.DepositDateToM; }
			set{ this.DepositDateToM = value; }
		}

		protected string DepositAmountM;
		[DAL.DataColumn("Deposit_Amount")]
		public string DepositAmount
		{
			get{ return this.DepositAmountM; }
			set{ this.DepositAmountM = value; }
		}

		protected string DepositStatusIdM;
		[DAL.DataColumn("Bank_Deposit_Status_Id")]
		public string DepositStatusId
		{
			get{ return this.DepositStatusIdM; }
			set{ this.DepositStatusIdM = value; }
		}

		protected string DepositAccountIdM;
		[DAL.DataColumn("Bank_Account_Id")]
		public string DepositAccountId
		{
			get{ return this.DepositAccountIdM; }
			set{ this.DepositAccountIdM = value; }
		}
		
		protected string DepositAccountNoM;
		[DAL.DataColumn("Bank_Account_Number")]
		public string DepositAccountNo
		{
			get{ return this.DepositAccountNoM; }
			set{ this.DepositAccountNoM = value; }
		}

		protected string ItemDepositedM;
		[DAL.DataColumn("Item_Count")]
		public string ItemDeposited
		{
			get{ return this.ItemDepositedM; }
			set{ this.ItemDepositedM = value; }
		}


		#endregion Class Members

		protected BankDepositData aTable;

		string pSearchFieldType = null;
		string pSearchBoxValue = null;

		public BankDeposit() 
		{
			try
			{
  			  aTable = new BankDepositData();
			}
			catch(COMException e)
			{
				int x = e.ErrorCode;
			}
		}
	
		
		override public bool ValidateAndSave()
		{
			bool bOk=true;
	
		    // Do any validation
			if(!Validate(pSearchFieldType, pSearchBoxValue))
			{
				 // Do nothing
			}
			else
			{
				if(IDM == Convert.ToString("-1"))
				{
					bOk= aTable.Insert(DepositDateM, ItemDepositedM,DepositAmountM,DepositStatusIdM,DepositAccountIdM,out IDM);
				}
			
			}
            
			return bOk;
		}
		
		public DataSet GetDepositDataSet()
		{
			DataSet DepositDataSet = aTable.Exists(IDM,DepositStatusIdM,DepositDateM,DepositDateToM,DepositAmountM,DepositAccountNoM,ItemDepositedM );
			return DepositDataSet;
		}

		public bool Validate( string pSearchFieldType, string pSearchBoxValue)
		{
			bool bValid = true;
			string strval;
			int intval;
			decimal decval;
			DateTime dateval;
			if (pSearchBoxValue != "") 
			{
			
				switch (pSearchFieldType)
				{
					case "System.String" :
						try
						{
							strval = Convert.ToString(pSearchBoxValue);
						}
						catch
						{
							bValid = false;
						}
						break;
	
					case "System.Int32" :
						try
						{
							intval = Convert.ToInt32(pSearchBoxValue);
						}
						catch
						{
							bValid = false;
						}
						break;
					case "System.Decimal" :
						try
						{
							decval = Convert.ToDecimal(pSearchBoxValue);
						}
						catch
						{
							bValid = false;
						}
					
						break;
					case "System.DateTime" :
						try
						{
							dateval = Convert.ToDateTime(pSearchBoxValue);
						}
						catch
						{
							bValid = false;
						}
						if ((bValid) && ((Convert.ToDateTime(pSearchBoxValue) < Convert.ToDateTime("01/01/1900") ) || 
							(Convert.ToDateTime(pSearchBoxValue) > Convert.ToDateTime("12/31/2099") )    ) )
						{
							bValid = false;
						}
						break;
				}
			}
			return bValid;
		}
	}

	public class BankDepositItem : QBusinessObject
		{
			#region Class Members
			// Column fields

			protected int IDM=-1;
			[DAL.DataColumn("Deposit_Item_Id")]
			public int Deposit_Item_Id
			{
				get{ return this.IDM; }
				set{ this.IDM=value;  }
			}
		
			protected int Bank_Deposit_IdM;
			[DAL.DataColumn("Bank_Deposit_Id")]
			public int Bank_Deposit_Id
			{
				get{ return this.Bank_Deposit_IdM; }
				set{ this.Bank_Deposit_IdM = value; }
			}


			protected int Payment_IdM;
			[DAL.DataColumn("Payment_Id")]
			public int Payment_Id
			{
				get{ return this.Payment_IdM; }
				set{ this.Payment_IdM = value; }
			}

			override public bool ValidateAndSave()
			{
				bool bOk=true;
				// Do any validation
				if(!Validate())
				{
				}
				else
				{
					if(IDM == -1)
					{
						bOk= aTable.Insert(Bank_Deposit_IdM,Payment_IdM,out IDM);
					}
					
				}
            
				return bOk;
			}
		

			protected BankDepositItemData aTable;

			public BankDepositItem() 
			{
				IDM = -1;
				try
				{
               
					aTable = new BankDepositItemData();

				}
				catch(COMException e)
				{
					int x = e.ErrorCode;
				}
			}
			#endregion
			public bool Validate()
			{
				bool bValid = true;



				return bValid;
			}
		}
		
	












	}

