// Created by:	Benoit Nadon
// Date:		2006-01-13

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using FileStore;
using QSP.WebControl;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// Allows to stream a file and show it as the client response.
	/// Solution to eliminate security issues with redirecting to real file.
	/// NOTE: THE PAGE NEEDS TO INHERIT QSP.WebControl.PageSwitchStatePage
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:PDFStoreMerger runat=server></{0}:PDFStoreMerger>")]
	public class PDFStoreMerger : System.Web.UI.WebControls.WebControl, IPageSwitchStateControl
	{
		private const string PDFSTOREPAGE_URL = "/QSPFulfillment/Common/PDFStorePage.aspx";

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
		public PDFStore Store
		{
			get 
			{
				PDFStore pdfStore = null;

				try
				{
					pdfStore = (PDFStore) ViewState["PDFStore"];
				} 
				catch { }

				return pdfStore;
			}
			set 
			{
				ViewState["PDFStore"] = value;
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
		public virtual void Merge() 
		{
			Merge(Store);
		}

		/// <summary>
		/// Streams the file and outputs it in the response.
		/// </summary>
		/// <param name="filePath">File Path</param>
		public virtual void Merge(PDFStore pdfStore) 
		{
			int pageSwitchStateID;

			Store = pdfStore;

			pageSwitchStateID = Page.SavePageSwitchState();

			if(Mode == FilePageMode.Internal) 
			{
				Context.Response.Redirect(PDFSTOREPAGE_URL + "?PageSwitchStateID=" + pageSwitchStateID.ToString(), false);
			} 
			else if(Mode == FilePageMode.PopUp) 
			{
				Context.Response.Write("<script>window.open('" + PDFSTOREPAGE_URL + "?IsNewWindow=true&PageSwitchStateID=" + pageSwitchStateID.ToString() + "','',\"toolbar = yes,status=yes,scrollbars=yes,resizable=yes, width=800, height=550\");</script>");
			}
		}

		/// <summary>
		/// Removes the default span tag rendered by a web control.
		/// </summary>
		protected override void Render(HtmlTextWriter writer) { }

		public void SavePageSwitchState(int pageSwitchStateID)
		{
			Page.PageSwitchState[pageSwitchStateID]["PDFStore"] = Store;
		}
	}
}
