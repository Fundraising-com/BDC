using System;

namespace EFundraisingCRMWeb
{
	/// <summary>
	/// Summary description for IPage.
	/// </summary>
	interface IPage {
		string PageInformation { get; }
		string PageDescription { get; }
		void Search(string searchQuery);
		void Create(string redirection);
	}
}
