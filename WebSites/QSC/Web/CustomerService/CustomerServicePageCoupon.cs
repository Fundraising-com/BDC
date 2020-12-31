using System;
using System.Web.UI.WebControls;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for CustomerServicePageCoupon.
	/// </summary>
	/// 
	
	public class CustomerServicePageCoupon:CustomerServicePage
	{
		public Button btnBack;
		public Button btnNext;
		private Label lblMessage;
		private Label lblErrorMessage;
		private Label lblHeader;
		public event EventHandler NextClick;
		public event EventHandler BackClick;
		protected couponstep1 ctrlCouponstep1;
		protected CustomerServiceControlCoupon ctrlCouponstep2;
		protected CustomerServiceControlCoupon ctrlCouponstep3;
		private QSPFulfillment.DataAccess.Business.CouponBusiness bCouponBusiness;
		public CustomerServicePageCoupon()
		{
			
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		private void Page_Render(object sender, EventArgs e)
		{
			
			this.ActionPerformed = false;

				if(CurrentStep == 0)
				{
					ClearValue();
					ctrlCouponstep1.ClearValue();
					this.CurrentStep =1;
				}

				if(CurrentStep == 1)
				{
					ctrlCouponstep1.Visible = true;
					ctrlCouponstep2.Visible = false;
					ctrlCouponstep3.Visible= false;
					this.btnBack.Visible = false;
					this.btnNext.Visible = true;
					this.btnNext.Text = "Next";
					this.ctrlCouponstep1.SetValueElement();
				}
				else if(CurrentStep == 2)
				{
					ctrlCouponstep1.Visible = false;
					ctrlCouponstep2.Visible = true;
					ctrlCouponstep3.Visible= false;
					this.btnBack.Visible = true;
					this.btnNext.Visible = false;
					this.btnNext.Text = "Next";
					this.ctrlCouponstep2.SetValueElement();
					
				}
				else if(CurrentStep == 3)
				{
					ctrlCouponstep1.Visible = false;
					ctrlCouponstep2.Visible = false;
					ctrlCouponstep3.Visible= true;
					this.btnNext.Text = "Save";
					this.btnBack.Visible = true;
					this.btnNext.Visible = true;
					this.ctrlCouponstep3.SetValueElement();
				}
			
		}
		#region Web Form Designer generated code
		protected void OnInit(EventArgs e,Button Back,Button Next,Label Message,Label Header,Label ErrorMessage)
		{
			this.btnBack = Back;
			this.btnNext = Next;
			this.lblMessage= Message;
			this.lblHeader = Header;
			lblErrorMessage = ErrorMessage;
			InitializeComponent();
			base.OnInit(e);							
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnBack.Click +=new EventHandler(btnBack_Click);	
			this.btnNext.Click +=new EventHandler(btnNext_Click);
			this.Load += new EventHandler(Page_Load);
			this.PreRender +=new EventHandler(Page_Render);
		}
		#endregion
		
		public bool Step1Completed
		{
			get
			{
				if(ViewState["Step1Completed"] == null)
					return false;

				return Convert.ToBoolean(ViewState["Step1Completed"]);
			
			}
			set{ViewState["Step1Completed"] = value;}
		}
		public bool Step2Completed
		{
			get
			{
				if(ViewState["Step2Completed"] == null)
					return false;

				return Convert.ToBoolean(ViewState["Step2Completed"]);
			
			}
			set{ViewState["Step2Completed"] = value;}
		}
		public bool Step3Completed
		{
			get
			{
				if(ViewState["Step3Completed"] == null)
					return false;

				return Convert.ToBoolean(ViewState["Step3Completed"]);
			
			}
			set{ViewState["Step3Completed"] = value;}
		}
		public bool ActionPerformed
		{
			get
			{
				if(ViewState["ActionPerformed"] == null)
					return false;

				return Convert.ToBoolean(ViewState["ActionPerformed"]);
			
			}
			set{ViewState["ActionPerformed"] = value;}
		}
		public int CurrentStep
		{
			get
			{
				if(ViewState["CurrentStep"] == null)
					return 0;

				return Convert.ToInt32(ViewState["CurrentStep"]);
			
			}
			set{ViewState["CurrentStep"] = value;}
		}
		public string Coupon
		{
			get
			{
				if(ViewState["Coupon"] == null)
					return "";

				return ViewState["Coupon"].ToString();
			
			}
			set{ViewState["Coupon"] = value;}
		}
        public bool InvoiceOrder
        {
            get
            {
                if (ViewState["InvoiceOrder"] != null)
                    return (bool)ViewState["InvoiceOrder"];
                else
                    return false;
            }
            set { ViewState["InvoiceOrder"] = value; }
        }
		public QSPFulfillment.DataAccess.Common.ActionObject.Magazine MagazineInfo
		{
			get
			{
				if(ViewState["MagazineInfo"] == null)
					return new QSPFulfillment.DataAccess.Common.ActionObject.Magazine();

				return (QSPFulfillment.DataAccess.Common.ActionObject.Magazine)ViewState["MagazineInfo"];
			
			}
			set{ViewState["MagazineInfo"] = value;}
		}
		
		public int CouponSetID
		{
			get
			{
				if(ViewState["CouponSetID"] == null)
					return 0;

				return Convert.ToInt32(ViewState["CouponSetID"]);
			}
			set
			{
				this.ViewState["CouponSetID"]= value;
			}
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			if(BackClick != null)
				BackClick(sender,e);
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if(NextClick != null)
				NextClick(sender,e);
		}
		private void ClearValue()
		{
			this.btnNext.Text = "Next";
			this.CustomerInfo = null;
			this.MagazineInfo = null;
			this.OrderInfo = null;
			
		}
		public string Message
		{
			get
			{
				
				return this.lblMessage.Text;
				
			}
			set
			{
				this.lblMessage.Text =value;
				

			}
		}
		public string ErrorMessage
		{
			get
			{
				
				return this.lblMessage.Text;
				
			}
			set
			{
				this.lblMessage.Text =value;
				

			}
		}
		public string Header
		{
			get
			{
				
				return this.lblHeader.Text;
				
			}
			set
			{
			
				this.lblHeader.Text =value;
				

			}
		}

		public int OrderQualifierID 
		{
			get 
			{
				if(ViewState["OrderQualifierID"] == null)
					ViewState["OrderQualifierID"] = 39008;

				return Convert.ToInt32(ViewState["OrderQualifierID"]);
			}
			set 
			{
				ViewState["OrderQualifierID"] = value;
			}
		}

		#region business
		public CouponBusiness BusCoupon
		{
			get
			{
				if(bCouponBusiness == null)
					bCouponBusiness = new CouponBusiness(this.MessageManager);

				return bCouponBusiness;
			}
				
		}
		#endregion

		
	}
}
