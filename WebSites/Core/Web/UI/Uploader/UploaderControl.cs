//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	December 01, 2004	-	Louis Turmel	Comments added
//	February 25, 2005	-	Louis Turmel	Code Comments
//	March	 28, 2005	-	Louis Turmel	Bug Fix of: File already in use
//	End of Version 1.0.0

//	April	25, 2005	-	Louis Turmel	Implementation of Globalizer Objects
//

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

namespace GA.BDC.Core.Web.UI.Uploader
{
	/// <summary>
	///	ASP.Net Web Server Controls, used to upload single file from Client
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
	///	
	///	namespace mySimpleUploaderWebPage {
	///		
	///		public class mySimpleUploaderWebPage : System.Web.UI.Page {
	///			
	///			protected efundraising.Web.UI.Uploader.UploaderControl _myFileUploader;
	///			
	///			private void Page_Load(object sender, System.EventArgs e) {
	///				if(!base.IsPostBack) {
	///					this.InitializeUploaderControls();
	///				}
	///			}
	///			
	///			private void InitializeUploaderControls() {
	///				this._myFileUploader.UrlToUploadFile = Server.MapPath("[YOUR UPLOAD SERVER FOLDER OF YOUR CHOICE"]);
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
	///				this._myFileUploader.OnUploadFile += new FileUploadEventHandler(this._myFileUploader_OnUploadFile);
	///				
	///			}
	///			#endregion
	///			
	///			public _myFileUploader_OnUploadFile(object sender, FileUploadEventArgs e) {
	///				// DO SOME ACTIONS OF YOUR CHOICE HERE !
	///			}			
	///		}		
	///	} 
	/// </code>
	///	</example>
	///	<remarks>This ASP.Net Web Controls can be inherits from others advanced control's</remarks>
	[ToolboxData("<{0}:UploaderControl runat=server></{0}:UploaderControl>")]
	public class UploaderControl : System.Web.UI.WebControls.WebControl, INamingContainer	{
		
		private const string __TYPE = "[TYPE]";
		private const string __TYPE_TEXT = "[UTEXT] [TYPE] file";

		#region public virtual event

		/// <summary>
		/// Event used when an file have been successful save on the Upload Folder
		/// </summary>
		public virtual event FileUploadedEventHandler OnUploadedFile;
		public virtual event UploadedErrorEventHandler OnErrorRaise;

		#endregion

		#region private variables

		private string _uploadtextbutton = "Upload...";
		private string _currentFileUploaded = "";

		private string _UploadImageUrl = "";
		private string _UploadCodeName = "";

		/// <summary>
		/// Define the Trackable Button Type [Image, HyperLink or Buttton]
		/// </summary>
		private Tracking.TrackingButtonType _UploadTrackingType;

		private string _filemaxsize;

		private string _FolderToSaveFile;
		private string _sessionID;

		private string _UploadText = null;
		
		#endregion

		#region protected controls

		/// <summary>
		/// Control for uploading the file between the client and server
		/// </summary>
		protected System.Web.UI.HtmlControls.HtmlInputFile _htmlinputfile;
		
		/// <summary>
		/// Trackable Button used to upload the submitted image
		/// </summary>
		protected Tracking.TrackingButton _uploadBtn;
		
        protected System.Web.UI.WebControls.TextBox _txtBadFileType;

		protected Table _DerivedObjectContainer;

		#endregion

		#region public constructor

		/// <summary>
		/// class constructor
		/// </summary>
		public UploaderControl() : base() {
			this._htmlinputfile = new HtmlInputFile();
			this._uploadBtn = new Tracking.TrackingButton();
			this._uploadBtn.CodeName = this.UploadCodeName;
			this._uploadBtn.ButtonType = this.UploadButtonType;
			this._uploadBtn.ImageUrl = this.UploadDisplayImageUrl;
			this._DerivedObjectContainer = new Table();
		}

		#endregion

		#region private methods event

		/// <summary>
		/// Method controller used when UploadButton is click by a client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _uploadBtn_Click(object sender, EventArgs e) {
			try {
				string oFile = this.UploadFile(this._htmlinputfile);
				this._htmlinputfile.Dispose();
				if(oFile != null)
					this.OnUploadedFile(this, new UploadFileEventArgs(oFile));
			} catch(Exception ex) {
				this.OnErrorRaise(this, new UploadErrorEventArgs(ex));
			}
		}

		#endregion	

		#region protected override methods

		/// <summary>
		/// This method is use when the Web Server Controls have been invoke by 
		/// the page container.
		/// </summary>
		protected override void CreateChildControls() {
			this._uploadBtn.Click += new ClickHandler(this._uploadBtn_Click);
			
			//-- Settings the properties for the Tracking Button;			
			this._uploadBtn.Text = this.UploadButtonText;		
			this._uploadBtn.CodeName = this.UploadCodeName;
			this._uploadBtn.ButtonType = this.UploadButtonType;
			this._uploadBtn.ImageUrl = this.UploadDisplayImageUrl;
			//-- End of Settings properties for Tracking Button

			this._htmlinputfile.Size = 40;
			this._htmlinputfile.MaxLength = 250;
			
			//-- Setting up the Design for the Web Server Control
			Table oTbl = new Table();
			TableRow oRow;
			
			TableCell oCell;
			oCell = new TableCell();
			oCell.ColumnSpan = 2;
			oCell.Controls.Add(this._htmlinputfile);
			oCell.Width = System.Web.UI.WebControls.Unit.Percentage(100);
			oRow = new TableRow();
			oRow.Width = System.Web.UI.WebControls.Unit.Percentage(100);
			oRow.Controls.Add(oCell);
			
			oTbl.Controls.Add(oRow);
			oCell = new TableCell();
			oCell.ColumnSpan = 2;
			oCell.Controls.Add(this._DerivedObjectContainer);
			this._DerivedObjectContainer.Width = System.Web.UI.WebControls.Unit.Percentage(100);
			oCell.Width = System.Web.UI.WebControls.Unit.Percentage(100);
			oRow = new TableRow();
			oRow.Width = System.Web.UI.WebControls.Unit.Percentage(100);
			oRow.Controls.Add(oCell);
			oTbl.Controls.Add(oRow);		
			oCell = new TableCell();
			oCell.Controls.Add(this._uploadBtn);
			oRow = new TableRow();
			oRow.Controls.Add(oCell);
			oTbl.Controls.Add(oRow);
			this.Controls.Add(oTbl);			
			//-- End of Setting for the Web Server Control

			base.CreateChildControls();
		}

		/// <summary>
		/// This Method is used by derivable Web Server Control that
		/// Inherit from this base UploaderFile class. For add some
		/// Specifics controls inside the base design
		/// </summary>
		/// <param name="pObject"></param>
		protected virtual void InsertInDerivedTable(object pObject) {
			TableCell oCell = new TableCell();
			oCell.Width = System.Web.UI.WebControls.Unit.Percentage(100);
			oCell.Controls.Add((System.Web.UI.Control)pObject);
			TableRow oRow = new TableRow();
			oRow.Width = System.Web.UI.WebControls.Unit.Percentage(100);
			oRow.Controls.Add(oCell);
			this._DerivedObjectContainer.Controls.Add(oRow);
		}

		/// <summary>
		/// This Method is used by derivable Web Server Control that
		/// Inherit from this base UploaderFile class. For add some
		/// Specifics controls inside the base design
		/// </summary>
		/// <param name="pObject"></param>
		protected virtual void InsertInDerivedTable(object[] pObject) {
			TableRow oRow = new TableRow();
			for(short i=0;i<pObject.Length;i++) {
				TableCell oCell = new TableCell();
				oCell.Controls.Add((System.Web.UI.Control)pObject[i]);
				oRow.Controls.Add(oCell);
			}
			this._DerivedObjectContainer.Controls.Add(oRow);
		}

		/// <summary>
		/// This Method is used by derivable Web Server Control that
		/// Inherit from this base UploaderFile class. For add some
		/// Specifics controls inside the base design
		/// </summary>
		/// <param name="pObject"></param>
		/// <param name="pType">Type of new Object to insert in the default Design</param>
		protected virtual void InsertInDerivedTable(object pObject, Type pType) {
			TableCell oCell = new TableCell();
			oCell.Controls.Add((System.Web.UI.Control)pObject);
			TableRow oRow = new TableRow();
			oRow.Controls.Add(oCell);
			this._DerivedObjectContainer.Controls.Add(oRow);
		}

		/// <summary>
		/// This Method is used by derivable Web Server Control that
		/// Inherit from this base UploaderFile class. For add some
		/// Specifics controls inside the base design
		/// </summary>
		/// <param name="pControlToInsert"></param>
		protected void InsertControls(System.Web.UI.Control pControlToInsert) {
			TableRow oRow = new TableRow();
			TableCell oCell = new TableCell();
			oCell.Controls.Add(pControlToInsert);
			oRow.Controls.Add(oCell);
			this.Controls[0].Controls[1].Controls.AddAt(0,oCell);
		}

		#endregion

		#region private methods

		/// <summary>
		/// Function call for uploading the client file
		/// </summary>
		/// <param name="pPostedFile"></param>
		/// <returns></returns>
		private string UploadFile(HtmlInputFile pPostedFile) {
			if(pPostedFile.PostedFile.FileName != "") {
				string[] oPath = pPostedFile.PostedFile.FileName.Split('\\');
				string oFilename = oPath[oPath.Length-1];
				if(this.UrlToUploadFile.IndexOf(oFilename,0,this.UrlToUploadFile.Length) == -1)
					oFilename = this.UrlToUploadFile + @"\"+ oFilename;			
				this.UrlToUploadFile = oFilename;
				// Save the file as...???
				try {
					pPostedFile.PostedFile.SaveAs(this.UrlToUploadFile);
					pPostedFile.Dispose();
				} catch(Exception ex) {
					throw ex;
				}
				this._currentFileUploaded = oFilename;
				return oFilename;
			} else
				return null;
		}

		#endregion

		#region public virtual methods

		/// <summary>
		/// Method for delete the uploaded file
		/// </summary>
		public virtual void DeleteUploadFile() {
			System.IO.File.Delete(this._currentFileUploaded);
		}

		#endregion		
		
		#region Attributes

		#region internal virtual attributes

		/// <summary>
		/// Get the current file name uploaded by the client
		/// </summary>
		protected virtual string GetCurrentFilenameUploaded {
			get{ return this._currentFileUploaded; }
		}

		#endregion
		
		#region Text Properties

		[Bindable(true), Category(UploaderVars.__VSDesignTextProp), DefaultValue("Upload...")]
		public string UploadTextButton {
			get {
				if(base.ViewState[UploaderVars.__UploadTextButton] != null)
					return (string)base.ViewState[UploaderVars.__UploadTextButton];
				return this._uploadtextbutton;
			} set {
				if(base.ViewState[UploaderVars.__UploadTextButton] == null)
					base.ViewState.Add(UploaderVars.__UploadTextButton, value);
				else
					base.ViewState[UploaderVars.__UploadTextButton] = value;
				this._uploadtextbutton = value;
			}
		}

		#endregion

		[Bindable(true)]
		public string FileMaxSize {
			get {
				if(base.ViewState[UploaderVars.__FileMaxSize] == null)
					return (string)base.ViewState[UploaderVars.__FileMaxSize];
				return this._filemaxsize;
			} set {
				if(base.ViewState[UploaderVars.__FileMaxSize] == null)
					base.ViewState.Add(UploaderVars.__FileMaxSize, value);
				else
					base.ViewState[UploaderVars.__FileMaxSize] = value;
				this._filemaxsize = value;
			}
		}

		#region Appearance 

		[Bindable(true), Category(UploaderVars.__VSDesignApp), DefaultValue("Upload...")]
		public string UploadButtonText {
			get{ return this._uploadtextbutton; }
			set{ this._uploadtextbutton = value; }
		}

		#endregion

		[Bindable(true), Category(UploaderVars.__VSDesignUploader)]
		public string UrlToUploadFile {
			get{ return this._FolderToSaveFile; }
			set{ this._FolderToSaveFile = value; }
		}	
		
		[Bindable(true), Category(UploaderVars.__VSDesignUploader)]
		public virtual Tracking.TrackingButtonType UploadButtonType {
			get {
				if(base.ViewState[UploaderVars.__UploadTrackingTypeName] != null)
					return (Tracking.TrackingButtonType)Enum.Parse(typeof(Tracking.TrackingButtonType),(string)base.ViewState[UploaderVars.__UploadTrackingTypeName]);
				return this._UploadTrackingType;
			} set {
				if(base.ViewState[UploaderVars.__UploadTrackingTypeName] == null)
					base.ViewState.Add(UploaderVars.__UploadTrackingTypeName,value.ToString());
				else
					base.ViewState[UploaderVars.__UploadTrackingTypeName] = value.ToString();
				this._UploadTrackingType = value;
			}
		}

		[Bindable(true), Category(UploaderVars.__VSDesignUploader)]
		public virtual string UploadCodeName {
			get {
				if(base.ViewState[UploaderVars.__UploadCodeName] != null)
					return (string)base.ViewState[UploaderVars.__UploadCodeName];
				return this._UploadCodeName;
			} set { 
				if(base.ViewState[UploaderVars.__UploadCodeName] == null)
					base.ViewState.Add(UploaderVars.__UploadCodeName,value);
				else
					base.ViewState[UploaderVars.__UploadCodeName] = value;
				this._UploadCodeName = value;
			}
		}	

		[Bindable(true), Category(UploaderVars.__VSDesignUploader)]
		public virtual string UploadDisplayImageUrl {
			get { 
				if(base.ViewState[UploaderVars.__UploadImageUrl] != null)
					return (string)base.ViewState[UploaderVars.__UploadImageUrl];
				return this._UploadImageUrl;
			} set {
				if(base.ViewState[UploaderVars.__UploadImageUrl] == null)
					base.ViewState.Add(UploaderVars.__UploadImageUrl,value);
				else
					base.ViewState[UploaderVars.__UploadImageUrl] = value;
				this._UploadImageUrl = value;
			}
		}

		#endregion
	}
}
