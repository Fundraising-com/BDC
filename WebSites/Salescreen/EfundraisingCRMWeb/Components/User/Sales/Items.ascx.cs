using log4net;

namespace EFundraisingCRMWeb.Components.User.Sales
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using System.ComponentModel;
	using System.Web.SessionState;
	using System.Web.UI;
    using efundraising.EFundraisingCRM;
    using efundraising.EnterpriseComponents;
    using MyExtensionMethods;
    using System.Linq;
    using System.Xml.Linq;
    using System.Collections.Generic;
    using System.Configuration;
    

	/// <summary>
	///		Summary description for .
	/// </summary>
    /// 

    //PROFIT % line 453
    //shipping  line 985
	public partial class Items : System.Web.UI.UserControl
	{
        public ILog Logger { get; set; }

        public Items()
	    {
            Logger = LogManager.GetLogger(GetType());
	    }
		public delegate void SetTextOnCard(int a ,string b);
		protected System.Web.UI.WebControls.DataGrid Datagrid;
		private int rowNumberItemAdded = -1;
		protected System.Web.UI.WebControls.ImageButton textOnCardImage;
		protected System.Web.UI.HtmlControls.HtmlImage textOnScratchcard;

		#region Private Properties
		private DropDownListProfitStatus IsRecalculateEvent = DropDownListProfitStatus.Default;

		private decimal totalAmount;
		private decimal shippingFees;
		private decimal totalProfit;
		private decimal gst;
		private decimal pst;
        private decimal hst;
		protected System.Web.UI.WebControls.Button RecalculateButton;
        //protected Components.User.Package.ProductLookUp ProductLookUp1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl productsearchframe;
		protected System.Web.UI.HtmlControls.HtmlImage calendarImage;
		protected System.Web.UI.HtmlControls.HtmlImage Img1;
		protected Components.User.Sales.TextOnCard TextOnCard1;
        private decimal totalSurcharge;
        private decimal invoiceAmount;
		private bool error = false;
        
        public int clientId
        {
            get
            {
                try
                {
                    if (ViewState[Global.SessionVariables.CLIENT_ID] == null)
                        return int.MinValue;
                    return System.Convert.ToInt32(ViewState[Global.SessionVariables.CLIENT_ID]);
                }
                catch (Exception)
                {
                    return int.MinValue;
                }
            }
            set
            {
                ViewState[Global.SessionVariables.CLIENT_ID] = value;
            }

        }

        public string clientSeq
        {
            get
            {
                return ViewState[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();
            }
            set
            {
                ViewState[Global.SessionVariables.CLIENT_SEQUENCE_CODE] = value;
            }
        }

		public void SetError(string msg)
		{
			errorLabel.Visible = true;
			errorLabel.Text = msg;
		}

		public decimal TotalAmount
		{
			get{return Convert.ToDecimal(TotalTextBox.Text.Remove(0,1));}
			set{TotalTextBox.Text = value.ToString("C");}
		}

		public decimal ShippingFees
		{
			get{return Convert.ToDecimal(ShippingFeeTextBox.Text.Remove(0,1));}
			set{ShippingFeeTextBox.Text = value.ToString("C");}
		}

		public decimal TotalProfit
		{
			get{return Convert.ToDecimal(TotalProfitTextBox.Text.Remove(0,1));}
			set{TotalProfitTextBox.Text = value.ToString("C");}
		}

        public decimal TotalSurcharge
        {
            get { return Convert.ToDecimal(SurchargeTextBox.Text.Remove(0, 1)); }
            set { SurchargeTextBox.Text = value.ToString("C"); }
        }

		public decimal GST
		{
			get{return Convert.ToDecimal(GSTTextBox.Text.Remove(0,1));}
			set{GSTTextBox.Text = value.ToString("C");}
		}

		public decimal PST
		{
			get{return Convert.ToDecimal(PSTtextBox.Text.Remove(0,1));}
			set{PSTtextBox.Text = value.ToString("C");}
		}

        public decimal HST
        {
            get { return Convert.ToDecimal(HSTtextBox.Text.Remove(0, 1)); }
            set { HSTtextBox.Text = value.ToString("C"); }
        }


        public decimal Discount
        {
            get
            {
                try
                {
                    if (DiscountTextBox.Text == "")
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToDecimal(DiscountTextBox.Text.Replace("$", "").Replace(",", ""));
                    }
                }
                catch (Exception e)
                {
                    return -1;
                }
            
            }
            set { DiscountTextBox.Text = value.ToString("C"); }
        }

        public decimal Surcharge
        {
            get
            {
                try
                {
                    if (SurchargeTextBox.Text == "")
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToDecimal(SurchargeTextBox.Text.Replace("$", "").Replace(",", ""));
                    }
                }

                catch (Exception e)
                {
                    return -1;
                }
                 }
            set { SurchargeTextBox.Text = value.ToString("C"); }
        }

        public int SurchargeReasonId
        {
            get
            {
                if (HiddenSurchargeIDLabel.Text == "")
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(HiddenSurchargeIDLabel.Text);
                }
            }
            set { HiddenSurchargeIDLabel.Text =value.ToString(); }
        }

        public int DiscountReasonId
        {
            get 
            {
                if (HiddenDiscountIDLabel.Text == "")
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(HiddenDiscountIDLabel.Text);
                }
            
            }
            set { HiddenDiscountIDLabel.Text = value.ToString(); }
        }



		public decimal InvoiceAmount
		{
			get{return Convert.ToDecimal(InvoiceAmountTextBox.Text.Remove(0,1));}
			set{InvoiceAmountTextBox.Text = value.ToString("C");}
		}


		public int RowNumberItemAdded
		{
			get
			{
				return rowNumberItemAdded;
			}
			set
			{
				rowNumberItemAdded = value;
			}
		}

		private DataTable dgDataTable 
		{
			get
			{
				return (DataTable)ViewState["dgDataTable"];
			}

			set
			{
				ViewState["dgDataTable"] = value;
			}
		}

		private int saleId
		{
			get
			{
				try
				{
					if (Request["saleId"] == null)
						return int.MinValue;
					return System.Convert.ToInt32(Request["saleId"]);
				}
				catch (Exception)
				{
					return int.MinValue;
				}
			}
		}


        public TextBox PromotionCodeBox
        {
            get { return PromoCodeTextBox; }
            set { PromoCodeTextBox = value; }
        }


        public TextBox DiscountBox
        {
            get { return DiscountTextBox; }
            set { DiscountTextBox = value; }
        }

        public TextBox SurchargeBox
        {
            get { return SurchargeTextBox; }
            set { SurchargeTextBox = value; }
        }

        public TextBox AdjTotalBox
        {
            get { return AdjusmentTotalTextBox; }
            set { AdjusmentTotalTextBox = value; }
        }

		private string prdLookUpColumn = "ProductLookUp";

		#endregion

	

		protected void Page_Load(object sender, System.EventArgs e)
		{


            if (!IsPostBack)
            {
                

                string conn = "";
                efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext db = new efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext();
              if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
              {
                  conn = ConfigurationManager.ConnectionStrings["EFRProdConnectionString"].ConnectionString;
                  db = new efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext(conn);
              }
              else
              {
                  conn = ConfigurationManager.ConnectionStrings["EFRProdConnectionStringDEV"].ConnectionString;
                  /*OE*/
                  db = new efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext(conn);
              }

               // get discount reasons
                var discounts = from d in db.charges
                                //where  d.display_efr == true && d.is_disabled == false
                                where d.is_disabled == false
                                orderby d.name ascending
                                select d;

                ListItem li = new ListItem();
                foreach (var discount in discounts)
                {
                    string name = discount.name;
                    string value = discount.charge_id.ToString();

                    li = new ListItem(name, value);
                    if (discount.is_credit == true)
                    {
                        DiscountReasonRadioButtonList.Items.Add(li);
                    }
                    else
                    {
                       SurchargeReasonRadioButtonList.Items.Add(li);
                    }
                }
                
                //li = new ListItem("Cancel", "");
                //DiscountReasonRadioButtonList.Items.Add(li);
               // SurchargeReasonRadioButtonList.Items.Add(li);

                //set dropdown selected value                    
                if (HiddenDiscountIDLabel.Text != "" && HiddenDiscountIDLabel.Text != "0")
                {
                    DiscountReasonRadioButtonList.SelectedValue = HiddenDiscountIDLabel.Text;
                }
                if (HiddenSurchargeIDLabel.Text != "" && HiddenSurchargeIDLabel.Text != "0")
                {
                    SurchargeReasonRadioButtonList.SelectedValue = HiddenSurchargeIDLabel.Text;
                }

                
            
            }

            //DiscountReasonRadioButtonList.Items.Add;
			if (!(error))
			{
				errorLabel.Visible = false;
			}
					

		}


		#region Public Methods

		public void SetData(SalesItemCollection sale, params DropDownListProfitStatus[] noResetProfitDDList)
		{
			if (noResetProfitDDList != null && noResetProfitDDList.Length > 0)
				IsRecalculateEvent = noResetProfitDDList[0];
			try
			{
				dgDataTable =  Components.Server.DataGrid.SaleItems.SaleItemsDataGrid.CreateDataTableSaleItems(sale);
				ItemsDatagrid.DataSource = dgDataTable;
				ItemsDatagrid.DataBind();
			
			}
			finally
			{
				IsRecalculateEvent = DropDownListProfitStatus.Default;
			}
		}
		
		public void SetData(DataTable dt, params DropDownListProfitStatus[] noResetProfitDDList)
		{
			if (noResetProfitDDList != null && noResetProfitDDList.Length > 0)
				IsRecalculateEvent = noResetProfitDDList[0];
			try
			{
				dgDataTable = dt.Copy();
				ItemsDatagrid.DataSource = dgDataTable;
				ItemsDatagrid.DataBind();
			}
			finally
			{
				IsRecalculateEvent = DropDownListProfitStatus.Default;
			}
		}

		public void SetOneEmptyRow()

		{
			dgDataTable =  Components.Server.DataGrid.SaleItems.SaleItemsDataGrid.CreateEmptyDataTableSaleItems();
			dgDataTable = Components.Server.DataGrid.SaleItems.SaleItemsDataGrid.AddNewRow(dgDataTable);
			ItemsDatagrid.DataSource = dgDataTable;
			ItemsDatagrid.DataBind();
		}


		public DataTable GetDataFromDatagrid()
		{
			decimal d = 0;
			return GetDataFromDatagrid(-1,ref d);
		}
		public DataTable GetDataFromDatagrid(int itemIndexToDelete)
		{
			decimal d = 0;
			return GetDataFromDatagrid(itemIndexToDelete,ref d);
		}
		public DataTable GetDataFromDatagrid(int itemIndexToDelete, ref decimal totalProfit)
		{
			DataTable dt =  Components.Server.DataGrid.SaleItems.SaleItemsDataGrid.CreateEmptyDataTableSaleItems();
			
			try{
			string[] colNames = Components.Server.DataGrid.SaleItems.SaleItemsDataGrid.GetColumnsName(dt);
			for (int i=0; i < this.ItemsDatagrid.Items.Count; i++)
			{
                bool emptyRow = true;
				if (i != itemIndexToDelete){
					if (this.ItemsDatagrid.Items[i].ItemType == ListItemType.Item || 
						this.ItemsDatagrid.Items[i].ItemType == ListItemType.AlternatingItem  )
					{
						DataRow dr = dt.NewRow();
						for (int j =0; j <colNames.Length; j++)
						{
							System.Web.UI.Control ctrl = this.ItemsDatagrid.Items[i].FindControl(colNames[j]);
							
							System.Web.UI.WebControls.TextBox tb = 	ctrl as System.Web.UI.WebControls.TextBox;
							if (tb != null)
							{
                                Components.User.Package.ProductLookUP2 p = (Components.User.Package.ProductLookUP2) this.ItemsDatagrid.Items[i].FindControl("ProductLookUp1");
   	
								//if (p.productDescription != "")
								//{
                                if (p.productIdInHidden != "")
								{
                                    emptyRow = false;
                                    if (p.productDescription.Length < 2)
                                    {
                                        p.productDescription = "1";
                                    }
                                }

                                if (j == 0 && p.productDescription.Length < 2)
                                {
                                    dr[colNames[j]] = "1";
                                }
                                else
                                {
                                    dr[colNames[j]] = tb.Text;
                                }
								

								//check that the qty is numeric
								if (colNames[j] == "Quantity")
								{
									string qty = tb.Text;
									if (!(Helper.IsNumeric(qty)))
									{
                                       dr[colNames[j]] = 1;
									}
								}

								//make a sum of total profit
								if (colNames[j] == "TotalProfit")
								{
									if (tb.Text.Length > 1)
									{

                                        if (tb.Text == "($20.00)" && colNames[j] == "TotalProfit")
                                        {
                                            totalProfit = -20;
                                        }
                                        else
                                        { 
                                            totalProfit = totalProfit + Convert.ToDecimal(tb.Text.Remove(0, 1));
                                        }
									}
								}
							}
							else
							{
								System.Web.UI.WebControls.DropDownList ddListProfit = ctrl as System.Web.UI.WebControls.DropDownList;
								if (ddListProfit != null && ddListProfit.Items.Count > 0)
								{
                                    dr["Profit"] = Convert.ToDecimal(ddListProfit.SelectedValue);
                                    //dr["Profit"] = Convert.ToDecimal(ddListProfit.SelectedValue).ToString("C").Substring(1);
								}
							}
						}
					
						if (!(emptyRow))
						{
                                                                               
                            Components.User.Package.ProductLookUP2 prLookUp = (Components.User.Package.ProductLookUP2)this.ItemsDatagrid.Items[i].FindControl("ProductLookUp1");
							dr["Product"] = prLookUp.productDescription;
							dr["SalesItemId"] = this.ItemsDatagrid.DataKeys[i].ToString();
							dt.Rows.Add(dr);
						}
					}
				}
			}
			}
			catch(Exception ex)
			{
				Logger.Error("Error in GetDataFromDatagrid", ex);
			}
		
			
			return dt;
		}


		public void RebindDatagrid(DataTable dt)
		{
			ItemsDatagrid.DataSource = dt;
			ItemsDatagrid.DataBind();
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.ItemsDatagrid.ItemDataBound +=new DataGridItemEventHandler(Datagrid_ItemDataBound);
            this.TextOnCard1.TextOnCardClicked += new TextOnCard.TextOnCardClickedEventHandler(this.TextOnCardButton_Click);

			//this.DeleteImage.Click += new System.Web.UI.ImageClickEventHandler(DeleteButton_Click);
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

		#region Private Methods
		protected void AddItemButton_Click(object sender, System.EventArgs e)
		{
			AddItem();
		}

		private void TextOnCardButton_Click(object sender, TextOnCardEventArgs e)
		{

			//this function updates the textbox if the datagrid with the new text

		    int id = e.ScratchBookID;
			string textOnCard = e.Text;

			//wnat to edit texton card (groupName)
			for (int i=0; i < this.ItemsDatagrid.Items.Count; i++)
			{							
				//get sbID
				int scratchBookId = Convert.ToInt32(dgDataTable.Rows[i]["ScratchBookID"]);
				if (scratchBookId == id)
				{
					//edit the text
					TextBox groupName = (TextBox) ItemsDatagrid.Items[i].FindControl("GroupName");
					groupName.Text = textOnCard;
				}
			}
			TextOnCard1.Visible = false;


		}

		private void AddItem()
		{
			dgDataTable = GetDataFromDatagrid();//Session["SaleDataTable"];
			ItemsDatagrid.DataSource = Components.Server.DataGrid.SaleItems.SaleItemsDataGrid.AddNewRow(dgDataTable);
			ItemsDatagrid.DataBind();
		}

		private void Datagrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
           {
                ScratchBook sc = null;
                bool disableDropDown = false;
                bool disableForFC = false;
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Components.User.Package.ProductLookUP2 prLookUp = (Components.User.Package.ProductLookUP2)e.Item.FindControl("ProductLookUp1");
                    //Components.User.Package.ProductLookUP2 prLookUp = e.Item.FindControl(prdLookUpColumn) as Components.User.Package.ProductLookUP2;
                    if (prLookUp != null)
                    {
                        prLookUp.productDescription = dgDataTable.Rows[e.Item.ItemIndex]["Product"].ToString();
                        prLookUp.productIdInHidden = dgDataTable.Rows[e.Item.ItemIndex]["ScratchBookID"].ToString();
                        prLookUp.saleIdInHidden = this.saleId.ToString();
                        prLookUp.rowNumber = e.Item.ItemIndex.ToString();
                    }


                    System.Web.UI.WebControls.DropDownList ddList = null;


                    string itemNo = dgDataTable.Rows[e.Item.ItemIndex]["SalesItemID"].ToString();
                    ImageButton img = (ImageButton)e.Item.FindControl("DeleteImage");
                    ImageButton imgEdit = (ImageButton)e.Item.FindControl("TextOnCardImage");
                    if (itemNo == "")
                    {
                        img.Visible = true;
                    }
                    else
                    {
                        //check if sale if confirmed, if not, can still delete
                        //EFundraisingCRM.SalesItem si = EFundraisingCRM.SalesItem.GetSalesItemsBySaleID(Convert.ToInt32(prLookUp.saleIdInHidden));


                        img.Visible = false;
                    }

                    int scratchBookId = -1;
                    try
                    {
                        scratchBookId = Convert.ToInt32(dgDataTable.Rows[e.Item.ItemIndex]["ScratchBookID"]);

                        //check if is a scratchard
                        ScratchBook sb = ScratchBook.GetScratchBookByID(scratchBookId);
                        ProductClass pc = ProductClass.GetProductClassById(sb.ProductClassId);
                        if (pc.Description == "Scratchcard" || pc.Description == "Scratchcard - Free T-shirt")
                        {
                            imgEdit.Visible = true;
                        }
                        else
                        {
                            imgEdit.Visible = false;
                        }
                        //disable frozen food for FCs
                        //1/15/2010 -- no product class gives the rights to FC to choose a profit %
                         bool isConsultant = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsConsultant();
                        //if ((sb.ProductClassId == 29 || sb.ProductClassId == 37) && isConsultant)
                        if (isConsultant)
                        {
                            disableForFC = true;
                        }

                    }
                    catch (Exception)
                    {
                    }

                    if (scratchBookId > -1)
                    {
                        System.Web.UI.Control ctrl = e.Item.FindControl("Profit");
                        ddList = ctrl as System.Web.UI.WebControls.DropDownList;
                        if (ddList != null)
                        {

                    
                            sc = ScratchBook.GetScratchBookByID(scratchBookId);
                            if (sc != null)
                            {
                                //1-15-2010 change where profit range is taken
                                decimal defaultProfit = 0;
                                //DataTable dt = Global.GetProfitValues(sc.PackageId, ref defaultProfit);
                                ProductBusinessRules rules = ProductBusinessRules.GetProductBusinessRulesByProductID(scratchBookId);
                                if (rules != null)
                                {
                                    List<ProfitRange> ranges =  ProfitRange.GetProfitRangeByProductBusinessRuleID(rules.ProductBusinessRuleID);

                                    var orderedRange = from r in ranges orderby r.ProfitPercentage select r;
                                    //var orderedRange = ranges.OfType<decimal>().OrderBy(id => id);

                                    foreach (ProfitRange range in orderedRange)
                                    {
                                        decimal p = Convert.ToDecimal(range.ProfitPercentage);

                                        //ddList.Items.Add(new ListItem(Convert.ToInt32(p * 100) + "%", p.ToString("C").Substring(1)));
                                        ddList.Items.Add(new ListItem(p + "%", p.ToString("G")));
                                       // ddList.Items.Add(new ListItem(p + "%", p.ToString("C").Substring(1));
                                        
                                      
                                    }
                                }

                               /*for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    decimal p = Convert.ToDecimal(dt.Rows[i]["profit"]);
                                    ddList.Items.Add(new ListItem(Convert.ToInt32(p * 100) + "%", p.ToString("C").Substring(1)));
                                }*/

                                //IF Product has no Package
                                //if (dt.Rows.Count < 1 || disableDropDown)
                                if (ddList.Items.Count < 1)
                                {
                                    ddList.Enabled = false;
                                }else
                                {
                                    if (disableForFC)
                                    {
                                        ddList.Enabled = false;
                                    }
                                    //assign the value
                                    string margin = dgDataTable.Rows[e.Item.ItemIndex]["Profit"].ToString();
                                    if (margin == "")
                                    {
                                        //ddList.SelectedValue = defaultProfit.ToString("C").Remove(0, 1);
                                        ddList.SelectedIndex = 0;
                                    }
                                    else
                                    {

                                       
                                        
                                             var temp = dgDataTable.Rows[e.Item.ItemIndex]["Profit"].ToString();
                                         decimal d = Convert.ToDecimal(temp);
                                       // decimal d = Convert.ToDecimal(dgDataTable.Rows[e.Item.ItemIndex]["Profit"]);
                                        if (d < 1)
                                        {
                                            //margin = (d * 100).ToString("C").Remove(0, 1);
                                            margin = (d * 100).ToString("G");
                                        }
                                        else
                                        {
                                            
                                            margin = d.ToString("G");
                                        }
                                        
                                        try
                                        {
                                            if (d < 1)
                                            {
                                                if (d == 0)
                                                {
                                                    ddList.SelectedValue = d.ToString("G");
                                                }
                                                else
                                                {
                                                    ddList.SelectedValue = d.ToString("G").Remove(0, 2);
                                                }
                                            }
                                            else
                                            {
                                                ddList.SelectedValue = d.ToString();
                                            }


                                        }
                                        catch (Exception x)
                                        {
                                            errorLabel.Text = "Prices for " + sc.Description + " are incorrect.";
                                            errorLabel.Visible = true;
                                            error = true;
                                        }
                                    }

                                }



                            }
                        }
                    }


                    System.Web.UI.WebControls.TextBox tbQuantity = e.Item.FindControl("Quantity") as System.Web.UI.WebControls.TextBox;
                    System.Web.UI.WebControls.TextBox tbQuantityFree = e.Item.FindControl("QuantityFree") as System.Web.UI.WebControls.TextBox;
                    if (disableForFC){
                        tbQuantityFree.ReadOnly = true;
                    }

                    

                    System.Web.UI.WebControls.TextBox tbItemPrice = e.Item.FindControl("Price") as System.Web.UI.WebControls.TextBox;
                    System.Web.UI.WebControls.TextBox tbTotalAmount = e.Item.FindControl("TotalAmount") as System.Web.UI.WebControls.TextBox;
                    System.Web.UI.WebControls.TextBox tbTotalProfit = e.Item.FindControl("TotalProfit") as System.Web.UI.WebControls.TextBox;
                    if (tbQuantity != null && tbQuantity.Text.Trim() != string.Empty
                        && tbTotalAmount != null && tbItemPrice != null && tbTotalProfit != null)
                    {
                        //tbTotalAmount.Text = string.Empty;
                        //tbTotalProfit.Text = string.Empty;
                        try
                        {
                            int qtty = Convert.ToInt32(tbQuantity.Text);

                        }
                        catch (Exception)
                        {
                        }
                    }

                }
            }
        }

		protected void DeleteButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				//Display Details for lead ID clicked
				ImageButton img = (ImageButton) sender;
				TableCell cell = (TableCell) img.Parent;
				DataGridItem item = (DataGridItem) cell.Parent;
				
				Delete(item.ItemIndex);
				CalculateTotals(false);
		    			
			}	
			catch(Exception ex)
			{
				//Logger.Error("Error in LeadNameLink_Click", ex);
			}
			
		}

		protected void TextOnCardButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				//Display Details for lead ID clicked
				ImageButton img = (ImageButton) sender;
				TableCell cell = (TableCell) img.Parent;
				DataGridItem item = (DataGridItem) cell.Parent;
				
				TextBox txt = (TextBox) ItemsDatagrid.Items[item.ItemIndex].FindControl("ScratchbookID");
				TextBox textOnCard = (TextBox) ItemsDatagrid.Items[item.ItemIndex].FindControl("GroupName");
			
				
				//Session[Global.SessionVariables.ItemsDataGrid] = GetDataFromDatagrid();
				TextOnCard1.ScratchbookID = Convert.ToInt32(txt.Text);
				TextOnCard1.Refresh(textOnCard.Text);
				if (TextOnCard1.Visible)
				{
					TextOnCard1.Visible = false;
				}
				else
				{
					TextOnCard1.Visible = true;
				}
				
			}	
			catch(Exception ex)
			{
				//Logger.Error("Error in LeadNameLink_Click", ex);
			}
			
		}

		public void Refresh()
		{
			IsRecalculateEvent = DropDownListProfitStatus.Recalculate;
			try
			{
				dgDataTable = GetDataFromDatagrid();//Session["SaleDataTable"];
				ItemsDatagrid.DataSource = dgDataTable;
				ItemsDatagrid.DataBind();
				if (dgDataTable.Rows.Count == 0)
				{
					SetOneEmptyRow();
				}
				
			}
			finally
			{
				IsRecalculateEvent = DropDownListProfitStatus.Default;
			}
		}

		public void Delete(int itemIndexToDelete)
		{
			try
			{
				dgDataTable = GetDataFromDatagrid(itemIndexToDelete);//Session["SaleDataTable"];
				ItemsDatagrid.DataSource = dgDataTable;
				ItemsDatagrid.DataBind();
			}
			finally{}
		}

		private void RecalculateButton_Click(object sender, System.EventArgs e)
		{
		    Refresh();
		}

		#endregion




		public bool CalculateTotals(bool validate)
		{
			int nbProductClass = 0;
			DataTable productList = new DataTable();
            DataColumn col = new DataColumn("ScratchbookID");
            productList.Columns.Add(col);
            col = new DataColumn("NbItems");
            productList.Columns.Add(col);

			return CalculateTotals(validate, ref nbProductClass, ref productList);
		}



		//get tax, shipping, qty free from the db
		public void GetTotals(int saleID)
		{

			try
			{
				//TO DO get qty free for each items
				decimal totalProfit = 0;
				DataTable dgDataTable = GetDataFromDatagrid(-1, ref totalProfit);

				TotalProfitTextBox.Text = totalProfit.ToString("C");
               
                
                

                Sale s = Sale.GetSaleByID(saleID);
				ApplicableTax[] taxes = ApplicableTax.GetApplicableTaxByID(saleID);




                decimal adjustment = 0;
                decimal gstOnAdj = 0;
                decimal pstOnAdj = 0;
                decimal hstOnAdj = 0;

                DiscountTextBox.Text = "$0";
                SurchargeTextBox.Text = "$0";
                PromoCodeTextBox.Text = "N/A";


                //Get Promotion code is available
                if (s.PromotionCodeID > 0)
                {
                    PromotionCode promo = PromotionCode.GetPromotionCodeByID(s.PromotionCodeID);
                    PromoCodeTextBox.Text = promo.PromotionExternalCode.ToString();
                    PromoCodeTextBox.ToolTip = promo.PromotionCodeDesc.ToString();
                }

                
                
                
                
                //only want the discount/surcharge
                efundraising.EFundraisingCRM.Adjustment[] adjs = efundraising.EFundraisingCRM.Adjustment.GetLatestAdjustmentsBySaleID(s.SalesId);
                foreach (efundraising.EFundraisingCRM.Adjustment a in adjs)
                {
                    if (a.AdjustmentNo == 1)
                    {
                       DiscountTextBox.Text = Convert.ToString(a.AdjustmentAmount);
                       Discount = Convert.ToDecimal(a.AdjustmentAmount);
                       DiscountReasonId = a.ReasonID;
                    }
                    if (a.AdjustmentNo == 2)
                    {
                        SurchargeTextBox.Text = Convert.ToString(a.AdjustmentAmount);
                        Surcharge = Convert.ToDecimal(a.AdjustmentAmount);
                        SurchargeReasonId = a.ReasonID;
                    }


                }

                AdjusmentTotalTextBox.Text = (Surcharge - Discount).ToString("C");
                adjustment = Surcharge - Discount;
                
                
                if (!EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsSaleTaxExempt(saleID))
                {
                    if (adjustment != 0)
                    {
                        EFundraisingCRMWeb.Components.Server.ManageSaleScreen.CalculateTaxes(adjustment, s.ClientId, s.ClientSequenceCode, ref hstOnAdj, ref pstOnAdj, ref gstOnAdj);
                    }
                }

               
				GSTTextBox.Text = "$0";
				PSTtextBox.Text = "$0";
                HSTtextBox.Text = "$0";
				ShippingFeeTextBox.Text = "$0";
       

				decimal pst = 0;
				decimal gst = 0;
                decimal hst = 0;
				decimal shipping = 0;
        
				foreach(ApplicableTax tax in taxes)
				{
					if (tax.TaxCode == "GST")
					{
                        gst = tax.TaxAmount; //- gstOnAdj;
						GSTTextBox.Text = gst.ToString("C");
						
					}
                    else if (tax.TaxCode == "PST")
                    {
                        pst = tax.TaxAmount; //- pstOnAdj;
                        PSTtextBox.Text = pst.ToString("C");

                    }
                    else
                    {
                        hst = tax.TaxAmount;
                        HSTtextBox.Text = hst.ToString("C");
                    }
				}

                //fix adjustment so it will calculate invoice total correctly
                adjustment = (adjustment * -1);
                InvoiceAmountTextBox.Text = s.TotalAmount.ToString("C");
                ShippingFeeTextBox.Text = s.ShippingFees.ToString("C");
               
				if (s.ShippingFees != decimal.MinValue)
				{
					shipping = s.ShippingFees;
				}

                if (s.ClientSequenceCode.ToUpper() == "OF")
                 {
                     Refresh();
                 }
                
                TotalTextBox.Text = (Convert.ToDecimal(s.TotalAmount) + adjustment - pst - gst - hst - shipping).ToString("C");
               
            
            }
			catch (Exception ex)
			{
				Logger.Error("Sales Screen: Items - GetTotals", ex);
			}
		
		}


		//the validate permits to not validate when the user just want to calculate amounts
		public bool CalculateTotals(bool validate, ref int nbProductClass, ref DataTable productList)
		{

            
            int line = 0;
			////contains product class and total amount ans shipping
			DataTable classTotals = new DataTable();

			DataColumn col = new DataColumn("ProductClassID");
			classTotals.Columns.Add(col);
          	col = new DataColumn("Quantity"); //used to calculate min order
			classTotals.Columns.Add(col);
			col = new DataColumn("TotalAmount");
			classTotals.Columns.Add(col);
			col = new DataColumn("TotalShipping");
			classTotals.Columns.Add(col);
			col = new DataColumn("TotalGST");
			classTotals.Columns.Add(col);
			col = new DataColumn("TotalPST");
			classTotals.Columns.Add(col);
            col = new DataColumn("TotalHST");
            classTotals.Columns.Add(col);
            

            DataTable packageTotals = new DataTable();
            col = new DataColumn("PackageID");
            packageTotals.Columns.Add(col);
            col = new DataColumn("Quantity"); //used to calculate min order
            packageTotals.Columns.Add(col);
          
			Refresh();
			int i = -1;  
			bool error = false;
			decimal totalProfit = 0;
			decimal totalAmount = 0;
            int qtyForSurcharge = 0;
            int additionalPrint = 0;

            try
			{
				//iterate through each item
				DataTable dgDataTable = GetDataFromDatagrid();
				//System.Collections.ArrayList productList = new System.Collections.ArrayList();
				if (dgDataTable.Rows.Count > 0)
				{
				
					foreach (DataRow dr in dgDataTable.Rows)
					{
						i++;
                
						int currentQty = 0;
						decimal currentAmount = 0;
						decimal profitPercentage = 0;
                        if (dr["Profit"] != DBNull.Value)
                        {
                            profitPercentage = Convert.ToDecimal(dr["Profit"]);
                        }

                        line = 1;
						string product = dr["ProductCode"].ToString();
						string t = dr["ScratchBookID"].ToString();
						int scratchBookId = Convert.ToInt32(dr["ScratchBookID"]);
						int qty = Convert.ToInt32(dr["Quantity"]);
                        qtyForSurcharge = qtyForSurcharge + qty;
				
						// Obtain references to row's controls
						TextBox qtyFreeTextBox = (TextBox) ItemsDatagrid.Items[i].FindControl("QuantityFree");
						TextBox itemPriceTextBox = (TextBox) ItemsDatagrid.Items[i].FindControl("Price");
						TextBox totalAmountTextBox = (TextBox) ItemsDatagrid.Items[i].FindControl("TotalAmount");
						TextBox totalProfitTextBox = (TextBox) ItemsDatagrid.Items[i].FindControl("TotalProfit");
                        DropDownList profit = (DropDownList)ItemsDatagrid.Items[i].FindControl("Profit");
						
                        ScratchBook sc = ScratchBook.GetScratchBookByID(scratchBookId);

                        int qtyFree = 0;
                        try
                        {
                            qtyFree = Convert.ToInt32(qtyFreeTextBox.Text);
                        }
                        catch (Exception x){}


						//get the qtyFree from database when the read only)
                        if (qtyFree == 0){
                            qtyFree = Convert.ToInt32(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetQtyFree(scratchBookId, qty));
                            qtyFreeTextBox.Text = qtyFree.ToString();
                        }
						
						

                        line = 2;
						decimal dTotal;
                        
						if (profitPercentage > 0)
						{
                            line = 3;
							//total que le client vend (profit potential * profit %)
							// moins son cout (affecte par le profit %)

							//eg. chocolat lui coute 30$, le vend pour 50$, total prodit 20$  (le profit% est de 50% et le raising potential est 100$)
							decimal raisingPotential; //= Convert.ToDecimal(sc.RaisingPotential);
							int totalQty = qtyFree + qty;

                            raisingPotential = Convert.ToDecimal(sc.RaisingPotential);
                           
                            

                            itemPriceTextBox.Text = (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetItemPrice(sc, profitPercentage / 100, totalQty)).ToString("C");
                         
                            itemPriceTextBox.ToolTip = "Raising Potential: " + raisingPotential.ToString("C");
											
							dTotal = Convert.ToDecimal(itemPriceTextBox.Text.Substring(1))*qty;
							totalAmountTextBox.Text = dTotal.ToString("C");
							totalAmountTextBox.ToolTip = "Total Raising Potential: " + (raisingPotential * totalQty).ToString("C");
						
							//decimal dTotalWithFreeItems = Convert.ToDecimal(itemPriceTextBox.Text.Substring(1))*(qty + qtyFree); 
							totalProfitTextBox.Text = ((raisingPotential * totalQty) - dTotal).ToString("C");
                            totalProfit += EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetSalesItemTotalProfit(dTotal, raisingPotential, totalQty);
                            line = 4;
						
						}
						else
							//goes here for lollipops, scratchards that have no profit %
						{
                            line = 5;
                            itemPriceTextBox.Text = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetItemPrice(sc, 0, qty).ToString("C");

                            //hack for 20 discount item.
                            if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetItemPrice(sc, 0, qty) < 0 && sc.ScratchBookId == 4242)
                            {
                                dTotal = -20;
                            }
                            else
                            {
                                dTotal = Convert.ToDecimal(itemPriceTextBox.Text.Substring(1)) * qty;
							
                            }

                            //dTotal = Convert.ToDecimal(itemPriceTextBox.Text.Substring(1))*qty;
							totalAmountTextBox.Text = dTotal.ToString("C");
							totalProfitTextBox.Text = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetSalesItemTotalProfit(sc, qty, qtyFree).ToString("C");
							totalProfit += EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetSalesItemTotalProfit(sc, qty, qtyFree);
						}


                        line = 6;
						totalAmount += dTotal;   //calculate the invoice amount regardless of the product class
                        line = 100;
						//store  in array for calculating average delivery date
                        if (productList == null)
                        {
                            line = 1005;
                        }
                        DataRow row = productList.NewRow();
                        line = 1001;
                        row["ScratchbookID"] = scratchBookId;
                        row["NbItems"] = qty;
                        productList.Rows.Add(row);
                        line = 101;
						//store totals for different product class
						ScratchBook sb = ScratchBook.GetScratchBookByID(scratchBookId);
						string pcID = (sb.ProductClassId).ToString();
                        string packageID = (sb.PackageId).ToString();

                        string sb_ID = (sb.ScratchBookId).ToString();
                        Session["sb_IDForSurchage"] = sb_ID;
                        Session["pcIDForSurchage"] = pcID;
                       

                        line = 102;
						//FIlter table 
						classTotals.DefaultView.RowFilter = "ProductClassID = " + pcID;

                        line = 103;
                        //UPDATE
                        //TO DO
                        //Treat Herhsey AND m&m has one product class
                        /////////////////



						if (classTotals.DefaultView.Count == 0)
						{
                            line = 7;
							//insert product class with current item amount
							row = classTotals.NewRow();
							row["ProductClassID"] = pcID;
						    row["Quantity"] = 0; //exludes the unique products with their own rules
							row["TotalAmount"] = dTotal;
							row["TotalShipping"] = 0;
							row["TotalGST"] = 0;
							row["TotalPST"] = 0;
                            row["TotalHST"] = 0;

							classTotals.Rows.Add(row);
							nbProductClass++;
                       
						}
						else
						{
                            line = 8;
							currentAmount = Convert.ToDecimal(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount",pcID));
							EFundraisingCRMWeb.Components.Server.ManageSaleScreen.SetValueToDataTable(classTotals, "ProductClassID", "TotalAmount",pcID, (currentAmount + dTotal).ToString());
				
						}

                        line = 9;
						//Update and validate QTY OF ClassTotals
						//check if product has its own rules  or is a master case (which has own value...from web config)
						ProductBusinessRules br = ProductBusinessRules.GetProductBusinessRulesByProductIdOnly(scratchBookId);
						string code = Components.Server.ManageSaleScreen.GetValueFromWebConfig("MasterCase","productCode");
						
                        ///
                        //UPDATE
                        //WITH NO CODES, Lollipo Master case has no longer  a specific code
                        //What should be done is put (MasterCase) inside description of product
                        ////



						//THIS SECTION FILLS PACKAGE TOTALS AND PRODUCT CLASS TOTAL
                        //WE MUST EXCLUDE FROM CLASS TOTAL ALL THE PRODUCTS WITH PACKAGE RULES
                        ///SAme for product
                        if (br == null)
						{
                            //PRODUCT CLASS SECTION
                            br = ProductBusinessRules.GetProductBusinessRulesByPackageIdOnly(Convert.ToInt32(packageID));
                            if (br == null)
                            {
                                line = 10;
                                currentQty = Convert.ToInt32(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "Quantity",  pcID));
                                //SETTING TOTAL QTY FOR PRODUCT CLASS
                                EFundraisingCRMWeb.Components.Server.ManageSaleScreen.SetValueToDataTable(classTotals, "ProductClassID", "Quantity", pcID, (currentQty + qty).ToString());
                                ////
                            }
                            else  //PACKAGE SECTION
                            {
                                line = 11;
                                //FIlter package table to add new package if necessary
                                packageTotals.DefaultView.RowFilter = "PackageID = " + packageID;
                                if (packageTotals.DefaultView.Count == 0)
                                {
                                    row = packageTotals.NewRow();
                                    row["PackageID"] = packageID;
                                    row["Quantity"] = 0; //exludes the unique products with their their own rules
                                    packageTotals.Rows.Add(row);
                                }

                                currentQty = Convert.ToInt32(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromDataTable(packageTotals, "PackageID", "Quantity", packageID));
                                //if null, insert package in table
                                EFundraisingCRMWeb.Components.Server.ManageSaleScreen.SetValueToDataTable(packageTotals, "PackageID", "Quantity", packageID, (currentQty + qty).ToString());

                                //added 6-11-2009 - Set the product class total for frozen food purposes
                                currentQty = Convert.ToInt32(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", pcID));
                           
                            }
						}
						else   //PRODUCT SECTION
						{
                            line = 12;
							//we validate the min qty for that product alonecause hes by itself in the business rules
							//otherwise, the min qty is calculated afterwards with the total for the whole product class
				
							//get the business rules for the master case
							if (br == null)
							{
                                
                                br = ProductBusinessRules.GetProductBusinessRulesByProductClass(sb.ProductClassId);
							}
					
							int minCases = Convert.ToInt32(Components.Server.ManageSaleScreen.GetValueFromWebConfig("MasterCase","minOrder"));
							if(validate)
							{

								if (qty < br.MinOrder && product.IndexOf(code,0) == -1)
								{
									error = true;
									SetError("The minimun quantity for product " + product + " is " + br.MinOrder.ToString());
								}
								else if (qty < minCases)
								{
									error = true;
									SetError("The minimun quantity for product " + product + " is " + minCases.ToString());
								}
							}
							
						}

					}//END FOR EACH item




                    line = 13;
					//determine if the min qty was reached for every package and product class except frozen food
					string message = "";
                    
                    error = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.ValidateMinQtyPackage(ref packageTotals, ref message);

                    if (!error)
                    {
                        error = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.ValidateMinQty(ref classTotals, ref message);
                        if (validate && error)
                        {
                            SetError(message);
                        }
                    }
                    else
                    {
                        SetError(message);
                    }
                    


                    line = 107;
					//calculate shipping fees for all prodcuts for all product class
					//Also sets the shipping in classTotal
                    bool pickUp = false; 
                    decimal totalShipping = 0;
                    decimal totalSurcharge = 0;
                   
                    efundraising.EFundraisingCRM.ClientAddress ca = efundraising.EFundraisingCRM.ClientAddress.GetClientAddressByIdSequenceAddressType(clientId, clientSeq, "ST");


                   

                    if (!(ShippingFeeTextBox.ReadOnly))//custom
                    {
                        string amt = ShippingFeeTextBox.Text.Replace("$", "");
                        amt = amt.Replace(",", ".");
                        try
                        {
                            line = 15;
                            decimal customShipping = Convert.ToDecimal(amt);
                            totalShipping = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetTotalShippingFees(productList, ref classTotals, customShipping);
                        }
                        catch (Exception x)
                        {
                            //amt not valid
                        }
                    }
                    else
                    {
                        if (ca != null)
                        {
                            if (!(ca.PickUp))
                            {
                                totalShipping = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetTotalShippingFees(productList, ref classTotals, -1);
                                
                            }
                        }
                    }


                    //if ()


                    line = 16;
					
                   

                    decimal adjustment = Surcharge - Discount;

                    AdjusmentTotalTextBox.Text = adjustment.ToString("C");
                    
                    //set taxes for each product class
                    decimal totalGST = 0;
                    decimal totalPST = 0;
                    decimal totalHST = 0;
                    decimal pstOnAdj = 0;
                    decimal gstOnAdj = 0;
                    decimal hstOnAdj = 0;
                   
					//EFundraisingCRMWeb.Components.Server.ManageSaleScreen.CalculateTaxesByProductClass(classTotals, clientID, clientSeqCode, ref totalGST, ref totalPST);
                    EFundraisingCRMWeb.Components.Server.ManageSaleScreen.CalculateTaxesByProductClass(classTotals, clientId, clientSeq, ref totalHST, ref totalGST, ref totalPST,adjustment,ref pstOnAdj, ref gstOnAdj, ref hstOnAdj );

					Session[Global.SessionVariables.CLASS_TOTALS] = classTotals;
                    Session[Global.SessionVariables.PACKAGE_TOTALS] = packageTotals;
                    				
					//update amounts
					ShippingFees = totalShipping;
					TotalProfit = totalProfit;
					TotalAmount = totalAmount;

                    #region added code for PVF prepack & Otis -- adding surcharge depending on qty
                    //added code for PVF prepack -- adding surcharge depending on qty
                    try
                    {
                        int pcIDTemp = Convert.ToInt32(Session["pcIDForSurchage"]);
                        string sb_IDtemp = Session["sb_IDForSurchage"].ToString();
                        int sb_IDpack = Convert.ToInt32(sb_IDtemp);


                        if (qtyForSurcharge < 250 && (sb_IDpack >= 3610 && sb_IDpack <= 3643))
                        {
                            TotalSurcharge = 150;
                            Surcharge = TotalSurcharge;
                            //Session["SurchargeSelection"] = 4;
                            Session["AddSurcharge"] = "active";
                            SurchargeReasonRadioButtonList.SelectedIndex = 2;
                        }
                        else if (qtyForSurcharge > 249 && (sb_IDpack >= 3610 && sb_IDpack <= 3643))
                        {
                            TotalSurcharge = (decimal)(0.70 * qtyForSurcharge);
                            Surcharge = TotalSurcharge;
                            //Session["SurchargeSelection"] = 4;
                            Session["AddSurcharge"] = "active";
                            SurchargeReasonRadioButtonList.SelectedIndex = 2;
                        }

                        if (qtyForSurcharge < 121 && pcIDTemp == 43)
                        {
                            TotalSurcharge = 150;
                            Session["AddSurcharge"] = "active";
                            SurchargeReasonRadioButtonList.SelectedIndex = 2;
                        }

                        else if (qtyForSurcharge > 240 && pcIDTemp == 43)
                        {
                            TotalSurcharge = 0;
                            Session["AddSurcharge"] = "active";
                            SurchargeReasonRadioButtonList.SelectedIndex = 2;
                        }
                        else if (pcIDTemp == 43)
                        {
                            TotalSurcharge = 50;
                            Session["AddSurcharge"] = "active";
                            SurchargeReasonRadioButtonList.SelectedIndex = 2;
                        }
                    }
                    catch (Exception ex)
                    { throw ex; }


                    #endregion



                    PST = totalPST + pstOnAdj;
                    GST = totalGST + gstOnAdj;
                    HST = totalHST + hstOnAdj;
                    Discount = Discount;
                    Surcharge = Surcharge;
                    InvoiceAmount = totalShipping + totalAmount + PST + GST + HST + Surcharge - Discount; 

					//scheduledDeliveryDays = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetMaxDeliveryDays(productList);
		        
				}
				else
				{
					TotalProfitTextBox.Text = "";
					TotalTextBox.Text = "";
					ShippingFeeTextBox.Text = "";
                    DiscountTextBox.Text = "";
                    SurchargeTextBox.Text = "";
                    GSTTextBox.Text = "";
					PSTtextBox.Text = "";
                    HSTtextBox.Text = "";
					InvoiceAmountTextBox.Text = "";
				}
											
			}
			catch(Exception ex)
			{
			     Logger.Error("Error in CalculateTotals. line: " + line.ToString(), ex);
			}
			
			return error;
			
		}

        public static string GetValueFromWebConfig(string key, string keyValue)
        {
            return efundraising.Configuration.ApplicationSettings.GetConfig()[key, keyValue].ToString();

        }

        public void DisableForConsultants()
        {
            ShippingFeeTextBox.ReadOnly = true;
            ShippingImageButton.Visible = false;
            DiscountReasonRadioButtonList.Enabled = false;
            SurchargeReasonRadioButtonList.Enabled = false;
            DiscountTextBox.ReadOnly = true;
            SurchargeTextBox.ReadOnly = true;
            AdjusmentTotalTextBox.ReadOnly = true;
        }


        public void EnableDiscounts()
        {
            DiscountReasonRadioButtonList.Enabled = true;
            SurchargeReasonRadioButtonList.Enabled = true;
            DiscountTextBox.ReadOnly = false;
            SurchargeTextBox.ReadOnly = false;
        }

		public void DisableEverything()
		{
			try
			{
				AddItemButton.Enabled = false;
				for (int i=0; i < this.ItemsDatagrid.Items.Count; i++)
				{
					Components.User.Package.ProductLookUP2 p = (Components.User.Package.ProductLookUP2) this.ItemsDatagrid.Items[i].FindControl("ProductLookUp1");
					p.DisableEverything();
	
					DropDownList profit = (DropDownList) this.ItemsDatagrid.Items[i].FindControl("Profit");
					Components.Server.ManageSaleScreen.MakeReadOnly(profit);
					TextBox qty = (TextBox) this.ItemsDatagrid.Items[i].FindControl("Quantity");
					qty.ReadOnly = true;
                    TextBox qtyFree = (TextBox)this.ItemsDatagrid.Items[i].FindControl("QuantityFree");
                    qtyFree.ReadOnly = true;
				}
                ShippingFeeTextBox.ReadOnly = true;
                ShippingImageButton.Visible = false;
                DiscountReasonRadioButtonList.Enabled = false;
                SurchargeReasonRadioButtonList.Enabled = false;
                DiscountTextBox.ReadOnly = true;
                SurchargeTextBox.ReadOnly = true;
                AdjusmentTotalTextBox.ReadOnly = true;
               
			}
			catch(Exception ex)
			{
				Logger.Error("Error in DisableEverything", ex);
			}
		
			
		}

        protected void ShippingImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (ShippingFeeTextBox.ReadOnly)
            {
                ShippingFeeTextBox.ReadOnly = false;
                ShippingFeeTextBox.BorderColor = Color.Yellow;
            }
            else
            {
                ShippingFeeTextBox.ReadOnly = true;
                ShippingFeeTextBox.BorderColor = Color.FromName("#5C86B0"); 
                //reset field
                ShippingFeeTextBox.Text = "";
            }
        }

        public void ResetAllProfitPercentage(int productClassID, string profit)
        {

				DataTable dgDataTable = GetDataFromDatagrid();
                if (dgDataTable.Rows.Count > 0)
                {
                    int i = -1;
                    foreach (DataRow dr in dgDataTable.Rows)
                    {
                        i++;
                        int scratchBookId = Convert.ToInt32(dr["ScratchBookID"]);
                        //get baseLevel for that product, we only want product class levels
                        string baseLevel = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBaseLevel(scratchBookId);
                        if (baseLevel == "ProductClass")
                        
                        {

                            ScratchBook sb = ScratchBook.GetScratchBookByID(scratchBookId);
                            int pcID = sb.ProductClassId;
                            if (pcID == productClassID)
                            {
                                //set dropdown
                               DropDownList ddprofit = (DropDownList)ItemsDatagrid.Items[i].FindControl("Profit");
                               ddprofit.SelectedValue = profit;
                                
                                
                            }
                        }
                    }
                }



        }

        public void ResetAllPackageProfitPercentage(int packageID, string profit)
        {

            DataTable dgDataTable = GetDataFromDatagrid();
            if (dgDataTable.Rows.Count > 0)
            {
                int i = -1;
                foreach (DataRow dr in dgDataTable.Rows)
                {
                    i++;
                    int scratchBookId = Convert.ToInt32(dr["ScratchBookID"]);
                    ScratchBook sb = ScratchBook.GetScratchBookByID(scratchBookId);
                  //get baseLevel for that product, we only want product class levels
                    string baseLevel = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBaseLevel(scratchBookId);
                    if (baseLevel == "Package")
                    {

                        int pID = sb.PackageId;
                        if (pID == packageID)
                        {
                            //set dropdown
                           DropDownList ddprofit = (DropDownList)ItemsDatagrid.Items[i].FindControl("Profit");
                            ddprofit.SelectedValue = profit;
                        }
                    }
                }
            }



        }


        public void ResetProfitPercentage(int productID, string profit)
        {

            DataTable dgDataTable = GetDataFromDatagrid();
            if (dgDataTable.Rows.Count > 0)
            {
                int i = -1;
                foreach (DataRow dr in dgDataTable.Rows)
                {
                    i++;
                    int scratchBookId = Convert.ToInt32(dr["ScratchBookID"]);
                      //get baseLevel for that product, we only want product levels
                        string baseLevel = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBaseLevel(scratchBookId);
                        if (baseLevel == "Product")
                        {
                            if (productID == scratchBookId)
                            {
                                //set dropdown
                                DropDownList ddprofit = (DropDownList)ItemsDatagrid.Items[i].FindControl("Profit");
                                ddprofit.SelectedValue = profit;

                            }
                        }
                }
            }



        }

        protected void SurchargeReasonRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SurchargeReasonRadioButtonList.SelectedValue))
            {
                // Popup result is the selected task
               // PopupControlExtender2.Commit(SurchargeReasonRadioButtonList.SelectedValue);
                HiddenSurchargeIDLabel.Text = SurchargeReasonRadioButtonList.SelectedValue;
                //PopupControlExtender2.Cancel();
            }
            else
            {
                // Cancel the popup
                //PopupControlExtender2.Cancel();
            }
            // Reset the selected item
            //SurchargeReasonRadioButtonList.ClearSelection();

        }

        protected void DiscountReasonRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DiscountReasonRadioButtonList.SelectedValue))
            {
                // Popup result is the selected task
                //PopupControlExtender1.Commit(DiscountReasonRadioButtonList.SelectedValue);
                HiddenDiscountIDLabel.Text = DiscountReasonRadioButtonList.SelectedValue;
                //PopupControlExtender1.Cancel();
            }
            else
            {
                // Cancel the popup
                //PopupControlExtender1.Cancel();
            }
            // Reset the selected item
           // DiscountReasonRadioButtonList.ClearSelection();
        }

       

   
				

	}
}

