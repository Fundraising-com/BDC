using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControls
{
	/// <summary>
	/// TextBoxLengthValidator - authored by Jisun Lee.
	/// </summary>
	[ToolboxData("<{0}:TextBoxLengthValidator runat=server ErrorMessage=\"TextBoxLengthValidator\"></{0}:TextBoxLengthValidator>")]
	public class TextBoxLengthValidator : BaseCompareValidator
	{
		/// <summary>
		/// Specifies the maximum length of the TextBox the control is validating.  If this value
		/// is less than 0, then inputs of any length are considered valid.
		/// </summary>
		[Bindable(true), 
		 Description("TextBoxLengthValidator_MaximumLength"), 
		 Category("Behavior"), 
		 DefaultValue(-1)]
		public int MaximumLength
		{
			get
			{
				object MaxLengthVS = this.ViewState["MaximumLength"];
				if (MaxLengthVS != null)
				{
					return (int) MaxLengthVS;
				}
				return -1;
			}
			set
			{
				this.ViewState["MaximumLength"] = value;
			}
		}
 
		#region Overriden Methods
		/// <summary>
		/// Adds client-side functionality for uplevel browsers by specifying the JavaScript function
		/// to call when validating, as well as a needed parameter (the MaximumLength property value).
		/// </summary>
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				writer.AddAttribute("evaluationfunction", "TextBoxLengthValidatorIsValid");
				writer.AddAttribute("maximumlength", this.MaximumLength.ToString());
			}
		}

		/// <summary>
		/// Checks to ensure that the ControlToValidate property is set to a TextBox
		/// </summary>
		protected override bool ControlPropertiesValid()
		{
			if (base.FindControl(base.GetControlRenderID(base.ControlToValidate)).GetType() != new System.Web.UI.WebControls.TextBox().GetType())
				throw new HttpException("Control to Validate of must be a text box.");

			return base.ControlPropertiesValid();
		}

		/// <summary>
		/// Performs the server-side validation.  If MaximumLength is less than 0, always returns True;
		/// otherwise, returns True only if the ControlToValidate's length is less than or equal to the
		/// specified MaximumLength.
		/// </summary>
		protected override bool EvaluateIsValid()
		{
			if (this.MaximumLength < 0)
				return true;

			string ControlToValidateName = base.GetControlValidationValue(base.ControlToValidate);

			return ControlToValidateName.Length <= System.Convert.ToInt32(this.MaximumLength);
		}

		/// <summary>
		/// Injects the JavaScript function that performs client-side validation for uplevel browsers.
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (base.RenderUplevel)
				Page.RegisterClientScriptBlock("TxtBxLngthValIsValid", 
					@"<script language='javascript'>
					function TextBoxLengthValidatorIsValid(val) 
					{ 
						var value = ValidatorGetValue(val.controltovalidate); 
						if (ValidatorTrim(value).length == 0) return true; 
						if (val.maximumlength < 0) return true; 
						return (value.length <= val.maximumlength);
					}
				</script>");
		}
		#endregion
	}
}
