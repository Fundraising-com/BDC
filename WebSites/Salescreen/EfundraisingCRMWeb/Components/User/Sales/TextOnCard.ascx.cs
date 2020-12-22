using log4net;

namespace EFundraisingCRMWeb.Components.User.Sales
{	
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
    using efundraising.EFundraisingCRM;

	/// <summary>
	///		Summary description for TextOnCard.
	/// </summary>
 
	public partial class TextOnCard : System.Web.UI.UserControl
	{

		private string productDesc;

        public ILog Logger { get; set; }
		public delegate void TextOnCardClickedEventHandler(object sender, TextOnCardEventArgs e);
		public event TextOnCardClickedEventHandler TextOnCardClicked;

	    public TextOnCard()
	    {
            Logger = LogManager.GetLogger(GetType());
	    }
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		public void Refresh(string textOnCard)
		{
			try
			{ 
				ScratchBook sb = ScratchBook.GetScratchBookByID(ScratchbookID);
				productDesc = sb.ProductCode; 

				productLabel.Text = "Text On " + productDesc;
				if (textOnCard == "")
				{
					int leadID = Convert.ToInt32(Session[Global.SessionVariables.LEAD_ID]);
					Lead lead = Lead.GetLeadByID(leadID);
					textTextBox.Text = "Thank You for Supporting " + lead.Organization;
					}
				else
				{
					textTextBox.Text = textOnCard;
				}
			}
			catch(Exception x)
			{
				Logger.Error("Error refreshing TextOnCard",x);
			}
		   
		}

		public int ScratchbookID
		{
			get{return Convert.ToInt32(scratchBookIDTextBox.Text);}
			set{scratchBookIDTextBox.Text = value.ToString();}
		}
		
		public int ProductDecs
		{
			set{productDesc = value.ToString();}
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

		}
		#endregion

		protected void saveButton_Click(object sender, System.EventArgs e)
		{
			TextOnCardEventArgs args = new TextOnCardEventArgs();
			args.ScratchBookID = ScratchbookID;
			args.Text = textTextBox.Text;

			// Raise the clicked event.
			if(this.TextOnCardClicked != null) 
				this.TextOnCardClicked(this, args);

	     
		}
	}

}
