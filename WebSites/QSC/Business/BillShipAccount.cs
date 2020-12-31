using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
//using DAL;

namespace Business
{
	/// <summary>
	/// Summary description for BillShipAccount.
	/// </summary>
	public class BillShipAccount : QBusinessObject
	{
		#region Class Members
		// Column fields

		protected int IDM ;
		[DAL.DataColumn("Campaign_Id")]
		public int Campaign_Id
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}
		#endregion Class Members

		protected DAL.BillShipAccountData aTable;

		string pSearchFieldType = null;
		string pSearchBoxValue = null;


		public BillShipAccount()
		{
			try
			{
				aTable = new DAL.BillShipAccountData();
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
			return bOk;
		}

		public DataTable GetBillShipAccountData()
		{
			return aTable.Exists(IDM);
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
}
