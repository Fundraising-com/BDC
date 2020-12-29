using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.GpfRpcObjects
{
   [Serializable]
   internal class GpfRpcTransactionChangeRequest
   {
      public GpfRpcTransactionChangeRequest(string input, string orderId, string status, string mechantNote)
      {
         this.C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         this.M = "changeStatus".ToString();
         this.merchant_note = mechantNote;
         this.status = status;
         this.ids = new string[] { input };
      }
      public string C { get; set; }
      public string M { get; set; }
      public string merchant_note { get; set; }
      public string status { get; set; }
      public string[] ids { get; set; }
   }
}
