using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using QSPForm.Business;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for StateSelector.
    /// </summary>
    public partial class SubdivisionSelector : System.Web.UI.UserControl {
        protected System.Web.UI.WebControls.ListBox lbxCurrentSubdivision;
        private SubdivisionCollection selectedSubdivision;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (IsPostBack) {
                if (this.GetSelectedSubdivision().Length != 0) {
                    AdjustSelectedSubdivision();
                    SetValue();
                }
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
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
        private void InitializeComponent() {
        }
        #endregion

        #region Property

        public SubdivisionCollection SelectedSubdivision {
            get { return selectedSubdivision; }
            set { this.selectedSubdivision = value; }
        }

        private DataTable AvailableSubdivision {
            get { return (DataTable)this.ViewState["availablesubdivision"]; }
            set { this.ViewState["availablesubdivision"] = value; }
        }

        //		#region CustomJavaScriptEvent
        //			public string OnBeforeSelectSubdivision
        //			{
        //				set{this.ViewState["OnBeforeSelectSubdivision"]=value;}
        //			}
        //			public string OnAfterSeleectSubdivision
        //			{
        //				set{this.ViewState["OnAfterSeleectSubdivision"]=value;}
        //			}
        //			public string OnBeforeRemoveSubdivision
        //			{
        //				set{this.ViewState["OnBeforeRemoveSubdivision"]=value;}
        //			}
        //			public string OnAfterRemoveSubdivision
        //			{
        //				set{this.ViewState["OnAfterRemoveSubdivision"]=value;}
        //			}
        //			private string OnBeforeSelectSubdivision
        //			{
        //				get{return this.ViewState["OnBeforeSelectSubdivision"];}
        //			}
        //			private string OnAfterSeleectSubdivision
        //			{
        //				get{return this.ViewState["OnAfterSeleectSubdivision"];}
        //			}
        //			private string OnBeforeRemoveSubdivision
        //			{
        //				get{return this.ViewState["OnBeforeRemoveSubdivision"];}
        //			}
        //			private string OnAfterRemoveSubdivision
        //			{
        //				get{return this.ViewState["OnAfterRemoveSubdivision"];}
        //			}
        //		#endregion CustomJavaScriptEvent

        #endregion Property

        #region Method
        public override void DataBind() {
            LoadData();
            SetValue();
        }

        private void LoadData() {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            // add view with role
            this.AvailableSubdivision = comSys.SelectAllUSState();

        }

        private void SetValue() {
            if (this.selectedSubdivision.Count == 0) {
                lbxSubdivision.DataSource = this.AvailableSubdivision;
                lbxSubdivision.DataTextField = QSPForm.Common.DataDef.SubdivisionTable.FLD_SUBDIVISION_NAME_1;
                lbxSubdivision.DataValueField = QSPForm.Common.DataDef.SubdivisionTable.FLD_PKID;
                lbxSubdivision.DataBind();
            }
            else {
                LoadSelectedSubdivision();
                LoadAvailableSubdivision();
            }
        }

        private void LoadSelectedSubdivision() {
            lbxSelectedSubdivision.Items.Clear();
            hidSelectedSubdivision.Value = "";

            foreach (Subdivision sub in selectedSubdivision) {
                lbxSelectedSubdivision.Items.Add(new ListItem(sub.SubdivisionName1, sub.SubdivisionCode));
                hidSelectedSubdivision.Value += sub.SubdivisionCode + ", ";
            }
        }

        private void LoadAvailableSubdivision() {
            lbxSubdivision.Items.Clear();
            foreach (DataRow row in this.AvailableSubdivision.Rows) {
                if (this.selectedSubdivision[row[QSPForm.Common.DataDef.SubdivisionTable.FLD_PKID].ToString()] == null) {
                    lbxSubdivision.Items.Add(new ListItem(row[QSPForm.Common.DataDef.SubdivisionTable.FLD_SUBDIVISION_NAME_1].ToString(), row[QSPForm.Common.DataDef.SubdivisionTable.FLD_PKID].ToString()));
                }
            }
        }

        private void AdjustSelectedSubdivision() {
            SubdivisionCollection col = new SubdivisionCollection();
            DataView dv = new DataView(this.AvailableSubdivision);
            dv.Sort = QSPForm.Common.DataDef.SubdivisionTable.FLD_PKID;
            int iIndex;
            foreach (string s in GetSelectedSubdivision()) {
                if (s.Trim() != String.Empty) {
                    iIndex = dv.Find(s.Trim());
                    if (iIndex > -1) {
                        col.Add(new Subdivision(dv[iIndex][QSPForm.Common.DataDef.SubdivisionTable.FLD_PKID].ToString(),
                            dv[iIndex][QSPForm.Common.DataDef.SubdivisionTable.FLD_SUBDIVISION_NAME_1].ToString()));
                    }
                }
            }
            this.selectedSubdivision = col;
        }

        public string[] GetSelectedSubdivision() {
            string list = hidSelectedSubdivision.Value;
            if (list.Length > 2) {
                list = list.Substring(0, list.Length - 2);
                list = list.Trim();
            }
            return list.Split(',');
        }
        #endregion Method
    }
}