namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess;
		using QSPFulfillment.DataAccess.Business;

	/// <summary>
	///		Summary description for PageSearchGroupOrder.
	/// </summary>
	public partial class PageSearchGroupOrder : ControlSearch
	{
		protected PageSearchGroup ctrlPageSearchGroup;
		protected PageSearchOrder ctrlPageSearchOrder;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.btnSearch.Click += new System.EventHandler(base.btnSearch_Click);
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

		protected override ParameterValueList GetValueToSearch()
		{
			ParameterValueList List = ctrlPageSearchGroup.GetParameterValue("");
			List.Merge(ctrlPageSearchOrder.GetParameterValue(""));
			return List;
		}
		protected override ParameterValueList GetValueToSearchFilter()
		{
			return  ctrlPageSearchOrder.GetParameterValueFilter("");
			
		}
	
		public override SearchMultiPage ResultType
		{
			get
			{				
				return SearchMultiPage.Order;
			}
		}
		public override bool Validate()
		{
			bool IsValid = true;

			IsValid &= ctrlPageSearchGroup.Validate();
			IsValid &= ctrlPageSearchOrder.Validate();

			return IsValid;
		}
		public override int ItemType
		{
			get
			{
				return ctrlPageSearchOrder.ItemType;
			}
			set
			{
				ctrlPageSearchOrder.ItemType = value;
			}
		}


	}
}
