using System;
using System.Runtime.Serialization;
namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for Customer.
	/// </summary>
	/// 
	[Serializable]
	public enum CustomerType
	{
		none						= 0,
		Regular						= 50601,
		FM							= 50602,
		Account						= 50603,
		Employee					= 50604,
		Certificate					= 50605,
		ReIssue						= 50606,
		NewSubForOrderCorrection	= 50607,
		NewOrderForNonExisting		= 50608
	}

	[Serializable]
	public class Customer
	{
		private Address aAddress;
		private string sFirstName;
		private string sLastName;
		private int iCustomerInstance;
		private string sEmail;
		private string sPhoneNumber;
		private CustomerType ctCustomerType;

		public Customer()
		{}
		public Customer(string LastName,string FirstName,Address Address)
		{
			this.sFirstName = FirstName;
			this.sLastName = LastName;
			this.aAddress = Address;
		}
      public Customer(string LastName, string FirstName, Address Address, string Email, string PhoneNumber)
      {
         this.sFirstName = FirstName;
         this.sLastName = LastName;
         this.aAddress = Address;
         this.sEmail = Email;
         this.sPhoneNumber = PhoneNumber;
      }

		public Address CustomerAddress
		{
			get
			{ 
				return this.aAddress; 
				}
				
			set{ this.aAddress = value; }
		}
		public string FirstName
		{
			get
			{ 
				return this.sFirstName; 
			}
				
			set{ this.sFirstName = value; }
		}
		public string LastName
		{
			get
			{ 
				return this.sLastName; 
			}
				
			set{ this.sLastName = value; }
		}
		public int CustomerInstance
		{
			get
			{ 
				return this.iCustomerInstance; 
			}
				
			set{ this.iCustomerInstance = value; }
		}
		public string  PhoneNumber
		{
			get
			{ 
				return this.sPhoneNumber; 
			}
				
			set{ this.sPhoneNumber = value; }
		}
		public string Email
		{
			get
			{ 
				return this.sEmail; 
			}
				
			set{ this.sEmail = value; }
		}
		public CustomerType Type
		{
			get
			{ 
				return this.ctCustomerType; 
			}
				
			set{ this.ctCustomerType = value; }
		}

	}
}
