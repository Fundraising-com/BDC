using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for Time.
	/// </summary>
	public class Time:TextBoxControlReqRev
	{
		private const string MSGERRORMSGREGULAREXPRESSION24H = "The time is invalid. Ex: 13:30";
		private const string MSGERRORMSGREGULAREXPRESSION12H = "The time is invalid. Ex: 1:40 AM";
		private string sErrorMsgRegularExpression = MSGERRORMSGREGULAREXPRESSION24H;
		private string sErrorMsgRequired = "The time is required";
		private string sTextRegularExpression = "*";
		private string sTextRequired = "*";
		private bool bIs24H	= true;
		
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
		public int Hour
		{
			get
			{
				string[] ss1 = base.Text.Split(':');
				return Convert.ToInt32(ss1[1]);
			}
			
		}
		public int Minute
		{
			get
			{
				string[] ss1 = base.Text.Split(':');
				return Convert.ToInt32(ss1[0]);
			}
			
		}
		public bool Is24H
		{
			get{return bIs24H;}
			set{bIs24H = value;}
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
			if(!Is24H)
				sErrorMsgRegularExpression = MSGERRORMSGREGULAREXPRESSION12H;

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
			if(bIs24H)
				rev.ValidationExpression = @"(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])(\s{0,1})$)";
			else
				rev.ValidationExpression = @"0?([1-9]|1[012])\:([0-5]\d)\ (AM|PM|am|pm|A.M.|P.M.|a.m.|p.m.)";
										   
		}									
	}
}
