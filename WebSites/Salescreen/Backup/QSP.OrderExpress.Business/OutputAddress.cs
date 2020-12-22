using System;
using System.Collections.Generic;
using System.Text;

namespace QSPForm.Business.com.ses.ws.AddressHygieneService
{
    public partial class OutputAddress
    {
        /// <summary>
        /// Creates a clean address without using the web service.
        /// Useful for failure to communicate with the web service.
        /// </summary>
        /// <param name="initialAddress">Initial address that would have been hygiened</param>
        public static OutputAddress CreateCleanOutputAddress(Address initialAddress)
        {
            OutputAddress outputAddress = new OutputAddress();

            outputAddress.Address1 = initialAddress.Address1;
            outputAddress.Address2 = initialAddress.Address2;
            outputAddress.City = initialAddress.City;
            outputAddress.County = initialAddress.County;
            outputAddress.Region = initialAddress.Region;
            outputAddress.PostCode = initialAddress.PostCode;
            outputAddress.PostCode2 = initialAddress.PostCode2;
            outputAddress.Country = initialAddress.Country;

            outputAddress.Selection = String.Empty;

            outputAddress.InitialAddress = initialAddress;
            outputAddress.Fault = Fault.NoError;

            outputAddress.Status = new Status();
            outputAddress.Status.ChangeStatus = ChangeStatus.None;
            outputAddress.Status.FormatChangeStatus = FormatChangeStatus.None;

            outputAddress.SuggestionList = new DetailedAddress[0];
            outputAddress.SuggestionListInformation = new SuggestionListInformation();
            outputAddress.SuggestionListInformation.CurrentCount = 0;
            outputAddress.SuggestionListInformation.Error = SuggestionListError.None;
            outputAddress.SuggestionListInformation.FirstItemID = 0;
            outputAddress.SuggestionListInformation.IsNextAvailable = false;
            outputAddress.SuggestionListInformation.IsPreviousAvailable = false;
            outputAddress.SuggestionListInformation.LastItemID = 0;
            outputAddress.SuggestionListInformation.Status = SuggestionListStatus.None;
            outputAddress.SuggestionListInformation.TotalCount = 0;

            return outputAddress;
        }
    }
}
