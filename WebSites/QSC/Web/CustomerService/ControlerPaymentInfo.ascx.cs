namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
		using QSPFulfillment.DataAccess.Common.ActionObject;

	/// <summary>
	///		Summary description for ControlerPaymentInfo.
	/// </summary>
	public partial class ControlerPaymentInfo : CustomerServiceControl
	{
		protected System.Web.UI.WebControls.TextBox tbxAuthorization;
		protected System.Web.UI.WebControls.TextBox tbxReturnCode;
		private CreditCard cciCreditCardInfo;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator5;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator1;
      public DataTable GiftCardDataSource;

      private string strCreditCardStatus;
		

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			
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

		public bool ShowCreditCardStatus 
		{
			get 
			{
				return this.tableRowCreditCardStatus.Visible;
			}
			set 
			{
				this.tableRowCreditCardStatus.Visible = value;
			}
		}

		private void SetValue()
		{  

			this.lblPaymentMethod.Text = CreditCardInfo.PaymentMethodID.ToString();
				
			if(CreditCardInfo.IsCreditCardPayment)
			{
				SetCreditCardInfo();
			} 
			else 
			{
				SetValueEmpty();
			}
			
			
					
		}

		public void SetValueEmpty() 
		{
			this.lblAuthorization.Text = "";
			this.lblCardholderName.Text = "";
			this.lblExpiry.Text = "";
			this.lblNumber.Text = "";
			this.lblReturnCode.Text = "";
			this.lblReturnCodeDesc.Text = "";
			this.lblStatus.Text = "";
         this.lblAmount.Text = "";
		}

		public override void DataBind()
		{
			if(EditMode)
				AddValuesDDLYear();

			LoadData();
			

			if(DataSource.Rows.Count !=0)
			{
				SetValueObject(DataSource.Rows[0]);
				if(EditMode)
				{
					SetValueEditMode();
				}
				else
				{					
					SetValue();
				}
			}

         this.dtlGiftCard.DataSource = GiftCardDataSource;
         this.dtlGiftCard.DataBind();
         base.DataBind();

         //base.DataBind ();
      }
		public void LoadData()
		{
			try
			{
				DataSource = new PAYMENTTable();
				this.Page.BusPayment.SelectCustomerCreditCardInformation(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance);

            GiftCardDataSource = new DataTable("GiftCard");
            this.Page.BusPayment.SelectCustomerGiftCardInformation(GiftCardDataSource, this.Page.OrderInfo.CustomerOrderHeaderInstance);

         }
         catch (NullReferenceException ex) 
			{
				bool hasKey = false;

				foreach(string key in Session.Keys) 
				{
					if(key == "CurrentInfoSession") 
					{
						hasKey = true;
					}
				}

				if(hasKey) 
				{
					ex.Source += " Has the session key.";
				} 
				else 
				{
					ex.Source += " Does not have the session key.";
				}

				throw ex;
			}
		}

		private void SetCreditCardInfo()
		{ 
			this.lblAuthorization.Text = cciCreditCardInfo.AuthorizationCode;
			this.lblCardholderName.Text = cciCreditCardInfo.CardHolderName;
			this.lblExpiry.Text = cciCreditCardInfo.ExpirationMonth +"/"+CreditCardInfo.ExpirationYear;
			this.lblNumber.Text = cciCreditCardInfo.SafeOutPutCreditCardNumber;;
			this.lblReturnCode.Text = cciCreditCardInfo.ReturnCode;
			this.lblReturnCodeDesc.Text ="";
			this.lblStatus.Text = strCreditCardStatus;
         this.lblAmount.Text = cciCreditCardInfo.Amount.ToString();

      }
		private void SetValueEditMode()
		{  
			SetPaymenthMethod((int)cciCreditCardInfo.PaymentMethodID);
				
			if(cciCreditCardInfo.IsCreditCardPayment)
			{
				SetCreditCardInfoEditMode();
			}
						
		}
		private void SetCreditCardInfoEditMode()
		{ 
			
			this.lblAuthorization.Text = cciCreditCardInfo.AuthorizationCode;
			
			if(cciCreditCardInfo.AsExpirationDate)
			{
				SetExpirationMonth(cciCreditCardInfo.ExpirationMonth);
				SetExpirationYear(cciCreditCardInfo.ExpirationYear);
			}
			this.tbxNumber.Text = cciCreditCardInfo.SafeOutPutCreditCardNumber;
			this.tbxCardholderName.Text = cciCreditCardInfo.CardHolderName;
		}
		
		private void SetPaymenthMethod(int PaymentMethodID)
		{
			this.ddlPaymentMethod.SelectedIndex = this.ddlPaymentMethod.Items.IndexOf(this.ddlPaymentMethod.Items.FindByValue(PaymentMethodID.ToString()));
		}
		public bool EditMode
		{
			
			get
			{
				if(ViewState["EditMode"]!= null)
					return Convert.ToBoolean(ViewState["EditMode"]);
				

				return false;
			}
			set
			{
				ViewState["EditMode"]=value;
				
				ShowHide(value);
				

			}
		}
		private void ShowHide(bool Value)
		{
			//si edit mode
			if(Value)
			{
				this.ddlPaymentMethod.Visible = Value;
				ddlMonth.Visible= Value;
				ddlYear.Visible= Value;
				lblSlash.Visible = Value;
				this.tbxNumber.Visible= Value;
				this.tbxCardholderName.Visible = Value;
				this.lblAuthorization.Visible = !Value;
				this.lblCardholderName.Visible = !Value;
				this.lblExpiry.Visible = !Value;
				this.lblNumber.Visible = !Value;
				this.lblReturnCode.Visible = !Value;
				this.lblReturnCodeDesc.Visible = !Value;
            this.lblAmount.Visible = Value;
			
			}
			else
			{
				this.ddlPaymentMethod.Visible = Value;
				ddlMonth.Visible= Value;
				ddlYear.Visible= Value;
				this.tbxNumber.Visible= Value;
				this.tbxCardholderName.Visible = Value;
				lblSlash.Visible =Value;
				this.lblAuthorization.Visible = !Value;
				this.lblCardholderName.Visible = !Value;
				this.lblExpiry.Visible = !Value;
				this.lblNumber.Visible = !Value;
            this.lblAmount.Visible = !Value;
			}
		}

		
		private void SetValueObject(DataRow row)
		{
			
			cciCreditCardInfo = new CreditCard((PaymentMethod)Convert.ToInt32(row["PaymentMethodID"]));

			if(cciCreditCardInfo.IsCreditCardPayment)
			{
				cciCreditCardInfo.CardHolderName =	row["CardholderName"].ToString();
				cciCreditCardInfo.CreditCardNumber=	row["CreditCardNumber"].ToString();
				cciCreditCardInfo.ExpirationMonth =	GetExpirationMonth(row);
				cciCreditCardInfo.ExpirationYear = GetExpirationYear(row);
				cciCreditCardInfo.AuthorizationCode =  row["AuthorizationCode"].ToString();
				this.cciCreditCardInfo.CustomerPaymentHeaderInstance = Convert.ToInt32(row["CustomerPaymentHeaderInstance"]);
				strCreditCardStatus = row["CreditCardStatus"].ToString();
            cciCreditCardInfo.Amount = Convert.ToDecimal(row["Amount"]);
         }



         ViewState["cciCreditCardInfo"] = cciCreditCardInfo;
		}
		public CreditCard CreditCardInfo
		{
			get
			{
				cciCreditCardInfo = (CreditCard)ViewState["cciCreditCardInfo"];;
				if(EditMode)
				{
					cciCreditCardInfo.CreditCardNumber = tbxNumber.Text.Replace(" ","");
					cciCreditCardInfo.ExpirationMonth = GetExpirationMonth();
					cciCreditCardInfo.ExpirationYear = GetExpirationYear();
					cciCreditCardInfo.PaymentMethodID = GetPaymentMethod();
					cciCreditCardInfo.CardHolderName = this.tbxCardholderName.Text;
               //cciCreditCardInfo.Amount = Convert.ToDecimal(this.lblAmount);
            }

            return cciCreditCardInfo;
			}
		}
		private PaymentMethod GetPaymentMethod()
		{
			return (PaymentMethod)Convert.ToInt32(this.ddlPaymentMethod.SelectedItem.Value);
		}
		
		private string GetExpirationMonth()
		{
			if(this.ddlMonth.SelectedItem.Value == "Select")
				return String.Empty;
			return this.ddlMonth.SelectedItem.Value;
		}
		private string GetExpirationYear()
		{
			if(this.ddlYear.SelectedItem.Value == "Select")
				return String.Empty;

			return this.ddlYear.SelectedItem.Value;
		}

		private void AddValuesDDLYear()
		{
			if(ddlYear.Items.Count ==0)
			{
				int start = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2,2));
				int end = start + 15;
				ddlYear.Items.Add(new ListItem("Select","Select"));
				for(int i =start; i<=end;i++)
				{
					if(i<10)
					{
						this.ddlYear.Items.Add(new ListItem("0"+i.ToString(),"0"+i.ToString()));
					}
					else
					{
						this.ddlYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
					}
				}
			}
		}
		private void SetExpirationMonth(string Month)
		{
			this.ddlMonth.SelectedIndex = this.ddlMonth.Items.IndexOf(this.ddlMonth.Items.FindByText(Month));
		}
		private void SetExpirationYear(string Year)
		{
			this.ddlYear.SelectedIndex = this.ddlYear.Items.IndexOf(this.ddlYear.Items.FindByText(Year));
		}
		
		private string GetExpirationMonth(DataRow row)
		{
			if(row["ExpirationDate"] != null && row["ExpirationDate"].ToString().Length == 4)
			{
				return row["ExpirationDate"].ToString().Substring(0,2);
			}
			return "";
		}
		private string GetExpirationYear(DataRow row)
		{
			if(row["ExpirationDate"] != null && row["ExpirationDate"].ToString().Length == 4)
			{
				return row["ExpirationDate"].ToString().Substring(2,2);
			}
			
			return "";
		}
		


	}
}
