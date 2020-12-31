using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using QSP.WebControl.DataAccess.Common;
using QSP.WebControl.ClientPersistentProperties;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for DropDownListReq.
	/// </summary>
	/// <Author>Benoit Nadon</Author>
	/// <Date>2005/04/11</Date>

	public class DropDownListReq : DropDownListCached,IClientPersistentPropertyContainer
	{
		public string ClassError = "";
		protected RequiredFieldValidator rfv;
		
		private string sErrorMsgRegularExpression = "";
		private string sErrorMsgRequired = "";
		private string sTextRegularExpression = "*";
		private string sTextRequired = "*";
		private const string REGEXREQUIRED = @"^([^0])(.+)$";

		private ClientPersistentPropertiesManagerControl clientPersistentPropertiesManagerControl = new ClientPersistentPropertiesManagerControl();
		
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
		[Bindable(true),Category("Appearance"),	DefaultValue("")] 
		public string InitialText
		{
			get
			{
				string initialText = String.Empty;

				if(ViewState["InitialText"] != null) 
				{
					initialText = ViewState["InitialText"].ToString();
				}

				return initialText;
			}
			set
			{
				ViewState["InitialText"] = value;
			}
		}
		[Bindable(true),Category("Appearance"),	DefaultValue("")] 
		public string InitialValue 
		{
			get
			{
				string initialValue = String.Empty;

				if(ViewState["InitialValue"] != null) 
				{
					initialValue = ViewState["InitialValue"].ToString();
				}

				return initialValue;
			}
			set
			{
				ViewState["InitialValue"] = value;
			}
		}

		[Bindable(true),Category("Appearance"),	DefaultValue(false)]
		public bool ShowFirstLine 
		{
			get
			{
				bool showFirstLine = true;

				if(ViewState["ShowFirstLine"] != null) 
				{
					showFirstLine = Convert.ToBoolean(ViewState["ShowFirstLine"]);
				}

				return showFirstLine;
			}
			set 
			{
				ViewState["ShowFirstLine"] = value;
			}
		}

		/// <summary>
		/// Message error when is required
		/// </summary>
		public string ErrorMsgRequired
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
		public string ErrorMsgRegExp
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
		public string TextMsgRequired
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
		public string TextRegExp
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

		protected override ControlCollection CreateControlCollection() 
		{ 
			return new ControlCollection(this) ; 
		}
		public ClientPersistentPropertiesManagerControl ClientPersistentProperties 
		{
			get 
			{
				return clientPersistentPropertiesManagerControl;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			if(Required)
			{
				rfv = new RequiredFieldValidator();
				//rfv.ID = this.ClientID + "RequiredFieldValidator";
				rfv.ControlToValidate =this.ID;
				rfv.EnableClientScript = (this.ClientScript!=false);
				rfv.Text = sTextRequired;
				rfv.ErrorMessage = sErrorMsgRequired;
				rfv.CssClass = CssClassError;
				rfv.InitialValue = this.InitialValue;
				Controls.Add(rfv);
			}

			ClientPersistentProperties.ID = this.ID + "_ClientPersistentProperties";
			Controls.Add(ClientPersistentProperties);
		}

		protected override void Render(HtmlTextWriter output)
		{
			if(this.AutoPostBack) 
			{
				this.Attributes["onChange"] = "document.forms[0].fireEvent(\"onsubmit\");" + this.Attributes["onChange"];
			}

			base.Render (output);

			if(rfv != null)
				rfv.Enabled = Required;

			if(Required) 
			{
				rfv.RenderControl(output);
			}

			ClientPersistentProperties.RenderControl(output);
		}


		public override void DataBind()
		{
			base.DataBind();
			
			if(ShowFirstLine) 
			{
				this.Items.Insert(0, new ListItem(InitialText, InitialValue));
			}
		}
	}
}
