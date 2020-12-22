namespace EFundraisingCRMWeb.Components.User.Package
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ProductLookUp.
	/// </summary>
	public partial class ProductLookUp : System.Web.UI.UserControl
	{
		
		private bool disableEverything = false;

		private string appPath
		{
			get
			{
				string ApPath = Request.ApplicationPath;
				if (!ApPath.EndsWith("/") )
					ApPath += "/";
				return ApPath;
			}
		}

		public string productDescription
		{
			get
			{
				return TextBox1.Text;
			}
			set
			{
				TextBox1.Text = value;
			}
		}


		public string productIdInHidden
		{
			get
			{
				return productidHidden.Value;
			}
			set
			{
				productidHidden.Value = value;
			}
		}

		
		public string saleIdInHidden
		{
			get
			{
				return saleidHidden.Value;
			}
			set
			{
				saleidHidden.Value = value;
			}
		}

	
		public string rowNumber
		{
			get
			{
				return rowNoHidden.Value;
			}
			set
			{
				rowNoHidden.Value = value;
			}
		}



		protected void Page_Load(object sender, System.EventArgs e)
		{
			
				
				if (!(disableEverything))
				{
					calendarImage.Attributes.Add("onClick", "javascript:ShowProductSearch(this);");
				
				}
			
//			Button1.Contols.Attributes.Add("onClick", "javascript:ShowProductSearch();");
			// Put user code to initialize the page here
			//LookupButton.Attributes.Add("onclick", 
			//		string.Format("fnCallDialog('{0}', '{1}', '{2}');", appPath, productNameTextBox.ClientID, productNameHidden.ClientID));
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

		
		protected override void OnPreRender(EventArgs e)
		{
			
			this.Page.RegisterStartupScript("VarPickdayVariables",string.Format("<script>var thePath='{0}';</script>", 
				appPath ));
			base.OnPreRender (e);
		}
		
		public void DisableEverything()
		{
			disableEverything = true;
						
		}

	
	}
}

