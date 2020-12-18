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
using System.Xml.Linq;
using AjaxControlToolkit;
using efundraising.EFundraisingCRM.Linq;
using System.Web.Services;
using System.Collections.Generic;

//authenticate with FormsAuthenticationModule class in global asax
//masterpage
//custom button control with image
//Ajax (page method and service method)
//gridview
//LINQ
//div
//css
//css
//security
//generics


//namespace EfundraisingCRM.Sales.SalesScreen
namespace EFundraisingCRMWeb.Sales.SalesScreen
{

    enum cells {
        paidUpfront = 5
    };

    public partial class AdminSection : EFundraisingCRMSalesBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
           int debugRowLine = 0;
           try
           {
                if (!IsPostBack)
                {
                    Panel1.BackColor = System.Drawing.Color.Red;
                    Tabs.BackColor = System.Drawing.Color.Red;

                    int catalogID = 132;//112;
                    QSPFulfillmentDataContext db = new QSPFulfillmentDataContext();
                    debugRowLine = 3;

                    if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
                    {
                        debugRowLine = 4;
                        catalogID = 132;
                        db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString);
                    }
                    else
                    {
                        db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString);
                    }

                   

                    UpdateGrid();


                    //fill country
                    ListItem li = new ListItem("US", "US");
                    country2DropDownList.Items.Add(li);
                    li = new ListItem("CA", "CA");
                    country2DropDownList.Items.Add(li);

                    //fill country
                    li = new ListItem("YES", "true");
                    paidUpfrontDropDownList.Items.Add(li);
                    li = new ListItem("NO", "false");
                    paidUpfrontDropDownList.Items.Add(li);


                    //fill category
                    FillCategories(CatalogCategoryDropDownList);

                    //fill vendor
                    var vendors = from v in db.vendors orderby v.vendor_name ascending select v;
                    foreach (var vendor in vendors)
                    {
                        string name = vendor.vendor_name;
                        string value = vendor.vendor_id.ToString();

                        li = new ListItem(name, value);
                        VendorDropDownList.Items.Add(li);
                    }

                    //fill productType
                    var productTypes = from pt in db.product_types select pt;
                    foreach (var type in productTypes)
                    {
                        string name = type.product_type_name;
                        string value = type.product_type_id.ToString();

                        li = new ListItem(name, value);
                        DropDownListProductType.Items.Add(li);
                    }


                }
            }
            catch (Exception x)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: NewSales Page Load line:" + debugRowLine.ToString(), x);
      
            }
         
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static CascadingDropDownNameValue[] GetDropDownContentsPageMethod(string knownCategoryValues, string category)
        {

            return new ProductManager().GetDropDownContents(knownCategoryValues, category);
        }



        //TO DO//
        //MOVE LINQ QUERIES TO DB LAYER
        //

        protected void UpdateGrid(object sender, EventArgs e)
        {
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            // db = new QSPFulfillmentDataContext();

            QSPFulfillmentDataContext db = new QSPFulfillmentDataContext();

            int catalogID = 132; //112
            if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
            {
                catalogID = 132;
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString);
            }
            else
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString);
            }


            int categoryID = 0;
            int itemID = 0;
            if (CategoryDropDownList.SelectedValue != "")
            {
                categoryID = Convert.ToInt32(CategoryDropDownList.SelectedValue);
            }
            if (ItemDropDownList.SelectedValue != "")
            {
                itemID = Convert.ToInt32(ItemDropDownList.SelectedValue);
            }


            var a = 0;
            var products =

                    //TRY TO JOIN ON COMMISSION WITH THE ITEM
                   //IF NO RESULT< THEN THE COMMISSION FOR CATAGORY IS CHOSEN
                  //(DO THE UNION until THIS IS COMBINED IN SAME LINQ QUERY WITH 2 LEFT JOINS ON SAME TABLE)
                   (
                from ci in db.catalogItems
                    join cicc in db.catalogItemCategoryCatalogItems on ci.catalog_item_id equals cicc.catalog_item_id
                    join cic in db.catalogItemCategories on cicc.catalog_item_category_id equals cic.catalog_item_category_id
                    join cr in db.commissionRates on ci.catalog_item_id equals cr.catalog_item_id
                    where (cic.catalog_id == catalogID && cic.catalog_item_category_id != 3403
                            && cic.catalog_item_category_id != 3404 && cic.catalog_item_category_id != 3060
                            && ci.catalog_item_status_id != 703
                            )
                   
                           
                    orderby cic.catalog_item_category_id, ci.catalog_item_id
                    select new
                    {

                        ci.catalog_item_id,
                        cic.catalog_item_category_id,
                        cic.catalog_item_category_name,
                        ci.catalog_item_name,
                        ci.catalog_item_code,
                        price = ci.price == null ? 0 : ci.price,
                        ci.product_id,
                        cr.country_code,
                        cr.commission_rate_value,
                        cr.profit_rate_value,
                        cr.is_paid_upfront,
                        cr.commission_rate_id,
                        isGroup = false,
                        cicc.catalog_item_category_catalog_item_id

                    })
                    .Union
                     (
           

                   from ci in db.catalogItems
                   join cicc in db.catalogItemCategoryCatalogItems on ci.catalog_item_id equals cicc.catalog_item_id
                   join cic in db.catalogItemCategories on cicc.catalog_item_category_id equals cic.catalog_item_category_id
                   join cr in db.commissionRates on cic.catalog_item_category_id equals cr.catalog_item_category_id
                   where cic.catalog_id == catalogID
                         && cic.catalog_item_category_id != 3403
                           && cic.catalog_item_category_id != 3404
                           && cic.catalog_item_category_id != 3060
                            //&& cic.catalog_item_category_id == 3914
                                    && ci.catalog_item_status_id != 703 
                                     
                                    && cr.catalog_item_id == 0 //gets the value for item 0, does not exclude the other items

                                    && !(from cr2 in db.commissionRates //remove ones from previous query
                                         select cr2.catalog_item_id)
                                     .Contains(ci.catalog_item_id)


                   orderby cic.catalog_item_category_id, ci.catalog_item_id
                   select new
                   {

                       ci.catalog_item_id,
                       cic.catalog_item_category_id,
                       cic.catalog_item_category_name,
                       ci.catalog_item_name,
                       ci.catalog_item_code,
                       price = ci.price == null ? 0 : ci.price,
                       ci.product_id,
                       cr.country_code,
                       cr.commission_rate_value,
                       cr.profit_rate_value,
                       cr.is_paid_upfront,
                       cr.commission_rate_id,
                       isGroup = true,
                       cicc.catalog_item_category_catalog_item_id

                   })
                    ;

            //COUNTRY filter
            string country = CountryDropDownList.SelectedValue;
            if (country != "")
            {
                products = products.Where(cr => cr.country_code.Equals(country));
            }
            
            //ITEM filter
            string item = ItemDropDownList.SelectedValue;
            if (item != "")
            {
                products = products.Where(cr => cr.catalog_item_id.Equals(itemID));
                // cr.catalog_item_id
                // ci.catalog_item_id == itemID/
            }

            //CATEGORY filter
            string cat = CategoryDropDownList.SelectedValue;
            if (cat != "")
            {
                products = products.Where(cic => cic.catalog_item_category_id.Equals(cat));
            }

            //PROFIT rate Filter
            string rate = ProfitDropDownList.SelectedValue;
            if ( rate != "")
            {
                products = products.Where(cr => cr.profit_rate_value.Equals(rate));
            }
              
            ItemGridView.DataSource = products;
            ItemGridView.DataBind();

            legend.InnerHtml = "Items Found (" + products.Count() + ")";

            
          
          
        }

        protected void ItemGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            QSPFulfillmentDataContext db = new QSPFulfillmentDataContext();

            if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString);
            }
            else
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString);
            }

            int catalogID = 132;//112
      


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //////dropdown


                DropDownList ddl = (DropDownList) e.Row.FindControl("categoryGridDropDownList");

                FillCategories(ddl);              
                ddl.DataBind();
                ddl.SelectedValue = ItemGridView.DataKeys[e.Row.RowIndex].Values["catalog_item_category_id"].ToString();


                ////////
                bool upfront = Convert.ToBoolean(e.Row.Cells[(int)cells.paidUpfront].Text);
            
                if (upfront)
                {
                    e.Row.Cells[(int)cells.paidUpfront].Text = "Yes";
                }
                else
                {
                    e.Row.Cells[(int)cells.paidUpfront].Text = "No";
                }
                

                TextBox tb = (TextBox)e.Row.FindControl("CommissionTextBox");
                if (tb.Text != null)
                {
                    decimal percentage = Convert.ToDecimal(tb.Text);
                    //string newPercent = percentage * 100 + "%";
               
                   //tb.Text = percentage.ToString("0:#%");
                    tb.Text = percentage.ToString(); 
                    
                    //put warning if its Group, means the user will afect the whole category
                    bool isGroup = Convert.ToBoolean(ItemGridView.DataKeys[e.Row.RowIndex].Values["isGroup"]);
              
                    if (isGroup)
                    {                
                        tb.BorderColor = System.Drawing.Color.Red;
                        tb.BorderStyle = BorderStyle.Dashed;
                        tb.BorderWidth = 1;
                    }
                    
                }              
                
                tb = (TextBox)e.Row.FindControl("ProfitRateTextBox");
                if (tb.Text != null)
                {
                    if (tb.Text == "" || tb.Text == "&nbsp;" || tb.Text == "0.000")
                    {
                        tb.Text = "n/a";
                    }
                }
                                
                string country = ItemGridView.DataKeys[e.Row.RowIndex].Values["country_code"].ToString();
                            
                Image im = (Image)e.Row.FindControl("countryImage");
                if (im != null)
                {
                    if (country == "US")
                    {
                        im.ImageUrl = "../../Ressources/Images/us.jpg";
                    }
                    else if (country == "CA")
                    {
                        im.ImageUrl = "../../Ressources/Images/canada.jpg";
                    }
                }
                
                
                   

            }
        }


        protected void ItemGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            bool disable = false;
            bool save = false;
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Page")
            {
                index = index - 1;
            }
            
           /*Dim btn As Button = DirectCast(e.CommandSource, Button)
	         Dim gvr As GridViewRow = DirectCast(btn.NamingContainer, GridViewRow)

	         Dim sessionID As String = GridView1.DataKeys(gvr.RowIndex)("SessionID").ToString()
            
            
            */
                        
            QSPFulfillmentDataContext db = new QSPFulfillmentDataContext();

            if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString);
            }
            else
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString);
            }

            int catalogID = 132;//112
           
            int commissionRateID = Convert.ToInt32(ItemGridView.DataKeys[index].Values["commission_rate_id"]);
            int catalogItemID = Convert.ToInt32(ItemGridView.DataKeys[index].Values["catalog_item_id"]);
            int catalogItemCatID = Convert.ToInt32(ItemGridView.DataKeys[index].Values["catalog_item_category_id"]);
            int catalogItemCategoryCatalogID = Convert.ToInt32(ItemGridView.DataKeys[index].Values["catalog_item_category_catalog_item_id"]);

            
            int productID = Convert.ToInt32(ItemGridView.DataKeys[index].Values["product_id"]);
            bool isGrouped = Convert.ToBoolean(ItemGridView.DataKeys[index].Values["isGroup"]);
            string country = ItemGridView.DataKeys[index].Values["country_code"].ToString();
            string paidUpfront = ItemGridView.Rows[index].Cells[(int)cells.paidUpfront].Text.ToString();
            decimal commission = 0;
            string name = "";
            string code = "";
            decimal profitRate = 0;
            decimal price = 0;
            bool isPaidUpFront = false;


            if (paidUpfront == "Yes")
            {
                isPaidUpFront = true;
            }


            DropDownList dl = (DropDownList)ItemGridView.Rows[index].FindControl("categoryGridDropDownList");
            int selectedCategory = Convert.ToInt32(dl.SelectedValue);
            


            TextBox tb = (TextBox)ItemGridView.Rows[index].FindControl("CommissionTextBox");
            if (tb.Text != null)
            {
                commission = Convert.ToDecimal(tb.Text.Replace(",", "."));
            }

            tb = (TextBox)ItemGridView.Rows[index].FindControl("ProfitRateTextBox");
            if (tb.Text != null && tb.Text != "n/a")
            {
                profitRate = Convert.ToDecimal(tb.Text.Replace(",","."));
            }
            tb = (TextBox)ItemGridView.Rows[index].FindControl("ProductNameTextBox");
            if (tb.Text != null)
            {
                name = tb.Text;
            }
            tb = (TextBox)ItemGridView.Rows[index].FindControl("ProductCodeTextBox");
            if (tb.Text != null)
            {
                code = tb.Text;
            }
            tb = (TextBox)ItemGridView.Rows[index].FindControl("PriceTextBox");
            if (tb.Text != null)
            {
                string p = tb.Text.Replace(",", ".");
                price = Convert.ToDecimal(p.Replace("$", ""));
               
            }


           if (e.CommandName == "Delete")
              {
                commissionRate commrate = (from c in db.commissionRates
                                           where c.commission_rate_id == commissionRateID
                                           select c).First();
                db.commissionRates.DeleteOnSubmit(commrate);
                db.SubmitChanges();


               Label lbl = (Label)ItemGridView.Rows[index].FindControl("saveLabel");
               lbl.Text = "Item Deleted";
               lbl.Visible = true;






           }
           else if (e.CommandName == "Edit")
           {
               disable = true;




           }

           else if (e.CommandName == "Select")
           {
               save = true;
           }

            if (save || disable)
            {
               if (ItemGridView.Rows[index].RowType == DataControlRowType.DataRow)
               {
                   //get record
                   var product = (from p in db.products
                                  where p.product_id == productID
                                        && p.business_division_id == 4
                                  select p).Single();
                   product.price = price;
                   product.commission = commission;
                   product.product_name = name;
                   product.product_code = code;
                   product.IVITEM = code;
                   if (disable){
                       product.product_status_id = 703; //Inactive Product
                   }

                   var item = (from ci in db.catalogItems
                               where ci.catalog_item_id == catalogItemID
                                     && ci.catalog_id == catalogID
                               select ci).Single();
                   item.price = price;
                   item.catalog_item_name = name;
                   item.catalog_item_code = code;
                   
                   if (disable)
                   {
                       item.catalog_item_status_id = 703; //Inactive Product
                   }

                   var catalogItemDetail = (from cid in db.catalog_item_details
                                            join ci in db.catalogItems on cid.catalog_item_id equals ci.catalog_item_id
                                            where ci.catalog_item_id == catalogItemID
                                            select cid).Single();

                   catalogItemDetail.price = price;
                   catalogItemDetail.catalog_item_detail_name = name;
                   catalogItemDetail.catalog_item_detail_code = code;
                   

                   //update category

                   var category = (from c in db.catalogItemCategoryCatalogItems
                                   where c.catalog_item_category_catalog_item_id == catalogItemCategoryCatalogID
                                   select c).Single();
                   category.catalog_item_category_id = selectedCategory;

                   //


                   var comm = (from cr in db.commissionRates
                               where cr.commission_rate_id == commissionRateID
                               select cr).SingleOrDefault();

                   comm.commission_rate_value = commission;
                   comm.profit_rate_value = profitRate;


                   db.SubmitChanges();


                   Label lbl = (Label)ItemGridView.Rows[index].FindControl("saveLabel");
                   lbl.Text = "Item Saved";
                   lbl.Visible = true;
               }
           }
            
        }

   

        protected void ItemGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ItemGridView.PageIndex = e.NewPageIndex;
            ItemGridView.DataBind();
            UpdateGrid();
        }
        


        protected void ItemGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
                //put warning if its Group, means the user will afect the whole category
                /*   bool isGrouped = Convert.ToBoolean(ItemGridView.DataKeys[e.NewEditIndex].Values["isGroup"]);
                             
 
                    if (isGrouped)
                    {

                        TextBox tb = (TextBox) ItemGridView.Rows[e.NewEditIndex].FindControl("CommissionTextBox");
                        if (tb.Text != null)
                        {

                            tb.BorderColor = System.Drawing.Color.Red;
                            tb.BorderStyle = BorderStyle.Dashed;
                            tb.BorderWidth = 3;
                        }
                    }*/
        }


        //INSERT PRODUCT
        protected void Button1_Click(object sender, EventArgs e)
        {

            QSPFulfillmentDataContext db = new QSPFulfillmentDataContext();

            if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString);
            }
            else
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString);
            }
                      
     
    
            int product_type = Convert.ToInt32(DropDownListProductType.SelectedValue); 
            string name = TextBoxProductName.Text;
            string product_code = TextBoxProductCode.Text;
            string price = TextBoxPrice.Text;
            bool error = false;
            try
            {
                int ivi = Convert.ToInt32(product_code);
            }
            catch (Exception x)
            {
                LabelError.Visible = true;
                LabelError.Text = "Error in IVItem!";
                error = true;
            }

            if (!error)
            {
                string ivitem = product_code;
                if (ivitemTextBox.Text != "")
                {
                    ivitem = ivitemTextBox.Text;
                }

                string ivicoup = ivicoupTextBox.Text;

                int user = 101762;
                int catalogID = 132;// 112;
                if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
                {
                    catalogID = 132;
                    user = 101721;
                }



                int businessDivision = 4;
                int vendor = Convert.ToInt32(VendorDropDownList.SelectedValue);

                int catalogCategory = Convert.ToInt32(CatalogCategoryDropDownList.SelectedValue);

                var insert = db.pr_product_catalog_item_insert(product_type,
                                                               product_code,
                                                               name,
                                                               ivitem,
                                                               ivicoup,
                                                               price,
                                                               user,
                                                               businessDivision,
                                                               vendor,
                                                               catalogID,
                                                               catalogCategory);
                if (insert != 0)
                {
                    LabelError.Visible = true;
                    LabelError.Text = "Error inserting product!";
                }
                else
                {
                    LabelError.Visible = true;
                    LabelError.Text = "Success!";
                }

            }

        }

        protected void ItemGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
         /*  
            
          */
        }


        //INSERT VENDOR
        protected void Button2_Click(object sender, EventArgs e)
        {

            int user = 101762;

            QSPFulfillmentDataContext db = new QSPFulfillmentDataContext();

            if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
            {
                user = 101721;
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString);
            }
            else
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString);
            }

            string name = TextBoxVendorName.Text;
         
         


            int? result = 0;
            var insert = db.pr_vendor_Insert(null,
                                                           null,
                                                           null,
                                                           null, 
                                                           name,   
                                                           "0",
                                                           "",
                                                           "",                                                           
                                                           null,
                                                           0,ref result);
            if (insert != 0)
            {
                LabelError.Visible = true;
                LabelError.Text = "Error inserting vendor!";
            }

        }

        protected void InsertCatalogButton_Click(object sender, EventArgs e)
        {
            QSPFulfillmentDataContext db = new QSPFulfillmentDataContext();

            if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString);
            }
            else
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString);
            }

            string name = TextBoxCategoryName.Text;
            int user = 101762;
            int catalogID = 132;// 112;
            int formID = 42; //EFR
            int formSectionType = 1;
            if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
            {
                catalogID = 132;
                user = 101721;
            }


            int? result = 0;
            var insert = db.pr_product_category_insert(user, catalogID, name, formID, formSectionType);
            if (insert != 0)
            {
                LabelError.Visible = true;
                LabelError.Text = "Error inserting category!";
            }
            else
            {
                //CatalogCategoryDropDownList.Items.Add(li);
            }
        }

        protected void Tabs_ActiveTabChanged(object sender, EventArgs e)
        {

        }

        protected void insertCommissionButton_Click(object sender, EventArgs e)
        {

            QSPFulfillmentDataContext db = new QSPFulfillmentDataContext();

            if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString);
            }
            else
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString);
            }

            
            int categoryID = Convert.ToInt32(CategoryDropDownList.SelectedValue);
            int itemID = 0;
            if (ItemDropDownList.SelectedValue != "")
            {
                itemID = Convert.ToInt32(ItemDropDownList.SelectedValue);
            }

            decimal profitrateValue = 0;
            if (profitRateTextBox.Text.Trim() != "")
            {
                profitrateValue = Convert.ToDecimal(profitRateTextBox.Text.Replace(",", "."));    
            }
            
            decimal commRate = Convert.ToDecimal(commRateTextbox.Text.Replace(",","."));
            bool paidUpfront = Convert.ToBoolean(paidUpfrontDropDownList.SelectedValue);
            string Country = country2DropDownList.SelectedValue;



            int? result = 0;
            var insert = db.pr_commission_rate_insert(categoryID, itemID, profitrateValue, commRate, paidUpfront,Country);
            if (insert != 0)
            { 
               CommissionErrorLabel.Visible = true;
               CommissionErrorLabel.Text = "Error inserting commission!";
            }
            else
            {
                //CatalogCategoryDropDownList.Items.Add(li);
            }


        }


        private void FillCategories(DropDownList ddl)
        {
            int catalogID = 132;// 112;
            QSPFulfillmentDataContext db = new QSPFulfillmentDataContext();

            if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
            {
                catalogID = 132;
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString);
            }
            else
            {
                db = new QSPFulfillmentDataContext(ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString);
            }

            //fill category
            var categories = from p in db.catalogItemCategories where p.catalog_id == catalogID select p;
            foreach (var cat in categories)
            {
                string name = cat.catalog_item_category_name;
                string value = cat.catalog_item_category_id.ToString();

                if (name != "EFR WS" && name != "Lollipops" && name != "3393" && name != "3035")
                {
                    ListItem li = new ListItem(name, value);
                    ddl.Items.Add(li);

                }

            }

        }

      

        protected void EfundImageButton_Click(object sender, ImageClickEventArgs e)
        {

            Response.Redirect("http://caefr3k07.corp.ad.timeinc.com:1480/AdministrationProductpackage.aspx", false);
        }


     
    }
}
