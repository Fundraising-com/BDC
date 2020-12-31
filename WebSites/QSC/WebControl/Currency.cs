using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for Currency.
	/// </summary>
	public class Currency:TextBoxControlReqCompVal
	{
		private const string MSGERRORMSGREGULAREXPRESSION24H = "The amount is invalid. Ex: 2.40";
		private string sErrorMsgRegularExpression = MSGERRORMSGREGULAREXPRESSION24H;
		private string sErrorMsgRequired = "The amount is required";
		private string sTextRegularExpression = "^[0-9]*(.[0-9]+)?$";
		private string sTextRequired = "*";
		
		public Currency()
		{
			
		}
	
		
		public override string ErrorMsgRequired
		{
			get
			{
				return sErrorMsgRequired;
			}
			set
			{
				sErrorMsgRequired = value;
			}
		}
		public override string ErrorMsgCompareValidation
		{
			get
			{
				return sErrorMsgRegularExpression;
			}
			set
			{
				sErrorMsgRegularExpression = value;
			}
		}
		public override string TextMsgRequired
		{
			get
			{
				return sTextRequired;
			}
			set
			{
				sTextRequired = value;
			}
		}
		public override string TextCompareValidation
		{
			get
			{
				return sTextRegularExpression;
			}
			set
			{
				sTextRegularExpression = value;
			}
		}

		[Bindable(true),Category("Appearance"),	DefaultValue(-1.0)] 
		public double EmptyValue 
		{
			get 
			{
				double emptyValue = -1.0;

				if(ViewState["EmptyValue"] != null) 
				{
					emptyValue = Convert.ToDouble(ViewState["EmptyValue"]);
				}

				return emptyValue;
			}
			set 
			{
				ViewState["EmptyValue"] = value;
			}
		}

		public double Value 
		{
			get 
			{
				double value;

				if(Text != String.Empty) 
				{
					value = Convert.ToDouble(Text);
				} 
				else 
				{
					value = EmptyValue;
				}

				return value;
			}
			set 
			{
				if(value != EmptyValue) 
				{
					Text = value.ToString();
				} 
				else 
				{
					Text = String.Empty;
				}
			}
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{	
			
			base.Render(output);
			if(Required)
				rfv.RenderControl(output);

			cval.RenderControl(output);
		}
		protected override void OnInit(EventArgs e) 
		{
			if(Required)
			{
				rfv = new RequiredFieldValidator();
				rfv.ControlToValidate =this.ID;
				rfv.EnableClientScript = (this.ClientScript!=false);
				rfv.Text = sTextRequired;
				rfv.ErrorMessage = sErrorMsgRequired;
				rfv.CssClass = base.CssClassError;
				Controls.Add(rfv);
				
			}
						
			BuildRegulareExpression();
			Controls.Add(cval);		
			
		}
		private void BuildRegulareExpression()
		{
			cval = new CompareValidator();
			SetValidationExpression();
			cval.Text = sTextRegularExpression;
			cval.ErrorMessage = sErrorMsgRegularExpression;
			cval.ControlToValidate = this.ID; 
			cval.EnableClientScript = (this.ClientScript !=false);
			cval.CssClass = base.CssClassError;

			
		}
			
		private void SetValidationExpression()
		{
			cval.Type = ValidationDataType.Currency;
			cval.Operator = ValidationCompareOperator.DataTypeCheck;					   
		}	
	}
}
