using System;

namespace GA.BDC.Core.AddressHygiene
{
	/// <summary>
	/// Summary description for IValidateAddress.
	/// </summary>
	
	interface IValidateAddress
	{
		AddressHygieneCollection ValidateAddress(AddressHygiene addrHyg);
		AddressHygieneCollection ValidateCollectionAddresses(AddressHygieneCollection addrHygCol);
		IValidateAddress CreateValidateAddressInstance(System.Net.CookieContainer ck);
	}
}
