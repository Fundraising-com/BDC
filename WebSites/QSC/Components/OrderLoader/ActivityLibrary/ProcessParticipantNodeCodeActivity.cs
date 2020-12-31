using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using BusinessServices;
using System.Xml.Linq;
using Data;

namespace ActivityLibrary
{

    public sealed class ProcessParticipantNodeCodeActivity : CodeActivity
    {
        public InArgument<XElement> _Node { get; set; }
        public InArgument<OrderBatchService> _Service { get; set; }

        private ParseParticipantNode aParser;
        private ParseAddressNode aAddrParser;
        private ParseMagOrderDetailNode aMODParser;
        private ParsePaymentNode aPParser;
        private ParseInternetIDNode aIIDParser;


        public ProcessParticipantNodeCodeActivity(ParseParticipantNode a, ParseMagOrderDetailNode aMO
            , ParseAddressNode aAP, ParsePaymentNode aP, ParseInternetIDNode aIIDP)
        {
            aParser = a;
            aAddrParser = aAP;
            aMODParser = aMO;
            aPParser = aP;
            aIIDParser = aIIDP;
        }
        protected override void Execute(CodeActivityContext context)
        {
            XElement x = _Node.Get(context);

            OrderBatchService aService = _Service.Get(context);
            ParticipantNode aPNode = aParser(x);

            aService.SetCurrentStudent(aService.CurrentTeacher, aPNode.LASTNAME, aPNode.FIRSTNAME);

            var aQuery = from e in x.Descendants("MAGAZINEORDERS")
                         select e;

            foreach (var order in aQuery)
            {


                ProcessMagOrderNodeActivity aActivity = new ProcessMagOrderNodeActivity(aAddrParser, aMODParser, aPParser,aIIDParser);

                IDictionary<String, Object> wfresults = WorkflowInvoker.Invoke(aActivity,
                      new Dictionary<String, Object>
                      {
                          {"_Node", order}
                          ,{"_Service", aService}
                        
                      }
                   );
            }

        }
    }
}
