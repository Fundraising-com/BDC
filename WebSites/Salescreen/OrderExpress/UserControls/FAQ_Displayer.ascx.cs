//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_FAQ_Displayer_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'FAQ_Displayer.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Business;
using QSPForm.Common.DataDef;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for FAQ_Displayer.
    /// </summary>
    public partial class FAQ_Displayer : BaseFAQ_Displayer {
        protected AppItemData dtsFAQ = new AppItemData();

        protected void Page_DataBinding(Object sender, System.EventArgs e) {
            ContentManagerSystem contMgrSys = new ContentManagerSystem();

            dtsFAQ = contMgrSys.SelectAllFAQItemByNoAppItem((int)this.Page.AppItem);

            dtLst.DataSource = dtsFAQ.AppItemFAQ;
            dtLst.DataBind();
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.dtLst.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtLst_Item_Databind);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        private void dtLst_Item_Databind(object sender, System.Web.UI.WebControls.DataListItemEventArgs e) {
            DataRow row = dtsFAQ.Tables[AppItemFAQTable.TBL_FAQ_ITEMS].Rows[e.Item.ItemIndex];

            if (row != null) {
                if (((bool)row["IsRedirect"]) == true)
                    ((HyperLink)e.Item.Controls[1]).Attributes.Add("OnClick", "window.open('" + row["Answer"] + "','QSPForm','toolbars=no,scrollbars=yes,width=610,height=450,resizable=yes');");
                else
                    ((HyperLink)e.Item.Controls[1]).Attributes.Add("OnClick", "var owin; owin = window.open('FAQ_Viewer.aspx?faq=" + row["FAQ_ID"] + "','QSPForm','toolbars=no,scrollbars=yes,width=610,height=450,resizable=yes'); owin.focus();");
            }
        }
    }
}