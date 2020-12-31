using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Common;
using Common.TableDef;
using Business.Objects;

namespace QSPFulfillment.AcctMgt.Control
{
	/// <summary>
	/// Summary description for CASummaryHyperLink.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:CloneCALinkButton runat=server></{0}:CloneCALinkButton>")]
	public class CloneCALinkButton : System.Web.UI.WebControls.LinkButton, INamingContainer
	{
		[Bindable(true), 
		Category("Data"), 
		DefaultValue(0)] 
		public int CampaignID 
		{
			get 
			{
				if(this.ViewState["CampaignID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["CampaignID"]);
			}
			set 
			{
				this.ViewState["CampaignID"] = value;
			}
		}

		protected override void OnClick(EventArgs e)
		{
			if(this.CampaignID != 0) 
			{
				try 
				{
					Campaign oCampaign = new Campaign(this.CampaignID);

					oCampaign.Clone(this.CampaignID);

					base.OnClick (e);
				} 
				catch(MessageException ex) 
				{
					if(this.Page is AcctMgtPage) 
					{
						((AcctMgtPage) this.Page).SetPageError(ex);
					} 
					else 
					{
						throw ex;
					}
				}
			}
		}
	}
}