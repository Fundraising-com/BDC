using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.OrderMgt
{
	/// <summary>
	/// Summary description for CASummaryHyperLink.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:PackingSlipLinkButton runat=server></{0}:PackingSlipLinkButton>")]
	public class PackingSlipLinkButton : QSPFulfillment.CommonWeb.RSGenerationLinkButton
	{
		private const string REPORT_NAME = "PrintPickList";

		[Bindable(true), 
		Category("Data"), 
		DefaultValue(0)] 
		public int OrderID 
		{
			get 
			{
				if(this.ViewState["OrderID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["OrderID"]);
			}
			set 
			{
				this.ViewState["OrderID"] = value;
			}
		}

		[Bindable(true), 
		Category("Data"), 
		DefaultValue(0)] 
		public int BatchID 
		{
			get 
			{
				if(this.ViewState["BatchID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["BatchID"]);
			}
			set 
			{
				this.ViewState["BatchID"] = value;
			}
		}

		[Bindable(true), 
		Category("Data"), 
		DefaultValue(0)] 
		public DateTime BatchDate
		{
			get 
			{
				if(this.ViewState["BatchDate"] == null)
					return new DateTime(1995, 1, 1);

				return Convert.ToDateTime(this.ViewState["BatchDate"]);
			}
			set 
			{
				this.ViewState["BatchDate"] = value;
			}
		}

		public override void DataBind()
		{
			if(this.OrderID != 0 && this.BatchID != 0 && this.BatchDate != new DateTime(1995, 1, 1)) 
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
			parameterValue.Name = "ReportType";
			parameterValue.Value = "2";
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "ReportRequestID";
			parameterValue.Value = "0";
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "OrderId";
			parameterValue.Value = this.OrderID.ToString();
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "BatchId";
			parameterValue.Value = this.BatchID.ToString();
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "BatchDate";
			parameterValue.Value = this.BatchDate.ToString();
			parameterValues.Add(parameterValue);

			return parameterValues;
		}
	}
}