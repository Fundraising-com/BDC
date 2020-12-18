namespace EFundraisingCRMWeb.Components.User.Sales
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Components.Server;
    using EFundraisingCRMWeb;
    using efundraising.EFundraisingCRM;

	/// <summary>
	///		Summary description for SaleInfo.
	/// </summary>
	public partial class SaleInfo : System.Web.UI.UserControl
	{
		
 		private int cID;
		private bool disableConsultant = false;
		private bool disableEverything = false;
		private bool disableForConsultant = false;



		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//TrackButton.Visible = false;
			if (!IsPostBack)
			{
				SetConsultantName(false);
				SetPONumberStatus(false);
				SetCarrier(false);
				SetCarrierShippingOption(false);

				if (disableEverything)
				{
					ManageSaleScreen.MakeReadOnly(consultantDropDownList);
					ManageSaleScreen.MakeReadOnly(poNoStatusDropdownlist);
					ManageSaleScreen.MakeReadOnly(carrierDropdownlist);
					ManageSaleScreen.MakeReadOnly(shippingOptionDropdownlist);
		
				}
				else if (disableForConsultant)
				{
					ManageSaleScreen.MakeReadOnly(consultantDropDownList);
                    ManageSaleScreen.MakeReadOnly(poNoStatusDropdownlist);
					ManageSaleScreen.MakeReadOnly(carrierDropdownlist);
					ManageSaleScreen.MakeReadOnly(shippingOptionDropdownlist);
				}
				else if (disableConsultant)
				{
					Components.Server.ManageSaleScreen.MakeReadOnly(consultantDropDownList);
				}
			}
		}

        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (poNoStatusDropdownlist.SelectedIndex > 1)
        //    { chkPOComm.Checked = true; }
        //    else
        //    { chkPOComm.Checked = false; }

            
        //}
		
        
        
        private void SetConsultantName(bool bReset)
		{
			if (bReset || consultantDropDownList.Items.Count < 1)
			{
				consultantDropDownList.Items.Clear();
				ConsultantCollections consuls = Global.GetConsultants(Application);
						
				consultantDropDownList.Items.Add(new ListItem("None", int.MinValue.ToString()));
				for (int i = 0; i < consuls.Count; i++)
				{
					Consultant cns =  consuls[i] as Consultant;
					consultantDropDownList.Items.Add(new ListItem(cns.Name,cns.ConsultantId.ToString()));
				}
				if (cID > 0)
				{
					consultantDropDownList.SelectedValue = cID.ToString();
                    consultantDropDownList.ForeColor = Color.Black;
				}
				else
				{
                    consultantDropDownList.ForeColor = Color.Red; //warning

					//efundraising.Diagnostics.Logger.Error("Sales Screen: Sale Info. Consultant ID is null");
				}
				
					
				
			}
		}

		
		private void SetPONumberStatus(bool bReset)
		{
			if (bReset || poNoStatusDropdownlist.Items.Count < 1)
			{
				poNoStatusDropdownlist.Items.Clear();
				PoStatusCollections poStsColl = Global.GetPoStatusCollection(Application);
//		
				poNoStatusDropdownlist.Items.Add(new ListItem("--Please Select--",Int16.MinValue.ToString()));
				for (int i = 0; i < poStsColl.Count; i++)
				{
					PoStatus pos =  poStsColl[i] as PoStatus;
					poNoStatusDropdownlist.Items.Add(new ListItem(pos.Description,pos.PoStatusId.ToString()));
				}
			}
		}

		
		private void SetCarrier(bool bReset)
		{
			if (bReset || carrierDropdownlist.Items.Count < 1)
			{
				carrierDropdownlist.Items.Clear();
				carrierDropdownlist.Items.Add(new ListItem("None", Int16.MinValue.ToString()));
				CarriersCollection crColl = Global.GetCarrierCollection(Application);
				for (int i = 0; i < crColl.Count; i++)
				{
					Carrier crr =  crColl[i] as Carrier;
					carrierDropdownlist.Items.Add(new ListItem(crr.Description,crr.CarrierId.ToString()));
				}

			}
		}

		
		private void SetCarrierShippingOption(bool bReset)
		{
			if (bReset || shippingOptionDropdownlist.Items.Count < 1)
			{
				shippingOptionDropdownlist.Items.Clear();
				CarrierShippingOptionCollection csoColl = Global.GetCarrierShippingOptionCollection(Application);


				
				shippingOptionDropdownlist.Items.Add(new ListItem("None", Int16.MinValue.ToString()));
				for (int i = 0; i < csoColl.Count; i++)
				{
					CarrierShippingOption cso =  csoColl[i] as CarrierShippingOption;
					shippingOptionDropdownlist.Items.Add(new ListItem(cso.Description,cso.ShippingOptionId.ToString()));
				}
			}
		}


		private void SetSelectedValue(string sValue, System.Web.UI.WebControls.DropDownList dd)
		{
			try
			{
				dd.SelectedValue = sValue;
			}
			catch (Exception)
			{
			}
		}

		public void SetInfo(Sale s, Client cl)
		{
			SetConsultantName(false);
			SetPONumberStatus(false);
			SetCarrier(false);
			SetCarrierShippingOption(false);

            
			
			if (s != null)
            {

                try
                {
                    if (s.PoStatusId.ToString() == "2" )
                        s.PoStatusId = 3;

                    if (s.PoStatusId.ToString() == "1" || s.PoStatusId < 0)
                        s.PoStatusId = 4;

                    if (s.PoStatusId.ToString() == "3")
                        s.POConComm = 1;

                    if (s.POConComm == 1)
                    { chkPOComm.Checked = true; }
                    else
                    { chkPOComm.Checked = false; }
                }
                catch (Exception)
                { }

                SetSelectedValue(s.ConsultantId.ToString(), consultantDropDownList);
                SetSelectedValue(s.PoStatusId.ToString(), poNoStatusDropdownlist);

                ////
                ///Must Add The disabled Carrier for the existing sale
                Carrier c = Carrier.GetCarrierByID(s.CarrierId);
                if (c == null)
                {
                    s.CarrierId = 0;

                }
                else if (!(c.Active))
                {
                    carrierDropdownlist.Items.Add(new ListItem(c.Description, s.CarrierId.ToString()));
                }
                //////////////////
                SetSelectedValue(s.CarrierId.ToString(), carrierDropdownlist);
                SetSelectedValue(s.ShippingOptionId.ToString(), shippingOptionDropdownlist);
                saleIdTextBox.Text = s.SalesId.ToString();

                if (s.WaybillNo != null)
                {
                    waybillNoTextBox.Text = s.WaybillNo.ToString();
                }

                //get external consultant ID
                Lead lead = Lead.GetLeadByID(s.LeadId);
                if (lead.ExtConsultantId > 0 )
                {
                   Consultant consultant = new Consultant();
                   consultant = Consultant.GetConsultantByID(lead.ExtConsultantId);
                   extConsultantNnameTextBox.Text = consultant.Name;
                }

                //waybillNoTextBox.Text = s.WaybillNo;
                
               
            }
		}

		public void DisableConsultant()
		{
			disableConsultant = true;
		}

		public void DisableForConsultants()
		{
		   disableForConsultant = true;
		   disableConsultant = true;
		   waybillNoTextBox.ReadOnly= true;
           chkPOComm.Visible = false;
		}

        public void DisableForConsultants2()
        {
            
            chkPOComm.Visible = false;
        }
		
		public void DisableEverything()
		{
			disableEverything = true;
			waybillNoTextBox.ReadOnly= true;
            
		}

		
		


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void WaybillImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			
		    
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
		//for UPS
			if (waybillNoTextBox.Text != "")
			{
				string script = "<script language='javascript'>window.open('http://cricket.efundraisingcorp.com/addtrack.php?waybill=" + waybillNoTextBox.Text + "','Streaming', 'width=340, height=170, location=no, menubar=no, status=no, toolbar=no, scrollbars=no, resizable=no')</script>"; 
				Page.RegisterClientScriptBlock("Open", script); 
			}
		}


		#region GET/SET

		public int ConsulantID
		{
			get{return Convert.ToInt32(consultantDropDownList.SelectedValue);}
			set{
				cID = value;
				consultantDropDownList.SelectedValue = value.ToString();}
		}

		public int SaleID
		{

			get{
				if (saleIdTextBox.Text == "")
				{
                   return int.MinValue;
				}
				else
				{
					return Convert.ToInt32(saleIdTextBox.Text);
				}
				
			}
			set{saleIdTextBox.Text = value.ToString();}
		}

        public DropDownList PO_DDL
        {
            get { return this.poNoStatusDropdownlist; }
        }
        
        public int POStatusID
		{
			get{return Convert.ToInt16(poNoStatusDropdownlist.SelectedValue);}
		}

		public int CarrierID
		{

			get{return Convert.ToInt32(carrierDropdownlist.SelectedValue);}
		}

		public int ShipppingOptionID
		{
		   get{return Convert.ToInt32(shippingOptionDropdownlist.SelectedValue);}
		}

		public string WaybillNo
		{
			get{return waybillNoTextBox.Text;}
            set { waybillNoTextBox.Text = value; }
		}

        public string ExtConsultant
        {
            get { return extConsultantNnameTextBox.Text; }
            set { extConsultantNnameTextBox.Text = value; }
        }

        public int POConComm
        {
            get
            {
                if (chkPOComm.Checked == true)
                {
                    return 1;
                }
                else
                {
                    return 0;

                }

            }
            set
            {
                if (value == 1)
                {
                    chkPOComm.Checked = true;
                }
                else {
                    chkPOComm.Checked = false;
                }
            }
        }

		#endregion
	}
}

