namespace EFundraisingCRMWeb.Components.User.Sales
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Components.Server;
    using efundraising.EFundraisingCRM;
    using System.Text;
    using System.Web.UI;



	/// <summary>
	///		Summary description for Status.
	/// </summary>
	public partial class Status : System.Web.UI.UserControl
	{

       // protected Components.User.Sales.SaleInfo SaleInfo1;
        private int saleStatusID;
		private bool disableForConsultant = false;
		private bool disableEverything = false;
        private bool enableSaleSataus = false;

       // private int SaleStatusddl;
        
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

            //SaleStatusddl = Convert.ToInt16(saleStatusDropDownList.SelectedIndex);
            if (!IsPostBack)
			{

             

                SetSaleStatus(false);
				SetProductionStatus(false);
				SetARStatus(false);
				SetCreditCheckStatus(false);

                SaleStatusddl = Convert.ToInt16(saleStatusDropDownList.SelectedIndex);
			
				if (disableEverything)
				{
					ManageSaleScreen.MakeReadOnly(saleStatusDropDownList);
					ManageSaleScreen.MakeReadOnly(arStatusDropdownlist);
					ManageSaleScreen.MakeReadOnly(productStatusDropdownlist);
					ManageSaleScreen.MakeReadOnly(creditStatusDropdownlist);

				}else if (disableForConsultant)
				{
					ManageSaleScreen.MakeReadOnly(saleStatusDropDownList);
					ManageSaleScreen.MakeReadOnly(arStatusDropdownlist);
					ManageSaleScreen.MakeReadOnly(productStatusDropdownlist);
					ManageSaleScreen.MakeReadOnly(creditStatusDropdownlist);
				}
                
                if (enableSaleSataus)
                {
                    ManageSaleScreen.RemoveReadOnly(saleStatusDropDownList);
                }
			}

			
		}

		#region Private Properties and Methods		
		
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int poStatusID = 0;

             
            
            SaleInfo postat = (SaleInfo)this.NamingContainer.FindControl("SaleInfo1");
            if (postat != null)
            {
                poStatusID = postat.POStatusID;
                //SaleStatusddl = saleStatusDropDownList.SelectedIndex;
            }
            else 
            {
                throw new Exception("Cannot find sales info usercontrol");
            }

            if (poStatusID == Int16.MinValue && saleStatusDropDownList.SelectedValue == "2")
            {
                string script = "alert('Please Select PO Status before Confirming Sale');"; Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                saleStatusDropDownList.SelectedIndex = SaleStatusddl;           
            }
        }
       
        
        
        
        private void SetSaleStatus(bool bReset)
		{
			if (bReset || saleStatusDropDownList.Items.Count < 1)
			{
				saleStatusDropDownList.Items.Clear();
				SalesStatusCollection ssColl = Global.GetSalesStatusCollection(Application);

				foreach (SalesStatus ss in ssColl)
				{
					saleStatusDropDownList.Items.Add(new ListItem(ss.Description, ss.SalesStatusID.ToString()));
				}
				saleStatusDropDownList.SelectedValue = "1";
			}
		}

		
		private void SetProductionStatus(bool bReset)
		{
			if (bReset || productStatusDropdownlist.Items.Count < 1)
			{
				productStatusDropdownlist.Items.Clear();
				ProductionStatusCollection pdColl = Global.GetProductionStatusCollection(Application);
				foreach (ProductionStatus pds in pdColl)
				{
					productStatusDropdownlist.Items.Add(new ListItem(pds.Description, pds.ProductionStatusID.ToString()));
				}
				productStatusDropdownlist.SelectedValue = "1";
			}
		}

		
		private void SetARStatus(bool bReset)
		{
			if (bReset || arStatusDropdownlist.Items.Count < 1)
			{
				arStatusDropdownlist.Items.Clear();
				ARStatusCollection arColl = Global.GetARStatusCollection(Application);

				foreach (ARStatus theobj in arColl)
				{
					arStatusDropdownlist.Items.Add(new ListItem(theobj.Description, theobj.ARStatusID.ToString()));
				}
				arStatusDropdownlist.SelectedValue = "21";

			}
		}

		
		private void SetCreditCheckStatus(bool bReset)
		{
			if (bReset || creditStatusDropdownlist.Items.Count < 1)
			{
				creditStatusDropdownlist.Items.Clear();
				CreditCheckStatusCollection ccColl = Global.GetCreditCheckStatusCollection(Application);
				creditStatusDropdownlist.Items.Add(new ListItem("None", int.MinValue.ToString()));
				foreach (CreditCheckStatus theobj in ccColl)
				{
					creditStatusDropdownlist.Items.Add(new ListItem(theobj.Description, theobj.CreditCheckStatusID.ToString()));
				}
			}
		}
		#endregion

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

		public void SetInfo(Sale s, int leadID)
		{
            
			SetSaleStatus(false);
			SetProductionStatus(false);
			SetARStatus(false);
			SetCreditCheckStatus(false);
			if (s != null)
			{
				SetSelectedValue(s.SalesStatusId.ToString(), saleStatusDropDownList);
				SetSelectedValue(s.ArStatusId.ToString(), arStatusDropdownlist);
				SetSelectedValue(s.ProductionStatusId.ToString(), productStatusDropdownlist);
                //get the last credit check from credit cehck request table
                efundraising.EFundraisingCRM.CreditCheckRequest ccs = efundraising.EFundraisingCRM.CreditCheckRequest.GetCreditCheckRequestByLeadIDLast(leadID);
                if (ccs != null)
                {
                    if (ccs.CreditStatusID != null)
                    {
                        SetSelectedValue(ccs.CreditStatusID.ToString(), creditStatusDropdownlist);
                    }
                }
                
                
				
			}
		}

		public void DisableForConsultants()
		{
		   disableForConsultant = true;
			
		}

		public void DisableEverything()
		{

			disableEverything = true;

		}

        public void EnableCancelStatus()
        {
            enableSaleSataus= true;
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

		#region GET/SET

		public int SaleStatusID
		{
			get
			{
				if (saleStatusDropDownList.SelectedValue == "")
				{
					return int.MinValue;
				}
				else
				{
					return Convert.ToInt32(saleStatusDropDownList.SelectedValue);
				}
				
			}
		}

		public int ProductionStatusID
		{
			get{return Convert.ToInt32(productStatusDropdownlist.SelectedValue);}
			set{productStatusDropdownlist.SelectedValue = value.ToString();} 
		}

		public int ArStatusID
		{
			get{return Convert.ToInt32(arStatusDropdownlist.SelectedValue);}
		}

		public int CreditStatusID
		{
			get{return Convert.ToInt32(creditStatusDropdownlist.SelectedValue);}
		}

        protected int SaleStatusddl
        {
            get
            {
                if (ViewState["SaleStatusddl"] != null)
                    return Convert.ToInt32(ViewState["SaleStatusddl"]);
                else
                    return 1;
            }
            set
            {
                ViewState["SaleStatusddl"] = value;
            }
        }



		#endregion
	}
}
