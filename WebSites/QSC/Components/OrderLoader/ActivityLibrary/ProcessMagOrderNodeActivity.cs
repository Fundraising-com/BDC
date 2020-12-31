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

    public sealed class ProcessMagOrderNodeActivity : CodeActivity
    {
        public InArgument<XElement> _Node { get; set; }
        public InArgument<OrderBatchService> _Service { get; set; }

        private ParseAddressNode aAddrParser;
        private ParseMagOrderDetailNode aMODParser;
        private ParsePaymentNode aPParser;
        private ParseInternetIDNode aIIDParser;

        public ProcessMagOrderNodeActivity(ParseAddressNode aA, ParseMagOrderDetailNode aM, ParsePaymentNode aP,ParseInternetIDNode aI=null)
        {
            aAddrParser = aA;
            aMODParser = aM;
            aPParser = aP;
            aIIDParser = aI;
        }

        protected override void Execute(CodeActivityContext context)
        {
            XElement x = _Node.Get(context);
            OrderBatchService aService = _Service.Get(context);

            XElement aBillToAddress = x.Descendants("MAILINGADDRESS").First();
            XElement aPayment = x.Descendants("PAYMENT").First();
            XElement aAddress = x.Descendants("MAILINGADDRESS").First();

            PaymentNode aPNode = aPParser(aPayment);
            AddressNode aANode = aAddrParser(aAddress);
            //TODO
            string payorFirstName = "";//aPNode.PayorName.Split(
            string payorLastName = "";

            aService.SetCurrentCustomerOrderHeader(aService.CurrentBatch, aService.CurrentStudent);
            if (aIIDParser != null)
            {
                XElement aInternetID = x.Descendants("INTERNETID").First();
                InternetIDNode aIIDNode = aIIDParser(aInternetID);

                aService.SetCurrentInternetOrderID(aService.CurrentCOH, (int)aIIDNode.INTERNETID);
            }
            
            aService.SetCurrentCustomerBillTo(aService.CurrentCOH, payorFirstName, payorLastName,aANode.Address1, aANode.Address2, aANode.City, aANode.Province, aANode.Postal);

           
            var aQuery = from o in x.Descendants("MAGORDERDETAIL")
                                select o;

            foreach (var order in aQuery)
            {
                ProcessMagOrderDetailNodeActivity aActivity = new ProcessMagOrderDetailNodeActivity(aMODParser, aAddrParser);

                IDictionary<String, Object> wfresults = WorkflowInvoker.Invoke(aActivity,
                    new Dictionary<String, Object>
                    {
                        {"_Node", order}
                        ,{"_Service", aService}
                        ,{"_PaymentMethod", aPNode.Type}
                        
                    }
                );

            }

            aService.ValidateCurrentCOH();

            aService.SetCurrentPayment(aService.CurrentCOH, aPNode);
            
        }
    
    }
}
