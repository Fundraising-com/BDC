using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Common;
using Data;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;
using System.Xml;


namespace BusinessServices
{
    public delegate BatchNode ParseBatchNode(XElement node);//, OrderBatchService a, out int campaignID, out int accountID);
    public delegate EnvelopeNode ParseEnvelopeNode(XElement node);
    public delegate ParticipantNode ParseParticipantNode(XElement node);
    public delegate PaymentNode ParsePaymentNode(XElement node);
    public delegate AddressNode ParseAddressNode(XElement node);
    public delegate MagOrderDetailNode ParseMagOrderDetailNode(XElement node);
    public delegate InternetIDNode ParseInternetIDNode(XElement node);

    public class BatchNodeParser
    {
        public BatchNode ParseInternetNode(XElement node)
        {
            BatchNode _bNode = new BatchNode();

            _bNode.ORDERID = -1;
            _bNode.CAMPAIGNID = Convert.ToInt32(node.Descendants("CAMPAIGNID").First().Value);
            _bNode.ACCOUNTID = Convert.ToInt32(node.Descendants("ACCOUNTID").First().Value);
            _bNode.ACCOUNTNAME = node.Descendants("ACCOUNTNAME").First().Value;
            _bNode.ORDERQUALIFIER = (int)BatchOrderQualifier.Internet;
            _bNode.BATCHDATE = node.Descendants("BATCHDATE").First().Value;
            _bNode.SOURCEID = Convert.ToInt32(node.Descendants("SOURCEID").First().Value);
            _bNode.ORDERTYPE = _bNode.SOURCEID == 24 ? (int)BatchOrderTypeCode.FreeSubs : (int)BatchOrderTypeCode.CA;
            _bNode.DATESENT = "";
            _bNode.DATERECVD = "";
            _bNode.CHECKAMOUNT = 0.00M;
            _bNode.TOTALGROSSESTIMATE = 0.00M;
            _bNode.TOTALFORMS = 0;
            _bNode.ISSTAFF = 0;
            _bNode.REPORTEDAMOUNT = Convert.ToDecimal(node.Descendants("REPORTEDAMOUNT").First().Value);
            _bNode.PROBLEMCODE = 0;
            _bNode.TOTALENVELOPES = 0;
            return _bNode;

        }
        public BatchNode ParseNonInternetNode(XElement node)
        {
             BatchNode _bNode = new BatchNode();

            _bNode.BATCHDATE = "";
            _bNode.ORDERID = Convert.ToInt32(node.Descendants("ORDERID").First().Value);
            _bNode.CAMPAIGNID = Convert.ToInt32(node.Descendants("CAMPAIGNID").First().Value);
            _bNode.ACCOUNTID = Convert.ToInt32(node.Descendants("ACCOUNTID").First().Value);
            _bNode.ACCOUNTNAME = node.Descendants("ACCOUNTNAME").First().Value;
            _bNode.ORDERQUALIFIER = Convert.ToInt32(node.Descendants("ORDERQUALIFIER").First().Value);
            _bNode.ORDERTYPE = Convert.ToInt32(node.Descendants("ORDERTYPE").First().Value);
            _bNode.DATESENT = node.Descendants("DATESENT").First().Value;
            _bNode.DATERECVD = node.Descendants("DATERECVD").First().Value;
            _bNode.PROBLEMCODE = Convert.ToInt32(node.Descendants("PROBLEMCODE").First().Value);
            _bNode.CHECKAMOUNT = Convert.ToDecimal(node.Descendants("CHECKAMOUNT").First().Value);
            _bNode.TOTALGROSSESTIMATE = Convert.ToDecimal(node.Descendants("TOTALGROSSESTIMATE").First().Value);
            _bNode.TOTALFORMS = Convert.ToInt32(node.Descendants("TOTALFORMS").First().Value);
            _bNode.TOTALENVELOPES = Convert.ToInt32(node.Descendants("TOTALENVELOPES").First().Value);
            _bNode.ISSTAFF = Convert.ToInt32(node.Descendants("ISSTAFF").First().Value);
            _bNode.REPORTEDAMOUNT = 0.00M;

            return _bNode;
        }
    }
    public class EnvelopeNodeParser
    {
        public EnvelopeNode ParseInternetNode(XElement node)
        {
            EnvelopeNode _envelopeNode = new EnvelopeNode();
            var teacherNode = node.Descendants("TEACHER").First();

            _envelopeNode.LASTNAME = teacherNode.Descendants("LASTNAME").First().Value;
            _envelopeNode.FIRSTINITIAL = teacherNode.Descendants("MIDDLEINITIAL").First().Value;
            _envelopeNode.CLASSROOM = teacherNode.Descendants("CLASSROOM").First().Value;

            
            return _envelopeNode;

        }
        public EnvelopeNode ParseNonInternetNode(XElement node)
        {
            EnvelopeNode _envelopeNode = new EnvelopeNode();

            _envelopeNode.LASTNAME = node.Descendants("LASTNAME").First().Value;
            _envelopeNode.FIRSTINITIAL = node.Descendants("FIRSTINITIAL").First().Value;
            _envelopeNode.CLASSROOM = node.Descendants("CLASSROOM").First().Value;
            _envelopeNode.NUMBEROFFORMS = Convert.ToInt32(node.Descendants("NUMBEROFFORMS").First().Value);
            _envelopeNode.NUMBEROFUNITS = Convert.ToInt32(node.Descendants("NUMBEROFUNITS").First().Value);

            return _envelopeNode;

        }
    }

    public class AddressNodeParser
    {
        public AddressNode ParseInternetNode(XElement node)
        {
            AddressNode aNode = new AddressNode();   
            aNode.Address1 = node.Descendants("ADDR1").First().Value;
            aNode.Address2 = node.Descendants("ADDR2").First().Value;
            aNode.City = node.Descendants("CITY").First().Value;
            aNode.Province = node.Descendants("PROVINCE").First().Value;
            aNode.Postal = node.Descendants("POSTAL").First().Value;
            aNode.Status = Convert.ToInt32(node.Descendants("STATUSINSTANCE").First().Value);

            return aNode;
        }
        

    }
    public class ParticipantNodeParser
    {
        public ParticipantNode ParseInternetNode(XElement node)
        {
            ParticipantNode aNode = new ParticipantNode();
            aNode.LASTNAME = node.Descendants("LASTNAME").First().Value;
            aNode.MIDDLEINITIAL = node.Descendants("MIDDLEINITIAL").First().Value;
            aNode.FIRSTNAME = node.Descendants("FIRSTNAME").First().Value;
            return aNode;
        }
        public ParticipantNode ParseNonInternetNode(XElement node)
        {
            ParticipantNode aNode = new ParticipantNode();
            aNode.LASTNAME = node.Descendants("LASTNAME").First().Value;
            aNode.FIRSTNAME = node.Descendants("FIRSTNAME").First().Value;
            return aNode;
        }
    }

    public class PaymentNodeParser
    {
        
        public PaymentNode ParseInternetNode(XElement node)
        {
            PaymentNode aNode = new PaymentNode();

            aNode.PayorName = node.Descendants("PAYMENTNAME").First().Value;
            aNode.Type = Convert.ToInt32(node.Descendants("PAYMENTTYPE").First().Value);
            aNode.CardType = Convert.ToInt32(node.Descendants("PAYMENTTYPE").First().Value);
             aNode.PHONE = node.Descendants("PHONE").First().Value;
            aNode.EXP = "0000";//  node.Descendants("EXP").First().Value;
            aNode.CreditCardNumber = "0000000000000000";
            aNode.VERISIGNID = node.Descendants("VERISIGNID").First().Value;

            //we know this got settled from qsp.ca so therefore a good payment
            aNode.STATUSINSTANCE = (int)CreditCardPaymentStatus.Error;


            return aNode;
        }
        public PaymentNode ParseNonInternetNode(XElement node)
        {
            PaymentNode aNode = new PaymentNode();

            aNode.PayorName = node.Descendants("PAYMENTNAME").First().Value;
            aNode.Type = Convert.ToInt32(node.Descendants("PAYMENTTYPE").First().Value);
            aNode.CardType = Convert.ToInt32(node.Descendants("PAYMENTTYPE").First().Value);
            aNode.PHONE = node.Descendants("PHONE").First().Value;
            aNode.EXP =  node.Descendants("EXP").First().Value;
            aNode.CreditCardNumber = node.Descendants("CC").First().Value;

            //we know this got settled from qsp.ca so therefore a good payment
            aNode.STATUSINSTANCE = Convert.ToInt32(node.Descendants("PAYMENTSTATUS").First().Value);


            return aNode;
        }
    }

             
    public class MagOrderDetailParser
    {
        public MagOrderDetailNode ParseInternetNode(XElement node)
        {
            MagOrderDetailNode aNode = new MagOrderDetailNode();

            aNode.RECIPIENT = node.Descendants("RECIPIENT").First().Value;
            aNode.PRODUCTCODE = node.Descendants("PRODUCTCODE").First().Value;
            aNode.PRODUCTNAME = node.Descendants("PRODUCTNAME").First().Value;
            aNode.QUANTITY  = Convert.ToInt32(node.Descendants("QUANTITY").First().Value);
            aNode.PRICE  = Convert.ToDecimal(node.Descendants("PRICE").First().Value);
            aNode.PRODUCTLINE  = Convert.ToInt32(node.Descendants("PRODUCTLINE").First().Value);
            aNode.RENEWA = node.Descendants("RENEWAL").First().Value;
            aNode.STATUSINSTANCE = (int)CustomerOrderDetailStatus.Paid;
            aNode.PRICEOVERRIDE = Convert.ToInt32(node.Descendants("PRICEOVERRIDE").First().Value);           
            return aNode;
        }
         public MagOrderDetailNode ParseNonInternetNode(XElement node)
        {
            MagOrderDetailNode aNode = new MagOrderDetailNode();

            aNode.RECIPIENT = node.Descendants("RECIPIENT").First().Value;
            aNode.PRODUCTCODE = node.Descendants("PRODUCTCODE").First().Value;
            aNode.PRODUCTNAME = node.Descendants("PRODUCTNAME").First().Value;
            aNode.QUANTITY  = Convert.ToInt32(node.Descendants("QUANTITY").First().Value);
            //aNode.C
            aNode.PRICE  = Convert.ToDecimal(node.Descendants("PRICE").First().Value);
            aNode.PRODUCTLINE  = Convert.ToInt32(node.Descendants("PRODUCTLINE").First().Value);
            aNode.RENEWA = node.Descendants("RENEWAL").First().Value;
            aNode.STATUSINSTANCE = Convert.ToInt32(node.Descendants("STATUSINSTANCE").First().Value);
            aNode.PRICEOVERRIDE = Convert.ToInt32(node.Descendants("PRICEOVERRIDE").First().Value);           
            return aNode;
        }
    }
    public class InternetNodeParser
    {
        public InternetIDNode ParseInternetNode(XElement node)
        {
            InternetIDNode aNode = new InternetIDNode();
            aNode.INTERNETID = Convert.ToInt32(node.Value);
            return aNode;
        }
    }
    public class ProcessingFeeNodeParser
    {
        public ProcessingFeeNode ParseInternetNode(XElement node)
        {
            ProcessingFeeNode aNode = new ProcessingFeeNode();
            
            return aNode;
        }
    }
}
