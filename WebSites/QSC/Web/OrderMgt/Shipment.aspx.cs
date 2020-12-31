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

namespace QSPFulfillment.OrderMgt
{
    /// <summary>
    /// Summary description for Shipment.
    /// </summary>
    public class Shipment : QSPFulfillment.CommonWeb.QSPPage
    {
        protected System.Web.UI.WebControls.DataGrid DataGrid1;
        protected System.Web.UI.HtmlControls.HtmlForm Form1;

        protected string zProductLine;
        protected string zOrderIDS;
        protected string zProductCode;
        protected int nBackorderOnly;
        protected System.Web.UI.WebControls.Label lblUsername;
        protected System.Web.UI.WebControls.TextBox tbDateShipped;
        protected System.Web.UI.WebControls.DropDownList ddCarrier;
        protected System.Web.UI.WebControls.TextBox tbWeight;
        protected System.Web.UI.WebControls.DropDownList ddWeightUnit;
        protected System.Web.UI.WebControls.TextBox tbWaybill;
        protected System.Web.UI.WebControls.TextBox tbExpectedDeliveryDate;
        protected System.Web.UI.WebControls.TextBox tbCartonsShipped;
        protected System.Web.UI.WebControls.TextBox tbSkids;
        protected System.Web.UI.WebControls.TextBox tbNote;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.DropDownList ddTop;
        protected System.Web.UI.WebControls.Button Button1;
        protected System.Web.UI.WebControls.Button Button2;
        protected System.Web.UI.WebControls.TextBox OrderIDs;
        protected System.Web.UI.WebControls.Button SearchBtn;
        protected System.Web.UI.WebControls.DropDownList ddPrint;
        protected System.Web.UI.WebControls.CheckBox chkbBackOrderOnly;
        protected System.Web.UI.WebControls.TextBox tbItemDesc;
        protected int nIndividualOrders;
        protected QSPFulfillment.OrderMgt.UC.ShipmentGroup ucShipmentGroup;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            if (!IsPostBack)
            {
                //zProductLine= Request.QueryString["P"].ToString();
                //nIndividualOrders = Convert.ToInt32(Request.QueryString["I"].ToString());
                PopulateCarriers();
                PopulateShipmentGroups();
                ucShipmentGroup.SelectedValue = 0;
                PopulateDG();
                tbDateShipped.Text = Convert.ToString(System.DateTime.Now);
                lblUsername.Text = aUserProfile.UserName;

            }
        }

        private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            PackingSlipLinkButton hylPackingSlipLinkButton = null;

            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem ||
                e.Item.ItemType == ListItemType.EditItem ||
                e.Item.ItemType == ListItemType.SelectedItem)
            {
                try
                {
                    hylPackingSlipLinkButton = (PackingSlipLinkButton)e.Item.FindControl("hylPackingSlipLinkButton");
                }
                catch { }

                if (hylPackingSlipLinkButton != null)
                {
                    hylPackingSlipLinkButton.DataBind();
                }
            }
        }

        private void PopulateCarriers()
        {
            Business.Shipment oShipment = new Business.Shipment();
            ddCarrier.DataSource = oShipment.GetCarriers();
            ddCarrier.DataBind();

        }

        private void PopulateShipmentGroups()
        {
            this.ucShipmentGroup.Bind();
        }

        private void PopulateDG()
        {
            zOrderIDS = this.OrderIDs.Text.ToString();
            zProductLine = Request.QueryString["P"].ToString();
            nIndividualOrders = Convert.ToInt32(Request.QueryString["I"].ToString());
            zProductCode = this.tbItemDesc.Text.ToString();
            if (this.chkbBackOrderOnly.Checked)
            { nBackorderOnly = 1; }
            else
            { nBackorderOnly = 0; }
            int nShipmentGroupID = this.ucShipmentGroup.SelectedValue;
            Business.Shipment oShipment = new Business.Shipment();
            //MS June 21, 07
            DataGrid1.DataSource = oShipment.GetShippableOrders(Convert.ToInt32(ddTop.SelectedValue), zProductLine, nIndividualOrders, zOrderIDS, Convert.ToInt32(ddPrint.SelectedValue), zProductCode, nBackorderOnly, nShipmentGroupID);
            DataGrid1.DataBind();

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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            this.chkbBackOrderOnly.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            this.ddTop.SelectedIndexChanged += new System.EventHandler(this.ddTop_SelectedIndexChanged);
            this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
            this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void ddTop_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            zProductLine = Request.QueryString["P"].ToString();
            nIndividualOrders = Convert.ToInt32(Request.QueryString["I"].ToString());
            zProductCode = this.tbItemDesc.Text.ToString();
            this.DataGrid1.PageSize = Convert.ToInt32(this.ddTop.SelectedValue);

            PopulateDG();
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            Business.Shipment oShipment = new Business.Shipment();
            Int32 lShipmentId;
            bool bIsShip;
            bool bError = false;
            string sOrderIds = "";
            HtmlInputHidden oHidden = new HtmlInputHidden();
            HtmlInputHidden oHiddenCOH = new HtmlInputHidden();
            HtmlInputHidden oHiddenTransID = new HtmlInputHidden();

            lblMessage.Text = "";

            if (tbDateShipped.Text + "" == "")
            {
                // error msg here
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = lblMessage.Text + "You must enter a shipment date.<BR>";
                bError = true;

            }
            else
            {
                if (IsDate(tbDateShipped.Text) == false)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = lblMessage.Text + "You must enter a valid shipment date.<BR>";
                    bError = true;
                }

            }

            if (tbWaybill.Text + "" == "")
            {
                // error msg here
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = lblMessage.Text + "You must enter a Waybill Number.<BR>";
                bError = true;
            }

            if (tbCartonsShipped.Text + "" == "")
            {
                tbCartonsShipped.Text = "0";
                // error msg here
                /*lblMessage.ForeColor = System.Drawing.Color.Red;
				lblMessage.Text = lblMessage.Text + "You must enter the number of cartons shipped.<BR>";
				bError = true;*/
            }
            else
            {
                try
                {
                    int l1 = (int)Convert.ToInt32(tbCartonsShipped.Text);

                    if (l1 < 0)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = lblMessage.Text + "You must enter a valid number for the number of cartons shipped.<BR>";
                        bError = true;
                    }
                }
                catch
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = lblMessage.Text + "You must enter the number for the number of cartons shipped.<BR>";
                    bError = true;
                }

            }


            if (tbWeight.Text + "" == "")
            {
                tbWeight.Text = "0";
                // error msg here
                /*lblMessage.ForeColor = System.Drawing.Color.Red;
				lblMessage.Text = lblMessage.Text + "You must enter the weight.<BR>";
				bError = true;*/
            }
            else
            {
                try
                {
                    double l2 = Convert.ToDouble(tbWeight.Text);

                    if (l2 < 0)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = lblMessage.Text + "You must enter a valid weight.<BR>";
                        bError = true;
                    }
                }
                catch
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = lblMessage.Text + "You must enter a valid weight.<BR>";
                    bError = true;
                }

            }

            if (tbSkids.Text + "" == "")
            {
                tbSkids.Text = "0";
                // error msg here
                /*lblMessage.ForeColor = System.Drawing.Color.Red;
				lblMessage.Text = lblMessage.Text + "You must enter the number of skids.<BR>";
				bError = true;*/
            }
            else
            {
                try
                {
                    int l3 = (int)Convert.ToInt32(tbSkids.Text);

                    if (l3 < 0)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = lblMessage.Text + "You must enter a valid number for the number of skids.<BR>";
                        bError = true;
                    }
                }
                catch
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = lblMessage.Text + "You must enter the number for the number of skids.<BR>";
                    bError = true;
                }

            }


            if (bError == false)
            {

                string sShipDate = Convert.ToString(tbDateShipped.Text);
                string sWeightUnitOfMeasure = Convert.ToString(ddWeightUnit.SelectedValue);
                string sComment = Convert.ToString(tbNote.Text);
                string sWayBill = Convert.ToString(tbWaybill.Text);
                int lCarrierId = Convert.ToInt32(ddCarrier.SelectedValue);
                int lNumberOfBoxes = Convert.ToInt32(tbCartonsShipped.Text);
                int lNumberOfSkids = Convert.ToInt32(tbSkids.Text);
                double dWeight = Convert.ToDouble(tbWeight.Text);

                int lUserId = Convert.ToInt32(aUserProfile.Instance);

                int? shipmentGroupID = null;
                bool shipmentGroupIDSet = false;

                foreach (DataGridItem dgItem in DataGrid1.Items)
                {
                    CheckBox oCheckbox = new CheckBox();
                    oCheckbox = (CheckBox)dgItem.FindControl("cbShip");

                    bIsShip = oCheckbox.Checked;

                    if (bIsShip)
                    {
                        oHiddenCOH = (HtmlInputHidden)dgItem.FindControl("OrderHeader");
                        oHidden = (HtmlInputHidden)dgItem.FindControl("HOrderId");
                        oHiddenTransID = (HtmlInputHidden)dgItem.FindControl("TransID");

                        if (sOrderIds + "" == "")
                        {
                            sOrderIds = oHidden.Value;
                        }
                        else
                        {
                            sOrderIds = sOrderIds + "," + oHidden.Value;
                        }

                        HtmlInputHidden hihShipmentGroupID = (HtmlInputHidden)dgItem.FindControl("ShipmentGroupID");
                        int? sgID;
                        if (hihShipmentGroupID.Value == "")
                        {
                            sgID = null;
                        }
                        else
                        {
                            sgID = Convert.ToInt32(hihShipmentGroupID.Value);
                        }

                        if (sgID != shipmentGroupID && shipmentGroupIDSet)
                        {
                            bError = true;
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Text = "You can only Ship Confirm Orders with the same Shipment Group in a particular Shipment.";
                        }
                        else
                        {
                            shipmentGroupIDSet = true;
                            shipmentGroupID = sgID;
                        }
                    }
                }
                nIndividualOrders = Convert.ToInt32(Request.QueryString["I"].ToString());

                if (sOrderIds + "" == "")
                {
                    bError = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "You must choose atleast one batch for shipment.";

                }

                if (bError == false)
                {
                    if (nIndividualOrders == 0)
                    {


                        lShipmentId = oShipment.ShipBatch(sOrderIds, 1,
                                    (int)lCarrierId, sShipDate, "1/1/1995", lNumberOfBoxes,
                                    (float)dWeight, lNumberOfSkids, sWeightUnitOfMeasure, sComment,
                                    Session.SessionID, lUserId, sWayBill, Request.QueryString["P"].ToString(), shipmentGroupID);

                        // Now add the entries to the waybill table.
                        oShipment.InsertShipmentWaybill(lShipmentId, sWayBill);

                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Orders Shipped Successfully.  Shipment Id is <a href='ShipmentHistory.aspx?ShipmentId=" + Convert.ToString(lShipmentId) + "'>" + Convert.ToString(lShipmentId) + "</a>.";
                        PopulateDG();
                        ClearFormFields();
                    }
                    else if (nIndividualOrders == 1)
                    {

                        int orderHeader = Convert.ToInt32(oHiddenCOH.Value);
                        int nTransID = Convert.ToInt32(oHiddenTransID.Value);

                        lShipmentId = oShipment.ShipOrderItem(orderHeader, nTransID, 1,
                            (int)lCarrierId, sShipDate, "1/1/1995", lNumberOfBoxes,
                            (float)dWeight, lNumberOfSkids, sWeightUnitOfMeasure, sComment,
                            Session.SessionID, lUserId, sWayBill);

                        // Now add the entries to the waybill table.
                        oShipment.InsertShipmentWaybill(lShipmentId, sWayBill);

                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Orders Shipped Successfully.  Shipment Id is <a href='ShipmentHistory.aspx?ShipmentId=" + Convert.ToString(lShipmentId) + "'>" + Convert.ToString(lShipmentId) + "</a>.";
                        PopulateDG();
                        ClearFormFields();
                    }
                }
            }
        }

        private void ClearFormFields()
        {
            ddCarrier.SelectedIndex = 0;
            ddWeightUnit.SelectedIndex = 0;
            tbDateShipped.Text = Convert.ToString(System.DateTime.Now);
            tbWeight.Text = "";
            tbWaybill.Text = "";
            tbNote.Text = "";
            tbCartonsShipped.Text = "";
            tbSkids.Text = "";

        }

        /// <summary>
        /// Checks whether or not a date is a valid date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsDate(string sdate)
        {
            DateTime dt;
            bool isDate = true;
            try
            {
                dt = DateTime.Parse(sdate);
            }
            catch
            {
                isDate = false;
            }
            return isDate;
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            Button1_Click(sender, e);
        }

        private void SearchBtn_Click(object sender, System.EventArgs e)
        {
            PopulateDG();
        }

        private void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "PrintPackingSlip")
            {
            }
        }

        private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {

        }
    }
}
