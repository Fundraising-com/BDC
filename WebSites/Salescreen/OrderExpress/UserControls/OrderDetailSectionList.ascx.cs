using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using System.Configuration;
using dataDef = QSPForm.Common.DataDef.OrderDetailTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls
{
    /// <summary>
    ///		Summary description for Phone Number List.
    /// </summary>
    public partial class OrderDetailSectionList : BaseOrderDetailSectionList//BaseWebUserControl
    {
        protected dataDef dTblOrderDetail = new dataDef();
        protected FormSectionTable dTblFormSection = new FormSectionTable();
        protected DataView dvFormSection = new DataView();
        private OrderData dtsOrder = new OrderData();
        private int c_ParentID;
        private int c_FormSectionTypeID = 0;
        private int c_FormSectionNumber = 0;
        protected CatalogItemDetailTable tblCatalogItemDetail = new CatalogItemDetailTable();
        private int c_FormID = 0;
        private decimal profitRate = 0;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here				
            //			if (IsFirstLoad)
            //			{

            //			}
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CustVal_MinQty.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.CustVal_MinQty_ServerValidate);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        private void SetBusinessMessage()
        {
            CommonUtility clsUtil = new CommonUtility();
            clsUtil.SetFormBusinessMessage(lblBusinessMessage, QSPForm.Business.AppItem.OrderForm_Step4, c_FormID, c_FormSectionTypeID);
            trBusinessMessage.Visible = (lblBusinessMessage.Text.Length > 0);
            if (trBusinessMessage.Visible)
            {
                lblBusinessMessage.Text = "<BR>" + lblBusinessMessage.Text + "<BR>";
            }

        }

        protected void Page_DataBinding(object sender, System.EventArgs e)
        {
            try
            {
                //retreive data detail item for db
                //Init DataList		

            }
            catch (Exception ex)
            {
                this.Page.SetPageError(ex);
            }
        }

        public override void BindForm()
        {
            try
            {
                //retreive data detail item for db
                //Init DataList
                FillFilter();
                SetBusinessMessage();
                BindSectionList();
            }
            catch (Exception ex)
            {
                this.Page.SetPageError(ex);
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            ManageSectionDisplay();
            SetJScriptForSectionGridCalculation();
        }

        private void ManageSectionDisplay()
        {
            if (tblSection1.Visible)
            {
                OrderDetailList_Section1.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section1.FormSectionNumber = 1;
            }
            if (tblSection2.Visible)
            {
                OrderDetailList_Section2.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section2.FormSectionNumber = 2;
            }
            if (tblSection3.Visible)
            {
                OrderDetailList_Section3.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section3.FormSectionNumber = 3;
            }
        }

        public override int ParentID
        {
            get
            {
                return c_ParentID;
            }
            set
            {
                c_ParentID = value;
            }
        }

        public int FormSectionTypeID
        {
            get
            {
                return c_FormSectionTypeID;
            }
            set
            {
                c_FormSectionTypeID = value;
            }
        }

        public int FormSectionNumber
        {
            get
            {
                return c_FormSectionNumber;
            }
            set
            {
                c_FormSectionNumber = value;
            }
        }

        public decimal ProfitRate
        {
            get
            {
                if (profitRate == 0)
                {
                    profitRate = Convert.ToDecimal(this.DataSource.OrderHeader.Rows[0][QSPForm.Common.DataDef.OrderHeaderTable.FLD_PROFIT_RATE].ToString());
                }

                return profitRate;
            }
            set
            {
                profitRate = value;
            }
        }

        public bool DisableQtyValidator
        {
            get
            {
                bool d = false;
                if (this.ViewState["DisableQtyValidator"] != null)
                    d = Convert.ToBoolean(this.ViewState["DisableQtyValidator"].ToString());
                return d;
            }
            set
            {
                this.ViewState["DisableQtyValidator"] = value;
            }
        }

        public override OrderData DataSource
        {
            get
            {
                return dtsOrder;
            }
            set
            {
                dtsOrder = value;
                dTblOrderDetail = dtsOrder.OrderDetail;
                if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FORM_ID))
                    c_FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);

            }
        }

        private void BindSectionList()
        {
            QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();
            dTblFormSection = frmSys.SelectAllFormSectionByFormID(c_FormID, c_FormSectionTypeID);

            dvFormSection.Table = dTblFormSection;
            string sFilter = "";
            string sFilterType = "";
            sFilterType = FormSectionTable.FLD_FORM_SECTION_TYPE_ID + " = " + c_FormSectionTypeID.ToString();
            dvFormSection.RowFilter = sFilterType;
            //Build Section
            bool IsSection1 = dTblFormSection.IsContainFormSectionType(c_FormSectionTypeID, 1);
            tblSection1.Visible = IsSection1;
            if (IsSection1)
            {
                string sTitle = "Section 1:";
                string sBusinessMessage = "";
                sFilter = sFilterType + " AND ISNULL(" + FormSectionTable.FLD_FORM_SECTION_NUMBER + ", 0) <= 1";
                dvFormSection.RowFilter = sFilter;
                if (dvFormSection.Count > 0)
                {
                    DataRow frmSecRow = dvFormSection[0].Row;
                    if (!frmSecRow.IsNull(FormSectionTable.FLD_FORM_SECTION_TITLE))
                    {
                        sTitle = frmSecRow[FormSectionTable.FLD_FORM_SECTION_TITLE].ToString();
                    }
                    if (!frmSecRow.IsNull(FormSectionTable.FLD_DESCRIPTION))
                    {
                        sBusinessMessage = frmSecRow[FormSectionTable.FLD_DESCRIPTION].ToString();
                    }
                }
                OrderDetailList_Section1.SectionTitle = sTitle;
                OrderDetailList_Section1.SectionBusinessMessage = sBusinessMessage;
                OrderDetailList_Section1.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section1.FormSectionNumber = 1;
                OrderDetailList_Section1.ProfitRate = this.ProfitRate;
                OrderDetailList_Section1.DataSource = this.DataSource;
                //if (IsPEForm)
                OrderDetailList_Section1.DisableQtyValidator = this.DisableQtyValidator;
                OrderDetailList_Section1.BindForm();
            }
            bool IsSection2 = dTblFormSection.IsContainFormSectionType(c_FormSectionTypeID, 2);
            tblSection2.Visible = IsSection2;
            if (IsSection2)
            {
                string sTitle = "Section 2:";
                string sBusinessMessage = "";
                sFilter = sFilterType + " AND " + FormSectionTable.FLD_FORM_SECTION_NUMBER + " = 2";
                dvFormSection.RowFilter = sFilter;
                if (dvFormSection.Count > 0)
                {
                    DataRow frmSecRow = dvFormSection[0].Row;
                    if (!frmSecRow.IsNull(FormSectionTable.FLD_FORM_SECTION_TITLE))
                    {
                        sTitle = frmSecRow[FormSectionTable.FLD_FORM_SECTION_TITLE].ToString();
                    }
                    if (!frmSecRow.IsNull(FormSectionTable.FLD_DESCRIPTION))
                    {
                        sBusinessMessage = frmSecRow[FormSectionTable.FLD_DESCRIPTION].ToString();
                    }
                }
                OrderDetailList_Section2.SectionTitle = sTitle;
                OrderDetailList_Section2.SectionBusinessMessage = sBusinessMessage;
                OrderDetailList_Section2.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section2.FormSectionNumber = 2;
                OrderDetailList_Section2.ProfitRate = this.ProfitRate;
                OrderDetailList_Section2.DataSource = this.DataSource;
                OrderDetailList_Section2.BindForm();
            }
            bool IsSection3 = dTblFormSection.IsContainFormSectionType(c_FormSectionTypeID, 3);
            tblSection3.Visible = IsSection3;
            if (IsSection3)
            {
                string sTitle = "";
                string sBusinessMessage = "";
                sFilter = sFilterType + " AND " + FormSectionTable.FLD_FORM_SECTION_NUMBER + " = 3";
                dvFormSection.RowFilter = sFilter;
                if (dvFormSection.Count > 0)
                {
                    DataRow frmSecRow = dvFormSection[0].Row;
                    if (!frmSecRow.IsNull(FormSectionTable.FLD_FORM_SECTION_TITLE))
                    {
                        sTitle = frmSecRow[FormSectionTable.FLD_FORM_SECTION_TITLE].ToString();
                    }
                    if (!frmSecRow.IsNull(FormSectionTable.FLD_DESCRIPTION))
                    {
                        sBusinessMessage = frmSecRow[FormSectionTable.FLD_DESCRIPTION].ToString();
                    }
                }
                OrderDetailList_Section3.SectionTitle = sTitle;
                OrderDetailList_Section3.SectionBusinessMessage = sBusinessMessage;
                OrderDetailList_Section3.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section3.FormSectionNumber = 3;
                OrderDetailList_Section3.ProfitRate = this.ProfitRate;
                OrderDetailList_Section3.DataSource = this.DataSource;
                OrderDetailList_Section3.BindForm();
            }
        }

        public override int FormID
        {
            get
            {
                return c_FormID;
            }
            set
            {
                c_FormID = value;
            }
        }

        //private bool IsPEForm
        //{
        //    get
        //    {
        //        bool ispe = false;
        //        foreach (string s in PEForm)
        //        {
        //            if (s == this.FormID.ToString())
        //            {
        //                ispe = true;
        //                break;
        //            }
        //        }
        //        return ispe;
        //    }
        //}

        //private string[] PEForm
        //{
        //    get
        //    {
        //        string peForm = "";
        //        if (ConfigurationManager.AppSettings["PEForm"] != null)
        //            peForm = ConfigurationManager.AppSettings["PEForm"].ToString();

        //        if (peForm.Length > 0)
        //            return peForm.Split(',');
        //        else
        //            return null;
        //    }
        //}

        public string LabelGrandTotalQuantityClientID
        {
            get
            {
                return lblTotalQuantity.ClientID;
            }
        }

        public string HiddenMinTotalQuantityClientID
        {
            get
            {
                return hdnMinTotalQuantity.ClientID;
            }
        }

        public override bool UpdateDataSource()
        {
            bool IsSuccess = false;
            CommonUtility clsUtil = new CommonUtility();
            //Update Order Detail and Profit Rate
            float profitRate = 0;
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            //if (radBtnLstProfitRate.SelectedIndex > -1)
            //{
            //    profitRate = Convert.ToSingle(radBtnLstProfitRate.SelectedValue);
            //}
            //clsUtil.UpdateRow(ordRow, OrderHeaderTable.FLD_PROFIT_RATE, profitRate.ToString());

            if (tblSection1.Visible)
            {
                OrderDetailList_Section1.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section1.FormSectionNumber = 1;
                OrderDetailList_Section1.DataSource = this.DataSource;
                OrderDetailList_Section1.UpdateDataSource();
            }
            if (tblSection2.Visible)
            {
                OrderDetailList_Section2.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section2.FormSectionNumber = 2;
                OrderDetailList_Section2.DataSource = this.DataSource;
                OrderDetailList_Section2.UpdateDataSource();
            }
            if (tblSection3.Visible)
            {
                OrderDetailList_Section3.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section3.FormSectionNumber = 3;
                OrderDetailList_Section3.DataSource = this.DataSource;
                OrderDetailList_Section3.UpdateDataSource();
            }

            //Operation Sucessful
            IsSuccess = true;

            return IsSuccess;
        }

        public override bool ValidateForm()
        {
            bool blnValid = false;
            if (tblSection1.Visible)
            {
                OrderDetailList_Section1.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section1.FormSectionNumber = 1;
                OrderDetailList_Section1.DataSource = this.DataSource;
                blnValid = OrderDetailList_Section1.ValidateForm();
            }
            if (tblSection2.Visible)
            {
                OrderDetailList_Section2.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section2.FormSectionNumber = 2;
                OrderDetailList_Section2.DataSource = this.DataSource;
                if (blnValid)
                    blnValid = OrderDetailList_Section2.ValidateForm();
            }
            if (tblSection3.Visible)
            {
                OrderDetailList_Section3.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailList_Section3.FormSectionNumber = 3;
                OrderDetailList_Section3.DataSource = this.DataSource;
                if (blnValid)
                    blnValid = OrderDetailList_Section3.ValidateForm();
            }

            //if everything have been ok
            //ValSum.Visible = !blnValid;
            //trValSum.Visible = !blnValid;
            return blnValid;
        }

        public void PreparePEForm()
        {
            if (!OrderDetailList_Section2.HasSelectedItem)
            {
                OrderDetailList_Section1.DisableQtyValidator = false;
            }
            else
            {
                OrderDetailList_Section1.DisableQtyValidator = true;
            }

            OrderDetailList_Section1.SetQtyValidator();
        }

        private void CustVal_MinQty_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            //int minTotalQty = 0;
            //int CountOfQty = 0;
            //Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
            //minTotalQty = bizSys.GetMinTotalQuantity(FormID);
            ////Personalized the error message
            //string tagToreplace = "[MinTotalQuantity]";
            //CustomValidator custVal = (CustomValidator) source;
            //custVal.ErrorMessage = custVal.ErrorMessage.Replace(tagToreplace, minTotalQty.ToString());

            //if (minTotalQty > 0)
            //{				
            //    for(int iCount = 0 ; iCount < dtgOrderDetail.Items.Count; iCount ++)
            //    {			
            //        DataGridItem dgItem = dtgOrderDetail.Items[iCount];
            //        //Quantity
            //        CompareValidator compVal = ((CompareValidator) dgItem.FindControl("compVal_Quantity"));
            //        TextBox txtQty = (TextBox) dgItem.FindControl("txtQuantity");
            //        compVal.Validate();				
            //        if(compVal.IsValid)				
            //        {
            //            if(txtQty.Text.Trim().Length > 0)
            //            {
            //                int qty = Convert.ToInt32(txtQty.Text);
            //                CountOfQty += qty;
            //            }
            //        }				
            //    }
            //    args.IsValid = (CountOfQty >= minTotalQty);
            //}
            //else
            args.IsValid = true;
        }

        private void FillFilter()
        {
            QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();
            FormProfitRateTable dTblFormProfit = frmSys.SelectAllProfitRateByFormID(FormID);
            radBtnLstProfitRate.DataValueField = FormProfitRateTable.FLD_PROFIT_RATE;
            radBtnLstProfitRate.DataTextField = FormProfitRateTable.FLD_PROFIT_RATE;
            radBtnLstProfitRate.DataTextFormatString = "{0:P2}";
            radBtnLstProfitRate.DataSource = dTblFormProfit;
            radBtnLstProfitRate.DataBind();
            float profit = 0;
            if (dTblFormProfit.Rows.Count > 0)
            {
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                if (!ordRow.IsNull(OrderHeaderTable.FLD_PROFIT_RATE))
                    profit = Convert.ToSingle(ordRow[OrderHeaderTable.FLD_PROFIT_RATE]);

                if (profit == 0)
                {
                    profit = dtsOrder.OrderDetail.CommonProfitRate;
                }

                ListItem lstItem = radBtnLstProfitRate.Items.FindByValue(profit.ToString());
                if (lstItem != null)
                {
                    radBtnLstProfitRate.ClearSelection();
                    lstItem.Selected = true;
                }
                else
                {
                    //Take the first one if...
                    if (radBtnLstProfitRate.Items.Count > 0)
                    {
                        radBtnLstProfitRate.ClearSelection();
                        radBtnLstProfitRate.Items[0].Selected = true;
                    }
                    else
                    { //if nothings is in the list
                    }
                }
            }
            else
                radBtnLstProfitRate.SelectedIndex = -1;

            //tblProfitRate.Visible = (radBtnLstProfitRate.Items.Count > 0);
            tblProfitRate.Visible = false;
        }

        protected void radBtnLstProfitRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalculatePrice();
            BindSectionList();
        }

        private void RecalculatePrice()
        {
            UpdateDataSource();
            float profitRate = 0;
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            if (!ordRow.IsNull(OrderHeaderTable.FLD_PROFIT_RATE))
            {
                profitRate = Convert.ToSingle(ordRow[OrderHeaderTable.FLD_PROFIT_RATE]);
            }
            if (profitRate > 0)
            {
                DataView dv = new DataView(this.dtsOrder.OrderDetail);
                string sFilter = "";
                sFilter = dataDef.FLD_FORM_SECTION_TYPE_ID + " = " + c_FormSectionTypeID.ToString();
                dv.RowFilter = sFilter;
                foreach (DataRowView drvwRow in dv)
                {
                    DataRow row = drvwRow.Row;
                    if (row.RowState != DataRowState.Deleted)
                    {
                        row[dataDef.FLD_PROFIT_RATE] = profitRate;
                        Decimal calc_price = Convert.ToDecimal(row[dataDef.FLD_CALCULATED_PRICE]);
                        row[dataDef.FLD_PRICE] = calc_price;
                    }
                }
            }
        }

        public void SetJScriptForSectionGridCalculation()
        {
            string sPrefix = "";
           
            if (c_FormSectionTypeID == QSPForm.Common.FormSectionType.STANDARD_PRODUCT)
                sPrefix = "StandardSection_";
            else if (c_FormSectionTypeID == QSPForm.Common.FormSectionType.OTHER_PRODUCT)
                sPrefix = "OtherSection_";
            string sArrTotalQtyCtrl = "";
            string sArrTotalQtyCtrl2 = ""; //for min quantity check
            string sArrTotalAmtCtrl = "";
            if (tblSection1.Visible)
            {
                sArrTotalQtyCtrl = "'" + OrderDetailList_Section1.LabelTotalQuantityClientID + "'";
                sArrTotalQtyCtrl2 = "'" + OrderDetailList_Section1.hdnMinTotalQuantityClientID + "'";
                sArrTotalAmtCtrl = "'" + OrderDetailList_Section1.LabelTotalAmountClientID + "'";
            }
            if (tblSection2.Visible)
            {
                if (sArrTotalQtyCtrl.Length > 0)
                    sArrTotalQtyCtrl = sArrTotalQtyCtrl + ",";
                sArrTotalQtyCtrl = sArrTotalQtyCtrl + "'" + OrderDetailList_Section2.LabelTotalQuantityClientID + "'";
                if (sArrTotalAmtCtrl.Length > 0)
                    sArrTotalAmtCtrl = sArrTotalAmtCtrl + ",";
                sArrTotalAmtCtrl = sArrTotalAmtCtrl + "'" + OrderDetailList_Section2.LabelTotalAmountClientID + "'";
            }
            if (tblSection3.Visible)
            {
                if (sArrTotalQtyCtrl.Length > 0)
                    sArrTotalQtyCtrl = sArrTotalQtyCtrl + ",";
                sArrTotalQtyCtrl = sArrTotalQtyCtrl + "'" + OrderDetailList_Section3.LabelTotalQuantityClientID + "'";
                if (sArrTotalAmtCtrl.Length > 0)
                    sArrTotalAmtCtrl = sArrTotalAmtCtrl + ",";
                sArrTotalAmtCtrl = sArrTotalAmtCtrl + "'" + OrderDetailList_Section3.LabelTotalAmountClientID + "'";
            }
            System.Text.StringBuilder sJScript = new System.Text.StringBuilder();
            sJScript.Append("<SCRIPT language=javascript>                                               \n");
            sJScript.Append("	                                                                        \n");
            sJScript.Append(" var " + sPrefix + "AmountList = new Array (" + sArrTotalAmtCtrl + ");\n");
            sJScript.Append(" var " + sPrefix + "QtyList = new Array (" + sArrTotalQtyCtrl + ");\n");
            sJScript.Append(" var " + sPrefix + "MinQtyList = new Array (" + sArrTotalQtyCtrl2 + ");\n");
            sJScript.Append(" var " + sPrefix + "lblAmountTotalID ='" + this.lblTotalAmount.ClientID + "';\n");
            sJScript.Append(" var " + sPrefix + "lblQTYTotalID = '" + this.lblTotalQuantity.ClientID + "';\n");
            sJScript.Append(" var " + sPrefix + "hdnMinTotalQuantity = '" + this.hdnMinTotalQuantity.ClientID + "';\n");
            sJScript.Append("	                                                                        \n");
            sJScript.Append("   function " + sPrefix + "RefreshSectionGrandTotal() \n");
            sJScript.Append("   { \n");
            sJScript.Append("       " + sPrefix + "RefreshSectionGrandTotalAmount(); \n");
            sJScript.Append("       " + sPrefix + "RefreshSectionTotalQuantity(); \n");
            sJScript.Append("       " + sPrefix + "RefreshSectionTotalQuantity2(); \n");
            sJScript.Append("   } \n");
            sJScript.Append("	                                                                        \n");
            sJScript.Append("   function " + sPrefix + "RefreshSectionGrandTotalAmount()\n");
            sJScript.Append("   {\n");
            sJScript.Append("       var cptList = 0.00;\n");
            sJScript.Append("       var sTotal;\n");
            sJScript.Append("       for(var x = 0; x < " + sPrefix + "AmountList.length; x++)\n");
            sJScript.Append("       {\n");
            sJScript.Append("       	sTotal = document.getElementById(" + sPrefix + "AmountList[x]).innerHTML.replace('$','');\n");
            sJScript.Append("       	sTotal = ValidateFieldValue(sTotal);\n");
            sJScript.Append("       	cptList += parseFloat(sTotal.replace(/,/g,''));	\n");
            sJScript.Append("       }\n");
            sJScript.Append("       document.getElementById(" + sPrefix + "lblAmountTotalID).innerHTML = formatCurrency(cptList);\n");
            sJScript.Append("   }\n");

            sJScript.Append("   function " + sPrefix + "RefreshSectionTotalQuantity()\n");
            sJScript.Append("   {\n");
            sJScript.Append("       var cptList = 0;		\n");
            sJScript.Append("       for(var x = 0; x < " + sPrefix + "QtyList.length; x++)\n");

            sJScript.Append("       {\n");
            sJScript.Append("       	if(!isNaN(parseInt(document.getElementById(" + sPrefix + "QtyList[x]).innerHTML)))\n");
            sJScript.Append("           {\n");
            sJScript.Append("    	        cptList += parseInt(document.getElementById(" + sPrefix + "QtyList[x]).innerHTML);\n");
            //sJScript.Append("               alert('cptList = ' + cptList);   \n"); 
            sJScript.Append("           }\n");
            sJScript.Append("       }   \n");
            sJScript.Append("       document.getElementById(" + sPrefix + "lblQTYTotalID).innerHTML = cptList;\n");
            sJScript.Append("   }\n");

            sJScript.Append("   function " + sPrefix + "RefreshSectionTotalQuantity2()\n");
            sJScript.Append("   {\n");
            sJScript.Append("       var cptList = 0;		\n");
            sJScript.Append("       for(var x = 0; x < " + sPrefix + "MinQtyList.length; x++)\n");

            sJScript.Append("       {\n");
            sJScript.Append("       	if(!isNaN(parseInt(document.getElementById(" + sPrefix + "MinQtyList[x]).value)))\n");
            sJScript.Append("           {\n");
            //sJScript.Append("               alert('Get = " + sPrefix + "MinQtyList[x]');   \n"); 
            sJScript.Append("    	        cptList += parseInt(document.getElementById(" + sPrefix + "MinQtyList[x]).value);\n");
            //sJScript.Append("               alert('cptList = ' + cptList);   \n"); 
            sJScript.Append("           }\n");
            sJScript.Append("       }   \n");
            //sJScript.Append("       alert('Put = " + sPrefix + "hdnMinTotalQuantity');   \n");
            sJScript.Append("       document.getElementById(" + sPrefix + "hdnMinTotalQuantity).value = cptList;\n");
            sJScript.Append("   }\n");

            sJScript.Append("</SCRIPT>");

            // Register Client Script
            this.Page.RegisterStartupScript(sPrefix + "SetSectionCalculationGrid", sJScript.ToString());
        }
    }
}