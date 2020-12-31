using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	///<summary>Address</summary>
	public class Address : QBusinessObject
	{
		#region Class Members
		protected int IDM=-1;
		[DAL.DataColumn("address_id")]
		public int address_id
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}
		protected string street1M;
		[DAL.DataColumn("street1")]
		public string street1
		{
			get{ return this.street1M; }
			set{ this.street1M=value;  }
		}
		protected string street2M;
		[DAL.DataColumn("street2")]
		public string street2
		{
			get{ return this.street2M; }
			set{ this.street2M=value;  }
		}
		protected string cityM;
		[DAL.DataColumn("city")]
		public string city
		{
			get{ return this.cityM; }
			set{ this.cityM=value;  }
		}
		protected string stateProvinceM;
		[DAL.DataColumn("stateProvince")]
		public string stateProvince
		{
			get{ return this.stateProvinceM; }
			set{ this.stateProvinceM=value;  }
		}
		protected string postal_codeM;
		[DAL.DataColumn("postal_code")]
		public string postal_code
		{
			get{ return this.postal_codeM; }
			set{ this.postal_codeM=value;  }
		}
		protected string zip4M;
		[DAL.DataColumn("zip4")]
		public string zip4
		{
			get{ return this.zip4M; }
			set{ this.zip4M=value;  }
		}
		protected string countryM;
		[DAL.DataColumn("country")]
		public string country
		{
			get{ return this.countryM; }
			set{ this.countryM=value;  }
		}
		protected int address_typeM;
		[DAL.DataColumn("address_type")]
		public int address_type
		{
			get{ return this.address_typeM; }
			set{ this.address_typeM=value;  }
		}
		protected int AddressListIDM;
		[DAL.DataColumn("AddressListID")]
		public int AddressListID
		{
			get{ return this.AddressListIDM; }
			set{ this.AddressListIDM=value;  }
		}

		#endregion Class Members
		
		#region Constructors
		private DAL.AddressDataAccess aTable;
		///<summary>default constructor</summary>
		public Address()
		{
			this.address_id		= -1;
			this.street1		= "";
			this.street2		= "";
			this.city			= "";
			this.stateProvince	= "";
			this.postal_code	= "";
			this.zip4			= "";
			this.country		= "";
			this.AddressListID  = -5;
			this.aTable = new DAL.AddressDataAccess();
		}

		///<summary>constructor</summary>
		public Address(int Address_id)
		{
			this.IDM = Address_id;
			this.street1		= "";
			this.street2		= "";
			this.city			= "";
			this.stateProvince	= "";
			this.postal_code	= "";
			this.zip4			= "";
			this.country		= "";
			this.AddressListID  = -5;
			this.aTable = new DAL.AddressDataAccess();
		}
		#endregion Constructors

		#region ValidateAndSave
		///<summary>check it then submit it</summary>
		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
				return this.Save();
			}
			else
			{
				return false;
			}
		}


		///<summary>See if it exists</summary>
		public bool Exists(int nType, int nAddressListId)
		{
			bool bExists = false;
			DataTable a = aTable.Exists(nType, nAddressListId);
				
			if(a.Rows.Count > 0)
			{
				DataRow dr = a.Rows[0];
				Fill(dr,this.GetType());
				bExists = true;
			}

			return bExists;
		}


		///<summary>Check for compliance with biz rules</summary>
		public bool Validate()
		{
			/* setup variables to track validation */
			bool blValid = true;

			return blValid;
		}


		///<summary>Save an Address to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		public bool Save()
		{
			bool blSave = true;

			if(IDM == -1)
			{
				blSave = aTable.Insert(street1M, street2M, cityM, stateProvinceM, postal_codeM, zip4M, countryM, address_typeM, AddressListIDM, out IDM);
			}
			else
			{
				blSave = aTable.Update(IDM, street1M, street2M, cityM, stateProvinceM, postal_codeM, zip4M, countryM, address_typeM, AddressListIDM);
			}

			return blSave;
		}
		#endregion ValidateAndSave

		public void PopulateFromDB(int type, int AddressListID)
		{
			DataTable dtAddress = aTable.Exists(type, AddressListID);
			string rowsStr = dtAddress.Rows.Count.ToString();
			string ParamName = "";
			if(dtAddress.Rows.Count == 0)
			{
				/* no data returned, it happens */
				this.address_id		= -1;
				this.street1		= "";
				this.street2		= "";
				this.city			= "";
				this.stateProvince	= "";
				this.postal_code	= "";
				this.zip4			= "";
				this.country		= "";
				this.AddressListID  = AddressListID;
			}
			else if(dtAddress.Rows.Count == 1)
			{
				/* one row returned, good stuff */
				DataRow DR = dtAddress.Rows[0];
				this.address_id		= Convert.ToInt32 (DR["address_id"]);
				this.street1		= Convert.ToString(DR["street1"]);
				this.street2		= Convert.ToString(DR["street2"]);
				this.city			= Convert.ToString(DR["city"]);
				this.stateProvince	= Convert.ToString(DR["stateProvince"]);
				this.postal_code	= Convert.ToString(DR["postal_code"]);
				this.zip4			= Convert.ToString(DR["zip4"]);
				this.country		= Convert.ToString(DR["country"]);
				this.address_type	= type;
				this.AddressListID	= AddressListID;
			}
			else if(dtAddress.Rows.Count > 1)
			{
				rowsStr += " rows of data were returned when 1 was expected for this AddressListID - type Pair";
				ParamName = AddressListID.ToString() + " - " + type.ToString();
				throw new ArgumentException(rowsStr, ParamName);
			}
			else
			{
				ParamName = AddressListID.ToString() + " - " + type.ToString();
				throw new ArgumentException("There is no data for the address_id", ParamName);
			}

		}
	}
}


