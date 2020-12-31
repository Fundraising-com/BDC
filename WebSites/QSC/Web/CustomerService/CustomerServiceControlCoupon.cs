using System;
using System.Data;
using QSPFulfillment.DataAccess.Common;
using System.ComponentModel;



namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for CustomerServiceControl.
	/// </summary>
	public class CustomerServiceControlCoupon : CustomerServiceControl
	{
		

		public CustomerServiceControlCoupon()
		{
			
		}

			
		#region Web Form Designer generated code
		protected override void OnInit(EventArgs e)
		{
			
			InitializeComponent();
			base.OnInit(e);							
		}
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			
			this.Page.BackClick +=new EventHandler(btnBack_Click);	
			this.Page.NextClick +=new EventHandler(btnNext_Click);		
			
		}
		#endregion
	
		
		public  new CustomerServicePageCoupon Page
		{
			get
			{
				return (CustomerServicePageCoupon)base.Page;
			}
			set
			{
				base.Page = value;
			}
		}
		private void btnBack_Click(object sender, EventArgs e)
		{
			if(!this.Page.ActionPerformed)
			{
				this.Page.CurrentStep--;	
				this.Page.ActionPerformed = true;
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			try
			{
				
				if(!this.Page.ActionPerformed)
				DoAction();
				
				
				
			}
			catch(Exception ex)
			{
				if(ex is ExceptionFulf)
					this.Page.SetPageError((ExceptionFulf)ex);
			}
		}
		protected virtual void DoAction()
		{
		
		}
		public virtual void SetValueElement()
		{
							
		}

	
	}
}
