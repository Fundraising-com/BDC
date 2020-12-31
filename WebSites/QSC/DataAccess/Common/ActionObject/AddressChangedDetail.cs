using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for CatalystDataFulfillmentHouse.
	/// </summary>
	public class AddressChangedDetail
	{
		private string initialAcc = String.Empty;
		private string enteredAcc = String.Empty;
		private string initialAddress1 = String.Empty;
		private string enteredAddress1 = String.Empty;
		private string initialAddress2 = String.Empty;
		private string enteredAddress2 = String.Empty;
		private string initialCity = String.Empty;
		private string enteredCity = String.Empty;
		private string initialStateProvince = String.Empty;
		private string enteredStateProvince = String.Empty;
		private string initialPostal = String.Empty;
		private string enteredPostal = String.Empty;
		private string addressTypeDesc = String.Empty;
		private string vendorNumber = String.Empty;

		public AddressChangedDetail() { }

		public AddressChangedDetail(string initialAcc, string enteredAcc, string initialAddress1, string enteredAddress1, string initialAddress2, string enteredAddress2, string initialCity, string enteredCity, string initialStateProvince, string enteredStateProvince, string initialPostal, string enteredPostal, string addressTypeDesc, string vendorNumber) 
		{
			this.initialAcc = initialAcc;
			this.enteredAcc = enteredAcc;
			this.initialAddress1 = initialAddress1;
			this.enteredAddress1 = enteredAddress1;
			this.initialAddress2 = initialAddress2;
			this.enteredAddress2 = enteredAddress2;
			this.initialCity = initialCity;
			this.enteredCity = enteredCity;
			this.initialStateProvince = initialStateProvince;
			this.enteredStateProvince = enteredStateProvince;
			this.initialPostal = initialPostal;
			this.enteredPostal = enteredPostal;
			this.addressTypeDesc=addressTypeDesc;
			this.vendorNumber = vendorNumber;
		}

		public string InitialAcc
		{
			get 
			{
				return initialAcc;
			}
			set 
			{
				initialAcc = value;
			}
		}

		public string EnteredAcc
		{
			get 
			{
				return enteredAcc;
			}
			set 
			{
				enteredAcc = value;
			}
		}

		public string InitialAddress1 
		{
			get 
			{
				return initialAddress1;
			}
			set 
			{
				initialAddress1 = value;
			}
		}

		public string EnteredAddress1 
		{
			get 
			{
				return enteredAddress1;
			}
			set 
			{
				enteredAddress1 = value;
			}
		}

		public string InitialAddress2 
		{
			get 
			{
				return initialAddress2;
			}
			set 
			{
				initialAddress2 = value;
			}
		}

		public string EnteredAddress2 
		{
			get 
			{
				return enteredAddress2;
			}
			set 
			{
				enteredAddress2 = value;
			}
		}

		public string InitialCity 
		{
			get 
			{
				return initialCity;
			}
			set 
			{
				initialCity = value;
			}
		}

		public string EnteredCity 
		{
			get 
			{
				return enteredCity;
			}
			set 
			{
				enteredCity = value;
			}
		}

		public string InitialStateProvince 
		{
			get 
			{
				return initialStateProvince;
			}
			set 
			{
				initialStateProvince = value;
			}
		}

		public string EnteredStateProvince 
		{
			get 
			{
				return enteredStateProvince;
			}
			set 
			{
				enteredStateProvince = value;
			}
		}

		public string InitialPostal
		{
			get 
			{
				return initialPostal;
			}
			set 
			{
				initialPostal = value;
			}
		}

		public string EnteredPostal
		{
			get 
			{
				return enteredPostal;
			}
			set 
			{
				enteredPostal = value;
			}
		}
		
		public string AddressTypeDesc
		{
			get 
			{
				return addressTypeDesc;
			}
			set 
			{
				addressTypeDesc = value;
			}
		}
		public string VendorNumber
		{
			get 
			{
				return vendorNumber;
			}
			set 
			{
				vendorNumber = value;
			}
		}

	}
}
