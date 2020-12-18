//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_ToolBar_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'ToolBar.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Business;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for ToolBar.
    /// </summary>
    public partial class ToolBar : BaseToolBar {
        public event EventHandler SaveClick;
        public event EventHandler EditClick;
        public event EventHandler DeleteClick;

        private void InitializeComponent() {
            this.imgBtnEdit.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnEdit_Click);
            this.imgBtnDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnDelete_Click);
            this.imgBtnSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSave_Click);
            this.Load += new EventHandler(Page_Load);
            this.PreRender += new EventHandler(ToolBar_PreRender);
        }

        protected override void OnInit(EventArgs e) {
            InitializeComponent();

            ////rights no longer use
            //if(this.Page.RightUpdate != null)
            //    this.imgBtnEdit.Visible = this.Page.RightUpdate;
            //if (this.Page.RightUpdate != null)
            //    this.imgBtnSave.Visible = this.Page.RightUpdate;
            //if (this.Page.RightDelete != null)
            //    this.imgBtnDelete.Visible = this.Page.RightDelete;

            //Eric Charest, not right rules;
            //RNKBACK
            //this.imgBtnEdit.Visible = (this.Page.Role >= AuthSystem.ROLE_ADMINISTRATOR); // (this.Page.RightInsert);
            //this.imgBtnSave.Visible = (this.Page.Role >= AuthSystem.ROLE_ADMINISTRATOR);
            //this.imgBtnDelete.Visible = (this.Page.Role >= AuthSystem.ROLE_ADMINISTRATOR);

            base.OnInit(e);
        }

        #region Event
        protected void Page_Load(object sender, System.EventArgs e) {
            //setDisplayMode();
        }

        private void imgBtnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            if (EditClick != null)
                EditClick(sender, e);
        }

        private void imgBtnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            if (DeleteClick != null)
                DeleteClick(sender, e);
        }

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            if (SaveClick != null)
                SaveClick(sender, e);
        }
        #endregion Event

        #region Property
        /// <summary>
        /// Specify which button to display: 1 - READ MODE, 2 - EDIT MODE
        /// </summary>
        override public ImageButton DeleteButton {
            get { return this.imgBtnDelete; }
        }

        override public ImageButton EditButton {
            get { return this.imgBtnEdit; }
        }

        public ImageButton SaveButton {
            get { return this.imgBtnSave; }
        }

        override public HyperLink CancelButton {
            get { return this.hypLnkCancel; }
        }

        override public int DisplayMode {
            get {
                try { return Convert.ToInt32(this.ViewState["DisplayMode"]); }
                catch { return 0; }
            }
            set {
                this.ViewState["DisplayMode"] = value;
                SetDisplayMode();
            }
        }

        //		public bool ShowSaveButton
        //		{
        //			get
        //			{
        //				try{return Convert.ToBoolean(this.ViewState["ShowSaveButton"]);}
        //				catch{return false;}
        //			}
        //			set
        //			{
        //				this.ViewState["ShowSaveButton"]=value;
        //				this.imgBtnSave.Visible = value;
        //			}
        //		}
        //		public bool ShowDeleteButton
        //		{

        override public bool ShowDeleteButton {
            set {
                this.imgBtnDelete.Visible = value;
            }
        }

        //		public bool ShowEditButton
        //		{
        //			get
        //			{
        //				try{return Convert.ToBoolean(this.ViewState["ShowEditButton"]);}
        //				catch{return false;}
        //			}
        //			set
        //			{
        //				this.ViewState["ShowEditButton"]=value;
        //				this.imgBtnEdit.Visible = value;
        //			}
        //		}
        #endregion Propety

        private void SetDisplayMode() {
            switch (this.DisplayMode) {
                case DISPLAY_READ:
                    this.trEditMode.Visible = false;
                    this.trReadOnlyMode.Visible = true;
                    break;
                case DISPLAY_EDIT:
                    this.trEditMode.Visible = true;
                    this.trReadOnlyMode.Visible = false;
                    break;
                case DISPLAY_CLOSE:
                    this.trEditMode.Visible = false;
                    this.trReadOnlyMode.Visible = true;
                    this.imgBtnEdit.Visible = false;
                    break;
                default:
                    this.trEditMode.Visible = false;
                    this.trReadOnlyMode.Visible = false;
                    break;
            }
        }

        private void ToolBar_PreRender(object sender, EventArgs e) {
        }
    }
}