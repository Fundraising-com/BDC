using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControls
{
	/// <summary>
	/// TextLengthValidator checks the length of text in a textbox to ensure it is below a 
	/// specified limit.
	/// </summary>
	public class TextLengthValidator : BaseValidator
	{
		protected int m_iMaxLength;						// Maximum length allowed for the string
		protected bool m_bDisplayLength = false;		// Whether we should attempt and display the characters entered string
		protected string m_sText;							// Holds the original error text

		/// <summary>
		/// The maximum number of characters that can be entered into the textbox.
		/// </summary>
		public int MaxLength
		{
			get { return m_iMaxLength; }
			set { m_iMaxLength = value; }
		}

		/// <summary>
		/// A boolean indicating whether we want to display the number of characters entered if over limit.
		/// </summary>
		public bool DisplayCharactersEntered
		{
			get { return m_bDisplayLength; }
			set { m_bDisplayLength = value; }
		}

		public override string Text
		{
			get { return base.Text; }
			set
			{
				if (m_sText == null) m_sText = value;
				base.Text = value;
			}
		}

		private string GetScriptUrl()
		{
			System.Text.StringBuilder strBuild = new System.Text.StringBuilder();
			strBuild.Append("<script language=javascript>\n");	
			strBuild.Append("	var text;		\n");
			strBuild.Append("	function checkLength(val){		\n");
			strBuild.Append("		if (text == null) text = val.innerHTML; \n");
			strBuild.Append("		var value = ValidatorTrim(ValidatorGetValue(val.controltovalidate)); \n");
			strBuild.Append("		if (value.length > val.maxLength) {\n");
			strBuild.Append("			val.innerHTML = text;		\n");
			strBuild.Append("			if (val.displayEntered.toLowerCase() == 'true')			\n");
			strBuild.Append("				val.innerHTML += ' (' + value.length + ' characters entered)';		\n");
			strBuild.Append("		} else {				\n");
			strBuild.Append("			return true;		\n");
			strBuild.Append("		}						\n");
			strBuild.Append("	}							\n");
			strBuild.Append("</script>	\n");
			

			return strBuild.ToString();
//
//			string sTemp = @"<script language=""javascript"" src=""{0}""></script>";
//			string sRoot = HttpContext.Current.Request.ApplicationPath;
//			if (!sRoot.EndsWith("/")) sRoot += "/";
//			string sScriptName = sRoot + "Script/CheckLength.js";
//			return string.Format( sTemp, sScriptName );
		}

		protected override bool EvaluateIsValid()
		{
			string sValue = GetControlValidationValue(ControlToValidate);

			// If the validator is not bound to a control
			if (sValue == null) return true;

			if (sValue.Length > m_iMaxLength)
			{
				this.Text = m_sText;
				if (DisplayCharactersEntered)
				{
					this.Text += " (" + sValue.Length.ToString() + " characters entered)";
				}
				return false;
			}

			return true;
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender (writer);

			if (RenderUplevel)
			{
				writer.AddAttribute("evaluationfunction", "checkLength");
				writer.AddAttribute("maxLength", MaxLength.ToString());
				writer.AddAttribute("displayEntered", DisplayCharactersEntered.ToString());
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			if (RenderUplevel)
			{
				string sScriptKey = typeof(TextLengthValidator).FullName;
				// Only register the script if it has not already been registered
				if (!Page.IsClientScriptBlockRegistered(sScriptKey))
					Page.RegisterClientScriptBlock(sScriptKey, GetScriptUrl());
			}
		}

	}
}
