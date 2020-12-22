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

namespace EFundraisingCRMWeb.Components.User.ClientControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using efundraising.EnterpriseComponents;

    /// <summary>
    ///		Summary description for CustomerInformation.
    /// </summary>
    public partial class CustomerInformation : System.Web.UI.UserControl
    {

        private string clientSequenceCode = "";
        private string timeToCall;
        private bool disableForConsultant = false;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            ClearErrorLabels();

            // Put user code to initialize the page here
            if (!(IsPostBack))
            {
                SetSequenceCodedropdownlist();

                if (disableForConsultant)
                {
                    Components.Server.ManageSaleScreen.MakeReadOnly(sequenceCodedropdownlist);
                }
            }

        }

        private void SetSequenceCodedropdownlist()
        {
            sequenceCodedropdownlist.Items.Clear();
            sequenceCodedropdownlist.DataTextField = "clientSequenceCode";
            sequenceCodedropdownlist.DataValueField = "clientSequenceCode";
            sequenceCodedropdownlist.DataSource = efundraising.EFundraisingCRM.ClientSequence.GetClientSequences();
            sequenceCodedropdownlist.DataBind();

            if (clientSequenceCode == "")
            {
                sequenceCodedropdownlist.SelectedValue = efundraising.Configuration.ApplicationSettings.GetConfig()["ClientSequenceCode", "default"].ToString();
            }
            else
            {
                sequenceCodedropdownlist.SelectedValue = clientSequenceCode;
            }


            TimeToCallDropdownlist.SelectedValue = timeToCall;



        }


        public efundraising.EFundraisingCRM.Client GetCustomerInfoFromInterface()
        {
            efundraising.EFundraisingCRM.Client cl = new efundraising.EFundraisingCRM.Client();

            cl.ClientSequenceCode = sequenceCodedropdownlist.SelectedValue;
            if (ClientIDTextBox.Text == "")
            {
                cl.ClientId = int.MinValue;
            }
            else
            {
                cl.ClientId = Convert.ToInt32(ClientIDTextBox.Text);
            }

            cl.FirstName = FirstNameTextBox.Text;
            cl.LastName = LastnameTextbox.Text;
            cl.Organization = OrganizationTextBox.Text;
            cl.Email = EmailTextBox.Text;
            cl.DayPhone = GetClientPhoneFromInterface(dPhone1, dPhone2, dPhone3);
            cl.DayPhoneExt = dPhone4.Text;
            cl.DayTimeCall = TimeToCallDropdownlist.SelectedValue;
            //
            cl.EveningPhone = GetClientPhoneFromInterface(ePhone1, ePhone2, ePhone3);
            cl.EveningPhoneExt = ePhone4.Text;
            //
            cl.Fax = GetClientPhoneFromInterface(fPhone1, fPhone2, fPhone3);
            return cl;
        }

        public void SetCustomerInfo(efundraising.EFundraisingCRM.Client cl)
        {
            if (cl == null)
                return;

            sequenceCodedropdownlist.SelectedValue = cl.ClientSequenceCode;
            clientSequenceCode = cl.ClientSequenceCode;
            ClientIDTextBox.Text = cl.ClientId.ToString();
            FirstNameTextBox.Text = cl.FirstName;
            LastnameTextbox.Text = cl.LastName;
            OrganizationTextBox.Text = cl.Organization;
            EmailTextBox.Text = cl.Email;

            timeToCall = cl.DayTimeCall;
            if (timeToCall != "Morning" && timeToCall != "Afternoon" && timeToCall != "Evening")
            {
                timeToCall = "Any Time";
            }
            TimeToCallDropdownlist.SelectedValue = timeToCall;

            SetClientAddressPhone(cl);

        }

        private void SetClientAddressPhone(efundraising.EFundraisingCRM.Client cl)
        {
            if (cl.DayPhone != null && cl.DayPhone.Trim() != string.Empty)
            {
                try
                {
                    string[] split = cl.DayPhone.Split('-');
                    dPhone1.Text = split[0];
                    dPhone2.Text = split[1];
                    dPhone3.Text = split[2];


                }
                catch (Exception x)
                {
                }
            }
            if (cl.DayPhoneExt != null)
            {
                if (cl.DayPhoneExt.Length > 5)
                {
                    dPhone4.Text = cl.DayPhoneExt.Substring(0, 5);
                }
                else
                {
                    dPhone4.Text = cl.DayPhoneExt;
                }
            }


            if (cl.EveningPhone != null && cl.EveningPhone.Trim() != string.Empty)
            {
                try
                {
                    string[] split = cl.EveningPhone.Trim().Split('-');
                    ePhone1.Text = split[0];
                    ePhone2.Text = split[1];
                    ePhone3.Text = split[2];
                }
                catch (Exception x)
                {
                }
            }

            if (cl.EveningPhoneExt != null)
            {
                if (cl.EveningPhoneExt.Length > 5)
                {
                    ePhone4.Text = cl.EveningPhoneExt.Substring(0, 5);
                }
                else
                {
                    ePhone4.Text = cl.EveningPhoneExt;
                }
            }


            if (cl.Fax != null && cl.Fax.Trim() != string.Empty)
            {
                try
                {
                    string[] split = cl.Fax.Trim().Split('-');
                    fPhone1.Text = split[0];
                    fPhone2.Text = split[1];
                    fPhone3.Text = split[2];
                }
                catch (Exception x2)
                {
                }
            }
        }



        private string GetClientPhoneFromInterface(TextBox txtPhone1, TextBox txtPhone2, TextBox txtPhone3, TextBox txtPhoneExt)
        {
            if (txtPhone1.Text.Trim() != string.Empty && txtPhone2.Text.Trim() != string.Empty
                && txtPhone3.Text.Trim() != string.Empty && txtPhoneExt.Text.Trim() != string.Empty)
            {
                return txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim() + "-" + txtPhoneExt.Text.Trim();
            }

            return string.Empty;
        }

        private string GetClientPhoneFromInterface(TextBox txtPhone1, TextBox txtPhone2, TextBox txtPhone3)
        {
            if (txtPhone1.Text.Trim() != string.Empty && txtPhone2.Text.Trim() != string.Empty
                && txtPhone3.Text.Trim() != string.Empty)
            {
                return txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim();
            }

            return string.Empty;
        }

        public bool IsValid()
        {
            bool valid = true;
            if (FirstNameTextBox.Text.Trim() == "")
            {
                valid = false;
                FirstNameErrorLabel.Visible = true;
            }
            if (LastnameTextbox.Text.Trim() == "")
            {
                valid = false;
                LastNameErrorLabel.Visible = true;
            }
            if (OrganizationTextBox.Text.Trim() == "")
            {
                valid = false;
                OrganizationErrorLabel.Visible = true;
            }
            if ((dPhone1.Text.Trim() == "" || dPhone2.Text.Trim() == "" || dPhone3.Text.Trim() == "") &&
               (ePhone1.Text.Trim() == "" || ePhone2.Text.Trim() == "" || ePhone3.Text.Trim() == ""))
            {
                valid = false;
                dayPhoneErrorLabel.Visible = true;
            }

            if (!(Helper.IsNumeric(dPhone1.Text.Trim())) || !(Helper.IsNumeric(dPhone2.Text.Trim())) || !(Helper.IsNumeric(dPhone3.Text.Trim())))
            {
                //only invalid if a number is entered
                if (dPhone1.Text.Trim() != "" || dPhone2.Text.Trim() != "" || dPhone3.Text.Trim() != "")
                {
                    valid = false;
                    dayPhoneErrorLabel.Visible = true;
                }
            }

            if (!(Helper.IsNumeric(ePhone1.Text.Trim())) || !(Helper.IsNumeric(ePhone2.Text.Trim())) || !(Helper.IsNumeric(ePhone3.Text.Trim())))
            {
                //only invalid if a number is entered
                if (ePhone1.Text.Trim() != "" || ePhone2.Text.Trim() != "" || ePhone3.Text.Trim() != "")
                {
                    valid = false;
                    EvePhoneErrorLabel.Visible = true;
                }
            }

            if (dPhone4.Text.Trim().Length > 0)
            {
                if (!(Helper.IsNumeric(dPhone4.Text.Trim())))
                {
                    valid = false;
                    dayPhoneErrorLabel.Visible = true;
                }

            }

            if (ePhone4.Text.Trim().Length > 0)
            {
                if (!(Helper.IsNumeric(ePhone4.Text.Trim())))
                {
                    valid = false;
                    EvePhoneErrorLabel.Visible = true;
                }

            }

            return valid;

        }
        private void ClearErrorLabels()
        {
            FirstNameErrorLabel.Visible = false;
            LastNameErrorLabel.Visible = false;
            OrganizationErrorLabel.Visible = false;
            dayPhoneErrorLabel.Visible = false;
            EvePhoneErrorLabel.Visible = false;
        }

        public void DisableForConsultants()
        {
            disableForConsultant = true;
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


        public int ClientID
        {
            set { ClientIDTextBox.Text = value.ToString(); }
        }

        public string ClientSequenceCode
        {
            set { clientSequenceCode = value.ToString(); }
            get { return clientSequenceCode; }
        }

        public string FirstName
        {
            set { FirstNameTextBox.Text = value; }
        }
        public string LastName
        {
            set { LastnameTextbox.Text = value; }
        }
        public string Organization
        {
            set { OrganizationTextBox.Text = value; }
        }
        public string Email
        {
            set { EmailTextBox.Text = value; }
        }
        public string DayPhone
        {
            set
            {
                if (value != null)
                {
                    string[] split = value.Split('-');
                    if (split.Length > 2)
                    {
                        dPhone1.Text = split[0];
                        dPhone2.Text = split[1];
                        dPhone3.Text = split[2];
                    }
                    else
                    {
                        dPhone1.Text = value;
                    }
                }

            }
        }

        public string DayPhoneExt
        {
            set { dPhone4.Text = value; }
        }
        public string EveningPhone
        {
            set
            {
                if (value != null)
                {
                    string[] split = value.Split('-');
                    if (split.Length > 2)
                    {
                        ePhone1.Text = split[0];
                        ePhone2.Text = split[1];
                        ePhone3.Text = split[2];
                    }
                    else
                    {
                        ePhone1.Text = value;
                    }
                }


            }
        }
        public string EveningPhoneExt
        {
            set { ePhone4.Text = value; }
        }

        public string Fax
        {
            set
            {
                if (value != null)
                {
                    string[] split = value.Split('-');
                    if (split.Length > 2)
                    {
                        fPhone1.Text = split[0];
                        fPhone2.Text = split[1];
                        fPhone3.Text = split[2];
                    }
                    else
                    {
                        fPhone1.Text = value;
                    }
                }
            }
        }

        public string TimeToCall
        {
            set
            {
                timeToCall = value;
                if (timeToCall != "Morning" && timeToCall != "Afternoon" && timeToCall != "Evening")
                {
                    timeToCall = "Any Time";
                }
                TimeToCallDropdownlist.SelectedValue = timeToCall;
            }
        }


        #endregion
    }
}
