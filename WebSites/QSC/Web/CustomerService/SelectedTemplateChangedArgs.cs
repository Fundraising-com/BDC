using System;
using Business.Objects;

namespace QSPFulfillment.CustomerService
{
	public delegate void SelectedTemplateChangedEventHandler(object sender, SelectedTemplateChangedArgs e);
	/// <summary>
	/// Summary description for SelectedTemplateChangedArgs.
	/// </summary>
	public class SelectedTemplateChangedArgs : System.EventArgs
	{
		private LetterTemplateItem selectedTemplate = null;

		public SelectedTemplateChangedArgs(LetterTemplateItem selectedTemplate)
		{
			this.selectedTemplate = selectedTemplate;
		}
		
		public LetterTemplateItem SelectedTemplate
		{
			get
			{
				return selectedTemplate;
			}
		}
	}
}
