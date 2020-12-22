using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace EFundraisingCRMWeb.Components.User.ClientControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using efundraising.EFundraisingCRM;


    /// <summary>
    ///		Summary description for ClientRequests.
    /// </summary>
    public partial class ClientRequests : System.Web.UI.UserControl
    {
        private int leadID;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

        }



        public void DoDataBind(int leadID)
        {
            efundraising.EFundraisingCRM.LeadVisit[] leadVisits = efundraising.EFundraisingCRM.LeadVisit.GetLeadVisitsByLeadID(leadID);
            RequestDatagrid.DataSource = Components.Server.DataGrid.Leads.LeadVisitsDataGrid.CreateDataTableLeadVisits(leadVisits);
            RequestDatagrid.DataBind();



        }

        #region LeadID setter

        public void SetLeadID(int val)
        {
            leadID = val;
        }


        #endregion

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
            this.RequestDatagrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.RequestDatagrid_PageIndexChanged);

        }
        #endregion

        private void RequestDatagrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            ///get leadid
            int leadID = Convert.ToInt32(Session[Global.SessionVariables.LEAD_ID]);
            RequestDatagrid.CurrentPageIndex = e.NewPageIndex;
            DoDataBind(leadID);
        }
    }
}
