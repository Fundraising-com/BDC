using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for TextBoxRegularExpression.
	/// </summary>
	public class TextBoxRegEx : TextBoxControlReqRev//System.Web.UI.WebControls.TextBox
	{
		
		private string sErrorMsgRegularExpression = "";
		private string sErrorMsgRequired = "";
		//private string sErrorMsgRegExp = "";
		private string sTextRegularExpression = "*";
		private string sTextRequired = "*";
		private string sRegExp = "";
		
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


		[Bindable(true),Category("Behavior"),	DefaultValue("")]
		public string RegularExpression
		{
			get
			{
				return sRegExp;
			}
			set
			{
				sRegExp = value;
			}
		}
		[Bindable(true),Category("Behavior"),	DefaultValue("")]
		public string RegExErrorMessage
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
			this.rev.ValidationExpression = sRegExp;
		}
	}
}
