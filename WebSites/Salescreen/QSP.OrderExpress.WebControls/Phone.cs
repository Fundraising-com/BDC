using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for Phone.
	/// </summary>
	/// <Author>Dave Mustaikis</Author>
	/// <Date>10 décembre 2003</Date>
	public class Phone : TextBoxControlReqRev
	{
	
		private string sErrorMsgRegularExpression = "The phone number is invalid";
		private string sErrorMsgRequired = "The phone number is required";
		private string sTextRegularExpression = "*";
		private string sTextRequired = "*";
				
				
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
				rfv.EnableClientScript = (ClientScript!=false);
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
			rev.EnableClientScript = (this.ClientScript!=false);
			rev.CssClass = base.CssClassError;
		}
			
		private void SetValidationExpression()
		{
            this.rev.ValidationExpression = @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}";
		}
		 
	}
}
