using System;
using QSPForm.Business.com.ses.ws.AddressHygieneService;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Address Hygiene Confirm even arguments
    /// </summary>
    /// <author>Benoit Nadon</author>
    /// <creationdate>08/27/2007</creationdate>
    public class AddressHygieneConfirmArgs : EventArgs {
        private Address address = null;

        public AddressHygieneConfirmArgs() { }

        public AddressHygieneConfirmArgs(Address address) {
            Address = address;
        }

        public Address Address {
            get {
                return address;
            }
            set {
                address = value;
            }
        }
    }
}