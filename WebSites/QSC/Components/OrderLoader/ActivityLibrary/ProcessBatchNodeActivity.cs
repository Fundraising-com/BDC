using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Xml.Linq;
using BusinessServices;
using Data;
using Microsoft.Practices.ServiceLocation;

namespace ActivityLibrary
{

    public sealed class ProcessBatchNodeActivity : CodeActivity
    {
        public InArgument<XElement> _batchNode { get; set; }
        public InArgument<ParseBatchNode> _batchNodeParser { get; set; }
        public InArgument<ParseEnvelopeNode> _envelopeNodeParser { get; set; }
        public InArgument<ParseParticipantNode> _participantNodeParser { get; set; }
        public InArgument<ParsePaymentNode> _paymentNodeParser { get; set; }
        public InArgument<ParseAddressNode> _addressNodeParser { get; set; }
        public InArgument<ParseMagOrderDetailNode> _magorderdetailNodeParser { get; set; }
        public InArgument<string> _ke3Filename { get; set; }



        private ParseBatchNode aBParser;
        private ParseEnvelopeNode aEParser;
        private ParseParticipantNode aPParser;
        private ParsePaymentNode aPayParser;
        private ParseAddressNode aAParser;
        private ParseMagOrderDetailNode aMOParser;
        private ParseInternetIDNode aIIDParser;

        public ProcessBatchNodeActivity(ParseBatchNode a, ParseEnvelopeNode aEN, ParseParticipantNode aPN
                                        ,ParsePaymentNode aPayN, ParseAddressNode aAN, ParseMagOrderDetailNode aMON, ParseInternetIDNode aIIDP)
        {
            aBParser = a;
            aEParser = aEN;
            aPParser = aPN;
            aPayParser = aPayN;
            aAParser = aAN;
            aMOParser = aMON;
            aIIDParser= aIIDP;
        }

        protected override void Execute(CodeActivityContext context)
        {
            DateTime aStart = new DateTime();
            aStart = DateTime.Now;
            
            XElement x = _batchNode.Get(context);
            BatchNode bnode = aBParser(x);
            string fname = _ke3Filename.Get(context);
            int orderbatchsequence = 1;

            OrderBatchService _batchService = ServiceLocator.Current.GetInstance<OrderBatchService>();

            //Internet nodes have date filled in whereas landed ones wont
            DateTime dt = bnode.BATCHDATE == "" ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                :  new DateTime(Convert.ToInt32(bnode.BATCHDATE.Substring(4, 4)), Convert.ToInt32(bnode.BATCHDATE.Substring(0, 2)), Convert.ToInt32(bnode.BATCHDATE.Substring(2, 2)));
            DateTime dateReceived = bnode.DATERECVD == "" ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                : new DateTime(Convert.ToInt32(bnode.DATERECVD.Substring(4, 4)), Convert.ToInt32(bnode.DATERECVD.Substring(0, 2)), Convert.ToInt32(bnode.DATERECVD.Substring(2, 2)));
            DateTime dateSent = bnode.DATESENT == "" ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                : new DateTime(Convert.ToInt32(bnode.DATESENT.Substring(4, 4)), Convert.ToInt32(bnode.DATESENT.Substring(0, 2)), Convert.ToInt32(bnode.DATESENT.Substring(2, 2)));
        
               

            _batchService.SetCurrentBatch(dt
                        , bnode.ORDERQUALIFIER.Value
                        , (int)bnode.ORDERTYPE                       
                        , bnode.CAMPAIGNID.Value
                        , bnode.ACCOUNTID.Value
                        , bnode.ORDERID.Value
                        , dateSent
                        , dateReceived
                        , bnode.CHECKAMOUNT
                        , bnode.TOTALGROSSESTIMATE
                        , (int)bnode.ISSTAFF
                        , bnode.REPORTEDAMOUNT
                        , (int)bnode.PROBLEMCODE
                        , (int)bnode.TOTALENVELOPES
                        );
            
            _batchService.CurrentBatch.KE3FileName = fname;

            var yDoc = from e in x.Descendants("ENVELOPE")
                       select e;
            foreach (var _envelope in yDoc)
            {
                ProcessEnvelopeNodeActivity aEActivity = new ProcessEnvelopeNodeActivity(aEParser, aPParser, aAParser, aMOParser, aPayParser, aIIDParser);

                IDictionary<String, Object> wfresults = WorkflowInvoker.Invoke(aEActivity,
                      new Dictionary<String, Object>
                      {
                          {"_Node", _envelope}
                          ,{"_Service", _batchService}
              //            ,{"_OrderBatchSequence", orderbatchsequence}
                        
                      }
                   );

            }
          

            _batchService.SetCurrentBatchStatistics();

            _batchService.SaveCurrentBatch();

            //pr_GenerateBatchMergePurgeList
        }
    }
}
