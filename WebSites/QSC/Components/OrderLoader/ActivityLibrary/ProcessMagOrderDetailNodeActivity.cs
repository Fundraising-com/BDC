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

    public sealed class ProcessMagOrderDetailNodeActivity : CodeActivity
    {
        public InArgument<XElement> _Node { get; set; }
        public InArgument<OrderBatchService> _Service { get; set; }
        public InArgument<int> _PaymentMethod { get; set; }

        private ParseMagOrderDetailNode aParser;
        private ParseAddressNode aAddrParser;

        public ProcessMagOrderDetailNodeActivity(ParseMagOrderDetailNode aP, ParseAddressNode aA)
        {
            aParser = aP;
            aAddrParser = aA;
        }

        protected override void Execute(CodeActivityContext context)
        {
            XElement x = _Node.Get(context);
            OrderBatchService aService = _Service.Get(context);
            int paymentMethod = _PaymentMethod.Get(context);

            MagOrderDetailNode aDetail = aParser(x);
            AddressNode aAddrNode = null;
            // need ship to address node
            if(x.Descendants("SHIPTO").Count<XElement>() != 0)
            {
                XElement aAddress= x.Descendants("SHIPTO").First();
                aAddrNode = aAddrParser(aAddress);
            }

            
            aService.SetCurrentCustomerOrderDetail(aService.CurrentCOH, aDetail, paymentMethod, aAddrNode);


        }
    }
}
