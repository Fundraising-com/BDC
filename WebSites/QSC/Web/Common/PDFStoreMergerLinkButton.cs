using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.IO;
using FileStore;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// Summary description for FileStreamLinkButton.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:PDFStoreMergerLinkButton runat=server></{0}:PDFStoreMergerLinkButton>")]
	public class PDFStoreMergerLinkButton : System.Web.UI.WebControls.LinkButton
	{
		public PDFStore Store
		{
			get
			{
				PDFStore pdfStore = null;

				try
				{
					pdfStore = (PDFStore) this.ViewState["PDFStore"];
				} 
				catch { }

				return pdfStore;
			}
			set
			{
				this.ViewState["PDFStore"] = value;
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

			PDFStoreMerger pdfStoreMergerControl = new PDFStoreMerger();
			this.Controls.Add(pdfStoreMergerControl);

			pdfStoreMergerControl.Mode = Mode;
			pdfStoreMergerControl.Merge(Store);

			this.Text = text;
			base.OnClick (e);
		}
	}
}
