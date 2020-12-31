using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Xml.Linq;
using BusinessServices;
using Data;

namespace ActivityLibrary
{

    public sealed class ProcessEnvelopeNodeActivity : CodeActivity
    {
        public InArgument<XElement> _Node { get; set; }
        public InArgument<OrderBatchService> _Service { get; set; }
       

        private ParseEnvelopeNode aEParser;
        private ParseParticipantNode aPNParser;
        private ParseAddressNode aANParser;
        private ParseMagOrderDetailNode aMOParser;
        private ParsePaymentNode aPayParser;
        private ParseInternetIDNode aIIDParser;

        public ProcessEnvelopeNodeActivity(ParseEnvelopeNode a
                                            ,ParseParticipantNode aPN
                                            ,ParseAddressNode aA
                                            ,ParseMagOrderDetailNode aMO
                                            ,ParsePaymentNode aPayN
                                            ,ParseInternetIDNode aIIDP)
        {
            aEParser=a;
            aPNParser=aPN;
            aANParser=aA;
            aMOParser=aMO;
            aPayParser = aPayN;
            aIIDParser = aIIDP;
        }
 
        protected override void Execute(CodeActivityContext context)
        {
            XElement x = _Node.Get(context);

            OrderBatchService aService = _Service.Get(context);
            EnvelopeNode aENode = aEParser(x);

            aService.SetCurrentTeacher(aENode.LASTNAME, aENode.FIRSTINITIAL, aENode.CLASSROOM);

            var yDoc = from e in x.Descendants("PARTICIPANT")
                       select e;
            foreach (var _participant in yDoc)
            {
                ProcessParticipantNodeCodeActivity aActivity = new ProcessParticipantNodeCodeActivity(aPNParser, aMOParser, aANParser, aPayParser, aIIDParser);
                
                IDictionary<String, Object> wfresults = WorkflowInvoker.Invoke(aActivity,
                      new Dictionary<String, Object>
                      {
                          {"_Node", _participant}
                          ,{"_Service", aService}
                        
                      }
                   );
                
            }
             
        }
    }
}
