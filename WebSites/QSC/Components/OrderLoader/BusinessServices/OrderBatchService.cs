using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business;
using Data;
using Common;
using Microsoft.Practices.ServiceLocation;
using System.Data.Objects;
using System.Data;
using System.Diagnostics;


namespace BusinessServices
{
    public class OrderBatchService : IBusinessService
    {
        private EFRepository<Batch> _orderBatchRepository = null;
        public Teacher CurrentTeacher { get; set; }
        public Student CurrentStudent { get; set; }
        public CustomerOrderHeader CurrentCOH { get; set; }
        public Batch CurrentBatch { get; set; }
        public Customer CurrentCustomerBillTo { get; set; }
        public CreditCardBatch CurrentCreditCardBatch { get; set; }   //Only create a batch for landed order
        public InternetOrderID CurrentInternetOrderID { get; set; }   //Only create a batch for landed order
 
        private PaymentNode _paymentNode { get; set; }
 

        public Batch Batch
        {
            get { return CurrentBatch; }
        }

        public OrderBatchService( EFRepository<Batch> _b)
        {
            _orderBatchRepository = _b;
            CurrentCreditCardBatch = null;
        }
        public OrderBatchService()
        {
            CurrentCreditCardBatch = null;
        }

    
       
        public Batch SetCurrentBatch(DateTime aDate
                    , int BatchOrderQualifier
                    , int OrderTypeCode
                    , int nCampaignID
                    , int nAccountID
                    , int nOrderID
                    , DateTime aDateSent
                    , DateTime aDateReceived
                    , decimal checkAmount
                    , decimal totalGrossEstimate
                    , int isStaff
                    , decimal reportedAmount //seem to do nothing with this
                    , int problemCode
                    , int totalEnvelopes)
        {            
            CurrentBatch = ServiceLocator.Current.GetInstance<Batch>();

            _orderBatchRepository.Add(CurrentBatch);

            CurrentBatch.OrderQualifierID = BatchOrderQualifier;
            CurrentBatch.OrderTypeCode = OrderTypeCode;
            CurrentBatch.StatusInstance = (int)BatchStatus.InProcess;
            CurrentBatch.CampaignID = nCampaignID;
            CurrentBatch.AccountID = nAccountID;
            CurrentBatch.OrderID = nOrderID;
            CurrentBatch.DateCreated = aDate;
            CurrentBatch.StartImportTime = DateTime.Now;
            CurrentBatch.DateReceived = aDateReceived;
            CurrentBatch.DateSent = aDateSent;
            CurrentBatch.CheckPayableToQSPAmount = checkAmount;
            CurrentBatch.ReportedEnvelopes = totalEnvelopes;
            CurrentBatch.ProblemID = problemCode;
            CurrentBatch.CampaignNetTotal = totalGrossEstimate;

            return CurrentBatch;
        }
        public Batch SetCurrentBatch(DateTime aDate, int BatchOrderQualifier,  int nCampaignID, int nAccountID)
        {
            CurrentBatch = ServiceLocator.Current.GetInstance<Batch>();
            _orderBatchRepository.Add(CurrentBatch);
            CurrentBatch.OrderID = -1;
            CurrentBatch.OrderQualifierID = BatchOrderQualifier;
            CurrentBatch.StatusInstance = (int)BatchStatus.InProcess; 
            CurrentBatch.CampaignID = nCampaignID;
            CurrentBatch.AccountID = nAccountID;
            CurrentBatch.DateCreated = aDate;
 
            return CurrentBatch;
        }

        public Teacher SetCurrentTeacher(string lastName, string middleInitial, string classroom)
        {

            IRepository<Teacher> _b = ServiceLocator.Current.GetInstance<IRepository<Teacher>>();
            Specification<Teacher> teacherExists = new Specification<Teacher>(t=>t.AccountID==Batch.AccountID &&
                                                                        t.Classroom==classroom &&
                                                                        t.LastName==lastName);
            var query = _b.DoQuery(teacherExists);
            Teacher _c = query.FirstOrDefault<Teacher>();

            if (_c == null)
            {
                _c = ServiceLocator.Current.GetInstance<Teacher>();
                _c.LastName = lastName;
                _c.MiddleInitial = middleInitial;
                _c.Classroom = classroom;
                _c.AccountID = (int)Batch.AccountID;
                _c.DateCreated = DateTime.Now;
                _c.DateChanged = _c.DateCreated;
                //_orderBatchRepository._ctx.AddObject("Teachers", _c);
                _orderBatchRepository.AddToT<Teacher>(ref _c);
            }
            else
                _orderBatchRepository.AttachToT<Teacher>(ref _c);


            CurrentTeacher = _c;
            

            return CurrentTeacher;
        }

        public Student SetCurrentStudent(Teacher aTeacher, string lastname, string firstname)
        {
            Student student = null;

            var ncnt = CurrentBatch.CustomerOrderHeaders.Where(coh => coh.Student != null && (coh.Student.FirstName == firstname
                                                                                        && coh.Student.LastName == lastname
                                                                                        && coh.Student.Teacher.LastName == aTeacher.LastName
                                                                                          && coh.Student.Teacher.Classroom == aTeacher.Classroom
                                                                                       ));

            foreach (var whatthe in ncnt)
            {
                student = whatthe.Student;
            }

            //Student not in the list of current COH's
            if (student == null)
            {
                IRepository<Student> _b = ServiceLocator.Current.GetInstance<IRepository<Student>>();
                Specification<Student> studentExists = new Specification<Student>(s => s.TeacherInstance == aTeacher.Instance &&
                                                                            s.LastName == lastname &&
                                                                            s.FirstName == firstname);
                var query = _b.DoQuery(studentExists);

                student = query.FirstOrDefault<Student>();

                if (student == null)
                {
                    student = ServiceLocator.Current.GetInstance<Student>();
                    student.Teacher = aTeacher;
                    student.LastName = lastname;
                    student.FirstName = firstname;
                    student.DateCreated = DateTime.Now;
                    _orderBatchRepository.AddToT<Student>(ref student);
                }
                else
                {                   
                    _orderBatchRepository.AttachToT<Student>(ref student);
                }
            }
            CurrentStudent = student;

            return student;

        }
        public CustomerOrderHeader SetCurrentCustomerOrderHeader(Batch _batch, Student _student)
        {
            CustomerOrderHeader _coh;
            _coh = ServiceLocator.Current.GetInstance<CustomerOrderHeader>();
            CurrentCOH = _coh;
            CurrentCOH.Batch = _batch;
            CurrentCOH.AccountID = CurrentBatch.AccountID;
            CurrentCOH.CampaignID = CurrentBatch.CampaignID;
            CurrentCOH.Type = (int)CustomerOrderHeaderType.Regular;
            CurrentCOH.Student = _student;

            CurrentCOH.StatusInstance = (int)CustomerOrderHeaderStatus.Good;
            CurrentCOH.NextDetailTransID = 0;
            CurrentCOH.CreationDate = DateTime.Now;
            CurrentCOH.NumberInvoicesSent = 0;
            CurrentCOH.ForceInvoice = false;
            CurrentCOH.DelFlag = false;


            _orderBatchRepository.AddToT<CustomerOrderHeader>(ref _coh);

            return CurrentCOH;
        }

        public InternetOrderID SetCurrentInternetOrderID(CustomerOrderHeader _coh, int nInternetOrderID)
        {
            InternetOrderID _IOI;

            _IOI = ServiceLocator.Current.GetInstance<InternetOrderID>();
            _IOI.InternetOrderID1 = nInternetOrderID;
            _IOI.CustomerOrderHeader = _coh;
            _orderBatchRepository.AddToT<InternetOrderID>(ref _IOI);


            return _IOI;
        }

        public Customer SetCurrentCustomerBillTo(CustomerOrderHeader _aCOH, string _fname, string _lname, string _addr1, string _addr2, string _city, string _province, string _postal, int _status = (int)CustomerStatus.Good)
        {

            CurrentCustomerBillTo = FindOrCreateCustomer(_fname,_lname,_addr1, _addr2, _city, _province, _postal);

            if (CurrentCustomerBillTo != null)
            {
                CurrentCustomerBillTo.StatusInstance = _status;
            }

            _aCOH.Customer = CurrentCustomerBillTo;
            return CurrentCustomerBillTo;

        }

        protected Customer FindOrCreateCustomer(string _fname, string _lname, string _addr1, string _addr2, string _city, string _province, string _postal)
        {
            Customer _c = null;

            var ncnt = CurrentBatch.CustomerOrderHeaders.Where(coh =>coh.Customer!= null && (coh.Customer.FirstName == _fname
                                                                                        && coh.Customer.LastName == _lname
                                                                                        && coh.Customer.Address1 == _addr1
                                                                                        && coh.Customer.Address2 == _addr2
                                                                                        && coh.Customer.City == _city
                                                                                        && coh.Customer.State == _province
                                                                                        && coh.Customer.Zip == _postal));
            foreach(var whatthe in ncnt)
            {

                CustomerOrderHeader coh2 = whatthe;

                _c = coh2.Customer;

            }
            if (_c == null)
            {
                 var foo  = from coh in CurrentBatch.CustomerOrderHeaders from
                                             cod in coh.CustomerOrderDetails 
                                             where cod.Customer != null && (cod.Customer.FirstName == _fname
                                                                                        && cod.Customer.LastName == _lname
                                                                                        && cod.Customer.Address1 == _addr1
                                                                                        && cod.Customer.Address2 == _addr2
                                                                                        && cod.Customer.City == _city
                                                                                        && cod.Customer.State == _province
                                                                                        && cod.Customer.Zip == _postal)
                                select cod.Customer;
                 foreach (var cod2 in foo)
                 {

                     _c = cod2;
                 }
                 
                 Debug.WriteLine(foo.ToString());

            }
           

            if (_c == null)
            {
                IRepository<Customer> _b = ServiceLocator.Current.GetInstance<IRepository<Customer>>();
                Specification<Customer> customerExists = new Specification<Customer>(c => c.FirstName == _fname
                                                                                            && c.LastName == _lname
                                                                                            && c.Address1 == _addr1
                                                                                            && c.Address2 == _addr2
                                                                                            && c.City == _city
                                                                                            && c.State == _province
                                                                                            && c.Zip == _postal);
                var query = _b.DoQuery(customerExists);

                _c = query.FirstOrDefault<Customer>();

                if (_c == null)
                {
                    _c = ServiceLocator.Current.GetInstance<Customer>();
                    _c.Address1 = _addr1;
                    _c.Address2 = _addr2;
                    _c.City = _city;
                    _c.State = _province;
                    _c.Zip = _postal;
                    _c.ChangeDate = DateTime.Now;
                    _orderBatchRepository.AddToT<Customer>(ref _c);
                }
                else
                {
                    _orderBatchRepository.AttachToT<Customer>(ref _c);
                }
            }
            return _c;
        }

        public void SetCurrentPayment(CustomerOrderHeader _aCOH, PaymentNode _aXMLPaymentNode)
        {

            _aCOH.PaymentMethodInstance = _aXMLPaymentNode.Type;

            if (_aXMLPaymentNode.Type != (int)PaymentMethodInstance.Cash
                && _aXMLPaymentNode.Type != (int)PaymentMethodInstance.Error
                && _aXMLPaymentNode.Type != (int)PaymentMethodInstance.Other
                && _aCOH.StatusInstance == (int)CustomerOrderHeaderStatus.Good)
            {
                CreditCardPayment _ccp = ServiceLocator.Current.GetInstance<CreditCardPayment>();
                CustomerPaymentHeader _cph = ServiceLocator.Current.GetInstance<CustomerPaymentHeader>();


                // Set CustomerPaymentheader
                _cph.CustomerOrderHeader = CurrentCOH;
                _cph.DateCreated = DateTime.Now;
                _cph.DateChanged = DateTime.Now;
                _cph.PaymentBatchDate = DateTime.Now;
                _cph.PaymentBatchID = 0;
                _cph.IsCreditCard = true;

                //Set CreditCardPayment
                _ccp.CustomerPaymentHeader = _cph;
                _ccp.DateCreated = DateTime.Now;
                _ccp.DateChanged = DateTime.Now;
                DateTime aDefault = new DateTime(1995, 1, 1);
                _ccp.AuthorizationDate = aDefault;
                _ccp.VeriSignID = _aXMLPaymentNode.VERISIGNID;
                _ccp.StatusInstance = _aXMLPaymentNode.STATUSINSTANCE;
                _ccp.ExpirationDate = _aXMLPaymentNode.EXP;

                // If ccp is in error set customerpaymentheader to error
                if (_ccp.StatusInstance == (int)CreditCardPaymentStatus.Error)
                {
                    _cph.StatusInstance = (int)CustomerPaymentHeaderStatus.Error;
                }
                _cph.TotalAmount = (decimal)CurrentCOH.CustomerOrderDetails.Sum(cod => cod.Price);

                if (CurrentBatch.OrderQualifierID != (int)BatchOrderQualifier.Internet)
                {
                    if (CurrentCreditCardBatch == null)
                    {
                        CreditCardBatch _c = ServiceLocator.Current.GetInstance<CreditCardBatch>();
                        _orderBatchRepository.AddToT<CreditCardBatch>(ref _c);
                        CurrentCreditCardBatch = _c;
                    }
                    _ccp.UnEncryptedCC = _aXMLPaymentNode.CreditCardNumber;
                    _ccp.CreditCardNumber = _aXMLPaymentNode.CreditCardNumber.Substring(0, 4) + "**********";
                    _ccp.CreditCardBatch = CurrentCreditCardBatch;
                }
                else
                {
                    _ccp.CreditCardNumber = _aXMLPaymentNode.CreditCardNumber;

                    _ccp.CreditCardBatch = null;
                }
            }
        

            //If COH is paid by cash or for an internet order then COD is considered paid
            if (_aCOH.PaymentMethodInstance == (int)PaymentMethodInstance.Cash ||
                CurrentBatch.OrderQualifierID == (int)BatchOrderQualifier.Internet)
            {

                foreach (var _cod in _aCOH.CustomerOrderDetails)
                {
                    if (_cod.PriceOverrideID == (int)PriceOverride.InvalidPrice)
                        _cod.StatusInstance = (int)CustomerOrderDetailStatus.Error;
                    else
                        _cod.StatusInstance = (int)CustomerOrderDetailStatus.Paid;

                    //Set payment applied fields
                    _cod.TaxA = _cod.Tax;
                    _cod.Tax2A = _cod.Tax2;
                    _cod.PriceA = _cod.PriceA;
                }              
            }         
        }

        public void SetCurrentCustomerOrderDetail(CustomerOrderHeader _aCOH, MagOrderDetailNode aNode, int nPaymentMethod, AddressNode aShipTo = null)
        {
            Debug.Assert(_aCOH != null);

            CustomerOrderDetail _cod = ServiceLocator.Current.GetInstance<CustomerOrderDetail>();

            _cod.CustomerOrderHeader = _aCOH;
            _aCOH.NextDetailTransID += 1;
            _cod.TransID = (int)_aCOH.NextDetailTransID;

            _cod.ProductCode = aNode.PRODUCTCODE;
            _cod.ProductName = aNode.PRODUCTNAME;
            _cod.Price = Convert.ToDecimal(aNode.PRICE);
            _cod.ProductType = Convert.ToInt32(aNode.PRODUCTLINE);
            _cod.Renewal = aNode.RENEWA;
            _cod.StatusInstance = Convert.ToInt32(aNode.STATUSINSTANCE);
            _cod.Recipient = aNode.RECIPIENT;
            _cod.DelFlag = false;
            _cod.Quantity = Convert.ToInt32(aNode.QUANTITY);
            _cod.ChangeDate = DateTime.Now;
            _cod.CreationDate = DateTime.Now;
            _cod.PriceOverrideID = aNode.PRICEOVERRIDE;

            if (aShipTo != null)
            {
                Customer aCustomer = FindOrCreateCustomer("","",aShipTo.Address1, aShipTo.Address2, aShipTo.City, aShipTo.Province, aShipTo.Postal);
                aCustomer.StatusInstance = (int)CustomerStatus.Good;
                _cod.Customer = aCustomer;
              
            }

            Boolean bPricingOk = SetCustomerOrderDetailMagPricingData(_cod);

            //Taxes
            if (_cod.PricingDetailsID > 0)
            {             
                TaxService ts = ServiceLocator.Current.GetInstance<TaxService>();
                TaxResult tResult = ts.CalculateTax(DateTime.Now, (decimal)_cod.Price, (int)_cod.ProgramSectionID, _cod.ProductCode, "", (int)CurrentBatch.CampaignID
                                                    , (int)_cod.PricingDetailsID, "ON"); //TODO get account province 
                _cod.Tax = tResult.Tax1;
                _cod.Tax2 = tResult.Tax2;
                _cod.Gross = tResult.Gross;
                _cod.Net = tResult.Net;           
            }            
        }

        private Boolean SetCustomerOrderDetailMagPricingData(CustomerOrderDetail _cod)
        {
            Boolean bOk = true;
            ProductAndPricingService _pss = ServiceLocator.Current.GetInstance<ProductAndPricingService>();

            //Find Closest matching offer
            if (CurrentBatch.OrderQualifierID == (int)BatchOrderQualifier.Internet)
            {
                //Internet orders use the remit code so we have to find the product code and force it into COD
                ProductCodeFromRemitCode _code = _pss.GetProductCodeFromRemitCode(_cod.ProductCode, (int)_cod.Quantity, (int)CurrentBatch.CampaignID);
                _cod.ProductCode = _code.ProductCode;
            }

            //CD_ORDERDETAIL_OVERRIDE_COUPON
            int priceoverride = (int)_cod.PriceOverrideID;
            // TODO Get rid of magic 5
            PricingData pData = _pss.GetMagazineClosestMatchingOffer(_cod.ProductCode, (int)_cod.Quantity, (decimal)_cod.Price, (int)CurrentBatch.CampaignID, 46001, 5.00, (int)_cod.PriceOverrideID, out priceoverride);
            if (pData != null && priceoverride != (int)PriceOverride.InvalidPrice)
            {
                _cod.PricingDetailsID = pData.MagPrice_instance;
                _cod.ProgramSectionID = pData.ProgramSection;
                _cod.PriceOverrideID = priceoverride;

                //If another offer is used change the term and price for this COD
                if (priceoverride == (int)PriceOverride.ClosestMatchingOffer)
                {
                    _cod.Quantity = pData.Term;
                    _cod.Price = pData.Price;
                }
            
                //sort of stupid should return during closest matching offer
                IRepository<ProductPricings> _b = ServiceLocator.Current.GetInstance<IRepository<ProductPricings>>();
                Specification<ProductPricings> exists = new Specification<ProductPricings>(p => p.MagPrice_Instance == _cod.PricingDetailsID);
                var query = _b.DoQuery(exists);

                ProductPricings _c = query.FirstOrDefault<ProductPricings>();
                _cod.CatalogPrice = _c.QSP_Price;
            }

            return bOk;
        }

        public Boolean SaveCurrentBatch()
        {
            Boolean bOk = true;
  
            if (CurrentCreditCardBatch != null)
            {
                CurrentCreditCardBatch.TotalRecordCount = 0;
                CurrentCreditCardBatch.TotalDollarAmount = 0;
                CurrentCreditCardBatch.DateCreated = DateTime.Now;
                CurrentCreditCardBatch.ChangeDate = DateTime.Now;
                CurrentCreditCardBatch.StartImportTime = DateTime.Now;
                CurrentCreditCardBatch.EndImportTime = DateTime.Now;
                CurrentCreditCardBatch.OutputFileName = "";

                //Set status and fields
                //Need to look thru all COD's
                foreach (var coh in CurrentBatch.CustomerOrderHeaders.Where(coh=>coh.PaymentMethodInstance == (int)PaymentMethodInstance.MC || coh.PaymentMethodInstance == (int)PaymentMethodInstance.Visa))
                {
                    foreach (var cod in coh.CustomerOrderDetails)
                    {
                        CurrentCreditCardBatch.TotalRecordCount += 1;
                        CurrentCreditCardBatch.TotalDollarAmount += (int)(cod.Price * 100);
                        cod.StatusInstance = (int)CustomerOrderDetailStatus.PaidPending;
                    }
                }
                foreach (var ccp in CurrentCreditCardBatch.CreditCardPayments)
                {
                    ccp.StatusInstance = (int)CreditCardPaymentStatus.NeedsToBeSent;
                }

                CurrentBatch.StatusInstance = (int)BatchStatus.CCPending;
                CurrentCreditCardBatch.Status = (int)BatchStatus.CCPending;
            }
            else
                CurrentBatch.OriginalStatusInstance = (int)BatchStatus.InProcess;



            int i = _orderBatchRepository.Save();



            var cList = from pph in
                            (from coh in CurrentBatch.CustomerOrderHeaders
                             where coh.PaymentMethodInstance == (int)PaymentMethodInstance.MC || coh.PaymentMethodInstance == (int)PaymentMethodInstance.Visa
                             select coh.CustomerPaymentHeaders)
                        select pph;
            foreach (var cph in cList)
            {
                foreach (var realcph in cph)
                {
                    CreditCardPayment ccp = realcph.CreditCardPayment;
                    if (ccp.StatusInstance == (int)CreditCardPaymentStatus.NeedsToBeSent)
                    {

                    }
                }
            }
          
            return bOk;
        }


        public Boolean ValidateCurrentCOH()
        {
            Boolean bok = true;

            //If any customers are in error then set order to in error

            return bok;
        }

        public Boolean ValidateCurrentBatch()
        {
            Boolean bok = true;


            return bok;
        }

        public void SetCurrentBatchStatistics()
        {
            if (CurrentBatch.CustomerOrderHeaders.Count > 0)
            {
                CurrentBatch.CalculatedAmount = CurrentBatch.CustomerOrderHeaders.Sum(coh => coh.CustomerOrderDetails.Sum(cod => cod.Price));
                CurrentBatch.EnterredCount = CurrentBatch.CustomerOrderHeaders.Sum(coh => coh.CustomerOrderDetails.Count());

            
                CurrentBatch.TeacherCount = CurrentBatch.CustomerOrderHeaders
                            .GroupBy(coh => new { Col1 = coh.Student.Teacher.LastName, col2 = coh.Student.Teacher.Classroom })
                            .Count();


                CurrentBatch.StudentCount = CurrentBatch.CustomerOrderHeaders
                            .GroupBy(coh => new { Col1 = coh.Student.LastName, col2 = coh.Student.FirstName })
                            .Count();

               
                //Customer count is bill to
                CurrentBatch.CustomerCount = CurrentBatch.CustomerOrderHeaders
                            .GroupBy(coh => new
                            {
                                Col1 = coh.Customer.LastName,
                                col2 = coh.Customer.FirstName
                              ,
                                col3 = coh.Customer.Address1
                                ,
                                col4 = coh.Customer.Address2
                                ,
                                col5 = coh.Customer.City
                                ,
                                col6 = coh.Customer.State
                                ,
                                col7 = coh.Customer.Zip
                            })
                            .Select(a => a.Count())
                            .First<int>();
            }
            CurrentBatch.EndImportTime = DateTime.Now;
            CurrentBatch.ChangeDate = DateTime.Now;
            CurrentBatch.ChangeUserID = "IMPO";

        }
    }
}
