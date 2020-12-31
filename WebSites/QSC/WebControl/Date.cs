using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for Date.
	/// </summary>
	public class Date:TextBoxControlReqRev
	{

		private const string MSGERRORMSGREGULAREXPRESSIONCANADA = "The date is invalid. Ex: 24/12/2004";
		private const string MSGERRORMSGREGULAREXPRESSIONUS = "The date is invalid. Ex: 12/24/2004";
		private string sErrorMsgRegularExpression = MSGERRORMSGREGULAREXPRESSIONUS;
		private string sErrorMsgRequired = "The date is required";
		private string sTextRegularExpression = "*";
		private string sTextRequired = "*";
		
		private CountryCheck cdCountryDate = CountryCheck.Canada;


				
		public CountryCheck Country
		{
			get
			{
				return cdCountryDate;
			}
			set
			{
				cdCountryDate= value;
			}
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
		public int Day
		{
			get
			{
				DateTime date = DateTime.Parse(base.Text);
				return date.Day;
			}
			
		}
		public int Year
		{
			get
			{
				DateTime date = DateTime.Parse(base.Text);
				return date.Year;
			}
			
		}
		public int Month
		{
			get
			{
				DateTime date = DateTime.Parse(base.Text);
				return date.Month;
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
			this.Width = new Unit("100px");
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
			if(cdCountryDate == CountryCheck.Canada)
				sErrorMsgRegularExpression = MSGERRORMSGREGULAREXPRESSIONCANADA;
			
			rev = new RegularExpressionValidator();
			SetValidationExpression();
			rev.Text = sTextRegularExpression;
			rev.ErrorMessage = sErrorMsgRegularExpression;
			rev.ControlToValidate = this.ID; 
			rev.EnableClientScript = (this.ClientScript !=false);
			rev.CssClass = base.CssClassError;

			
		}
			
		private void SetValidationExpression()
		{
			if(cdCountryDate == CountryCheck.US)
				rev.ValidationExpression = @"^(?:(?:(?:0?[13578]|1[02])(\/)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$";
			else
				rev.ValidationExpression = @"^(?:(?:(?:0?[13578]|1[02])(\/)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$";
					//TODO:expression for canada					   
		}									
	}
}

