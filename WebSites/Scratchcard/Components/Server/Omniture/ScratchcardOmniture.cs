using System;

namespace GA.BDC.WEB.ScratchcardWeb.Components.Server.Omniture
{
	/// <summary>
	/// Summary description for ScratchcardOmniture.
	/// </summary>
	public class ScratchcardOmniture : GA.BDC.Core.Tracking.Omniture
	{
		public ScratchcardOmniture(string JSFileName): base(JSFileName) 
		{
					
		}
				
		protected override string PageFormat 
		{
		get 
		{
		return "SC US: {0}: {1}"; 
		}
		}

		public override void SetPageNameAndCategory(string PageCategory, string PageName) 
		{
		this.PageName = string.Format(PageFormat, PageCategory, PageName);
		this.Channel = PageCategory;
		}
	}
}
