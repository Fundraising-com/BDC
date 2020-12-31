using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using Business;
//using DAL;

namespace QSPFulfillment.OrderMgt //StraightOrderEntry
{
	///<summary>OrderReturns</summary>
	public partial class OrderReturns : QSPFulfillment.AcctMgt.AcctMgtPage
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			this.DGGiftOrderDetail.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DGGiftOrderDetail_ItemCreated);
			this.DGGiftOrderDetail.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGGiftOrderDetail_ItemCommand);
			this.DGGiftOrderDetail.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DGGiftOrderDetail_ItemDataBound);
		}
		#endregion auto-generated code
		
		#region Item Declarations


		protected QSPFulfillment.OrderMgt.UC.BatchType ucBatchType;
		protected QSPFulfillment.OrderMgt.UC.OrderQualifier ucOrderQualifier;
		//protected System.Web.UI.WebControls.DropDownList ddlOrderType;
		//protected System.Web.UI.WebControls.DropDownList ddlOrderQualifier;
		//private string connStringCommon  = "server=161.230.158.127; uid=msiddiq; pwd=msiddiq; database=QSPCanadaCommon";
		//private string connStringOrderManagement = "server=161.230.158.127; uid=msiddiq; pwd=msiddiq; database=QSPCanadaOrderManagement";
		//Business.CodeHeader OrderTypeCodeHeader = Business.CodeHeader.OrderTypeCode;
		//Business.CodeHeader OrderQualifierCodeHeader = Business.CodeHeader.OrderQualifier;
		//DataView GiftOrderDV = new DataView();
		#endregion Item Declarations
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				populate_list_items();
				dgGiftOrder_bind();
			}
		}

		private void populate_list_items()
		{
			//--------------Batch Types-------------------//
			this.ucBatchType.Bind();

			//----------Order Qualifiers-----------------//
			this.ucOrderQualifier.Bind();
		}

		private void dgGiftOrder_bind()
		{
			Business.GiftOrderReturned g = new Business.GiftOrderReturned();
			if (Convert.ToString(lblOrderId.Text) == "")
			{
				g.Order_Id =  0;  //Convert.ToInt32(lblOrderId.Text) ;  
			}
			else
			{
				g.Order_Id =  Convert.ToInt32(lblOrderId.Text) ;
			}
			//GiftOrderds = g.GetGiftOrderDataSet();
			//dvGiftOrderds = GiftOrderds.Tables["Table"].DefaultView;
			DataTable GiftOrderDT = g.GetGiftOrderData();
			DataView  GiftOrderDV = GiftOrderDT.DefaultView;

			//dvDeposits.Sort = dgDepositsSortfield;
			DGGiftOrderDetail.DataSource = GiftOrderDV;
			DGGiftOrderDetail.DataBind();
		}


		protected void CampaignId_TextChanged(object s, System.EventArgs e)
		{
			lblMessage.Text = "";//Reset error message 
			
			Business.BillShipAccount Ac = new Business.BillShipAccount();
			Ac.Campaign_Id = Convert.ToInt32(CampaignId.Text);
			DataTable AccountDT = Ac.GetBillShipAccountData();
			if (AccountDT.Rows.Count> 0)
			{
				// Populate Data
				//JLC-TODO: This should be going on in a Business class (reading of the data)
				//then the webpage reads the biz class members
				ShiptoAccountId.Text =	AccountDT.Rows[0].ItemArray[8].ToString();
				Acc.Text = ShiptoAccountId.Text;
				ShiptoFMId.Text = AccountDT.Rows[0].ItemArray[19].ToString();
				//ShiptoEmpId.Text = AccountDT.Rows[0].ItemArray[8].ToString();
				lblShiptoName.Text = AccountDT.Rows[0].ItemArray[9].ToString();
				lblShiptoAddress.Text = AccountDT.Rows[0].ItemArray[10].ToString();
				lblShiptoCity.Text=AccountDT.Rows[0].ItemArray[12].ToString();
				lblBilltoName.Text = AccountDT.Rows[0].ItemArray[2].ToString();
				lblBilltoAddress.Text = AccountDT.Rows[0].ItemArray[3].ToString();
				lblBilltoCity.Text= AccountDT.Rows[0].ItemArray[5].ToString();

				if (AccountDT.Rows[0].ItemArray[20].ToString()== "")
				{
					ShiptoContactName1.Text=AccountDT.Rows[0].ItemArray[15].ToString();
				}
				else
				{
					ShiptoContactName1.Text=AccountDT.Rows[0].ItemArray[20].ToString();
				}
				if (AccountDT.Rows[0].ItemArray[21].ToString()== "")
				{
					ShiptoContactName2.Text=AccountDT.Rows[0].ItemArray[16].ToString();
				}
				else
				{
					ShiptoContactName2.Text=AccountDT.Rows[0].ItemArray[21].ToString();
				}

				if (AccountDT.Rows[0].ItemArray[23].ToString()== "")

				{
					ShiptoContactEmail.Text=AccountDT.Rows[0].ItemArray[24].ToString();
				}
				else
				{
					ShiptoContactEmail.Text=AccountDT.Rows[0].ItemArray[24].ToString();
				}
				
				
				if (AccountDT.Rows[0].ItemArray[22].ToString()== "")
				{
					ShiptoContactPhone.Text=AccountDT.Rows[0].ItemArray[17].ToString();
				}
				else
				{
					ShiptoContactPhone.Text=AccountDT.Rows[0].ItemArray[22].ToString();
				}
				//ShiptoContactFax.Text=AccountDT.Rows[0].ItemArray[16].ToString();


				BilltoAccountId.Text=AccountDT.Rows[0].ItemArray[1].ToString();
				BilltoFMId.Text = AccountDT.Rows[0].ItemArray[19].ToString();
			}
			else
			{
				// No account Data found set field values to null
				ShiptoAccountId.Text = null;
				InitializeField();
			}
			//Ac.ValidateAndSave();
			
		}

	
		protected void ShiptoAccountId_TextChanged(object sender, System.EventArgs e)
		{
			if (ShiptoAccountId.Text != Acc.Text)
			{
				InitializeField();
			}
			else
			{
				ShiptoFMId.Text = 	null;
				ShiptoEmpId.Text = 	null;
			}
		}

	
		private void InitializeField()
		{
			CampaignId.Text=null;
			InvoiceId.Text=null;
			//ShiptoAccountId.Text =	null;
			ShiptoFMId.Text = 	null;
			ShiptoEmpId.Text = 	null;
			lblShiptoName.Text = 	null;
			lblShiptoAddress.Text =	null;
			lblShiptoCity.Text=	null;
			lblBilltoName.Text = 	null;
			lblBilltoAddress.Text = 	null;
			lblBilltoCity.Text= 	null;
			BilltoAccountId.Text=null;
			BilltoFMId.Text =null;
			ShiptoContactName1.Text=null;
			ShiptoContactName2.Text=null;
			ShiptoContactEmail.Text=null;
			ShiptoContactPhone.Text=null;
			ShiptoContactFax.Text=null;
			// lblDateCreated.Text=null;
			//lblDateModified.Text=null;
			//	lblLastUpdatedBy.Text=null;
			ShiptoContactEmail.Text=null;
		}

		
		protected void ShiptoFMId_TextChanged(object s, System.EventArgs e)
		{
			ShiptoAccountId.Text =	null;
			ShiptoEmpId.Text = 	null;
		}

		
		protected void ShiptoEmpId_TextChanged(object s, System.EventArgs e)
		{
			ShiptoFMId.Text = 	null;
			ShiptoAccountId.Text =	null;
		}

		
		private void DGGiftOrderDetail_SelectedIndexChanged(object s, System.EventArgs e)
		{
			lblMessage.Text = "sind command";
		}

		
		public void tb_TextChanged (object s, System.EventArgs e)
		{
			//Response.Write(this.Events.ToString());
		}
	
		
		private void DGGiftOrderDetail_ItemCommand(object s, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "InsertGiftItem")
			{
				lblMessage.Text = "";
				if  (Convert.ToInt32(CampaignId.Text)> 0) 
				{
								
					if ((e.Item.ItemType==ListItemType.Footer)&& ValidateHeader() )
					{
						TextBox tbPCode = (TextBox) e.Item.FindControl("ProductCode");
						TextBox PDesc   = (TextBox) e.Item.FindControl("ProductDesc");
						DropDownList ddlCType = (DropDownList) e.Item.FindControl("ddlCatalogType");
						TextBox CatName = (TextBox) e.Item.FindControl("CatalogName");
						TextBox Qty = (TextBox) e.Item.FindControl("Quantity");
						TextBox UPrice = (TextBox) e.Item.FindControl("UnitPrice");
						TextBox CatPrice = (TextBox) e.Item.FindControl("CatalogPrice");
						
						DropDownList ddlpoverride = (DropDownList)e.Item.FindControl("ddlPriceOverride");

						if ((PDesc.Text== "")|| (tbPCode.Text=="") ||(tbPCode.Text=="0"))
						{
							lblMessage.Text = "Invalid product code";
						}
						
						
						if ((PDesc.Text != "" && tbPCode.Text != "0" && tbPCode.Text!="") )
						{	
							
							if (Convert.ToDouble(CatPrice.Text) != Convert.ToDouble(UPrice.Text)&&
								((ddlpoverride.SelectedValue =="")||(ddlpoverride.SelectedValue =="45004") ))
							{
								lblMessage.Text = "Unit price does not match with catalog price";
							}
						}	
						if (ddlCType.SelectedValue =="")
						{
							lblMessage.Text = "Please select catalog type";
						}
					
						if (Convert.ToInt32(Qty.Text) < 1)
						{
							lblMessage.Text = "Invalid Quantity";
						}

					}
					
					if (this.lblOrderId.Text== "" )
					{
						//insert customer
						DataSet CustDataSet = new DataSet();
						Business.CustomerAcc C = new Business.CustomerAcc();
						C.Account_Id = Convert.ToInt32(this.BilltoAccountId.Text);
						C.ChangeUser_Id = "1";
						C.ValidateAndSave();
						lblMessage.Text = Convert.ToString(C.CustomerInstance);

						// Create Batch and Customer Order Header record.
						DataSet BatchOrderHeaderds = new DataSet();
						Business.BatchAndCodeHeader Bcoh = new Business.BatchAndCodeHeader();
						Bcoh.BatchDate =DateTime.Today;
						Bcoh.BilltoacctId= Convert.ToInt32(BilltoAccountId.Text);
						Bcoh.ShiptoacctId= Convert.ToInt32(ShiptoAccountId.Text);
						Bcoh.Campaignid= Convert.ToInt32(CampaignId.Text);
						Bcoh.StatusId= 40001; //New
						Bcoh.OrdertypecodeId= this.ucBatchType.SelectedValue;
						Bcoh.OrderqualifierId= this.ucOrderQualifier.SelectedValue;
						Bcoh.CustomerinstanceId= C.CustomerInstance;
						Bcoh.ChangeUser_Id="1";
						Bcoh.OrderId=-1;
						Bcoh.Coh=-1;
						Bcoh.ValidateAndSave();

						lblMessage.Text = Convert.ToString(Bcoh.OrderId) +" "+Convert.ToString(Bcoh.Coh);
						lblOrderId.Text= Convert.ToString(Bcoh.OrderId);
						coh.Text = Convert.ToString(Bcoh.Coh);
					
						//  Create OrderItem
						TextBox Qty1 = (TextBox) e.Item.FindControl("Quantity");
						TextBox UPrice1 = (TextBox) e.Item.FindControl("UnitPrice");
						TextBox CatPrice1 = (TextBox) e.Item.FindControl("CatalogPrice");
						TextBox tbPCode1 = (TextBox) e.Item.FindControl("ProductCode");

						DataSet OrderDetailds = new DataSet();
						Business.OrderDetail COD = new Business.OrderDetail();
						COD.Coh = Bcoh.Coh;
						COD.producttype = 46002;  //Gift
						COD.productcode_Id = Convert.ToString(tbPCode1.Text);
						COD.quantity= Convert.ToInt32(Qty1.Text);
						COD.price = Convert.ToDouble(UPrice1.Text);
						COD.catalogprice = Convert.ToDouble(CatPrice1.Text);
						COD.status = 500; //OrderItemGood
						COD.ValidateAndSave();
						dgGiftOrder_bind();
					}
					else
					{
						if ((e.Item.ItemType==ListItemType.Footer)&& ValidateHeader() )
						{
							TextBox Qty1 = (TextBox) e.Item.FindControl("Quantity");
							TextBox UPrice1 = (TextBox) e.Item.FindControl("UnitPrice");
							TextBox CatPrice1 = (TextBox) e.Item.FindControl("CatalogPrice");
							TextBox tbPCode1 = (TextBox) e.Item.FindControl("ProductCode");
						
							//DataSet BatchOrderHeaderds = new DataSet();
							//BatchAndCodeHeader Bcoh = new BatchAndCodeHeader();

							DataSet OrderDetailds = new DataSet();
							Business.OrderDetail COD = new Business.OrderDetail();
							COD.Coh = Convert.ToInt32(coh.Text);
							COD.producttype = 46002;  //Gift
							COD.productcode_Id = Convert.ToString(tbPCode1.Text);
							COD.quantity= Convert.ToInt32(Qty1.Text);
							COD.price = Convert.ToDouble(UPrice1.Text);
							COD.catalogprice = Convert.ToDouble(CatPrice1.Text);
							COD.status = 500; //OrderItemGood
							COD.ValidateAndSave();
							dgGiftOrder_bind();
						}
					}

				}
				else
				{
					lblMessage.Text = "Campaign Id s required, please enter";
				}
			}
			if (e.CommandName == "ValidatePcode")
			{
				lblMessage.Text = "";
				if  (Convert.ToInt32(CampaignId.Text)> 0)

				{
					if(e.Item.ItemType==ListItemType.Footer)
					{
						TextBox tbPCode = (TextBox) e.Item.FindControl("ProductCode");
						TextBox CatPrice = (TextBox) e.Item.FindControl("CatalogPrice");
						TextBox PDesc   = (TextBox) e.Item.FindControl("ProductDesc");
						TextBox CatName = (TextBox) e.Item.FindControl("CatalogName");
					
						Business.ProductCatalogPrice prod = new Business.ProductCatalogPrice();
						prod.Campaign_Id = Convert.ToInt32(CampaignId.Text);
						prod.ProductCode = tbPCode.Text;
						DataTable ProductDT =	prod.GetProductCatalogPriceData();
						//if (ProductDataSet.Tables["Table"].Rows.Count> 0)
						if(ProductDT.Rows.Count > 0)
						{
							//lblMessage.Text = "";
							CatPrice.Text = ProductDT.Rows[0].ItemArray[3].ToString();
							PDesc.Text    = ProductDT.Rows[0].ItemArray[2].ToString();
							CatName.Text  = ProductDT.Rows[0].ItemArray[5].ToString();
						}
						else
						{
							lblMessage.Text = "No item record found for the product code "+tbPCode.Text;
						}
					}
				}
				else
				{
					lblMessage.Text = "Campaign Id is required, please enter";
				}
			}
		}


		private void DGGiftOrderDetail_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Footer)
			{
				TextBox tb = (TextBox) e.Item.FindControl("ProductCode");
				//tb.TextChanged +=new EventHandler(tb_TextChanged);
				//TextBox tb= c as TextBox;
               
				if (tb.Text  != "0")
				{
					//e.Item.Cells[2].Text = "Non Zero";
				}
				else
				{
					//e.Item.Cells[2].Text = "Zero";
				}
				//tb.TextChanged +=new EventHandler(tb_TextChanged);
				
			}
		}
	

		#region items to be coded into a control ? eliminated sql, now removed hardcoded ddl stuff
		private void populateDDLCatalog(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//Populate the ProgramMasterSubType
			//Business.CodeHeader CatalogTypeCodeHeader = Business.CodeHeader.ProgramMasterSubType;
			DropDownList DDL = (DropDownList) e.Item.FindControl("ddlCatalogType");
			DDL.Items.Clear();
			DDL.Items.Add(new ListItem("", String.Empty));
			DDL.Items.Add(new ListItem("Fundraising","30300"));
		}

		private void populateDDLPriceOverride(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//Populate the OverrideID DDL
			//Business.CodeHeader PriceOverrideCodeHeader = Business.CodeHeader.OverrideID;
			DropDownList DDL = (DropDownList) e.Item.FindControl("ddlPriceOverride");
			DDL.Items.Clear();
			DDL.Items.Add(new ListItem("", String.Empty));
			DDL.Items.Add(new ListItem("Closest Matching Offer","45003"));
			DDL.Items.Add(new ListItem("Coupon","45001"));
			DDL.Items.Add(new ListItem("Invalid Price","45002"));
			DDL.Items.Add(new ListItem("None","45004"));
			DDL.Items.Add(new ListItem("Replacement","45005"));
		}
		#endregion items to be coded into a control ? eliminated sql, now removed hardcoded ddl stuff

		public bool ValidateHeader()
		{
			//this only validates one item at a time
			//we like to validate the whole page
			//need to use built in validators
			//and change this approach

			if ( (this.BilltoAccountId.Text == "") ||
				 (this.ShiptoAccountId.Text == "") )
			{ 
				lblMessage.Text = "Bill/Shipto account Id is required, please enter";
				return false;
			}
			else if((this.ucBatchType.SelectedValue == -5) || (this.ucOrderQualifier.SelectedValue == -5))
			{
				lblMessage.Text = "Order Type/Qualifier is missing, please enter";
				return false;
			}	
			return true;
		}
	

		private void DGGiftOrderDetail_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Footer)
			{
				populateDDLCatalog(e);
				populateDDLPriceOverride(e);
			}
		}

		
	}
}
