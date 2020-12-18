using System;
using System.Collections.Generic;
using System.Text;

namespace QSPForm.Business.com.ses.ws.AddressHygieneService
{
    public partial class Address
    {
        public override bool Equals(object obj)
        {
            bool isEqual = false;
            Address comparedAddress = obj as Address;

            if (comparedAddress != null)
            {
                isEqual =
                    (Address1 == comparedAddress.Address1 &&
                    Address2 == comparedAddress.Address2 &&
                    City == comparedAddress.City &&
                    County == comparedAddress.County &&
                    Region == comparedAddress.Region &&
                    PostCode == comparedAddress.PostCode &&
                    PostCode2 == comparedAddress.PostCode2 &&
                    Country == comparedAddress.Country);
            }

            return isEqual;
        }
    }
}
