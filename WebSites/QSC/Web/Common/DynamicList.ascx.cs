namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for DynamicList.
	/// </summary>
	public partial class DynamicList : System.Web.UI.UserControl
	{

		#region control initialization
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}
		
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
		}
		#endregion control initialization
		
		#region Item Declarations
		#endregion Item Declarations

		#region Control properties
		
		#region Seperator
		private string _Seperator;
		public string Seperator
		{
			get 
			{
				return _Seperator;
			}
			set
			{
				_Seperator = value;
			}
		}
		#endregion Seperator

		#region DataString
		private string _DataString;
		public string DataString
		{
			get 
			{
				return _DataString;
			}
			set
			{
				_DataString = value;
			}
		}
		#endregion DataString

		#region Bind
		public void Bind()
		{
			this.ltTheList.Text = "";
			string temp = _DataString;
			temp = "<li>" + temp.Replace(_Seperator, "</li><li>");
			
			//remove the last <li>
			temp = temp.Remove(temp.Length-4, 4);
			
			//setup the list
			this.ltTheList.Text = "<ul>" + temp + "</ul>";
		}
		#endregion Bind

		#endregion Control properties
	}
}
