using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using QSP.WebControl;
//using QSP.ERP.Business;
using Business.Objects;
using Common;
using QSPFulfillment.CommonWeb;
using CampaignDataAccessRef = DAL.CampaignData;
namespace QSPFulfillment.AcctMgt
{
    public partial class SearchCampaignByAccount : QSPFulfillment.CommonWeb.QSPPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        private Message messageManager = new Message(true);
        CampaignDataAccessRef CampaignDataAccess = new CampaignDataAccessRef();
        public int AccountID
        {
            get
            {
                if (this.ViewState["AccountID"] == null)
                    return 0;

                return Convert.ToInt32(this.ViewState["AccountID"]);
            }
            set
            {
                this.ViewState["AccountID"] = value;
            }
        }
        private string AccountName
        {
            get;
            set;
        }
        #region Fields
        private string NameSearch
        {
            get
            {
                return this.tbxGroupName.Text;
            }
            set
            {
                this.tbxGroupName.Text = value;
            }
        }
        private int AccountIDSearch
        {
            get
            {
                try
                {
                    return Convert.ToInt32(this.tbxGroupID.Text);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                this.tbxGroupID.Text = value.ToString();
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, System.EventArgs e)
        {
            
           dtgMain.CurrentPageIndex = 0;

           DataBindResults();
        }

        private void DataBindResults()
        {
            try
            {

                AccountCampaignList acList = new AccountCampaignList();
             
               // CampaignDataAccess.SelectSearch("", AccountIDSearch, 0, NameSearch, "", "", "", 0, "", "", 0, 0, "");

                acList.SearchCampaignByAccount("", AccountIDSearch, 0, NameSearch, "", "", "", 0, "", "", 0);

                //DataRelation CAccountCampaign = new DataRelation("CAccountCampaignNew", new DataColumn[] {
                //        acList.dataSet.Tables["CAccount"].Columns["ID"]}, new DataColumn[] {
                //        acList.dataSet.Tables["Campaign"].Columns["BillToAccountID"]}, false);
                //acList.dataSet.Relations.Add(CAccountCampaign);




                this.dtgMain.DataSource = acList.dataSet.Campaign.DefaultView;
                //acList.dataSet.CAccount.Count



                this.dtgMain.DataBind();

            }
            catch (MessageException ex)
            {
                this.RegisterClientScriptBlock2("ErrorMessage", GetScriptError(ex.HTMLMessage));
            }

        }

        public void RegisterClientScriptBlock2(string key, string script)
        {

            if (key == "ValidatorIncludeScript")
            {
                script = "\r\n<script language=\"javascript\" src=\"/CustomerService/CSWebUIValidation.js\"></script>";

            }
            base.ClientScript.RegisterClientScriptBlock(GetType(), key, script);
        }
        private string GetScriptError(string ErrorMessage)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script language=\"javascript\">\n");

            sb.Append("s=\"" + ErrorMessage + "\";\n");

            sb.Append("</script>\n");
            return sb.ToString();
        }


        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {
            string JavaScriptStr = "<script>window.opener.document.forms[0].elements['" + Request.QueryString["caller"].ToString() + "'].value = '";
            JavaScriptStr += e.CommandArgument.ToString();
            JavaScriptStr += "';self.close()</script>";
            ClientScript.RegisterClientScriptBlock(GetType(), "anything", JavaScriptStr);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            tbxGroupName.Text = "";
            tbxGroupID.Text = "";
        }

     
    }
}
