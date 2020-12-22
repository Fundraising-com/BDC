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
//using EFundraisingCRMWeb.App_Data;

namespace EFundraisingCRMWeb.Components.User
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using efundraising.EFundraisingCRM;

    /// <summary>
    ///		Summary description for Address.
    /// </summary>
    public partial class Address : System.Web.UI.UserControl, ICloneable
    {

        public event EventHandler eventSetAsBillingAddress;

        private string state = "AL";
        private string country = "US";
        private int zone = 3;


        /*public System.Web.UI.WebControls.CheckBox setAsBillingAddressCheckBox
        {
            get
            {
                return SetAsBillingAddressCheckBox;
            }
        }
*/
        
        public void Refresh()
        {
            StreetAddress = this.StreetAddress;
        }

        #region ICloneable Members

        public object Clone()
        {
           // return this.MemberwiseClone();

            ClientAddress ca = new ClientAddress();
            ca.AddressZoneId = Zone;

            return ca;

        }

        #endregion


        protected void Page_Load(object sender, System.EventArgs e)
        {
            AttnOfValidator.Visible = false;
            AddressValidator.Visible = false;
            ZipValidator.Visible = false;
            CityValidator.Visible = false;
            errorLabel.Visible = false;
            LocationValidator.Visible = false;


            // Put user code to initialize the page here
            if (!IsPostBack)
            {
                SetCountryDropDown();
                SetAddressZone(ZoneDropDownList);

                try
                {
                    CountryDropDownList.SelectedValue = country;
                }
                catch
                {
                    errorLabel.Text = "Country (" + country + ") is incorrect. ";
                    errorLabel.Visible = true;
                }


                SetCountryStateDropDown();
                try
                {                   
                    StateDropDownList.SelectedValue = state.ToUpper();
                }
                catch
                {
                    errorLabel.Text = "State (" + state + ") is incorrect. ";
                    errorLabel.Visible = true;
                }


                if (zone > 3 || zone < 1) //saw 0 in the database !!
                {
                    ZoneDropDownList.SelectedValue = "3";
                }
                else
                {
                    ZoneDropDownList.SelectedValue = zone.ToString();
                }
            }


        }

        public void RefreshDropDowns()
        {
            //ZoneDropDownList.SelectedValue = zone.ToString();
            CountryDropDownList.SelectedValue = country;
            StateDropDownList.SelectedValue = state;
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            //this.SetAsBillingAddressCheckBox.CheckedChanged +=new EventHandler(SetAsBillingAddressCheckBox_CheckedChanged);

            this.CountryDropDownList.SelectedIndexChanged += new EventHandler(CountryDropDownList_SelectedIndexChanged);
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

        public void SetClientShippingAddress(ClientAddress clAddress)
        {
            SetAddressZone(ZoneDropDownList);
            if (clAddress != null)
            {
        
                AddressTextBox.Text = IsNullReturnEmpty(clAddress.StreetAddress);
                CityTextBox.Text = IsNullReturnEmpty(clAddress.City);
                if (clAddress.ZipCode != null)
                {
                    string[] zip = clAddress.ZipCode.Split('-');
                    Zip1TextBox.Text = zip[0];
                    if (zip.Length > 1)
                    {
                        Zip2TextBox.Text = zip[1];
                    }
                }

                country = IsNullReturnEmpty(clAddress.CountryCode);
                LocationTextBox.Text = IsNullReturnEmpty(clAddress.Location);
                AttentionOfTextBox.Text = IsNullReturnEmpty(clAddress.AttentionOf);
                zone = Convert.ToInt32(clAddress.AddressZoneId);
                ZoneDropDownList.SelectedValue = zone.ToString();
                //ZoneDropDownList.SelectedItem.Value = zone.ToString();
                SetCountryStateDropDown();
                try
                {
                  state = IsNullReturnEmpty(clAddress.StateCode);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                AddressTextBox.Text = string.Empty;
                CityTextBox.Text = string.Empty;
                LocationTextBox.Text = string.Empty;
                Zip1TextBox.Text = string.Empty;
                Zip2TextBox.Text = string.Empty;
                AttentionOfTextBox.Text = string.Empty;
                ZoneDropDownList.SelectedValue = "3";

            }



        }

        public void SetClientShippingAddress(Address clAddress)
        {
            SetAddressZone(ZoneDropDownList);
            if (clAddress != null)
            {

                AddressTextBox.Text = IsNullReturnEmpty(clAddress.StreetAddress);
                CityTextBox.Text = IsNullReturnEmpty(clAddress.City);
                string[] zip = clAddress.Zip.Split('-');
                Zip1TextBox.Text = zip[0];
                if (zip.Length > 1)
                {
                    Zip2TextBox.Text = zip[1];
                }

                country = IsNullReturnEmpty(clAddress.Country);
                LocationTextBox.Text = IsNullReturnEmpty(clAddress.Location);
                AttentionOfTextBox.Text = IsNullReturnEmpty(clAddress.AttentionOf);
                zone = Convert.ToInt32(clAddress.Zone);
                //ZoneDropDownList.SelectedItem.Value = zone.ToString();

                SetCountryStateDropDown();
                try
                {
                    state = IsNullReturnEmpty(clAddress.State);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                AddressTextBox.Text = string.Empty;
                CityTextBox.Text = string.Empty;
                LocationTextBox.Text = string.Empty;
                Zip1TextBox.Text = string.Empty;
                Zip2TextBox.Text = string.Empty;
                AttentionOfTextBox.Text = string.Empty;
                ZoneDropDownList.SelectedValue = "3";

            }



        }


        public void SetClientBillingAddress(ClientAddress clAddress)
        {
            SetAddressZone(ZoneDropDownList);
            if (clAddress != null)
            {
                AddressTextBox.Text = IsNullReturnEmpty(clAddress.StreetAddress);
                CityTextBox.Text = IsNullReturnEmpty(clAddress.City);


                int pos = clAddress.ZipCode.IndexOf("-");
                if (pos > -1)
                {
                    Zip1TextBox.Text = clAddress.ZipCode.Substring(0, pos);
                    Zip2TextBox.Text = clAddress.ZipCode.Substring(pos + 1);

                }
                else
                {
                    Zip1TextBox.Text = IsNullReturnEmpty(clAddress.ZipCode);
                }




                country = IsNullReturnEmpty(clAddress.CountryCode);
                zone = Convert.ToInt32(clAddress.AddressZoneId.ToString());
                SetCountryStateDropDown();

                try
                {
                    state = IsNullReturnEmpty(clAddress.StateCode);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                CityTextBox.Text = string.Empty;
                Zip1TextBox.Text = string.Empty;
                Zip2TextBox.Text = string.Empty;
                AttentionOfTextBox.Text = string.Empty;
                //ZoneDropDownList.SelectedValue = "3";
            }
        }

        public void SetClientShipingInfo(Client cl)
        {
            if (cl == null)
                return;
            // Set client Billing Address
            ClientAddress clAddress = cl.ClientShippingAddress;
            SetClientShippingAddress(clAddress);

        }

        public void SetClientShipingInfo(Address a)
        {
            if (a == null)
                return;
            
            SetClientShippingAddress(a);

        }

        public void SetClientShipingInfo(ClientAddress a)
        {
            if (a == null)
                return;

            SetClientShippingAddress(a);

        }

        public void SetClientBillingInfo(Client cl)
        {
            if (cl == null)
                return;
            // Set client Billing Address
            ClientAddress clAddress = cl.ClientBillingAddress;
            //SetClientBillingAddress(clAddress);
            SetClientBillingAddress(clAddress);

        }


        

        private string IsNullReturnEmpty(string sValue)
        {
            if (sValue == null)
                return string.Empty;
            return sValue.Trim();
        }


        private void SetAddressZone(DropDownList ddL)
        {
            if (ddL.Items.Count == 0)
            {
                efundraising.EFundraisingCRM.AddressZone[] addZone = efundraising.EFundraisingCRM.AddressZone.GetAddressZones();
                for (int i = 0; i < addZone.Length; i++)
                {
                    ddL.Items.Add(new ListItem(addZone[i].Description, addZone[i].AddressZoneId.ToString().Trim()));
                }
            }
        }


        public void SetCountryStateDropDown()
        {
            if (StateDropDownList.Items.Count > 0)
                return;
            CountryCollections countryColl = (CountryCollections)Application[Global.shippingCountriesKey];
            StateDropDownList.Items.Clear();
            StateDropDownList.DataValueField = "StateCode";
            StateDropDownList.DataTextField = "StateName";
            Country c = countryColl[CountryDropDownList.SelectedValue];
            if (c != null)
            {
                StateDropDownList.DataSource = c.CountryStates;
                StateDropDownList.SelectedValue = null; //to fix lead 640159
                StateDropDownList.DataBind();
            }


        }

        public void SetNewCountryStateDropDown(string country)
        {
            CountryCollections countryColl = (CountryCollections)Application[Global.shippingCountriesKey];
            StateDropDownList.Items.Clear();
            StateDropDownList.DataValueField = "StateCode";
            StateDropDownList.DataTextField = "StateName";
            Country c = countryColl[country];
            if (c != null)
            {
                StateDropDownList.DataSource = c.CountryStates;
                StateDropDownList.DataBind();
            }
        }



        public void SetCountryDropDown()
        {
            if (CountryDropDownList.Items.Count > 0)
                return;

            CountryCollections countryColl = (CountryCollections)Application[Global.shippingCountriesKey];
            CountryDropDownList.Items.Clear();
            CountryDropDownList.DataValueField = "CountryCode";
            CountryDropDownList.DataTextField = "CountryName";

            CountryDropDownList.DataSource = countryColl;
            CountryDropDownList.DataBind();

        }



        public ClientAddress GetClientAddress()
        {
            ClientAddress clAddress = new ClientAddress(int.MinValue);
            clAddress.StreetAddress = AddressTextBox.Text.Trim();
            clAddress.City = CityTextBox.Text.Trim();
            clAddress.ZipCode = Zip2TextBox.Text.Trim();
            clAddress.StateCode = StateDropDownList.SelectedValue;
            clAddress.CountryCode = CountryDropDownList.SelectedValue;
            clAddress.AttentionOf = AttentionOfTextBox.Text.Trim();
            clAddress.AddressZoneId = int.Parse(ZoneDropDownList.SelectedValue);
            clAddress.Location = LocationTextBox.Text.Trim();
            return clAddress;
        }

        //for billing, sont need same as
        public bool IsAddressValid(bool shipping)
        {
            return IsAddressValid(shipping, false);
        }



        public bool IsAddressValid(bool shipping, bool sameAs)
        {
            bool valid = true;

            if (shipping)
            {
                if (AttentionOf.Trim() == "")
                {
                    AttnOfValidator.Visible = true;
                    valid = false;
                }

                if (Location.Trim() == "")
                {
                    LocationValidator.Visible = true;
                    valid = false;
                }
            }

            //we dont want to validate the shipping address if the shipping is same as billing
            if (!(shipping) || (shipping && !(sameAs)))
            {
                if (StreetAddress.Trim() == "")
                {
                    AddressValidator.Visible = true;
                    valid = false;
                }
                if (City.Trim() == "")
                {
                    CityValidator.Visible = true;
                    valid = false;
                }
                if (Zip.Trim() == "" || Zip.Length < 5)
                {
                    ZipValidator.Visible = true;
                    valid = false;
                }
            }

            return valid;
        }

        public void PrintAddressError(bool bt)
        {
            if (bt)
            {
                errorLabel.Text = "The address is missing information.";
                errorLabel.Visible = true;
            }
            else
            {
                errorLabel.Text = "The address is missing information.";
                errorLabel.Visible = true;
            }
        }

        public void SetControlAsShipping(bool shipping)
        {
            if (shipping)
            {
                LocationLabel.Visible = true;
                LocationTextBox.Visible = true;
                AttentionOfTextBox.Visible = true;
                AttentionOfLabel.Visible = true;
                ZoneDropDownList.Visible = true;
                ZoneLabel.Visible = true;


            }
        }

        public void EnableLocation(bool enable)
        {
            LocationTextBox.Enabled = enable;
        }

        public void EnableAttnOf(bool enable)
        {
            AttentionOfTextBox.Enabled = enable;
        }
        public void Enable(bool enable)
        {
            AddressTextBox.Enabled = enable;
            CityTextBox.Enabled = enable;
            Zip1TextBox.Enabled = enable;
            Zip2TextBox.Enabled = enable;
            StateDropDownList.Enabled = enable;
            CountryDropDownList.Enabled = enable;
            ZoneDropDownList.Enabled = enable;
        }



        #region GET/SET

        public string AttentionOf
        {
            set { AttentionOfTextBox.Text = value; }
            get { return AttentionOfTextBox.Text.Trim(); }
        }

        public string StreetAddress
        {
            set { AddressTextBox.Text = value; }
            get { return AddressTextBox.Text; }
        }

        public string City
        {
            set { CityTextBox.Text = value; }
            get { return CityTextBox.Text; }
        }
        public string State
        {
            set { state = value; StateDropDownList.SelectedValue = value.Trim(); }
            get { return StateDropDownList.SelectedValue; }

        }  


        public string Country
        {
            set { country = value; }
            get { return CountryDropDownList.SelectedValue; }
        }

        public string Zip
        {
            set { Zip1TextBox.Text = value; }
            get
            {
                if (Zip2TextBox.Text.Trim() != "")
                {
                    return Zip1TextBox.Text + "-" + Zip2TextBox.Text;
                }
                else
                {
                    return Zip1TextBox.Text;
                }


            }
        }

        public int Zone
        {
            set { zone = value; }
            get { return Convert.ToInt32(ZoneDropDownList.SelectedValue); }


        }

        public string Zip1
        {
            set { Zip1TextBox.Text = value; }
            get { return Zip1TextBox.Text; }
            
        }
        public string Zip2
        {
            set { Zip2TextBox.Text = value; }
            get { return Zip2TextBox.Text; }

        }


        public string Location
        {
            set { LocationTextBox.Text = value; }
            get { return LocationTextBox.Text; }
        }


        #endregion


        #region Handle Events
        private void CountryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            StateDropDownList.Items.Clear();
            SetCountryStateDropDown();
        }

        private void SetAsBillingAddressCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (eventSetAsBillingAddress != null)
                eventSetAsBillingAddress(sender, e);
        }
        #endregion

    }
}
