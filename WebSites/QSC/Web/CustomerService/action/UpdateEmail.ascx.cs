using System;
using System.Data;
using System.Text.RegularExpressions;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.CustomerService.action
{
    public partial class UpdateEmail : CustomerServiceActionControl
    {
        protected const string MSG_HEADER = "Update Email";
        private const string REGEXEMAIL = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"; 
        private string Email
        {
            get
            {
                return this.txtEmail.Text;
            }
            set
            {
                this.txtEmail.Text = value;
            }
        }

        private string NewEmail
        {
            get
            {
                return this.txtNewEmail.Text;
            }
            set
            {
                this.txtNewEmail.Text = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {          

            if (!IsPostBack)
            {
                LoadData();

                ConfirmButtonVisibility();

                if (DataSource.Rows.Count != 0)
                {
                    DataRow row = DataSource.Rows[0];
                    Email = row["Email"] == null ? String.Empty : row["Email"].ToString();
                }              
            }
        }

        protected override void SetValueElement()
        {
            this.Page.Header = MSG_HEADER;
        }

        private void LoadData()
        {
            DataSource = new DataTable("Customer");
            this.Page.BusCustomer.SelectCustomerByCOD(DataSource, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);
        }

        private void ConfirmButtonVisibility()
        {         

            //Only enable confirm button after email Address has been checked
            this.Page.ConfirmButton.Enabled = false;
            this.btnValidateEmail.Enabled = true;

        }

        protected override void DoAction()
        {
            this.Page.BusCustomer.UpdateCustomerEmailByCOD(this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID, NewEmail);

        }

        protected void btnValidateEmail_Click(object sender, System.EventArgs e)
		{
            string newEmail = NewEmail;
            this.lblEmailError.Text = "";
            bool isEmail = Regex.IsMatch(NewEmail, REGEXEMAIL, RegexOptions.IgnoreCase);
            if (isEmail)
            {
                this.Page.ConfirmButton.Enabled = true;
                this.btnValidateEmail.Enabled = false;
            }
            else
            {
                this.lblEmailError.Text = "Incorrect Email address.";               
            }
        }
    }
}