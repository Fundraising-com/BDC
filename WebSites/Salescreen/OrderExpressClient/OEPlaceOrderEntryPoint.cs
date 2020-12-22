using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using efundraising.EFundraisingCRM;
using System.Collections.Generic;
using QSP.Business.Fulfillment.Domain;
using QSP.Business.Fulfillment;
//using BusinessObject = QSP.Business.Fulfillment;
using QSPForm.Business;
using NHibernate;
using NHibernate.Criterion;
using System.Data;
using efundraising.Diagnostics;
using System.Linq;
using System.Data;
using BusinessCommunication = QSPForm.Business.Communication;


/*Dim client As New Net.WebClient
client.DownloadString(url)
 * 
 * */


//line 711 Product not process by OE



namespace OrderExpressClient
{
	/// <summary>su
	/// Summary description for OEPlaceOrderEntryPoint.
	/// </summary>
	public class OEPlaceOrderEntryPoint:efundraising.Core.BusinessBase
	{
        private int IncompletePEID = 0;
        private int InProcessID = 0;
        private int OnHoldID = 0;
        private int PendingApprovalID = 0;
        private int CancelledID = 0;
        private int CancelledOEOnlyID = 9;
             
		public OEPlaceOrderEntryPoint()
		{
			//
			// TODO: Add constructor logic hereyou watch you
		}


        public static void Main()
        {
        }

		public void Execute()
		{
            OrderExpressClient.com.qsp.ws.ProgramType programType = new OrderExpressClient.com.qsp.ws.ProgramType();
		
			if(programType == null)
			{
				// TODO throw exception failed to retrive program type collection
			
			}
			else
			{

			}

		}



        public OrderExpressClient.com.qsp.ws.Order GetOrder(int orderID)
        {
            OrderExpressClient.com.qsp.ws.EfundraisingServices oe = new OrderExpressClient.com.qsp.ws.EfundraisingServices();
            OrderExpressClient.com.qsp.ws.Order order = new OrderExpressClient.com.qsp.ws.Order();

            try
            {
                order = oe.GetOrder(orderID, true);
            }
            catch (Exception x)
            {
                Logger.LogError("Error getting status of order " + orderID + ". Probably order doesnt exists. " + x.StackTrace);
            }


            return order;
        }
		public OrderExpressClient.com.qsp.ws.OrderStatus GetOrderStatus(int orderID, ref DateTime orderShippedDate)
		{
			OrderExpressClient.com.qsp.ws.EfundraisingServices oe = new OrderExpressClient.com.qsp.ws.EfundraisingServices();
			OrderExpressClient.com.qsp.ws.OrderStatus orderStatus = new OrderExpressClient.com.qsp.ws.OrderStatus();
			
			try
			{
			   orderStatus = oe.GetOrderStatus(orderID,true);
               bool specified = true;
			   oe.GetOrderShippedDate(orderID, true, out orderShippedDate, out specified);
			}
			catch(Exception x)
			{
				Logger.LogError("Error getting status of order " + orderID + ". Probably order doesnt exists. " + x.StackTrace);
            }

		
	    	return orderStatus;
    	}


		public OrderExpressClient.com.qsp.ws.Warehouse[] GetWarehouseList()
		{
            OrderExpressClient.com.qsp.ws.Warehouse[] warehouses = null;
			try
			{
				OrderExpressClient.com.qsp.ws.EfundraisingServices oe = new OrderExpressClient.com.qsp.ws.EfundraisingServices();
				warehouses = oe.GetWarehousePickupList(true,true);
			}
			catch(Exception x)
			{
				Logger.LogError("Error getting warehouselist",x);
			}

		
			return warehouses;
		}


        public string PlaceOrder(Sale sale, int clientID, string clientSequenceCode, Hashtable hWebConfig, DataTable saleTaxAndShip, int crmUserId, int as400Id)
		{
            int debug = 1;
			string error = "";
            bool errorFound = false;
            bool isProcessed = false;
				
			try
			{
                //if we have the id, we dont want to modify anything except the status to cancel
                if (as400Id > 0)
                {
                    isProcessed = true;
                }

                OrderData orderData = new OrderData();
                string saleStatus = "";
                //get sale status
                efundraising.EFundraisingCRM.SalesStatus ss = efundraising.EFundraisingCRM.SalesStatus.GetSalesStatusByID(sale.SalesStatusId);
                if (ss != null)
                {
                    saleStatus = ss.Description;
                }

                //check if we edit
                bool edit = false;
                if (sale.ExtOrderID > 0)
                {
                    edit = true;
                }

                //get tax and shipping
                decimal shippingFee = 0;
                decimal GSTfee = 0;
                decimal PSTfee = 0;
                decimal GSTrate = 0;
                decimal PSTrate = 0;
                decimal discount = 0;
                decimal surcharge = 0;
                string qspConnStr = hWebConfig["qspConnStr"].ToString();
                int discountReasonID = 0;
                int surchargeReasonID = 0;
                string comment = "";
                bool isPrepaid = false;
                int saleId = sale.SalesId;
                int qspOrderID = sale.ExtOrderID;


                //TO DO
                //CHECK IF OE STATUS IS PROCESSED OR RELEASE, IF SO EXIT



                /////////
                int userID = 0;
                int fmID = 0;
                int sourceID = 0;
                bool commissionFound = true;
        

                if (sale.PaymentTermId == 2 || sale.PaymentTermId == 8 || sale.PaymentTermId == 12)
                {
                    isPrepaid = true;
                }

                LogSimple.LogInfo("Sale :" + saleId + " Getting web.config values");
                if (saleTaxAndShip != null)
                {
                    foreach (DataRow dr in saleTaxAndShip.Rows)
                    {
                        if (Convert.ToInt32(dr["SaleID"]) == saleId)
                        {
                            GSTfee = Convert.ToDecimal(dr["GST"]);
                            PSTfee = Convert.ToDecimal(dr["PST"]);
                            GSTrate = Convert.ToDecimal(dr["GSTRate"]);
                            PSTrate = Convert.ToDecimal(dr["PSTRate"]);
                            shippingFee = Convert.ToDecimal(dr["Shipping"]);
                            discount = -(Convert.ToDecimal(dr["Discount"]));
                            surcharge = Convert.ToDecimal(dr["Surcharge"]);
                            discountReasonID = Convert.ToInt32(dr["DiscountReasonID"]);
                            surchargeReasonID = Convert.ToInt32(dr["SurchargeReasonID"]);

                            comment = dr["Comment"].ToString();

                            break;
                        }
                    }
                }
                
                //get client 
                LogSimple.LogInfo("Sale :" + saleId + " Getting Client");
				Client client = Client.GetClientByID(clientID,clientSequenceCode);
                client.Organization = client.Organization.Replace("*", "");

                Lead lead = null;

                int oeOrganizationTypeID = 4;
                int oeOrganizationLevelID = 4;
                //get group type
                GroupType gt = GroupType.GetGroupTypeByLeadID(client.LeadId);
                if (gt.Description.IndexOf("School") > -1){
                    oeOrganizationTypeID = 5;//campus

                     lead = Lead.GetLeadByID(client.LeadId);
                     string org = efundraising.EFundraisingCRM.OrganizationType.GetOrganizationTypeByID(lead.OrganizationTypeId);

                     if (org.IndexOf("Elementary") > -1)
                     {
                         oeOrganizationLevelID = 1;
                     
                     }else if (org.IndexOf("Middle") > -1)
                     {
                         oeOrganizationLevelID = 2;
                     }else if (org.IndexOf("High") > -1)
                     {
                         oeOrganizationLevelID = 3;
                     }
                
                }

               

                //get source
                GetSourceID(sale.ConsultantId, client.LeadId, ref sourceID);
                //get FM
                
                bool userError = GetFMID(ref fmID, sale.ConsultantId, client.LeadId, ref error, saleId);
                if (userError)
                {
                    errorFound = true;
                    error = "Error finding the FM on OE";
                }
                else
                {
                    userError = GetUserID(ref userID, ref error, saleId, crmUserId);
                    if (userError)
                    {
                        errorFound = true;
                        error = "Error finding the user on OE";
                    }

                }

                //double check code came out with an ID or an error
                if ((fmID == 0 || userID == 0) && !errorFound)
                {
                    error = "FM error on OE";
                    errorFound = true;
                }
                


                LogSimple.LogInfo("Sale :" + saleId + " Getting addresses from EFR");
				//get clientAddress
				ClientAddress billingAddress = ClientAddress.GetClientAddressByIdSequenceAddressType(clientID,clientSequenceCode,"BT");
				ClientAddress shippingAddress = ClientAddress.GetClientAddressByIdSequenceAddressType(clientID,clientSequenceCode,"ST");
				if (shippingAddress == null)
				{
					shippingAddress = billingAddress;
				}
                
                QSPForm.Business.Communication.Notifications notification = new QSPForm.Business.Communication.Notifications();
                
                LogSimple.LogInfo("Sale :" + saleId + " Checking for location");
                if (shippingAddress.Location == null)
                {
                    error = "Warning: No Location was entered for shipping";
                    errorFound = true;
                }
                
                //remove weird charactes from address
                shippingAddress.StreetAddress = shippingAddress.StreetAddress.Replace("*", "");
                billingAddress.StreetAddress = billingAddress.StreetAddress.Replace("*", "");


                //validate Address length
                if (shippingAddress.StreetAddress.Length > 50 || billingAddress.StreetAddress.Length > 50)
                {
                    error = "Warning: Address length is too long";
                    errorFound = true;

                }

                 //validate zips at least 5 code
                if (shippingAddress.ZipCode.Length < 5 || billingAddress.ZipCode.Length < 5)
                {
                    error = "Warning: Zip Code Invalid";
                    errorFound = true;
                }

                //get program types
                Cache cache = HttpContext.Current.Cache;

                LogSimple.LogInfo("Sale :" + saleId + " Get program type ids");
                ICriteria criteria = ProgramType.CreateCriteria2();
                criteria.Add(Expression.Eq(ProgramType.ProgramTypeNameProperty, "Other Food"));
                List<ProgramType> programTypes = ProgramType.GetProgramTypeList(criteria);
                int programTypeID = programTypes[0].ProgramTypeId;

                criteria = ProgramType.CreateCriteria2();
                criteria.Add(Expression.Eq(ProgramType.ProgramTypeNameProperty, "WFCChocolate"));
                programTypes = ProgramType.GetProgramTypeList(criteria);
                int programTypeIDWFC = programTypes[0].ProgramTypeId;
 
                if (!(errorFound))
                {

                    int formID = 0;
                    int catalogItemCategoryID = 0;
                    string catalogItemCategoryName = "";
                    int orderStatusID = 0;

                    List<Form> forms = null;
                    List<CatalogItem> catalogItems = null;

                    //set initial values
                    LogSimple.LogInfo("Sale :" + saleId + " Set web.config values");
                    string form = hWebConfig["Form"].ToString();
                    string chargeTo = hWebConfig["ChargeTo"].ToString();
                    int catalogID = Convert.ToInt32(hWebConfig["CatalogID"].ToString());
                    int createUserID = Convert.ToInt32(hWebConfig["createUserID"].ToString());
                    int businessDivisionID = Convert.ToInt32(hWebConfig["businessDivisionID"].ToString());
                    DateTime startDate = Convert.ToDateTime(hWebConfig["startDate"].ToString());
                    DateTime endDate = Convert.ToDateTime(hWebConfig["endDate"].ToString());
                    int fiscalYear = Convert.ToInt32(hWebConfig["fiscalYear"].ToString());
                

                    int productIndex = -1;
                    int accountID = 0;

                   


                    //set ship and bill address	
                    LogSimple.LogInfo("Sale :" + saleId + " Set billing address");
                    QSP.Business.Fulfillment.PostalAddress bAddress = bAddress = new QSP.Business.Fulfillment.PostalAddress();
                    if (edit)
                    {
                        bAddress.PostalAddressId = Convert.ToInt32(GetBillingPostalAddressID(qspOrderID));
                    }


                    //TO DO Validate Zip Codes for both countris
                    bAddress.Address1 = billingAddress.StreetAddress;
                    bAddress.City = billingAddress.City;
                    bAddress.SubdivisionCode = GetSubDivisionCode(billingAddress.StateCode);
                    bAddress.County = "NULL";                   
                    bAddress.FirstName = client.FirstName; //not used by as400
                    bAddress.LastName = client.LastName;  //not used by as400
                    bAddress.Name = client.Organization; //not used by as400
                    bAddress.CreateUserId = userID;

                    string zip4 = "";
                    LogSimple.LogInfo("Sale :" + saleId + " Setting biiling zip");
                    if (billingAddress.CountryCode != "CA")
                    {
                        bAddress.Zip = billingAddress.ZipCode.Substring(0, 5);

                        if (billingAddress.ZipCode.Length >= 9)
                        {
                            zip4 = billingAddress.ZipCode.Replace("-", "").Substring(5, 4);
                            if (zip4.Length == 4)
                            {
                                bAddress.Zip4 = zip4;
                            }
                        }
                    }
                    else
                    {
                        bAddress.Zip = billingAddress.ZipCode.Replace(" ", "").ToUpper();
                    }

                    int addressZoneID = billingAddress.AddressZoneId;
                    LogSimple.LogInfo("Sale :" + saleId + " Getting address zone from qsp");
                    efundraising.EFundraisingCRM.AddressZone az = efundraising.EFundraisingCRM.AddressZone.GetAddressZoneByID(addressZoneID);
                    if (az.Description == "Commercial")
                    {
                        bAddress.ResidentialArea = false;
                    }
                    else
                    {
                        bAddress.ResidentialArea = true;
                    }

                    QSP.Business.Fulfillment.PostalAddress sAddress = new QSP.Business.Fulfillment.PostalAddress();
                    if (edit)
                    {
                        sAddress.PostalAddressId = GetShippingPostalAddressID(qspOrderID);
                    }


                    LogSimple.LogInfo("Sale :" + saleId + " Setting shipping address");
                    //get first name last name from attention of
                    string attn = shippingAddress.AttentionOf.Trim();
                    int pos = attn.IndexOf(" ", 1);
                    string fName = client.FirstName;
                    string lName = client.LastName;

                    if (pos > 1)
                    {
                        fName = attn.Substring(0, pos);
                        if (attn.Substring(pos + 1).Length > 50)
                        {
                            lName = attn.Substring(pos + 1, 50); //max length is 50
                        }
                        else
                        {
                            lName = attn.Substring(pos + 1);
                        }
                    }

                    sAddress.Address1 = shippingAddress.StreetAddress;
                    sAddress.City = shippingAddress.City;
                    sAddress.County = "NULL";
                    sAddress.SubdivisionCode = GetSubDivisionCode(shippingAddress.StateCode);
                    sAddress.FirstName = fName;
                    sAddress.LastName = lName;
                    sAddress.Name = shippingAddress.Location;
                    sAddress.CreateUserId = userID;

                    LogSimple.LogInfo("Sale :" + saleId + " Setting shipping zip");
                    if (shippingAddress.CountryCode != "CA")
                    {
                        sAddress.Zip = shippingAddress.ZipCode.Substring(0, 5);

                        if (shippingAddress.ZipCode.Length >= 9)
                        {
                            zip4 = shippingAddress.ZipCode.Replace("-", "");
                            zip4 = zip4.Substring(5, 4);
                            if (zip4.Length == 4)
                            {
                                sAddress.Zip4 = zip4;
                            }
                        }
                    }
                    else
                    {
                        sAddress.Zip = shippingAddress.ZipCode.Replace(" ", "").ToUpper();
                    }
                    addressZoneID = shippingAddress.AddressZoneId;
                    az = efundraising.EFundraisingCRM.AddressZone.GetAddressZoneByID(addressZoneID);
                    if (az.Description == "Commercial")
                    {
                        sAddress.ResidentialArea = false;
                    }
                    else
                    {
                        sAddress.ResidentialArea = true;
                    }

                    QSP.Business.Fulfillment.ShipmentGroup shipmentGroup = new QSP.Business.Fulfillment.ShipmentGroup();
                    if (edit)
                    {
                        LogSimple.LogInfo("Sale :" + saleId + " Setting existing shipment group");
                        shipmentGroup.ShipmentGroupId = GetShipmentGroupID(qspOrderID);
                    }

                    //get phone number (max 10 digits)
                    QSP.Business.Fulfillment.PhoneNumber phoneNumber = new QSP.Business.Fulfillment.PhoneNumber();
                    if (edit)
                    {
                        LogSimple.LogInfo("Sale :" + saleId + " Setting existing phone number");
                        phoneNumber.PhoneNumberId = GetPhoneNumberID(qspOrderID);
                    }


                    phoneNumber.CreateUserId = userID;
                    if (shippingAddress.Phone1 != null && shippingAddress.Phone1.Length >= 10)
                    {
                        phoneNumber.Phone_Number = shippingAddress.Phone1.Replace("-", "");
                    }
                    else if (shippingAddress.Phone2 != null && shippingAddress.Phone2.Length >= 10)
                    {
                        phoneNumber.Phone_Number = shippingAddress.Phone2.Replace("-", "");
                    }
                    else if (client.DayPhone != null && client.DayPhone.Length >= 10)
                    {
                        phoneNumber.Phone_Number = client.DayPhone.Replace("-", "");
                        if (client.DayPhoneExt != null)
                        {
                            phoneNumber.Phone_Number = client.DayPhone + client.DayPhoneExt;
                        }
                    }
                    else
                    {
                        phoneNumber.Phone_Number = "0000000000";

                    }

                    if (phoneNumber.Phone_Number.Length >= 10)
                    {
                        phoneNumber.Phone_Number = phoneNumber.Phone_Number.Substring(0, 10);
                    }

                    shipmentGroup.DeliveryWarehouseId = (billingAddress.WarehouseId == 0 || billingAddress.WarehouseId == int.MinValue) ? null : (int?)billingAddress.WarehouseId;
                    shipmentGroup.RequestedDeliveryDate = sale.ScheduledDeliveryDate;
                    shipmentGroup.RequestedDeliveryTime = sale.ScheduledDeliveryDate;


                    //check if pickup
                    if (shippingAddress.PickUp)
                    {
                        LogSimple.LogInfo("Sale :" + saleId + " Getting warehouse from qsp");
                        criteria = DeliveryMethod.CreateCriteria2();
                        criteria.Add(Expression.Eq(DeliveryMethod.DeliveryMethodNameProperty, "Pick up at Warehouse"));
                        List<DeliveryMethod> deliveryMethodList = DeliveryMethod.GetDeliveryMethodList(criteria);
                        int deliveryMethodID = deliveryMethodList[0].DeliveryMethodId;

                        shipmentGroup.DeliveryMethodId = deliveryMethodID;
                        shipmentGroup.DeliveryWarehouseId = shippingAddress.WarehouseId;

                    }
                    else
                    {
                        shipmentGroup.DeliveryMethodId = 1;//CommonCarrier;
                    }
                    shipmentGroup.ShippingCharges = shippingFee;
                    shipmentGroup.CreateUserId = userID;

                    List<OrderDetailData> orderDetailDataList = new List<OrderDetailData>();

                    int nbProducts = sale.SalesItems.Count;
                    int productFound = 0;
                    int productTBD = 0;

                    //get specific status id
                    setQSPOrderStatusIDs();

                    //for each item
                    LogSimple.LogInfo("Sale :" + saleId + " For each items....");

                    //do a table with all the product code of the sale for better compare
                    List<string> products = new List<string>();
                    foreach (SalesItem item in sale.SalesItems)
                    {
                        ScratchBook sc = ScratchBook.GetScratchBookByID(item.ScratchBookId);
                        string productCode = sc.ProductCode;
                        products.Add(productCode);

                    }


                    Hashtable samplesDuplicateFound = new Hashtable();
                    Hashtable productCodeLink = new Hashtable();
                    string missingItems = "";

                    //get commissions for the sale
                    DataTable dtCommissions = efundraising.EFundraisingCRM.Sale.GetSaleCommssions(sale.SalesId);
                    

                    //create a new item 
                    foreach (SalesItem item in sale.SalesItems)
                    {

                                               

                        /////////////////////////////////////
                        int catalogItemID = 0;
                        productIndex++;

                        LogSimple.LogInfo("Sale :" + saleId + " Get scratchbook from efr (sbid= " + item.ScratchBookId + ")");
                        ScratchBook sc = ScratchBook.GetScratchBookByID(item.ScratchBookId);
                        string itemName = sc.Description;
                        string productCode = sc.ProductCode;
                        //look for FR to take out
                        if (sc.ProductCode.StartsWith("FR"))
                        {
                            productCode = sc.ProductCode.Substring(2);
                        }

                        
                        //GET COMMISSION
                        decimal commissionRate = 0;
                        var query = from comm in dtCommissions.AsEnumerable()
                                    where comm.Field<int>("scratch_book_id") == item.ScratchBookId
                                    select comm;
                        try{
                           DataTable dt = query.CopyToDataTable();
                           commissionRate = Convert.ToDecimal(dt.Rows[0]["commission_rate_no_free"]);
                        }catch{
                           // if (sc.ProductClassId != 8 && sc.ProductClassId != 2)//brochure, its normal to have nothing back
                            if (item.SalesAmount != 0)
                            {
                                return "Sale " + sale.SalesId + ": Commission error on item " + sc.ProductCode;
                                /*bool found = false;
                                string productCodes = System.Configuration.ConfigurationSettings.AppSettings["ProductsNoCommission"];
                                string[] codes = productCodes.Split(',');
                                foreach (string code in codes){
                                    if (code == sc.ProductCode){
                                        found = true;
                                    }
                                }

                                if (!found){
                                    return "Sale " + sale.SalesId + ": Commission error on item " + sc.ProductCode;
                                }*/
                            }
                           
                        }

                        //IF SAMPLE, CHECK IF SAME PRODUCT EXISTS IN THE SALE
                        //IF YES, WE SKIP IT FOR NOW AND ADJUST THE QTY AFTER
                        //IF NO, PROCESS NORMALLY

                        int itemFound = 0;

                        int i = itemName.IndexOf("Sample", StringComparison.InvariantCultureIgnoreCase);
                        if (itemName.IndexOf("Sample") >= 0)
                        {
                            foreach (string code in products)
                            {
                                if (code == productCode)
                                {
                                    itemFound++;
                                }
                            }
                        }

                        if (itemFound > 1)
                        {
                            samplesDuplicateFound.Add(productCode, item.QuantitySold);

                        }
                        else
                        { //item not already exists

                            int qty = item.QuantityFree + item.QuantitySold;

                            if (qty > 0)
                            {
                                //get form
                                LogSimple.LogInfo("Sale :" + saleId + " Get form");
                                criteria = Form.CreateCriteria2();
                                criteria.Add(Expression.Eq(Form.FormNameProperty, form));
                                List<Form> formList = Form.GetFormList(criteria);
                                
                               
                                formID = formList[0].FormId;

                                // Hack for Program agreement sync from qsp to as400. Updating formid for Otis and pvf orders. Before sales is inserted in OE Order table 
                                try
                                {
                                    foreach (SalesItem items in sale.SalesItems)
                                    {
                                        if (items.ProductClassId == 43)
                                        {
                                            formID = 172;
                                        }
                                        if (items.ProductClassId == 42)
                                        {
                                            formID = 92;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }

                                int catalogItemDetailID = 0;
                                //get product id
                                LogSimple.LogInfo("Sale :" + saleId + " Get Product ID");
                                criteria = Product.CreateCriteria2();
                                criteria.Add(Expression.Eq(Product.ProductCodeProperty, productCode));
                                criteria.Add(Expression.Eq(Product.BusinessDivisionIdProperty, businessDivisionID));
                                List<Product> productList = Product.GetProductList(criteria);
                                if (productList != null && productList.Count > 0)
                                {
                                    //get last inserted, others wont match catalog id if we made many script inserts
                                    int productID = productList[productList.Count - 1].ProductId;
                                    string productName = productList[productList.Count - 1].ProductName;
                                    decimal retailPrice = Convert.ToDecimal(productList[productList.Count - 1].Price);

                                    //get catalog item
                                    LogSimple.LogInfo("Sale :" + saleId + " Get Catalog Item");
                                    criteria = CatalogItem.CreateCriteria2();
                                    criteria.Add(Expression.Eq(CatalogItem.ProductIdProperty, productID));
                                    criteria.Add(Expression.Eq(CatalogItem.CatalogIdProperty, catalogID));
                                    List<CatalogItem> catalogItemList = CatalogItem.GetCatalogItemList(criteria);
                                    if (catalogItemList != null && catalogItemList.Count > 0)
                                    {
                                        catalogItemID = catalogItemList[0].CatalogItemId;

                                        //get catalog item detail
                                        LogSimple.LogInfo("Sale :" + saleId + " Get catalog item detail");
                                        criteria = CatalogItemDetail.CreateCriteria2();
                                        criteria.Add(Expression.Eq(CatalogItem.CatalogItemIdProperty, catalogItemID));
                                        List<CatalogItemDetail> catalogItemDetailList = CatalogItemDetail.GetCatalogItemDetailList(criteria);
                                        if (catalogItemDetailList != null && catalogItemDetailList.Count > 0)
                                        {
                                            catalogItemDetailID = catalogItemDetailList[0].CatalogItemDetailId;

                                            //update hashtable linking catalogItemDetailID to ProductCode
                                            productCodeLink.Add(productCode, catalogItemDetailID);

                                            //get catalogItemCategoryID (gonna be used for commission rates)
                                            LogSimple.LogInfo("Sale :" + saleId + " Get catalog item category catalog item");
                                            criteria = CatalogItemCategoryCatalogItem.CreateCriteria2();
                                            criteria.Add(Expression.Eq(CatalogItemCategoryCatalogItem.CatalogItemIdProperty, catalogItemID));
                                            List<CatalogItemCategoryCatalogItem> catalogItemCategoryCatalogItemList = CatalogItemCategoryCatalogItem.GetCatalogItemCategoryCatalogItemList(criteria);
                                            if (catalogItemCategoryCatalogItemList != null && catalogItemCategoryCatalogItemList.Count > 0)
                                            {
                                                catalogItemCategoryID = catalogItemCategoryCatalogItemList[0].CatalogItemCategoryId;
                                            }

                                            //get category name
                                            LogSimple.LogInfo("Sale :" + saleId + " Get catalog item catagory");
                                            criteria = CatalogItemCategory.CreateCriteria2();
                                            criteria.Add(Expression.Eq(CatalogItemCategoryCatalogItem.CatalogItemCategoryIdProperty, catalogItemCategoryID));
                                            List<CatalogItemCategory> catalogItemCategoryList = CatalogItemCategory.GetCatalogItemCategoryList(criteria);
                                            if (catalogItemCategoryList != null && catalogItemCategoryList.Count > 0)
                                            {
                                                catalogItemCategoryName = catalogItemCategoryList[0].CatalogItemCategoryName;
                                            }
                                        }
                                    }


                                    //ALL WFC  DONT GO INTO OE

                                    if (catalogItemCategoryName == "Stock WFC" || catalogItemCategoryName == "Personalized WFC" ||
                                        catalogItemCategoryName == "WFC CA")           // || productCode == "750 NC" || productCode == "825 NC")
                                    {
                                        return "No OE - WFC";
                                    }

                                    //Release = 401
                                    //Process = 301
                                    //wait appr = 5
                                    // orderStatusID = 5;

                                    orderStatusID = PendingApprovalID;
                                    LogSimple.LogInfo("Sale :" + saleId + " Setting Order Status");
                                    //
                                    //
                                    //
                                    //
                                    //
                                    //
                                    //
                                    //
                                    //
                                    //
                                    //
                                    //IF ITS CONFIRMED teh staus change for as400 to pick upsale
                                    if (saleStatus == "Confirmed")
                                    {

                                        if (catalogItemCategoryName == "Personalized WFC")
                                        {
                                            orderStatusID = IncompletePEID;
                                        }
                                        else
                                        {
                                            orderStatusID = InProcessID;
                                        }
                                        ///////////////////////////////
                                    }
                                    else if (saleStatus == "Cancelled")
                                    {
                                        if (isProcessed)
                                        {
                                            orderStatusID = CancelledID;
                                        }
                                        else
                                        {
                                            orderStatusID = CancelledOEOnlyID;
                                        }
                                        
                                    }
                                    //
                                    ////
                                    //

                                    //
                                    //
                                    //
                                    //
                                    //
                                    //

                                    //set program type
                                    if (catalogItemCategoryName == "Personalized WFC" || catalogItemCategoryName == "Stock WFC" || catalogItemCategoryName == "WFC CA")
                                    {
                                        programTypeID = programTypeIDWFC;
                                    }


                                    decimal saleAmt = Convert.ToDecimal(sale.TotalAmount);

                                    if (catalogItemDetailID != 0)
                                    {

                                        ScratchBook sb = ScratchBook.GetScratchBookByID(item.ScratchBookId);
                                        decimal profitRate = 0;
                                        if (sb.RaisingPotential > 0)
                                        {
                                            LogSimple.LogInfo("Sale :" + saleId + " Calculating Profit Rate");
                                            profitRate = (Convert.ToDecimal(sb.RaisingPotential) - item.UnitPriceSold) / Convert.ToDecimal(sb.RaisingPotential);
                                        }
                                        else
                                        {
                                            //IF NO RAISING POTENTIAL CANNOT BE PROCESSED BY OE
                                            //exception for van wyk cards
                                         /*   pos = sb.Description.ToUpper().IndexOf("CARD");
                                            if (!sb.Description.StartsWith("Sample") && pos == -1)
                                            {
                                                //if no sample in the name, treated as a sample
                                                return "No OE - Sample";
                                            }
                                            */

                                        }

                                        productFound++;
                                        OrderDetailData orderDetailData = new OrderDetailData();
                                        orderDetailData.OrderDetail = new OrderDetail();
                                        orderDetailData.OrderDetailTaxList = new List<OrderDetailTax>();
                                        orderDetailData.OrderDetail.CatalogItemDetailId = catalogItemDetailID;
                                        

                                        orderDetailData.OrderDetail.Quantity = qty;
                                        //if its a sample, the qty becomes a qty free
                                        if (itemName.IndexOf("Sample") > -1)
                                        {
                                            //GET NON SAMPLE COUNTERPART
                                            sb = efundraising.EFundraisingCRM.ScratchBook.GetScratchBookNonPromoByProductCode(sb.ProductCode);
                                            orderDetailData.OrderDetail.AdjustmentQuantity = -(qty);
                                            orderDetailData.OrderDetail.Price = retailPrice;
                                        }
                                        else
                                        {
                                            orderDetailData.OrderDetail.AdjustmentQuantity = -(item.QuantityFree);
                                            orderDetailData.OrderDetail.Price = item.UnitPriceSold;
                                        }

                                        orderDetailData.OrderDetail.OrderStatusId = orderStatusID;
                                        orderDetailData.OrderDetail.SourceId = sourceID;
                                        orderDetailData.OrderDetail.CreateUserId = userID;
                                        orderDetailData.OrderDetail.UpdateUserId = userID;



                                        //get commission rate 
                                        LogSimple.LogInfo("Sale :" + saleId + " Getting commission Rate");



                                        //not used for now, need to calculate rules on efundprod, the rate on OE is static
                                        // decimal rate = QSPForm.Business.Controller.OrderController.GetCommissionRate(catalogItemID, catalogItemCategoryID, profitRate, billingAddress.CountryCode, isPrepaid);
                                        // if (rate != -1)
                                        // {
                                        List<QSP.Business.Fulfillment.Commission> commissions = new List<QSP.Business.Fulfillment.Commission>();

                                        QSP.Business.Fulfillment.Commission commission = new QSP.Business.Fulfillment.Commission();
                                        commission.CommissionRate = commissionRate;
                                        commission.CommissionTypeId = 1;
                                        commission.CreateDate = DateTime.Now;
                                        commission.CreateUserId = userID;
                                        commission.FmId = fmID.ToString();

                                        commissions.Add(commission);
                                        orderDetailData.CommissionList = commissions;
                                        /*  }
                                          else
                                          {
                                              commissionFound = false;
                                          }*/

                                        //set taxes

                                        //check what is the product class, if its tax exempt
                                        efundraising.EFundraisingCRM.ProductClass pc = efundraising.EFundraisingCRM.ProductClass.GetProductClassById(item.ProductClassId);
                                        
                                        bool isTaxExempt = pc.TaxExempt;
                                        
                                        LogSimple.LogInfo("Sale :" + saleId + " Setting Taxes");
                                        OrderDetailTax GST = new OrderDetailTax();
                                        OrderDetailTax PST = new OrderDetailTax();
                                                             
                                        GST.TaxTypeId = 2; //federal
                                        GST.TaxRate = Convert.ToDouble(GSTrate);
                                        PST.TaxTypeId = 3; //prov
                                        PST.TaxRate = Convert.ToDouble(PSTrate);
                                                                  

                                        if (isTaxExempt)
                                        {
                                            debug = 10;
                                            GST.TaxAmount = 0;
                                            PST.TaxAmount = 0;
                                        }
                                        else
                                        {
                                            
                                            GST.TaxAmount = GSTrate * item.SalesAmount;
                                            PST.TaxAmount = PSTrate * (item.SalesAmount + GST.TaxAmount);
                                            //The first item will carry the taxes on adjustment
                                            if (productIndex == 0)
                                            {                                                
                                                decimal adj = discount - surcharge;
                                                decimal gstOnAdj = GSTrate * adj;
                                                decimal pstOnAdj = PSTrate * (adj + gstOnAdj);
                                           
                                                GST.TaxAmount += gstOnAdj;
                                                PST.TaxAmount += pstOnAdj;                                           
                                                
                                            }

                                         

                                        }
                                        GST.CreateUserId = userID;
                                        PST.CreateUserId = userID;
                                        GST.UpdateUserId = userID;
                                        PST.UpdateUserId = userID;

                                        debug = 12;

                                        //The first item will carry the taxes on shipping
                                        if (productIndex == 0)
                                        {
                                            //OrderDetailTax GSTShip = new OrderDetailTax();
                                            //GSTShip.TaxTypeId = 4; //fed
                                            decimal gstonShip = GSTrate* shippingFee;
                                            GST.TaxAmount += gstonShip;
                                            PST.TaxAmount += PSTrate *  (gstonShip  + shippingFee);

                                           /* OrderDetailTax PSTShip = new OrderDetailTax();
                                            PSTShip.TaxTypeId = 5; //prov
                                            PSTShip.TaxRate = Convert.ToDouble(PSTrate);
                                            PSTShip.TaxAmount = PSTrate * (GSTShip.TaxAmount + shippingFee);
                                            PSTShip.CreateUserId = userID;

                                            if (PSTShip.TaxAmount > 0)
                                            {
                                                orderDetailData.OrderDetailTaxList.Add(PSTShip);
                                            }
                                            if (GSTShip.TaxAmount > 0)
                                            {
                                                orderDetailData.OrderDetailTaxList.Add(GSTShip);
                                            }*/

                                        }

                                        if (PST.TaxAmount > 0)
                                        {
                                            orderDetailData.OrderDetailTaxList.Add(PST);
                                        }
                                        if (GST.TaxAmount > 0)
                                        {
                                            orderDetailData.OrderDetailTaxList.Add(GST);
                                        }

                                        LogSimple.LogInfo("Sale :" + saleId + " Adding orderDetailData to orderList");
                                        orderDetailDataList.Add(orderDetailData);


                                    }//if catalog item detail id > 0
                                    else 
                                    {
                                        missingItems = missingItems + productCode;
                                    }
                                }//if product list > 0
                                else //if qty = 0
                                {
                                    missingItems = missingItems + productCode;
                                }
                            }
                            else
                            {
                                productTBD++;
                            }
                        }//if item exists
                    } //for each item
                                         

                    //IF Samples with existing counterpart were found, adjust the qty
                    foreach (string key in samplesDuplicateFound.Keys)
                    {
                        int qty = Convert.ToInt32(samplesDuplicateFound[key]);
                        //get catalogItemID
                        int catalogItemDetailID = Convert.ToInt32(productCodeLink[key]);
                        foreach (OrderDetailData odd in orderDetailDataList)
                        {
                            if (odd.OrderDetail.CatalogItemDetailId == catalogItemDetailID)
                            {
                                odd.OrderDetail.Quantity = odd.OrderDetail.Quantity + qty;
                                odd.OrderDetail.AdjustmentQuantity = -(qty);
                            }
                        }


                    }

                    int campaignID = 0;
                    string detailError = "";  
                    //create/edit account
                    if (isProcessed)
                    {
                        //still need to get the accountID
                        accountID = GetAccountID(qspOrderID);
                        campaignID = GetCampaignID(accountID);
                    }
                    else
                    {
                        QSP.Business.Fulfillment.Account account = new QSP.Business.Fulfillment.Account();
                        if (edit)
                        {
                            LogSimple.LogInfo("Sale :" + saleId + " Getting Account ID");
                            account.AccountId = GetAccountID(qspOrderID);
                            LogSimple.LogInfo("Account Found :" + account.AccountId);
                            //update through linq if sale as an account no
                            efundraising.EFundraisingCRM.Linq.QSPFulfillmentDataContext oe = new efundraising.EFundraisingCRM.Linq.QSPFulfillmentDataContext(qspConnStr);
                            LogSimple.LogInfo("Connection: :" + qspConnStr);
                            efundraising.EFundraisingCRM.Linq.account acct = oe.accounts.SingleOrDefault(a => a.account_id == account.AccountId);
                            if (acct != null)
                            {
                                acct.fm_id = fmID.ToString();
                                acct.account_status_id = 101; //set to 101 if it war an error code
                                //if the code was 301 (processed), settting to 101 will not do anything
                                oe.SubmitChanges();
                            }

                        }



                        if (client.Organization.Length > 50)
                        {
                            account.AccountName = client.Organization.Substring(0, 50); //Max length of ora name is 50
                        }
                        else
                        {
                            account.AccountName = client.Organization;
                        }
                        account.AccountStatusId = 101; //in process
                        account.AccountTypeId = 1; //standard
                        account.CreateUserId = userID;
                        account.CreditLimit = 0;
                        account.FmId = fmID.ToString();

                        AccountData accountData = new AccountData();
                        accountData.Account = account;
                        accountData.BillingPhoneNumber = phoneNumber.Phone_Number;
                        accountData.ShippingPhoneNumber = phoneNumber.Phone_Number;
                        accountData.OrganizationLevelId = oeOrganizationLevelID;
                        accountData.OrganizationTypeId = oeOrganizationTypeID;


                        LogSimple.LogInfo("Sale :" + saleId + " Saving account");

                        QSPForm.Business.Controller.AccountController.SaveAccount(accountData, notification, client.ClientId, client.ClientSequenceCode, programTypeID);


                        if (notification.IsSuccessful())
                        {
                            LogSimple.LogInfo("Sale :" + saleId + " Getting existing account");
                            accountID = Convert.ToInt32(notification[0].DynamicValues[0]);
                        }
                        else
                        {
                            LogSimple.LogInfo("Sale :" + saleId + " Error saving account");
                            error = "Error Saving Account";
                            detailError = notification[notification.Count - 1].NotificationMessage.ToString();
                            if (notification[notification.Count - 1].DynamicValues != null)
                            {
                                detailError = detailError + "  " + notification[notification.Count - 1].DynamicValues[0].ToString();
                                if (notification[notification.Count - 1].DynamicValues.Count > 1)
                                {
                                    detailError = detailError + "  " + notification[notification.Count - 1].DynamicValues[1].ToString();
                                }
                            }
                            return error + "  --" + detailError;
                        }

                    }//if isProcessed

        
                        LogSimple.LogInfo("Sale :" + saleId + " Check if products and commission were found");
                        
                        //DEV
                        //if (productFound == nbProducts - productTBD - samplesDuplicateFound.Count && orderDetailDataList.Count > 0)
                        if (productFound == nbProducts - productTBD - samplesDuplicateFound.Count && orderDetailDataList.Count > 0 && commissionFound)
                        {
                            if (sAddress.SubdivisionCode != "" && bAddress.SubdivisionCode != "")
                            {

                                //Place order
                                int custID = Convert.ToInt32(hWebConfig["custID"]);
                                int orderTypeID = Convert.ToInt32(hWebConfig["orderTypeID"]);

                                if (!isProcessed)
                                {
                                    //CREATE CAMPAIGN
                                    Campaign campaign = new Campaign();
                                    if (edit)
                                    {
                                        LogSimple.LogInfo("Sale :" + saleId + " Get existing campaign ID with account:" + accountID);
                                        campaign.CampaignId = GetCampaignID(accountID);
                                        LogSimple.LogInfo("Sale :" + saleId + " Get existing campaign ID " + campaign.CampaignId);
                                    }
                                    campaign.AccountId = accountID;
                                    campaign.ARORBL = null;
                                    campaign.CampaignName = client.Organization;
                                    campaign.Comments = "";
                                    campaign.Deleted = false;
                                    campaign.EndDate = endDate;
                                    campaign.Enrollment = 0;
                                    campaign.FmId = fmID.ToString();    //sale consultant
                                    campaign.FormId = null;
                                    campaign.GoalEstimatedGross = 0;
                                    campaign.ProgramTypeId = programTypeID;            // This means EFR in the program_type table
                                    campaign.StartDate = startDate;
                                    campaign.TaxExemptionExpirationDate = null;
                                    campaign.TaxExemptionNumber = null;
                                    campaign.TradeClassId = null;
                                    campaign.WarehouseId = null;
                                    campaign.CreateUserId = userID;
                                    campaign.FiscalYear = fiscalYear;
                                    CampaignData campaignData = new CampaignData();
                                    campaignData.Campaign = campaign;
                                    campaignData.BillingPhoneNumber = phoneNumber.Phone_Number;
                                    campaignData.ShippingPhoneNumber = phoneNumber.Phone_Number;



                                    LogSimple.LogInfo("Sale :" + saleId + " Saving campaign");
                                    QSPForm.Business.Controller.CampaignController.SaveCampaign(campaignData, notification);
                                    campaignID = campaign.CampaignId;
                                }

                            
                     
                       
                        /////////////////////
                            QSP.Business.Fulfillment.Order order = new QSP.Business.Fulfillment.Order();
                            if (edit)
                            {
                                LogSimple.LogInfo("Sale :" + saleId + " Get existing order ID");
                                order.OrderId = qspOrderID;
                            }

                            //if isProcessed, only allow cancellation
                            if (!isProcessed || saleStatus == "Cancelled")
                            {
                                order.OrderStatusId = orderStatusID;
                            }

                            //the comment is the delivery instruction

                            string time = sale.ScheduledDeliveryDate.ToString();
                            pos = time.IndexOf(" ");
                            time = time.Substring(pos, time.Length - pos);
                            if (time == " 12:00:00 AM")
                            {
                                order.Comments = "";
                            }
                            else
                            {
                                order.Comments = "Requested delivery time: " + time;  
                            }
                            
                            order.CreateUserId = userID ;
                            order.OrderTypeId = orderTypeID;
                            order.OrderDate = DateTime.Now;
                            order.FormId = formID;
                            order.Deleted = false;
                            order.CustomerId = custID;
                            order.CreateDate = DateTime.Now;
                            order.FmId = fmID.ToString();
                            order.CampaignId = campaignID;
                            order.SourceId = sourceID;
                            order.UpdateUserId = userID;
                                orderData.Order = order;
                                orderData.OrderDetailDataList = orderDetailDataList;
                                orderData.ShipmentGroup = shipmentGroup;
                                orderData.PostalAddressBilling = bAddress;
                                orderData.PostalAddressShipping = sAddress;
                                orderData.PhoneNumber = phoneNumber;
                                
                            
                            LogSimple.LogInfo("Sale :" + saleId + " Saving Order. Bill address:(" + orderData.PostalAddressBilling.Address1.ToString() + "   Ship Address:(" + orderData.ShipmentGroup.ShippingPostalAddressId.ToString() + ")" +  orderData.PostalAddressShipping.Address1.ToString() + "   Time:" + DateTime.Now);   
                            QSPForm.Business.Controller.OrderController.SaveOrder(orderData, notification);

                       
               

                            if (notification.IsSuccessful())
                            {
                                LogSimple.LogInfo("Sale :" + saleId + " Saving Sale Successful");
                                int orderID = order.OrderId;

                                if (orderID > 0)
                                {

                                    //create-edit charge
                                    //*********************
                                    //get all charges
                                    List<QSP.Business.Fulfillment.OrderCharge> orderCharges = QSP.Business.Fulfillment.OrderCharge.GetOrderChargeListFromOrder(qspOrderID);

                                    bool discountUpdated = false;
                                    bool surchargeUpdated = false;
                                    //check if one charge is a discount (can obnly have one of each)
                                    foreach (QSP.Business.Fulfillment.OrderCharge orderCharge in orderCharges)
                                    {
                                        int chargeID = orderCharge.ChargeId;
                                        QSP.Business.Fulfillment.Charge charge = Charge.GetCharge(chargeID);
                                        if (charge.IsCredit)//UPDATE ORDER CHARGE with discount
                                        {
                                            discountUpdated = true;
                                            orderCharge.ChargeId = discountReasonID;
                                            orderCharge.Amount = discount;
                                        }
                                        else //UPDATE order charge with surcharge
                                        {
                                            surchargeUpdated = true;
                                            orderCharge.ChargeId = surchargeReasonID;
                                            orderCharge.Amount = surcharge;
                                        }

                                        orderCharge.UpdateDate = DateTime.Now;
                                        orderCharge.UpdateUserId = userID;
                                        orderCharge.Update();
                                    }

                                    //Insert discount and surcharge  + TAX
                                    if (discount < 0 && !discountUpdated)
                                    {
                                        OrderCharge orderCharge = new OrderCharge();
                                        orderCharge.OrderId = order.OrderId;
                                        orderCharge.AccountId = accountID;
                                        orderCharge.ChargeToId = chargeTo;
                                        orderCharge.Amount = discount;
                                        orderCharge.ChargeId = discountReasonID;
                                        orderCharge.CreateDate = DateTime.Now;
                                        orderCharge.CreateUserId = userID;
                                        orderCharge.Insert();
                                    }

                                    if (surcharge > 0 && !surchargeUpdated)
                                    {
                                        OrderCharge orderCharge = new OrderCharge();
                                        orderCharge.OrderId = order.OrderId;
                                        orderCharge.AccountId = accountID;
                                        orderCharge.ChargeToId = chargeTo;
                                        orderCharge.Amount = surcharge;
                                        orderCharge.ChargeId = surchargeReasonID;
                                        orderCharge.CreateDate = DateTime.Now;
                                        orderCharge.CreateUserId = userID;
                                        orderCharge.Insert();
                                    }

                                    ///**********************
                                    ///
                    




                                    //BEFORE VALIDATING DOUBLE CHECK THAT THE SALE WAS CONFIRMED IN OE
                                    if (saleStatus == "Confirmed")
                                    {
                                        criteria = QSP.Business.Fulfillment.Order.CreateCriteria2();
                                        criteria.Add(Expression.Eq(QSP.Business.Fulfillment.Order.OrderIdProperty, orderID));
                                        List<QSP.Business.Fulfillment.Order> orderList = QSP.Business.Fulfillment.Order.GetOrderList(criteria);
                                        if (orderList != null && orderList.Count > 0)
                                        {
                                            int status = orderList[0].OrderStatusId;
                                            if (status != 5)//not wait approval
                                            {
                                                sale.IsValidated = 1;
                                            }
                                            else
                                            {
                                                //try again
                                                QSPForm.Business.Controller.OrderController.SaveOrder(orderData, notification);

                                                LogSimple.LogInfo("Sale :" + saleId + " Error Confirmed Status was Not Updated");
                                                efundraising.Diagnostics.Logger.LogError("Error UPDATING CONFIRMED STATUS." + " Sale:" + sale.SalesId + "  orderStatusID in orderData object is: " + orderData.Order.OrderStatusId.ToString());

                                            }
                                        }


                                    }

                                    //EFUND UPDATE
                                   // sale.ExtOrderID = Convert.ToInt32(orderID);
                                    sale.Comment = "Order Express Order-ID = " + Convert.ToString(orderID);
                                    sale.ProductionStatusId = 13;//in process
                                    LogSimple.LogInfo("Sale :" + saleId + " Update efr sale with oe id");
                                    int j = sale.Update();
                                    if (j < 1)
                                    {
                                        string msg = "Error updating WFC order ID. Sale:" + sale.SalesId;
                                        efundraising.Diagnostics.Logger.LogError(msg);
                                    }
                                }
                                else
                                {
                                    LogSimple.LogInfo("Sale :" + saleId + " Error getting order id");
                                    error = error + " -- Error getting order ID. Sale:" + sale.SalesId;
                                }

                            }
                            else
                            {
                                //error pushing to QSP
                                LogSimple.LogInfo("Sale :" + saleId + " Error placing order");
                                error = "Error placing order. Sale:" + sale.SalesId;
                                detailError = notification[notification.Count - 1].NotificationMessage.ToString();
                                if (notification[notification.Count - 1].DynamicValues != null)
                                {
                                    detailError = detailError + "  " + notification[notification.Count - 1].DynamicValues[0].ToString();
                                    if (notification[notification.Count - 1].DynamicValues.Count > 1)
                                    {
                                        detailError = detailError + "====================================>  " + notification[notification.Count - 1].DynamicValues[1].ToString();
                                    }
                                }


                            }


                        }
                        else
                        {
                            //error w/ state code
                            LogSimple.LogInfo("Sale :" + saleId + " Error with state code");
                            error = "Error with state code." + shippingAddress.StateCode + "/" + billingAddress.StateCode + " Sale:" + sale.SalesId;

                        }   //if no subdiviion code


                        if (error != "")
                        {
                            //QSP SALE
                            if (edit)
                            {
                                orderData.Order.OrderId = qspOrderID;
                                orderData.Order.OrderId = OnHoldID;
                                LogSimple.LogInfo("Sale :" + saleId + " Save order with onHold satus");
                                QSPForm.Business.Controller.OrderController.SaveOrder(orderData, notification);

                            }


                            //EFUND SALE
                            sale.ProductionStatusId = 17;//on hold
                            sale.Comment = sale.Comment + " -" + error;

                            int j = sale.Update();
                            if (j < 1)
                            {
                                error = error + " Error updating WFC order ID. Sale:" + sale.SalesId;
                            }

                            efundraising.Diagnostics.Logger.LogError(error + " " + detailError);

                        }
                    }
                    else
                    {
                        //no product found or commission
                        LogSimple.LogInfo("Sale :" + saleId + " Error finding product or commission");
                        if (programTypeID != 7){
                           error = "Sale " + sale.SalesId + " could not be processed. Product Not Found (" + missingItems + ")" ;
                        
                           sale.ProductionStatusId = 17;//on hold
                           sale.Comment = sale.Comment + " -" + error;

                           int j = sale.Update();
                        }
                    }

                }
                else //if error found
                {
                    sale.ProductionStatusId = 17;//on hold
                    sale.Comment = sale.Comment + " -" + error;

                    int j = sale.Update();
                }
  
                
                
			}
			catch(Exception x)
			{
                if (x.Message.Length > 50)
                {
                    error = "Sale " + sale.SalesId + ". Error placing order! (" + x.Message.Substring(0, 50) + ") line:" + debug.ToString();
                }
                else
                {
                    error = "Sale " + sale.SalesId + ". Error placing order! (" + x.Message + ")";
                }
				efundraising.Diagnostics.Logger.LogError(error, x);

				sale.ProductionStatusId = 17;//on hold
                if (x.Message.Length > 50)
                {
                    sale.Comment = sale.Comment;// +" -" + error + " (" + x.Message.Substring(0, 50);
                }
                else
                {
                    sale.Comment = sale.Comment;// +" -" + error + " (" + x.Message;
                }
				

				int j = sale.Update();
			}
            			
           return error;
		
		}

       

		private string GetSubDivisionCode(string stateCode)
		{
			string state = "";
			try
			{

                if (stateCode == "ALB")
                {
                    state = "CA-AB";
                }
                else if (stateCode == "ONT" || stateCode == "OT")
                {
                    state = "CA-ON";
                }
                else if (stateCode == "QU" || stateCode == "QUE")
                {
                    state = "CA-QC";
                }
                else if (stateCode == "NF" || stateCode == "LB")
                {
                    state = "CA-NL";
                }
                else if (stateCode == "SA")
                {
                    state = "CA-SK";
                }

                else if (stateCode == "MAN")
                {
                    state = "CA-MB";
                }
                
                else
                {

                    efundraising.EFundraisingCRM.Subdivision sd = efundraising.EFundraisingCRM.Subdivision.GetSubdivisionByCode(stateCode);
                    //check if perfect match

                    if (sd != null)
                    {
                        state = stateCode;
                    }
                    //check if match with US in front
                    else
                    {
                        sd = efundraising.EFundraisingCRM.Subdivision.GetSubdivisionByCode("US-" + stateCode);
                        if (sd != null)
                        {
                            state = "US-" + stateCode;
                        }
                        else
                        {
                            //try with canada
                            sd = efundraising.EFundraisingCRM.Subdivision.GetSubdivisionByCode("CA-" + stateCode);
                            if (sd != null)
                            {
                                state = "CA-" + stateCode;
                            }

                        }

                    }
                }
			}
			catch(Exception x)
			{
				string error = "Error in GetSubdivision.";
				efundraising.Diagnostics.Logger.LogError(error, x);
				return "";
			}
			
			return state;
        
		}

		public void test()
		{
			OrderExpressClient.com.qsp.ws.EfundraisingServices oe = new OrderExpressClient.com.qsp.ws.EfundraisingServices();	
    		OrderExpressClient.com.qsp.ws.ProgramType[] programTypes = oe.GetProgramType();
		}

        private void setQSPOrderStatusIDs()
        {
            ICriteria criteria = OrderStatus.CreateCriteria2();
            criteria.Add(Expression.Eq(OrderStatus.OrderStatusNameProperty, "Incomplete PE"));
            List<OrderStatus> orderStatusList = OrderStatus.GetOrderStatusList(criteria);
            if (orderStatusList != null && orderStatusList.Count > 0)
            {
                IncompletePEID = orderStatusList[0].OrderStatusId;
            }
            criteria = OrderStatus.CreateCriteria2();
            criteria.Add(Expression.Eq(OrderStatus.OrderStatusNameProperty, "In Process"));
            orderStatusList = OrderStatus.GetOrderStatusList(criteria);
            if (orderStatusList != null && orderStatusList.Count > 0)
            {
                InProcessID = orderStatusList[0].OrderStatusId;
            }
            criteria = OrderStatus.CreateCriteria2();
            criteria.Add(Expression.Eq(OrderStatus.OrderStatusNameProperty, "Wait Appr."));
            orderStatusList = OrderStatus.GetOrderStatusList(criteria);
            if (orderStatusList != null && orderStatusList.Count > 0)
            {
                PendingApprovalID = orderStatusList[0].OrderStatusId;
            }
            criteria = OrderStatus.CreateCriteria2();
            criteria.Add(Expression.Eq(OrderStatus.OrderStatusNameProperty, "Hold"));
            orderStatusList = OrderStatus.GetOrderStatusList(criteria);
            if (orderStatusList != null && orderStatusList.Count > 0)
            {
                OnHoldID = orderStatusList[0].OrderStatusId;
            }

            criteria = OrderStatus.CreateCriteria2();
            criteria.Add(Expression.Eq(OrderStatus.OrderStatusNameProperty, "In process to be cancelled"));
            orderStatusList = OrderStatus.GetOrderStatusList(criteria);
            if (orderStatusList != null && orderStatusList.Count > 0)
            {
                CancelledID = orderStatusList[0].OrderStatusId;
            }
        }

        public int GetAccountID(int qspOrderID)
        {
            int campaignID = 0;
            int accountID = 0;

            ICriteria criteria = QSP.Business.Fulfillment.Order.CreateCriteria2();
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.Order.OrderIdProperty, qspOrderID));
            List<QSP.Business.Fulfillment.Order> orderList = QSP.Business.Fulfillment.Order.GetOrderList(criteria);
            if (orderList != null && orderList.Count > 0)
            {
                campaignID = orderList[0].CampaignId;
            }

            criteria = QSP.Business.Fulfillment.Campaign.CreateCriteria2();
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.Campaign.CampaignIdProperty, campaignID));
            List<QSP.Business.Fulfillment.Campaign> campaignList = QSP.Business.Fulfillment.Campaign.GetCampaignList(criteria);
            if (campaignList != null && campaignList.Count > 0)
            {
                accountID = campaignList[0].AccountId;
            }

            return accountID;
                  

        }


        public string GetAccountStatus(int qspOrderID, int accountID)
        {
            int? accountStatusID = 0;
            string accountStatus = "";
                       
    
            ICriteria criteria = QSP.Business.Fulfillment.Account.CreateCriteria2();
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.Account.AccountIdProperty, accountID));
            List<QSP.Business.Fulfillment.Account> accountList = QSP.Business.Fulfillment.Account.GetAccountList(criteria);
            if (accountList != null && accountList.Count > 0)
            {
                accountStatusID = accountList[0].AccountStatusId;
            }

            criteria = AccountStatus.CreateCriteria2();
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.AccountStatus.AccountStatusIdProperty, accountStatusID));
            List<QSP.Business.Fulfillment.AccountStatus> accountStatusList = QSP.Business.Fulfillment.AccountStatus.GetAccountStatusList(criteria);
            if (accountStatusList != null && accountStatusList.Count > 0)
            {
                accountStatus = accountStatusList[0].AccountStatusName;
            }

            return accountStatus;


        }

        private int GetShippingPostalAddressID(int qspOrderID)
        {
            
            int shippingPostalAddressID = 0;

            int shipmentGroupID = GetShipmentGroupID(qspOrderID);
  
            ICriteria criteria = QSP.Business.Fulfillment.ShipmentGroup.CreateCriteria2();
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.ShipmentGroup.ShipmentGroupIdProperty, shipmentGroupID));
            List<QSP.Business.Fulfillment.ShipmentGroup> shipmentGroupList = QSP.Business.Fulfillment.ShipmentGroup.GetShipmentGroupList(criteria);
            if (shipmentGroupList != null && shipmentGroupList.Count > 0)
            {
                 shippingPostalAddressID = Convert.ToInt32(shipmentGroupList[0].ShippingPostalAddressId);
               

            }

            return shippingPostalAddressID;


        }

        private int GetShipmentGroupID(int qspOrderID)
        {
            int shipmentGroupID = 0;

            ICriteria criteria = QSP.Business.Fulfillment.OrderDetail.CreateCriteria2();
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.OrderDetail.OrderIdProperty, qspOrderID));
            List<QSP.Business.Fulfillment.OrderDetail> orderDetailList = QSP.Business.Fulfillment.OrderDetail.GetOrderDetailList(criteria);
            if (orderDetailList != null && orderDetailList.Count > 0)
            {
                shipmentGroupID = orderDetailList[0].ShipmentGroupId;
            }

            return shipmentGroupID;
        }

        private int? GetBillingPostalAddressID(int qspOrderID)
        {
            int? billingPostalAddressID = null;
           
            ICriteria criteria = QSP.Business.Fulfillment.Order.CreateCriteria2();
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.Order.OrderIdProperty, qspOrderID));
            List<QSP.Business.Fulfillment.Order> orderList = QSP.Business.Fulfillment.Order.GetOrderList(criteria);
            if (orderList != null && orderList.Count > 0)
            {
                billingPostalAddressID = orderList[0].BillingPostalAddressId;
            }

            return billingPostalAddressID;


        }

        private int GetCampaignID(int accountID)
        {
            int campaignID = 0;

            ICriteria criteria = QSP.Business.Fulfillment.Campaign.CreateCriteria2();
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.Campaign.AccountIdProperty, accountID));
            List<QSP.Business.Fulfillment.Campaign> campaignList = QSP.Business.Fulfillment.Campaign.GetCampaignList(criteria);
            if (campaignList != null && campaignList.Count > 0)
            {
                LogSimple.LogInfo("Fetched 1 row, campaign: " + campaignList[0].CampaignId.ToString());
                campaignID = campaignList[0].CampaignId;
            }
            return campaignID;
        
        }

        private int GetPhoneNumberID(int qspOrderID)
        {
            int phoneNumberID = 0;
           
            int shipmentGroupID = GetShipmentGroupID(qspOrderID);

            ICriteria criteria = QSP.Business.Fulfillment.ShipmentGroup.CreateCriteria2();
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.ShipmentGroup.ShipmentGroupIdProperty, shipmentGroupID));
            List<QSP.Business.Fulfillment.ShipmentGroup> shipmentGroupList = QSP.Business.Fulfillment.ShipmentGroup.GetShipmentGroupList(criteria);
            if (shipmentGroupList != null && shipmentGroupList.Count > 0)
            {                    
                if (shipmentGroupList[0].ShippingPhoneNumberId == null)
                {
                    phoneNumberID = 0;
                }
                else
                {
                    phoneNumberID = Convert.ToInt32(shipmentGroupList[0].ShippingPhoneNumberId);
                }
            }
                
            

            return phoneNumberID;


        }


        private void GetNameFromID(int consultantID, ref string firstName, ref string lastName, bool Fromlead)
        {

            if (consultantID == 3481) //lead to be qualified
            {
                firstName = "efr";
                lastName = "fundraising";
            }
            else
            {
                //get the real FM
                efundraising.EFundraisingCRM.Consultant consultant = efundraising.EFundraisingCRM.Consultant.GetConsultantByID(consultantID);
                string name = consultant.Name;
                int pos = name.LastIndexOf(" ");
                if (pos > 0)
                {
                    firstName = name.Substring(0, pos);
                    lastName = name.Substring(pos + 1);
                }
                else
                {
                    if (Fromlead) //lead info is not good, put pack efr fundraising
                    {
                        firstName = "efr";
                        lastName = "fundraising";
                    }
                    else
                    {
                        firstName = name;
                        lastName = "";
                    }
                }
            }
        }


        private bool GetFMID(ref int fmID,  int consultantId, int leadID, ref string error, int saleId)
        {           

           bool errorFound = false;
         //   LogSimple.LogInfo("Sale :" + saleId + " Getting FM (cid=" + sale.ConsultantId + ")");
          /*  efundraising.EFundraisingCRM.Consultant consultant = efundraising.EFundraisingCRM.Consultant.GetConsultantByID(consultantId);
            string name = consultant.Name;
            string firstName = name;
            string lastName = "";
            int pos = name.LastIndexOf(" ");
            if (pos > 0)
            {
                firstName = name.Substring(0, pos);
                lastName = name.Substring(pos + 1);
            }*/


        
            //if website fm id, use the fm from lead id it exists and put source id
           //LogSimple.LogInfo("Sale :" + saleId + " Setting Source");

           int sourceID;
           string firstName = "";
           string lastName = "";


            ICriteria criteria = QSP.Business.Fulfillment.Source.CreateCriteria2();
            List<QSP.Business.Fulfillment.Source> sources = null;
            bool useLeadFM = false;
            if (consultantId == 3518 || consultantId == 3450)
            {
                //check if theres a lead consultant
                efundraising.EFundraisingCRM.Lead lead = efundraising.EFundraisingCRM.Lead.GetLeadByID(leadID);
   
                //EFUND
                if (consultantId == 3518)
                {
                    if (lead.ConsultantId == 3518)
                    {
                        firstName = "efr";
                        lastName = "fundraising";
                    }else
                    {
                        useLeadFM = true;
                    }
                }
                else if (consultantId == 3450) //FR
                {
                    if (lead.ConsultantId == 3450)  //if FR, put source id accordingly
                    {
                        firstName = "efr";
                        lastName = "fundraising";
                    }else{
                        useLeadFM = true;
                    }
                }
         
                if (useLeadFM)
                {
                    GetNameFromID(lead.ConsultantId, ref firstName, ref lastName,true);
                }

            }else
            {
                    GetNameFromID(consultantId, ref firstName, ref lastName, false);
            }


            //unknown exists but creates problems and not assigned
            if (firstName.ToUpper() != "UNKNOWN" && firstName.ToUpper() != "NOT")
            {
                //TO DO check if fm is null
                // LogSimple.LogInfo("Sale :" + saleId + " Get FM from QSP");
                criteria = QSP.Business.Fulfillment.FieldSalesManager.CreateCriteria2();
                criteria.Add(Expression.Eq(QSP.Business.Fulfillment.FieldSalesManager.FirstNameProperty, firstName));
                criteria.Add(Expression.Eq(QSP.Business.Fulfillment.FieldSalesManager.LastNameProperty, lastName));
                criteria.Add(Expression.Eq(QSP.Business.Fulfillment.FieldSalesManager.DeletedProperty, false));

                List<QSP.Business.Fulfillment.FieldSalesManager> fms = QSP.Business.Fulfillment.FieldSalesManager.GetFieldSalesManagerList(criteria);

                //check if fm is found on qsp
                if (fms.Count > 0)
                {
                    fmID = Convert.ToInt32(fms[0].FmId);
                }
                else
                {
                    errorFound = true;
                    error = "The FM for the sale " + saleId + " does not exist on QSP";//error
                }
            }
            else
            {
                errorFound = true;
                error = "The FM for the sale " + saleId + " does not exist on QSP";//error
             
            }

            return errorFound;

        }


          private bool GetUserID(ref int userID, ref string error, int saleId, int crmUserId)
        {           

           bool errorFound = false;
           string firstName = "";
           string lastName = "";

           GetNameFromID(crmUserId, ref firstName, ref lastName, false);


            //TO DO check if fm is null
            ICriteria criteria = QSP.Business.Fulfillment.User.CreateCriteria2();
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.User.FirstNameProperty, firstName));
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.User.LastNameProperty, lastName));
            criteria.Add(Expression.Eq(QSP.Business.Fulfillment.User.DeletedProperty, false));

            List<QSP.Business.Fulfillment.User> users = QSP.Business.Fulfillment.User.GetUserList(criteria);
            //check if user is found on qsp
            if (users.Count > 0)
            {
                userID = Convert.ToInt32(users[0].UserId);
            }
            else
            {
                errorFound = true;
                error = "The current user (" + firstName + " " + lastName + ") does not exist on QSP";//error
            }

            return errorFound;

        }

        private void GetSourceID(int consultantID, int leadID, ref int sourceID)
        {
            //check if theres a lead consultant
            efundraising.EFundraisingCRM.Lead lead = efundraising.EFundraisingCRM.Lead.GetLeadByID(leadID);


            ICriteria criteria = QSP.Business.Fulfillment.Source.CreateCriteria2();
            if (consultantID == 3450 && lead.ConsultantId == 3450) //FR
            {   
                criteria.Add(Expression.Eq(QSP.Business.Fulfillment.Source.SourceNameProperty, "fundraising.com"));
                List<QSP.Business.Fulfillment.Source> sources = QSP.Business.Fulfillment.Source.GetSourceList(criteria);
                if (sources != null && sources.Count > 0)
                {
                    sourceID = sources[0].SourceId;
                }
            }
            else
            {
                //get source id for efund
                criteria.Add(Expression.Eq(QSP.Business.Fulfillment.Source.SourceNameProperty, "efundraising.com"));
                List<QSP.Business.Fulfillment.Source>  sources = QSP.Business.Fulfillment.Source.GetSourceList(criteria);
                if (sources != null && sources.Count > 0)
                {
                    sourceID = sources[0].SourceId;
                }
            }

        }


	}
}
