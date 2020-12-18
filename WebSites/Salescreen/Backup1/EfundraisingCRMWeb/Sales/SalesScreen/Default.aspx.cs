using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EFundraisingCRMWeb;
//using EFundraisingCRMWeb.App_Data;
using EFundraisingCRMWeb.Components.Server;
using efundraising.EFundraisingCRM;
using NHibernate;
using NHibernate.Expression;
using System.Collections.Generic;
using EfundraisingCRM.AddressHygiene.staging;
using System.Linq;
using System.Net.Mail;



namespace EFundraisingCRMWeb.Sales.SalesScreen
{
    public partial class Default : EFundraisingCRMSalesBasePage
    {



        protected EFundraisingCRMWeb.Components.User.Sales.SaleInfoList SaleInfoList1;
        protected EFundraisingCRMWeb.Components.User.Lead.LeadSummary LeadSummary1;
        protected EFundraisingCRMWeb.Components.User.ClientControls.ClientRequests ClientRequests1;
        protected EFundraisingCRMWeb.Components.User.ClientControls.CustomerInformation ClientInformation;
        protected EFundraisingCRMWeb.Components.User.Address ShippingAddress;
        protected System.Web.UI.WebControls.Label NoSaleLabel;
        protected EFundraisingCRMWeb.Components.User.Address BillingAddress;

        //protected EFundraisingCRMWeb.Components.User.Search.SearchForm SearchForm1;
        /*protected Components.User.CreateNew.Create Create1;
        protected Components.User.Menu.PageInformation PageInformation1;*/


        protected void Page_Load(object sender, System.EventArgs e)
        {
            bool isvalid = true;
            
            System.Diagnostics.Debug.Assert(isvalid, "Hello", "dasda");
      


            //set country in session
            Session["Country"] = BillingAddress.Country;

            ////



         //   efundraising.Diagnostics.Logger.LogError("Sales Screen: Default Page Load. Line:", new Exception());

            Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 60)) + "; url=../../SessionExpired.aspx");

            int row = 0;
            AddressHygieneControl.OutputAddress += new EFundraisingCRMWeb.Components.User.AddressHygiene.OutputAddressEventHandler(AddressSelected);
           
            try
            {
              /*  if (Session["SessionID"] != null)
                {*/

                    row = 1;
                    WarehouseErrorLabel.Visible = false;
                    ErrorLabel.Visible = false;
                    ErrorClientInfoLabel.Visible = false;

                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    row = 2;

                    // Put user code to initialize the page here
                    if (!IsPostBack)
                    {
                        bool isProd = Convert.ToBoolean(ManageSaleScreen.GetValueFromWebConfig("EFundraisingProd.Production", "isProduction"));
                 
                        row = 3;
                        //if (isProd)
                        //{
 //OE                          FillWarehouseDropdown();
                        //}
                        row = 4;
                        BillingAddress.SetControlAsShipping(false);
                        ShippingAddress.SetControlAsShipping(true);

                        //empty session to be able to resquest new clients

                        Session[Global.SessionVariables.CLIENT_ID] = null;
                        Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE] = null;
                        Session[Global.SessionVariables.LEAD_ID] = null;

                        if (Request["clid"] != null)
                        {
                            Session[Global.SessionVariables.CLIENT_ID] = Request["clid"];
                        }
                        if (Request["seq"] != null)
                        {
                            Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE] = Request["seq"];
                        }
                        if (Request["lid"] != null)
                        {
                            Session[Global.SessionVariables.LEAD_ID] = Request["lid"];
                        }

                        row = 5;
                        if (LeadID > -1)
                        {  //cehck query string
                            //check if a client exists for that lead
                            efundraising.EFundraisingCRM.Client c = efundraising.EFundraisingCRM.Client.GetClientByLeadID(LeadID);
                            if (c != null)
                            {
                                Session[Global.SessionVariables.CLIENT_ID] = c.ClientId;
                                Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE] = c.ClientSequenceCode;
                                newSalesbutton.Enabled = true;
                            }
                            else
                            {
                                //set client info from lead info
                                SetClientInfoForLead(LeadID, true);
                                SameAsCheckBox.Checked = true;
                                PickUpCheckBox.Checked = false;
                                WarehouseDropDownList.Visible = false;

                                //set empty sale grid
                                //int nbSales = SaleInfoList1.DoDataBind(EFundraisingCRM.Sale.GetSaleCollectionsByClient(clientInfo));

                                //check if a client exists for that lead
                                //int nbSales = SaleInfoList1.DoDataBind(EFundraisingCRM.Sale.GetSaleCollectionsByClient(clientInfo));
                                LeadSummary1.FillControl(LeadID);
                                ClientHeader1.SetLeadInfo(LeadID);
                                ClientRequests1.DoDataBind(LeadID);
                            }
                        }

                        //if came in with a client id or the the client id was set previously	
                        if (clientID > -1)
                        {
                            newSalesbutton.Enabled = true;
                            clientInfo = ManageClient.GetClient(clientID, clientSequenceCode);

                            if (clientInfo != null)
                            {
                                //set leadid in session
                                Session[Global.SessionVariables.LEAD_ID] = clientInfo.LeadId;

                                BillingAddress.SetClientBillingInfo(clientInfo);
                                

                                //if client doesnt have ST address, check Same as Billing
                                if (clientInfo.ClientShippingAddress == null)
                                {
                                    SameAsCheckBox.Checked = true;
                                    PickUpCheckBox.Checked = false;
                                    WarehouseDropDownList.Visible = false;
                                    ShippingAddress.SetClientBillingInfo(clientInfo);
                                    ShippingAddress.Enable(false);
                                }
                                else
                                {
                                    SameAsCheckBox.Checked = false;
                                    ShippingAddress.SetClientShipingInfo(clientInfo);
                                    if (clientInfo.ClientShippingAddress.PickUp)
                                    {
                                        PickUpCheckBox.Checked = true;
                                        try
                                        {
                                            WarehouseDropDownList.SelectedValue = clientInfo.ClientShippingAddress.WarehouseId.ToString();
                                        }
                                        catch (Exception x)
                                        {

                                            WarehouseErrorLabel.Visible = true;
                                        }
                                        WarehouseDropDownList.Visible = true;
                                    }
                                    else
                                    {
                                        PickUpCheckBox.Checked = false;
                                        WarehouseDropDownList.Visible = false;
                                    }
                                }

                                efundraising.EFundraisingCRM.SaleCollection sales = efundraising.EFundraisingCRM.Sale.GetSaleCollectionsByClient(clientInfo);

                                Session[Global.SessionVariables.SALE_COLLECTION] = sales;

                                int nbSales = SaleInfoList1.DoDataBind(sales);

                                LeadSummary1.FillControl(clientInfo.LeadId);
                                ClientRequests1.DoDataBind(clientInfo.LeadId);
                                ClientInformation.SetCustomerInfo(clientInfo);
                                clientInfoInMemory = clientInfo;
                                ClientHeader1.SetClientInfo(clientID, clientSequenceCode);

                                //Only allow modifying shipping address if an unconfirmed sale is present or 
                                //no sale at all
                                /*
                                                            efundraising.EFundraisingCRM.Client client = efundraising.EFundraisingCRM.Client.GetClientByLeadID(LeadID);
                                                            efundraising.EFundraisingCRM.SaleCollection saless = efundraising.EFundraisingCRM.Sale.GetSaleCollectionsByClient(client);


                                                            bool allConfirmedSales = true;
                                                            foreach (efundraising.EFundraisingCRM.Sale sale in saless)
                                                            {
                                                                efundraising.EFundraisingCRM.SalesStatus status = efundraising.EFundraisingCRM.SalesStatus.GetSalesStatusByID(sale.SalesStatusId);
                                                                if (status.Description != "Confirmed")
                                                                {
                                                                    allConfirmedSales = false;
                                                                }
                                                            }

                                                            if (allConfirmedSales && saless.Count > 0)
                                                            {
                                                                SameAsCheckBox.Enabled = false;
                                                                ShippingAddress.Enable(false);
                                                                ShippingAddress.EnableLocation(true);
                                                                ShippingAddress.EnableAttnOf(true);
                                                            }*/

                            }
                            else  //exist must no info
                            {
                                efundraising.Diagnostics.Logger.LogError("Sales Screen: Default Page Load -->Client Does not exist");
                            }
                        }
                        else  //client doesnt exists
                        {
                            newSalesbutton.Enabled = false;
                            SaleCollection sales = new SaleCollection();
                            int nbSales = SaleInfoList1.DoDataBind(sales);

                            //efundraising.Diagnostics.Logger.LogError("Sales Screen: Default Page Load -->No Client Specified");
                        }

                        SetRights();


                    }
               /*}
               else //no session
                {
                    int a = 1;
                }*/
                
           }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: Default Page Load. Line:" + row, ex);
            }
        }

        public void Refresh()
        {

        }



        #region IPage Members

        public string PageInformation
        {
            get
            {
                return "Main Sales Form";
            }
        }

        public string PageDescription
        {
            get
            {
                return "Sale Form";
            }
        }

    /*    public override void Search(string searchQuery)
        {
            //get leadid coprrspondiong to sale id
            EFundraisingCRM.Sale s = EFundraisingCRM.Sale.GetSaleByID(Convert.ToInt32(searchQuery));
            if (s != null)
            {
                Redirect("../../Sales/SalesScreen/Default.aspx?clid=" + s.ClientId + "&seq=" + s.ClientSequenceCode);
            }


        }
        *//*
        public override void Create(string redirection)
        {
            base.Create(redirection);
        }
        */
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            //sequenceCodedropdownlist.SelectedIndexChanged +=new EventHandler(sequenceCodedropdownlist_SelectedIndexChanged);
            //ShippingAddress.eventSetAsBillingAddress +=new EventHandler(ShippingAddress1_eventSetAsBillingAddress);
            this.newSalesbutton.Click += new EventHandler(newSalesbutton_Click);
            this.SaleInfoList1.OnSaleSelect += new Components.User.Sales.saleSelect(SaleInfoList1_OnSaleSelect);
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        #region Properties

        private int LeadID
        {
            get
            {
                try
                {
                    if (Session[Global.SessionVariables.LEAD_ID] == null)
                        return int.MinValue;
                    else
                        return Convert.ToInt32(Session[Global.SessionVariables.LEAD_ID]);

                }
                catch (Exception)
                {
                    return int.MinValue;
                }
            }
        }

        private string clientSequenceCode
        {
            get
            {
                try
                {
                    if (Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE] == null)
                        return "";
                    else
                        return Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();

                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        private int clientID
        {
            get
            {
                try
                {
                    if (Session[Global.SessionVariables.CLIENT_ID] == null)
                    {
                        return int.MinValue;
                    }
                    else
                    {
                        return Convert.ToInt32(Session[Global.SessionVariables.CLIENT_ID]);
                    }

                }
                catch (Exception)
                {
                    return int.MinValue;
                }
            }
        }


        private efundraising.EFundraisingCRM.Client clientInfo;

        private efundraising.EFundraisingCRM.Client clientInfoInMemory
        {
            get
            {
                return (Client)ViewState["clientInfoInMemory"];
            }
            set
            {
                ViewState["clientInfoInMemory"] = value;
            }
        }


        #endregion

        private void sequenceCodedropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SetAllControlsVisibleAs(bool bflag)
        {
            ClientInformation.Visible = bflag;
            ShippingAddress.Visible = bflag;
            BillingAddress.Visible = bflag;
            SaleInfoList1.Visible = bflag;
            LeadSummary1.Visible = bflag;
            ClientRequests1.Visible = bflag;
            billingShippingTable.Visible = bflag;
            leadRequestTable.Visible = bflag;
            saveInfobutton.Visible = bflag;
            newSalesbutton.Visible = bflag;
        }


 ////OE     private void FillWarehouseDropdown()
 //       {
 //           //get source id for efund
 //           List<QSP.Business.Fulfillment.Warehouse> warehouses = QSP.Business.Fulfillment.Warehouse.GetPickupWarehouseList();
            
 //           foreach (QSP.Business.Fulfillment.Warehouse warehouse in warehouses)
 //           {
 //               ListItem li = new ListItem();
 //               li.Text = warehouse.FulfWarehouseId.ToString() + " (" + warehouse.WarehouseName + ")";
 //               li.Value = warehouse.WarehouseId.ToString();
 //               WarehouseDropDownList.Items.Add(li);
 //           }
 //       }


        private void ShippingAddress1_eventSetAsBillingAddress(object sender, EventArgs e)
        {

            CheckBox cb = sender as CheckBox;
            if (cb != null && clientInfoInMemory != null)
            {
                if (!cb.Checked)
                {
                    ShippingAddress.SetClientShippingAddress(clientInfoInMemory.ClientShippingAddress);
                }
                else
                {
                    ShippingAddress.SetClientShippingAddress(clientInfoInMemory.ClientBillingAddress);
                }
            }
        }

        private void SetClientInfoForLead(int leadID, bool sameAsBilling)
        {
            try
            {
                Lead l = Lead.GetLeadByID(LeadID);
                BillingAddress.StreetAddress = l.StreetAddress;
                BillingAddress.City = l.City;
                BillingAddress.Country = l.CountryCode;
                BillingAddress.State = l.StateCode;
                BillingAddress.Zip = l.ZipCode;
                BillingAddress.Zone = l.AddressZoneId;

                if (sameAsBilling)
                {
                    ShippingAddress.StreetAddress = l.StreetAddress;
                    ShippingAddress.City = l.City;
                    ShippingAddress.Country = l.CountryCode;
                    ShippingAddress.State = l.StateCode;
                    ShippingAddress.Zip = l.ZipCode;
                    ShippingAddress.Zone = l.AddressZoneId;
                    ShippingAddress.Enable(false);

                }

                //set client seq with partner
                if (l.PromotionId > 0)
                {
                    Promotion p = Promotion.GetPromotionByID(l.PromotionId);
                    Partner pa = Partner.GetPartnerByID(p.PartnerId);
                    if (pa.PartnerName == "Fundraising.com")
                    {
                        ClientInformation.ClientSequenceCode = "IF";
                    }
                    else if (l.PromotionId == 115)
                    {
                        ClientInformation.ClientSequenceCode = "CG";
                    }
                    else if (l.CountryCode == "CA")
                    {
                        ClientInformation.ClientSequenceCode = "CI";
                    }
                }
                Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE] = ClientInformation.ClientSequenceCode;

                ClientInformation.FirstName = l.FirstName;
                ClientInformation.LastName = l.LastName;
                ClientInformation.Organization = l.Organization;
                ClientInformation.Email = l.Email;
                ClientInformation.DayPhone = l.DayPhone;
                ClientInformation.DayPhoneExt = l.DayPhoneExt;
                ClientInformation.EveningPhone = l.EveningPhone;
                ClientInformation.EveningPhoneExt = l.EveningPhoneExt;
                ClientInformation.Fax = l.Fax;
                ClientInformation.TimeToCall = l.DayTimeCall;
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("SetClientInfoForLead: " + leadID, ex);
            }

        }

        private string SaveClientShippingAddress(efundraising.EFundraisingCRM.Client theClient)
        {
            try
            {
                if (theClient != null)
                {
                    ClientAddress clShippingAddress = theClient.ClientShippingAddress;
                    ClientAddress clFromInterface = this.ShippingAddress.GetClientAddress();
                    if (clShippingAddress != null)
                    {
                        clShippingAddress.AttentionOf = clFromInterface.AttentionOf;
                        clShippingAddress.StreetAddress = clFromInterface.StreetAddress;
                        clShippingAddress.ZipCode = clFromInterface.ZipCode;
                        clShippingAddress.StateCode = clFromInterface.StateCode;
                        clShippingAddress.CountryCode = clFromInterface.CountryCode;
                        clShippingAddress.City = clFromInterface.City;
                        //clShippingAddress.Phone1 = clFromInterface.Phone1;
                        //clShippingAddress.Phone2 = clFromInterface.Phone2;
                        clShippingAddress.AddressZoneId = clFromInterface.AddressZoneId;
                        clShippingAddress.Update();
                    }
                    else
                    {
                        clFromInterface.ClientId = theClient.ClientId;
                        clFromInterface.ClientSequenceCode = theClient.ClientSequenceCode;
                        clFromInterface.AddressType = ClientAddressType.ShippingAddress.AddressType;
                        clFromInterface.AddressId = int.MinValue;
                        clFromInterface.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("SaveClientShippingAddress: Cannot Save Client Shipping Address", ex);
                return ex.ToString();
            }
            return string.Empty;
        }

        protected void saveInfobutton_Click(object sender, System.EventArgs e)
        {
            SaveInfo(true);
           
        }

        private void SaveInfo(bool addressHygiene)
        {
            bool valid = true;

            try
            {
                
                //validate fields  
                if (!(ClientInformation.IsValid()))
                {
                    valid = false;
                    ErrorClientInfoLabel.Visible = true;
                }

                if (!(BillingAddress.IsAddressValid(false)))
                {
                    valid = false;
                    BillingAddress.PrintAddressError(true);
                }

                //if (SameAsCheckBox.Enabled)
                // {
                if (!(ShippingAddress.IsAddressValid(true, SameAsCheckBox.Checked)))
                {
                    valid = false;
                    ShippingAddress.PrintAddressError(false);
                }
                // }

                bool enableSuggestionList = true;
                bool isBilling = true;

                //ADDRES HYGIENE

                string value = Components.Server.ManageSaleScreen.GetValueFromWebConfig("AddressHygieneEnabled", "Value");
                //ignore recommendation
                if (addressHygiene == false)
                {
                    value = "false";
                }
                if (value == "true")
                {
                    if (valid)
                    {
                        valid = AddressHygiene(BillingAddress, enableSuggestionList, isBilling);
                        if (valid)
                        {
                            isBilling = false;
                            valid = AddressHygiene(ShippingAddress, enableSuggestionList, isBilling);
                        }
                    }
                }



                if (valid)
                {

                    efundraising.EFundraisingCRM.Client client = efundraising.EFundraisingCRM.Client.GetClientByLeadID(LeadID);
                    if (client != null)
                    {
                        efundraising.EFundraisingCRM.SaleCollection saless = efundraising.EFundraisingCRM.Sale.GetSaleCollectionsByClient(client);
                        bool allConfirmedSales = true;
                        foreach (efundraising.EFundraisingCRM.Sale sale in saless)
                        {
                            efundraising.EFundraisingCRM.SalesStatus status = efundraising.EFundraisingCRM.SalesStatus.GetSalesStatusByID(sale.SalesStatusId);
                            System.Diagnostics.Debug.Write(sale.SalesId);
                            if (sale.SalesId == 67294)
                            {
                                int a = 1;
                            }
                            if (status.Description != "Confirmed")
                            {
                                allConfirmedSales = false;
                            }
                        }

                        if (allConfirmedSales && saless.Count > 0)
                        {
                            WarningLabel.Visible = true;
                        }
                        else
                        {
                            WarningLabel.Visible = false;
                        }
                    }


                    int warehouseID = int.MinValue;
                    if (PickUpCheckBox.Checked)
                    {
                        warehouseID = Convert.ToInt32(WarehouseDropDownList.SelectedValue);

                    }


                    //get client from db if exists
                    Client c;
                    Lead l = null;
                    int newID = 0;
                    bool newClient = false;

                    if (clientID > 0)
                    {
                        newClient = false;
                        c = Client.GetClientByLeadIDAndSequenceCode(clientID, clientSequenceCode);
                    }
                    else
                    {
                        newClient = true;
                        c = new Client();
                        l = Lead.GetLeadByID(LeadID);
                    }


                    Client clFromInterface = this.ClientInformation.GetCustomerInfoFromInterface();

                    c.ClientSequenceCode = clFromInterface.ClientSequenceCode;
                    c.FirstName = clFromInterface.FirstName;
                    c.LastName = clFromInterface.LastName;
                    c.Organization = clFromInterface.Organization;
                    c.Email = clFromInterface.Email;
                    c.DayPhone = clFromInterface.DayPhone;
                    c.DayPhoneExt = clFromInterface.DayPhoneExt;
                    c.EveningPhone = clFromInterface.EveningPhone;
                    c.EveningPhoneExt = clFromInterface.EveningPhoneExt;
                    c.Fax = clFromInterface.Fax;
                    c.DayTimeCall = clFromInterface.DayTimeCall;

                    if (newClient)
                    {
                        c.LeadId = LeadID;
                        c.ChannelCode = l.ChannelCode;
                        c.PromotionId = l.PromotionId;
                        c.OrganizationClassCode = "OTH";
                        c.DivisionId = l.DivisionId;
                        newID = c.Insert();

                        ClientInformation.ClientID = newID;

                        Session[Global.SessionVariables.CLIENT_ID] = newID;
                        Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE] = clFromInterface.ClientSequenceCode;

                        newSalesbutton.Enabled = true;



                    }
                    else
                    {
                        try
                        {
                            c.Update();
                            newSalesbutton.Enabled = true;
                        }
                        catch (Exception x)  //Error On Trigger
                        {
                            ErrorLabel.Text = "Error while updating." + x.Message.Substring(0, 100);
                            ErrorLabel.Visible = true;
                        }

                    }



                    //save address
                    ClientAddress ca = ClientAddress.GetClientAddressByIdSequenceAddressType(clientID, clientSequenceCode, "BT");

                    bool noAddress = false;
                    if (ca == null)
                    {
                        noAddress = true;

                    }

                    if (newClient)
                    {
                        ca = new ClientAddress();
                        ca.ClientId = newID;
                        ca.ClientSequenceCode = clFromInterface.ClientSequenceCode;
                        ca.AddressType = "BT";
                    }
                    else if (noAddress)
                    {
                        ca = new ClientAddress();
                        ca.ClientId = clientID;
                        ca.ClientSequenceCode = clientSequenceCode;
                        ca.AddressType = "BT";
                    }

                    ca.StreetAddress = BillingAddress.StreetAddress;
                    ca.City = BillingAddress.City;
                    ca.StateCode = BillingAddress.State;
                    ca.ZipCode = BillingAddress.Zip;
                    ca.CountryCode = BillingAddress.Country;
                    ca.AddressZoneId = BillingAddress.Zone;
                    ca.Phone1 = clFromInterface.DayPhone + clFromInterface.DayPhoneExt;
                    ca.Phone2 = clFromInterface.EveningPhone + clFromInterface.EveningPhoneExt;


                    if (newClient || noAddress)
                    {
                        ca.Insert();
                    }

                    else
                    {
                        try
                        {
                            ca.Update();
                        }
                        catch (Exception x)  //Error On Trigger
                        {

                            ErrorLabel.Text = "Error while updating." + x.Message.Substring(0, 100);
                            ErrorLabel.Visible = true;
                        }
                    }



                    //check if a Ship address exists, and update to match bill to
                    ca = ClientAddress.GetClientAddressByIdSequenceAddressType(clientID, clientSequenceCode, "ST");

                    if (ca != null && SameAsCheckBox.Checked)
                    {
                        //update existing ST addrtess with BT info
                        ca.AttentionOf = ShippingAddress.AttentionOf;
                        ca.StreetAddress = BillingAddress.StreetAddress;
                        ca.City = BillingAddress.City;
                        ca.StateCode = BillingAddress.State;
                        ca.ZipCode = BillingAddress.Zip;
                        ca.CountryCode = BillingAddress.Country;
                        ca.AddressZoneId = BillingAddress.Zone;
                        ca.Phone1 = clFromInterface.DayPhone + clFromInterface.DayPhoneExt;
                        ca.Phone2 = clFromInterface.EveningPhone + clFromInterface.EveningPhoneExt;
                        ca.Location = ShippingAddress.Location;
                        ca.PickUp = PickUpCheckBox.Checked;
                        ca.WarehouseId = warehouseID;

                        ca.Update();

                    }

                    else if (ca != null && !SameAsCheckBox.Checked)
                    {
                        //update existing ship to address info
                        ca.AttentionOf = ShippingAddress.AttentionOf;
                        ca.StreetAddress = ShippingAddress.StreetAddress;
                        ca.City = ShippingAddress.City;
                        ca.StateCode = ShippingAddress.State;
                        ca.ZipCode = ShippingAddress.Zip;
                        ca.CountryCode = ShippingAddress.Country;
                        ca.AddressZoneId = ShippingAddress.Zone;
                        ca.Phone1 = clFromInterface.DayPhone + clFromInterface.DayPhoneExt;
                        ca.Phone2 = clFromInterface.EveningPhone + clFromInterface.EveningPhoneExt;
                        ca.Location = ShippingAddress.Location;
                        ca.PickUp = PickUpCheckBox.Checked;
                        ca.WarehouseId = warehouseID;

                        ca.Update();

                    }
                    else if (ca == null && !SameAsCheckBox.Checked)
                    {

                        //create a ship to address info
                        ClientAddress newST = ClientAddress.GetClientAddressByIdSequenceAddressType(clientID, clientSequenceCode, "BT");

                        //EFundraisingCRM.ClientAddress newST = new EFundraisingCRM.ClientAddress();
                        newST.ClientId = clientID;
                        newST.ClientSequenceCode = clientSequenceCode;
                        newST.AddressType = "ST";
                        newST.AttentionOf = ShippingAddress.AttentionOf;
                        newST.StreetAddress = ShippingAddress.StreetAddress;
                        newST.City = ShippingAddress.City;
                        newST.StateCode = ShippingAddress.State;
                        newST.ZipCode = ShippingAddress.Zip;
                        newST.CountryCode = ShippingAddress.Country;
                        newST.AddressZoneId = ShippingAddress.Zone;
                        newST.Phone1 = clFromInterface.DayPhone + clFromInterface.DayPhoneExt;
                        newST.Phone2 = clFromInterface.EveningPhone + clFromInterface.EveningPhoneExt;
                        newST.Location = ShippingAddress.Location;
                        newST.PickUp = PickUpCheckBox.Checked;
                        newST.WarehouseId = warehouseID;

                        newST.Insert();

                        //	}

                    }
                    else
                    {
                        //create a ship to address info
                        ClientAddress newST = ClientAddress.GetClientAddressByIdSequenceAddressType(clientID, clientSequenceCode, "BT");

                        //EFundraisingCRM.ClientAddress newST = new EFundraisingCRM.ClientAddress();
                        newST.ClientId = clientID;
                        newST.ClientSequenceCode = clientSequenceCode;
                        newST.AddressType = "ST";
                        newST.AttentionOf = ShippingAddress.AttentionOf;
                        newST.StreetAddress = BillingAddress.StreetAddress;
                        newST.City = BillingAddress.City;
                        newST.StateCode = BillingAddress.State;
                        newST.ZipCode = BillingAddress.Zip;
                        newST.CountryCode = BillingAddress.Country;
                        newST.AddressZoneId = BillingAddress.Zone;
                        newST.Phone1 = clFromInterface.DayPhone + clFromInterface.DayPhoneExt;
                        newST.Phone2 = clFromInterface.EveningPhone + clFromInterface.EveningPhoneExt;
                        newST.Location = ShippingAddress.Location;
                        newST.PickUp = PickUpCheckBox.Checked;
                        newST.WarehouseId = warehouseID;

                        newST.Insert();
                    }
                }

                if (valid)
                {
                    Session["clientinfovalid"] = true;
                }
                else
                {
                    Session["clientinfovalid"] = false;
                }
               

            }
            
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Error in Save Info on default page. LeadID=" + LeadID, ex);
                ErrorLabel.Text = "Error while updating." + ex.Message.ToString();
                ErrorLabel.Visible = true;
            }
            
        }

        protected void newSalesbutton_Click(object sender, EventArgs e)
        {
            try
            {
                // run save client method to make sure client info is correctly inputed before trying to enter a new sale
                //SaveInfo(true);
                if (Session["clientinfovalid"] == null || Session["clientinfovalid"].ToString() == "False")
                {
                    RegisterStartupScript("myAlert", "<script>alert('Please Save client first.')</script>");
                    return;
                }
                
                
                if (Session[Global.SessionVariables.CLIENT_ID] == null)
                {
                    //Create client first
                    RegisterStartupScript("myAlert", "<script>alert('Please create client first.')</script>");
                }
                 
                else
                {
                    

                    int clientID = Convert.ToInt32(Session[Global.SessionVariables.CLIENT_ID]);
                    string clientSeqCode = Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();

                    ClientAddress ST = ClientAddress.GetClientAddressByIdSequenceAddressType(clientID, clientSeqCode, "ST");
                    if (ST == null) 
                    {
                        RegisterStartupScript("myAlert", "<script>alert('Please put the Attention Of for shipping.')</script>");
                    }else if (ST.AttentionOf == null || ST.AttentionOf == ""){
                                      
                    
                        RegisterStartupScript("myAlert", "<script>alert('Please put the Attention Of for shipping.')</script>");
                    }
                    else if (ST.Location == null || ST.Location == "")
                    {
                        RegisterStartupScript("myAlert", "<script>alert('Please put the Location for shipping.')</script>");
                    }
                    else
                    {
                        Redirect("~/Sales/SalesScreen/NewSales.aspx?clid=" + clientID + "&clseq=" + clientSeqCode);
                    }
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: newSalesbutton error", ex);
            }
        }

        private void SaleInfoList1_OnSaleSelect(object sender, System.EventArgs e)
        {
            if (clientInfoInMemory != null)
            {
                string salesId = sender as String;

                // run save client method to make sure client info is correctly inputed before trying to enter a sale
                SaveInfo(true);
                if (!(Boolean)Session["clientinfovalid"])
                {
                    return;
                }

                else
                {
                    if (salesId != null)
                        Redirect(string.Format("~/Sales/SalesScreen/NewSales.aspx?sid={0}", salesId));
                }
            }

        }

        protected void SameAsCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (SameAsCheckBox.Checked)
            {
                CopyAddress();

            }
            else
            {
                ShippingAddress.Enable(true);
                ShippingAddress.EnableLocation(true);
                ShippingAddress.EnableAttnOf(true);

            }

        }

        private void CopyAddress()
        {
            ShippingAddress.StreetAddress = BillingAddress.StreetAddress;
            ShippingAddress.City = BillingAddress.City;
            ShippingAddress.Zip = BillingAddress.Zip;
            ShippingAddress.Country = BillingAddress.Country;
            ShippingAddress.SetNewCountryStateDropDown(BillingAddress.Country);

            ShippingAddress.State = BillingAddress.State;
            ShippingAddress.RefreshDropDowns();
            ShippingAddress.Enable(false);

        }

        private void SetRights()
        {
            try
            {
                if (ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_Accounting) ||
                     ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_IT))
                {
                    SyncButton.Visible = true;
                }

                if (ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_Production) ||
                    ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_IT))
                {
                    AdminButton.Visible = true;
                }

                if (!(ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_IT) ||
                      ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_Production) ||
                      ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_SaleSupport)))
                {
                    ClientInformation.DisableForConsultants();
                }

            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: SetRights", ex);
            }


        }

        protected void PickUpCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (PickUpCheckBox.Checked)
            {
                WarehouseDropDownList.Visible = true;
            }
            else
            {
                WarehouseDropDownList.Visible = false;
            }
        }

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            /*	WarehouseDescLabel.Visible = true;
			
                int pos = WarehouseDropDownList.SelectedValue.IndexOf("/");
                string name = WarehouseDropDownList.SelectedValue.Substring(pos + 1,WarehouseDropDownList.SelectedValue.Length - (pos + 1));
                WarehouseDescLabel.Text = name;*/
        }



        private void AddressSelected(object Sender, OutputAddress OutputAddress, bool ChangeStatus)
        {

            bool isBilling = AddressHygieneControl.IsBilling;

      
            System.Drawing.Color labelColor = System.Drawing.Color.Red;
            string labelText = "";

      
            if (isBilling)
            {
                BillingAddress.StreetAddress = OutputAddress.Address1.ToLower() + " " + OutputAddress.Address2.ToLower();
                BillingAddress.City = OutputAddress.City;
                BillingAddress.Zip1 = OutputAddress.PostCode;
                BillingAddress.Zip2 = OutputAddress.PostCode2;
                BillingAddress.State = ConvertToEFRProvince(OutputAddress.Region, false);
                BillingAddress.Country = OutputAddress.Country;
            }
            else
            {
                ShippingAddress.StreetAddress = OutputAddress.Address1.ToLower() + " " + OutputAddress.Address2.ToLower();
                ShippingAddress.City = OutputAddress.City;
                ShippingAddress.Zip1 = OutputAddress.PostCode;
                ShippingAddress.Zip2 = OutputAddress.PostCode2;
                ShippingAddress.State = ConvertToEFRProvince(OutputAddress.Region, false);
                ShippingAddress.Country = OutputAddress.Country;
            }
                            
            //Display Status 
            if (ChangeStatus)
            {
                labelColor = System.Drawing.Color.Green;
                labelText = "Address Updated. Please Double Check";
            }
            else
            {
                labelText = "Address Validated";
            }

            ///////////////////////
            if (isBilling)
            {
                AddressHygieneStatusBillLabel0.Visible = true;
                AddressHygieneStatusBillLabel0.ForeColor = labelColor;
                AddressHygieneStatusBillLabel0.Text = labelText;
            }
            else
            {
                AddressHygieneStatusShipLabel.Visible = true;
                AddressHygieneStatusShipLabel.ForeColor = labelColor;
                AddressHygieneStatusShipLabel.Text = labelText;
            }

        }

        private Address CreateAddress(EFundraisingCRMWeb.Components.User.Address address)
        {
            Address newAddress = new Address();

            newAddress.Address1 = address.StreetAddress.ToUpper();
            newAddress.City = address.City;
            if (address.Country == "US")
            {
                newAddress.Country = "UNITED STATES";
            }
            else if (address.Country == "CA")
            {
                newAddress.Country = "CANADA";
            }
            else
            {
                newAddress.Country = address.Country;
            }
            
            newAddress.PostCode = address.Zip1;
            newAddress.PostCode2 = address.Zip2;


            newAddress.Region = ConvertToEFRProvince(address.State, true);
            newAddress.Address2 = "";
            newAddress.County = "";

            return newAddress;

        }

        private bool AddressHygiene(EFundraisingCRMWeb.Components.User.Address address, bool enableSuggestionList, bool isBilling)
        {    
            int line = 0;
            try{

            
            //address hygiense
            bool labelVisible = true;
            System.Drawing.Color labelColor = System.Drawing.Color.Red;
            string labelText = "";
            bool valid = true;
            bool addressChanged = false;
            Address newAddress = CreateAddress(address);

            if (AddressHygieneControl.DoAddressHygieneNoDelagate(newAddress, true, ref addressChanged, isBilling))
            {
                line = 5;
                labelVisible = true;
                OutputAddress outputAddress =
                AddressHygieneControl.OutAddress;

                //Check if there was a Suggestion List error
                if (outputAddress.SuggestionListInformation.Error != SuggestionListError.None)
                {
                    labelColor = System.Drawing.Color.Red;
                    labelText = "Error: " + outputAddress.SuggestionListInformation.Error.ToString();

                    valid = false;

                    line = 10;
      
                }
                //Check if there was an error with the address that came back
                else if (outputAddress.Fault != Fault.NoError)
                {
                    labelColor = System.Drawing.Color.Red;
                    labelText = "Error: " + AddressHygieneControl.GetErrorFromFault(outputAddress.Fault.ToString());
                    valid = false;

                    line = 15;
            
                }

                else if (addressChanged)
                {
                    line = 20;
                    labelColor = System.Drawing.Color.Green;
                    if (isBilling)
                    {
                        labelText = "Billing Address Updated";
                        BillingAddress.StreetAddress = outputAddress.Address1.ToLower() + " " + outputAddress.Address2.ToLower();
                        BillingAddress.City = outputAddress.City;
                        BillingAddress.Zip1 = outputAddress.PostCode;
                        BillingAddress.Zip2 = outputAddress.PostCode2;
                        BillingAddress.State = ConvertToEFRProvince(outputAddress.Region, false);
                        BillingAddress.Country = outputAddress.Country;
                    }
                    else
                    {
                        line = 25;
                        labelText = "Shipping Address Updated";
                        ShippingAddress.StreetAddress = outputAddress.Address1.ToLower() + " " + outputAddress.Address2.ToLower();
                        ShippingAddress.City = outputAddress.City;
                        ShippingAddress.Zip1 = outputAddress.PostCode;
                        ShippingAddress.Zip2 = outputAddress.PostCode2;
                        ShippingAddress.State = ConvertToEFRProvince(outputAddress.Region, false);
                        ShippingAddress.Country = outputAddress.Country;
                    }

                }
                else
                {
                    labelVisible = false;
                }
            }
            line = 30;
            if (isBilling)
            {
                AddressHygieneStatusBillLabel0.Visible = labelVisible;
                AddressHygieneStatusBillLabel0.ForeColor = labelColor;
                AddressHygieneStatusBillLabel0.Text = labelText;
            }
            else
            {
                line = 35;
                AddressHygieneStatusShipLabel.Visible = labelVisible;
                AddressHygieneStatusShipLabel.ForeColor = labelColor;
                AddressHygieneStatusShipLabel.Text = labelText;
            }

            if (valid == false)
            {
                IgnoreButton.Visible = true;
            }
            else
            {
                IgnoreButton.Visible = false;
            }
            ////////////////////
            return valid;
            
            }
            catch (Exception ex)
            {
                
                efundraising.Diagnostics.Logger.LogError("Error in AddressHygiene. LeadID=" + line.ToString(), ex);
                return false;
            }
        }

        private string ConvertToEFRProvince(string prov, bool invert)
        {
            //convert FROM efr prov
            if (invert)
            {
                switch (prov.ToUpper())
                {
                    case "QU": return "QC";
                    case "OT": return "ON";
                    case "ALB": return "AB";
                    case "MAN": return "MB";
                    case "SA": return "SK";
                    case "NF": return "NL";
                    case "LB": return "NL";

                }
            }
            else
            {
                switch (prov.ToUpper())
                {

                    case "QC": return "QC";
                    case "ON": return "ON";
                    case "AB": return "AB";
                    case "MB": return "MB";
                    case "SK": return "SA";
                    case "NL": return "NB";
                }
            }

            //found nothing
            return prov;
        }

  

        protected void IgnoreButton_Click(object sender, EventArgs e)
        {
            AddressHygieneControl.Visible = false;
            AddressHygieneStatusBillLabel0.Visible = false;
            AddressHygieneStatusShipLabel.Visible = false;
            IgnoreButton.Visible = false;
            SaveInfo(false);
        }

        protected void AdminButton_Click(object sender, EventArgs e)
        {
            Redirect(string.Format("~/Sales/SalesScreen/AdminSection.aspx"));
        }

        protected void SyncButton_Click(object sender, EventArgs e)
        {
           
            Redirect(string.Format("~/Sales/SalesScreen/XML.aspx"));
        }

        
        protected void NewAddressButton_Click(object sender, EventArgs e)
        {

            try
            {
               
                if (LeadID > 0)
                {
                    //Session[Global.SessionVariables.LEAD_ID] = LeadID;
                    Session["NewShippingLeadID"] = LeadID;
                    string script = "<script language='javascript'>window.open('" + "AddressPopUp.aspx" + "','Streaming', 'width=500, height=500, location=no, menubar=no, status=no, toolbar=no, scrollbars=yes, resizable=yes')</script>";
                    Page.RegisterClientScriptBlock("Open", script);
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Error in NewAddressButton_Click", ex);

            }

        }

     

        
     
    }
}
