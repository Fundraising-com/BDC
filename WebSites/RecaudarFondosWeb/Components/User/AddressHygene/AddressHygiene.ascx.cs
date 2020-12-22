namespace efundraising.RecaudarFondosWeb.Components.User.AddressHygiene
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Text;
	using System.Resources;
	using System.Collections;
	using System.Net;
    using GA.BDC.Core.AddressHygiene.com.qsp.AddressHygiene;
  
    //Delegate signifying user selected one of the addresses in the Suggestion List
    public delegate void OutputAddressEventHandler(object sender, OutputAddress outputAddress, bool addressChanged, int nAddress);

	/// <summary>
	/// Address Validate Control
	/// </summary>
	/// <remarks>
	/// Created: June 20, 2007
	/// </remarks>
	public class AddressHygiene : System.Web.UI.UserControl
	{
		protected AddressHygieneContract addressHygieneContract;

		//Event signifying user selected one of the addresses in the Suggestion List
		public event OutputAddressEventHandler OutputAddress;

		#region Fields

		//The address information returned from the Address Hygiene Web Service
		public OutputAddress OutAddress
		{
			get 
			{
				if (Session["OutputAddress"] == null)
					return new OutputAddress();

				return (OutputAddress)Session["OutputAddress"];
			}
			set 
			{
				Session["OutputAddress"] = value;
			}
		}

        private OutputAddress[] OutAddresses
        {
            get
            {
                if (ViewState["OutputAddresses"] == null)
                    return new OutputAddress[] { new OutputAddress() };

                return (OutputAddress[])ViewState["OutputAddresses"];
            }
            set
            {
                ViewState["OutputAddresses"] = value;
            }
        }




		//Whether or not the OutAddress is different than the original Address
		private bool AddressChanged
		{
			get 
			{
				if (ViewState["AddressChanged"] == null)
					return false;

				return (bool)ViewState["AddressChanged"];
			}
			set 
			{
				ViewState["AddressChanged"] = value;
			}
		}				

		//Whether logged into the Address Hygiene Web Service
		private bool IsLoggedIn
		{
			get 
			{
				if (ViewState["IsLoggedIn"] == null)
					return false;

				return (bool)ViewState["IsLoggedIn"];
			}
			set 
			{
				ViewState["IsLoggedIn"] = value;
			}
		}

		//Persists the login across Address Hygiene requests
		private CookieContainer CookieContain
		{
			get 
			{
				if (ViewState["CookieContainer"] == null)
					ViewState["CookieContainer"] = new CookieContainer();

				return (CookieContainer)ViewState["CookieContainer"];
			}
			set 
			{
				ViewState["IsLoggedIn"] = value;
			}
		}

		private String CurrentSortExpression
		{
			get 
			{
				if (ViewState["CurrentSortExpression"] == null)
					return String.Empty;

				return (String)ViewState["CurrentSortExpression"];
			}
			set 
			{
				ViewState["CurrentSortExpression"] = value;
			}
		}

		private bool CurrentSortAscending
		{
			get 
			{
				if (ViewState["CurrentSortDescending"] == null)
					return false;

				return (bool)ViewState["CurrentSortDescending"];
			}
			set 
			{
				ViewState["CurrentSortDescending"] = value;
			}
		}

		#endregion

        public bool IsAddressHygieneEnabled
        {
            get
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["AddressHygieneEnabled"].ToLower() == "true")
                    return true;
                else
                    return false;
            }
        }


        private void Page_Load(object sender, System.EventArgs e)
        {
        }

		public bool Login()
		{
			if (!IsLoggedIn)
			{
				LoginMessageRequest loginMessageRequest = new LoginMessageRequest();
				loginMessageRequest.UserName = System.Configuration.ConfigurationSettings.AppSettings["AddressHygieneUsername"];
				loginMessageRequest.Password = System.Configuration.ConfigurationSettings.AppSettings["AddressHygienePassword"];
				if (addressHygieneContract.Login(loginMessageRequest))
					IsLoggedIn = true;
			}
			return IsLoggedIn;
		}


        public bool DoAddressHygiene(Address address, bool enableSuggestionList)
        {
            return DoAddressHygiene(address);
        }



		public bool DoAddressHygiene(Address address)
        {
			ResetData();

			//Instantiate Address Hygiene Web Service Classes
            addressHygieneContract = new AddressHygieneContract();
            addressHygieneContract.Url = System.Configuration.ConfigurationSettings.AppSettings["AddressHygieneURL"];
            AddressHygieneSingleMessageRequest addressHygieneSingleMessageRequest = new AddressHygieneSingleMessageRequest();

			//Initialize Address Hygiene Settings
            addressHygieneSingleMessageRequest.EnableSuggestionList = false; //enableSuggestionList;
            addressHygieneSingleMessageRequest.Address = address;
            addressHygieneContract.CookieContainer = CookieContain;

			//Login to Address Hygiene Web Service
			if (Login())
			{
				//Perform Address Hygiene
				OutAddress = addressHygieneContract.HygieneAddress(addressHygieneSingleMessageRequest).OutputAddress;
				ParseOutputAddress(0);
				return true;
			}
			else
				return false;
		}

		private void ResetData()
		{
            OutAddress = null;
            OutAddresses = null;
		}

        private void ParseOutputAddress(int nAddress)
		{
			//Store whether or not the initial address has been changed
            AddressChanged = AddressChanged || OutAddress.Status.ChangeStatus != ChangeStatus.None || OutAddress.Status.FormatChangeStatus != FormatChangeStatus.None;

            //If Address Hygiene returned a suggestion list, display it; if not, return results
            OutputAddress(this, OutAddress, AddressChanged, nAddress);
            AddressChanged = false;
		}

		
	}
}
