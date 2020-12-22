using System;

namespace efundraising.RecaudarFondosWeb.Components.Server.Omniture
{
	/// <summary>
	/// Summary description for RecaudarFondosOmniture.
	/// </summary>
	public class RecaudarFondosOmniture : efundraising.Tracking.Omniture
	{
		public RecaudarFondosOmniture(string JSFileName): base(JSFileName) {
			
		}
		
		protected override string PageFormat {
			get {
				return "RF US: {0}: {1}"; 
			}
		}

		public override void SetPageNameAndCategory(string PageCategory, string PageName) {
			this.PageName = string.Format(PageFormat, PageCategory, PageName);
			this.Channel = PageCategory;
		}
	}
}
