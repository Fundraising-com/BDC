namespace EFundraisingCRMWeb.Components.User.PaymentInformation
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Components.Server;
    using efundraising.EFundraisingCRM;

	/// <summary>
	///		Summary description for PaymentOptions.
	/// </summary>
	public partial class PaymentOptions : System.Web.UI.UserControl
	{

		public event EventHandler ProcessCreditCard;
        
		private short paymentTermID = short.MinValue;
		private short paymentMethodID = short.MinValue;
		private short depositMethodID = short.MinValue;
		private bool disaleEverything = false;

        string country = "";
        int oeId = 0;

        public int clientId
        {
            get
            {
                try
                {
                     if (ViewState[Global.SessionVariables.CLIENT_ID] == null)
                        return int.MinValue;
                    return System.Convert.ToInt32(ViewState[Global.SessionVariables.CLIENT_ID]);
                }
                catch (Exception)
                {
                    return int.MinValue;
                }
            }
            set
            {
                ViewState[Global.SessionVariables.CLIENT_ID] = value;
            }

        }

        public string clientSeq
        {
            get
            {
                return ViewState[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();
            }
            set
            {
                ViewState[Global.SessionVariables.CLIENT_SEQUENCE_CODE] = value;
            }


        }
		protected void Page_Load(object sender, System.EventArgs e)
		{
            
			//check if client has a sale id and paid by credsit card
            PaymentOptions a = (PaymentOptions) paymentMethodDropDownList.NamingContainer;
            ContentPlaceHolder b = (ContentPlaceHolder )a.NamingContainer;
            TextBox txt = (TextBox) b.FindControl("OEIdTextBox");
            if (txt.Text != null && txt.Text != "")
            {
                oeId = Convert.ToInt32(txt.Text);
            }

            country = Session["Country"].ToString();     
                     

			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				SetPaymentMethodDropDownList(false);
				SetPaymentTermsDropDownList(false);
				
				if (disaleEverything)
				{
					ManageSaleScreen.MakeReadOnly(paymentMethodDropDownList);
					ManageSaleScreen.MakeReadOnly(PaymentTermDropdownList);
					ManageSaleScreen.MakeReadOnly(SaleDepositMethodDropdownList);
					SaleDepositPaymentTextBox.ReadOnly = true;
                    ProcessButton.Enabled = false;
				}
			}
		}

		private void SetPaymentMethodDropDownList(bool bReset)
		{
				
			foreach (PaymentMethod pm in Global.GetPaymentMethodCollections(Application))
			{
				paymentMethodDropDownList.Items.Add(new ListItem(pm.Description, pm.PaymentMethodId.ToString()));
				SaleDepositMethodDropdownList.Items.Add(new ListItem(pm.Description, pm.PaymentMethodId.ToString()));
			}
			SaleDepositMethodDropdownList.Items.Add(new ListItem("None", short.MinValue.ToString()));

			if (paymentMethodID == short.MinValue)
			{
				//get default value from webconfig
				paymentMethodID = Convert.ToInt16(efundraising.Configuration.ApplicationSettings.GetConfig()["PaymentMethodID", "default"]);
			}

            ProcessButton.Visible = false;
			if (paymentMethodID ==  2 || paymentMethodID ==  3 || paymentMethodID ==  8 || paymentMethodID == 9)
			{
                if (oeId > 0)
                {
                    ProcessButton.Visible = true;
                }
               
			}
			

			paymentMethodDropDownList.SelectedValue = paymentMethodID.ToString();
			SaleDepositMethodDropdownList.SelectedValue = depositMethodID.ToString();


		}

		private void SetPaymentTermsDropDownList(bool bReset)
		{
			if (PaymentTermDropdownList.Items.Count < 1 || bReset)
				PaymentTermDropdownList.Items.Clear();
			
			foreach (PaymentTerm pyt in Global.GetPaymentTermCollections(Application))
			{
				PaymentTermDropdownList.Items.Add(new ListItem(pyt.Description, pyt.PaymentTermId.ToString()));
			}
			
			if (paymentTermID == short.MinValue)
			{
				//get default value from webconfig
				paymentTermID = Convert.ToInt16(efundraising.Configuration.ApplicationSettings.GetConfig()["PaymentTermID", "default"]);
			}
			
			PaymentTermDropdownList.SelectedValue = paymentTermID.ToString();
			
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

		private void ProcessCreditCardLinkButton_Click(object sender, System.EventArgs e)
		{
		
		}


		public void SetInfo(Sale s)
		{
			
			paymentMethodID = s.PaymentMethodId;
		    paymentTermID = s.PaymentTermId;
			depositMethodID = s.UpfrontPaymentMethodId;
			if (s.UpfrontPaymentRequired == short.MinValue)
			{
				SaleDepositPaymentTextBox.Text = "0";
			} 
			else
			{
				SaleDepositPaymentTextBox.Text = s.UpfrontPaymentRequired.ToString(); 
			}
		}

		public void DisableForConsultants()
		{
			//nothing for now
		}

		public void DisableEverything()
		{
			disaleEverything = true;
		}

		private void ProcessImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			ProcessCC();
		}

		protected void paymentMethodDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int paymentMethodID = Convert.ToInt32(paymentMethodDropDownList.SelectedValue);
            ProcessButton.Visible = false;

            if (paymentMethodID ==  2 || paymentMethodID ==  3 || paymentMethodID ==  8 || paymentMethodID == 9)
			{
                //if client is US and order is in OE
                //if (country == "US" && oeId > 0)
                if (oeId > 0)
                {
                    ProcessButton.Visible = true;
                }
			}
			
			
		}

		private void SaveButton_Click(object sender, System.EventArgs e)
		{
		
		}

		private void ProcessLinkButton_Click(object sender, System.EventArgs e)
		{
			ProcessCC();
		}

		private void ProcessCC()
		{
			try
			{

				string msg = "";
				if (clientId == null)
				{
					msg = "client ID is empty";
				}
				if (clientSeq == null)
				{
					msg = msg + "client seq is empty";
				}

				/*string clientID = Session[Global.SessionVariables.CLIENT_ID].ToString();
				string clientSeqCode = Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();
				*/

				if (msg == "")
				{
					//string url = "../CreditCard/Default.aspx?clientId=" + clientID + "&clientSequenceCode=" + clientSeqCode;
                    string url = "../CreditCard/Default.aspx?clientId=" + clientId + "&clientSequenceCode=" + clientSeq;

					//opens new window
					string script = "<script language='javascript'>window.open('" + url + "','Streaming', 'width=700, height=600, location=no, menubar=no, status=no, toolbar=no, scrollbars=yes, resizable=yes')</script>"; 
					Page.RegisterClientScriptBlock("Open", script); 
				}
				else
				{
                  efundraising.Diagnostics.Logger.LogError("Sales Screen: Process CC " + msg);
				}
			}
			catch(Exception x)
			{
				efundraising.Diagnostics.Logger.LogError("Sales Screen: Process CC ",x);
						
			}

			
			
		}

		protected void ProcessButton_Click(object sender, System.EventArgs e)
		{
			ProcessCC();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("http://www.google.com");
		}



		#region GET/SET

		public short PaymentMethodID
		{
			get{return Convert.ToInt16(paymentMethodDropDownList.SelectedValue);}
			set
			{
				paymentMethodID = value;
				//paymentMethodDropDownList.SelectedValue = value.ToString()
				;}
		}
		public short PaymentTermID
		{
			get{
				if (PaymentTermDropdownList.SelectedValue == "")
				{
					return short.MinValue;
				}
				else
				{
					return Convert.ToInt16(PaymentTermDropdownList.SelectedValue);
				}
				
			}
			set
			{
				paymentTermID = value;
				PaymentTermDropdownList.SelectedValue = value.ToString();}
		}

		public short DepositMethodID
		{
			get{return Convert.ToInt16(SaleDepositMethodDropdownList.SelectedValue);}
			set
			{
				depositMethodID = value;
				SaleDepositMethodDropdownList.SelectedValue = value.ToString();}
		}

		public decimal SaleDepositPayment
		{
			get
			{
				if (SaleDepositPaymentTextBox.Text == "")
				{
					return 0;
				}
				else
				{
					return Convert.ToDecimal(SaleDepositPaymentTextBox.Text);
				}		;}
			set
			{
				if (value == int.MinValue)
				{
					SaleDepositPaymentTextBox.Text = "0";
			    } 
				else
	     		{
				   SaleDepositPaymentTextBox.Text = value.ToString();
				}
	        }
			
		}
		#endregion
	}
}
