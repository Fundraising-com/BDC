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
using GA.BDC.Core.efundraisingCore;
using GA.BDC.Core.Diagnostics;

namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for Scratchcard.
	/// </summary>
	public class Scratchcard : ScratchcardWebBase
	{
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content1;
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content2;
		protected System.Web.UI.WebControls.Image imgTopRight;
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content3;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected System.Web.UI.WebControls.Image imgIcone;
		protected System.Web.UI.WebControls.Image imgTitre;
		protected System.Web.UI.WebControls.ImageButton imgCardLeft;
		protected System.Web.UI.WebControls.Image imgYourLogo;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.Image imgCard2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl5;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl ButtonPanelControl1;
		protected System.Web.UI.WebControls.Image Image1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl6;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl7;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl8;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl9;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl10;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl11;
		protected System.Web.UI.WebControls.Image imgCard1;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl ButtonPanelControl3;
		protected GA.BDC.Core.Web.UI.MasterPages.MasterPage MasterPage1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				eFundEnv env = eFundEnv.Create();
				string omniturePageName = "";
				string type = Request.QueryString["type"];
				imgTopRight.ImageUrl = "Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/topright/top_" + type + ".gif";
			
				switch (type) 
			
				{
					case "soccer" : 
						omniturePageName = "Soccer";
						break;
					case "basket" : 
						omniturePageName = "Basketball";
						break;
					case "foot" : 
						omniturePageName = "Football";
						break;
					case "softball" : 
						omniturePageName = "Softball";
						break;
					case "baseball" : 
						omniturePageName = "Baseball";
						break;
					case "hockey" : 
						omniturePageName = "Hockey";
						break;
					case "volley" : 
						omniturePageName = "Volleyball";
						break;
					case "cheer" : 
						omniturePageName = "Cheerleading";
						break;
					case "bowling" : 
						omniturePageName = "Bowling";
						break;
					case "elementary" : 
						omniturePageName = "Elementary School";
						break;
					case "highschool" : 
						omniturePageName = "High School";
						break;
					case "university" : 
						omniturePageName = "University";
						break;
					case "church_nonc" : 
						omniturePageName = "Church";
						break;
					default : 
						omniturePageName = "Other";
						break;
				}
					
				imgIcone.ImageUrl = "Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/icone_title/it_" + type + ".gif";
				imgTitre.ImageUrl = "Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/title/t_" + type + ".gif";
			
				// check if we have to display the inside or outside of the card
				if (Request.QueryString["showInside"] != null)
				{
					imgCardLeft.ImageUrl = "Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/back_pannel_inside.gif";
					imgCard1.ImageUrl = "Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/inside.gif";
					if (env.PartnerInfo.PartnerID == 129)
						imgCard1.ImageUrl = "Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/in_card_ca.jpg";
					ImageButton1.Visible = false;
					imgYourLogo.Visible = false;
				}
				else
				{
					imgCardLeft.ImageUrl = "Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/back_pannel.gif";
					imgCard1.ImageUrl = "Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/bigcards/" + type + ".jpg";
					imgYourLogo.Visible = true;
				
					// check if we have a second card to display
					if (Request.QueryString["secondType"] != null)
					{
						string secondType = Request.QueryString["secondType"];
						imgCard2.ImageUrl = "Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/bigcards/" + secondType + ".jpg";
						ImageButton1.Visible = true;
						imgCard2.Visible = true;
						ContentPanelControl4.Visible = true;
						Session["copySecondType"] = Request.QueryString["secondType"];
					}
					else
					{
						ImageButton1.Visible = false;
						imgCard2.Visible = false;
						ContentPanelControl4.Visible = false;
						Session["copySecondType"] = "";
					}
				}
			
				ScratchcardOmnitureTracking.SetPageNameAndCategory("Public", omniturePageName);
				Globalize(PagePanelControl1);
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in Page Load of Scratchcard.aspx",ex);
				
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
			this.imgCardLeft.Click += new System.Web.UI.ImageClickEventHandler(this.imgCardLeft_Click);
			this.ButtonPanelControl3.Click += new TrackingButtonEventHandler(this.ButtonPanelControl3_Click);

		}
		#endregion
		
		
		private void imgCardLeft_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				if (Request.QueryString["showInside"] != null)
				{
					if ((string)Session["copySecondType"] != "")
					{
						string temp = (string)Session["copySecondType"];
						Response.Redirect("Scratchcard.aspx?Type=" + Request.QueryString["type"] + "&secondType=" + temp,false);
					}
					else
					{
						Response.Redirect("Scratchcard.aspx?Type=" + Request.QueryString["type"],false);
					}
				}
				else
				{
					Response.Redirect("Scratchcard.aspx?Type=" + Request.QueryString["type"] + "&showInside=true",false);
				}
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in imgCardLeft_Click of scratchcard",ex);
			}
		}

		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			imgCardLeft_Click(sender, e);
		}

	

		private void ButtonPanelControl3_Click(object sender, System.EventArgs e)
		{
		string url = System.Configuration.ConfigurationSettings.AppSettings["efundraisingStoreURL"];
			Response.Redirect(url);
		}
	}
}
