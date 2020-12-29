//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	February 25, 2005	-	Louis Turmel	Code Comments
//

using System;

namespace GA.BDC.Core.Web.UI.Uploader.ContactList
{
	
	/// <summary>
	/// Internal class containing somes public const variables used for
	/// the Web Server Controls - ContactlistUploader
	/// </summary>
	///	<remarks>This class cannot be use externaly of this namespace
	/// and inheritable</remarks>
	internal sealed class UploaderVars{
		
		#region ViewStates Variables Name

		public const string __ContactListType = "ContactListType";
		public const string __ShowContactListChooser = "ShowContactListChooser";
		public const string __ListType = "ListType";
		public const string __UploadTextButton = "UploadTextButton";
		public const string __ContactListFileType = "ContactListFileType";
		public const string __CssDropDownStyle = "CssDropDownStyle";			

		#endregion

		#region Category for VS.Net IDE Properties Box

		public const string __VSDesignApp = "Appearance";
		public const string __VSDesignTextProp = "Text Properties";
		public const string __VSDesignToolTips = "Tool Tips";
		public const string __VSDesignCssClass = "CssClass";
		public const string __VSDesignUploader = "Working Folder";
		
		#endregion

		
	}
}
