using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessServices;

namespace ActivityLibrary
{
    public class ProcessInternetFileActivity : ProcessFileBaseActivity
    {
        protected override ProcessBatchNodeActivity CreateAndInitializeBatchNodeActivity()
        {
 
            BatchNodeParser aBN = new BatchNodeParser();
            EnvelopeNodeParser aEN = new EnvelopeNodeParser();
            MagOrderDetailParser aMO = new MagOrderDetailParser();
            PaymentNodeParser aPN = new PaymentNodeParser();
            ParticipantNodeParser aPartP = new ParticipantNodeParser();
            AddressNodeParser aAP = new AddressNodeParser();
            InternetNodeParser aIIDP = new InternetNodeParser();


            ProcessBatchNodeActivity aBNA = new ProcessBatchNodeActivity(aBN.ParseInternetNode
                      , aEN.ParseInternetNode
                      , aPartP.ParseInternetNode
                      , aPN.ParseInternetNode
                      , aAP.ParseInternetNode
                      , aMO.ParseInternetNode
                      , aIIDP.ParseInternetNode);

            return aBNA;
        }
    }
}
