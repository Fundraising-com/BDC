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
using GA.BDC.Core.Diagnostics;

namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for ProfitCalculator.
	/// </summary>
	public class ProfitCalculator : ScratchcardWebBase
	{
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content1;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
		protected System.Web.UI.WebControls.Image Image1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl5;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl9;
		protected System.Web.UI.WebControls.Image Image2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl6;
		protected System.Web.UI.WebControls.TextBox txtGroupMembers;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl ButtonPanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl lblError;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl7;
		protected System.Web.UI.WebControls.TextBox txtRaise;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl8;
		protected System.Web.UI.WebControls.TextBox txtProfits;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl ButtonPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl Buttonpanelcontrol3;
		protected GA.BDC.Core.Web.UI.MasterPages.MasterPage MasterPage1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ScratchcardOmnitureTracking.SetPageNameAndCategory("Public", "Profit");
			Globalize(PagePanelControl1);
		}
		
		private void txtGroupMembers_TextChanged(object sender, System.EventArgs e)
		{		
			try 
			{
				int price = 20;
				int numberOfParticipants = int.Parse(txtGroupMembers.Text);	
				double profitPotential = 100;
				double freeCards = numberOfParticipants * 0.1;
				double totalCost = numberOfParticipants * price;

				
				if(numberOfParticipants != int.MinValue)
				{
					txtRaise.Text =(profitPotential * (numberOfParticipants + freeCards)).ToString();
					txtProfits.Text = (Convert.ToDouble(txtRaise.Text.Trim()) - totalCost).ToString();
				}
				
			}
			catch
			{
				lblError.Text = "Please enter a valid number of group members.";
				txtGroupMembers.Text = "";
				txtRaise.Text = "";
				txtProfits.Text = "";
				Logger.LogWarn("Error in calculating profit on scratchard");
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
			this.txtGroupMembers.TextChanged += new System.EventHandler(this.txtGroupMembers_TextChanged);
			this.ButtonPanelControl1.Click += new TrackingButtonEventHandler(this.ButtonPanelControl1_Click);
			this.Buttonpanelcontrol3.Click += new TrackingButtonEventHandler(this.Buttonpanelcontrol3_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ButtonPanelControl1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void OrderNowImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string url = System.Configuration.ConfigurationSettings.AppSettings["efundraisingStoreURL"];
			Response.Redirect(url);
		}

		private void Buttonpanelcontrol3_Click(object sender, System.EventArgs e)
		{
			string url = System.Configuration.ConfigurationSettings.AppSettings["efundraisingStoreURL"];
			Response.Redirect(url);
		}
	}
}
