using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Xml;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for ExpressionBuilder.
    /// </summary>
    public partial class ExpressionBuilder : System.Web.UI.UserControl {
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPosition;
        private CommonUtility clsUtil = new CommonUtility();

        private const string ERR01 = "Cannot Load Data";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                //this.Page.SmartNavigation = true;
                FillTreeView();

                if (Request["IDRefCtrl"] != null) {
                    IDRefCtrl = Request["IDRefCtrl"].ToString();
                }

                if (Request["NameRefCtrl"] != null) {
                    NameRefCtrl = Request["NameRefCtrl"].ToString();
                }

                //				if (Request["formID"] != null)
                //				{
                //					formID = Request["formID"].ToString();
                //				}
            }

            //clsUtil.SetJScriptForCloseSelector(imgBtnOK,"document.getElementById('"+this.txtExpression.ClientID+"').value",this.IDRefCtrl);

            //txtExpression.Attributes["onbeforedeactivate"] = "SavePosition(this);";
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
            this.imgBtnOK.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnOK_Click);
            this.tvBusinessRule.SelectedNodeChanged += new EventHandler(tvBusinessRule_SelectedNodeChanged);
            this.tvFunction.SelectedNodeChanged += new EventHandler(tvFunction_SelectedNodeChanged);
            this.tvOperation.SelectedNodeChanged += new EventHandler(tvOperation_SelectedNodeChanged);
            this.tvBusinessRule.TreeNodeDataBound += new TreeNodeEventHandler(tvBusinessRule_TreeNodeDataBound);
        }

        private void tvBusinessRule_TreeNodeDataBound(object sender, TreeNodeEventArgs e) {
        }
        #endregion

        private string LocalHostPath {
            get {
                try {
                    return Request.Path;
                }
                catch {
                    return String.Empty;
                }
            }
        }

        private string formID {
            get {
                try {
                    string formID = "0";
                    if (Request["formID"] != null) {
                        formID = Request["formID"].ToString();
                    }
                    return formID;
                }
                catch { return String.Empty; }
            }
            set { this.ViewState["formID"] = value; }
        }

        private string IDRefCtrl {
            get {
                try { return this.ViewState["IdRefCtrl"].ToString(); }
                catch { return String.Empty; }
            }
            set { this.ViewState["IdRefCtrl"] = value; }
        }

        private string NameRefCtrl {
            get {
                try { return this.ViewState["NameRefCtrl"].ToString(); }
                catch { return String.Empty; }
            }
            set { this.ViewState["NameRefCtrl"] = value; }
        }

        private void imgBtnOK_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            this.Page.RegisterClientScriptBlock("", clsUtil.GetJScriptForCloseSelector(txtExpression.Text, "", IDRefCtrl, ""));
        }

        private void FillTreeView() {
            try {
                //LoadFromXML(tvOperation,this.LocalHostPath+"/XML/Operators.xml","Operators");
                //LoadFromXML(tvFunction,this.LocalHostPath+"/XML/Functions.xml","Functions");
                //LoadFromXML(tvBusinessRule,this.LocalHostPath+"/XML/Business.xml","Business Rules");
                LoadBusinessRule();
            }
            catch (Exception ex) {
                this.lblError.Visible = true;
                this.lblError.Text = ERR01 + "(" + ex.Message + ")";
            }
        }

        private void LoadBusinessRule() {
            /*
             * Global Variables (Business Field selectall entitytype() ), 
             * Global Caculated fields(Business Rule Base form ), 
             * Business Rule(current form)
            */
            QSPForm.Business.BusinessRuleSystem bSystem = new QSPForm.Business.BusinessRuleSystem();
            DataTable tblBusiness = bSystem.SelectAllByFormID(Convert.ToInt32(this.formID), true);

            TreeNode SpecificRoot = new TreeNode();
            SpecificRoot.Text = "Specific business rules";
            TreeNode calculatedField = new TreeNode();
            calculatedField.Text = "Global Calculated Fields";
            TreeNode globalVariable = new TreeNode();
            globalVariable.Text = "Global Variables";

            //LoadFromDataTable(SpecificRoot,tblBusiness,QSPForm.Common.DataDef.BusinessRuleTable.FLD_FIELD_NAME);
            // To split node between Global variables and business rule -- 
            //***********************************************************
            TreeNode newTreeNode;
            foreach (DataRow row in tblBusiness.Rows) {
                newTreeNode = new TreeNode();
                newTreeNode.Text = row[QSPForm.Common.DataDef.BusinessRuleTable.FLD_FIELD_NAME].ToString();
                if (row[QSPForm.Common.DataDef.BusinessRuleTable.FLD_FORM_ID].ToString() != this.formID) {
                    globalVariable.ChildNodes.Add(newTreeNode);
                }
                else {
                    SpecificRoot.ChildNodes.Add(newTreeNode);
                }
            }
            //***********************************************************

            QSPForm.Business.BusinessFieldSystem bfSys = new QSPForm.Business.BusinessFieldSystem();
            tblBusiness = bfSys.SelectAllByFormID(Convert.ToInt32(this.formID));

            LoadFromDataTable(calculatedField, tblBusiness, QSPForm.Common.DataDef.BusinessRuleTable.FLD_FIELD_NAME);


            this.tvBusinessRule.Nodes.Clear();
            this.tvBusinessRule.Nodes.Add(SpecificRoot);
            this.tvBusinessRule.Nodes.Add(calculatedField);
            this.tvBusinessRule.Nodes.Add(globalVariable);
        }

        private void LoadFromDataTable(TreeNode tn, DataTable dt, string textColumnName) {
            TreeNode newTreeNode;
            foreach (DataRow row in dt.Rows) {
                newTreeNode = new TreeNode();
                newTreeNode.Text = row[textColumnName].ToString();
                tn.ChildNodes.Add(newTreeNode);
            }
        }

        private void LoadFromXML(TreeView tv, string path, string rootName) {

            XmlDocument dom = new XmlDocument();
            XmlDocument newDom = new XmlDocument();
            TreeNode tn = new TreeNode();
            dom.Load(path);
            XmlNodeList nodeList = dom.SelectNodes("treeNodes");

            tv.Nodes.Clear();
            tn.Text = rootName;
            AddNode(nodeList, tn);
            tv.Nodes.Add(tn);
        }

        private void AddNode(XmlNodeList nodeList, TreeNode parent) {
            TreeNode treeNode;

            foreach (XmlNode node in nodeList) {
                if (node.LocalName == "treeNode") {
                    treeNode = new TreeNode();
                    treeNode.Text = node.Attributes[0].InnerText.Replace(" ", "&nbsp;");
                    parent.ChildNodes.Add(treeNode);

                    if (node.HasChildNodes) {
                        AddNode(node.ChildNodes, treeNode);
                    }
                }
                else if (node.HasChildNodes) {
                    AddNode(node.ChildNodes, parent);
                }
            }
        }

        protected void tvBusinessRule_SelectedNodeChanged(object sender, EventArgs e) {
            txtExpression.Text = txtExpression.Text + tvBusinessRule.SelectedNode.Text;
        }

        protected void tvOperation_SelectedNodeChanged(object sender, EventArgs e) {
            txtExpression.Text = txtExpression.Text + tvOperation.SelectedNode.Text;
        }

        protected void tvFunction_SelectedNodeChanged(object sender, EventArgs e) {
            txtExpression.Text = txtExpression.Text + tvFunction.SelectedNode.Text;
        }
    }
}