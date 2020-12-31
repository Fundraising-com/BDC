using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControl
{

	public enum CountryCheck {Canada,US,All};
	/// <summary>
	/// Summary description for PostalCode.
	/// </summary>
	public class PostalCode:TextBoxControlReqRev, ISearch
	{
		private string mParameterName = "";
		private string mContentType = "";
		private bool bValidation = true;

		private const string MSGERRORMSGREGULAREXPRESSIONCANADA = "The postal code is invalid. Ex: H1H1H1";
		private const string MSGERRORMSGREGULAREXPRESSIONUS = "The ZIP code is invalid. Ex: 11111-1111";
		private string sErrorMsgRegularExpression = MSGERRORMSGREGULAREXPRESSIONUS;
		private const string sErrorMsgRequiredUS = "The ZIP code is required";
		private const string sErrorMsgRequiredCanada = "The Postal Code is required";
		private string sErrorMsgRequired;
		private string sTextRegularExpression = "*";
		private string sTextRequired = "*";
		
		private CountryCheck cdCountryDate = CountryCheck.Canada;

		public PostalCode()
		{
			
		}
					
		public CountryCheck TypeDate
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

		public override int MaxLength
		{
			get
			{
				int maxLength = 10;

				if(cdCountryDate == CountryCheck.Canada)
				{
					maxLength = 6;
				}

				return maxLength;
			}
			set { }
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
				SetErrorMsgRequired();
			
				rfv.CssClass = base.CssClassError;
				Controls.Add(rfv);
				
			}		
		}

		protected override void OnPreRender(EventArgs e)
		{
			BuildRegulareExpression();
			Controls.Add(rev);

			base.OnPreRender (e);
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
				rev.ValidationExpression = @"(^\d{5}$)|(^\d{5}-\d{4}$)";
			else if(cdCountryDate == CountryCheck.Canada)
				rev.ValidationExpression = @"^[A-Z][0-9][A-Z][0-9][A-Z][0-9]$"; //@"^\D{1}\d{1}\D{1}\-?\d{1}\D{1}\d{1}";
			else
				rev.ValidationExpression = @"^((\d{5}$)|(^\d{5}-\d{4}))|([A-Z][0-9][A-Z][0-9][A-Z][0-9])$";
										   
		}	
		private void SetErrorMsgRequired()
		{
			if(cdCountryDate == CountryCheck.Canada)
				rfv.ErrorMessage = sErrorMsgRequiredCanada;
			else
				rfv.ErrorMessage = sErrorMsgRequiredUS;
		}

		[Bindable(true),Category("SqlQuery"),DefaultValue("")]
		public string ParameterName
		{
			get
			{
				return this.mParameterName;
			}
			set
			{	
				
				this.mParameterName = value;	
			}
		}
		[Bindable(true),Category("SqlQuery"),DefaultValue("")]
		public string Value
		{
			get{return base.Text;}
		}

		[Bindable(true), Category("SqlQuery"), DefaultValue("")]
		public string ContentType
		{
			get 
			{
				return this.mContentType;
			}
			set  
			{
				this.mContentType = value;
			}
		}

		[Bindable(true), Category("SqlQuery"), DefaultValue(true)]
		public bool Validation
		{
			get
			{
				return this.bValidation;
			}
			set 
			{
				this.bValidation = value;
			}
		}
	}
}




