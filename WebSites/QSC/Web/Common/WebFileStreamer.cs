// Created by:	Benoit Nadon
// Date:		2006-01-01

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Business.ReportExecution;
using QSP.WebControl;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// Allows to stream a file and show it as the client response.
	/// Solution to eliminate security issues with redirecting to real file.
	/// NOTE: THE PAGE NEEDS TO INHERIT QSP.WebControl.PageSwitchStatePage
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:WebFileStreamer runat=server></{0}:WebFileStreamer>")]
	public class WebFileStreamer : System.Web.UI.WebControls.WebControl, IPageSwitchStateControl
	{
		private const string WEBFILEPAGE_URL = "/QSPFulfillment/Common/WebFilePage.aspx";

		private new PageSwitchStatePage Page 
		{
			get 
			{
				return (PageSwitchStatePage) base.Page;
			}
		}

		/// <summary>
		/// Name of the file to stream.
		/// </summary>
		[Bindable(true), 
		Category("Data"), 
		DefaultValue("")] 
		public string FilePath 
		{
			get 
			{
				string filePath = String.Empty;

				if(ViewState["FilePath"] != null) 
				{
					filePath = ViewState["FilePath"].ToString();
				}

				return filePath;
			}
			set 
			{
				ViewState["FilePath"] = value;
			}
		}

		[Bindable(true), 
		Category("Data"), 
		DefaultValue("")] 
		private string MimeType
		{
			get 
			{
				string mimeType = String.Empty;

				if(ViewState["MimeType"] != null) 
				{
					mimeType = ViewState["MimeType"].ToString();
				}

				return mimeType;
			}
			set 
			{
				ViewState["MimeType"] = value;
			}
		}

		/// <summary>
		/// File output behavior when the streaming is started.
		/// </summary>
		[Bindable(true), 
		Category("Data"), 
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

		/// <summary>
		/// Streams the file and outputs it in the response.
		/// </summary>
		public virtual void Stream() 
		{
			Stream(FilePath);
		}

		/// <summary>
		/// Streams the file and outputs it in the response.
		/// </summary>
		/// <param name="filePath">File Path</param>
		public virtual void Stream(string filePath) 
		{
			Stream(filePath, MimeType);
		}

		/// <summary>
		/// Streams the file and outputs it in the response.
		/// </summary>
		/// <param name="filePath">File Path</param>
		/// <param name="mimeType">File's Mime-Type</param>
		public virtual void Stream(string filePath, string mimeType) 
		{
			int pageSwitchStateID;

			FilePath = filePath;
			MimeType = mimeType;

			pageSwitchStateID = Page.SavePageSwitchState();

			if(Mode == FilePageMode.Internal) 
			{
				Context.Response.Redirect(WEBFILEPAGE_URL + "?PageSwitchStateID=" + pageSwitchStateID.ToString(), false);
			} 
			else if(Mode == FilePageMode.PopUp) 
			{
				Context.Response.Write("<script>window.open('" + WEBFILEPAGE_URL + "?IsNewWindow=true&PageSwitchStateID=" + pageSwitchStateID.ToString() + "','',\"toolbar = yes,status=yes,scrollbars=yes,resizable=yes, width=800, height=550\");</script>");
			}
		}

		/// <summary>
		/// Removes the default span tag rendered by a web control.
		/// </summary>
		protected override void Render(HtmlTextWriter writer) { }

		public void SavePageSwitchState(int pageSwitchStateID)
		{
			Page.PageSwitchState[pageSwitchStateID]["FilePath"] = FilePath;
			Page.PageSwitchState[pageSwitchStateID]["MimeType"] = MimeType;
		}
	}
}
