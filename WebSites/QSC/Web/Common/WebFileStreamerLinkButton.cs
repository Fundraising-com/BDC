using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.IO;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// Summary description for FileStreamLinkButton.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:WebFileStreamerLinkButton runat=server></{0}:WebFileStreamerLinkButton>")]
	public class WebFileStreamerLinkButton : System.Web.UI.WebControls.LinkButton
	{
		[Bindable(true), Category("Navigation"), DefaultValue("")]
		public string NavigateUrl 
		{
			get
			{
				string navigateUrl = String.Empty;

				if(this.ViewState["NavigateUrl"] != null) 
				{
					navigateUrl = this.ViewState["NavigateUrl"].ToString();
				}

				return navigateUrl;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		[Bindable(true), Category("Navigation"), DefaultValue("")]
		public string MimeType 
		{
			get 
			{
				string mimeType = "application/pdf";

				if(this.ViewState["MimeType"] != null) 
				{
					mimeType = this.ViewState["MimeType"].ToString();
				}

				return mimeType;
			}
			set 
			{
				this.ViewState["MimeType"] = value;
			}
		}

		/// <summary>
		/// File output behavior when the button is clicked.
		/// </summary>
		[Bindable(true), 
		Category("Navigation"), 
		DefaultValue("")]
		public FilePageMode Mode 
		{
			get 
			{
				FilePageMode mode = FilePageMode.Internal;

				try 
				{
					mode = (FilePageMode) ViewState["Mode"];
				}
				catch { }

				return mode;
			}
			set 
			{
				ViewState["Mode"] = value;
			}
		}

		protected override void OnClick(EventArgs e)
		{
			string text = this.Text;

			WebFileStreamer wfStreamerControl = new WebFileStreamer();
			this.Controls.Add(wfStreamerControl);

			wfStreamerControl.Mode = Mode;
			wfStreamerControl.Stream(NavigateUrl, MimeType);

			this.Text = text;
			base.OnClick (e);
		}
	}
}
