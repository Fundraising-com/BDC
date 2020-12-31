namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Windows.Forms.Design;
	using System.ComponentModel.Design;
	using DAL;

	/// <summary>
	///		Summary description for CodeDetailDropDown.
	/// </summary>
	//[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))] 
	public partial class CodeDetailDropDown : System.Web.UI.UserControl
	{
		protected int nHeader=-1;		
		public int CodeHeader
		{
			get{ return this.nHeader; }
			set{ this.nHeader=value;  }
		}

		protected int nCurrSelection=-1;		
		public int CurrSelection
		{
			get{ return this.nCurrSelection; }
			set{ this.nCurrSelection=value;  }
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		public bool LoadList()
	    {
			bool bOk = true;
			
			CodeDetailDataAccess a = new CodeDetailDataAccess();
			CodeDetailList.DataSource=  a.GetListFromHeader(nHeader);
			CodeDetailList.DataTextField="Description";
			CodeDetailList.DataValueField="Instance";
			CodeDetailList.DataBind();
			return bOk;
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
		
	}
}
