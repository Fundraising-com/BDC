using System;

namespace EFundraisingCRMWeb.Components.User.Sales
{
	/// <summary>
	/// Summary description for TextOnCardEventArgs.
	/// </summary>
	
	public class TextOnCardEventArgs : EventArgs
	{
		private int scratchBookID;
		private string text;
		public int ScratchBookID
		{
			get { return scratchBookID; }
			set { scratchBookID = value; }
		}
		public string Text
		{
			get { return text; }
			set { text = value; }
		}
	}
}
