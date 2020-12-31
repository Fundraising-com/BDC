using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	///<summary>Business Representation of an Account List</summary>
	public class AccountList : QBusinessObject
	{
		#region Class Members
		protected string fmidM;
		public string fmid
		{
			get { return this.fmidM;  }
			set { this.fmidM = value; }
		}

		protected int AccountIDM;
		public int AccountID
		{
			get { return this.AccountIDM;  }
			set { this.AccountIDM = value; }
		}

		protected string AccountNameM;
		public string AccountName
		{
			get { return this.AccountNameM;  }
			set { this.AccountNameM = value; }
		}

		protected string CityM;
		public string City
		{
			get { return this.CityM;  }
			set { this.CityM = value; }
		}

		protected string StateProvinceM;
		public string StateProvince
		{
			get { return this.StateProvinceM;  }
			set { this.StateProvinceM = value; }
		}

		protected string FMFirstNameM;
		public string FMFirstName
		{
			get { return this.FMFirstNameM;  }
			set { this.FMFirstNameM = value; }
		}

		protected string FMLastNameM;
		public string FMLastName
		{
			get { return this.FMLastNameM;  }
			set { this.FMLastNameM = value; }
		}

		protected string OrderByM;
		public string OrderBy
		{
			get { return this.OrderByM;  }
			set { this.OrderByM = value; }
		}

		protected string PostalM;
		public string Postal
		{
			get { return this.PostalM;  }
			set { this.PostalM = value; }
		}

		protected int FiscalYearM;
		public int FiscalYear
		{
			get { return this.FiscalYearM;  }
			set { this.FiscalYearM = value; }
		}

		private int SortIntM
		{
			get
			{
				//determine how to sort the data
				int retval = 0;
				switch (OrderByM)
				{
					case "Id":
						retval = 1;
						break;
					case "Name":
						retval = 2;
						break;
					case "City":
						retval = 3;
						break;
					case "State":
						retval = 4;
						break;
					case "Sponsor":
						retval = 5;
						break;
					case "Phone":
						retval = 6;
						break;
					case "StartDate":
						retval = 7;
						break;
					case "EndDate":
						retval = 8;
						break;
					case "FMID":
						retval = 9;
						break;
					default:
						retval = 2;
						break;
				}
				return retval;
			}
		}

		public string DebugString
		{
			get
			{
				return
					"AccountList DebugString: fmid: " + this.fmidM
					+ "\r\nAccountList DebugString: AccountId: " + this.AccountIDM
					+ "\r\nAccountList DebugString: AccountName: " + this.AccountName
					+ "\r\nAccountList DebugString: City: " + this.CityM
					+ "\r\nAccountList DebugString: State/province: " + this.StateProvinceM
					+ "\r\nAccountList DebugString: Postal Code: " + PostalM
					+ "\r\nAccountList DebugString: FmFirstName: " + this.FMFirstNameM
					+ "\r\nAccountList DebugString: FmLastName: " + this.FMLastNameM
					+ "\r\nAccountList DebugString: OrderBy: " + this.OrderByM
					+ "\r\nAccountList DebugString: sortint: " + this.SortIntM
					+ "\r\n";
			}
		}
		#endregion

		#region Constructors
		protected DAL.AccountListDataAccess aTable = new DAL.AccountListDataAccess();
		///<summary>default constructor</summary>
		public AccountList()
		{
			this.AccountName = "";
			this.City = "";
			this.FMFirstName = "";
			this.fmid = "";
			this.FMLastName = "";
			this.OrderBy = "";
			this.StateProvince = "";
			this.FiscalYearM=0;
		}
		#endregion Constructors

		#region ValidateAndSave
		///<summary>not relevant here</summary>
		override public bool ValidateAndSave()
		{
			return false;
		}
		#endregion ValidateAndSave

		#region DAL call
		///<summary>Get a list of accounts</summary>
		public DataTable GetAccountList()
		{
			return aTable.GetAccountList(
				this.fmid,
				this.AccountID,
				this.AccountName,
				this.City,
				this.StateProvince,
				this.Postal,
				this.FMFirstName,
				this.FMLastName,
				this.FiscalYearM,
				this.SortIntM);
		}
		#endregion DAL call
	}
}


