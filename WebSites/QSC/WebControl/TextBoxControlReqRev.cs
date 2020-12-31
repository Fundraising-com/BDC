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
	public abstract class TextBoxControlReqRev:System.Web.UI.WebControls.TextBox
	{
		public string ClassError = "";
		protected RequiredFieldValidator rfv;
		protected RegularExpressionValidator rev;
	
		[Bindable(true),Category("Behavior"),	DefaultValue(true)]
		public bool ClientScript
		{
			get
			{
				bool clientScript = true;

				if(ViewState["ClientScript"] != null) 
				{
					clientScript = Convert.ToBoolean(ViewState["ClientScript"]);
				}

				return clientScript;
			}
			set
			{
				ViewState["ClientScript"] = value;
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
				bool required = false;

				if(ViewState["Required"] != null) 
				{
					required = Convert.ToBoolean(ViewState["Required"]);
				}

				return required;
			}

			set
			{
				ViewState["Required"] = value;
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
		public abstract string ErrorMsgRegExp
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
		public abstract string TextRegExp
		{
			get;
			set;
		}
			
		
		
	}
}
