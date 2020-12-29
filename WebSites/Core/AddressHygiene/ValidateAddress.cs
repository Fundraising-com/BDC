using System;

namespace efundraising.AddressHygiene
{
	/// <summary>
	/// Summary description for ValidateAddress.
	/// </summary>
	/// 

	
	using efundraising.AddressHygiene.com.rdigest.us.uspvl3k28_dev;
	using Business.com.ses.ws.AddressHygiene;
	using System.Collections;

	public class ValidateAddress
	{
		private static ValidateAddress ValidateAddressInstance = new ValidateAddress();
		private static readonly string hostName = "uspvl3k28-dev.us.rdigest.com";
		private static readonly int portNumber = 20004;
		private static readonly string reposPath = "c:\\dqxi\\11_5_devt\\repository\\configuration_rules\\";
		private static readonly string dataFlowOption = "projects\\eFundraising\\us\\trans_address_data_cleanse_usa.xml";
		private static readonly string substVarOptions = "dqxiserver_devt_substitutions.xml";

        private FirstlogicIQService dataQuality;
		private ValidateAddress()
		{
			//
			// TODO: Add constructor logic here
			//
             //dataQuality = new FirstlogicIQService();
		}

		public string ValidateAnAddressReturnXml(AddressHygiene addr)
		{
			if (addr == null)
				return string.Empty;
			FirstlogicIQService dataQuality = new FirstlogicIQService();
			return dataQuality.runTransactionDataflowWithXmlData(hostName, portNumber, reposPath, dataFlowOption, 
				substVarOptions, true, addr.ToXmlAddressValidation());
		}

		public string ValidateAddressCollectionReturnXml(AddressHygieneCollection addrCollection)
		{
			if (addrCollection == null || addrCollection.Count < 1)
			return string.Empty;
			FirstlogicIQService dataQuality = new FirstlogicIQService();
			return dataQuality.runTransactionDataflowWithXmlData(hostName, portNumber, reposPath, dataFlowOption, 
				substVarOptions, true, addrCollection.ToXmlAddressValidation());
		}

		public string ValidateAddressInXml(string xmlAddress)
		{
			if (xmlAddress == null || xmlAddress.Trim() == string.Empty)
				return string.Empty;

			FirstlogicIQService dataQuality = new FirstlogicIQService();
			return dataQuality.runTransactionDataflowWithXmlData(hostName, portNumber, reposPath, dataFlowOption, 
				substVarOptions, true, xmlAddress);
		}

		public AddressHygiene ValidateAnAddress(AddressHygiene addr)
		{
			if (addr == null)
				return null;

			AddressHygieneCollection addrCol = new AddressHygieneCollection();
			addrCol.Add(addr);
			
			AddressHygieneCollection addrColResult = ValidateAddressCollection(addrCol);
			if (addrColResult == null || addrColResult.Count < 1)
				return null;

			return addrColResult[0];
		}

		
		public AddressHygieneCollection ValidateAddressCollection(AddressHygieneCollection AddrCollection)
		{
			if (AddrCollection == null || AddrCollection.Count < 1)
				return null;
			FirstlogicIQService dataQuality = new FirstlogicIQService();
			string xmlResult = string.Empty;
			string xmlString = AddrCollection.ToXmlAddressValidation();
			xmlResult = dataQuality.runTransactionDataflowWithXmlData(hostName, portNumber, reposPath, dataFlowOption, 
				substVarOptions, true, xmlString);

			AddressHygieneCollection addrCol = new AddressHygieneCollection();
			addrCol.LoadXmlFromWebService(xmlResult);

			return addrCol;
		}

		public AddressHygieneCollection ValidateAddressCollectionInBatch(AddressHygieneCollection AddrCollection, bool inBatch)
		{
			if (AddrCollection == null || AddrCollection.Count < 1)
					 return null;

			FirstlogicIQService dataQuality = new FirstlogicIQService();
			string xmlResult = string.Empty;
			string xmlQuery = AddrCollection.ToXmlAddressValidation();


			xmlResult = dataQuality.runTransactionDataflowWithXmlData(hostName, portNumber,reposPath, dataFlowOption, 
				substVarOptions, true, xmlQuery);

			AddressHygieneCollection addrCol = new AddressHygieneCollection();
			addrCol.LoadXmlFromWebService(xmlResult);

			return addrCol;
		}



		public static ValidateAddress GetValidateAddressInstance()
		{
			return ValidateAddressInstance;
		}

        //public static string runTransactionDataflowWithXmlData(FirstlogicIQService dataQuality, string xmlAddress)
        //{
        //    if (dataQuality == null)
        //        return string.Empty;

        //    return dataQuality.runTransactionDataflowWithXmlData(hostName, portNumber, reposPath, dataFlowOption,
        //        substVarOptions, true, xmlAddress);
        //}
	}


	public class ValidateAddress2: IValidateAddress
	{
		//private static ValidateAddress2 ValidateAddressInstance= new ValidateAddress2();
		private System.Net.CookieContainer cookie = null;
		private ValidateAddress2()
		{
		}
		
		private ValidateAddress2(System.Net.CookieContainer ck)
		{
			cookie = ck;
		}

		#region TryWebSevice
        private Business.com.ses.ws.AddressHygiene.LoginMessageRequest CreateLoginMessageRequest()
		{
			LoginMessageRequest loginRequest = new LoginMessageRequest();
			loginRequest.UserName = efundraising.Configuration.ApplicationSettings.GetConfig()["AddressHygiene", 0, "username"];
			loginRequest.Password = efundraising.Configuration.ApplicationSettings.GetConfig()["AddressHygiene", 0, "password"];;
			return loginRequest;
		}

		Business.com.ses.ws.AddressHygiene.Address[] ConvertToQSPAddress(AddressHygieneCollection AddrCollection)
		{
			ArrayList arList = new ArrayList();
			Address qspAddr = null;
			for (int i= 0; i < AddrCollection.Count; i++)
			{
				qspAddr = ConvertToQSPAddress(AddrCollection[i]);
				arList.Add(qspAddr);
			}
			return (Address[])arList.ToArray(typeof(Address));
		}

		public Business.com.ses.ws.AddressHygiene.OutputAddress[] HygieneAddresses(AddressHygieneCollection AddrCollection)
		{
			return HygieneAddresses(ConvertToQSPAddress(AddrCollection));
		}

        public Business.com.ses.ws.AddressHygiene.OutputAddress[] HygieneAddresses(Business.com.ses.ws.AddressHygiene.Address[] adds)
		{
			if (adds == null || adds.Length < 1)
				return null;

			AddressHygieneContract addressContract = new AddressHygieneContract();
			AddressHygieneMessageRequest addressMsgReq = new AddressHygieneMessageRequest();
			addressMsgReq.EnableSuggestionList = true;
			addressMsgReq.Addresses = adds;
			if (addressContract.Login(CreateLoginMessageRequest()))
			{
				AddressHygieneMessageResponse addressMsgResponse = addressContract.HygieneAddresses(addressMsgReq);
				return addressMsgResponse.OutputAddresses;
			}
			return null;
		}

        public Business.com.ses.ws.AddressHygiene.OutputAddress HygieneAddress(AddressHygiene addrHyg)
		{
			return HygieneAddress(ConvertToQSPAddress(addrHyg));
		}


        public Business.com.ses.ws.AddressHygiene.OutputAddress HygieneAddress(Business.com.ses.ws.AddressHygiene.Address addrHyg)
		{
			if (addrHyg == null)
				return null;

			AddressHygieneContract addressContract = new AddressHygieneContract();
			AddressHygieneSingleMessageRequest singleMsg = new AddressHygieneSingleMessageRequest();
			//Initialize Address Hygiene Settings
			singleMsg.EnableSuggestionList = true;
			singleMsg.Address = addrHyg;
			if(cookie != null) {
				addressContract.CookieContainer = this.cookie;
			} else {
				addressContract.CookieContainer = new System.Net.CookieContainer();
			}
			if (addressContract.Login(CreateLoginMessageRequest()))
			{
				AddressHygieneSingleMessageResponse singleResponse = addressContract.HygieneAddress(singleMsg);
				if (singleResponse != null)
					return singleResponse.OutputAddress;
			}
			return null;
		}


        Business.com.ses.ws.AddressHygiene.Address ConvertToQSPAddress(AddressHygiene addr)
		{
			if (addr == null)
				return null;
			Address qspAddr = null;
			qspAddr = new Address();
			qspAddr.Address1 = addr.StreetAddress;
			qspAddr.Address2 = string.Empty;
			qspAddr.City = addr.City;
			qspAddr.Region = addr.StateCode;
			qspAddr.Country = addr.CountryCode;
			qspAddr.County = addr.County;
			qspAddr.PostCode = addr.ZipCode;
			qspAddr.PostCode2 = string.Empty;

			return qspAddr;
		}


		public static ValidateAddress2 GetValidateAddressInstance()
		{
			return new ValidateAddress2();
		}

		
		public static ValidateAddress2 GetValidateAddressInstance(System.Net.CookieContainer ck)
		{
			return new ValidateAddress2(ck);
		}

		#endregion

		#region IValidateAddress Members

		public AddressHygieneCollection ValidateAddress(AddressHygiene addrHyg)
		{
			// TODO:  Add ValidateAddress2.ValidateAddress implementation
			return null;
		}

		public AddressHygieneCollection ValidateCollectionAddresses(AddressHygieneCollection addrHygCol)
		{
			// TODO:  Add ValidateAddress2.ValidateCollectionAddresses implementation
			return null;
		}


		IValidateAddress efundraising.AddressHygiene.IValidateAddress.CreateValidateAddressInstance(System.Net.CookieContainer ck)
		{
			// TODO:  Add ValidateAddress2.efundraising.AddressHygiene.IValidateAddress.CreateValidateAddressInstance implementation
			return null;
		}

		#endregion
	}
}
