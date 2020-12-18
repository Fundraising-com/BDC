using System;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for Phone with default REGEX.
	/// </summary>
	/// <Author>Dave Mustaikis</Author>
	/// <Date>10 décembre 2003</Date>
	public class EMail : TextBoxControlReqRev
	{
		
		private string sErrorMsgRegularExpression = "The E-mail address is invalid";
		private string sErrorMsgRequired = "The E-mail is required";
		private string sTextRegularExpression = "*";
		private string sTextRequired = "*";
		private const string REGEXEMAIL = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"; 
		#region
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
		#endregion

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
			//Assing a requiered field validator to the control
			if(Required)
			{
				rfv = new RequiredFieldValidator();
				rfv.ControlToValidate =this.ID;
				rfv.EnableClientScript = (this.ClientScript!=false);
				rfv.Text = sTextRequired;
				rfv.ErrorMessage = sErrorMsgRequired;
				rfv.CssClass = CssClassError;
				Controls.Add(rfv);
				
			}
						
			BuildRegulareExpression();
			Controls.Add(rev);		
			
		}
		/// <summary>
		/// Assign a RegularExpressionValidator to the control
		/// </summary>
		private void BuildRegulareExpression()
		{
			rev = new RegularExpressionValidator();
			SetValidationExpression();
			rev.Text = sTextRegularExpression;
			rev.ErrorMessage = sErrorMsgRegularExpression;
			rev.ControlToValidate = this.ID; 
			rev.EnableClientScript = (this.ClientScript !=false);
			rev.CssClass = CssClassError;
		}
		/// <summary>
		/// Validation Expression for an EMail
		/// </summary>
		private void SetValidationExpression()
		{
            rev.ValidationExpression = REGEXEMAIL;
		}
		 
	}
}
