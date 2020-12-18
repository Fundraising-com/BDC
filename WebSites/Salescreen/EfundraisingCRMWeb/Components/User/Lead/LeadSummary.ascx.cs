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

namespace EFundraisingCRMWeb.Components.User.Lead
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using efundraising.EFundraisingCRM;

    /// <summary>
    ///		Summary description for LeadSummary.
    /// </summary>
    /// 
    public partial class LeadSummary : System.Web.UI.UserControl
    {

        private int leadID;
        private DateTime leadEntryDate;
        private string channel;
        private string promotion;
        private string clientStatus;
        private string groupType;
        private string vif;
        private bool kitSent;
        private int participantCount;
        private string partner;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here



        }


        public void FillControl(int leadID)
        {
            efundraising.EFundraisingCRM.Lead lead = efundraising.EFundraisingCRM.Lead.GetLeadByID(leadID);
            if (lead != null)
            {
                efundraising.EFundraisingCRM.LeadChannel leadChannel = efundraising.EFundraisingCRM.LeadChannel.GetLeadChannelByID(lead.ChannelCode);
                efundraising.EFundraisingCRM.Promotion _promotion = efundraising.EFundraisingCRM.Promotion.GetPromotionByLeadID(leadID);
                efundraising.EFundraisingCRM.GroupType _groupType = efundraising.EFundraisingCRM.GroupType.GetGroupTypeByLeadID(leadID);
                efundraising.EFundraisingCRM.Partner _partner = efundraising.EFundraisingCRM.Partner.GetPartnerByLeadID(leadID);

                LeadID = leadID;
                LeadIDLabel.Text = leadID.ToString();
                LeadEntryDate = lead.LeadEntryDate;
                LeadEntryDateLabel.Text = lead.LeadEntryDate.ToShortDateString();
                Channel = leadChannel.Description;
                ChannelLabel.Text = leadChannel.Description;
                Promotion = _promotion.Description;
                PromotionLabel.Text = _promotion.Description;
                GroupType = _groupType.Description;
                GroupTypeLabel.Text = _groupType.Description;
                KitSent = lead.KitSent;
                KitSentLabel.Text = efundraising.Utilities.Format.Format.ToBooleanString(lead.KitSent, true);
                ParticipantCount = lead.ParticipantCount;
                ParticipantCountLabel.Text = lead.ParticipantCount.ToString();
                Partner = _partner.PartnerName;
                PartnerLabel.Text = _partner.PartnerName;
                if (lead.ClientStatusId == 1)
                {
                    ClientStatusLabel.Text = "VIP";
                }
                else
                {
                    ClientStatusLabel.Text = "Standard";
                }
                VIFLabel.Text = lead.Vif;

            }

        }

        #region Set/Get Methods

        #region LeadID setter and getters
        public int LeadID
        {
            get { return leadID; }
            set { leadID = value; }
        }
        #endregion

        #region LeadEntryDate setter and getters
        public DateTime LeadEntryDate
        {
            get { return leadEntryDate; }
            set { leadEntryDate = value; }
        }
        #endregion

        #region Channel setter and getters
        public string Channel
        {
            get { return channel; }
            set { channel = value; }
        }
        #endregion

        #region Promotion setter and getters
        public string Promotion
        {
            get { return promotion; }
            set { promotion = value; }
        }
        #endregion

        #region ClientStatus setter and getters
        public string ClientStatus
        {
            get { return clientStatus; }
            set { clientStatus = value; }
        }
        #endregion

        #region GroupType setter and getters
        public string GroupType
        {
            get { return groupType; }
            set { groupType = value; }
        }
        #endregion

        #region VIF setter and getters
        public string Vif
        {
            get { return vif; }
            set { vif = value; }
        }
        #endregion

        #region KitSent setter and getters
        public bool KitSent
        {
            get { return kitSent; }
            set { kitSent = value; }
        }
        #endregion

        #region ParticipantCount setter and getters
        public int ParticipantCount
        {
            get { return participantCount; }
            set { participantCount = value; }
        }
        #endregion

        #region Partner setters and getters
        public string Partner
        {
            get { return partner; }
            set { partner = value; }
        }
        #endregion
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

        }
        #endregion
    }
}
