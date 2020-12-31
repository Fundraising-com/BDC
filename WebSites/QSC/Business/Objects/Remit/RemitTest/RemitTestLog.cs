using System;
using System.Collections;

namespace Business.Objects.RemitTests
{
	/// <summary>
	/// Message creating and collecting class
	/// </summary>
	/// <remarks>
	/// Madina Saitakhmetova
	/// August 2006
	/// </remarks>
	public class RemitTestLog
	{
		private const string HEADER_TAG = "<tr><td><b>";
		private const string HEADER_TAG_CLOSE = "</b></td></tr>";
		private const string SUCCESS_MESSAGE = "<tr><td>Test passed successfully.</td></tr>";
		private const string SUCCESSFUL_FIX_MESSAGE = "<tr><td>Fix was successfully applied. Retesting was successful.</td></tr>";
		private const string SUCCESSFUL_FIX_MESSAGE_BADENTRIES = "<tr><td>Erroneous entries have been removed from the Remit. File containing entries that need to be fixed is attached in the e-mail.</td></tr>";
		private const string FAIL_MESSAGE = "<tr><td>Test failed.</td></tr>";
		private const string FIX_FAIL_MESSAGE = "<tr><td>Fix failed. Remit processing aborted. Error summary is sent to IT.</td></tr>";
		private const string NEW_LINE = "<tr><td>&nbsp;</td></tr>";

		private string _htmlMessage;
		private AttachmentCollection _attachments;

		public string htmlMessage
		{
			set 
			{
				_htmlMessage = value;
			}

			get
			{
				return _htmlMessage;
			}
		}

		public AttachmentCollection attachments 
		{
			get 
			{
				if(_attachments == null) 
				{
					_attachments =  new AttachmentCollection();
				}

				return _attachments;
			}
		}

		public RemitTestLog()
		{
			_htmlMessage = "<table style=\"font-family:arial;\"><tr><td><h4>Remit Validation</h4></td></tr><tr><td>&nbsp;</td></tr>";
		}

		public void AddNewHeader(string headerText)
		{
			_htmlMessage += HEADER_TAG + headerText + HEADER_TAG_CLOSE;
		}

		public void AddNewLine(string lineText)
		{
			_htmlMessage += "<tr><td>" + lineText + "</td></tr>" + NEW_LINE;
		}

		public void AddNewSuccessMsg()
		{
			_htmlMessage += SUCCESS_MESSAGE + NEW_LINE;
		}

		public void AddNewSuccessfulFixMsg()
		{
			_htmlMessage += SUCCESSFUL_FIX_MESSAGE + NEW_LINE;
		}

		public void AddNewSuccessfulBadEntriesFixMsg()
		{
			_htmlMessage += SUCCESSFUL_FIX_MESSAGE_BADENTRIES + NEW_LINE;		
		}

		public void AddNewFailMsg()
		{
			_htmlMessage += FAIL_MESSAGE + NEW_LINE;
		}

		public void AddNewFixFailMsg()
		{
			_htmlMessage += FIX_FAIL_MESSAGE + NEW_LINE;
		}	
	
		public void CloseHtmlMessage()
		{
			_htmlMessage += "</table>";
		}	
	}
}
