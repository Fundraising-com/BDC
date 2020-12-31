using System;
using System.Data;
using System.ComponentModel;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.Fulfillment.Control
{
	/// <summary>
	/// Summary description for CustomerServiceControl.
	/// </summary>
	public class FulfillmentControl : QSPUserControl
	{
		private void Page_PreRender(object sender, EventArgs e)
		{
			AddJavaScript();
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
			
			this.Page.PreRender +=new EventHandler(Page_PreRender);
			
		}
		#endregion

		public  new FulfillmentPage Page
		{
			get
			{
				return (FulfillmentPage)base.Page;
			}
			set
			{
				base.Page = value;
			}
		}
		
		protected virtual void AddJavaScript()
		{
			
		}
	}
}
