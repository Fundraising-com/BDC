using System;
using System.Collections.Generic;
using QSPForm.Business.com.qsp.ws.AccountFinderService;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for MatchingAccountsConfirmEventHandler
    /// </summary>
    public class MatchingAccountsConfirmEventArgs : System.EventArgs {
        private List<OutputAccount> matchingAccounts = null;

        public MatchingAccountsConfirmEventArgs() { }

        public MatchingAccountsConfirmEventArgs(List<OutputAccount> matchingAccounts) {
            MatchingAccounts = matchingAccounts;
        }

        public List<OutputAccount> MatchingAccounts {
            get {
                return matchingAccounts;
            }
            set {
                matchingAccounts = value;
            }
        }
    }
}