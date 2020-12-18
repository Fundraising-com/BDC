namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Xml;
    using GA.BDC.Core.Diagnostics;

	/// <summary>
	///		Summary description for ScratchcardsTop.
	/// </summary>
	public class ScratchcardsTop : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label FirstPartLabel;
		protected System.Web.UI.WebControls.Repeater bulletList;
		protected System.Web.UI.WebControls.Repeater Repeater1;
		protected System.Web.UI.WebControls.Label SecondPartLabel;
		protected System.Web.UI.WebControls.Label ThirdPartLabel;
		protected System.Web.UI.WebControls.Image imgScratchcard;
		protected System.Web.UI.WebControls.Label ThirdPartTitleLabel;

		private int packageId;
		private string cultureName;
		protected System.Web.UI.WebControls.ImageButton orderNowButton;
		private string extraDescXML;

		private void Page_Load(object sender, System.EventArgs e)
		{
		   LoadPackageValue();
		}

		private void LoadPackageValue()
		{
            //reads xml field for text to display
			try
			{
				string firstPart = "";
				string secondPart = "";
				string thirdPartTitle = "";
				string thirdPart = "";


				if (cultureName == "en-CA")
				{
					orderNowButton.Visible = false;
				}
				

				XmlTextReader reader =  new XmlTextReader(new System.IO.StringReader(extraDescXML));

				System.Collections.ArrayList values = new System.Collections.ArrayList();
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element &&
						reader.Name == "FirstPart") //host is found
					{
						reader.Read(); //falls on the value
						firstPart = reader.Value;
					}


					if (reader.NodeType == XmlNodeType.Element &&
						reader.Name == "BulletItem") //host is found
					{
						reader.Read(); //falls on the value
						values.Add(reader.Value);
					}

					if (reader.NodeType == XmlNodeType.Element &&
						reader.Name == "SecondPart") //host is found
					{
						reader.Read(); //falls on the value
						secondPart = reader.Value;
					}

					if (reader.NodeType == XmlNodeType.Element &&
						reader.Name == "ThirdPartTitle") //host is found
					{
						reader.Read(); //falls on the value
						thirdPartTitle = reader.Value;
					}
				
					if (reader.NodeType == XmlNodeType.Element &&
						reader.Name == "ThirdPart") //host is found
					{
						reader.Read(); //falls on the value
						thirdPart = reader.Value;
					}

				}

			

				FirstPartLabel.Text = firstPart;
				SecondPartLabel.Text = secondPart;
				ThirdPartTitleLabel.Text = thirdPartTitle;
				ThirdPartLabel.Text = thirdPart;
			
				bulletList.DataSource = values;
				bulletList.DataBind();
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in Parsing xml for control ScratchcardsTop",ex);
				
			}

			
			
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
			this.orderNowButton.Click += new System.Web.UI.ImageClickEventHandler(this.orderNowButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void orderNowButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string url = System.Configuration.ConfigurationSettings.AppSettings["efundraisingStoreURL"];
			Response.Redirect(url);
		}

		#region Properties

		public int PackageId
		{
			get { return packageId; }
			set { packageId = value; }
		}

		public string ImageUrl
		{
			set { imgScratchcard.ImageUrl = value; }
		}

		public string ExtraDescXML
		{
			set { extraDescXML = value; }
		}

		public string CultureName
		{
			set { cultureName = value; }
		}
		#endregion Properties

	}
}
