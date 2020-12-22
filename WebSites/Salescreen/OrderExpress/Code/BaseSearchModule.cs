using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using QSP.WebControl;
using System.Collections;
using System.Web.UI;
using QSPForm.Business;
using QSPForm.Common.DataDef;

namespace QSP.OrderExpress.Web.Code {
    public delegate void SearchModuleEventHandler(object sender, SearchModuleClickedArgs e);

    public class SearchModuleClickedArgs : EventArgs {
        private string strText;
        private int intValue;
        private string strCriteria;

        public SearchModuleClickedArgs(string Text, int Value, string Criteria) {
            strText = Text;
            intValue = Value;
            strCriteria = Criteria;
        }

        /// <summary>
        /// Get text entered
        /// </summary>
        public string FilterExpression {
            get {
                return strText;
            }
        }

        /// <summary>
        /// get value of the search criteria
        /// </summary>
        public int SearchMode {
            get {
                return intValue;
            }
        }

        /// <summary>
        /// get text of the search criteria
        /// </summary>
        public string Criteria {
            get {
                return strCriteria;
            }
        }
    }

    /// <summary>
    ///		Summary description for BaseSearchModule.
    /// </summary>
    public class BaseSearchModule : BaseWebUserControl//,IList,ICollection,IEnumerable,IStateManager
    {
        private AppItemData dtsAppItemData;
        public event SearchModuleEventHandler OnSearch;
        //public event EventHandler DataBind;
        private string sDefaultSearch;
        protected bool bEnabled = true;
        private System.Web.UI.WebControls.ImageButton imgBtnSearch;
        private System.Web.UI.WebControls.TextBox txtCriteria;
        private System.Web.UI.WebControls.DropDownList ddlSearchBy;
        private System.Web.UI.WebControls.Label lblSearchByAlpha;
        private AlphaSearch ctrlAlphaSearch;
        private System.Web.UI.WebControls.Label lblHeader;
        private const string MSGSEARCUNAVAILABLE = "Search is unavailable for the moment.  Sorry for the inconvenience.";
        private DataView DVSearchOption = new DataView();
        private QSPForm.Business.AppItem searchAppItem = 0;
        private const string HEADER_TEXT = "You can search using the following criteria: "; //"Search by: "

        private void Page_Load(object sender, EventArgs e) {
        }

        private void Page_PreRender(object sender, System.EventArgs e) {
            AddJavascript();
        }

        private void Page_Diposed(object sender, EventArgs e) {

        }

        protected override void OnDataBinding(EventArgs e) {
            try {
                LoadData();
                FillDropDownList();
                CreateHeader();
                this.SearchCriteriaAlphaSearch = DefaultSearch;
                this.ddlSearchBy.DataBind();
                this.txtCriteria.DataBind();
                this.imgBtnSearch.DataBind();
                this.ctrlAlphaSearch.DataBind();

                base.OnDataBinding(e);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        private void LoadData() {
            ContentManagerSystem CMSys = new ContentManagerSystem();
            if ((int)searchAppItem > 0)
                dtsAppItemData = CMSys.SelectAllSearchItemsByNoAppItem((int)searchAppItem);
            else
                dtsAppItemData = CMSys.SelectAllSearchItemsByNoAppItem((int)this.Page.AppItem);
            DVSearchOption = CMSys.SelectAllByDetailType(dtsAppItemData, AppItemData.DETAIL_TYPE_WEBFORM_SEARCH);
            DVSearchOption.Sort = AppItemDetailTable.FLD_DISPLAY_ORDER + ", " + AppItemDetailTable.FLD_NAME + ", " + AppItemDetailTable.FLD_NO_DETAIL;
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
            this.ctrlAlphaSearch.OnClick += new QSP.WebControl.AlphaSearchEventHandler(this.ctrlAlphaSearchClick);
            this.imgBtnSearch.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSearch_Click);
            this.Disposed += new System.EventHandler(this.Page_Diposed);
            this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new System.EventHandler(this.Page_PreRender);
        }

        #endregion

        private void imgBtnSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            FireEvent();
        }

        private void FireEvent() {
            if (OnSearch != null)
                OnSearch(this, new SearchModuleClickedArgs(this.txtCriteria.Text.Trim(), Convert.ToInt32(this.ddlSearchBy.SelectedItem.Value), this.ddlSearchBy.SelectedItem.Text));
        }

        private void FillDropDownList() {
            if (!IsPostBack) {
                if (ddlSearchBy.Items.Count == 0) {
                    ListItem Item;

                    foreach (DataRowView Row in DVSearchOption) {
                        Item = new ListItem(Row[AppItemTable.FLD_NAME].ToString(), Row[AppItemDetailTable.FLD_NO_DETAIL].ToString());
                        this.ddlSearchBy.Items.Add(Item);

                        if (Convert.ToBoolean(Row[AppItemDetailTable.FLD_IS_DEFAULT])) {
                            sDefaultSearch = Row[AppItemDetailTable.FLD_NAME].ToString();
                            ddlSearchBy.ClearSelection();
                            Item.Selected = true;
                        }
                    }
                }
            }
        }

        protected System.Web.UI.WebControls.ImageButton ButtonSearch {
            get {
                return imgBtnSearch;
            }
            set {
                imgBtnSearch = value;
            }
        }

        public void ApplySearch() {
            FireEvent();
        }

        protected QSP.WebControl.AlphaSearch ControlAlphaSearch {
            get {
                return ctrlAlphaSearch;
            }
            set {
                ctrlAlphaSearch = value;
            }
        }

        public System.Web.UI.WebControls.DropDownList DropDownListSearchBy {
            get {
                return ddlSearchBy;
            }
            set {
                ddlSearchBy = value;
            }
        }

        public System.Web.UI.WebControls.Label LabelSearchByAlpha {
            get {
                return lblSearchByAlpha;
            }
            set {
                lblSearchByAlpha = value;
            }
        }

        protected System.Web.UI.WebControls.Label LabelHeader {
            get {
                return lblHeader;
            }
            set {
                lblHeader = value;
            }
        }

        protected System.Web.UI.WebControls.TextBox TextBoxCriteria {
            get {
                return txtCriteria;
            }
            set {
                txtCriteria = value;
            }
        }

        public QSPForm.Business.AppItem SearchAppItem {
            get {
                return searchAppItem;
            }
            set {
                searchAppItem = value;
            }
        }

        /// <summary>
        /// this information is available only after a databind
        /// </summary>
        public string DefaultSearch {
            get { return sDefaultSearch; }
        }

        /// <summary>
        /// Get and Set text entered
        /// </summary>
        public string FilterExpression {
            get {
                return txtCriteria.Text.Trim();
            }
            set {
                txtCriteria.Text = value;
            }
        }

        /// <summary>
        /// Get and Set value of the search criteria
        /// </summary>
        public int SearchMode {
            get {
                return Convert.ToInt32(ddlSearchBy.SelectedItem.Value);
            }
            set {
                ListItem lstItem;
                lstItem = ddlSearchBy.Items.FindByValue(value.ToString());
                if (lstItem != null) {
                    ddlSearchBy.ClearSelection();
                    lstItem.Selected = true;
                }
            }
        }

        /// <summary>
        /// get text of the search criteria
        /// </summary>
        public string Criteria {
            get { return ddlSearchBy.SelectedItem.Text; }
        }

        private string SearchCriteriaAlphaSearch {
            get {
                return this.lblSearchByAlpha.Text;
            }
            set {
                this.lblSearchByAlpha.Text = value;
            }
        }

        private void ctrlAlphaSearchClick(object sender, QSP.WebControl.AlphaSearchClickedArgs e) {
            FireEvent(e);
        }

        private void FireEvent(QSP.WebControl.AlphaSearchClickedArgs e) {
            if (OnSearch != null)
                OnSearch(this, new SearchModuleClickedArgs(e.CharSelected.ToString(), 0, this.ddlSearchBy.SelectedItem.Text));
        }

        private void CreateHeader() {
            int index = 0;
            this.lblHeader.Text = HEADER_TEXT;

            if (dtsAppItemData != null) {
                foreach (DataRowView row in DVSearchOption) {
                    if (index >= 3) {
                        lblHeader.Text += ", ...";
                        break;
                    }
                    if (index >= 1)
                        lblHeader.Text += ", ";

                    lblHeader.Text += row[AppItemTable.FLD_NAME];
                    index++;
                }
            }
        }

        private void AddJavascript() {
            if (imgBtnSearch != null) {
                //System.Text.StringBuilder strBuild = new System.Text.StringBuilder();
                //strBuild.Append("<script language=javascript>\n");
                //strBuild.Append("<!--			\n");	
                //strBuild.Append("	function InspectKeyCode()\n");
                //strBuild.Append("	{\n");
                //strBuild.Append("		if(event.keyCode == 13) {\n");
                //strBuild.Append("			event.keyCode = 0;\n");
                //strBuild.Append("			event.returnValue=false; 	\n");
                //strBuild.Append("		" + this.Page.ClientScript.GetPostBackEventReference(this.imgBtnSearch, "OnClick") + "; 	\n");
                //strBuild.Append("		}\n");
                //strBuild.Append("	}\n"); 
                ////End of the script
                //strBuild.Append("//-->\n");
                //strBuild.Append("</script>");

                //this.Page.RegisterClientScriptBlock("InspectKeyCode_function",strBuild.ToString());	
                //string sJScript = "InspectKeyCode();";
                ////sJScript = "if(event.keyCode == 13) { event.keyCode = 0; event.returnValue = false; alert('test1'); } else {alert('test2'); }";// + this.imgBtnSearch.ClientID + ".click; }";
                ////sJScript = "if(event.keyCode == 13) " + this.Page.GetPostBackClientEvent(this.imgBtnSearch, "OnClick") + ";";
                //txtCriteria.Attributes.Add("OnKeyPress", sJScript);
            }
        }
    }
}