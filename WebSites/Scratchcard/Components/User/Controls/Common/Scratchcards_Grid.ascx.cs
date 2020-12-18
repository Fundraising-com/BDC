namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using GA.BDC.Core.eFundraisingStore;

	/// <summary>
	///		Summary description for Scratchcards_Grid.
	/// </summary>
	public class Scratchcards_Grid : System.Web.UI.UserControl
	{
		#region Protected Variables

		protected System.Web.UI.WebControls.Image imgScratchcard;
		protected System.Web.UI.WebControls.Label lblScratchcard;
		protected System.Web.UI.WebControls.HyperLink hplScratchcard;
		protected System.Web.UI.WebControls.DataList dtlScratchcardsPics;
		
		#endregion

		#region Constants

		protected const int PICS_SEPARATOR = 3;
		protected const string PICS_PATH ="UserResources/images/_ScratchcardWeb_/en-US/aboutproduct/smallcards/";
		protected const string BIG_PICS_PATH ="UserResources/Images/en-CA/Scratchard/BigPics/";
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblCategory;
		protected System.Web.UI.WebControls.Label shortDescLabel;
		protected System.Web.UI.WebControls.Image Image2;
		protected const string ENLARGE_JAVA ="javascript:NewWindow"; // height="99" alt="General" src="resources/images/_efund_/_classic_/en-us/scratchcards/cards/generalsport.jpg";
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			dtlScratchcardsPics.RepeatColumns = PICS_SEPARATOR;
	
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Setters and Getters
		public int PicsSeparator
		{
			get{return PICS_SEPARATOR;}
		}
		public string Category
		{
			get{ return lblCategory.Text;}
			set{lblCategory.Text = value;}
		}

		public string ShortDesc
		{
			get{ return shortDescLabel.Text;}
			set{ shortDescLabel.Text = value;}
		}

		public string ImageURL
		{
			get{ return ((System.Web.UI.WebControls.Image)dtlScratchcardsPics.FindControl("imgScratchcard")).ImageUrl;}

			set{
				((System.Web.UI.WebControls.Image)dtlScratchcardsPics.FindControl("imgScratchcard")).ImageUrl = value;
			}
		}
		public string Description
		{
			get{return lblScratchcard.Text;}
			set{lblScratchcard.Text = value;}
		}

		public string Link
		{
			get{return hplScratchcard.NavigateUrl;}
			set{hplScratchcard.NavigateUrl = value;}
		}
		public string PicsPath
		{
			get{return PICS_PATH;}
		}
		public string BigPicsPath
		{
			get{return BIG_PICS_PATH;}
		}
		public string EnlargeJava
		{
			get {return ENLARGE_JAVA;}
		}
		public DataList SCDatalist
		{
			get{ return dtlScratchcardsPics;}
			set{ dtlScratchcardsPics = value;}
		}
		#endregion
	}
}
