namespace EFundraisingCRMWeb.Components.User.AddressHygiene
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
	using EfundraisingCRM.AddressHygiene.staging;

	//Delegate signifying user selected one of the addresses in the Suggestion List
	public delegate void OutputAddressEventHandler(object sender, OutputAddress outputAddress, bool addressChanged);
    

	/// <summary>
	/// Address Validate Control
	/// </summary>
	/// <remarks>
	/// Created: June 20, 2007
	/// </remarks>
	public class AddressHygiene : System.Web.UI.UserControl
	{
		#region Fields

		private const int address1Column = 0;
		private const int address2Column = 1;
		private const int cityColumn = 2;
		private const int countyColumn = 3;
		private const int regionColumn = 4;
		private const int postCodeColumn = 5;
		private const int postCode2Column = 6;
		private const int countryColumn = 7;

		protected System.Web.UI.WebControls.DataGrid SuggestionListDataGrid;
		protected System.Data.DataView suggestionListDataView;
		protected AddressHygieneContract addressHygieneContract;

		#endregion

		#region Events
		//Event signifying address has been returned
		public event OutputAddressEventHandler OutputAddress;
		#endregion

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
			this.SuggestionListDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.SuggestionListDataGrid_ItemCommand);
			this.SuggestionListDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.SuggestionListDataGrid_PageIndexChanged);
			this.SuggestionListDataGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.SuggestionListDataGrid_Sort);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		#region Properties

		//The address information returned from the Address Hygiene Web Service
		public OutputAddress OutAddress
		{
			get 
			{
				if (ViewState["OutputAddress"] == null)
					return new OutputAddress();

				return (OutputAddress)ViewState["OutputAddress"];
			}
			set 
			{
				ViewState["OutputAddress"] = value;
			}
		}

        public bool IsBilling
        {
            get
            {
                if (ViewState["IsBilling"] == null)
                    return false;

                return Convert.ToBoolean(ViewState["IsBilling"]);
            }
            set
            {
                ViewState["IsBilling"] = value;
            }
        }

		private DataView SuggestionListDataView
		{
			get
			{
				if (suggestionListDataView == null)
					suggestionListDataView = ConvertSuggestionListToDataView(OutAddress.SuggestionList);

				return suggestionListDataView;
			}
			set
			{
				suggestionListDataView = value;
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

		#region Methods
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
			ResetData();

			//Instantiate Address Hygiene Web Service Classes
			addressHygieneContract = new AddressHygieneContract();
			AddressHygieneSingleMessageRequest addressHygieneSingleMessageRequest = new AddressHygieneSingleMessageRequest();

			//Initialize Address Hygiene Settings
			addressHygieneSingleMessageRequest.EnableSuggestionList = enableSuggestionList;
			addressHygieneSingleMessageRequest.Address = address;
			addressHygieneContract.CookieContainer = CookieContain;

			//Login to Address Hygiene Web Service
			if (Login())
			{
				//Perform Address Hygiene
				OutAddress = addressHygieneContract.HygieneAddress(addressHygieneSingleMessageRequest).OutputAddress;
				ParseOutputAddress();
				return true;
			}
			else
				return false;
		}

        public bool DoAddressHygieneNoDelagate(Address address, bool enableSuggestionList, ref bool addressChanged, bool isBilling)
        {
            try
            {
                IsBilling = isBilling;
                int err = 0;

                ResetData();

                //Instantiate Address Hygiene Web Service Classes
                addressHygieneContract = new AddressHygieneContract();
                AddressHygieneSingleMessageRequest addressHygieneSingleMessageRequest = new AddressHygieneSingleMessageRequest();

                //Initialize Address Hygiene Settings
                addressHygieneSingleMessageRequest.EnableSuggestionList = enableSuggestionList;
                addressHygieneSingleMessageRequest.Address = address;
                addressHygieneContract.CookieContainer = CookieContain;

                //Login to Address Hygiene Web Service
                if (Login())
                {

                    //Perform Address Hygiene
                    OutAddress = addressHygieneContract.HygieneAddress(addressHygieneSingleMessageRequest).OutputAddress;


                    //////////////////////////
                    //Store whether or not the initial address has been changed


                    AddressChanged = AddressChanged || OutAddress.Status.FormatChangeStatus != FormatChangeStatus.None;
                    if (!AddressChanged)
                    {
                        if (OutAddress.Status.ChangeStatus != ChangeStatus.County && OutAddress.Status.ChangeStatus != ChangeStatus.None)
                        {
                            AddressChanged = true;
                        }
                    }


                    //If Address Hygiene returned a suggestion list, display it; if not, return results
                    if (OutAddress.SuggestionList.Length == 0)
                    {
                        addressChanged = AddressChanged;

                        SuggestionListDataGrid.Visible = false;
                        AddressChanged = false;
                    }
                    else
                    {
                        DisplaySuggestionList();
                    }
                    ///////////////////////////////////

                    return true;
                }
                else
                    return false;
            }
            catch (Exception x)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: Address Hygiene error", x);
                return false;
            }
        }


        public bool DoAddressDoubleHygieneNoDelagate(Address billAddress, Address shipAddress, bool enableSuggestionList, ref bool addressChanged)
        {/*
            ResetData();

            //Instantiate Address Hygiene Web Service Classes
            addressHygieneContract = new AddressHygieneContract();
            AddressHygieneMessageRequest addressHygieneMessageRequest = new AddressHygieneMessageRequest();

            //Initialize Address Hygiene Settings
            addressHygieneMessageRequest.EnableSuggestionList = enableSuggestionList;
            addressHygieneMessageRequest.Addresses = new Address[2];
            addressHygieneMessageRequest.Addresses[0] = billAddress;
            addressHygieneMessageRequest.Addresses[1] = shipAddress;

            addressHygieneContract.CookieContainer = CookieContain;

            //Login to Address Hygiene Web Service
            if (Login())
            {
                //Perform Address Hygiene
                AddressHygieneMessageResponse response = addressHygieneContract.HygieneAddresses(addressHygieneMessageRequest);
                OutAddress = response.OutputAddresses[0];
                OutAddress2 = response.OutputAddresses[1];


                //////////////////////////
                //Store whether or not the initial address has been changed
                AddressChanged = AddressChanged || OutAddress.Status.ChangeStatus != ChangeStatus.None || OutAddress.Status.FormatChangeStatus != FormatChangeStatus.None;

                //If Address Hygiene returned a suggestion list, display it; if not, return results
                if (OutAddress.SuggestionList.Length == 0)
                {
                    addressChanged = AddressChanged;
                    SuggestionListDataGrid.Visible = false;
                    AddressChanged = false;
                }else if (OutAddress2.SuggestionList.Length == 0)
                {
                    addressChanged = AddressChanged;
                    SuggestionListDataGrid.Visible = false;
                    AddressChanged = false;
                }
                else
                {
                    DisplaySuggestionList();
                }
                ///////////////////////////////////

                return true;
            }
            else*/
                return false;
        }

		private void ResetData()
		{
			OutAddress = null;
			SuggestionListDataView = null;
			SuggestionListDataGrid.DataSource = null;
			SuggestionListDataGrid.Visible = false;
			SuggestionListDataGrid.SelectedIndex = -1;
			SuggestionListDataGrid.CurrentPageIndex = 0;
		}

		private void ParseOutputAddress()
		{
			//Store whether or not the initial address has been changed
			AddressChanged = AddressChanged || OutAddress.Status.ChangeStatus != ChangeStatus.None || OutAddress.Status.FormatChangeStatus != FormatChangeStatus.None;

			//If Address Hygiene returned a suggestion list, display it; if not, return results
			if (OutAddress.SuggestionList.Length == 0)
			{
				OutputAddress(this, OutAddress, AddressChanged);
				SuggestionListDataGrid.Visible = false;
				AddressChanged = false;
			}
			else
			{
				DisplaySuggestionList();
			}
		}




      

		private void DisplaySuggestionList()
		{
			SuggestionListDataGrid.DataSource = SuggestionListDataView;

			//Filter/Rename Columns depending on Address Country
			if (OutAddress.SuggestionList[0].Country == "CA" || OutAddress.SuggestionList[0].Country == "CANADA")
			{
				SuggestionListDataGrid.Columns[regionColumn].HeaderText = "Province";
				SuggestionListDataGrid.Columns[postCodeColumn].HeaderText = "Postal Code";
				SuggestionListDataGrid.Columns[postCode2Column].Visible = false;
				SuggestionListDataGrid.Columns[countyColumn].Visible = false;
			}

			SuggestionListDataGrid.Visible = true;
			SuggestionListDataGrid.DataBind();
		}

		private DataView ConvertSuggestionListToDataView(Address[] suggestionList)
		{
			DataView dv = new DataView(ConvertSuggestionListToDataTable(suggestionList));
			return dv;
		}

		private DataTable ConvertSuggestionListToDataTable(Address[] suggestionList)
		{
			DataTable dt = new DataTable();
			dt.TableName = "SuggestionList";

			DataRow dr;

			dt.Columns.Add("Address1", typeof(System.String));
			dt.Columns.Add("Address2", typeof(System.String));
			dt.Columns.Add("City", typeof(System.String));
			dt.Columns.Add("County", typeof(System.String));
			dt.Columns.Add("Region", typeof(System.String));
			dt.Columns.Add("PostCode", typeof(System.String));
			dt.Columns.Add("PostCode2", typeof(System.String));
			dt.Columns.Add("Country", typeof(System.String));

			for(int row = 0; row < suggestionList.Length; row++)
			{
				dr = dt.NewRow();
				dr[0] = suggestionList[row].Address1;
				dr[1] = suggestionList[row].Address2;
				dr[2] = suggestionList[row].City;
				dr[3] = suggestionList[row].County;
				dr[4] = suggestionList[row].Region;
				dr[5] = suggestionList[row].PostCode;
				dr[6] = suggestionList[row].PostCode2;
				dr[7] = suggestionList[row].Country;
				dt.Rows.Add(dr);
			}

			return dt;
		}

		private void SuggestionListDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Select")
			{
				//User selected an address from the Suggestion List. Rehygiene with this address as the initial address
				SetInputAddressFromDataGridItem(e.Item);
				AddressChanged = true;
                bool addressChanged = true;
                OutputAddress(this, OutAddress, AddressChanged);
                SuggestionListDataGrid.Visible = false;

				//DoAddressHygiene(OutAddress, true, ref addressChanged,false);
			}
		}

		private void SetInputAddressFromDataGridItem(DataGridItem e) 
		{
			OutAddress.Address1 = e.Cells[address1Column].Text.Replace("&nbsp;", "");
            OutAddress.Address2 = e.Cells[address2Column].Text.Replace("&nbsp;", "");
            OutAddress.City = e.Cells[cityColumn].Text.Replace("&nbsp;", "");
            OutAddress.County = e.Cells[countyColumn].Text.Replace("&nbsp;", "");
            OutAddress.Region = e.Cells[regionColumn].Text.Replace("&nbsp;", "");
            OutAddress.PostCode = e.Cells[postCodeColumn].Text.Replace("&nbsp;", "");
            OutAddress.PostCode2 = e.Cells[postCode2Column].Text.Replace("&nbsp;", "");
            OutAddress.Country = e.Cells[countryColumn].Text.Replace("&nbsp;", "");
		}

		private void SuggestionListDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			SuggestionListDataGrid.SelectedIndex = -1;
			SuggestionListDataGrid.CurrentPageIndex = e.NewPageIndex;
			this.DisplaySuggestionList();
		}

		private void SuggestionListDataGrid_Sort(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			SuggestionListDataGrid.SelectedIndex = -1;

			if(CurrentSortAscending && CurrentSortExpression == e.SortExpression)
			{
				SuggestionListDataView.Sort = e.SortExpression + " " + "DESC";
				CurrentSortAscending = false;
			}
			else
			{
				SuggestionListDataView.Sort = e.SortExpression + " " + "ASC";
				CurrentSortAscending = true;
			}

			CurrentSortExpression = e.SortExpression;

			this.DisplaySuggestionList();
		}

		public string GetErrorFromFault(string Fault)
		{
			Components.User.AddressHygiene. AddressHygieneErrorCodes addressHygieneErrorCodes = new AddressHygieneErrorCodes();
			string error = addressHygieneErrorCodes[Fault].ToString();
			if (error == null)
				error = "Unknown error";
			return error;
		}
		#endregion
	}
}
