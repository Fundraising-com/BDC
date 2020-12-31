using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using DAL;
//using Business;

namespace QSPFulfillment.Finance
{
	///<summary>BankDepositAdd</summary>
	public partial class BankDepositAdd : QSPFulfillment.CommonWeb.QSPPage
	{

		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
		}
		#endregion auto-generated code

		#region Item Declarations
		protected QSPFulfillment.CommonWeb.UC.DateEntry DepositDate;
		
		public string dgPaymentsSortfield = "PAYMENT_ID";
		public string AccountInfo = "";
		public int    SepratorIndex = 0;
		public int    SepratorIndex1 = 0;
		protected System.Web.UI.WebControls.Panel Panel1;
		DataView dvPayments = new DataView();
		#endregion Item Declarations

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				InitializeData();
				populate_AccountList();
				dg_Payment_bind();
			}
		}

		private void InitializeData()
		{
			// Set the Deposit Date to current date and payment count and amount to zero
			// The sigle digit does not put leading zero and User Control validate against "MM/DD/YYYY"
			//DepositDate.Text = DateTime.Now.ToString("d");

			//DepositDate.Text = DateTime.Now.ToString("MM")+"/"+DateTime.Now.ToString("dd")+"/"+DateTime.Now.ToString("yyyy");
			DepositDate.Date = DateTime.Now;
			BankDepositStatus.Text = "New";
			DepositAmount.Text = Convert.ToString(0);
			ItemCount.Text = Convert.ToString(0);

		}


		private void dg_Payment_bind()
		{
			DataSet Paymentds = new DataSet();
			Business.Payments P = new Business.Payments();
			Paymentds = P.GetPaymentNotDepositedDataSet();
			dvPayments = Paymentds.Tables["Table"].DefaultView;
			dgPayments.DataSource = dvPayments;

			dvPayments.Sort =dgPaymentsSortfield;
			dgPayments.DataBind();
		}


		private void populate_AccountList()
		{
			DataSet BankAccds = new DataSet();
			DAL.BankAccountData BAData =	new DAL.BankAccountData();
			BankAccds = BAData.GetAllBankAccount("", ""); //Get All
			ddlBankAccountNumber.DataSource= BankAccds;
			ddlBankAccountNumber.DataBind();
			ddlBankAccountName.DataSource =BankAccds;
			ddlBankAccountName.DataBind();
			ddlBankAccountId.DataSource= BankAccds;
			ddlBankAccountId.DataBind();
		
		}

		protected void pbUnSelectPayments_Click(object sender, System.EventArgs e)
		{
			lblMessage.Visible = false;
			CheckPayment(false);
			AddPaymentandCount(pbUnSelectPayments,e);
		}

		protected void pbselectPayments_Click(object sender, System.EventArgs e)

		{
			lblMessage.Visible = false;
			CheckPayment(true);
			AddPaymentandCount(pbSelectPayments,e);
		}

		private void CheckPayment(bool pCheckUncheck)
		{
			DataGridItem dgItem;
			dgPayments.SelectedIndex = 0;
			CheckBox chkBox ;


			for (int i=0; i < dgPayments.Items.Count; i++)
			{
				dgItem = dgPayments.Items[i];
				chkBox = (CheckBox)dgItem.FindControl("ckbSelectPayment");
				if (chkBox.Checked != pCheckUncheck)
				{
					chkBox.Checked = pCheckUncheck;

				}
				else
				{
					chkBox.Checked = pCheckUncheck;

				}


			}
		}


		public void AddPaymentandCount(object sender, System.EventArgs e)
		{
			DataGridItem dgItem;
			double TotalPaymentAmount =0  ;
			int PaymentCount =0 ;
			CheckBox chkBox ;

           lblMessage.Visible = false;
			for (int i=0; i < dgPayments.Items.Count; i++)
			{
				dgPayments.SelectedIndex = 0;
				dgItem = dgPayments.Items[i];
				chkBox = (CheckBox)dgItem.FindControl("ckbSelectPayment");
				if ((chkBox != null) && chkBox.Checked)

				{

					PaymentCount = PaymentCount+1;
					TotalPaymentAmount += Convert.ToDouble(dgItem.Cells[5].Text);

				}
	     	}

			DepositAmount.Text = TotalPaymentAmount.ToString();
			ItemCount.Text = PaymentCount.ToString();
		}

		private void AddBankDeposit()
		{
			DataGridItem dgItem;
			CheckBox chkBox;
			//int paycheck
			Business.BankDeposit bd = new Business.BankDeposit();

			bd.Bank_Deposit_Id = Convert.ToString(-1);

			//bd.DepositDate			= DepositDate.Text;
			string DepositDateStr   = "";
			TextBox TB = (TextBox) DepositDate.FindControl("tb_DATE");
			if(TB != null) { DepositDateStr = "" + TB.Text; }
			bd.DepositDate			= DepositDateStr;
			bd.ItemDeposited		= ItemCount.Text;
			bd.DepositAmount		= DepositAmount.Text;
			bd.DepositStatusId	    = Convert.ToString("55002");
			bd.DepositAccountId		= ddlBankAccountId.SelectedValue;
			bd.ValidateAndSave();

			// Deposit Item Inserts

			Business.BankDepositItem bdi	=	new Business.BankDepositItem();
			//Each Record inserted in Deposit Item will be a new record
			//and Deposit Id will be same for all payments selected for insert
			//bdi.Deposit_Item_Id = -1;
			bdi.Bank_Deposit_Id	= Convert.ToInt32(bd.Bank_Deposit_Id);

			//Now get the Payment Id for seelected payments starting from first record
			dgPayments.SelectedIndex = 0;
			for (int i=0; i < dgPayments.Items.Count; i++)
			{
				dgItem = dgPayments.Items[i];
				chkBox = (CheckBox)dgItem.FindControl("ckbSelectPayment");
				if ((chkBox != null) && chkBox.Checked)

				{

					bdi.Deposit_Item_Id = -1;
					bdi.Payment_Id =  Convert.ToInt32(dgItem.Cells[1].Text);
					bdi.ValidateAndSave();

				}

			}

		}


		public bool IsPaymentSelected()
		{
			bool PaymentSelected = false;
			DataGridItem dgItem;
			dgPayments.SelectedIndex = 0;
			CheckBox chkBox ;
			for (int i=0; i < dgPayments.Items.Count; i++)
			{
				dgItem = dgPayments.Items[i];
				chkBox = (CheckBox)dgItem.FindControl("ckbSelectPayment");
				if (chkBox.Checked == true)
				{
					PaymentSelected = true;
					break;
				}
			}
			return(PaymentSelected);
		}

		public string getAccountInfo(string pField, string pFieldValue)

		{
			DataSet SBankAccds = new DataSet();
			DAL.BankAccountData SBAData =	new DAL.BankAccountData();
			SBankAccds = SBAData.GetAllBankAccount(pField,pFieldValue); //Get selected account
					
			return (SBankAccds.Tables[0].Rows[0]["bank_account_Id"].ToString()+"$"+SBankAccds.Tables[0].Rows[0]["bank_account_number"] +"@" + SBankAccds.Tables[0].Rows[0]["bank_account_name"]);
			
		}


		protected void ddlBankAccountId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			AccountInfo = getAccountInfo("Bank_Account_Id",ddlBankAccountId.SelectedValue.ToString());

			//Remove Bank Account Id we donot need it here

			AccountInfo = AccountInfo.Substring( AccountInfo.IndexOf('$')+1);

			// Take portion that contain Account Number

			ddlBankAccountNumber.SelectedValue = AccountInfo.Substring(0,AccountInfo.IndexOf('@'));

			// Take portion that contain Bank Name

			ddlBankAccountName.SelectedValue =   AccountInfo.Substring(AccountInfo.IndexOf('@')+1);

		}

		protected void ddlBankAccountNumber_SelectedIndexChanged(object sender, System.EventArgs e)

		{
			AccountInfo = getAccountInfo("Bank_Account_Number",ddlBankAccountNumber.SelectedValue);
			ddlBankAccountId.SelectedValue = AccountInfo.Substring(0,AccountInfo.IndexOf('$')) ;
			ddlBankAccountName.SelectedValue =  AccountInfo.Substring( AccountInfo.IndexOf('@')+1);

		}

		private void ddlBankAccountName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Bank Name are not unique therefore disabled
			//AccountInfo = getAccountInfo("Bank_Account_Name",ddlBankAccountName.SelectedValue);
			//ddlBankAccountId.SelectedValue = AccountInfo.Substring(0,AccountInfo.IndexOf('$')) ;
			//ddlBankAccountNumber.SelectedValue =  AccountInfo.Substring( AccountInfo.IndexOf('$')+1,AccountInfo.IndexOf('@')-2);
		}

		protected void pbAddBankDeposit_Click(object sender, System.EventArgs e)
		{
			if (IsPaymentSelected())
			{
				//if (Convert.ToDateTime(DepositDate.Text) >= DateTime.Today)
				if(DepositDate.Date >= DateTime.Today)
				{
					BankDepositStatus.Text = "Approve";
					lblMessage.Visible = false;
					AddBankDeposit();
					// Add code to remove payments which have just been updated
					// with new Deposit Id.ReQuery
					InitializeData();
					dg_Payment_bind();
				}
				else
				{
					lblMessage.Visible = true;
					lblMessage.Text ="Deposit Date can not be earlier than today";

				}
			}
			else
			{
				lblMessage.Visible = true;
				lblMessage.Text = "Please select atleast one Payment";
			}


		}

	}
}

