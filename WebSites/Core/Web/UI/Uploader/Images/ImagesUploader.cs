//
//	February 25, 2005	-	Louis Turmel	Code Comments
//	March 22, 2005		-	Louis Turmel	New Features Implementation
//	March 28, 2005		-	Louis Turmel	Bug fixing about image resizer
//											Bug of: File already in use
//

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

using GA.BDC.Core.Utilities;
using GA.BDC.Core.Utilities.Images;
using GA.BDC.Core.Utilities.SizeConverter;

namespace GA.BDC.Core.Web.UI.Uploader.Images
{
	
	/// <summary>
	/// ASP.Net Web Server Controls, used to upload single Image File from client to server
	/// You can use Web Server Controls attribute inside the declaration of HTML Tag 
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
	///	// Add the following namespaces
	///	using efundraising;
	///	using efundraising.Web;
	///	using efundraising.Web.UI.
	///	using efundraising.Web.Uploader;
	///	using efundraising.Web.Uploader.Images;
	///	
	///	using efundraising.Utilities;
	///	using efundraising.Utilities.Images;
	///	
	///	namespace mySampleImageUploaderWebForm {
	///		
	///		public class mySampleImageUploaderWebForm : System.Web.UI.Page {
	///			
	///			protected efundraising.Web.UI.Uploader.Images.ImagesUploader _myImageUploader;
	///			
	///			private void Page_Load(object sender, System.EventArgs e) {
	///				if(!base.IsPostBack) {
	///					this.InitializeUploaderControls();	
	///				}
	///			}
	///			
	///			private void InitializeUploaderControls() {
	///			
	///				this._myImageUploader.UrlToUploadFile = Server.MapPath("[YOUR UPLOAD SERVER FOLDER OF YOUR CHOICE"]);
	///				this._myImageUploader.ByteDefinition = efundraising.Utilities.SizeConverter.ByteDefinition.KB;
	///				this._myImageUploader.UrlToUploadFile = "jpg, jpeg, gif";
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
	///				this._myImageUploader.OnUploadFile += new FileUploadEventHandler(this._myImageUploader_OnUploadFile);
	///				this._myImageUploader.OnUploadError += new UploadErrorEventHandler(this._myImageUploader_OnUploadError);
	///				
	///			}
	///			#endregion
	///			
	///			private void _myImageUploader_OnUploadFile(object sender, FileUploadedEventArgs e) {
	///				// DO SOME ACTIONS HERE !
	///			}
	///			
	///			private void _myImageUploader_OnUploadError(object sender, UploadedErrorEventArgs e) {
	///				// DO SOME ACTIONS HERE !
	///			}
	///		}
	///	}	
	/// </code>
	/// </example>
	[ToolboxData("<{0}:ImagesUploader runat=server></{0}:ImagesUploader>")]
	public class ImagesUploader : UploaderControl, INamingContainer {
		
		#region public override event

		/// <summary>
		/// Event raise when the Image have been uploade successfully by the client
		/// </summary>
		public override event FileUploadedEventHandler OnUploadedFile;

		/// <summary>
		/// Event raise when the Image Upload process have encounted an error
		/// </summary>
		public event UploadedErrorEventHandler OnUploadError;

		#endregion

		#region private variables

		private Label _ImageSpec;
		private Label _Recommandation;
		private Label _Size;
		private Label _Message;

		private Label _ErrorImageType;
		private Label _ErrorDimension;
		private Label _ErrorSize;

		private short _FormatSize = 50;

		private int _Size_Height = 137;
		private int _Size_Width = 248;

		private string _CssTextStyle = "NormalText";
		private string _CssError_Color = "Error";
		
		private string _ImageSpecText;
		private string _RecommandationText;
		private string _MaxSixeText;
		
		private string _Error_Recommandation_Text;
		private string _Error_Size_Text;
		private string _Error_Dimension_Text;

		private bool _AllowAutoResize = true;
		
		private Utilities.Images.ImagesValidationSpec _ImgSpec;
		private Utilities.Images.ValidationStatus[] _ImgValStatus;
		private Utilities.Images.ImageType[] _ImageTypeContainer;

		private Utilities.SizeConverter.ByteDefinition _ByteDefinition = Utilities.SizeConverter.ByteDefinition.KB;

		#endregion

		#region private const

		private const string __UTEXT = "[UTEXT]";
		private const string __SIZE = "[SIZE]";
		private const string __WIDTH = "[WIDTH]";
		private const string __HEIGHT = "[HEIGHT]";
		private const string __TYPE = "[TYPE]";

		private const string __SIZE_TEXT = "[UTEXT] [SIZE]";
		private const string __RECO_TEXT = "[UTEXT] [WIDTH]*[HEIGHT] pixels ([SIZE])";
		private const string __TYPE_TEXT = "[UTEXT] [TYPE] files";

		#endregion

		/// <summary>
		/// class constructor for this Web Server Control
		/// </summary>
		public ImagesUploader() : base() {
			// Attach the event of base class to this control
			base.OnUploadedFile += new FileUploadedEventHandler(this.ImagesUploader_OnUploadedFile);
			base.OnErrorRaise += new UploadedErrorEventHandler(this.ImagesUploader_OnErrorRaise);
		}

		/// <summary>
		/// Method initializing all sub-controls of ImagesUploader Web Server Control
		/// </summary>
		private void CreateLabelObjects() {
			//-- Instanciate the controls objects
			this._ImageSpec = new Label();
			this._Recommandation = new Label();
			this._Size = new Label();
			//-- End of Instanciate the controls objects

			//-- Setting up the ImagesValidation class
			this._ImgSpec = new ImagesValidationSpec();
			this._ImgSpec.Height = this.ImageHeight;
			this._ImgSpec.Width = this.ImageWidth;
			this._ImgSpec.Size.Size = this._FormatSize;
			this._ImgSpec.Size.SizeType = this._ByteDefinition;
			this._ImgSpec.ImagesType = this.AcceptedImagesType;
			//-- End of Settings up the ImagesValidation class

			//-- Settings up Display of ImageUpload requirements inside the Control
			string oImgTp = "";
			for(short i=0;i<this._ImgSpec.ImagesType.Length;i++) {
				if(i==0)
					oImgTp = this._ImgSpec.ImagesType[i].ToString();
				else if(i == (this._ImgSpec.ImagesType.Length -1))
                    oImgTp += " or " + this._ImgSpec.ImagesType[i].ToString();
				else
					oImgTp += ", " + this._ImgSpec.ImagesType[i].ToString();
			}
			this._ImageSpec.Text = __TYPE_TEXT.Replace(__UTEXT,this.ImageSpecText).Replace(__TYPE,oImgTp);
			this._Recommandation.Text = __RECO_TEXT.Replace(__UTEXT,this.RecommendationText).Replace(__WIDTH,this.ImageWidth.ToString()).Replace(__HEIGHT,this.ImageHeight.ToString()).Replace(__SIZE,this._ImgSpec.Size.Size.ToString() + this._ImgSpec.Size.SizeType.ToString());
			this._Size.Text = __SIZE_TEXT.Replace(__UTEXT,this.MaxSizeText).Replace(__SIZE,this._ImgSpec.Size.Size.ToString() + this._ImgSpec.Size.SizeType.ToString());
			this._Size.Visible = false;
			//-- End of Display Image Upload requirements
	
			//-- Setting for error message controls
			this._ErrorSize = new Label();
			this._ErrorImageType = new Label();
			this._ErrorDimension = new Label();
			
			if(this.ErrorColor != null && this.ErrorColor != "") {
				this._ErrorSize.CssClass = this.ErrorColor;
				this._ErrorImageType.CssClass = this.ErrorColor;
				this._ErrorDimension.CssClass = this.ErrorColor;
			}

			if(this.CssTextClass != null && this.CssTextClass != "") {
				this._ImageSpec.CssClass = this.CssTextClass;
				this._Size.CssClass = this.CssTextClass;
				this._Recommandation.CssClass = this.CssTextClass;
			}

			this._ErrorSize.Text = this.ErrorSizeText;
			this._ErrorImageType.Text = this.ErrorRecommendationText;
			this._ErrorDimension.Text = this.ErrorDimensionText;

			this._ErrorSize.Visible = false;
			this._ErrorImageType.Visible = false;
			this._ErrorDimension.Visible = false;			
			//-- End of settings for errors messages controls

			//-- Inserting inside the base control the ImageUploader Controls
			object[] oT = { this._ImageSpec, this._ErrorImageType };
			base.InsertInDerivedTable(oT);
			object[] oTb = { this._Recommandation, this._ErrorDimension };
			base.InsertInDerivedTable(oTb);
			object[] oTc = { this._Size, this._ErrorSize };
			base.InsertInDerivedTable(oTc);		
			//-- End of Inserting
		}

		/// <summary>
		/// This method is use when the Web Server Controls have been invoke by 
		/// the page container.
		/// </summary>
		protected override void CreateChildControls() {	
			this.CreateLabelObjects();
			base.CreateChildControls();
		}
	
		#region public attributes

		[Bindable(true), Category(UploaderVars.__VSDesignUploader),DefaultValue(true)]
		public bool AllowAutoImageResize {
			get{
				if(base.ViewState[ImagesCtrlVars.__AllowResizing] != null)
					return (bool)base.ViewState[ImagesCtrlVars.__AllowResizing];
				else
					return this._AllowAutoResize;
			} set {
				if(base.ViewState[ImagesCtrlVars.__AllowResizing] == null)
					base.ViewState.Add(ImagesCtrlVars.__AllowResizing, value);
				else
					base.ViewState[ImagesCtrlVars.__AllowResizing] = value;
				this._AllowAutoResize = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(UploaderVars.__VSDesignTextProp)]
		public string ErrorDimensionText {
			get {
				if(base.ViewState[ImagesCtrlVars.__Error_Dimension_Text] != null)
					return (string)base.ViewState[ImagesCtrlVars.__Error_Dimension_Text];
				else
					return this._Error_Dimension_Text;
			} set {
				if(base.ViewState[ImagesCtrlVars.__Error_Dimension_Text] == null)
					base.ViewState.Add(ImagesCtrlVars.__Error_Dimension_Text, value);
				else
					base.ViewState[ImagesCtrlVars.__Error_Dimension_Text] = value;
				this._Error_Dimension_Text = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(UploaderVars.__VSDesignTextProp)]
		public string ErrorSizeText {
			get{
				if(base.ViewState[ImagesCtrlVars.__Error_Size_Text] != null)
					return (string)base.ViewState[ImagesCtrlVars.__Error_Size_Text];
				else
					return this._Error_Size_Text;
			} set	{
				if(base.ViewState[ImagesCtrlVars.__Error_Size_Text] == null)
					base.ViewState.Add(ImagesCtrlVars.__Error_Size_Text, value);
				else
					base.ViewState[ImagesCtrlVars.__Error_Size_Text] = value;
				this._Error_Size_Text = value;
			}	
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(UploaderVars.__VSDesignTextProp)]
		public string ErrorRecommendationText {
			get {
				if(base.ViewState[ImagesCtrlVars.__Error_Recommendation_Text] != null)
					return (string)base.ViewState[ImagesCtrlVars.__Error_Recommendation_Text];
				else
					return this._Error_Recommandation_Text;
			} set {
				if(base.ViewState[ImagesCtrlVars.__Error_Recommendation_Text] == null)
					base.ViewState.Add(ImagesCtrlVars.__Error_Recommendation_Text, value);
				else
					base.ViewState[ImagesCtrlVars.__Error_Recommendation_Text] = value;
				this._Error_Recommandation_Text = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(ImagesCtrlVars.__VSDesign_ImageType)]
		public Utilities.Images.ImageType[] AcceptedImagesType {
			get {
				if(base.ViewState[ImagesCtrlVars.__ImagesType] != null)
					return (Utilities.Images.ImageType[])base.ViewState[ImagesCtrlVars.__ImagesType];
				else
					return this._ImageTypeContainer;
			} set {
				if(base.ViewState[ImagesCtrlVars.__ImagesType] == null)
					base.ViewState.Add(ImagesCtrlVars.__ImagesType, value);
				else
					base.ViewState[ImagesCtrlVars.__ImagesType] = value;
				this._ImageTypeContainer = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(UploaderVars.__VSDesignCssClass), DefaultValue("NormalText")]
		public string CssTextClass {
			get {
				if(base.ViewState[ImagesCtrlVars.__TextCss] != null)
					return (string)base.ViewState[ImagesCtrlVars.__TextCss];
				else
					return this._CssTextStyle;
			} set {
				if(base.ViewState[ImagesCtrlVars.__TextCss] == null)
					base.ViewState.Add(ImagesCtrlVars.__TextCss, value);
				else
					base.ViewState[ImagesCtrlVars.__TextCss] = value;
				this._CssTextStyle = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(ImagesCtrlVars.__VSDesign_ImageDimension), DefaultValue(248)]
		public int ImageWidth {
			get {
				if(base.ViewState[ImagesCtrlVars.__Dimension_Width] != null)
					return (int)base.ViewState[ImagesCtrlVars.__Dimension_Width];
				else
					return this._Size_Width;
			} set {
				if(base.ViewState[ImagesCtrlVars.__Dimension_Width] == null)
					base.ViewState.Add(ImagesCtrlVars.__Dimension_Width, value);
				else
					this.ViewState[ImagesCtrlVars.__Dimension_Width] = value;
				this._Size_Width = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(ImagesCtrlVars.__VSDesign_ImageDimension), DefaultValue(137)]
		public int ImageHeight {
			get {
				if(base.ViewState[ImagesCtrlVars.__Dimension_Height] != null)
					return (int)base.ViewState[ImagesCtrlVars.__Dimension_Height];
				else
					return this._Size_Height;
			} set {
				if(base.ViewState[ImagesCtrlVars.__Dimension_Height] == null)
					base.ViewState.Add(ImagesCtrlVars.__Dimension_Height, value);
				else
					base.ViewState[ImagesCtrlVars.__Dimension_Height] = value;
				this._Size_Height = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(ImagesCtrlVars.__VSDesign_ImageText), DefaultValue("Images must be")]
		public string ImageSpecText {
			get {
				if(base.ViewState[ImagesCtrlVars.__ImageSpec] != null)
					return (string)base.ViewState[ImagesCtrlVars.__ImageSpec];
				else
					return this._ImageSpecText;
			} set {
				if(base.ViewState[ImagesCtrlVars.__ImageSpec] == null)
					base.ViewState.Add(ImagesCtrlVars.__ImageSpec, value);
				else
					base.ViewState[ImagesCtrlVars.__ImageSpec] = value; 
				this._ImageSpecText = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(ImagesCtrlVars.__VSDesign_ImageText), DefaultValue("Recommendation:")]
		public string RecommendationText {
			get {
				if(base.ViewState[ImagesCtrlVars.__Recommendation] != null)
					return (string)base.ViewState[ImagesCtrlVars.__Recommendation];
				else
					return this._RecommandationText;
			} set {
                if(base.ViewState[ImagesCtrlVars.__Recommendation] == null)
					base.ViewState.Add(ImagesCtrlVars.__Recommendation,value);
				else
                    base.ViewState[ImagesCtrlVars.__Recommendation] = value;
				this._RecommandationText = value;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(ImagesCtrlVars.__VSDesign_ImageText), DefaultValue("Maximum size:")]
		public string MaxSizeText {
			get {
				if(base.ViewState[ImagesCtrlVars.__MaxSizeText] != null)
					return (string)base.ViewState[ImagesCtrlVars.__MaxSizeText];
				else
					return this._MaxSixeText;
			} set {
				if(base.ViewState[ImagesCtrlVars.__MaxSizeText] == null)
					base.ViewState.Add(ImagesCtrlVars.__MaxSizeText,value);
				else
					base.ViewState[ImagesCtrlVars.__MaxSizeText] = value;
				this._MaxSixeText = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(ImagesCtrlVars.__VSDesign_ImageType),DefaultValue(Utilities.SizeConverter.ByteDefinition.KB)]
		public Utilities.SizeConverter.ByteDefinition ByteDefinition {
			get {
				if(base.ViewState[ImagesCtrlVars.__SizeType] != null)
					return (Utilities.SizeConverter.ByteDefinition)Enum.Parse(typeof(Utilities.SizeConverter.ByteDefinition),(string)base.ViewState[ImagesCtrlVars.__SizeType]);
				else
					return this._ByteDefinition;
			} set {
				if(base.ViewState[ImagesCtrlVars.__SizeType] == null)
					base.ViewState.Add(ImagesCtrlVars.__SizeType, value);
				else
					base.ViewState[ImagesCtrlVars.__SizeType] = value;
				this._ByteDefinition = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Bindable(true), Category(UploaderVars.__VSDesignCssClass), DefaultValue("Error")]
		public string ErrorColor {
			get {				
				if(base.ViewState[ImagesCtrlVars.__Error_Color] != null)
					return (string)base.ViewState[ImagesCtrlVars.__Error_Color];
				else
					return this._CssError_Color;
			} set {
				if(base.ViewState[ImagesCtrlVars.__Error_Color] == null)
					base.ViewState.Add(ImagesCtrlVars.__Error_Color, value);
				else
					base.ViewState[ImagesCtrlVars.__Error_Color] = value;
				this._CssError_Color = value;
			}
		}
	
		#endregion

		/// <summary>
		/// Method raise when a file have been upload successfully by a Web Page Client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImagesUploader_OnUploadedFile(object sender, UploadFileEventArgs e) {
			try {
				string oFinalName = "";
				bool oValidation = this.CheckFileValidation(e.Filename, ref oFinalName);
				this.OnUploadedFile(this,new Uploader.UploadFileEventArgs(oFinalName, oValidation));
			} catch(Exception ex) {
				this.OnUploadError(this, new Uploader.UploadErrorEventArgs(e.Filename));
			}
		}   
     
		private void ImagesUploader_OnErrorRaise(object sender, UploadErrorEventArgs e) {
			this.OnUploadError(this,e);
		}

		/// <summary>
		/// Function of validation of the integrity of the image uploaded
		/// by the web user
		/// </summary>
		/// <param name="pFilename"></param>
		private bool CheckFileValidation(string pFilename, ref string pFinalName) {
			
			this._ErrorSize.Visible = false;
			this._ErrorImageType.Visible = false;
			this._ErrorDimension.Visible = false;

			this._ImgSpec.Height = this.ImageHeight;
			this._ImgSpec.Width = this.ImageWidth;

			this._ImgSpec.Size.Size = this._FormatSize;
			this._ImgSpec.Size.SizeType = this._ByteDefinition;
			this._ImgSpec.ImagesType = this._ImageTypeContainer;
			
			// Image validation process here...
			this._ImgValStatus = Utilities.Images.ImagesValidation.GetImagesValidation(pFilename,this._ImgSpec);
			int oValCount = Enum.GetValues(typeof(Utilities.Images.ImagesValidationCode)).Length;
			bool oDelImage = false;
			//-- Validation loop
			for(short i=0;i<oValCount;i++) {
				if(this._ImgValStatus[i] == Utilities.Images.ValidationStatus.Reject) {
					oDelImage = true;
					switch((Utilities.Images.ImagesValidationCode)i) {
						case Utilities.Images.ImagesValidationCode.BadFileType:
							this._ErrorImageType.Visible = true;
							break;
						case Utilities.Images.ImagesValidationCode.DimensionNotFit:
							this._ErrorDimension.Visible = true;
							if(this.AllowAutoImageResize) {
								try {
									System.IO.FileInfo oF = new System.IO.FileInfo(pFilename);
									pFinalName = pFilename.Replace(oF.Extension,"") + "_Resize" + oF.Extension;
									Utilities.Images.ImagesHelper.ImageReducer(pFilename,pFinalName,short.Parse(this.ImageWidth.ToString()));
									this._ErrorDimension.Visible = false;
									oDelImage = false;
									this._ImgValStatus[i] = Utilities.Images.ValidationStatus.Accept;
									i = (short)oValCount;
								} catch(Exception ex) {
									System.IO.File.Delete(pFilename);
									throw ex;
								}
							}
							break;
						case Utilities.Images.ImagesValidationCode.ExceedFileSize:
							this._ErrorSize.Visible = true;
							break;
					}
				} else
					pFinalName = pFilename;
			}
			//-- End of validation loop
			try {
				if(oDelImage)
					System.IO.File.Delete(pFinalName);
			} catch(Exception ex) {
				throw ex; 
			}
			if(oDelImage)
				return false;
			else 
				return true;
		}
	}
}
