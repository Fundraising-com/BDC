using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for CatalystDataFulfillmentHouse.
	/// </summary>
	public class CatalystDataFulfillmentHouse
	{
		private string initialName = String.Empty;
		private string enteredName = String.Empty;
		private string initialAddress1 = String.Empty;
		private string enteredAddress1 = String.Empty;
		private string initialAddress2 = String.Empty;
		private string enteredAddress2 = String.Empty;
		private string initialCity = String.Empty;
		private string enteredCity = String.Empty;
		private string initialStateProvince = String.Empty;
		private string enteredStateProvince = String.Empty;
		private string initialZip = String.Empty;
		private string enteredZip = String.Empty;

		public CatalystDataFulfillmentHouse() { }

		public CatalystDataFulfillmentHouse(string initialName, string enteredName, string initialAddress1, string enteredAddress1, string initialAddress2, string enteredAddress2, string initialCity, string enteredCity, string initialStateProvince, string enteredStateProvince, string initialZip, string enteredZip) 
		{
			this.initialName = initialName;
			this.enteredName = enteredName;
			this.initialAddress1 = initialAddress1;
			this.enteredAddress1 = enteredAddress1;
			this.initialAddress2 = initialAddress2;
			this.enteredAddress2 = enteredAddress2;
			this.initialCity = initialCity;
			this.enteredCity = enteredCity;
			this.initialStateProvince = initialStateProvince;
			this.enteredStateProvince = enteredStateProvince;
			this.initialZip = initialZip;
			this.enteredZip = enteredZip;
		}

		public string InitialName 
		{
			get 
			{
				return initialName;
			}
			set 
			{
				initialName = value;
			}
		}

		public string EnteredName 
		{
			get 
			{
				return enteredName;
			}
			set 
			{
				enteredName = value;
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
				return initialAddress2;
			}
			set 
			{
				initialAddress2 = value;
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

		public string InitialZip 
		{
			get 
			{
				return initialZip;
			}
			set 
			{
				initialZip = value;
			}
		}

		public string EnteredZip 
		{
			get 
			{
				return enteredZip;
			}
			set 
			{
				enteredZip = value;
			}
		}
	}
}
