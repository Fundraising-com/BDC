using System;
using System.Data;
using System.Collections.Generic;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.Data.Sql;
using GA.BDC.Core.ESubsGlobal.QSPECommerce;
using GA.BDC.Core.ESubsGlobal.QSPFulfillment;

namespace GA.BDC.Core.ESubsGlobal.DataAccess
{
    public class QSPReplication:ESubsGlobalDatabase
    {
        public QSPReplication() { }

        #region Cart

        private Cart loadCart(DataRow row)
        {
            Cart cart = new Cart();

            // Store database values into our business object
            cart.CartID = DBValue.ToInt32(row["cart_id"]);
            cart.XCatalogGroupID = DBValue.ToInt32(row["x_catalog_group_id"]);
            cart.OnlineParticipantID = DBValue.ToInt32(row["online_participant_id"]);
            cart.SiteID = DBValue.ToInt32(row["site_id"]);
            cart.BillingXPostalAddressID = DBValue.ToInt32(row["billing_x_postal_address_id"]);
            cart.BillingXPhoneNumberID = DBValue.ToInt32(row["billing_x_phone_number_id"]);
            cart.Email = DBValue.ToString(row["email"]);
            cart.CCType = DBValue.ToString(row["cc_type"]);
            cart.CCExpMonth = DBValue.ToInt32(row["cc_exp_month"]);
            cart.CCExpYear = DBValue.ToInt32(row["cc_exp_year"]);
            cart.CCNameOnCard = DBValue.ToString(row["cc_name_on_card"]);
            cart.XOrderID = DBValue.ToInt32(row["x_order_id"]);
            cart.CartGUID = DBValue.ToString(row["cart_guid"]);
            cart.XCreditCardID = DBValue.ToInt32(row["x_credit_card_id"]);
            cart.EDSOrderID = DBValue.ToInt32(row["eds_order_id"]);
            cart.CreateDate = DBValue.ToDateTime(row["create_date"]);
            cart.ModifyDate = DBValue.ToDateTime(row["modify_date"]);
            cart.ModifiedBy = DBValue.ToString(row["modified_by"]);
            cart.DeletedTF = DBValue.ToInt32(row["deleted_tf"]);
            cart.PriceApplied = DBValue.ToDecimal(row["price_applied"]);
            cart.TemplateID = DBValue.ToInt32(row["template_id"]);
            cart.IsOrderExportable = DBValue.ToInt32(row["isorderexportable"]);

            // return the filled object
            return cart;
        }

        #endregion

        #region Cart Detail

        private CartDetail loadCartDetail(DataRow row)
        {
            CartDetail cartDetail = new CartDetail();

            // Store database values into our business object
            cartDetail.CartDetailID = DBValue.ToInt32(row["cart_detail_id"]);
            cartDetail.CartID = DBValue.ToInt32(row["cart_id"]);
            cartDetail.XCatalogItemDetailID = DBValue.ToInt32(row["x_catalog_item_detail_id"]);
            cartDetail.XOrderDetailID = DBValue.ToInt32(row["x_order_detail_id"]);
            cartDetail.RenewalTF = DBValue.ToInt32(row["renewal_tf"]);
            cartDetail.GiftTF = DBValue.ToInt32(row["gift_tf"]);
            cartDetail.Quantity = DBValue.ToInt32(row["quantity"]);
            cartDetail.ShippingXPostalAddressID = DBValue.ToInt32(row["shipping_x_postal_address_id"]);
            cartDetail.CreateDate = DBValue.ToDateTime(row["create_date"]);
            cartDetail.ModifyDate = DBValue.ToDateTime(row["modify_date"]);
            cartDetail.ModifiedBy = DBValue.ToString(row["modified_by"]);
            cartDetail.DeletedTF = DBValue.ToInt32(row["deleted_tf"]);
            cartDetail.PriceApplied = DBValue.ToDecimal(row["price_applied"]);
            cartDetail.GiftEmailAddress = DBValue.ToString(row["gift_email_address"]);
            cartDetail.GiftPersonalizedMessage = DBValue.ToString(row["gift_personalized_message"]);
            cartDetail.GiftRecipient = DBValue.ToString(row["gift_recipient"]);
            cartDetail.GiftFrom = DBValue.ToString(row["gift_from"]);

            // return the filled object
            return cartDetail;
        }


        public CartDetail GetCartDetailByOrderDetailID(int id)
        {
            return GetCartDetailByOrderDetailID(id, null);
        }

        private CartDetail GetCartDetailByOrderDetailID(int id, SqlInterface si)
        {
            CartDetail cartDetail = null;

            string storedProcName = "pr_get_cart_detail_by_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@order_detail_id", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        cartDetail = loadCartDetail(dt.Rows[0]);
                       
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }

                return cartDetail;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            
        }

        #endregion

        #region Catalog Item

        public  CatalogItem GetCatalogItemByID(int id)
        {
            return GetCatalogItemByID(id, null);
        }     
        private CatalogItem GetCatalogItemByID(int id, SqlInterface si)
        {
            CatalogItem catalogItem = null;

            string storedProcName = "es_get_catalog_item_by_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@catalog_item_id ", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        catalogItem = LoadCatalogItem(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return catalogItem;
        }

        public CatalogItem GetCatalogItemByCatalogIDandCode(int catalogid, string catalogItemCode)
        {
            return GetCatalogItemByCatalogIDandCode(catalogid, catalogItemCode, null);
        }
        private CatalogItem GetCatalogItemByCatalogIDandCode(int catalogid, string catalogItemCode, SqlInterface si)
        {
            CatalogItem catalogItem = null;

            string storedProcName = "es_get_catalog_item_by_catalog_id_and_code";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@catalog_id ", DbType.Int32, DBValue.ToDBInt32(catalogid)));
                paramCol.Add(new SqlDataParameter("@catalog_item_code ", DbType.String, DBValue.ToString(catalogItemCode)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        catalogItem = LoadCatalogItem(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return catalogItem;
        }

        private CatalogItem LoadCatalogItem(DataRow row)
        {
            CatalogItem ci = new CatalogItem();

            // Store database values into our business object
            ci.CatalogItemId = DBValue.ToInt32(row["catalog_item_id"]);
            ci.CatalogId = DBValue.ToInt32(row["catalog_id"]);
            ci.ProductId = DBValue.ToInt32(row["product_id"]);
            ci.CatalogItemCode = DBValue.ToString(row["catalog_item_code"]);
            ci.CatalogItemName = DBValue.ToString(row["catalog_item_name"]);
            ci.Description = DBValue.ToString(row["description"]);
            ci.NbUnits = DBValue.ToInt32(row["nb_units"]);
            ci.Price = DBValue.ToDecimal(row["price"]);
            ci.ImageUrl = DBValue.ToString(row["image_url"]);
            ci.Deleted = DBValue.ToBoolean(row["deleted"]);
            ci.CreateDate = DBValue.ToDateTime(row["create_date"]);
            ci.CreateUserId = DBValue.ToInt32(row["create_user_id"]);
            ci.UpdateDate = DBValue.ToDateTime(row["update_date"]);
            ci.UpdateUserId = DBValue.ToInt32(row["update_user_id"]);

            return ci;
        }

        public CatalogItem GetLatestCatalogItemByCatalogItemID(int catalog_item_id, int catalog_id)
        {
            return GetLatestCatalogItemByCatalogItemID(catalog_item_id, catalog_id, null);
        }

        private CatalogItem GetLatestCatalogItemByCatalogItemID(int catalog_item_id, int catalog_id, SqlInterface si)
        {
            CatalogItem catalogItem = null;

            string storedProcName = "es_get_latest_catalog_item_by_catalog_item_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Catalog_item_id", DbType.Int32, DBValue.ToDBInt32(catalog_item_id)));
                paramCol.Add(new SqlDataParameter("@Catalog_id", DbType.Int32, DBValue.ToDBInt32(catalog_id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        catalogItem = LoadCatalogItem(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return catalogItem;
        }

        public CatalogItem GetCatalogItemByOrderDetailID(int id)
        {
            return GetCatalogItemByOrderDetailID(id, null);
        }

        private CatalogItem GetCatalogItemByOrderDetailID(int id, SqlInterface si)
        {
            CatalogItem catalogItem = null;

            string storedProcName = "es_get_catalog_item_by_order_detail_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@order_detail_id ", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        catalogItem = LoadCatalogItem(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return catalogItem;
        }

        #endregion

        #region Product Type

        private ProductType LoadProductType(DataRow row)
        {
            ProductType product_type = new ProductType();

            // Store database values into our business object
            product_type.ProductTypeID = DBValue.ToInt32(row["product_type_id"]);
            product_type.ProductLineID = DBValue.ToInt32(row["product_line_id"]);
            product_type.ProductTypeName = DBValue.ToString(row["product_type_name"]);
            product_type.AdministrationSupply = DBValue.ToInt32(row["administration_supply"]);
            product_type.FulfillmentCharge = DBValue.ToDecimal(row["fulfillment_charge"]);
            if (row.Table.Columns.Contains("erp_product_type_id"))
            {
            product_type.ERPProductTypeID = DBValue.ToInt32(row["erp_product_type_id"]);
            }

            // return the filled object
            return product_type;
        }

        public List<ProductType> GetProduct_types()
        {
            return GetProductTypes(null);
        }

        private List<ProductType> GetProductTypes(SqlInterface si)
        {
            List <ProductType> product_types = new List<ProductType>();

            string storedProcName = "pr_get_product_types";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();


                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            product_types.Add(LoadProductType(dt.Rows[i]));
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return product_types;
        }


        public ProductType GetProductTypeByID(int id)
        {
            return GetProductTypeByID(id, null);
        }

        private ProductType GetProductTypeByID(int id, SqlInterface si)
        {
            ProductType product_type = null;

            string storedProcName = "pr_get_product_type_by_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Product_type_id", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        product_type = LoadProductType(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return product_type;
        }

        public ProductType GetProductTypeByCatalogItemID(int catalog_item_id)
        {
            return GetProductTypeByCatalogItemID(catalog_item_id,  null);
        }

        private ProductType GetProductTypeByCatalogItemID(int catalog_item_id, SqlInterface si)
        {
            ProductType product_type = null;
            string storedProcName = "es_get_product_type_by_catalog_item_id".ToString();

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Catalog_item_id", DbType.Int32, DBValue.ToDBInt32(catalog_item_id)));
               
                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        product_type = LoadProductType(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return product_type;
        }

        //GetProductTypeByCatalogItemID
        #endregion


        #region XCatalog

        private XCatalog loadXCatalog(DataRow row)
        {
            XCatalog xcatalog = new XCatalog();

            // Store database values into our business object
            xcatalog.XCatalogID = DBValue.ToInt32(row["x_catalog_id"]);
            xcatalog.CatalogName = DBValue.ToString(row["catalog_name"]);
            xcatalog.LanguageID = DBValue.ToInt32(row["language_id"]);
            xcatalog.HomepageOrder = DBValue.ToInt32(row["homepage_order"]);
            xcatalog.CreateDate = DBValue.ToDateTime(row["create_date"]);
            xcatalog.ModifyDate = DBValue.ToDateTime(row["modify_date"]);
            xcatalog.ModifiedBy = DBValue.ToString(row["modified_by"]);
            xcatalog.DeletedTF = DBValue.ToInt32(row["deleted_tf"]);
            xcatalog.XCatalogTypeID = DBValue.ToInt32(row["x_catalog_type_id"]);

            // return the filled object
            return xcatalog;
        }


        public XCatalog GetXCatalogByID(int id)
        {
            return GetXCatalogByID(id, null);
        }

        private XCatalog GetXCatalogByID(int id, SqlInterface si)
        {
            XCatalog xCatalog = null;

            string storedProcName = "es_get_x_catalog_by_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@X_Catalog_Id", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        xCatalog = loadXCatalog(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }

                return xCatalog;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            
        }


        #endregion

     
    }
}
