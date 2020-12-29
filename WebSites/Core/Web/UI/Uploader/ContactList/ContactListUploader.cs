//
//	February 28, 2005	-	Louis Turmel	Code Comments
//	March	 04, 2005	-	Louis Turmel	Event Handler Modification
//

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;	
using System.ComponentModel;

using GA.BDC.Core.AddressBooks;
using GA.BDC.Core.AddressBooks.From;
using GA.BDC.Core.AddressBooks.Types;

using GA.BDC.Core.Web.UI.Uploader.ContactList.Events;

namespace GA.BDC.Core.Web.UI.Uploader.ContactList
{
	
	/// <summary>
	/// ASP.Net Web Server Controls, used to upload single file from Client
	///	to Server.
	///	You can use Web Server Controls attribute inside the declaration of HTML Tag 
	///	for this control to set your Control preferences
	/// </summary>
	/// <example>
	/// <code>
	/// // For using this control, you must add controls in your Tool Box Tab. For this. Rigth Click
	/// // with you mouse on the Tool Box Tab of your choice and select 'Add/Remove Object'
	/// // After you should browse to select the dll file of this controls
	/// 
	/// // This is a simple example of using UploaderControl on WebForm
	/// 
	/// using System;
	///	using System.Collections;
	///	using System.ComponentModel;
	///	using System.Data;
	///	using System.Drawing;
	///	using System.Web;
	///	using System.Web.SessionState;
	///	using System.Web.UI;
	///	using System.Web.UI.WebControls;
	///	using System.Web.UI.HtmlControls;
	///	
	///	// Add the following namespace
	///	using efundraising;
	///	using efundraising.Web;
	///	using efundraising.Web.UI.
	///	using efundraising.Web.Uploader;
	///	using efundraising.Web.Uploader.ContactList;
	///	
	///	namespace mySimpleUploaderWebPage {
	///		
	///		public class mySimpleUploaderWebPage : System.Web.UI.Page {
	///			
	///			protected efundraising.Web.UI.Uploader.ContactListUploader _myContactList;
	///			
	///			private void Page_Load(object sender, System.EventArgs e) {
	///				if(!base.IsPostBack) {
	///					this.InitializeUploaderControls();
	///				}
	///			}
	///			
	///			private void InitializeUploaderControls() {
	///				FileType[] ofiletype = new FileType[2];
	///				ofiletype[0] = (FileType)Enum.Parse(typeof(FileType), "pst");
	///				ofiletype[1] = (FileType)Enum.Parse(typeof(FileType), "csv");
	///				ContactListType oCtcType = (ContactListType)Enum.Parse(typeof(ContactListType), "Excel");
	///				this._myContactList.ApprovedFileType = ofiletype;	
	///				this._myContactList.UploadFileProc = oCtcType;
	///				this._myContactList.UrlToUploadFile = Server.MapPath("[YOUR UPLOAD SERVER FOLDER OF YOUR CHOICE"]);
	///			}
	///			
	///			#region Web Form Designer generated code
	///			override protected void OnInit(EventArgs e)
	///			{
	///				//
	///				// CODEGEN: This call is required by the ASP.NET Web Form Designer.
	///				//
	///				InitializeComponent();
	///				base.OnInit(e);
	///			}
	///			
	///			/// <summary>
	///			/// Required method for Designer support - do not modify
	///			/// the contents of this method with the code editor.
	///			/// </summary>
	///			private void InitializeComponent() {    
	///				this.Load += new System.EventHandler(this.Page_Load);
	///				
	///				/// YOU SHOULD HAVE THIS EVENT ATTACHED TO YOUR FILE UPLOADER
	///				this._myContactList.OnUploadFile += new FileUploadEventHandler(this._myContactList_OnUploadFile);
	///				this._myContactList.OnContactListTypeChanged += new ContactListTypeChangeEventHandler(this._myContactList_OnContactListTypeChanged);
	///				this._myContactList.OnContactListError += new ContactListErrorEventHandler(this._myContactList_OnContactListError);
	///			}
	///			#endregion
	///			
	///			public _myContactList_OnUploadFile(object sender, UploadFileEventArgs e) {
	///				// DO SOME ACTIONS OF YOUR CHOICE HERE !
	///			}			
	///			
	///			public _myContactList_OnContactListTypeChanged(object sender, ContactListTypeChangeEventArgs e) {
	///				// DO SOME ACTIONS OF YOUR CHOICE HERE !
	///			}
	///			
	///			public _myContactList_OnContactListError(object sender, ContactListErrorEventArgs e) {
	///				// DO SOME ACTIONS OF YOUR CHOICE HERE !
	///			}
	///		}		
	///	} 
	/// </code>
	/// </example>
	/// <remarks>This control cannot be Inherit</remarks>
	[ToolboxData("<{0}:ContactListUploader runat=server></{0}:ContactListUploader>")]
	public sealed class ContactListUploader : UploaderControl, INamingContainer {
	
		#region private event
		
		/// <summary>
		/// Event raise when the contact list type have been changed
		/// </summary>
		public event ContactListTypeChangeEventHandler OnContactListTypeChanged;

		/// <summary>
		/// Event raise if the contact list contain error
		/// </summary>
		public event ContactListErrorEventHandler OnContactListError;

		#endregion

		#region private override event
		
		/// <summary>
		/// Event raise from the base component, UploaderControl, after the file upload
		/// </summary>
		//public override event FileUploadedEventHandler OnUploadedFile;

		public event Events.ContactListUploadedEventHandler OnContactListUpload;
		
		#endregion

		#region private variables

		private EmailImporter _myImporter;
		private ContactInfo[] _myContactList;

		private DropDownList _ddlContactListType;
		private string _cssDropDownStyle = "";
		private ContactListType _contactlisttype;
		private	ContactListType[] _ListType; 

		private FileType[] _ContactListFileType;
		private bool _ShowContactListSelection;

		#endregion

		#region constructor

		/// <summary>
		/// Default Web Server Control constructor
		/// </summary>
		public ContactListUploader() {
			// Attaching an method for this event
			base.OnUploadedFile += new FileUploadedEventHandler(this.ContactListUploader_OnUploadedFile);
		//	base.OnErrorRaise += new UploadedErrorEventHandler(this.ContactListUploader_OnErrorRaise);
		}

		#endregion

		/// <summary>
		/// Default method, call automatically when the control have been Init from the Page Container
		/// </summary>
		protected override void CreateChildControls() {
			base.CreateChildControls();
			this._ddlContactListType = new DropDownList();
			this._ddlContactListType.AutoPostBack = true;
			this._ddlContactListType.EnableViewState = true;
			this._ddlContactListType.Items.Clear();
			this._ddlContactListType.CssClass = this.CssDropDownStyle;
			this._ListType = this.ListType;
			this.AddTypeListProvider();
		}

		#region private Events methods

		/// <summary>
		/// Event method when the FileType DropDownList have been Selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void oddlFlType_SelectedIndexChanged(object sender, EventArgs e) {
            this.UploadFileProv = (GA.BDC.Core.AddressBooks.Types.ContactListType)Enum.Parse(typeof(GA.BDC.Core.AddressBooks.Types.ContactListType), this._ddlContactListType.SelectedValue);
			this.OnContactListTypeChanged(this, new ContactListTypeChangeEventArgs());
		}
		
		/// <summary>
		/// Event method for the base event OnUploadedFile
		/// We know the contact list file have been save on our server, then we can
		/// do the contact list process for get the contact list entries
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void ContactListUploader_OnUploadedFile(object sender, GA.BDC.Core.Web.UI.Uploader.UploadFileEventArgs e)
        {
			try {
				// We Get the contact list data
				this.GetContactList();
				// Throwing new Event from this controls (not from the base)
                this.OnContactListUpload(this, new GA.BDC.Core.Web.UI.Uploader.ContactList.Events.ContactListUploadEventArgs());
			} catch(Exception ex) {
				if(ex.Message != "Thread was being aborted.")
					this.OnContactListError(this,new ContactListErrorEventArgs(ex.Message));
			} finally {
				try {
					// The file uploaded is deleted
					this.DeleteUploadFile();
				} catch{}
			}	
		}

        private void ContactListUploader_OnErrorRaise(object sender, GA.BDC.Core.Web.UI.Uploader.UploadErrorEventArgs e)
        {
			this.OnContactListError(this, new ContactListErrorEventArgs(e.ExceptionRaise.Message));
		}

		#endregion

		#region private methods

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true)]
		public FileType[] ApprovedFileType {
			get {
				if(base.ViewState[UploaderVars.__ContactListFileType] != null)
					return (FileType[])base.ViewState[UploaderVars.__ContactListFileType];
				return this._ContactListFileType;
			} set {
				if(base.ViewState[UploaderVars.__ContactListFileType] == null)
					base.ViewState.Add(UploaderVars.__ContactListFileType, value);
				else
					base.ViewState[UploaderVars.__ContactListFileType] = value;
			    this._ContactListFileType = value;
			}
		}

		/// <summary>
		/// Method to extract all Contact List Entries from the file submitted 
		/// by the client from the page
		/// </summary>
		private void GetContactList() {
			// Open the file
			System.IO.FileInfo oFile = new System.IO.FileInfo(base.UrlToUploadFile);
			FileType[] ofileType = this.ApprovedFileType;
			bool oFind = false;
			//-- Extention detectetion loop
			for(short i=0;i<ofileType.Length;i++) {
				if(ofileType[i] == FileType.None) {
					if(oFile.Extension == "") {
						oFind = true;
						break;
					}
				} else {
					if(oFile.Extension.ToLower() == "." + ofileType[i].ToString()){
						oFind = true;
						break;
					}
				}
			}
			//-- End of Extention detection loop
			if(oFind) {
                this._myImporter = new GA.BDC.Core.AddressBooks.EmailImporter(base.UrlToUploadFile, this.UploadFileProv);
				// We Get the contact list
				this._myContactList = this._myImporter.GetContactList;
			} else
				throw new Exception("The file format of your address book is not valid");
		}
		
		/// <summary>
		/// Method to add List
		/// </summary>
		private void AddTypeListProvider() {
			if(this.ShowContactListSelection) {
				for(int i=0;i<this.ListType.Length;i++)
					this._ddlContactListType.Items.Add(this.ListType[i].ToString());
				this._ddlContactListType.DataBind();
				base.InsertControls(this._ddlContactListType);
				this._ddlContactListType.SelectedIndexChanged += new EventHandler(this.oddlFlType_SelectedIndexChanged);
			}
		}
		
		#endregion

		#region public override methods

		/// <summary>
		/// Method for deleting the file uploaded
		/// </summary>
		public override void DeleteUploadFile() {
			base.DeleteUploadFile();
		}

		#endregion
		
		#region internal override methods
		
		/// <summary>
		/// Get the FileName of the file uploaded
		/// </summary>
		protected override string GetCurrentFilenameUploaded {
			get{ return base.GetCurrentFilenameUploaded; }
		}

		#endregion

		#region public attributes

		[Bindable(true), Category(UploaderVars.__VSDesignCssClass), DefaultValue("")]
		public string CssDropDownStyle {
			get{
				if(base.ViewState[UploaderVars.__CssDropDownStyle] != null)
					return (string)base.ViewState[UploaderVars.__CssDropDownStyle];
				return this._cssDropDownStyle;
			}
			set{
				if(base.ViewState[UploaderVars.__CssDropDownStyle] == null)
					base.ViewState.Add(UploaderVars.__CssDropDownStyle,value);
				else
					base.ViewState[UploaderVars.__CssDropDownStyle] = value;
				this._cssDropDownStyle = value;
			}
		}
		
		[Bindable(true), Category(UploaderVars.__VSDesignUploader)]
		public ContactListType[] ListType {
			get{
				if(base.ViewState[UploaderVars.__ListType] != null)
					return (ContactListType[])base.ViewState[UploaderVars.__ListType];
				return this._ListType; 
			}
			set{ 
				if(base.ViewState[UploaderVars.__ListType] == null)
					base.ViewState.Add(UploaderVars.__ListType, value);
				else
					base.ViewState[UploaderVars.__ListType] = value;
				this._ListType = value; 
			}
		}
	
		public ContactInfo[] UploadContactList {
			get{ return this._myContactList; }
		}

		public ContactListType UploadFileProv {
			get{ 
				if(base.ViewState[UploaderVars.__ContactListType] != null)
					return (ContactListType)base.ViewState[UploaderVars.__ContactListType];
				return this._contactlisttype;
			}
			set{ 
				if(base.ViewState[UploaderVars.__ContactListType] == null)
					base.ViewState.Add(UploaderVars.__ContactListType, value);
				else
					base.ViewState[UploaderVars.__ContactListType] = value;
				this._contactlisttype = value; 
			}
		}

		public int CountContacts {
			get{ return this.UploadContactList.Length; }
		}

		[Bindable(true), Category(UploaderVars.__VSDesignUploader), DefaultValue(false)]
		public bool ShowContactListSelection {
			get{ 
				if(base.ViewState[UploaderVars.__ShowContactListChooser] != null)
					return (bool)this._ShowContactListSelection; 
				return this._ShowContactListSelection;
			}
			set { 
				if(base.ViewState[UploaderVars.__ShowContactListChooser] == null)
					base.ViewState.Add(UploaderVars.__ShowContactListChooser,value);
				else
					base.ViewState[UploaderVars.__ShowContactListChooser] = value;
				this._ShowContactListSelection = value;
			}
		}

		#endregion
	}
}
