using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using QSP.WebControl.DataAccess.Common;
using QSP.WebControl.ClientPersistentProperties;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for TextBoxReq.
	/// </summary>
	/// <Author>Benoit Nadon</Author>
	/// <Date>14 mars 2005</Date>

	public class TextBoxReq : TextBoxControlReqRev, IClientPersistentPropertyContainer
	{
		private string sErrorMsgRegularExpression = "";
		private string sErrorMsgRequired = "";
		private string sTextRegularExpression = "*";
		private string sTextRequired = "*";
		private const string REGEXREQUIRED = @"^.*$";
		private ClientPersistentPropertiesManagerControl clientPersistentPropertiesManagerControl = new ClientPersistentPropertiesManagerControl();
		
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

		public ClientPersistentPropertiesManagerControl ClientPersistentProperties 
		{
			get 
			{
				return clientPersistentPropertiesManagerControl;
			}
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{	
			base.Render(output);

			if(rfv != null)
				rfv.Enabled = Required;

			if(Required) 
			{
				rfv.RenderControl(output);
			}

			ClientPersistentProperties.RenderControl(output);
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

			ClientPersistentProperties.ID = this.ID + "_ClientPersistentProperties";
			Controls.Add(ClientPersistentProperties);
		}
	}
}
