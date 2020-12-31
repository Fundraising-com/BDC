using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.AcctMgt.Control
{
	/// <summary>
	/// Summary description for ConfirmationAgreementLinkButton.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:ConfirmationAgreementLinkButton runat=server></{0}:ConfirmationAgreementLinkButton>")]
	public class ConfirmationAgreementLinkButton : QSPFulfillment.CommonWeb.RSGenerationLinkButton
	{
		private const string REPORT_NAME = "CampaignConfirmationAgreement";

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

		public override void DataBind()
		{
			if(this.CampaignID != 0) 
			{
				SetValue();
			} 

			base.DataBind();
		}

		private void SetValue() 
		{
			this.ReportName = REPORT_NAME;
			this.ParameterValues = GetParameterValueCollection();
			this.Mode = FilePageMode.PopUp;
		}

		private ParameterValueCollection GetParameterValueCollection() 
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			parameterValue = new ParameterValue();
			parameterValue.Name = "CampaignID";
			parameterValue.Value = this.CampaignID.ToString();
			parameterValues.Add(parameterValue);

			return parameterValues;
		}
	}
}