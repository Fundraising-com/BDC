using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using QSP.WebControl.DataAccess.Common;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for TextBoxFloat.
	/// </summary>
	/// <Author>Benoit Nadon</Author>
	/// <Date>14 mars 2005</Date>

	public class TextBoxFloat : TextBoxControlReqRev
	{
		
		
		private string sErrorMsgRegularExpression = "";
		private string sErrorMsgRequired = "";
		private string sTextRegularExpression = "*";
		private string sTextRequired = "*";
		private const string REGEXNUMERIC = @"^([0-9]*|[0-9]{1,3},([0-9]{3},)*[0-9]{3})(\.[0-9]+)?$";
		
		/// <summary>
		/// Message error when is required
		/// </summary>
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
		/// <summary>
		/// Message error when is not a valid email
		/// </summary>
		public override string ErrorMsgRegExp
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
		/// <summary>
		/// Text when is required
		/// </summary>
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
		/// <summary>
		/// Text when is not valid email
		/// </summary>
		public override string TextRegExp
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

			rev.RenderControl(output);
		}
		protected override void OnInit(EventArgs e) 
		{
			//if(Required)
			//{
				rfv = new RequiredFieldValidator();
				rfv.ControlToValidate =this.ID;
				rfv.EnableClientScript = (this.ClientScript!=false);
				rfv.Text = sTextRequired;
				rfv.ErrorMessage = sErrorMsgRequired;
				rfv.CssClass = base.CssClassError;
				Controls.Add(rfv);
			//}
						
			BuildRegulareExpression();
					
			Controls.Add(rev);
			
		}
		private void BuildRegulareExpression()
		{
			rev = new RegularExpressionValidator();
			SetValidationExpression();
			rev.Text = sTextRegularExpression;
			rev.ErrorMessage = sErrorMsgRegularExpression;
			rev.ControlToValidate =this.ID; 
			rev.EnableClientScript = (ClientScript!=false);
			rev.CssClass = base.CssClassError;
		}
			
		private void SetValidationExpression()
		{
            this.rev.ValidationExpression = REGEXNUMERIC;
		}
		 
	}
}
