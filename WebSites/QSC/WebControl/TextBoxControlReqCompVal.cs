using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for TextBoxControl.
	/// </summary>
	/// <Author>Dave Mustaikis</Author>
	/// <Date>10 décembre 2003</Date>
	/// <remarks>Super class for text box control with behavior</remarks>
	public abstract class TextBoxControlReqCompVal: System.Web.UI.WebControls.TextBox
	{
		public string ClassError = "";
		private bool bClientScript= true;
		protected RequiredFieldValidator rfv;
		protected CompareValidator cval;
		private bool IsRequired;

		public TextBoxControlReqCompVal()
		{
			
		}
	
		
			
	
		[Bindable(true),Category("Behavior"),	DefaultValue(false)]
		public bool ClientScript
		{
			get
			{
				return bClientScript;
			}

			set
			{
				bClientScript = value;
			}
		}
		[Bindable(true),Category("Appearance"),	DefaultValue("")]
		public string CssClassError 
		{
			get
			{
				return ClassError;
			}

			set
			{
				ClassError = value;
			}
		}
		[Bindable(true),Category("Appearance"),	DefaultValue(false)] 
		public bool Required 
		{
			get
			{
				return IsRequired;
			}

			set
			{
				IsRequired = value;
			}
		}
		public abstract string ErrorMsgRequired
		{
			get;
			set;
		}
		/// <summary>
		/// Message error when is not a valid email
		/// </summary>
		public abstract string ErrorMsgCompareValidation
		{
			get;
			set;
		}
		/// <summary>
		/// Text when is required
		/// </summary>
		public abstract string TextMsgRequired
		{
			get;
			set;
		}
		/// <summary>
		/// Text when is not valid email
		/// </summary>
		public abstract string TextCompareValidation
		{
			get;
			set;
		}
			
		
		
	}
}

	

