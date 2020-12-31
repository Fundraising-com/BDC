using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace QSPFulfillment.Reports
{
	///<summary>Summary description for RemitProcessingReport.</summary>
	public partial class RemitProcessingReport : QSPFulfillment.CommonWeb.QSPPage
	{
		#region auto-generated code
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
		#endregion auto-generated code
			
		#region item declarations
		protected QSPFulfillment.CommonWeb.UC.DateEntry FromRemitDate, ToRemitDate;
		#endregion item declarations
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}
	}
}
