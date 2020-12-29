//
// 2004-11-30 - Stephen Lim - New class.
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;

namespace GA.BDC.Core.Web.UI.MasterPages
{
	[ToolboxData("<{0}:MasterPage runat=server></{0}:MasterPage>"),
	ToolboxItem(typeof(WebControlToolboxItem)),
	Designer(typeof(ReadWriteControlDesigner))]
	public class MasterPage : System.Web.UI.HtmlControls.HtmlContainerControl
	{
		#region Fields
		private string templateFile;
		private Control template = null;
		private ArrayList contents = new ArrayList();
		#endregion

		#region Constructors
		public MasterPage() 
		{

		}
		#endregion

		#region Methods
		protected override void AddParsedSubObject(object obj) 
		{
			// Only add Content objects
            if (obj is GA.BDC.Core.Web.UI.MasterPages.Content) 
			{
				this.contents.Add(obj);
			}
		}

		protected override void OnInit(EventArgs e) 
		{
			this.BuildMasterPage();
			this.BuildContents();
			base.OnInit(e);
		}

		private void BuildMasterPage() 
		{
			if (Master == "") 
			{
				throw new Exception("Master property for MasterPage must be defined");
			}
			this.template = this.Page.LoadControl(Master);
			//this.template.ID = this.ID + "_MasterTemplate";
			
			int count = this.template.Controls.Count;
			for (int index = 0; index < count; index++) 
			{
				Control control = this.template.Controls[0];
				this.template.Controls.Remove(control);
				if (control.Visible) 
				{
					this.Controls.Add(control);
				}
			}
			this.Controls.AddAt(0, this.template);
		}

		private void BuildContents() 
		{
			foreach (Content content in this.contents) 
			{
				Control region = this.FindControl(content.ContentPlaceHolderID);
                if (region == null || !(region is GA.BDC.Core.Web.UI.MasterPages.ContentPlaceHolder)) 
				{
					throw new Exception("ContentPlaceHolder with ID '" + content.ContentPlaceHolderID + "' must be defined");
				}
				region.Controls.Clear();
				
				int count = content.Controls.Count;
				for (int index = 0; index < count; index++) 
				{
					Control control = content.Controls[0];
					content.Controls.Remove(control);
					region.Controls.Add(control);
				}
			}
		}

		protected override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer) {}
		protected override void RenderEndTag(System.Web.UI.HtmlTextWriter writer) {}
		#endregion

		#region Properties
		[Category("MasterPage"), Description("Path of Template User Control")] 
		public string Master 
		{
			get { return this.templateFile; }
			set { this.templateFile = value; }
		}
		#endregion
	}
}
