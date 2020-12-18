using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace efundraising.EFundraisingCRMWeb.AR.CreditCheck
{
	/// <summary>
	/// Summary description for CreditCheckMainForm.
	/// </summary>
	/// 


	public enum CreditCheckTabs
	{
		CreditRequest = 0,
		CreditReport,
		CreditOptions
	}

	public partial class CreditCheckMainForm : System.Web.UI.Page
	{
		

		public void ChangeTab()
		{
		}
		
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				//check session to see what subform to display
				int subForm = Convert.ToInt32(CreditCheckTabs.CreditRequest);
 
				if (Session[EFundraisingCRMWeb.Global.SessionVariables.CONTROL_TO_DISPLAY] != null)
				{
					subForm = Convert.ToInt32(Session[EFundraisingCRMWeb.Global.SessionVariables.CONTROL_TO_DISPLAY]);
				
					CreditCheckTabStrip.SelectedIndex = subForm;
				}
					
				frameCreditCheck.Attributes["src"] = Enum.ToObject(typeof(CreditCheckTabs), subForm).ToString() + ".aspx";
			}
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void CreditCheckTabStrip_SelectedIndexChange(object sender, System.EventArgs e)
		{
		    //HtmlControl frame1 = (HtmlControl)this.FindControl("frameCreditCheck");
			int tabIndex = CreditCheckTabStrip.SelectedIndex;
			switch(tabIndex)
			{         
				case 0:   
					Session[efundraising.EFundraisingCRMWeb.Global.SessionVariables.CONTROL_TO_DISPLAY] = Convert.ToInt32(CreditCheckTabs.CreditRequest);
					break;                  
				case 1:
			    	Session[efundraising.EFundraisingCRMWeb.Global.SessionVariables.CONTROL_TO_DISPLAY] = Convert.ToInt32(CreditCheckTabs.CreditReport);
			        break;
                case 2: 
					Session[efundraising.EFundraisingCRMWeb.Global.SessionVariables.CONTROL_TO_DISPLAY] = Convert.ToInt32(CreditCheckTabs.CreditReport);
					break;
			}
			
			CreditCheckTabStrip.SelectedIndex = tabIndex;
			frameCreditCheck.Attributes["src"] = Enum.ToObject(typeof(CreditCheckTabs), tabIndex).ToString() + ".aspx";

			
		}
	}
}
