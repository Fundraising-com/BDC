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

namespace EFundraisingCRMWeb.Components.User
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    //using EFundraisingCRMWeb.App_Data;

    /// <summary>
    ///		Summary description for ClientHeader.
    /// </summary>
    public partial class ClientHeader : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {

          /*  if (Session[Global.SessionVariables.CLIENT_ID] != null && Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE] != null)
            {
                int clientID = Convert.ToInt32(Session[Global.SessionVariables.CLIENT_ID]);
                string clientSeqCode = Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();
                efundraising.EFundraisingCRM.Client c = efundraising.EFundraisingCRM.Client.GetClientByID(clientID, clientSeqCode);
                SetClientInfo(c);
            }
            //temporarely put lead info
            else if (Session[Global.SessionVariables.LEAD_ID] != null)
            {
                int leadID = Convert.ToInt32(Session[Global.SessionVariables.LEAD_ID]);
                efundraising.EFundraisingCRM.Lead l = efundraising.EFundraisingCRM.Lead.GetLeadByID(leadID);
                SetLeadInfo(l);
            }
            */
            // Put user code to initialize the page here
        }


        public void SetClientInfo(int clientId, string clientSeq)
        {
            efundraising.EFundraisingCRM.Client c = efundraising.EFundraisingCRM.Client.GetClientByID(clientId, clientSeq);
            SetClientInfo(c);
        }

        public void SetClientInfo(efundraising.EFundraisingCRM.Client cl)
        {
            if (cl == null) return;

            //EFundraisingCRM.Lead l = EFundraisingCRM.Lead.GetLeadByID(cl.LeadId);
            EmailLabel.Text = cl.Email;
            PhoneLabel.Text = cl.DayPhone + cl.DayPhoneExt;
            PersonLabel.Text = string.Format("{0} {1}", cl.FirstName, cl.LastName);
            GroupLabel.Text = cl.Organization;
        }

        public void SetLeadInfo(efundraising.EFundraisingCRM.Lead l)
        {
            if (l == null) return;
            //EFundraisingCRM.Lead l = EFundraisingCRM.Lead.GetLeadByID(cl.LeadId);
            EmailLabel.Text = l.Email;
            PhoneLabel.Text = l.DayPhone + l.DayPhoneExt;
            PersonLabel.Text = string.Format("{0} {1}", l.FirstName, l.LastName);
            GroupLabel.Text = l.Organization;
        }


        public void SetLeadInfo(int leadID)
        {
            efundraising.EFundraisingCRM.Lead l = efundraising.EFundraisingCRM.Lead.GetLeadByID(leadID);
            
            if (l == null) return;
            //EFundraisingCRM.Lead l = EFundraisingCRM.Lead.GetLeadByID(cl.LeadId);
            EmailLabel.Text = l.Email;
            PhoneLabel.Text = l.DayPhone + l.DayPhoneExt;
            PersonLabel.Text = string.Format("{0} {1}", l.FirstName, l.LastName);
            GroupLabel.Text = l.Organization;
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
