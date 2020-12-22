using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderExpress.DataGeneration
{
    public class Spring2011
    {
        #region Constants

        //const int CREATE_USER_ID = 100689;    // system user
        const int CREATE_USER_ID = 101935;    // Arun
        const string CONNECTION_STRING = "data source=qspdevdb1.qsp;initial catalog=QSPFulfillment;user id=QSPFormWebUser;password=Q$PFormW3bU53r;Connect Timeout=240;Application Name=OrderExpress;";

        // DEV
        // const string CONNECTION_STRING = "data source=qspdevdb1.qsp;initial catalog=QSPFulfillment;user id=QSPFormWebUser;password=QSPFormWebUser;Connect Timeout=240;Application Name=OrderExpress;";

        // TEST

        // STAGING

        // PROD
        //const string CONNECTION_STRING = "data source=qspproddb1.qsp;initial catalog=QSPFulfillment;user id=QSPFormWebUser;password=somewhereakinghasnowife;Connect Timeout=240;Application Name=OEDataGenerator;";

        #endregion

        #region Variables

        int otisBulk14OrderFormId = 0; // Otis/Mag combo PA uses same Order form.

        #endregion

        #region Spring 2011 : Lollipop

        public string AddLollipopSpring11FormBulk()
        {
            #region Start

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("start");

            // Open db context
            QSPFulfillmentDataContext context = new QSPFulfillmentDataContext(CONNECTION_STRING);
            sb.AppendLine("db context open");

            // Open the connection
            context.Connection.Open();

            #endregion

            try
            {
                #region Order form

                #region form_group

                // Create new form group object
                form_group newFormGroup = new form_group();

                // Fill in new form group data
                // newFormGroup.form_group_id;              // Autonumber generated on insert
                newFormGroup.form_group_name = "Lollipop Spring 2011 stock order form group";
                newFormGroup.deleted = false;
                newFormGroup.create_date = DateTime.Now;
                newFormGroup.create_user_id = CREATE_USER_ID;
                newFormGroup.update_date = DateTime.Now;
                newFormGroup.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.form_groups.InsertOnSubmit(newFormGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form group id = " + newFormGroup.form_group_id.ToString());
                // MessageBox.Show("New form group id = " + newFormGroup.form_group_id.ToString());

                #endregion

                #region form

                // Create new form object
                form newForm = new form();

                // Fill in new form data
                // newForm.form_id;                             // Autonumber generated on insert
                newForm.form_group_id = newFormGroup.form_group_id;
                newForm.entity_type_id = 4;                     // Type 4 = order
                newForm.form_code = "LollipopSpring2011";
                newForm.form_name = "Lollipop Spring 2011";
                newForm.description = "";
                newForm.order_terms_text = "";
                newForm.start_date = new DateTime(2010, 11, 17);
                newForm.end_date = new DateTime(2011, 6, 30);
                newForm.closing_time = new DateTime(2011, 6, 30);
                newForm.image_url = "images/CatalogItem/Lollipop.gif";
                newForm.is_base_form = false;
                newForm.parent_form_id = 8;                     // Base form for orders in prod and dev
                newForm.is_product_price_updatable = false;
                newForm.is_quantity_adjustment_allowed = true;
                newForm.tax_postal_address_type_id = 2;
                newForm.enabled = true;
                newForm.deleted = false;
                newForm.version = 1;
                //newForm.program_id = null;                    // Does not belong to Otis / Pine Valley
                newForm.program_type_id = 7;
                newForm.program_basics_text = "";
                newForm.create_date = DateTime.Now;
                newForm.create_user_id = CREATE_USER_ID;
                newForm.update_date = DateTime.Now;
                newForm.update_user_id = CREATE_USER_ID;
                //newForm.warehouse_type_id;
                newForm.is_bulk = false;
                newForm.report_name = "OrderForm";
                newForm.is_warehouse_selectable = true;
                newForm.default_warehouse_id = 35;
                newForm.season_id = 8;

                // Add the new form to the db context
                context.forms.InsertOnSubmit(newForm);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form id = " + newForm.form_id.ToString());
                // MessageBox.Show("New form id = " + newForm.form_id.ToString());

                #endregion

                #region form_permission

                form_permission newFormPermission1 = new form_permission();
                //newFormPermission1.form_permission_id;            // Autonumber generated on insert
                newFormPermission1.form_id = newForm.form_id;
                newFormPermission1.role_id = 0;                     // User
                newFormPermission1.allow_read = false;
                newFormPermission1.allow_write = false;
                newFormPermission1.create_date = DateTime.Now;
                newFormPermission1.create_user_id = CREATE_USER_ID;
                newFormPermission1.update_date = DateTime.Now;
                newFormPermission1.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission2 = new form_permission();
                //newFormPermission2.form_permission_id;            // Autonumber generated on insert
                newFormPermission2.form_id = newForm.form_id;
                newFormPermission2.role_id = 1;                     // Field Sale Manager (FM)
                newFormPermission2.allow_read = false;
                newFormPermission2.allow_write = false;
                newFormPermission2.create_date = DateTime.Now;
                newFormPermission2.create_user_id = CREATE_USER_ID;
                newFormPermission2.update_date = DateTime.Now;
                newFormPermission2.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission3 = new form_permission();
                //newFormPermission3.form_permission_id;            // Autonumber generated on insert
                newFormPermission3.form_id = newForm.form_id;
                newFormPermission3.role_id = 2;                     // Field Support
                newFormPermission3.allow_read = true;
                newFormPermission3.allow_write = true;
                newFormPermission3.create_date = DateTime.Now;
                newFormPermission3.create_user_id = CREATE_USER_ID;
                newFormPermission3.update_date = DateTime.Now;
                newFormPermission3.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission4 = new form_permission();
                //newFormPermission4.form_permission_id;            // Autonumber generated on insert
                newFormPermission4.form_id = newForm.form_id;
                newFormPermission4.role_id = 3;                     // Accounting Manager
                newFormPermission4.allow_read = true;
                newFormPermission4.allow_write = true;
                newFormPermission4.create_date = DateTime.Now;
                newFormPermission4.create_user_id = CREATE_USER_ID;
                newFormPermission4.update_date = DateTime.Now;
                newFormPermission4.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission5 = new form_permission();
                //newFormPermission5.form_permission_id;            // Autonumber generated on insert
                newFormPermission5.form_id = newForm.form_id;
                newFormPermission5.role_id = 4;                     // Admin
                newFormPermission5.allow_read = true;
                newFormPermission5.allow_write = true;
                newFormPermission5.create_date = DateTime.Now;
                newFormPermission5.create_user_id = CREATE_USER_ID;
                newFormPermission5.update_date = DateTime.Now;
                newFormPermission5.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission6 = new form_permission();
                //newFormPermission6.form_permission_id;            // Autonumber generated on insert
                newFormPermission6.form_id = newForm.form_id;
                newFormPermission6.role_id = 5;                     // Super User
                newFormPermission6.allow_read = true;
                newFormPermission6.allow_write = true;
                newFormPermission6.create_date = DateTime.Now;
                newFormPermission6.create_user_id = CREATE_USER_ID;
                newFormPermission6.update_date = DateTime.Now;
                newFormPermission6.update_user_id = CREATE_USER_ID;

                // Add the new form permissions to the db context
                context.form_permissions.InsertOnSubmit(newFormPermission1);
                context.form_permissions.InsertOnSubmit(newFormPermission2);
                context.form_permissions.InsertOnSubmit(newFormPermission3);
                context.form_permissions.InsertOnSubmit(newFormPermission4);
                context.form_permissions.InsertOnSubmit(newFormPermission5);
                context.form_permissions.InsertOnSubmit(newFormPermission6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form permissions ok");

                #endregion

                #region form_delivery_date_type

                form_delivery_date_type newFormDeliveryDateType1 = new form_delivery_date_type();
                //newFormDeliveryDateType1.form_delivery_date_type_id;  // Autonumber generated on insert
                newFormDeliveryDateType1.form_id = newForm.form_id;
                newFormDeliveryDateType1.delivery_date_type_id = 1;     // Choose a date

                context.form_delivery_date_types.InsertOnSubmit(newFormDeliveryDateType1);
                context.SubmitChanges();

                sb.AppendLine("form delivery date type ok");

                #endregion

                #region form_profit_rate

                form_profit_rate newFormProfitRate1 = new form_profit_rate();
                //newFormProfitRate1.form_profit_rate_id;                // Autonumber generated on insert
                newFormProfitRate1.form_id = newForm.form_id;
                newFormProfitRate1.profit_rate_id = 3;         // 1 = 40%, 2 = 45%, 3 = 50%
                newFormProfitRate1.deleted = false;
                newFormProfitRate1.create_date = DateTime.Now;
                newFormProfitRate1.create_user_id = CREATE_USER_ID;
                newFormProfitRate1.update_date = DateTime.Now;
                newFormProfitRate1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_profit_rates.InsertOnSubmit(newFormProfitRate1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form profit rate ok");

                #endregion

                #region form_order_type

                // Create new form order type object
                form_order_type newFormOrderType1 = new form_order_type();
                // newFormOrderType1.form_order_type_id;            // Autonumber generated on insert
                newFormOrderType1.form_id = newForm.form_id;
                newFormOrderType1.order_type_id = 1;                // 1 = standard order
                newFormOrderType1.deleted = false;
                newFormOrderType1.create_date = DateTime.Now;
                newFormOrderType1.create_user_id = CREATE_USER_ID;
                newFormOrderType1.update_date = DateTime.Now;
                newFormOrderType1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_order_types.InsertOnSubmit(newFormOrderType1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form order type ok");

                #endregion

                #region form_delivery_method

                // Create new record
                form_delivery_method newFormDeliveryMethod1 = new form_delivery_method();
                // newFormDeliveryMethod1.form_delivery_method_id;          // Autonumber generated on insert
                newFormDeliveryMethod1.delivery_method_id = 1;              // Common carrier
                newFormDeliveryMethod1.form_id = newForm.form_id;
                newFormDeliveryMethod1.deleted = false;
                newFormDeliveryMethod1.create_date = DateTime.Now;
                newFormDeliveryMethod1.create_user_id = CREATE_USER_ID;
                newFormDeliveryMethod1.update_date = DateTime.Now;
                newFormDeliveryMethod1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_delivery_methods.InsertOnSubmit(newFormDeliveryMethod1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form delivery methods ok");

                #endregion

                #region catalog_group

                // Create new record
                catalog_group newCatalogGroup = new catalog_group();
                // newCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newCatalogGroup.catalog_group_code = "LollipopSpring2011";
                newCatalogGroup.catalog_group_name = "Lollipop Spring 2011 order form";
                newCatalogGroup.description = "";
                newCatalogGroup.deleted = false;
                newCatalogGroup.create_date = DateTime.Now;
                newCatalogGroup.create_user_id = CREATE_USER_ID;
                newCatalogGroup.update_date = DateTime.Now;
                newCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region catalog

                // Create new record
                catalog newCatalog = new catalog();
                // newCatalog.newCatalog_id;          // Autonumber generated on insert
                newCatalog.catalog_group_id = newCatalogGroup.catalog_group_id;
                newCatalog.catalog_code = "LollipopSpring2011";
                newCatalog.catalog_name = "Lollipop Spring 2011 order form";
                newCatalog.culture = "en-US";
                newCatalog.description = "";
                newCatalog.start_date = new DateTime(2010, 11, 17);
                newCatalog.end_date = new DateTime(2011, 6, 30);
                newCatalog.deleted = false;
                newCatalog.create_date = DateTime.Now;
                newCatalog.create_user_id = CREATE_USER_ID;
                newCatalog.update_date = DateTime.Now;
                newCatalog.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newCatalog);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newCatalog.catalog_id.ToString());

                #endregion

                #region catalog_item_category

                // Create new record
                catalog_item_category newCatalogItemCategory1 = new catalog_item_category();
                // newCatalogItemCategory1.catalog_item_category_id;         // Autonumber generated on insert
                newCatalogItemCategory1.catalog_id = newCatalog.catalog_id;
                newCatalogItemCategory1.catalog_item_category_name = "Lollipop Spring 2011";
                newCatalogItemCategory1.parent_catalog_item_category_id = 0;
                newCatalogItemCategory1.deleted = false;
                newCatalogItemCategory1.create_date = DateTime.Now;
                newCatalogItemCategory1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategory1.update_date = DateTime.Now;
                newCatalogItemCategory1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newCatalogItemCategory1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newCatalogItemCategory1.catalog_item_category_id.ToString());

                #endregion

                #region form_section

                // Create new record
                form_section newFormSection1 = new form_section();
                // newFormSection1.form_section_id;              // Autonumber generated on insert
                newFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newFormSection1.form_id = newForm.form_id;
                newFormSection1.form_section_type_id = 1;        // 1 = standard product, 2 = supply product, 3 = optional product
                newFormSection1.form_section_number = 1;
                newFormSection1.form_section_title = "Lollipop Spring 2011";
                newFormSection1.description = "";
                newFormSection1.deleted = false;
                newFormSection1.create_date = DateTime.Now;
                newFormSection1.create_user_id = CREATE_USER_ID;
                newFormSection1.update_date = DateTime.Now;
                newFormSection1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newFormSection1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newFormSection1.form_section_id.ToString());

                #endregion

                #region product

                #region Create new record

                product newProduct1 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct1.product_code = "22365";
                newProduct1.product_name = "Yummy Lix";
                newProduct1.price = Convert.ToDecimal(0.00);
                newProduct1.nb_units = 576;
                newProduct1.nb_day_lead_time = 10;
                newProduct1.product_status_id = 101;            // 101 = Active
                newProduct1.product_type_id = 5;                // 5 = Candies
                newProduct1.business_division_id = 1;           // 1 = US
                newProduct1.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct1.coupon_id = null;
                newProduct1.description = "";
                newProduct1.image_url = "";
                newProduct1.is_free_sample = false;
                newProduct1.oracle_code = "";
                newProduct1.IVCOUP = "";
                newProduct1.IVITEM = "22365";
                newProduct1.unit_cost = Convert.ToDecimal(0.00);
                newProduct1.vendor_id = 30;
                newProduct1.vendor_item_code = "631552 022365";
                newProduct1.deleted = false;
                newProduct1.create_date = DateTime.Now;
                newProduct1.create_user_id = CREATE_USER_ID;
                newProduct1.update_date = DateTime.Now;
                newProduct1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct2 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct2.product_code = "22237";
                newProduct2.product_name = "Color Xploder";
                newProduct2.price = Convert.ToDecimal(0.00);
                newProduct2.nb_units = 576;
                newProduct2.nb_day_lead_time = 10;
                newProduct2.product_status_id = 101;            // 101 = Active
                newProduct2.product_type_id = 5;                // 5 = Candies
                newProduct2.business_division_id = 1;           // 1 = US
                newProduct2.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct2.coupon_id = null;
                newProduct2.description = "";
                newProduct2.image_url = "";
                newProduct2.is_free_sample = false;
                newProduct2.oracle_code = "";
                newProduct2.IVCOUP = "";
                newProduct2.IVITEM = "22237";
                newProduct2.unit_cost = Convert.ToDecimal(0.00);
                newProduct2.vendor_id = 30;
                newProduct2.vendor_item_code = "02237";
                newProduct2.deleted = false;
                newProduct2.create_date = DateTime.Now;
                newProduct2.create_user_id = CREATE_USER_ID;
                newProduct2.update_date = DateTime.Now;
                newProduct2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct3 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct3.product_code = "22389";
                newProduct3.product_name = "HEART SHAPE POP 1 OZ";
                newProduct3.price = Convert.ToDecimal(0.00);
                newProduct3.nb_units = 576;
                newProduct3.nb_day_lead_time = 10;
                newProduct3.product_status_id = 101;            // 101 = Active
                newProduct3.product_type_id = 5;                // 5 = Candies
                newProduct3.business_division_id = 1;           // 1 = US
                newProduct3.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct3.coupon_id = null;
                newProduct3.description = "";
                newProduct3.image_url = "";
                newProduct3.is_free_sample = false;
                newProduct3.oracle_code = "";
                newProduct3.IVCOUP = "";
                newProduct3.IVITEM = "22389";
                newProduct3.unit_cost = Convert.ToDecimal(0.00);
                newProduct3.vendor_id = 30;
                newProduct3.vendor_item_code = "022389";
                newProduct3.deleted = false;
                newProduct3.create_date = DateTime.Now;
                newProduct3.create_user_id = CREATE_USER_ID;
                newProduct3.update_date = DateTime.Now;
                newProduct3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct4 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct4.product_code = "22396";
                newProduct4.product_name = "SWEET YUMMY LIPS";
                newProduct4.price = Convert.ToDecimal(0.00);
                newProduct4.nb_units = 576;
                newProduct4.nb_day_lead_time = 10;
                newProduct4.product_status_id = 101;            // 101 = Active
                newProduct4.product_type_id = 5;                // 5 = Candies
                newProduct4.business_division_id = 1;           // 1 = US
                newProduct4.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct4.coupon_id = null;
                newProduct4.description = "";
                newProduct4.image_url = "";
                newProduct4.is_free_sample = false;
                newProduct4.oracle_code = "";
                newProduct4.IVCOUP = "";
                newProduct4.IVITEM = "22396";
                newProduct4.unit_cost = Convert.ToDecimal(0.00);
                newProduct4.vendor_id = 30;
                newProduct4.vendor_item_code = "022396";
                newProduct4.deleted = false;
                newProduct4.create_date = DateTime.Now;
                newProduct4.create_user_id = CREATE_USER_ID;
                newProduct4.update_date = DateTime.Now;
                newProduct4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct5 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct5.product_code = "22402";
                newProduct5.product_name = "SOUR YUMMY LIPS";
                newProduct5.price = Convert.ToDecimal(0.00);
                newProduct5.nb_units = 576;
                newProduct5.nb_day_lead_time = 10;
                newProduct5.product_status_id = 101;            // 101 = Active
                newProduct5.product_type_id = 5;                // 5 = Candies
                newProduct5.business_division_id = 1;           // 1 = US
                newProduct5.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct5.coupon_id = null;
                newProduct5.description = "";
                newProduct5.image_url = "";
                newProduct5.is_free_sample = false;
                newProduct5.oracle_code = "";
                newProduct5.IVCOUP = "";
                newProduct5.IVITEM = "22402";
                newProduct5.unit_cost = Convert.ToDecimal(0.00);
                newProduct5.vendor_id = 30;
                newProduct5.vendor_item_code = "022402";
                newProduct5.deleted = false;
                newProduct5.create_date = DateTime.Now;
                newProduct5.create_user_id = CREATE_USER_ID;
                newProduct5.update_date = DateTime.Now;
                newProduct5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct6 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct6.product_code = "22419";
                newProduct6.product_name = "MILKY POP";
                newProduct6.price = Convert.ToDecimal(0.00);
                newProduct6.nb_units = 576;
                newProduct6.nb_day_lead_time = 10;
                newProduct6.product_status_id = 101;            // 101 = Active
                newProduct6.product_type_id = 5;                // 5 = Candies
                newProduct6.business_division_id = 1;           // 1 = US
                newProduct6.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct6.coupon_id = null;
                newProduct6.description = "";
                newProduct6.image_url = "";
                newProduct6.is_free_sample = false;
                newProduct6.oracle_code = "";
                newProduct6.IVCOUP = "";
                newProduct6.IVITEM = "22419";
                newProduct6.unit_cost = Convert.ToDecimal(0.00);
                newProduct6.vendor_id = 30;
                newProduct6.vendor_item_code = "022419";
                newProduct6.deleted = false;
                newProduct6.create_date = DateTime.Now;
                newProduct6.create_user_id = CREATE_USER_ID;
                newProduct6.update_date = DateTime.Now;
                newProduct6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct7 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct7.product_code = "22426";
                newProduct7.product_name = "1OZ SOUR MANIA";
                newProduct7.price = Convert.ToDecimal(0.00);
                newProduct7.nb_units = 576;
                newProduct7.nb_day_lead_time = 10;
                newProduct7.product_status_id = 101;            // 101 = Active
                newProduct7.product_type_id = 5;                // 5 = Candies
                newProduct7.business_division_id = 1;           // 1 = US
                newProduct7.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct7.coupon_id = null;
                newProduct7.description = "";
                newProduct7.image_url = "";
                newProduct7.is_free_sample = false;
                newProduct7.oracle_code = "";
                newProduct7.IVCOUP = "";
                newProduct7.IVITEM = "22426";
                newProduct7.unit_cost = Convert.ToDecimal(0.00);
                newProduct7.vendor_id = 30;
                newProduct7.vendor_item_code = "022426";
                newProduct7.deleted = false;
                newProduct7.create_date = DateTime.Now;
                newProduct7.create_user_id = CREATE_USER_ID;
                newProduct7.update_date = DateTime.Now;
                newProduct7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct8 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct8.product_code = "22501";
                newProduct8.product_name = "LOLLIBALL LOLLIPOPS";
                newProduct8.price = Convert.ToDecimal(0.00);
                newProduct8.nb_units = 576;
                newProduct8.nb_day_lead_time = 10;
                newProduct8.product_status_id = 101;            // 101 = Active
                newProduct8.product_type_id = 5;                // 5 = Candies
                newProduct8.business_division_id = 1;           // 1 = US
                newProduct8.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct8.coupon_id = null;
                newProduct8.description = "";
                newProduct8.image_url = "";
                newProduct8.is_free_sample = false;
                newProduct8.oracle_code = "";
                newProduct8.IVCOUP = "";
                newProduct8.IVITEM = "22501";
                newProduct8.unit_cost = Convert.ToDecimal(0.00);
                newProduct8.vendor_id = 30;
                newProduct8.vendor_item_code = "022501";
                newProduct8.deleted = false;
                newProduct8.create_date = DateTime.Now;
                newProduct8.create_user_id = CREATE_USER_ID;
                newProduct8.update_date = DateTime.Now;
                newProduct8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct9 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct9.product_code = "22518";
                newProduct9.product_name = "EASTER JOY LOLLIPOPS";
                newProduct9.price = Convert.ToDecimal(0.00);
                newProduct9.nb_units = 576;
                newProduct9.nb_day_lead_time = 10;
                newProduct9.product_status_id = 101;            // 101 = Active
                newProduct9.product_type_id = 5;                // 5 = Candies
                newProduct9.business_division_id = 1;           // 1 = US
                newProduct9.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct9.coupon_id = null;
                newProduct9.description = "";
                newProduct9.image_url = "";
                newProduct9.is_free_sample = false;
                newProduct9.oracle_code = "";
                newProduct9.IVCOUP = "";
                newProduct9.IVITEM = "22518";
                newProduct9.unit_cost = Convert.ToDecimal(0.00);
                newProduct9.vendor_id = 30;
                newProduct9.vendor_item_code = "022518";
                newProduct9.deleted = false;
                newProduct9.create_date = DateTime.Now;
                newProduct9.create_user_id = CREATE_USER_ID;
                newProduct9.update_date = DateTime.Now;
                newProduct9.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct10 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct10.product_code = "22525";
                newProduct10.product_name = "MERRY RAINBBOW LOLLIPOPS";
                newProduct10.price = Convert.ToDecimal(0.00);
                newProduct10.nb_units = 576;
                newProduct10.nb_day_lead_time = 10;
                newProduct10.product_status_id = 101;            // 101 = Active
                newProduct10.product_type_id = 5;                // 5 = Candies
                newProduct10.business_division_id = 1;           // 1 = US
                newProduct10.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct10.coupon_id = null;
                newProduct10.description = "";
                newProduct10.image_url = "";
                newProduct10.is_free_sample = false;
                newProduct10.oracle_code = "";
                newProduct10.IVCOUP = "";
                newProduct10.IVITEM = "22525";
                newProduct10.unit_cost = Convert.ToDecimal(0.00);
                newProduct10.vendor_id = 30;
                newProduct10.vendor_item_code = "022525";
                newProduct10.deleted = false;
                newProduct10.create_date = DateTime.Now;
                newProduct10.create_user_id = CREATE_USER_ID;
                newProduct10.update_date = DateTime.Now;
                newProduct10.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct11 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct11.product_code = "22253";
                newProduct11.product_name = "FAITH RELIGIOUS LOLLIPOPS";
                newProduct11.price = Convert.ToDecimal(0.00);
                newProduct11.nb_units = 576;
                newProduct11.nb_day_lead_time = 10;
                newProduct11.product_status_id = 101;            // 101 = Active
                newProduct11.product_type_id = 5;                // 5 = Candies
                newProduct11.business_division_id = 1;           // 1 = US
                newProduct11.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct11.coupon_id = null;
                newProduct11.description = "";
                newProduct11.image_url = "";
                newProduct11.is_free_sample = false;
                newProduct11.oracle_code = "";
                newProduct11.IVCOUP = "";
                newProduct11.IVITEM = "22253";
                newProduct11.unit_cost = Convert.ToDecimal(0.00);
                newProduct11.vendor_id = 30;
                newProduct11.vendor_item_code = "02253";
                newProduct11.deleted = false;
                newProduct11.create_date = DateTime.Now;
                newProduct11.create_user_id = CREATE_USER_ID;
                newProduct11.update_date = DateTime.Now;
                newProduct11.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct12 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct12.product_code = "22532";
                newProduct12.product_name = "LOLLIWEEN LOLLIPOPS";
                newProduct12.price = Convert.ToDecimal(0.00);
                newProduct12.nb_units = 576;
                newProduct12.nb_day_lead_time = 10;
                newProduct12.product_status_id = 101;            // 101 = Active
                newProduct12.product_type_id = 5;                // 5 = Candies
                newProduct12.business_division_id = 1;           // 1 = US
                newProduct12.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct12.coupon_id = null;
                newProduct12.description = "";
                newProduct12.image_url = "";
                newProduct12.is_free_sample = false;
                newProduct12.oracle_code = "";
                newProduct12.IVCOUP = "";
                newProduct12.IVITEM = "22532";
                newProduct12.unit_cost = Convert.ToDecimal(0.00);
                newProduct12.vendor_id = 30;
                newProduct12.vendor_item_code = "022532";
                newProduct12.deleted = false;
                newProduct12.create_date = DateTime.Now;
                newProduct12.create_user_id = CREATE_USER_ID;
                newProduct12.update_date = DateTime.Now;
                newProduct12.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct13 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct13.product_code = "22257";
                newProduct13.product_name = "HOT POPS";
                newProduct13.price = Convert.ToDecimal(0.00);
                newProduct13.nb_units = 576;
                newProduct13.nb_day_lead_time = 10;
                newProduct13.product_status_id = 101;            // 101 = Active
                newProduct13.product_type_id = 5;                // 5 = Candies
                newProduct13.business_division_id = 1;           // 1 = US
                newProduct13.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct13.coupon_id = null;
                newProduct13.description = "";
                newProduct13.image_url = "";
                newProduct13.is_free_sample = false;
                newProduct13.oracle_code = "";
                newProduct13.IVCOUP = "";
                newProduct13.IVITEM = "22257";
                newProduct13.unit_cost = Convert.ToDecimal(0.00);
                newProduct13.vendor_id = 30;
                newProduct13.vendor_item_code = "02257";
                newProduct13.deleted = false;
                newProduct13.create_date = DateTime.Now;
                newProduct13.create_user_id = CREATE_USER_ID;
                newProduct13.update_date = DateTime.Now;
                newProduct13.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct14 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct14.product_code = "22258";
                newProduct14.product_name = "SUGAR FREE ASSORTED";
                newProduct14.price = Convert.ToDecimal(0.00);
                newProduct14.nb_units = 640;
                newProduct14.nb_day_lead_time = 10;
                newProduct14.product_status_id = 101;            // 101 = Active
                newProduct14.product_type_id = 5;                // 5 = Candies
                newProduct14.business_division_id = 1;           // 1 = US
                newProduct14.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct14.coupon_id = null;
                newProduct14.description = "";
                newProduct14.image_url = "";
                newProduct14.is_free_sample = false;
                newProduct14.oracle_code = "";
                newProduct14.IVCOUP = "";
                newProduct14.IVITEM = "22258";
                newProduct14.unit_cost = Convert.ToDecimal(0.00);
                newProduct14.vendor_id = 30;
                newProduct14.vendor_item_code = "02258";
                newProduct14.deleted = false;
                newProduct14.create_date = DateTime.Now;
                newProduct14.create_user_id = CREATE_USER_ID;
                newProduct14.update_date = DateTime.Now;
                newProduct14.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct15 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct15.product_code = "22631";
                newProduct15.product_name = "BASEBALL POPS";
                newProduct15.price = Convert.ToDecimal(0.00);
                newProduct15.nb_units = 576;
                newProduct15.nb_day_lead_time = 10;
                newProduct15.product_status_id = 101;            // 101 = Active
                newProduct15.product_type_id = 5;                // 5 = Candies
                newProduct15.business_division_id = 1;           // 1 = US
                newProduct15.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct15.coupon_id = null;
                newProduct15.description = "";
                newProduct15.image_url = "";
                newProduct15.is_free_sample = false;
                newProduct15.oracle_code = "";
                newProduct15.IVCOUP = "";
                newProduct15.IVITEM = "22631";
                newProduct15.unit_cost = Convert.ToDecimal(0.00);
                newProduct15.vendor_id = 30;
                newProduct15.vendor_item_code = "631552 022600";
                newProduct15.deleted = false;
                newProduct15.create_date = DateTime.Now;
                newProduct15.create_user_id = CREATE_USER_ID;
                newProduct15.update_date = DateTime.Now;
                newProduct15.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.products.InsertOnSubmit(newProduct1);
                context.products.InsertOnSubmit(newProduct2);
                context.products.InsertOnSubmit(newProduct3);
                context.products.InsertOnSubmit(newProduct4);
                context.products.InsertOnSubmit(newProduct5);
                context.products.InsertOnSubmit(newProduct6);
                context.products.InsertOnSubmit(newProduct7);
                context.products.InsertOnSubmit(newProduct8);
                context.products.InsertOnSubmit(newProduct9);
                context.products.InsertOnSubmit(newProduct10);
                context.products.InsertOnSubmit(newProduct11);
                context.products.InsertOnSubmit(newProduct12);
                context.products.InsertOnSubmit(newProduct13);
                context.products.InsertOnSubmit(newProduct14);
                context.products.InsertOnSubmit(newProduct15);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("products ok");

                #endregion

                #region catalog_item

                #region Create new record

                catalog_item newCatalogItem1 = new catalog_item();
                // newCatalogItem1.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem1.product_id = newProduct1.product_id;
                newCatalogItem1.catalog_id = newCatalog.catalog_id;
                newCatalogItem1.catalog_item_code = "22365";
                newCatalogItem1.catalog_item_name = "YUMMY LIX";
                newCatalogItem1.price = Convert.ToDecimal(0.00);
                newCatalogItem1.nb_units = 576;
                newCatalogItem1.image_url = "1";
                newCatalogItem1.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem1.catalog_item_export_name = "";
                newCatalogItem1.description = "";
                newCatalogItem1.deleted = false;
                newCatalogItem1.create_date = DateTime.Now;
                newCatalogItem1.create_user_id = CREATE_USER_ID;
                newCatalogItem1.update_date = DateTime.Now;
                newCatalogItem1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem2 = new catalog_item();
                // newCatalogItem2.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem2.product_id = newProduct2.product_id;
                newCatalogItem2.catalog_id = newCatalog.catalog_id;
                newCatalogItem2.catalog_item_code = "22237";
                newCatalogItem2.catalog_item_name = "COLOR XPLODER";
                newCatalogItem2.price = Convert.ToDecimal(0.00);
                newCatalogItem2.nb_units = 576;
                newCatalogItem2.image_url = "1";
                newCatalogItem2.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem2.catalog_item_export_name = "";
                newCatalogItem2.description = "";
                newCatalogItem2.deleted = false;
                newCatalogItem2.create_date = DateTime.Now;
                newCatalogItem2.create_user_id = CREATE_USER_ID;
                newCatalogItem2.update_date = DateTime.Now;
                newCatalogItem2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem3 = new catalog_item();
                // newCatalogItem3.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem3.product_id = newProduct3.product_id;
                newCatalogItem3.catalog_id = newCatalog.catalog_id;
                newCatalogItem3.catalog_item_code = "22389";
                newCatalogItem3.catalog_item_name = "HEART SHAPE POP 1 OZ";
                newCatalogItem3.price = Convert.ToDecimal(0.00);
                newCatalogItem3.nb_units = 576;
                newCatalogItem3.image_url = "1";
                newCatalogItem3.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem3.catalog_item_export_name = "";
                newCatalogItem3.description = "";
                newCatalogItem3.deleted = false;
                newCatalogItem3.create_date = DateTime.Now;
                newCatalogItem3.create_user_id = CREATE_USER_ID;
                newCatalogItem3.update_date = DateTime.Now;
                newCatalogItem3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem4 = new catalog_item();
                // newCatalogItem4.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem4.product_id = newProduct4.product_id;
                newCatalogItem4.catalog_id = newCatalog.catalog_id;
                newCatalogItem4.catalog_item_code = "22396";
                newCatalogItem4.catalog_item_name = "SWEET YUMMY LIPS";
                newCatalogItem4.price = Convert.ToDecimal(0.00);
                newCatalogItem4.nb_units = 576;
                newCatalogItem4.image_url = "1";
                newCatalogItem4.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem4.catalog_item_export_name = "";
                newCatalogItem4.description = "";
                newCatalogItem4.deleted = false;
                newCatalogItem4.create_date = DateTime.Now;
                newCatalogItem4.create_user_id = CREATE_USER_ID;
                newCatalogItem4.update_date = DateTime.Now;
                newCatalogItem4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem5 = new catalog_item();
                // newCatalogItem5.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem5.product_id = newProduct5.product_id;
                newCatalogItem5.catalog_id = newCatalog.catalog_id;
                newCatalogItem5.catalog_item_code = "22402";
                newCatalogItem5.catalog_item_name = "SOUR YUMMY LIPS";
                newCatalogItem5.price = Convert.ToDecimal(0.00);
                newCatalogItem5.nb_units = 576;
                newCatalogItem5.image_url = "1";
                newCatalogItem5.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem5.catalog_item_export_name = "";
                newCatalogItem5.description = "";
                newCatalogItem5.deleted = false;
                newCatalogItem5.create_date = DateTime.Now;
                newCatalogItem5.create_user_id = CREATE_USER_ID;
                newCatalogItem5.update_date = DateTime.Now;
                newCatalogItem5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem6 = new catalog_item();
                // newCatalogItem6.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem6.product_id = newProduct6.product_id;
                newCatalogItem6.catalog_id = newCatalog.catalog_id;
                newCatalogItem6.catalog_item_code = "22419";
                newCatalogItem6.catalog_item_name = "MILKY POP";
                newCatalogItem6.price = Convert.ToDecimal(0.00);
                newCatalogItem6.nb_units = 576;
                newCatalogItem6.image_url = "1";
                newCatalogItem6.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem6.catalog_item_export_name = "";
                newCatalogItem6.description = "";
                newCatalogItem6.deleted = false;
                newCatalogItem6.create_date = DateTime.Now;
                newCatalogItem6.create_user_id = CREATE_USER_ID;
                newCatalogItem6.update_date = DateTime.Now;
                newCatalogItem6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem7 = new catalog_item();
                // newCatalogItem7.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem7.product_id = newProduct7.product_id;
                newCatalogItem7.catalog_id = newCatalog.catalog_id;
                newCatalogItem7.catalog_item_code = "22426";
                newCatalogItem7.catalog_item_name = "1OZ SOUR MANIA";
                newCatalogItem7.price = Convert.ToDecimal(0.00);
                newCatalogItem7.nb_units = 576;
                newCatalogItem7.image_url = "1";
                newCatalogItem7.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem7.catalog_item_export_name = "";
                newCatalogItem7.description = "";
                newCatalogItem7.deleted = false;
                newCatalogItem7.create_date = DateTime.Now;
                newCatalogItem7.create_user_id = CREATE_USER_ID;
                newCatalogItem7.update_date = DateTime.Now;
                newCatalogItem7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem8 = new catalog_item();
                // newCatalogItem8.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem8.product_id = newProduct8.product_id;
                newCatalogItem8.catalog_id = newCatalog.catalog_id;
                newCatalogItem8.catalog_item_code = "22501";
                newCatalogItem8.catalog_item_name = "LOLLIBALL LOLLIPOPS";
                newCatalogItem8.price = Convert.ToDecimal(0.00);
                newCatalogItem8.nb_units = 576;
                newCatalogItem8.image_url = "1";
                newCatalogItem8.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem8.catalog_item_export_name = "";
                newCatalogItem8.description = "";
                newCatalogItem8.deleted = false;
                newCatalogItem8.create_date = DateTime.Now;
                newCatalogItem8.create_user_id = CREATE_USER_ID;
                newCatalogItem8.update_date = DateTime.Now;
                newCatalogItem8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem9 = new catalog_item();
                // newCatalogItem9.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem9.product_id = newProduct9.product_id;
                newCatalogItem9.catalog_id = newCatalog.catalog_id;
                newCatalogItem9.catalog_item_code = "22518";
                newCatalogItem9.catalog_item_name = "EASTER JOY LOLLIPOPS";
                newCatalogItem9.price = Convert.ToDecimal(0.00);
                newCatalogItem9.nb_units = 576;
                newCatalogItem9.image_url = "1";
                newCatalogItem9.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem9.catalog_item_export_name = "";
                newCatalogItem9.description = "";
                newCatalogItem9.deleted = false;
                newCatalogItem9.create_date = DateTime.Now;
                newCatalogItem9.create_user_id = CREATE_USER_ID;
                newCatalogItem9.update_date = DateTime.Now;
                newCatalogItem9.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem10 = new catalog_item();
                // newCatalogItem10.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem10.product_id = newProduct10.product_id;
                newCatalogItem10.catalog_id = newCatalog.catalog_id;
                newCatalogItem10.catalog_item_code = "22525";
                newCatalogItem10.catalog_item_name = "MERRY RAINBBOW LOLLIPOPS";
                newCatalogItem10.price = Convert.ToDecimal(0.00);
                newCatalogItem10.nb_units = 576;
                newCatalogItem10.image_url = "1";
                newCatalogItem10.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem10.catalog_item_export_name = "";
                newCatalogItem10.description = "";
                newCatalogItem10.deleted = false;
                newCatalogItem10.create_date = DateTime.Now;
                newCatalogItem10.create_user_id = CREATE_USER_ID;
                newCatalogItem10.update_date = DateTime.Now;
                newCatalogItem10.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem11 = new catalog_item();
                // newCatalogItem11.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem11.product_id = newProduct11.product_id;
                newCatalogItem11.catalog_id = newCatalog.catalog_id;
                newCatalogItem11.catalog_item_code = "22253";
                newCatalogItem11.catalog_item_name = "FAITH RELIGIOUS LOLLIPOPS";
                newCatalogItem11.price = Convert.ToDecimal(0.00);
                newCatalogItem11.nb_units = 576;
                newCatalogItem11.image_url = "1";
                newCatalogItem11.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem11.catalog_item_export_name = "";
                newCatalogItem11.description = "";
                newCatalogItem11.deleted = false;
                newCatalogItem11.create_date = DateTime.Now;
                newCatalogItem11.create_user_id = CREATE_USER_ID;
                newCatalogItem11.update_date = DateTime.Now;
                newCatalogItem11.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem12 = new catalog_item();
                // newCatalogItem12.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem12.product_id = newProduct12.product_id;
                newCatalogItem12.catalog_id = newCatalog.catalog_id;
                newCatalogItem12.catalog_item_code = "22532";
                newCatalogItem12.catalog_item_name = "LOLLIWEEN LOLLIPOPS";
                newCatalogItem12.price = Convert.ToDecimal(0.00);
                newCatalogItem12.nb_units = 576;
                newCatalogItem12.image_url = "1";
                newCatalogItem12.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem12.catalog_item_export_name = "";
                newCatalogItem12.description = "";
                newCatalogItem12.deleted = false;
                newCatalogItem12.create_date = DateTime.Now;
                newCatalogItem12.create_user_id = CREATE_USER_ID;
                newCatalogItem12.update_date = DateTime.Now;
                newCatalogItem12.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem13 = new catalog_item();
                // newCatalogItem13.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem13.product_id = newProduct13.product_id;
                newCatalogItem13.catalog_id = newCatalog.catalog_id;
                newCatalogItem13.catalog_item_code = "22257";
                newCatalogItem13.catalog_item_name = "HOT POPS";
                newCatalogItem13.price = Convert.ToDecimal(0.00);
                newCatalogItem13.nb_units = 576;
                newCatalogItem13.image_url = "1";
                newCatalogItem13.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem13.catalog_item_export_name = "";
                newCatalogItem13.description = "";
                newCatalogItem13.deleted = false;
                newCatalogItem13.create_date = DateTime.Now;
                newCatalogItem13.create_user_id = CREATE_USER_ID;
                newCatalogItem13.update_date = DateTime.Now;
                newCatalogItem13.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem14 = new catalog_item();
                // newCatalogItem14.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem14.product_id = newProduct14.product_id;
                newCatalogItem14.catalog_id = newCatalog.catalog_id;
                newCatalogItem14.catalog_item_code = "22258";
                newCatalogItem14.catalog_item_name = "SUGAR FREE ASSORTED";
                newCatalogItem14.price = Convert.ToDecimal(0.00);
                newCatalogItem14.nb_units = 640;
                newCatalogItem14.image_url = "1";
                newCatalogItem14.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem14.catalog_item_export_name = "";
                newCatalogItem14.description = "";
                newCatalogItem14.deleted = false;
                newCatalogItem14.create_date = DateTime.Now;
                newCatalogItem14.create_user_id = CREATE_USER_ID;
                newCatalogItem14.update_date = DateTime.Now;
                newCatalogItem14.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem15 = new catalog_item();
                // newCatalogItem15.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem15.product_id = newProduct15.product_id;
                newCatalogItem15.catalog_id = newCatalog.catalog_id;
                newCatalogItem15.catalog_item_code = "22631";
                newCatalogItem15.catalog_item_name = "BASEBALL POPS";
                newCatalogItem15.price = Convert.ToDecimal(0.00);
                newCatalogItem15.nb_units = 576;
                newCatalogItem15.image_url = "1";
                newCatalogItem15.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem15.catalog_item_export_name = "";
                newCatalogItem15.description = "";
                newCatalogItem15.deleted = false;
                newCatalogItem15.create_date = DateTime.Now;
                newCatalogItem15.create_user_id = CREATE_USER_ID;
                newCatalogItem15.update_date = DateTime.Now;
                newCatalogItem15.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newCatalogItem1);
                context.catalog_items.InsertOnSubmit(newCatalogItem2);
                context.catalog_items.InsertOnSubmit(newCatalogItem3);
                context.catalog_items.InsertOnSubmit(newCatalogItem4);
                context.catalog_items.InsertOnSubmit(newCatalogItem5);
                context.catalog_items.InsertOnSubmit(newCatalogItem6);
                context.catalog_items.InsertOnSubmit(newCatalogItem7);
                context.catalog_items.InsertOnSubmit(newCatalogItem8);
                context.catalog_items.InsertOnSubmit(newCatalogItem9);
                context.catalog_items.InsertOnSubmit(newCatalogItem10);
                context.catalog_items.InsertOnSubmit(newCatalogItem11);
                context.catalog_items.InsertOnSubmit(newCatalogItem12);
                context.catalog_items.InsertOnSubmit(newCatalogItem13);
                context.catalog_items.InsertOnSubmit(newCatalogItem14);
                context.catalog_items.InsertOnSubmit(newCatalogItem15);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region catalog_item_detail - 50%

                #region Create new record

                catalog_item_detail newCatalogItemDetail1 = new catalog_item_detail();
                // newCatalogItemDetail1.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail1.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemDetail1.catalog_item_detail_code = "22365";
                newCatalogItemDetail1.catalog_item_detail_name = "YUMMY LIX";
                newCatalogItemDetail1.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail1.nb_units = 576;
                newCatalogItemDetail1.profit_rate = 0.50;
                newCatalogItemDetail1.term = 0;
                newCatalogItemDetail1.description = "";
                newCatalogItemDetail1.is_default = false;
                newCatalogItemDetail1.deleted = false;
                newCatalogItemDetail1.create_date = DateTime.Now;
                newCatalogItemDetail1.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail1.update_date = DateTime.Now;
                newCatalogItemDetail1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail2 = new catalog_item_detail();
                // newCatalogItemDetail2.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail2.catalog_item_id = newCatalogItem2.catalog_item_id;
                newCatalogItemDetail2.catalog_item_detail_code = "22237";
                newCatalogItemDetail2.catalog_item_detail_name = "COLOR EXPLODER";
                newCatalogItemDetail2.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail2.nb_units = 576;
                newCatalogItemDetail2.profit_rate = 0.50;
                newCatalogItemDetail2.term = 0;
                newCatalogItemDetail2.description = "";
                newCatalogItemDetail2.is_default = false;
                newCatalogItemDetail2.deleted = false;
                newCatalogItemDetail2.create_date = DateTime.Now;
                newCatalogItemDetail2.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail2.update_date = DateTime.Now;
                newCatalogItemDetail2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail3 = new catalog_item_detail();
                // newCatalogItemDetail3.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail3.catalog_item_id = newCatalogItem3.catalog_item_id;
                newCatalogItemDetail3.catalog_item_detail_code = "22389";
                newCatalogItemDetail3.catalog_item_detail_name = "HEART SHAPE POP 1 OZ";
                newCatalogItemDetail3.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail3.nb_units = 576;
                newCatalogItemDetail3.profit_rate = 0.50;
                newCatalogItemDetail3.term = 0;
                newCatalogItemDetail3.description = "";
                newCatalogItemDetail3.is_default = false;
                newCatalogItemDetail3.deleted = false;
                newCatalogItemDetail3.create_date = DateTime.Now;
                newCatalogItemDetail3.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail3.update_date = DateTime.Now;
                newCatalogItemDetail3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail4 = new catalog_item_detail();
                // newCatalogItemDetail4.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail4.catalog_item_id = newCatalogItem4.catalog_item_id;
                newCatalogItemDetail4.catalog_item_detail_code = "22396";
                newCatalogItemDetail4.catalog_item_detail_name = "SWEET YUMMY LIPS";
                newCatalogItemDetail4.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail4.nb_units = 576;
                newCatalogItemDetail4.profit_rate = 0.50;
                newCatalogItemDetail4.term = 0;
                newCatalogItemDetail4.description = "";
                newCatalogItemDetail4.is_default = false;
                newCatalogItemDetail4.deleted = false;
                newCatalogItemDetail4.create_date = DateTime.Now;
                newCatalogItemDetail4.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail4.update_date = DateTime.Now;
                newCatalogItemDetail4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail5 = new catalog_item_detail();
                // newCatalogItemDetail5.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail5.catalog_item_id = newCatalogItem5.catalog_item_id;
                newCatalogItemDetail5.catalog_item_detail_code = "22402";
                newCatalogItemDetail5.catalog_item_detail_name = "SOUR YUMMY LIPS";
                newCatalogItemDetail5.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail5.nb_units = 576;
                newCatalogItemDetail5.profit_rate = 0.50;
                newCatalogItemDetail5.term = 0;
                newCatalogItemDetail5.description = "";
                newCatalogItemDetail5.is_default = false;
                newCatalogItemDetail5.deleted = false;
                newCatalogItemDetail5.create_date = DateTime.Now;
                newCatalogItemDetail5.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail5.update_date = DateTime.Now;
                newCatalogItemDetail5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail6 = new catalog_item_detail();
                // newCatalogItemDetail6.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail6.catalog_item_id = newCatalogItem6.catalog_item_id;
                newCatalogItemDetail6.catalog_item_detail_code = "22419";
                newCatalogItemDetail6.catalog_item_detail_name = "MILKY POP";
                newCatalogItemDetail6.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail6.nb_units = 576;
                newCatalogItemDetail6.profit_rate = 0.50;
                newCatalogItemDetail6.term = 0;
                newCatalogItemDetail6.description = "";
                newCatalogItemDetail6.is_default = false;
                newCatalogItemDetail6.deleted = false;
                newCatalogItemDetail6.create_date = DateTime.Now;
                newCatalogItemDetail6.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail6.update_date = DateTime.Now;
                newCatalogItemDetail6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail7 = new catalog_item_detail();
                // newCatalogItemDetail7.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail7.catalog_item_id = newCatalogItem7.catalog_item_id;
                newCatalogItemDetail7.catalog_item_detail_code = "22426";
                newCatalogItemDetail7.catalog_item_detail_name = "1OZ SOUR MANIA";
                newCatalogItemDetail7.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail7.nb_units = 576;
                newCatalogItemDetail7.profit_rate = 0.50;
                newCatalogItemDetail7.term = 0;
                newCatalogItemDetail7.description = "";
                newCatalogItemDetail7.is_default = false;
                newCatalogItemDetail7.deleted = false;
                newCatalogItemDetail7.create_date = DateTime.Now;
                newCatalogItemDetail7.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail7.update_date = DateTime.Now;
                newCatalogItemDetail7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail8 = new catalog_item_detail();
                // newCatalogItemDetail8.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail8.catalog_item_id = newCatalogItem8.catalog_item_id;
                newCatalogItemDetail8.catalog_item_detail_code = "22501";
                newCatalogItemDetail8.catalog_item_detail_name = "LOLLIBALL LOLLIPOPS";
                newCatalogItemDetail8.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail8.nb_units = 576;
                newCatalogItemDetail8.profit_rate = 0.50;
                newCatalogItemDetail8.term = 0;
                newCatalogItemDetail8.description = "";
                newCatalogItemDetail8.is_default = false;
                newCatalogItemDetail8.deleted = false;
                newCatalogItemDetail8.create_date = DateTime.Now;
                newCatalogItemDetail8.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail8.update_date = DateTime.Now;
                newCatalogItemDetail8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail9 = new catalog_item_detail();
                // newCatalogItemDetail9.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail9.catalog_item_id = newCatalogItem9.catalog_item_id;
                newCatalogItemDetail9.catalog_item_detail_code = "22518";
                newCatalogItemDetail9.catalog_item_detail_name = "EASTER JOY LOLLIPOPS";
                newCatalogItemDetail9.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail9.nb_units = 576;
                newCatalogItemDetail9.profit_rate = 0.50;
                newCatalogItemDetail9.term = 0;
                newCatalogItemDetail9.description = "";
                newCatalogItemDetail9.is_default = false;
                newCatalogItemDetail9.deleted = false;
                newCatalogItemDetail9.create_date = DateTime.Now;
                newCatalogItemDetail9.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail9.update_date = DateTime.Now;
                newCatalogItemDetail9.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail10 = new catalog_item_detail();
                // newCatalogItemDetail10.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail10.catalog_item_id = newCatalogItem10.catalog_item_id;
                newCatalogItemDetail10.catalog_item_detail_code = "22525";
                newCatalogItemDetail10.catalog_item_detail_name = "MERRY RAINBBOW LOLLIPOPS";
                newCatalogItemDetail10.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail10.nb_units = 576;
                newCatalogItemDetail10.profit_rate = 0.50;
                newCatalogItemDetail10.term = 0;
                newCatalogItemDetail10.description = "";
                newCatalogItemDetail10.is_default = false;
                newCatalogItemDetail10.deleted = false;
                newCatalogItemDetail10.create_date = DateTime.Now;
                newCatalogItemDetail10.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail10.update_date = DateTime.Now;
                newCatalogItemDetail10.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail11 = new catalog_item_detail();
                // newCatalogItemDetail11.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail11.catalog_item_id = newCatalogItem11.catalog_item_id;
                newCatalogItemDetail11.catalog_item_detail_code = "22253";
                newCatalogItemDetail11.catalog_item_detail_name = "FAITH RELIGIOUS LOLLIPOPS";
                newCatalogItemDetail11.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail11.nb_units = 576;
                newCatalogItemDetail11.profit_rate = 0.50;
                newCatalogItemDetail11.term = 0;
                newCatalogItemDetail11.description = "";
                newCatalogItemDetail11.is_default = false;
                newCatalogItemDetail11.deleted = false;
                newCatalogItemDetail11.create_date = DateTime.Now;
                newCatalogItemDetail11.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail11.update_date = DateTime.Now;
                newCatalogItemDetail11.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail12 = new catalog_item_detail();
                // newCatalogItemDetail12.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail12.catalog_item_id = newCatalogItem12.catalog_item_id;
                newCatalogItemDetail12.catalog_item_detail_code = "22532";
                newCatalogItemDetail12.catalog_item_detail_name = "LOLLIWEEN LOLLIPOPS";
                newCatalogItemDetail12.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail12.nb_units = 576;
                newCatalogItemDetail12.profit_rate = 0.50;
                newCatalogItemDetail12.term = 0;
                newCatalogItemDetail12.description = "";
                newCatalogItemDetail12.is_default = false;
                newCatalogItemDetail12.deleted = false;
                newCatalogItemDetail12.create_date = DateTime.Now;
                newCatalogItemDetail12.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail12.update_date = DateTime.Now;
                newCatalogItemDetail12.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail13 = new catalog_item_detail();
                // newCatalogItemDetail13.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail13.catalog_item_id = newCatalogItem13.catalog_item_id;
                newCatalogItemDetail13.catalog_item_detail_code = "22257";
                newCatalogItemDetail13.catalog_item_detail_name = "HOT POPS";
                newCatalogItemDetail13.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail13.nb_units = 576;
                newCatalogItemDetail13.profit_rate = 0.50;
                newCatalogItemDetail13.term = 0;
                newCatalogItemDetail13.description = "";
                newCatalogItemDetail13.is_default = false;
                newCatalogItemDetail13.deleted = false;
                newCatalogItemDetail13.create_date = DateTime.Now;
                newCatalogItemDetail13.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail13.update_date = DateTime.Now;
                newCatalogItemDetail13.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail14 = new catalog_item_detail();
                // newCatalogItemDetail14.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail14.catalog_item_id = newCatalogItem14.catalog_item_id;
                newCatalogItemDetail14.catalog_item_detail_code = "22258";
                newCatalogItemDetail14.catalog_item_detail_name = "SUGAR FREE ASSORTED";
                newCatalogItemDetail14.price = Convert.ToDecimal(160.00);
                newCatalogItemDetail14.nb_units = 640;
                newCatalogItemDetail14.profit_rate = 0.50;
                newCatalogItemDetail14.term = 0;
                newCatalogItemDetail14.description = "";
                newCatalogItemDetail14.is_default = false;
                newCatalogItemDetail14.deleted = false;
                newCatalogItemDetail14.create_date = DateTime.Now;
                newCatalogItemDetail14.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail14.update_date = DateTime.Now;
                newCatalogItemDetail14.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail15 = new catalog_item_detail();
                // newCatalogItemDetail15.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail15.catalog_item_id = newCatalogItem15.catalog_item_id;
                newCatalogItemDetail15.catalog_item_detail_code = "22631";
                newCatalogItemDetail15.catalog_item_detail_name = "BASEBALL POPS";
                newCatalogItemDetail15.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail15.nb_units = 576;
                newCatalogItemDetail15.profit_rate = 0.50;
                newCatalogItemDetail15.term = 0;
                newCatalogItemDetail15.description = "";
                newCatalogItemDetail15.is_default = false;
                newCatalogItemDetail15.deleted = false;
                newCatalogItemDetail15.create_date = DateTime.Now;
                newCatalogItemDetail15.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail15.update_date = DateTime.Now;
                newCatalogItemDetail15.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail1);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail2);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail3);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail4);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail5);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail6);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail7);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail8);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail9);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail10);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail11);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail12);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail13);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail14);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail15);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region catalog_item_category_catalog_item

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem1 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem1.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem1.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemCategoryCatalogItem1.display_order = 1;
                newCatalogItemCategoryCatalogItem1.deleted = false;
                newCatalogItemCategoryCatalogItem1.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem1.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem2 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem2.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem2.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem2.catalog_item_id = newCatalogItem2.catalog_item_id;
                newCatalogItemCategoryCatalogItem2.display_order = 1;
                newCatalogItemCategoryCatalogItem2.deleted = false;
                newCatalogItemCategoryCatalogItem2.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem2.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem2.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem3 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem3.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem3.catalog_item_id = newCatalogItem3.catalog_item_id;
                newCatalogItemCategoryCatalogItem3.display_order = 1;
                newCatalogItemCategoryCatalogItem3.deleted = false;
                newCatalogItemCategoryCatalogItem3.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem3.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem3.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem4 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem4.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem4.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem4.catalog_item_id = newCatalogItem4.catalog_item_id;
                newCatalogItemCategoryCatalogItem4.display_order = 1;
                newCatalogItemCategoryCatalogItem4.deleted = false;
                newCatalogItemCategoryCatalogItem4.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem4.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem4.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem5 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem5.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem5.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem5.catalog_item_id = newCatalogItem5.catalog_item_id;
                newCatalogItemCategoryCatalogItem5.display_order = 1;
                newCatalogItemCategoryCatalogItem5.deleted = false;
                newCatalogItemCategoryCatalogItem5.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem5.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem5.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem6 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem6.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem6.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem6.catalog_item_id = newCatalogItem6.catalog_item_id;
                newCatalogItemCategoryCatalogItem6.display_order = 1;
                newCatalogItemCategoryCatalogItem6.deleted = false;
                newCatalogItemCategoryCatalogItem6.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem6.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem6.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem7 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem7.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem7.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem7.catalog_item_id = newCatalogItem7.catalog_item_id;
                newCatalogItemCategoryCatalogItem7.display_order = 1;
                newCatalogItemCategoryCatalogItem7.deleted = false;
                newCatalogItemCategoryCatalogItem7.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem7.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem7.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem8 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem8.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem8.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem8.catalog_item_id = newCatalogItem8.catalog_item_id;
                newCatalogItemCategoryCatalogItem8.display_order = 1;
                newCatalogItemCategoryCatalogItem8.deleted = false;
                newCatalogItemCategoryCatalogItem8.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem8.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem8.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem9 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem9.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem9.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem9.catalog_item_id = newCatalogItem9.catalog_item_id;
                newCatalogItemCategoryCatalogItem9.display_order = 1;
                newCatalogItemCategoryCatalogItem9.deleted = false;
                newCatalogItemCategoryCatalogItem9.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem9.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem9.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem9.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem10 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem10.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem10.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem10.catalog_item_id = newCatalogItem10.catalog_item_id;
                newCatalogItemCategoryCatalogItem10.display_order = 1;
                newCatalogItemCategoryCatalogItem10.deleted = false;
                newCatalogItemCategoryCatalogItem10.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem10.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem10.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem10.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem11 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem11.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem11.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem11.catalog_item_id = newCatalogItem11.catalog_item_id;
                newCatalogItemCategoryCatalogItem11.display_order = 1;
                newCatalogItemCategoryCatalogItem11.deleted = false;
                newCatalogItemCategoryCatalogItem11.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem11.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem11.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem11.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem12 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem12.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem12.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem12.catalog_item_id = newCatalogItem12.catalog_item_id;
                newCatalogItemCategoryCatalogItem12.display_order = 1;
                newCatalogItemCategoryCatalogItem12.deleted = false;
                newCatalogItemCategoryCatalogItem12.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem12.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem12.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem12.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem13 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem13.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem13.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem13.catalog_item_id = newCatalogItem13.catalog_item_id;
                newCatalogItemCategoryCatalogItem13.display_order = 1;
                newCatalogItemCategoryCatalogItem13.deleted = false;
                newCatalogItemCategoryCatalogItem13.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem13.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem13.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem13.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem14 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem14.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem14.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem14.catalog_item_id = newCatalogItem14.catalog_item_id;
                newCatalogItemCategoryCatalogItem14.display_order = 1;
                newCatalogItemCategoryCatalogItem14.deleted = false;
                newCatalogItemCategoryCatalogItem14.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem14.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem14.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem14.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem15 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem15.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem15.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem15.catalog_item_id = newCatalogItem15.catalog_item_id;
                newCatalogItemCategoryCatalogItem15.display_order = 1;
                newCatalogItemCategoryCatalogItem15.deleted = false;
                newCatalogItemCategoryCatalogItem15.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem15.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem15.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem15.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem1);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem2);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem3);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem4);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem5);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem6);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem7);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem8);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem9);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem10);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem11);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem12);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem13);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem14);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem15);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #region business_rule

                #region Predefined Business rules

                #region Sales History Interval # Day

                // Create new form group object
                business_rule newBusinessRule1 = new business_rule();

                // Fill in new record data
                // newBusinessRule1.business_rule_id;           // Created upon insert
                newBusinessRule1.form_id = newForm.form_id;
                newBusinessRule1.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule1.field_id = 29;                 // 29 = [Interval_NbDay]
                newBusinessRule1.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule1.business_rule_name = "[Interval_NbDay]";
                newBusinessRule1.value_to_compare = "45";
                // newBusinessRule1.form_section_number;        // null, not used
                // newBusinessRule1.description;                // null, not used
                // newBusinessRule1.message;                    // null, not used
                newBusinessRule1.deleted = false;
                newBusinessRule1.create_date = DateTime.Now;
                newBusinessRule1.create_user_id = CREATE_USER_ID;
                newBusinessRule1.update_date = DateTime.Now;
                newBusinessRule1.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Sales History Minimum Total Amount:

                // Create new form group object
                business_rule newBusinessRule2 = new business_rule();

                // Fill in new record data
                // newBusinessRule2.business_rule_id;           // Created upon insert
                newBusinessRule2.form_id = newForm.form_id;
                newBusinessRule2.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule2.field_id = 105;                 // 105 = account_history_min_total_amount
                newBusinessRule2.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule2.business_rule_name = "account_history_min_total_amount";
                newBusinessRule2.value_to_compare = "2000";
                // newBusinessRule2.form_section_number;        // null, not used
                // newBusinessRule2.description;                // null, not used
                // newBusinessRule2.message;                    // null, not used
                newBusinessRule2.deleted = false;
                newBusinessRule2.create_date = DateTime.Now;
                newBusinessRule2.create_user_id = CREATE_USER_ID;
                newBusinessRule2.update_date = DateTime.Now;
                newBusinessRule2.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #endregion

                #region Order Requirements

                #region For All Sections

                #region Standard lead time

                // Create new form group object
                business_rule newBusinessRule3 = new business_rule();

                // Fill in new record data
                // newBusinessRule3.business_rule_id;           // Created upon insert
                newBusinessRule3.form_id = newForm.form_id;
                newBusinessRule3.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule3.field_id = 35;                 // 35 = min_nb_day_lead_time
                newBusinessRule3.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule3.business_rule_name = "min_nb_day_lead_time";
                newBusinessRule3.value_to_compare = "10";
                // newBusinessRule3.form_section_number;        // null, not used
                // newBusinessRule3.description;                // null, not used
                // newBusinessRule3.message;                    // null, not used
                newBusinessRule3.deleted = false;
                newBusinessRule3.create_date = DateTime.Now;
                newBusinessRule3.create_user_id = CREATE_USER_ID;
                newBusinessRule3.update_date = DateTime.Now;
                newBusinessRule3.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule3);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Minimum Total Quantity

                // Create new form group object
                business_rule newBusinessRule4 = new business_rule();

                // Fill in new record data
                // newBusinessRule4.business_rule_id;           // Created upon insert
                newBusinessRule4.form_id = newForm.form_id;
                newBusinessRule4.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule4.field_id = 49;                 // 49 = min_total_quantity
                newBusinessRule4.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule4.business_rule_name = "min_total_quantity";
                newBusinessRule4.value_to_compare = "1";
                // newBusinessRule4.form_section_number;        // null, not used
                // newBusinessRule4.description;                // null, not used
                // newBusinessRule4.message;                    // null, not used
                newBusinessRule4.deleted = false;
                newBusinessRule4.create_date = DateTime.Now;
                newBusinessRule4.create_user_id = CREATE_USER_ID;
                newBusinessRule4.update_date = DateTime.Now;
                newBusinessRule4.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule4);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Minimum Total Amount

                // Create new form group object
                business_rule newBusinessRule5 = new business_rule();

                // Fill in new record data
                // newBusinessRule5.business_rule_id;           // Created upon insert
                newBusinessRule5.form_id = newForm.form_id;
                newBusinessRule5.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule5.field_id = 45;                 // 45 = min_total_amount
                newBusinessRule5.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule5.business_rule_name = "min_total_amount";
                newBusinessRule5.value_to_compare = "1";
                // newBusinessRule5.form_section_number;        // null, not used
                // newBusinessRule5.description;                // null, not used
                // newBusinessRule5.message;                    // null, not used
                newBusinessRule5.deleted = false;
                newBusinessRule5.create_date = DateTime.Now;
                newBusinessRule5.create_user_id = CREATE_USER_ID;
                newBusinessRule5.update_date = DateTime.Now;
                newBusinessRule5.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule5);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Maximum Total Amount

                //// Create new form group object
                //business_rule newBusinessRule6 = new business_rule();

                //// Fill in new record data
                //// newBusinessRule6.business_rule_id;           // Created upon insert
                //newBusinessRule6.form_id = newForm.form_id;
                //newBusinessRule6.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                //newBusinessRule6.field_id = 108;                 // 108 = max_total_amount
                //newBusinessRule6.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                //newBusinessRule6.business_rule_name = "max_total_amount";
                //newBusinessRule6.value_to_compare = "50000";
                //// newBusinessRule6.form_section_number;        // null, not used
                //// newBusinessRule6.description;                // null, not used
                //// newBusinessRule6.message;                    // null, not used
                //newBusinessRule6.deleted = false;
                //newBusinessRule6.create_date = DateTime.Now;
                //newBusinessRule6.create_user_id = CREATE_USER_ID;
                //newBusinessRule6.update_date = DateTime.Now;
                //newBusinessRule6.update_user_id = CREATE_USER_ID;

                //// Add the new form group to the db context
                //context.business_rules.InsertOnSubmit(newBusinessRule6);

                //// Submit changes to the db
                //context.SubmitChanges();

                //sb.AppendLine("Business rule submitted");

                #endregion

                #endregion

                #endregion

                #endregion

                #region business_exception



                #region Standard product - minimum day lead time

                // Create new form group object
                business_exception newBusinessException2 = new business_exception();

                // Fill in new record data
                //newBusinessException2.business_exception_id;      // newBusinessException1
                newBusinessException2.form_id = newForm.form_id;
                newBusinessException2.business_exception_name = "Standard product - minimum day lead time";
                newBusinessException2.exception_type_id = 100;      // 100 = Note
                newBusinessException2.entity_type_id = 4;           // 4 = Order
                newBusinessException2.warning_message = "10 Business Days Lead-Time Required.<BR>If requested Lead-Time is <u>LESS</u> than 10 business Days, change the delivery date to meet this requirement.";
                newBusinessException2.app_item_id = 23;             // 23 = Order Form Step 3 - Information
                newBusinessException2.message = "For Product Orders by Common Carrier <u>LESS</u> than 10 Business Days cannot be guaranteed. ";
                newBusinessException2.exception_expression = "[nb_day_lead_time] <  [min_nb_day_lead_time]";
                newBusinessException2.fees_value_expression = "";
                newBusinessException2.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException2.form_section_number;       // null, not used
                // newBusinessException2.business_rule_id;          // null, not used
                newBusinessException2.deleted = false;
                newBusinessException2.create_date = DateTime.Now;
                newBusinessException2.create_user_id = CREATE_USER_ID;
                newBusinessException2.update_date = DateTime.Now;
                newBusinessException2.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion


                #region Standard product - expedited freight charges

                //// Create new form group object
                //business_exception newBusinessException5 = new business_exception();

                //// Fill in new record data
                ////newBusinessException5.business_exception_id;      // newBusinessException1
                //newBusinessException5.form_id = newForm.form_id;
                //newBusinessException5.business_exception_name = "Standard product - expedited freight charges";
                //newBusinessException5.exception_type_id = 103;      // 103 = Expedited freight charges
                //newBusinessException5.entity_type_id = 4;           // 4 = Order
                //newBusinessException5.warning_message = "";
                //// newBusinessException5.app_item_id;               // null, not used
                //newBusinessException5.message = "For Product Orders by Common Carrier, <u>LESS</u> than 4 Business Days, if there are any expedited freight charges relating to this order they will be recovered from the employee's 12-pay.";
                //newBusinessException5.exception_expression = "([nb_day_lead_time] <  [min_nb_day_lead_time]) AND  ([delivery_method_id] = [common_carrier])";
                //newBusinessException5.fees_value_expression = "";
                //newBusinessException5.form_section_type_id = 1;     // 1 = Standard product
                //// newBusinessException5.form_section_number;       // null, not used
                //// newBusinessException5.business_rule_id;          // null, not used
                //newBusinessException5.deleted = false;
                //newBusinessException5.create_date = DateTime.Now;
                //newBusinessException5.create_user_id = CREATE_USER_ID;
                //newBusinessException5.update_date = DateTime.Now;
                //newBusinessException5.update_user_id = CREATE_USER_ID;

                //// Add the new form group to the db context
                //context.business_exceptions.InsertOnSubmit(newBusinessException5);

                //// Submit changes to the db
                //context.SubmitChanges();

                //sb.AppendLine("New business exception");

                #endregion

                #region Standard product - minimum total quantity

                //// Create new form group object
                //business_exception newBusinessException7 = new business_exception();

                //// Fill in new record data
                ////newBusinessException7.business_exception_id;      // newBusinessException1
                //newBusinessException7.form_id = newForm.form_id;
                //newBusinessException7.business_exception_name = "Standard product - minimum total quantity";
                //newBusinessException7.exception_type_id = 900;      // 900 = Mandatory
                //newBusinessException7.entity_type_id = 4;           // 4 = Order
                //newBusinessException7.warning_message = "";
                //newBusinessException7.app_item_id = 24;             // 24 = Order form step 4 - order detail
                //newBusinessException7.message = "";
                //newBusinessException7.exception_expression = "([total_quantity] <  [min_total_quantity])";
                //newBusinessException7.fees_value_expression = "";
                //newBusinessException7.form_section_type_id = 1;     // 1 = Standard product
                //// newBusinessException7.form_section_number;       // null, not used
                //// newBusinessException7.business_rule_id;          // null, not used
                //newBusinessException7.deleted = false;
                //newBusinessException7.create_date = DateTime.Now;
                //newBusinessException7.create_user_id = CREATE_USER_ID;
                //newBusinessException7.update_date = DateTime.Now;
                //newBusinessException7.update_user_id = CREATE_USER_ID;

                //// Add the new form group to the db context
                //context.business_exceptions.InsertOnSubmit(newBusinessException7);

                //// Submit changes to the db
                //context.SubmitChanges();

                //sb.AppendLine("New business exception");

                #endregion

                #endregion

                #endregion

                sb.AppendLine("Success!");
            }
            catch (Exception ex)
            {
                //transaction.Rollback();

                sb.AppendLine("transaction rollback due to error");
                sb.AppendLine("Message: " + ex.Message);
                sb.AppendLine("Source: " + ex.Source);
                sb.AppendLine("");
                sb.AppendLine(ex.StackTrace);
            }
            finally
            {
                if (context.Connection != null)
                {
                    context.Connection.Close();
                }
            }

            sb.AppendLine("end");

            return sb.ToString();
        }

        #endregion

        #region Spring 2011

        public string Spring2011_Hershey()
        {
            #region Start

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("start");

            // Open db context
            QSPFulfillmentDataContext context = new QSPFulfillmentDataContext(CONNECTION_STRING);
            sb.AppendLine("db context open");

            // Open the connection
            context.Connection.Open();

            #endregion

            try
            {
                #region Order form

                #region form_group

                // Create new form group object
                form_group newFormGroup = new form_group();

                // Fill in new form group data
                // newFormGroup.form_group_id;              // Autonumber generated on insert
                newFormGroup.form_group_name = "Hershey Spring 2011 order form group";
                newFormGroup.deleted = false;
                newFormGroup.create_date = DateTime.Now;
                newFormGroup.create_user_id = CREATE_USER_ID;
                newFormGroup.update_date = DateTime.Now;
                newFormGroup.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.form_groups.InsertOnSubmit(newFormGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form group id = " + newFormGroup.form_group_id.ToString());
                // MessageBox.Show("New form group id = " + newFormGroup.form_group_id.ToString());

                #endregion

                #region form

                // Create new form object
                form newForm = new form();

                // Fill in new form data
                // newForm.form_id;                             // Autonumber generated on insert
                newForm.form_group_id = newFormGroup.form_group_id;
                newForm.entity_type_id = 4;                     // Type 4 = order
                newForm.form_code = "HersheySpring2011";
                newForm.form_name = "HersheySpring2011";
                newForm.description = "";
                newForm.order_terms_text = "";
                newForm.start_date = new DateTime(2010, 12, 8);
                newForm.end_date = new DateTime(2011, 6, 30);
                newForm.closing_time = new DateTime(2011, 6, 30);
                newForm.image_url = "images/CatalogItem/HersheyBar.gif";
                newForm.is_base_form = false;
                newForm.parent_form_id = 8;                     // Base form for orders in prod and dev
                newForm.is_product_price_updatable = false;
                newForm.is_quantity_adjustment_allowed = true;
                newForm.tax_postal_address_type_id = 2;
                newForm.enabled = true;
                newForm.deleted = false;
                newForm.version = 1;
                newForm.program_id = 3;                         // Otis = 3, PV = 2
                newForm.program_type_id = 7;
                newForm.program_basics_text = "";
                newForm.create_date = DateTime.Now;
                newForm.create_user_id = CREATE_USER_ID;
                newForm.update_date = DateTime.Now;
                newForm.update_user_id = CREATE_USER_ID;
                //newForm.warehouse_type_id;
                newForm.is_bulk = false;
                newForm.report_name = "OrderForm";
                newForm.is_warehouse_selectable = true;
                //newForm.default_warehouse_id = 27;
                newForm.season_id = 8;

                // Add the new form to the db context
                context.forms.InsertOnSubmit(newForm);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form id = " + newForm.form_id.ToString());
                // MessageBox.Show("New form id = " + newForm.form_id.ToString());

                #endregion

                #region form_permission

                form_permission newFormPermission1 = new form_permission();
                //newFormPermission1.form_permission_id;            // Autonumber generated on insert
                newFormPermission1.form_id = newForm.form_id;
                newFormPermission1.role_id = 0;                     // User
                newFormPermission1.allow_read = false;
                newFormPermission1.allow_write = false;
                newFormPermission1.create_date = DateTime.Now;
                newFormPermission1.create_user_id = CREATE_USER_ID;
                newFormPermission1.update_date = DateTime.Now;
                newFormPermission1.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission2 = new form_permission();
                //newFormPermission2.form_permission_id;            // Autonumber generated on insert
                newFormPermission2.form_id = newForm.form_id;
                newFormPermission2.role_id = 1;                     // Field Sale Manager (FM)
                newFormPermission2.allow_read = true;
                newFormPermission2.allow_write = true;
                newFormPermission2.create_date = DateTime.Now;
                newFormPermission2.create_user_id = CREATE_USER_ID;
                newFormPermission2.update_date = DateTime.Now;
                newFormPermission2.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission3 = new form_permission();
                //newFormPermission3.form_permission_id;            // Autonumber generated on insert
                newFormPermission3.form_id = newForm.form_id;
                newFormPermission3.role_id = 2;                     // Field Support
                newFormPermission3.allow_read = true;
                newFormPermission3.allow_write = true;
                newFormPermission3.create_date = DateTime.Now;
                newFormPermission3.create_user_id = CREATE_USER_ID;
                newFormPermission3.update_date = DateTime.Now;
                newFormPermission3.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission4 = new form_permission();
                //newFormPermission4.form_permission_id;            // Autonumber generated on insert
                newFormPermission4.form_id = newForm.form_id;
                newFormPermission4.role_id = 3;                     // Accounting Manager
                newFormPermission4.allow_read = true;
                newFormPermission4.allow_write = true;
                newFormPermission4.create_date = DateTime.Now;
                newFormPermission4.create_user_id = CREATE_USER_ID;
                newFormPermission4.update_date = DateTime.Now;
                newFormPermission4.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission5 = new form_permission();
                //newFormPermission5.form_permission_id;            // Autonumber generated on insert
                newFormPermission5.form_id = newForm.form_id;
                newFormPermission5.role_id = 4;                     // Admin
                newFormPermission5.allow_read = true;
                newFormPermission5.allow_write = true;
                newFormPermission5.create_date = DateTime.Now;
                newFormPermission5.create_user_id = CREATE_USER_ID;
                newFormPermission5.update_date = DateTime.Now;
                newFormPermission5.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission6 = new form_permission();
                //newFormPermission6.form_permission_id;            // Autonumber generated on insert
                newFormPermission6.form_id = newForm.form_id;
                newFormPermission6.role_id = 5;                     // Super User
                newFormPermission6.allow_read = true;
                newFormPermission6.allow_write = true;
                newFormPermission6.create_date = DateTime.Now;
                newFormPermission6.create_user_id = CREATE_USER_ID;
                newFormPermission6.update_date = DateTime.Now;
                newFormPermission6.update_user_id = CREATE_USER_ID;

                // Add the new form permissions to the db context
                context.form_permissions.InsertOnSubmit(newFormPermission1);
                context.form_permissions.InsertOnSubmit(newFormPermission2);
                context.form_permissions.InsertOnSubmit(newFormPermission3);
                context.form_permissions.InsertOnSubmit(newFormPermission4);
                context.form_permissions.InsertOnSubmit(newFormPermission5);
                context.form_permissions.InsertOnSubmit(newFormPermission6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form permissions ok");

                #endregion

                #region form_delivery_date_type

                form_delivery_date_type newFormDeliveryDateType1 = new form_delivery_date_type();
                //newFormDeliveryDateType1.form_delivery_date_type_id;  // Autonumber generated on insert
                newFormDeliveryDateType1.form_id = newForm.form_id;
                newFormDeliveryDateType1.delivery_date_type_id = 1;     // Choose a date

                context.form_delivery_date_types.InsertOnSubmit(newFormDeliveryDateType1);
                context.SubmitChanges();

                sb.AppendLine("form delivery date type ok");

                #endregion

                #region form_profit_rate

                form_profit_rate newFormProfitRate1 = new form_profit_rate();
                //newFormProfitRate1.form_profit_rate_id;                // Autonumber generated on insert
                newFormProfitRate1.form_id = newForm.form_id;
                newFormProfitRate1.profit_rate_id = 1;         // 1 = 40%, 2 = 45%, 3 = 50%
                newFormProfitRate1.deleted = false;
                newFormProfitRate1.create_date = DateTime.Now;
                newFormProfitRate1.create_user_id = CREATE_USER_ID;
                newFormProfitRate1.update_date = DateTime.Now;
                newFormProfitRate1.update_user_id = CREATE_USER_ID;

                form_profit_rate newFormProfitRate2 = new form_profit_rate();
                //newFormProfitRate2.form_profit_rate_id;                // Autonumber generated on insert
                newFormProfitRate2.form_id = newForm.form_id;
                newFormProfitRate2.profit_rate_id = 3;         // 1 = 40%, 2 = 45%, 3 = 50%
                newFormProfitRate2.deleted = false;
                newFormProfitRate2.create_date = DateTime.Now;
                newFormProfitRate2.create_user_id = CREATE_USER_ID;
                newFormProfitRate2.update_date = DateTime.Now;
                newFormProfitRate2.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_profit_rates.InsertOnSubmit(newFormProfitRate1);
                context.form_profit_rates.InsertOnSubmit(newFormProfitRate2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form profit rate ok");

                #endregion

                #region form_order_type

                // Create new form order type object
                form_order_type newFormOrderType1 = new form_order_type();
                // newFormOrderType1.form_order_type_id;            // Autonumber generated on insert
                newFormOrderType1.form_id = newForm.form_id;
                newFormOrderType1.order_type_id = 1;                // 1 = standard order
                newFormOrderType1.deleted = false;
                newFormOrderType1.create_date = DateTime.Now;
                newFormOrderType1.create_user_id = CREATE_USER_ID;
                newFormOrderType1.update_date = DateTime.Now;
                newFormOrderType1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_order_types.InsertOnSubmit(newFormOrderType1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form order type ok");

                #endregion

                #region form_delivery_method

                // Create new record
                form_delivery_method newFormDeliveryMethod1 = new form_delivery_method();
                // newFormDeliveryMethod1.form_delivery_method_id;          // Autonumber generated on insert
                newFormDeliveryMethod1.delivery_method_id = 1;              // Common carrier
                newFormDeliveryMethod1.form_id = newForm.form_id;
                newFormDeliveryMethod1.deleted = false;
                newFormDeliveryMethod1.create_date = DateTime.Now;
                newFormDeliveryMethod1.create_user_id = CREATE_USER_ID;
                newFormDeliveryMethod1.update_date = DateTime.Now;
                newFormDeliveryMethod1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_delivery_methods.InsertOnSubmit(newFormDeliveryMethod1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form delivery methods ok");

                #endregion

                #region catalog_group

                // Create new record
                catalog_group newCatalogGroup = new catalog_group();
                // newCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newCatalogGroup.catalog_group_code = "HersheySpring2011";
                newCatalogGroup.catalog_group_name = "Hershey Spring 2011 order form";
                newCatalogGroup.description = "Hershey Spring 2011 order form";
                newCatalogGroup.deleted = false;
                newCatalogGroup.create_date = DateTime.Now;
                newCatalogGroup.create_user_id = CREATE_USER_ID;
                newCatalogGroup.update_date = DateTime.Now;
                newCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region catalog

                // Create new record
                catalog newCatalog = new catalog();
                // newCatalog.newCatalog_id;          // Autonumber generated on insert
                newCatalog.catalog_group_id = newCatalogGroup.catalog_group_id;
                newCatalog.catalog_code = "HersheySpring2011";
                newCatalog.catalog_name = "Hershey Spring 2011 order form";
                newCatalog.culture = "en-US";
                newCatalog.description = "Hershey Spring 2011 order form";
                newCatalog.start_date = new DateTime(2010, 12, 8);
                newCatalog.end_date = new DateTime(2011, 6, 30);
                newCatalog.deleted = false;
                newCatalog.create_date = DateTime.Now;
                newCatalog.create_user_id = CREATE_USER_ID;
                newCatalog.update_date = DateTime.Now;
                newCatalog.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newCatalog);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newCatalog.catalog_id.ToString());

                #endregion

                #region catalog_item_category

                // Create new record
                catalog_item_category newCatalogItemCategory1 = new catalog_item_category();
                // newCatalogItemCategory1.catalog_item_category_id;         // Autonumber generated on insert
                newCatalogItemCategory1.catalog_id = newCatalog.catalog_id;
                newCatalogItemCategory1.catalog_item_category_name = "Hershey Spring 2011";
                newCatalogItemCategory1.parent_catalog_item_category_id = 0;
                newCatalogItemCategory1.deleted = false;
                newCatalogItemCategory1.create_date = DateTime.Now;
                newCatalogItemCategory1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategory1.update_date = DateTime.Now;
                newCatalogItemCategory1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newCatalogItemCategory1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newCatalogItemCategory1.catalog_item_category_id.ToString());

                #endregion

                #region form_section

                // Create new record
                form_section newFormSection1 = new form_section();
                // newFormSection1.form_section_id;              // Autonumber generated on insert
                newFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newFormSection1.form_id = newForm.form_id;
                newFormSection1.form_section_type_id = 1;        // 1 = standard product, 2 = supply product, 3 = optional product
                newFormSection1.form_section_number = 1;
                newFormSection1.form_section_title = "Hershey Spring 2011";
                newFormSection1.description = "";
                newFormSection1.deleted = false;
                newFormSection1.create_date = DateTime.Now;
                newFormSection1.create_user_id = CREATE_USER_ID;
                newFormSection1.update_date = DateTime.Now;
                newFormSection1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newFormSection1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newFormSection1.form_section_id.ToString());

                #endregion

                #region product

                #region Create new record

                product newProduct1 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct1.product_code = "9938";
                newProduct1.product_name = "$2 Big Bar Assortment";
                newProduct1.price = Convert.ToDecimal(0.00);
                newProduct1.nb_units = 120;
                newProduct1.nb_day_lead_time = 0;
                newProduct1.product_status_id = 101;            // 101 = Active
                newProduct1.product_type_id = 4;                // 4 = Chocolate
                newProduct1.business_division_id = 1;           // 1 = US
                newProduct1.commission = Convert.ToDecimal(0.00);                  // 10% comission for the fm 
                newProduct1.coupon_id = null;
                newProduct1.description = "";
                newProduct1.image_url = "";
                newProduct1.is_free_sample = false;
                newProduct1.oracle_code = "";
                newProduct1.IVCOUP = "";
                newProduct1.IVITEM = "9938";
                newProduct1.unit_cost = Convert.ToDecimal(0.00);
                newProduct1.vendor_id = 7;
                newProduct1.vendor_item_code = "9938";
                newProduct1.deleted = false;
                newProduct1.create_date = DateTime.Now;
                newProduct1.create_user_id = CREATE_USER_ID;
                newProduct1.update_date = DateTime.Now;
                newProduct1.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.products.InsertOnSubmit(newProduct1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("products ok");

                #endregion

                #region catalog_item

                #region Create new record

                catalog_item newCatalogItem1 = new catalog_item();
                // newCatalogItem1.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem1.product_id = newProduct1.product_id;
                newCatalogItem1.catalog_id = newCatalog.catalog_id;
                newCatalogItem1.catalog_item_code = "9938";
                newCatalogItem1.catalog_item_name = "$2 Big Bar Assortment";
                newCatalogItem1.price = Convert.ToDecimal(0.00);
                newCatalogItem1.nb_units = 120;
                newCatalogItem1.image_url = "";
                newCatalogItem1.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem1.catalog_item_export_name = "";
                newCatalogItem1.description = "";
                newCatalogItem1.deleted = false;
                newCatalogItem1.create_date = DateTime.Now;
                newCatalogItem1.create_user_id = CREATE_USER_ID;
                newCatalogItem1.update_date = DateTime.Now;
                newCatalogItem1.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newCatalogItem1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region catalog_item_detail - 40%

                // Create new record
                catalog_item_detail newCatalogItemDetail1 = new catalog_item_detail();
                // newCatalogItemDetail1.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail1.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemDetail1.catalog_item_detail_code = "9938";
                newCatalogItemDetail1.catalog_item_detail_name = "$2 Big Bar Assortment";
                newCatalogItemDetail1.price = Convert.ToDecimal(144.00);
                newCatalogItemDetail1.nb_units = 120;
                newCatalogItemDetail1.profit_rate = 0.40;
                newCatalogItemDetail1.term = 0;
                newCatalogItemDetail1.description = "";
                newCatalogItemDetail1.is_default = false;
                newCatalogItemDetail1.deleted = false;
                newCatalogItemDetail1.create_date = DateTime.Now;
                newCatalogItemDetail1.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail1.update_date = DateTime.Now;
                newCatalogItemDetail1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region catalog_item_detail - 50%

                // Create new record
                catalog_item_detail newCatalogItemDetail4 = new catalog_item_detail();
                // newCatalogItemDetail4.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail4.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemDetail4.catalog_item_detail_code = "9938";
                newCatalogItemDetail4.catalog_item_detail_name = "$2 Big Bar Assortment";
                newCatalogItemDetail4.price = Convert.ToDecimal(120.00);
                newCatalogItemDetail4.nb_units = 120;
                newCatalogItemDetail4.profit_rate = 0.50;
                newCatalogItemDetail4.term = 0;
                newCatalogItemDetail4.description = "";
                newCatalogItemDetail4.is_default = false;
                newCatalogItemDetail4.deleted = false;
                newCatalogItemDetail4.create_date = DateTime.Now;
                newCatalogItemDetail4.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail4.update_date = DateTime.Now;
                newCatalogItemDetail4.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail4);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region catalog_item_category_catalog_item

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem1 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem1.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem1.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemCategoryCatalogItem1.display_order = 1;
                newCatalogItemCategoryCatalogItem1.deleted = false;
                newCatalogItemCategoryCatalogItem1.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem1.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #region business_rule

                #region Predefined Business rules

                #region Sales History Interval # Day

                // Create new form group object
                business_rule newBusinessRule1 = new business_rule();

                // Fill in new record data
                // newBusinessRule1.business_rule_id;           // Created upon insert
                newBusinessRule1.form_id = newForm.form_id;
                newBusinessRule1.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule1.field_id = 29;                 // 29 = [Interval_NbDay]
                newBusinessRule1.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule1.business_rule_name = "[Interval_NbDay]";
                newBusinessRule1.value_to_compare = "45";
                // newBusinessRule1.form_section_number;        // null, not used
                // newBusinessRule1.description;                // null, not used
                // newBusinessRule1.message;                    // null, not used
                newBusinessRule1.deleted = false;
                newBusinessRule1.create_date = DateTime.Now;
                newBusinessRule1.create_user_id = CREATE_USER_ID;
                newBusinessRule1.update_date = DateTime.Now;
                newBusinessRule1.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Sales History Minimum Total Amount:

                // Create new form group object
                business_rule newBusinessRule2 = new business_rule();

                // Fill in new record data
                // newBusinessRule2.business_rule_id;           // Created upon insert
                newBusinessRule2.form_id = newForm.form_id;
                newBusinessRule2.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule2.field_id = 105;                 // 105 = account_history_min_total_amount
                newBusinessRule2.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule2.business_rule_name = "account_history_min_total_amount";
                newBusinessRule2.value_to_compare = "2000";
                // newBusinessRule2.form_section_number;        // null, not used
                // newBusinessRule2.description;                // null, not used
                // newBusinessRule2.message;                    // null, not used
                newBusinessRule2.deleted = false;
                newBusinessRule2.create_date = DateTime.Now;
                newBusinessRule2.create_user_id = CREATE_USER_ID;
                newBusinessRule2.update_date = DateTime.Now;
                newBusinessRule2.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #endregion

                #region Order Requirements

                #region For All Sections

                #region Standard lead time

                // Create new form group object
                business_rule newBusinessRule3 = new business_rule();

                // Fill in new record data
                // newBusinessRule3.business_rule_id;           // Created upon insert
                newBusinessRule3.form_id = newForm.form_id;
                newBusinessRule3.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule3.field_id = 35;                 // 35 = min_nb_day_lead_time
                newBusinessRule3.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule3.business_rule_name = "min_nb_day_lead_time";
                newBusinessRule3.value_to_compare = "2";
                // newBusinessRule3.form_section_number;        // null, not used
                // newBusinessRule3.description;                // null, not used
                // newBusinessRule3.message;                    // null, not used
                newBusinessRule3.deleted = false;
                newBusinessRule3.create_date = DateTime.Now;
                newBusinessRule3.create_user_id = CREATE_USER_ID;
                newBusinessRule3.update_date = DateTime.Now;
                newBusinessRule3.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule3);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Minimum Total Quantity

                // Create new form group object
                business_rule newBusinessRule4 = new business_rule();

                // Fill in new record data
                // newBusinessRule4.business_rule_id;           // Created upon insert
                newBusinessRule4.form_id = newForm.form_id;
                newBusinessRule4.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule4.field_id = 49;                 // 49 = min_total_quantity
                newBusinessRule4.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule4.business_rule_name = "min_total_quantity";
                newBusinessRule4.value_to_compare = "2";
                // newBusinessRule4.form_section_number;        // null, not used
                // newBusinessRule4.description;                // null, not used
                // newBusinessRule4.message;                    // null, not used
                newBusinessRule4.deleted = false;
                newBusinessRule4.create_date = DateTime.Now;
                newBusinessRule4.create_user_id = CREATE_USER_ID;
                newBusinessRule4.update_date = DateTime.Now;
                newBusinessRule4.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule4);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Minimum Total Amount

                // Create new form group object
                business_rule newBusinessRule5 = new business_rule();

                // Fill in new record data
                // newBusinessRule5.business_rule_id;           // Created upon insert
                newBusinessRule5.form_id = newForm.form_id;
                newBusinessRule5.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule5.field_id = 45;                 // 45 = min_total_amount
                newBusinessRule5.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule5.business_rule_name = "min_total_amount";
                newBusinessRule5.value_to_compare = "2000";
                // newBusinessRule5.form_section_number;        // null, not used
                // newBusinessRule5.description;                // null, not used
                // newBusinessRule5.message;                    // null, not used
                newBusinessRule5.deleted = false;
                newBusinessRule5.create_date = DateTime.Now;
                newBusinessRule5.create_user_id = CREATE_USER_ID;
                newBusinessRule5.update_date = DateTime.Now;
                newBusinessRule5.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule5);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Maximum Total Amount

                // Create new form group object
                business_rule newBusinessRule6 = new business_rule();

                // Fill in new record data
                // newBusinessRule6.business_rule_id;           // Created upon insert
                newBusinessRule6.form_id = newForm.form_id;
                newBusinessRule6.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule6.field_id = 108;                 // 108 = max_total_amount
                newBusinessRule6.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule6.business_rule_name = "max_total_amount";
                newBusinessRule6.value_to_compare = "50000";
                // newBusinessRule6.form_section_number;        // null, not used
                // newBusinessRule6.description;                // null, not used
                // newBusinessRule6.message;                    // null, not used
                newBusinessRule6.deleted = false;
                newBusinessRule6.create_date = DateTime.Now;
                newBusinessRule6.create_user_id = CREATE_USER_ID;
                newBusinessRule6.update_date = DateTime.Now;
                newBusinessRule6.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #endregion

                #endregion

                #endregion

                #region business_exception

                #region Standard product - shipping fees above and below $2,000

                // Create new form group object
                business_exception newBusinessException1 = new business_exception();

                // Fill in new record data
                //newBusinessException1.business_exception_id;      // newBusinessException1
                newBusinessException1.form_id = newForm.form_id;
                newBusinessException1.business_exception_name = "Standard product - shipping fees above and below $2,000";
                newBusinessException1.exception_type_id = 102;      // 102 = Freight charges
                newBusinessException1.entity_type_id = 4;           // 4 = Order
                newBusinessException1.warning_message = "Based on <u>Actual</u> 'Ship' Dates:  If <u>Initial</u> Order is <u>Less</u> Than $2,000 Net - $50 Freight Charge Applies.  If <u>Initial</u> Order is <u>Greater</u> Than $2,000 Net or <u>Addt'l</u> Orders (<u>Less</u> Than $2,000 Net) Ship w/i 45 Business Days - $50 Freight Charge Is Waived.";
                newBusinessException1.app_item_id = 24;             // 24 = Order Form Step 4 - Order Detail
                newBusinessException1.message = "Based on <u>Actual</u> 'Ship' Dates:  If <u>Initial</u> Order is <u>Less</u> Than $2,000 Net - $50 Freight Charge Applies.  If <u>Initial</u> Order is <u>Greater</u> Than $2,000 Net or <u>Addt'l</u> Orders (<u>Less</u> Than $2,000 Net) Ship w/i 45 Business Days - $50 Freight Charge Is Waived.";
                newBusinessException1.exception_expression = "(([total_amount] <  [min_total_amount]) AND ([account_history_interval_max_total_amount] <  [account_history_min_total_amount])) AND ([delivery_method_id] = [common_carrier])";
                newBusinessException1.fees_value_expression = "50";
                newBusinessException1.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException1.form_section_number;       // null, not used
                // newBusinessException1.business_rule_id;          // null, not used
                newBusinessException1.deleted = false;
                newBusinessException1.create_date = DateTime.Now;
                newBusinessException1.create_user_id = CREATE_USER_ID;
                newBusinessException1.update_date = DateTime.Now;
                newBusinessException1.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #region Standard product - minimum day lead time

                // Create new form group object
                business_exception newBusinessException2 = new business_exception();

                // Fill in new record data
                //newBusinessException2.business_exception_id;      // newBusinessException1
                newBusinessException2.form_id = newForm.form_id;
                newBusinessException2.business_exception_name = "Standard product - minimum day lead time";
                newBusinessException2.exception_type_id = 100;      // 100 = Note
                newBusinessException2.entity_type_id = 4;           // 4 = Order
                newBusinessException2.warning_message = "4 Business Days Lead-Time Required.<BR>If requested Lead-Time is <u>LESS</u> than 4 business Days, change the delivery date to meet this requirement.";
                newBusinessException2.app_item_id = 23;             // 23 = Order Form Step 3 - Information
                newBusinessException2.message = "For Product Orders by Common Carrier <u>LESS</u> than 4 Business Days cannot be guaranteed. ";
                newBusinessException2.exception_expression = "[nb_day_lead_time] <  [min_nb_day_lead_time]";
                newBusinessException2.fees_value_expression = "";
                newBusinessException2.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException2.form_section_number;       // null, not used
                // newBusinessException2.business_rule_id;          // null, not used
                newBusinessException2.deleted = false;
                newBusinessException2.create_date = DateTime.Now;
                newBusinessException2.create_user_id = CREATE_USER_ID;
                newBusinessException2.update_date = DateTime.Now;
                newBusinessException2.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #region Standard product - maximum total sales notification

                // Create new form group object
                business_exception newBusinessException4 = new business_exception();

                // Fill in new record data
                //newBusinessException4.business_exception_id;      // newBusinessException1
                newBusinessException4.form_id = newForm.form_id;
                newBusinessException4.business_exception_name = "Standard product - maximum total sales notification";
                newBusinessException4.exception_type_id = 100;      // 100 = Note
                newBusinessException4.entity_type_id = 4;           // 4 = Order
                newBusinessException4.warning_message = "";
                // newBusinessException4.app_item_id;               // null, not used
                newBusinessException4.message = "The Order Total Amount is greather or equal to $50,000.";
                newBusinessException4.exception_expression = "[total_amount] >= [max_total_amount]";
                newBusinessException4.fees_value_expression = "";
                newBusinessException4.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException4.form_section_number;       // null, not used
                // newBusinessException4.business_rule_id;          // null, not used
                newBusinessException4.deleted = false;
                newBusinessException4.create_date = DateTime.Now;
                newBusinessException4.create_user_id = CREATE_USER_ID;
                newBusinessException4.update_date = DateTime.Now;
                newBusinessException4.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException4);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #region Standard product - expedited freight charges

                // Create new form group object
                business_exception newBusinessException5 = new business_exception();

                // Fill in new record data
                //newBusinessException5.business_exception_id;      // newBusinessException1
                newBusinessException5.form_id = newForm.form_id;
                newBusinessException5.business_exception_name = "Standard product - expedited freight charges";
                newBusinessException5.exception_type_id = 103;      // 103 = Expedited freight charges
                newBusinessException5.entity_type_id = 4;           // 4 = Order
                newBusinessException5.warning_message = "";
                // newBusinessException5.app_item_id;               // null, not used
                newBusinessException5.message = "For Product Orders by Common Carrier, <u>LESS</u> than 4 Business Days, if there are any expedited freight charges relating to this order they will be recovered from the employee's 12-pay.";
                newBusinessException5.exception_expression = "([nb_day_lead_time] <  [min_nb_day_lead_time]) AND  ([delivery_method_id] = [common_carrier])";
                newBusinessException5.fees_value_expression = "";
                newBusinessException5.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException5.form_section_number;       // null, not used
                // newBusinessException5.business_rule_id;          // null, not used
                newBusinessException5.deleted = false;
                newBusinessException5.create_date = DateTime.Now;
                newBusinessException5.create_user_id = CREATE_USER_ID;
                newBusinessException5.update_date = DateTime.Now;
                newBusinessException5.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException5);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #region Standard product - minimum total quantity

                // Create new form group object
                business_exception newBusinessException7 = new business_exception();

                // Fill in new record data
                //newBusinessException7.business_exception_id;      // newBusinessException1
                newBusinessException7.form_id = newForm.form_id;
                newBusinessException7.business_exception_name = "Standard product - minimum total quantity";
                newBusinessException7.exception_type_id = 900;      // 900 = Mandatory
                newBusinessException7.entity_type_id = 4;           // 4 = Order
                newBusinessException7.warning_message = "NOTE: Minimum Order - 8 Cases";
                newBusinessException7.app_item_id = 24;             // 24 = Order form step 4 - order detail
                newBusinessException7.message = "NOTE: Minimum Order - 8 Cases";
                newBusinessException7.exception_expression = "([total_quantity] <  [min_total_quantity])";
                newBusinessException7.fees_value_expression = "";
                newBusinessException7.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException7.form_section_number;       // null, not used
                // newBusinessException7.business_rule_id;          // null, not used
                newBusinessException7.deleted = false;
                newBusinessException7.create_date = DateTime.Now;
                newBusinessException7.create_user_id = CREATE_USER_ID;
                newBusinessException7.update_date = DateTime.Now;
                newBusinessException7.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException7);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #endregion

                #endregion

                sb.AppendLine("Success!");
            }
            catch (Exception ex)
            {
                //transaction.Rollback();

                sb.AppendLine("transaction rollback due to error");
                sb.AppendLine("Message: " + ex.Message);
                sb.AppendLine("Source: " + ex.Source);
                sb.AppendLine("");
                sb.AppendLine(ex.StackTrace);
            }
            finally
            {
                if (context.Connection != null)
                {
                    context.Connection.Close();
                }
            }

            sb.AppendLine("end");

            return sb.ToString();
        }
        public string Spring2011_Unipak()
        {
            #region Notes

            // To enforce the PA before an order can be made, we need to add code to:
            // QSPForm.WebApp.OrderStep_Selection.BindForm which is located in:
            // OrderExpress/UserControls/OrderStep_Selection.ascx

            // Supply catalogs in PA need one catalog for priced and one catalog for 
            // unpriced items

            #endregion

            #region Start

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("start");

            // Open db context
            QSPFulfillmentDataContext context = new QSPFulfillmentDataContext(CONNECTION_STRING);
            sb.AppendLine("db context open");

            // Open the connection
            context.Connection.Open();

            #endregion

            try
            {
                #region Order form

                #region form_group

                // Create new form group object
                form_group newFormGroup = new form_group();

                // Fill in new form group data
                // newFormGroup.form_group_id;              // Autonumber generated on insert
                newFormGroup.form_group_name = "Unipak Spring 2011 PFS form group";
                newFormGroup.deleted = false;
                newFormGroup.create_date = DateTime.Now;
                newFormGroup.create_user_id = CREATE_USER_ID;
                newFormGroup.update_date = DateTime.Now;
                newFormGroup.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.form_groups.InsertOnSubmit(newFormGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form group id = " + newFormGroup.form_group_id.ToString());
                // MessageBox.Show("New form group id = " + newFormGroup.form_group_id.ToString());

                #endregion

                #region form

                // Create new form object
                form newForm = new form();

                // Fill in new form data
                // newForm.form_id;                             // Autonumber generated on insert
                newForm.form_group_id = newFormGroup.form_group_id;
                newForm.entity_type_id = 4;                     // Type 4 = order
                newForm.form_code = "MC16";
                newForm.form_name = "Unipak Spring 2011 PFS order form";
                newForm.description = "";
                newForm.order_terms_text = "";
                newForm.start_date = new DateTime(2010, 12, 8);
                newForm.end_date = new DateTime(2011, 6, 30);
                newForm.closing_time = new DateTime(2011, 6, 30);
                newForm.image_url = "images/CatalogItem/chocochunk.gif";
                newForm.is_base_form = false;
                newForm.parent_form_id = 8;                     // Base form for orders in prod and dev
                newForm.is_product_price_updatable = false;
                newForm.is_quantity_adjustment_allowed = true;
                newForm.tax_postal_address_type_id = 2;
                newForm.enabled = true;
                newForm.deleted = false;
                newForm.version = 1;
                newForm.program_id = 4;                         // Otis = 3, PV = 2, UP = 4, MP = 5
                newForm.program_type_id = 7;
                newForm.program_basics_text = "";
                newForm.create_date = DateTime.Now;
                newForm.create_user_id = CREATE_USER_ID;
                newForm.update_date = DateTime.Now;
                newForm.update_user_id = CREATE_USER_ID;
                //newForm.warehouse_type_id;
                newForm.is_bulk = false;
                newForm.report_name = "OrderForm";
                //newForm.is_warehouse_selectable = false;
                //newForm.default_warehouse_id;
                newForm.season_id = 8;

                // Add the new form to the db context
                context.forms.InsertOnSubmit(newForm);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form id = " + newForm.form_id.ToString());
                // MessageBox.Show("New form id = " + newForm.form_id.ToString());

                #endregion

                #region form_permission

                form_permission newFormPermission1 = new form_permission();
                //newFormPermission1.form_permission_id;            // Autonumber generated on insert
                newFormPermission1.form_id = newForm.form_id;
                newFormPermission1.role_id = 0;                     // User
                newFormPermission1.allow_read = false;
                newFormPermission1.allow_write = false;
                newFormPermission1.create_date = DateTime.Now;
                newFormPermission1.create_user_id = CREATE_USER_ID;
                newFormPermission1.update_date = DateTime.Now;
                newFormPermission1.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission2 = new form_permission();
                //newFormPermission2.form_permission_id;            // Autonumber generated on insert
                newFormPermission2.form_id = newForm.form_id;
                newFormPermission2.role_id = 1;                     // Field Sale Manager (FM)
                newFormPermission2.allow_read = false;
                newFormPermission2.allow_write = false;
                newFormPermission2.create_date = DateTime.Now;
                newFormPermission2.create_user_id = CREATE_USER_ID;
                newFormPermission2.update_date = DateTime.Now;
                newFormPermission2.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission3 = new form_permission();
                //newFormPermission3.form_permission_id;            // Autonumber generated on insert
                newFormPermission3.form_id = newForm.form_id;
                newFormPermission3.role_id = 2;                     // Field Support
                newFormPermission3.allow_read = false;
                newFormPermission3.allow_write = false;
                newFormPermission3.create_date = DateTime.Now;
                newFormPermission3.create_user_id = CREATE_USER_ID;
                newFormPermission3.update_date = DateTime.Now;
                newFormPermission3.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission4 = new form_permission();
                //newFormPermission4.form_permission_id;            // Autonumber generated on insert
                newFormPermission4.form_id = newForm.form_id;
                newFormPermission4.role_id = 3;                     // Accounting Manager
                newFormPermission4.allow_read = false;
                newFormPermission4.allow_write = false;
                newFormPermission4.create_date = DateTime.Now;
                newFormPermission4.create_user_id = CREATE_USER_ID;
                newFormPermission4.update_date = DateTime.Now;
                newFormPermission4.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission5 = new form_permission();
                //newFormPermission5.form_permission_id;            // Autonumber generated on insert
                newFormPermission5.form_id = newForm.form_id;
                newFormPermission5.role_id = 4;                     // Admin
                newFormPermission5.allow_read = false;
                newFormPermission5.allow_write = false;
                newFormPermission5.create_date = DateTime.Now;
                newFormPermission5.create_user_id = CREATE_USER_ID;
                newFormPermission5.update_date = DateTime.Now;
                newFormPermission5.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission6 = new form_permission();
                //newFormPermission6.form_permission_id;            // Autonumber generated on insert
                newFormPermission6.form_id = newForm.form_id;
                newFormPermission6.role_id = 5;                     // Super User
                newFormPermission6.allow_read = false;
                newFormPermission6.allow_write = false;
                newFormPermission6.create_date = DateTime.Now;
                newFormPermission6.create_user_id = CREATE_USER_ID;
                newFormPermission6.update_date = DateTime.Now;
                newFormPermission6.update_user_id = CREATE_USER_ID;

                // Add the new form permissions to the db context
                context.form_permissions.InsertOnSubmit(newFormPermission1);
                context.form_permissions.InsertOnSubmit(newFormPermission2);
                context.form_permissions.InsertOnSubmit(newFormPermission3);
                context.form_permissions.InsertOnSubmit(newFormPermission4);
                context.form_permissions.InsertOnSubmit(newFormPermission5);
                context.form_permissions.InsertOnSubmit(newFormPermission6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form permissions ok");

                #endregion

                #region form_delivery_date_type

                form_delivery_date_type newFormDeliveryDateType1 = new form_delivery_date_type();
                //newFormDeliveryDateType1.form_delivery_date_type_id;  // Autonumber generated on insert
                newFormDeliveryDateType1.form_id = newForm.form_id;
                newFormDeliveryDateType1.delivery_date_type_id = 1;     // Choose a date

                context.form_delivery_date_types.InsertOnSubmit(newFormDeliveryDateType1);
                context.SubmitChanges();

                sb.AppendLine("form delivery date type ok");

                #endregion

                #region form_profit_rate

                form_profit_rate newFormProfitRate1 = new form_profit_rate();
                //newFormProfitRate1.form_profit_rate_id;                // Autonumber generated on insert
                newFormProfitRate1.form_id = newForm.form_id;
                newFormProfitRate1.profit_rate_id = 1;         // 1 = 40%, 2 = 45%, 3 = 50%
                newFormProfitRate1.deleted = false;
                newFormProfitRate1.create_date = DateTime.Now;
                newFormProfitRate1.create_user_id = CREATE_USER_ID;
                newFormProfitRate1.update_date = DateTime.Now;
                newFormProfitRate1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_profit_rates.InsertOnSubmit(newFormProfitRate1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form profit rate ok");

                #endregion

                #region form_order_type

                // Create new form order type object
                form_order_type newFormOrderType1 = new form_order_type();
                // newFormOrderType1.form_order_type_id;            // Autonumber generated on insert
                newFormOrderType1.form_id = newForm.form_id;
                newFormOrderType1.order_type_id = 1;                // 1 = standard order
                newFormOrderType1.deleted = false;
                newFormOrderType1.create_date = DateTime.Now;
                newFormOrderType1.create_user_id = CREATE_USER_ID;
                newFormOrderType1.update_date = DateTime.Now;
                newFormOrderType1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_order_types.InsertOnSubmit(newFormOrderType1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form order type ok");

                #endregion

                #region form_delivery_method

                // Create new record
                form_delivery_method newFormDeliveryMethod1 = new form_delivery_method();
                // newFormDeliveryMethod1.form_delivery_method_id;          // Autonumber generated on insert
                newFormDeliveryMethod1.delivery_method_id = 1;              // Common carrier
                newFormDeliveryMethod1.form_id = newForm.form_id;
                newFormDeliveryMethod1.deleted = false;
                newFormDeliveryMethod1.create_date = DateTime.Now;
                newFormDeliveryMethod1.create_user_id = CREATE_USER_ID;
                newFormDeliveryMethod1.update_date = DateTime.Now;
                newFormDeliveryMethod1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_delivery_methods.InsertOnSubmit(newFormDeliveryMethod1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form delivery methods ok");

                #endregion

                #region catalog_group

                // Create new record
                catalog_group newCatalogGroup = new catalog_group();
                // newCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newCatalogGroup.catalog_group_code = "MC16";
                newCatalogGroup.catalog_group_name = "Unipak Spring 2011 PFS order form";
                newCatalogGroup.description = "";
                newCatalogGroup.deleted = false;
                newCatalogGroup.create_date = DateTime.Now;
                newCatalogGroup.create_user_id = CREATE_USER_ID;
                newCatalogGroup.update_date = DateTime.Now;
                newCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region catalog

                // Create new record
                catalog newCatalog = new catalog();
                // newCatalog.newCatalog_id;          // Autonumber generated on insert
                newCatalog.catalog_group_id = newCatalogGroup.catalog_group_id;
                newCatalog.catalog_code = "MC16";
                newCatalog.catalog_name = "Unipak Spring 2011 PFS order form";
                newCatalog.culture = "en-US";
                newCatalog.description = "";
                newCatalog.start_date = new DateTime(2010, 12, 8);
                newCatalog.end_date = new DateTime(2011, 6, 30);
                newCatalog.deleted = false;
                newCatalog.create_date = DateTime.Now;
                newCatalog.create_user_id = CREATE_USER_ID;
                newCatalog.update_date = DateTime.Now;
                newCatalog.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newCatalog);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newCatalog.catalog_id.ToString());

                #endregion

                #region catalog_item_category

                // Create new record
                catalog_item_category newCatalogItemCategory1 = new catalog_item_category();
                // newCatalogItemCategory1.catalog_item_category_id;         // Autonumber generated on insert
                newCatalogItemCategory1.catalog_id = newCatalog.catalog_id;
                newCatalogItemCategory1.catalog_item_category_name = "Unipak Spring 2011 PFS";
                newCatalogItemCategory1.parent_catalog_item_category_id = 0;
                newCatalogItemCategory1.deleted = false;
                newCatalogItemCategory1.create_date = DateTime.Now;
                newCatalogItemCategory1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategory1.update_date = DateTime.Now;
                newCatalogItemCategory1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newCatalogItemCategory1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newCatalogItemCategory1.catalog_item_category_id.ToString());

                #endregion

                #region form_section

                // Create new record
                form_section newFormSection1 = new form_section();
                // newFormSection1.form_section_id;              // Autonumber generated on insert
                newFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newFormSection1.form_id = newForm.form_id;
                newFormSection1.form_section_type_id = 1;        // 1 = standard product, 2 = supply product, 3 = optional product
                newFormSection1.form_section_number = 1;
                newFormSection1.form_section_title = "Unipak Spring 2011 PFS";
                newFormSection1.description = "";
                newFormSection1.deleted = false;
                newFormSection1.create_date = DateTime.Now;
                newFormSection1.create_user_id = CREATE_USER_ID;
                newFormSection1.update_date = DateTime.Now;
                newFormSection1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newFormSection1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newFormSection1.form_section_id.ToString());

                #endregion

                #region business_rule

                #region Predefined Business rules

                #region Sales History Interval # Day

                // Create new form group object
                business_rule newBusinessRule1 = new business_rule();

                // Fill in new record data
                // newBusinessRule1.business_rule_id;           // Created upon insert
                newBusinessRule1.form_id = newForm.form_id;
                newBusinessRule1.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule1.field_id = 29;                 // 29 = [Interval_NbDay]
                newBusinessRule1.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule1.business_rule_name = "[Interval_NbDay]";
                newBusinessRule1.value_to_compare = "45";
                // newBusinessRule1.form_section_number;        // null, not used
                // newBusinessRule1.description;                // null, not used
                // newBusinessRule1.message;                    // null, not used
                newBusinessRule1.deleted = false;
                newBusinessRule1.create_date = DateTime.Now;
                newBusinessRule1.create_user_id = CREATE_USER_ID;
                newBusinessRule1.update_date = DateTime.Now;
                newBusinessRule1.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                //context.business_rules.InsertOnSubmit(newBusinessRule1);

                // Submit changes to the db
                //context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Sales History Minimum Total Amount:

                // Create new form group object
                business_rule newBusinessRule2 = new business_rule();

                // Fill in new record data
                // newBusinessRule2.business_rule_id;           // Created upon insert
                newBusinessRule2.form_id = newForm.form_id;
                newBusinessRule2.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule2.field_id = 105;                 // 105 = account_history_min_total_amount
                newBusinessRule2.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule2.business_rule_name = "account_history_min_total_amount";
                newBusinessRule2.value_to_compare = "2000";
                // newBusinessRule2.form_section_number;        // null, not used
                // newBusinessRule2.description;                // null, not used
                // newBusinessRule2.message;                    // null, not used
                newBusinessRule2.deleted = false;
                newBusinessRule2.create_date = DateTime.Now;
                newBusinessRule2.create_user_id = CREATE_USER_ID;
                newBusinessRule2.update_date = DateTime.Now;
                newBusinessRule2.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                //context.business_rules.InsertOnSubmit(newBusinessRule2);

                // Submit changes to the db
                //context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #endregion

                #region Order Requirements

                #region For All Sections

                #region Standard lead time

                // Create new form group object
                business_rule newBusinessRule3 = new business_rule();

                // Fill in new record data
                // newBusinessRule3.business_rule_id;           // Created upon insert
                newBusinessRule3.form_id = newForm.form_id;
                newBusinessRule3.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule3.field_id = 35;                 // 35 = min_nb_day_lead_time
                newBusinessRule3.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule3.business_rule_name = "min_nb_day_lead_time";
                newBusinessRule3.value_to_compare = "15";
                // newBusinessRule3.form_section_number;        // null, not used
                // newBusinessRule3.description;                // null, not used
                // newBusinessRule3.message;                    // null, not used
                newBusinessRule3.deleted = false;
                newBusinessRule3.create_date = DateTime.Now;
                newBusinessRule3.create_user_id = CREATE_USER_ID;
                newBusinessRule3.update_date = DateTime.Now;
                newBusinessRule3.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule3);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Minimum Total Quantity

                // Create new form group object
                business_rule newBusinessRule4 = new business_rule();

                // Fill in new record data
                // newBusinessRule4.business_rule_id;           // Created upon insert
                newBusinessRule4.form_id = newForm.form_id;
                newBusinessRule4.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule4.field_id = 49;                 // 49 = min_total_quantity
                newBusinessRule4.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule4.business_rule_name = "min_total_quantity";
                newBusinessRule4.value_to_compare = "20";
                // newBusinessRule4.form_section_number;        // null, not used
                // newBusinessRule4.description;                // null, not used
                // newBusinessRule4.message;                    // null, not used
                newBusinessRule4.deleted = false;
                newBusinessRule4.create_date = DateTime.Now;
                newBusinessRule4.create_user_id = CREATE_USER_ID;
                newBusinessRule4.update_date = DateTime.Now;
                newBusinessRule4.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule4);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Minimum Total Amount

                // Create new form group object
                business_rule newBusinessRule5 = new business_rule();

                // Fill in new record data
                // newBusinessRule5.business_rule_id;           // Created upon insert
                newBusinessRule5.form_id = newForm.form_id;
                newBusinessRule5.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule5.field_id = 45;                 // 45 = min_total_amount
                newBusinessRule5.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule5.business_rule_name = "min_total_amount";
                newBusinessRule5.value_to_compare = "2000";
                // newBusinessRule5.form_section_number;        // null, not used
                // newBusinessRule5.description;                // null, not used
                // newBusinessRule5.message;                    // null, not used
                newBusinessRule5.deleted = false;
                newBusinessRule5.create_date = DateTime.Now;
                newBusinessRule5.create_user_id = CREATE_USER_ID;
                newBusinessRule5.update_date = DateTime.Now;
                newBusinessRule5.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                //context.business_rules.InsertOnSubmit(newBusinessRule5);

                // Submit changes to the db
                //context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Maximum Total Amount

                // Create new form group object
                business_rule newBusinessRule6 = new business_rule();

                // Fill in new record data
                // newBusinessRule6.business_rule_id;           // Created upon insert
                newBusinessRule6.form_id = newForm.form_id;
                newBusinessRule6.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule6.field_id = 108;                 // 108 = max_total_amount
                newBusinessRule6.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule6.business_rule_name = "max_total_amount";
                newBusinessRule6.value_to_compare = "50000";
                // newBusinessRule6.form_section_number;        // null, not used
                // newBusinessRule6.description;                // null, not used
                // newBusinessRule6.message;                    // null, not used
                newBusinessRule6.deleted = false;
                newBusinessRule6.create_date = DateTime.Now;
                newBusinessRule6.create_user_id = CREATE_USER_ID;
                newBusinessRule6.update_date = DateTime.Now;
                newBusinessRule6.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                //context.business_rules.InsertOnSubmit(newBusinessRule6);

                // Submit changes to the db
                //context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #endregion

                #endregion

                #endregion

                #region business_exception

                #region Standard product - minimum day lead time

                // Create new form group object
                business_exception newBusinessException2 = new business_exception();

                // Fill in new record data
                //newBusinessException2.business_exception_id;      // newBusinessException1
                newBusinessException2.form_id = newForm.form_id;
                newBusinessException2.business_exception_name = "Standard product - minimum day lead time";
                newBusinessException2.exception_type_id = 100;      // 100 = Note
                newBusinessException2.entity_type_id = 4;           // 4 = Order
                newBusinessException2.warning_message = "15 Business Days Lead-Time Required.<BR>If requested Lead-Time is <u>LESS</u> than 15 business Days, change the delivery date to meet this requirement.";
                newBusinessException2.app_item_id = 23;             // 23 = Order Form Step 3 - Information
                newBusinessException2.message = "For Product Orders by Common Carrier <u>LESS</u> than 15 Business Days cannot be guaranteed. ";
                newBusinessException2.exception_expression = "[nb_day_lead_time] <  [min_nb_day_lead_time]";
                newBusinessException2.fees_value_expression = "";
                newBusinessException2.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException2.form_section_number;       // null, not used
                // newBusinessException2.business_rule_id;          // null, not used
                newBusinessException2.deleted = false;
                newBusinessException2.create_date = DateTime.Now;
                newBusinessException2.create_user_id = CREATE_USER_ID;
                newBusinessException2.update_date = DateTime.Now;
                newBusinessException2.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #region Standard product - minimum total quantity

                // Create new form group object
                business_exception newBusinessException7 = new business_exception();

                // Fill in new record data
                //newBusinessException7.business_exception_id;      // newBusinessException1
                newBusinessException7.form_id = newForm.form_id;
                newBusinessException7.business_exception_name = "Standard product - minimum total quantity";
                newBusinessException7.exception_type_id = 900;      // 900 = Mandatory
                newBusinessException7.entity_type_id = 4;           // 4 = Order
                newBusinessException7.warning_message = "NOTE: Minimum Order - 20 Cases";
                newBusinessException7.app_item_id = 24;             // 24 = Order form step 4 - order detail
                newBusinessException7.message = "NOTE: Minimum Order - 20 Cases";
                newBusinessException7.exception_expression = "([total_quantity] <  [min_total_quantity])";
                newBusinessException7.fees_value_expression = "";
                newBusinessException7.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException7.form_section_number;       // null, not used
                // newBusinessException7.business_rule_id;          // null, not used
                newBusinessException7.deleted = false;
                newBusinessException7.create_date = DateTime.Now;
                newBusinessException7.create_user_id = CREATE_USER_ID;
                newBusinessException7.update_date = DateTime.Now;
                newBusinessException7.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException7);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #endregion

                #endregion

                #region PA form

                #region form

                // Create new form object
                form newPAForm = new form();

                // Fill in new form data
                // newPAForm.form_id;                             // Autonumber generated on insert
                newPAForm.form_group_id = newFormGroup.form_group_id;
                newPAForm.entity_type_id = 12;                     // Type 12 = program agreement
                newPAForm.form_code = "MC16";
                newPAForm.form_name = "Unipak Spring 2011 PFS Program Agreement";
                newPAForm.description = "";
                newPAForm.order_terms_text = "";
                newPAForm.start_date = new DateTime(2010, 12, 8);
                newPAForm.end_date = new DateTime(2011, 6, 30);
                newPAForm.closing_time = new DateTime(2011, 6, 30);
                newPAForm.image_url = "images/CatalogItem/chocochunk.gif";
                newPAForm.is_base_form = false;
                newPAForm.parent_form_id = 48;                     // Base form for orders in prod and dev
                newPAForm.is_product_price_updatable = false;
                newPAForm.is_quantity_adjustment_allowed = true;
                newPAForm.tax_postal_address_type_id = 2;
                newPAForm.enabled = true;
                newPAForm.deleted = false;
                newPAForm.version = 1;
                newPAForm.program_id = 4;                         // Otis = 3, PV = 2, UP = 4, MP = 5
                newPAForm.program_type_id = 7;
                newPAForm.program_basics_text = "";
                newPAForm.create_date = DateTime.Now;
                newPAForm.create_user_id = CREATE_USER_ID;
                newPAForm.update_date = DateTime.Now;
                newPAForm.update_user_id = CREATE_USER_ID;
                //newPAForm.warehouse_type_id;
                newPAForm.is_bulk = false;
                newPAForm.report_name = "OrderForm";
                //newPAForm.is_warehouse_selectable = true;
                //newPAForm.default_warehouse_id;
                newPAForm.season_id = 8;

                // Add the new form to the db context
                context.forms.InsertOnSubmit(newPAForm);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form id = " + newPAForm.form_id.ToString());

                #endregion

                #region form_permission

                form_permission newPAFormPermission1 = new form_permission();
                //newPAFormPermission1.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission1.form_id = newPAForm.form_id;
                newPAFormPermission1.role_id = 0;                     // User
                newPAFormPermission1.allow_read = false;
                newPAFormPermission1.allow_write = false;
                newPAFormPermission1.create_date = DateTime.Now;
                newPAFormPermission1.create_user_id = CREATE_USER_ID;
                newPAFormPermission1.update_date = DateTime.Now;
                newPAFormPermission1.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission2 = new form_permission();
                //newPAFormPermission2.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission2.form_id = newPAForm.form_id;
                newPAFormPermission2.role_id = 1;                     // Field Sale Manager (FM)
                newPAFormPermission2.allow_read = true;
                newPAFormPermission2.allow_write = true;
                newPAFormPermission2.create_date = DateTime.Now;
                newPAFormPermission2.create_user_id = CREATE_USER_ID;
                newPAFormPermission2.update_date = DateTime.Now;
                newPAFormPermission2.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission3 = new form_permission();
                //newPAFormPermission3.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission3.form_id = newPAForm.form_id;
                newPAFormPermission3.role_id = 2;                     // Field Support
                newPAFormPermission3.allow_read = true;
                newPAFormPermission3.allow_write = true;
                newPAFormPermission3.create_date = DateTime.Now;
                newPAFormPermission3.create_user_id = CREATE_USER_ID;
                newPAFormPermission3.update_date = DateTime.Now;
                newPAFormPermission3.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission4 = new form_permission();
                //newPAFormPermission4.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission4.form_id = newPAForm.form_id;
                newPAFormPermission4.role_id = 3;                     // Accounting Manager
                newPAFormPermission4.allow_read = true;
                newPAFormPermission4.allow_write = true;
                newPAFormPermission4.create_date = DateTime.Now;
                newPAFormPermission4.create_user_id = CREATE_USER_ID;
                newPAFormPermission4.update_date = DateTime.Now;
                newPAFormPermission4.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission5 = new form_permission();
                //newPAFormPermission5.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission5.form_id = newPAForm.form_id;
                newPAFormPermission5.role_id = 4;                     // Admin
                newPAFormPermission5.allow_read = true;
                newPAFormPermission5.allow_write = true;
                newPAFormPermission5.create_date = DateTime.Now;
                newPAFormPermission5.create_user_id = CREATE_USER_ID;
                newPAFormPermission5.update_date = DateTime.Now;
                newPAFormPermission5.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission6 = new form_permission();
                //newPAFormPermission6.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission6.form_id = newPAForm.form_id;
                newPAFormPermission6.role_id = 5;                     // Super User
                newPAFormPermission6.allow_read = true;
                newPAFormPermission6.allow_write = true;
                newPAFormPermission6.create_date = DateTime.Now;
                newPAFormPermission6.create_user_id = CREATE_USER_ID;
                newPAFormPermission6.update_date = DateTime.Now;
                newPAFormPermission6.update_user_id = CREATE_USER_ID;

                // Add the new form permissions to the db context
                context.form_permissions.InsertOnSubmit(newPAFormPermission1);
                context.form_permissions.InsertOnSubmit(newPAFormPermission2);
                context.form_permissions.InsertOnSubmit(newPAFormPermission3);
                context.form_permissions.InsertOnSubmit(newPAFormPermission4);
                context.form_permissions.InsertOnSubmit(newPAFormPermission5);
                context.form_permissions.InsertOnSubmit(newPAFormPermission6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form permissions ok");

                #endregion

                #region form_profit_rate

                form_profit_rate newPAFormProfitRate1 = new form_profit_rate();
                //newPAFormProfitRate1.form_profit_rate_id;                // Autonumber generated on insert
                newPAFormProfitRate1.form_id = newPAForm.form_id;
                newPAFormProfitRate1.profit_rate_id = 1;         // 1 = 40%, 2 = 45%, 3 = 50%
                newPAFormProfitRate1.deleted = false;
                newPAFormProfitRate1.create_date = DateTime.Now;
                newPAFormProfitRate1.create_user_id = CREATE_USER_ID;
                newPAFormProfitRate1.update_date = DateTime.Now;
                newPAFormProfitRate1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_profit_rates.InsertOnSubmit(newPAFormProfitRate1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form profit rate ok");

                #endregion

                #region form_section

                // Create new record
                form_section newPAFormSection1 = new form_section();
                // newPAFormSection1.form_section_id;              // Autonumber generated on insert
                newPAFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newPAFormSection1.form_id = newPAForm.form_id;
                newPAFormSection1.form_section_type_id = 1;        // 1 = standard product, 2 = supply product, 3 = optional product
                newPAFormSection1.form_section_number = 1;
                newPAFormSection1.form_section_title = "Unipak Spring 2011 PFS";
                newPAFormSection1.description = "";
                newPAFormSection1.deleted = false;
                newPAFormSection1.create_date = DateTime.Now;
                newPAFormSection1.create_user_id = CREATE_USER_ID;
                newPAFormSection1.update_date = DateTime.Now;
                newPAFormSection1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newPAFormSection1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newPAFormSection1.form_section_id.ToString());

                #endregion

                #region Supplies in PA

                #region catalog_group

                // Create new record
                catalog_group newSupplyCatalogGroup = new catalog_group();
                // newSupplyCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newSupplyCatalogGroup.catalog_group_code = "MC16";
                newSupplyCatalogGroup.catalog_group_name = "Unipak Spring 2011 PFS Supplies";
                newSupplyCatalogGroup.description = "";
                newSupplyCatalogGroup.deleted = false;
                newSupplyCatalogGroup.create_date = DateTime.Now;
                newSupplyCatalogGroup.create_user_id = CREATE_USER_ID;
                newSupplyCatalogGroup.update_date = DateTime.Now;
                newSupplyCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newSupplyCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newSupplyCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region product

                #region Create new record

                product newSupplyProduct1 = new product();
                // newSupplyProduct1.product_id;                      // Autonumber generated on insert
                newSupplyProduct1.product_code = "CD2410";
                newSupplyProduct1.product_name = "S11 Snack Happy A PFS Brochure Priced";
                newSupplyProduct1.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct1.nb_units = 1;                       // boxes per case
                newSupplyProduct1.nb_day_lead_time = 0;
                newSupplyProduct1.product_status_id = 101;            // 101 = Active
                newSupplyProduct1.product_type_id = 7;                // 7 = Supply
                newSupplyProduct1.business_division_id = 1;           // 1 = US
                newSupplyProduct1.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct1.coupon_id = null;
                newSupplyProduct1.description = "";
                newSupplyProduct1.image_url = "";
                newSupplyProduct1.is_free_sample = false;
                newSupplyProduct1.oracle_code = "";
                newSupplyProduct1.IVCOUP = "";
                newSupplyProduct1.IVITEM = "CD2410";
                newSupplyProduct1.unit_cost = null;
                newSupplyProduct1.vendor_id = 41;
                newSupplyProduct1.vendor_item_code = "CD2410";
                newSupplyProduct1.deleted = false;
                newSupplyProduct1.create_date = DateTime.Now;
                newSupplyProduct1.create_user_id = CREATE_USER_ID;
                newSupplyProduct1.update_date = DateTime.Now;
                newSupplyProduct1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct2 = new product();
                // newSupplyProduct2.product_id;                      // Autonumber generated on insert
                newSupplyProduct2.product_code = "CD2412";
                newSupplyProduct2.product_name = "S11 Snack Happy A 3-Part NCR Forms";
                newSupplyProduct2.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct2.nb_units = 1;                       // boxes per case
                newSupplyProduct2.nb_day_lead_time = 0;
                newSupplyProduct2.product_status_id = 101;            // 101 = Active
                newSupplyProduct2.product_type_id = 7;                // 7 = Supply
                newSupplyProduct2.business_division_id = 1;           // 1 = US
                newSupplyProduct2.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct2.coupon_id = null;
                newSupplyProduct2.description = "";
                newSupplyProduct2.image_url = "";
                newSupplyProduct2.is_free_sample = false;
                newSupplyProduct2.oracle_code = "";
                newSupplyProduct2.IVCOUP = "";
                newSupplyProduct2.IVITEM = "CD2412";
                newSupplyProduct2.unit_cost = null;
                newSupplyProduct2.vendor_id = 41;
                newSupplyProduct2.vendor_item_code = "CD2412";
                newSupplyProduct2.deleted = false;
                newSupplyProduct2.create_date = DateTime.Now;
                newSupplyProduct2.create_user_id = CREATE_USER_ID;
                newSupplyProduct2.update_date = DateTime.Now;
                newSupplyProduct2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct3 = new product();
                // newSupplyProduct3.product_id;                      // Autonumber generated on insert
                newSupplyProduct3.product_code = "CD2223";
                newSupplyProduct3.product_name = "Snack Happy Promotion Poster (generic)";
                newSupplyProduct3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct3.nb_units = 1;                       // boxes per case
                newSupplyProduct3.nb_day_lead_time = 0;
                newSupplyProduct3.product_status_id = 101;            // 101 = Active
                newSupplyProduct3.product_type_id = 7;                // 7 = Supply
                newSupplyProduct3.business_division_id = 1;           // 1 = US
                newSupplyProduct3.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct3.coupon_id = null;
                newSupplyProduct3.description = "";
                newSupplyProduct3.image_url = "";
                newSupplyProduct3.is_free_sample = false;
                newSupplyProduct3.oracle_code = "";
                newSupplyProduct3.IVCOUP = "";
                newSupplyProduct3.IVITEM = "CD2223";
                newSupplyProduct3.unit_cost = null;
                newSupplyProduct3.vendor_id = 41;
                newSupplyProduct3.vendor_item_code = "CD2223";
                newSupplyProduct3.deleted = false;
                newSupplyProduct3.create_date = DateTime.Now;
                newSupplyProduct3.create_user_id = CREATE_USER_ID;
                newSupplyProduct3.update_date = DateTime.Now;
                newSupplyProduct3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct4 = new product();
                // newSupplyProduct4.product_id;                      // Autonumber generated on insert
                newSupplyProduct4.product_code = "CD2023";
                newSupplyProduct4.product_name = "PFS Homeroom Envelope (1) per class";
                newSupplyProduct4.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct4.nb_units = 1;                       // boxes per case
                newSupplyProduct4.nb_day_lead_time = 0;
                newSupplyProduct4.product_status_id = 101;            // 101 = Active
                newSupplyProduct4.product_type_id = 7;                // 7 = Supply
                newSupplyProduct4.business_division_id = 1;           // 1 = US
                newSupplyProduct4.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct4.coupon_id = null;
                newSupplyProduct4.description = "";
                newSupplyProduct4.image_url = "";
                newSupplyProduct4.is_free_sample = false;
                newSupplyProduct4.oracle_code = "";
                newSupplyProduct4.IVCOUP = "";
                newSupplyProduct4.IVITEM = "CD2023";
                newSupplyProduct4.unit_cost = null;
                newSupplyProduct4.vendor_id = 41;
                newSupplyProduct4.vendor_item_code = "CD2023";
                newSupplyProduct4.deleted = false;
                newSupplyProduct4.create_date = DateTime.Now;
                newSupplyProduct4.create_user_id = CREATE_USER_ID;
                newSupplyProduct4.update_date = DateTime.Now;
                newSupplyProduct4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct5 = new product();
                // newSupplyProduct5.product_id;                      // Autonumber generated on insert
                newSupplyProduct5.product_code = "CD2093";
                newSupplyProduct5.product_name = "QSP Plastic Food Bag";
                newSupplyProduct5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct5.nb_units = 1;                       // boxes per case
                newSupplyProduct5.nb_day_lead_time = 0;
                newSupplyProduct5.product_status_id = 101;            // 101 = Active
                newSupplyProduct5.product_type_id = 7;                // 7 = Supply
                newSupplyProduct5.business_division_id = 1;           // 1 = US
                newSupplyProduct5.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct5.coupon_id = null;
                newSupplyProduct5.description = "";
                newSupplyProduct5.image_url = "";
                newSupplyProduct5.is_free_sample = false;
                newSupplyProduct5.oracle_code = "";
                newSupplyProduct5.IVCOUP = "";
                newSupplyProduct5.IVITEM = "CD2093";
                newSupplyProduct5.unit_cost = null;
                newSupplyProduct5.vendor_id = 41;
                newSupplyProduct5.vendor_item_code = "CD2093";
                newSupplyProduct5.deleted = false;
                newSupplyProduct5.create_date = DateTime.Now;
                newSupplyProduct5.create_user_id = CREATE_USER_ID;
                newSupplyProduct5.update_date = DateTime.Now;
                newSupplyProduct5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct6 = new product();
                // newSupplyProduct6.product_id;                      // Autonumber generated on insert
                newSupplyProduct6.product_code = "CD2098";
                newSupplyProduct6.product_name = "Large Outer Envelope";
                newSupplyProduct6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct6.nb_units = 1;                       // boxes per case
                newSupplyProduct6.nb_day_lead_time = 0;
                newSupplyProduct6.product_status_id = 101;            // 101 = Active
                newSupplyProduct6.product_type_id = 7;                // 7 = Supply
                newSupplyProduct6.business_division_id = 1;           // 1 = US
                newSupplyProduct6.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct6.coupon_id = null;
                newSupplyProduct6.description = "";
                newSupplyProduct6.image_url = "";
                newSupplyProduct6.is_free_sample = false;
                newSupplyProduct6.oracle_code = "";
                newSupplyProduct6.IVCOUP = "";
                newSupplyProduct6.IVITEM = "CD2098";
                newSupplyProduct6.unit_cost = null;
                newSupplyProduct6.vendor_id = 41;
                newSupplyProduct6.vendor_item_code = "CD2098";
                newSupplyProduct6.deleted = false;
                newSupplyProduct6.create_date = DateTime.Now;
                newSupplyProduct6.create_user_id = CREATE_USER_ID;
                newSupplyProduct6.update_date = DateTime.Now;
                newSupplyProduct6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct7 = new product();
                // newSupplyProduct7.product_id;                      // Autonumber generated on insert
                newSupplyProduct7.product_code = "CD2106";
                newSupplyProduct7.product_name = "Medium Collection Envelope";
                newSupplyProduct7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct7.nb_units = 1;                       // boxes per case
                newSupplyProduct7.nb_day_lead_time = 0;
                newSupplyProduct7.product_status_id = 101;            // 101 = Active
                newSupplyProduct7.product_type_id = 7;                // 7 = Supply
                newSupplyProduct7.business_division_id = 1;           // 1 = US
                newSupplyProduct7.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct7.coupon_id = null;
                newSupplyProduct7.description = "";
                newSupplyProduct7.image_url = "";
                newSupplyProduct7.is_free_sample = false;
                newSupplyProduct7.oracle_code = "";
                newSupplyProduct7.IVCOUP = "";
                newSupplyProduct7.IVITEM = "CD2106";
                newSupplyProduct7.unit_cost = null;
                newSupplyProduct7.vendor_id = 41;
                newSupplyProduct7.vendor_item_code = "CD2106";
                newSupplyProduct7.deleted = false;
                newSupplyProduct7.create_date = DateTime.Now;
                newSupplyProduct7.create_user_id = CREATE_USER_ID;
                newSupplyProduct7.update_date = DateTime.Now;
                newSupplyProduct7.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.products.InsertOnSubmit(newSupplyProduct1);
                context.products.InsertOnSubmit(newSupplyProduct2);
                context.products.InsertOnSubmit(newSupplyProduct3);
                context.products.InsertOnSubmit(newSupplyProduct4);
                context.products.InsertOnSubmit(newSupplyProduct5);
                context.products.InsertOnSubmit(newSupplyProduct6);
                context.products.InsertOnSubmit(newSupplyProduct7);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("products ok");

                #endregion

                #region Priced catalog

                // Create new record
                catalog newSupplyCatalogPriced = new catalog();
                // newSupplyCatalogPriced.newCatalog_id;          // Autonumber generated on insert
                newSupplyCatalogPriced.catalog_group_id = newSupplyCatalogGroup.catalog_group_id;
                newSupplyCatalogPriced.catalog_code = "MC16";
                newSupplyCatalogPriced.catalog_name = "Unipak Spring 2011 PFS Supplies Priced";
                newSupplyCatalogPriced.culture = "en-US";
                newSupplyCatalogPriced.description = "";
                newSupplyCatalogPriced.start_date = new DateTime(2010, 12, 8);
                newSupplyCatalogPriced.end_date = new DateTime(2011, 6, 30);
                newSupplyCatalogPriced.deleted = false;
                newSupplyCatalogPriced.create_date = DateTime.Now;
                newSupplyCatalogPriced.create_user_id = CREATE_USER_ID;
                newSupplyCatalogPriced.update_date = DateTime.Now;
                newSupplyCatalogPriced.update_user_id = CREATE_USER_ID;
                newSupplyCatalogPriced.is_priced = true;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newSupplyCatalogPriced);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newSupplyCatalogPriced.catalog_id.ToString());

                #endregion

                #region Priced catalog_item_category

                // Create new record
                catalog_item_category newSupplyCatalogItemCategoryPriced1 = new catalog_item_category();
                // newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryPriced1.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemCategoryPriced1.catalog_item_category_name = "Unipak Spring 2011 PFS Supplies Priced";
                newSupplyCatalogItemCategoryPriced1.parent_catalog_item_category_id = 0;
                newSupplyCatalogItemCategoryPriced1.deleted = false;
                newSupplyCatalogItemCategoryPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newSupplyCatalogItemCategoryPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newSupplyCatalogItemCategoryPriced1.catalog_item_category_id.ToString());

                #endregion

                #region Priced form_section

                // Create new record
                form_section newSupplyPAFormSectionPriced1 = new form_section();
                // newSupplyPAFormSectionPriced1.form_section_id;              // Autonumber generated on insert
                newSupplyPAFormSectionPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyPAFormSectionPriced1.form_id = newPAForm.form_id;
                newSupplyPAFormSectionPriced1.form_section_type_id = 2;        // 1 = standard product, 2 = supply product, 3 = optional product
                newSupplyPAFormSectionPriced1.form_section_number = 2;
                newSupplyPAFormSectionPriced1.form_section_title = "Unipak Spring 2011 PFS Supplies Priced";
                newSupplyPAFormSectionPriced1.description = "";
                newSupplyPAFormSectionPriced1.deleted = false;
                newSupplyPAFormSectionPriced1.create_date = DateTime.Now;
                newSupplyPAFormSectionPriced1.create_user_id = CREATE_USER_ID;
                newSupplyPAFormSectionPriced1.update_date = DateTime.Now;
                newSupplyPAFormSectionPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newSupplyPAFormSectionPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newSupplyPAFormSectionPriced1.form_section_id.ToString());

                #endregion

                #region Priced catalog_item

                #region Create new record

                catalog_item newSupplyCatalogItemPriced1 = new catalog_item();
                // newSupplyCatalogItemPriced1.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced1.product_id = newSupplyProduct1.product_id;
                newSupplyCatalogItemPriced1.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced1.catalog_item_code = "CD2410";
                newSupplyCatalogItemPriced1.catalog_item_name = "S11 Snack Happy A PFS Brochure Priced";
                newSupplyCatalogItemPriced1.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced1.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced1.image_url = "";
                newSupplyCatalogItemPriced1.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced1.catalog_item_export_name = "";
                newSupplyCatalogItemPriced1.description = "";
                newSupplyCatalogItemPriced1.deleted = false;
                newSupplyCatalogItemPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced2 = new catalog_item();
                // newSupplyCatalogItemPriced2.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced2.product_id = newSupplyProduct2.product_id;
                newSupplyCatalogItemPriced2.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced2.catalog_item_code = "CD2412";
                newSupplyCatalogItemPriced2.catalog_item_name = "S11 Snack Happy A 3-Part NCR Forms";
                newSupplyCatalogItemPriced2.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced2.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced2.image_url = "";
                newSupplyCatalogItemPriced2.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced2.catalog_item_export_name = "";
                newSupplyCatalogItemPriced2.description = "";
                newSupplyCatalogItemPriced2.deleted = false;
                newSupplyCatalogItemPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced3 = new catalog_item();
                // newSupplyCatalogItemPriced3.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced3.product_id = newSupplyProduct3.product_id;
                newSupplyCatalogItemPriced3.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced3.catalog_item_code = "CD2223";
                newSupplyCatalogItemPriced3.catalog_item_name = "Snack Happy Promotion Poster (generic)";
                newSupplyCatalogItemPriced3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced3.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced3.image_url = "";
                newSupplyCatalogItemPriced3.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced3.catalog_item_export_name = "";
                newSupplyCatalogItemPriced3.description = "";
                newSupplyCatalogItemPriced3.deleted = false;
                newSupplyCatalogItemPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced4 = new catalog_item();
                // newSupplyCatalogItemPriced4.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced4.product_id = newSupplyProduct4.product_id;
                newSupplyCatalogItemPriced4.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced4.catalog_item_code = "CD2023";
                newSupplyCatalogItemPriced4.catalog_item_name = "PFS Homeroom Envelope (1) per class";
                newSupplyCatalogItemPriced4.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced4.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced4.image_url = "";
                newSupplyCatalogItemPriced4.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced4.catalog_item_export_name = "";
                newSupplyCatalogItemPriced4.description = "";
                newSupplyCatalogItemPriced4.deleted = false;
                newSupplyCatalogItemPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced5 = new catalog_item();
                // newSupplyCatalogItemPriced5.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced5.product_id = newSupplyProduct5.product_id;
                newSupplyCatalogItemPriced5.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced5.catalog_item_code = "CD2093";
                newSupplyCatalogItemPriced5.catalog_item_name = "QSP Plastic Food Bag";
                newSupplyCatalogItemPriced5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced5.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced5.image_url = "";
                newSupplyCatalogItemPriced5.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced5.catalog_item_export_name = "";
                newSupplyCatalogItemPriced5.description = "";
                newSupplyCatalogItemPriced5.deleted = false;
                newSupplyCatalogItemPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced6 = new catalog_item();
                // newSupplyCatalogItemPriced6.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced6.product_id = newSupplyProduct6.product_id;
                newSupplyCatalogItemPriced6.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced6.catalog_item_code = "CD2098";
                newSupplyCatalogItemPriced6.catalog_item_name = "Large Outer Envelope";
                newSupplyCatalogItemPriced6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced6.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced6.image_url = "";
                newSupplyCatalogItemPriced6.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced6.catalog_item_export_name = "";
                newSupplyCatalogItemPriced6.description = "";
                newSupplyCatalogItemPriced6.deleted = false;
                newSupplyCatalogItemPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced7 = new catalog_item();
                // newSupplyCatalogItemPriced7.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced7.product_id = newSupplyProduct7.product_id;
                newSupplyCatalogItemPriced7.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced7.catalog_item_code = "CD2106";
                newSupplyCatalogItemPriced7.catalog_item_name = "Medium Collection Envelope";
                newSupplyCatalogItemPriced7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced7.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced7.image_url = "";
                newSupplyCatalogItemPriced7.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced7.catalog_item_export_name = "";
                newSupplyCatalogItemPriced7.description = "";
                newSupplyCatalogItemPriced7.deleted = false;
                newSupplyCatalogItemPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced1);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced2);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced3);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced4);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced5);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced6);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced7);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region Priced catalog_item_detail

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced1 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced1.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced1.catalog_item_id = newSupplyCatalogItemPriced1.catalog_item_id;
                newSupplyCatalogItemDetailPriced1.catalog_item_detail_code = "CD2410";
                newSupplyCatalogItemDetailPriced1.catalog_item_detail_name = "S11 Snack Happy A PFS Brochure Priced";
                newSupplyCatalogItemDetailPriced1.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced1.nb_units = 1;
                newSupplyCatalogItemDetailPriced1.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced1.term = 1;
                newSupplyCatalogItemDetailPriced1.description = "";
                newSupplyCatalogItemDetailPriced1.is_default = false;
                newSupplyCatalogItemDetailPriced1.deleted = false;
                newSupplyCatalogItemDetailPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced2 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced2.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced2.catalog_item_id = newSupplyCatalogItemPriced2.catalog_item_id;
                newSupplyCatalogItemDetailPriced2.catalog_item_detail_code = "CD2412";
                newSupplyCatalogItemDetailPriced2.catalog_item_detail_name = "S11 Snack Happy A 3-Part NCR Forms";
                newSupplyCatalogItemDetailPriced2.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced2.nb_units = 1;
                newSupplyCatalogItemDetailPriced2.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced2.term = 1;
                newSupplyCatalogItemDetailPriced2.description = "";
                newSupplyCatalogItemDetailPriced2.is_default = false;
                newSupplyCatalogItemDetailPriced2.deleted = false;
                newSupplyCatalogItemDetailPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced3 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced3.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced3.catalog_item_id = newSupplyCatalogItemPriced3.catalog_item_id;
                newSupplyCatalogItemDetailPriced3.catalog_item_detail_code = "CD2223";
                newSupplyCatalogItemDetailPriced3.catalog_item_detail_name = "Snack Happy Promotion Poster (generic)";
                newSupplyCatalogItemDetailPriced3.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced3.nb_units = 1;
                newSupplyCatalogItemDetailPriced3.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced3.term = 1;
                newSupplyCatalogItemDetailPriced3.description = "";
                newSupplyCatalogItemDetailPriced3.is_default = false;
                newSupplyCatalogItemDetailPriced3.deleted = false;
                newSupplyCatalogItemDetailPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced4 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced4.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced4.catalog_item_id = newSupplyCatalogItemPriced4.catalog_item_id;
                newSupplyCatalogItemDetailPriced4.catalog_item_detail_code = "CD2023";
                newSupplyCatalogItemDetailPriced4.catalog_item_detail_name = "PFS Homeroom Envelope (1) per class";
                newSupplyCatalogItemDetailPriced4.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced4.nb_units = 1;
                newSupplyCatalogItemDetailPriced4.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced4.term = 1;
                newSupplyCatalogItemDetailPriced4.description = "";
                newSupplyCatalogItemDetailPriced4.is_default = false;
                newSupplyCatalogItemDetailPriced4.deleted = false;
                newSupplyCatalogItemDetailPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced5 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced5.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced5.catalog_item_id = newSupplyCatalogItemPriced5.catalog_item_id;
                newSupplyCatalogItemDetailPriced5.catalog_item_detail_code = "CD2093";
                newSupplyCatalogItemDetailPriced5.catalog_item_detail_name = "QSP Plastic Food Bag";
                newSupplyCatalogItemDetailPriced5.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced5.nb_units = 1;
                newSupplyCatalogItemDetailPriced5.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced5.term = 1;
                newSupplyCatalogItemDetailPriced5.description = "";
                newSupplyCatalogItemDetailPriced5.is_default = false;
                newSupplyCatalogItemDetailPriced5.deleted = false;
                newSupplyCatalogItemDetailPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced6 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced6.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced6.catalog_item_id = newSupplyCatalogItemPriced6.catalog_item_id;
                newSupplyCatalogItemDetailPriced6.catalog_item_detail_code = "CD2098";
                newSupplyCatalogItemDetailPriced6.catalog_item_detail_name = "Large Outer Envelope";
                newSupplyCatalogItemDetailPriced6.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced6.nb_units = 1;
                newSupplyCatalogItemDetailPriced6.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced6.term = 1;
                newSupplyCatalogItemDetailPriced6.description = "";
                newSupplyCatalogItemDetailPriced6.is_default = false;
                newSupplyCatalogItemDetailPriced6.deleted = false;
                newSupplyCatalogItemDetailPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced7 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced7.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced7.catalog_item_id = newSupplyCatalogItemPriced7.catalog_item_id;
                newSupplyCatalogItemDetailPriced7.catalog_item_detail_code = "CD2106";
                newSupplyCatalogItemDetailPriced7.catalog_item_detail_name = "Medium Collection Envelope";
                newSupplyCatalogItemDetailPriced7.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced7.nb_units = 1;
                newSupplyCatalogItemDetailPriced7.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced7.term = 1;
                newSupplyCatalogItemDetailPriced7.description = "";
                newSupplyCatalogItemDetailPriced7.is_default = false;
                newSupplyCatalogItemDetailPriced7.deleted = false;
                newSupplyCatalogItemDetailPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced1);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced2);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced3);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced4);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced5);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced6);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced7);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region Priced catalog_item_category_catalog_item

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced1 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_id = newSupplyCatalogItemPriced1.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced1.display_order = 1;
                newSupplyCatalogItemCategoryCatalogItemPriced1.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced2 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced2.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced2.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced2.catalog_item_id = newSupplyCatalogItemPriced2.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced2.display_order = 2;
                newSupplyCatalogItemCategoryCatalogItemPriced2.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced3 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_id = newSupplyCatalogItemPriced3.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced3.display_order = 3;
                newSupplyCatalogItemCategoryCatalogItemPriced3.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced4 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced4.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced4.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced4.catalog_item_id = newSupplyCatalogItemPriced4.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced4.display_order = 4;
                newSupplyCatalogItemCategoryCatalogItemPriced4.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced5 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_id = newSupplyCatalogItemPriced5.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced5.display_order = 5;
                newSupplyCatalogItemCategoryCatalogItemPriced5.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced6 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_id = newSupplyCatalogItemPriced6.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced6.display_order = 6;
                newSupplyCatalogItemCategoryCatalogItemPriced6.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced7 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_id = newSupplyCatalogItemPriced7.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced7.display_order = 6;
                newSupplyCatalogItemCategoryCatalogItemPriced7.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced1);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced2);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced3);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced4);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced5);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced6);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced7);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #endregion

                #endregion

                sb.AppendLine("Success!");
            }
            catch (Exception ex)
            {
                sb.AppendLine("transaction rollback due to error");
                sb.AppendLine("Message: " + ex.Message);
                sb.AppendLine("Source: " + ex.Source);
                sb.AppendLine("");
                sb.AppendLine(ex.StackTrace);
            }
            finally
            {
                if (context.Connection != null)
                {
                    context.Connection.Close();
                }
            }

            sb.AppendLine("end");

            return sb.ToString();
        }
        public string Spring2011_OtisBulk14()
        {
            #region Notes

            // To enforce the PA before an order can be made, we need to add code to:
            // QSPForm.WebApp.OrderStep_Selection.BindForm which is located in:
            // OrderExpress/UserControls/OrderStep_Selection.ascx

            // Supply catalogs in PA need one catalog for priced and one catalog for 
            // unpriced items

            #endregion

            #region Start

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("start");

            // Open db context
            QSPFulfillmentDataContext context = new QSPFulfillmentDataContext(CONNECTION_STRING);
            sb.AppendLine("db context open");

            // Open the connection
            context.Connection.Open();

            #endregion

            try
            {
                #region Order form

                #region form_group

                // Create new form group object
                form_group newFormGroup = new form_group();

                // Fill in new form group data
                // newFormGroup.form_group_id;              // Autonumber generated on insert
                newFormGroup.form_group_name = "Otis Spring 2011 Bulk 14 Item group";
                newFormGroup.deleted = false;
                newFormGroup.create_date = DateTime.Now;
                newFormGroup.create_user_id = CREATE_USER_ID;
                newFormGroup.update_date = DateTime.Now;
                newFormGroup.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.form_groups.InsertOnSubmit(newFormGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form group id = " + newFormGroup.form_group_id.ToString());
                // MessageBox.Show("New form group id = " + newFormGroup.form_group_id.ToString());

                #endregion

                #region form

                // Create new form object
                form newForm = new form();

                // Fill in new form data
                // newForm.form_id;                             // Autonumber generated on insert
                newForm.form_group_id = newFormGroup.form_group_id;
                newForm.entity_type_id = 4;                     // Type 4 = order
                newForm.form_code = "MC24";
                newForm.form_name = "Otis Spring 2011 Bulk 14 Item order form";
                newForm.description = "";
                newForm.order_terms_text = "";
                newForm.start_date = new DateTime(2010, 12, 8);
                newForm.end_date = new DateTime(2011, 6, 30);
                newForm.closing_time = new DateTime(2011, 6, 30);
                newForm.image_url = "images/CatalogItem/chocochunk.gif";
                newForm.is_base_form = false;
                newForm.parent_form_id = 8;                     // Base form for orders in prod and dev
                newForm.is_product_price_updatable = false;
                newForm.is_quantity_adjustment_allowed = true;
                newForm.tax_postal_address_type_id = 2;
                newForm.enabled = true;
                newForm.deleted = false;
                newForm.version = 1;
                newForm.program_id = 3;                         // Otis = 3, PV = 2
                newForm.program_type_id = 7;
                newForm.program_basics_text = "";
                newForm.create_date = DateTime.Now;
                newForm.create_user_id = CREATE_USER_ID;
                newForm.update_date = DateTime.Now;
                newForm.update_user_id = CREATE_USER_ID;
                //newForm.warehouse_type_id;
                newForm.is_bulk = true;
                newForm.report_name = "OrderForm";
                newForm.is_warehouse_selectable = false;
                newForm.default_warehouse_id = 27;
                newForm.season_id = 8;

                // Add the new form to the db context
                context.forms.InsertOnSubmit(newForm);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form id = " + newForm.form_id.ToString());
                // MessageBox.Show("New form id = " + newForm.form_id.ToString());

                #endregion

                #region form_permission

                form_permission newFormPermission1 = new form_permission();
                //newFormPermission1.form_permission_id;            // Autonumber generated on insert
                newFormPermission1.form_id = newForm.form_id;
                newFormPermission1.role_id = 0;                     // User
                newFormPermission1.allow_read = false;
                newFormPermission1.allow_write = false;
                newFormPermission1.create_date = DateTime.Now;
                newFormPermission1.create_user_id = CREATE_USER_ID;
                newFormPermission1.update_date = DateTime.Now;
                newFormPermission1.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission2 = new form_permission();
                //newFormPermission2.form_permission_id;            // Autonumber generated on insert
                newFormPermission2.form_id = newForm.form_id;
                newFormPermission2.role_id = 1;                     // Field Sale Manager (FM)
                newFormPermission2.allow_read = true;
                newFormPermission2.allow_write = true;
                newFormPermission2.create_date = DateTime.Now;
                newFormPermission2.create_user_id = CREATE_USER_ID;
                newFormPermission2.update_date = DateTime.Now;
                newFormPermission2.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission3 = new form_permission();
                //newFormPermission3.form_permission_id;            // Autonumber generated on insert
                newFormPermission3.form_id = newForm.form_id;
                newFormPermission3.role_id = 2;                     // Field Support
                newFormPermission3.allow_read = true;
                newFormPermission3.allow_write = true;
                newFormPermission3.create_date = DateTime.Now;
                newFormPermission3.create_user_id = CREATE_USER_ID;
                newFormPermission3.update_date = DateTime.Now;
                newFormPermission3.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission4 = new form_permission();
                //newFormPermission4.form_permission_id;            // Autonumber generated on insert
                newFormPermission4.form_id = newForm.form_id;
                newFormPermission4.role_id = 3;                     // Accounting Manager
                newFormPermission4.allow_read = true;
                newFormPermission4.allow_write = true;
                newFormPermission4.create_date = DateTime.Now;
                newFormPermission4.create_user_id = CREATE_USER_ID;
                newFormPermission4.update_date = DateTime.Now;
                newFormPermission4.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission5 = new form_permission();
                //newFormPermission5.form_permission_id;            // Autonumber generated on insert
                newFormPermission5.form_id = newForm.form_id;
                newFormPermission5.role_id = 4;                     // Admin
                newFormPermission5.allow_read = true;
                newFormPermission5.allow_write = true;
                newFormPermission5.create_date = DateTime.Now;
                newFormPermission5.create_user_id = CREATE_USER_ID;
                newFormPermission5.update_date = DateTime.Now;
                newFormPermission5.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission6 = new form_permission();
                //newFormPermission6.form_permission_id;            // Autonumber generated on insert
                newFormPermission6.form_id = newForm.form_id;
                newFormPermission6.role_id = 5;                     // Super User
                newFormPermission6.allow_read = true;
                newFormPermission6.allow_write = true;
                newFormPermission6.create_date = DateTime.Now;
                newFormPermission6.create_user_id = CREATE_USER_ID;
                newFormPermission6.update_date = DateTime.Now;
                newFormPermission6.update_user_id = CREATE_USER_ID;

                // Add the new form permissions to the db context
                context.form_permissions.InsertOnSubmit(newFormPermission1);
                context.form_permissions.InsertOnSubmit(newFormPermission2);
                context.form_permissions.InsertOnSubmit(newFormPermission3);
                context.form_permissions.InsertOnSubmit(newFormPermission4);
                context.form_permissions.InsertOnSubmit(newFormPermission5);
                context.form_permissions.InsertOnSubmit(newFormPermission6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form permissions ok");

                #endregion

                #region form_delivery_date_type

                form_delivery_date_type newFormDeliveryDateType1 = new form_delivery_date_type();
                //newFormDeliveryDateType1.form_delivery_date_type_id;  // Autonumber generated on insert
                newFormDeliveryDateType1.form_id = newForm.form_id;
                newFormDeliveryDateType1.delivery_date_type_id = 5;     // Otis Spring 2011, week starting in Sunday, 3 to 4 weeks cutoff

                context.form_delivery_date_types.InsertOnSubmit(newFormDeliveryDateType1);
                context.SubmitChanges();

                sb.AppendLine("form delivery date type ok");

                #endregion

                #region form_profit_rate

                form_profit_rate newFormProfitRate1 = new form_profit_rate();
                //newFormProfitRate1.form_profit_rate_id;                // Autonumber generated on insert
                newFormProfitRate1.form_id = newForm.form_id;
                newFormProfitRate1.profit_rate_id = 1;         // 1 = 40%, 2 = 45%, 3 = 50%
                newFormProfitRate1.deleted = false;
                newFormProfitRate1.create_date = DateTime.Now;
                newFormProfitRate1.create_user_id = CREATE_USER_ID;
                newFormProfitRate1.update_date = DateTime.Now;
                newFormProfitRate1.update_user_id = CREATE_USER_ID;

                //form_profit_rate newFormProfitRate2 = new form_profit_rate();
                ////newFormProfitRate2.form_profit_rate_id;                // Autonumber generated on insert
                //newFormProfitRate2.form_id = newForm.form_id;
                //newFormProfitRate2.profit_rate_id = 2;         // 1 = 40%, 2 = 45%, 3 = 50%
                //newFormProfitRate2.deleted = false;
                //newFormProfitRate2.create_date = DateTime.Now;
                //newFormProfitRate2.create_user_id = CREATE_USER_ID;
                //newFormProfitRate2.update_date = DateTime.Now;
                //newFormProfitRate2.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_profit_rates.InsertOnSubmit(newFormProfitRate1);
                //context.form_profit_rates.InsertOnSubmit(newFormProfitRate2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form profit rate ok");

                #endregion

                #region form_order_type

                // Create new form order type object
                form_order_type newFormOrderType1 = new form_order_type();
                // newFormOrderType1.form_order_type_id;            // Autonumber generated on insert
                newFormOrderType1.form_id = newForm.form_id;
                newFormOrderType1.order_type_id = 1;                // 1 = standard order
                newFormOrderType1.deleted = false;
                newFormOrderType1.create_date = DateTime.Now;
                newFormOrderType1.create_user_id = CREATE_USER_ID;
                newFormOrderType1.update_date = DateTime.Now;
                newFormOrderType1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_order_types.InsertOnSubmit(newFormOrderType1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form order type ok");

                #endregion

                #region form_delivery_method

                // Create new record
                form_delivery_method newFormDeliveryMethod1 = new form_delivery_method();
                // newFormDeliveryMethod1.form_delivery_method_id;          // Autonumber generated on insert
                newFormDeliveryMethod1.delivery_method_id = 1;              // Common carrier
                newFormDeliveryMethod1.form_id = newForm.form_id;
                newFormDeliveryMethod1.deleted = false;
                newFormDeliveryMethod1.create_date = DateTime.Now;
                newFormDeliveryMethod1.create_user_id = CREATE_USER_ID;
                newFormDeliveryMethod1.update_date = DateTime.Now;
                newFormDeliveryMethod1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_delivery_methods.InsertOnSubmit(newFormDeliveryMethod1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form delivery methods ok");

                #endregion

                #region catalog_group

                // Create new record
                catalog_group newCatalogGroup = new catalog_group();
                // newCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newCatalogGroup.catalog_group_code = "MC24";
                newCatalogGroup.catalog_group_name = "Otis Spring 2011 Bulk 14 Item order";
                newCatalogGroup.description = "";
                newCatalogGroup.deleted = false;
                newCatalogGroup.create_date = DateTime.Now;
                newCatalogGroup.create_user_id = CREATE_USER_ID;
                newCatalogGroup.update_date = DateTime.Now;
                newCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region catalog

                // Create new record
                catalog newCatalog = new catalog();
                // newCatalog.newCatalog_id;          // Autonumber generated on insert
                newCatalog.catalog_group_id = newCatalogGroup.catalog_group_id;
                newCatalog.catalog_code = "MC24";
                newCatalog.catalog_name = "Otis Spring 2011 Bulk 14 Item order";
                newCatalog.culture = "en-US";
                newCatalog.description = "";
                newCatalog.start_date = new DateTime(2010, 12, 8);
                newCatalog.end_date = new DateTime(2011, 6, 30);
                newCatalog.deleted = false;
                newCatalog.create_date = DateTime.Now;
                newCatalog.create_user_id = CREATE_USER_ID;
                newCatalog.update_date = DateTime.Now;
                newCatalog.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newCatalog);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newCatalog.catalog_id.ToString());

                #endregion

                #region catalog_item_category

                // Create new record
                catalog_item_category newCatalogItemCategory1 = new catalog_item_category();
                // newCatalogItemCategory1.catalog_item_category_id;         // Autonumber generated on insert
                newCatalogItemCategory1.catalog_id = newCatalog.catalog_id;
                newCatalogItemCategory1.catalog_item_category_name = "Otis Spring 2011 14 Bulk Item";
                newCatalogItemCategory1.parent_catalog_item_category_id = 0;
                newCatalogItemCategory1.deleted = false;
                newCatalogItemCategory1.create_date = DateTime.Now;
                newCatalogItemCategory1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategory1.update_date = DateTime.Now;
                newCatalogItemCategory1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newCatalogItemCategory1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newCatalogItemCategory1.catalog_item_category_id.ToString());

                #endregion

                #region form_section

                // Create new record
                form_section newFormSection1 = new form_section();
                // newFormSection1.form_section_id;              // Autonumber generated on insert
                newFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newFormSection1.form_id = newForm.form_id;
                newFormSection1.form_section_type_id = 1;        // 1 = standard product, 2 = supply product, 3 = optional product
                newFormSection1.form_section_number = 1;
                newFormSection1.form_section_title = "Otis Spring 2011 14 Bulk";
                newFormSection1.description = "";
                newFormSection1.deleted = false;
                newFormSection1.create_date = DateTime.Now;
                newFormSection1.create_user_id = CREATE_USER_ID;
                newFormSection1.update_date = DateTime.Now;
                newFormSection1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newFormSection1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newFormSection1.form_section_id.ToString());

                #endregion

                #region product

                #region Create new record

                product newProduct1 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct1.product_code = "74300";
                newProduct1.product_name = "44300 CHOCOLATE CHIP";
                newProduct1.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct1.nb_units = 1;                       // boxes per case
                newProduct1.nb_day_lead_time = 0;
                newProduct1.product_status_id = 101;            // 101 = Active
                newProduct1.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct1.business_division_id = 1;           // 1 = US
                newProduct1.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct1.coupon_id = null;
                newProduct1.description = "";
                newProduct1.image_url = "";
                newProduct1.is_free_sample = false;
                newProduct1.oracle_code = "";
                newProduct1.IVCOUP = "";
                newProduct1.IVITEM = "74300";
                newProduct1.unit_cost = null;
                newProduct1.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct1.vendor_item_code = "74300";
                newProduct1.deleted = false;
                newProduct1.create_date = DateTime.Now;
                newProduct1.create_user_id = CREATE_USER_ID;
                newProduct1.update_date = DateTime.Now;
                newProduct1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct2 = new product();
                // newProduct2.product_id;                      // Autonumber generated on insert
                newProduct2.product_code = "74304";
                newProduct2.product_name = "44304 BUTTER SUGAR";
                newProduct2.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct2.nb_units = 1;                       // boxes per case
                newProduct2.nb_day_lead_time = 0;
                newProduct2.product_status_id = 101;            // 101 = Active
                newProduct2.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct2.business_division_id = 1;           // 1 = US
                newProduct2.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct2.coupon_id = null;
                newProduct2.description = "";
                newProduct2.image_url = "";
                newProduct2.is_free_sample = false;
                newProduct2.oracle_code = "";
                newProduct2.IVCOUP = "";
                newProduct2.IVITEM = "74304";
                newProduct2.unit_cost = null;
                newProduct2.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct2.vendor_item_code = "74304";
                newProduct2.deleted = false;
                newProduct2.create_date = DateTime.Now;
                newProduct2.create_user_id = CREATE_USER_ID;
                newProduct2.update_date = DateTime.Now;
                newProduct2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct3 = new product();
                // newProduct3.product_id;                      // Autonumber generated on insert
                newProduct3.product_code = "74323";
                newProduct3.product_name = "44323 STRAWBERRY SHORTCAKE";
                newProduct3.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct3.nb_units = 1;                       // boxes per case
                newProduct3.nb_day_lead_time = 0;
                newProduct3.product_status_id = 101;            // 101 = Active
                newProduct3.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct3.business_division_id = 1;           // 1 = US
                newProduct3.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct3.coupon_id = null;
                newProduct3.description = "";
                newProduct3.image_url = "";
                newProduct3.is_free_sample = false;
                newProduct3.oracle_code = "";
                newProduct3.IVCOUP = "";
                newProduct3.IVITEM = "74323";
                newProduct3.unit_cost = null;
                newProduct3.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct3.vendor_item_code = "74323";
                newProduct3.deleted = false;
                newProduct3.create_date = DateTime.Now;
                newProduct3.create_user_id = CREATE_USER_ID;
                newProduct3.update_date = DateTime.Now;
                newProduct3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct4 = new product();
                // newProduct4.product_id;                      // Autonumber generated on insert
                newProduct4.product_code = "74308";
                newProduct4.product_name = "44308 CARNIVAL";
                newProduct4.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct4.nb_units = 1;                       // boxes per case
                newProduct4.nb_day_lead_time = 0;
                newProduct4.product_status_id = 101;            // 101 = Active
                newProduct4.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct4.business_division_id = 1;           // 1 = US
                newProduct4.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct4.coupon_id = null;
                newProduct4.description = "";
                newProduct4.image_url = "";
                newProduct4.is_free_sample = false;
                newProduct4.oracle_code = "";
                newProduct4.IVCOUP = "";
                newProduct4.IVITEM = "74308";
                newProduct4.unit_cost = null;
                newProduct4.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct4.vendor_item_code = "74308";
                newProduct4.deleted = false;
                newProduct4.create_date = DateTime.Now;
                newProduct4.create_user_id = CREATE_USER_ID;
                newProduct4.update_date = DateTime.Now;
                newProduct4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct5 = new product();
                // newProduct5.product_id;                      // Autonumber generated on insert
                newProduct5.product_code = "74319";
                newProduct5.product_name = "44319 CRANBERRY OATMEAL";
                newProduct5.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct5.nb_units = 1;                       // boxes per case
                newProduct5.nb_day_lead_time = 0;
                newProduct5.product_status_id = 101;            // 101 = Active
                newProduct5.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct5.business_division_id = 1;           // 1 = US
                newProduct5.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct5.coupon_id = null;
                newProduct5.description = "";
                newProduct5.image_url = "";
                newProduct5.is_free_sample = false;
                newProduct5.oracle_code = "";
                newProduct5.IVCOUP = "";
                newProduct5.IVITEM = "74319";
                newProduct5.unit_cost = null;
                newProduct5.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct5.vendor_item_code = "74319";
                newProduct5.deleted = false;
                newProduct5.create_date = DateTime.Now;
                newProduct5.create_user_id = CREATE_USER_ID;
                newProduct5.update_date = DateTime.Now;
                newProduct5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct6 = new product();
                // newProduct6.product_id;                      // Autonumber generated on insert
                newProduct6.product_code = "74314";
                newProduct6.product_name = "44314 TRIPLE CHOCOLATE CHUNK";
                newProduct6.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct6.nb_units = 1;                       // boxes per case
                newProduct6.nb_day_lead_time = 0;
                newProduct6.product_status_id = 101;            // 101 = Active
                newProduct6.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct6.business_division_id = 1;           // 1 = US
                newProduct6.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct6.coupon_id = null;
                newProduct6.description = "";
                newProduct6.image_url = "";
                newProduct6.is_free_sample = false;
                newProduct6.oracle_code = "";
                newProduct6.IVCOUP = "";
                newProduct6.IVITEM = "74314";
                newProduct6.unit_cost = null;
                newProduct6.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct6.vendor_item_code = "74314";
                newProduct6.deleted = false;
                newProduct6.create_date = DateTime.Now;
                newProduct6.create_user_id = CREATE_USER_ID;
                newProduct6.update_date = DateTime.Now;
                newProduct6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct7 = new product();
                // newProduct7.product_id;                      // Autonumber generated on insert
                newProduct7.product_code = "74307";
                newProduct7.product_name = "44307 WHITE CHOCOLATE MACADEMIA";
                newProduct7.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct7.nb_units = 1;                       // boxes per case
                newProduct7.nb_day_lead_time = 0;
                newProduct7.product_status_id = 101;            // 101 = Active
                newProduct7.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct7.business_division_id = 1;           // 1 = US
                newProduct7.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct7.coupon_id = null;
                newProduct7.description = "";
                newProduct7.image_url = "";
                newProduct7.is_free_sample = false;
                newProduct7.oracle_code = "";
                newProduct7.IVCOUP = "";
                newProduct7.IVITEM = "74307";
                newProduct7.unit_cost = null;
                newProduct7.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct7.vendor_item_code = "74307";
                newProduct7.deleted = false;
                newProduct7.create_date = DateTime.Now;
                newProduct7.create_user_id = CREATE_USER_ID;
                newProduct7.update_date = DateTime.Now;
                newProduct7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct8 = new product();
                // newProduct8.product_id;                      // Autonumber generated on insert
                newProduct8.product_code = "74305";
                newProduct8.product_name = "44305 PEANUT BUTTER";
                newProduct8.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct8.nb_units = 1;                       // boxes per case
                newProduct8.nb_day_lead_time = 0;
                newProduct8.product_status_id = 101;            // 101 = Active
                newProduct8.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct8.business_division_id = 1;           // 1 = US
                newProduct8.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct8.coupon_id = null;
                newProduct8.description = "";
                newProduct8.image_url = "";
                newProduct8.is_free_sample = false;
                newProduct8.oracle_code = "";
                newProduct8.IVCOUP = "";
                newProduct8.IVITEM = "74305";
                newProduct8.unit_cost = null;
                newProduct8.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct8.vendor_item_code = "74305";
                newProduct8.deleted = false;
                newProduct8.create_date = DateTime.Now;
                newProduct8.create_user_id = CREATE_USER_ID;
                newProduct8.update_date = DateTime.Now;
                newProduct8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct9 = new product();
                // newProduct9.product_id;                      // Autonumber generated on insert
                newProduct9.product_code = "74389";
                newProduct9.product_name = "44389 REDUCED FAT CHOCOLATE CHIP";
                newProduct9.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct9.nb_units = 1;                       // boxes per case
                newProduct9.nb_day_lead_time = 0;
                newProduct9.product_status_id = 101;            // 101 = Active
                newProduct9.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct9.business_division_id = 1;           // 1 = US
                newProduct9.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct9.coupon_id = null;
                newProduct9.description = "";
                newProduct9.image_url = "";
                newProduct9.is_free_sample = false;
                newProduct9.oracle_code = "";
                newProduct9.IVCOUP = "";
                newProduct9.IVITEM = "74389";
                newProduct9.unit_cost = null;
                newProduct9.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct9.vendor_item_code = "74389";
                newProduct9.deleted = false;
                newProduct9.create_date = DateTime.Now;
                newProduct9.create_user_id = CREATE_USER_ID;
                newProduct9.update_date = DateTime.Now;
                newProduct9.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct10 = new product();
                // newProduct10.product_id;                      // Autonumber generated on insert
                newProduct10.product_code = "74303";
                newProduct10.product_name = "44303 OATMEAL RAISIN";
                newProduct10.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct10.nb_units = 1;                       // boxes per case
                newProduct10.nb_day_lead_time = 0;
                newProduct10.product_status_id = 101;            // 101 = Active
                newProduct10.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct10.business_division_id = 1;           // 1 = US
                newProduct10.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct10.coupon_id = null;
                newProduct10.description = "";
                newProduct10.image_url = "";
                newProduct10.is_free_sample = false;
                newProduct10.oracle_code = "";
                newProduct10.IVCOUP = "";
                newProduct10.IVITEM = "74303";
                newProduct10.unit_cost = null;
                newProduct10.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct10.vendor_item_code = "74303";
                newProduct10.deleted = false;
                newProduct10.create_date = DateTime.Now;
                newProduct10.create_user_id = CREATE_USER_ID;
                newProduct10.update_date = DateTime.Now;
                newProduct10.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct11 = new product();
                // newProduct11.product_id;                      // Autonumber generated on insert
                newProduct11.product_code = "74351";
                newProduct11.product_name = "44351 HOLIDAY JOY COOKIE";
                newProduct11.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct11.nb_units = 1;                       // boxes per case
                newProduct11.nb_day_lead_time = 0;
                newProduct11.product_status_id = 101;            // 101 = Active
                newProduct11.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct11.business_division_id = 1;           // 1 = US
                newProduct11.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct11.coupon_id = null;
                newProduct11.description = "";
                newProduct11.image_url = "";
                newProduct11.is_free_sample = false;
                newProduct11.oracle_code = "";
                newProduct11.IVCOUP = "";
                newProduct11.IVITEM = "74351";
                newProduct11.unit_cost = null;
                newProduct11.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct11.vendor_item_code = "74351";
                newProduct11.deleted = false;
                newProduct11.create_date = DateTime.Now;
                newProduct11.create_user_id = CREATE_USER_ID;
                newProduct11.update_date = DateTime.Now;
                newProduct11.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct12 = new product();
                // newProduct12.product_id;                      // Autonumber generated on insert
                newProduct12.product_code = "74315";
                newProduct12.product_name = "44315 PINK COOKIE";
                newProduct12.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct12.nb_units = 1;                       // boxes per case
                newProduct12.nb_day_lead_time = 0;
                newProduct12.product_status_id = 101;            // 101 = Active
                newProduct12.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct12.business_division_id = 1;           // 1 = US
                newProduct12.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct12.coupon_id = null;
                newProduct12.description = "";
                newProduct12.image_url = "";
                newProduct12.is_free_sample = false;
                newProduct12.oracle_code = "";
                newProduct12.IVCOUP = "";
                newProduct12.IVITEM = "74315";
                newProduct12.unit_cost = null;
                newProduct12.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct12.vendor_item_code = "74315";
                newProduct12.deleted = false;
                newProduct12.create_date = DateTime.Now;
                newProduct12.create_user_id = CREATE_USER_ID;
                newProduct12.update_date = DateTime.Now;
                newProduct12.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct13 = new product();
                // newProduct13.product_id;                      // Autonumber generated on insert
                newProduct13.product_code = "70300";
                newProduct13.product_name = "40300 PRETZEL KIT";
                newProduct13.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct13.nb_units = 1;                       // boxes per case
                newProduct13.nb_day_lead_time = 0;
                newProduct13.product_status_id = 101;            // 101 = Active
                newProduct13.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct13.business_division_id = 1;           // 1 = US
                newProduct13.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct13.coupon_id = null;
                newProduct13.description = "";
                newProduct13.image_url = "";
                newProduct13.is_free_sample = false;
                newProduct13.oracle_code = "";
                newProduct13.IVCOUP = "";
                newProduct13.IVITEM = "70300";
                newProduct13.unit_cost = null;
                newProduct13.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct13.vendor_item_code = "70300";
                newProduct13.deleted = false;
                newProduct13.create_date = DateTime.Now;
                newProduct13.create_user_id = CREATE_USER_ID;
                newProduct13.update_date = DateTime.Now;
                newProduct13.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct14 = new product();
                // newProduct14.product_id;                      // Autonumber generated on insert
                newProduct14.product_code = "77145";
                newProduct14.product_name = "87145 APPLE CINNNAMON COFFEE CAKE";
                newProduct14.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct14.nb_units = 1;                       // boxes per case
                newProduct14.nb_day_lead_time = 0;
                newProduct14.product_status_id = 101;            // 101 = Active
                newProduct14.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct14.business_division_id = 1;           // 1 = US
                newProduct14.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct14.coupon_id = null;
                newProduct14.description = "";
                newProduct14.image_url = "";
                newProduct14.is_free_sample = false;
                newProduct14.oracle_code = "";
                newProduct14.IVCOUP = "";
                newProduct14.IVITEM = "77145";
                newProduct14.unit_cost = null;
                newProduct14.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct14.vendor_item_code = "77145";
                newProduct14.deleted = false;
                newProduct14.create_date = DateTime.Now;
                newProduct14.create_user_id = CREATE_USER_ID;
                newProduct14.update_date = DateTime.Now;
                newProduct14.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct15 = new product();
                // newProduct15.product_id;                      // Autonumber generated on insert
                newProduct15.product_code = "70370";
                newProduct15.product_name = "30370 DOUBLE CHOCOLATE CHIP BROWNIES";
                newProduct15.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct15.nb_units = 1;                       // boxes per case
                newProduct15.nb_day_lead_time = 0;
                newProduct15.product_status_id = 101;            // 101 = Active
                newProduct15.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct15.business_division_id = 1;           // 1 = US
                newProduct15.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct15.coupon_id = null;
                newProduct15.description = "";
                newProduct15.image_url = "";
                newProduct15.is_free_sample = false;
                newProduct15.oracle_code = "";
                newProduct15.IVCOUP = "";
                newProduct15.IVITEM = "70370";
                newProduct15.unit_cost = null;
                newProduct15.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct15.vendor_item_code = "70370";
                newProduct15.deleted = false;
                newProduct15.create_date = DateTime.Now;
                newProduct15.create_user_id = CREATE_USER_ID;
                newProduct15.update_date = DateTime.Now;
                newProduct15.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.products.InsertOnSubmit(newProduct1);
                context.products.InsertOnSubmit(newProduct2);
                context.products.InsertOnSubmit(newProduct3);
                context.products.InsertOnSubmit(newProduct4);
                context.products.InsertOnSubmit(newProduct5);
                context.products.InsertOnSubmit(newProduct6);
                context.products.InsertOnSubmit(newProduct7);
                context.products.InsertOnSubmit(newProduct8);
                context.products.InsertOnSubmit(newProduct9);
                context.products.InsertOnSubmit(newProduct10);
                context.products.InsertOnSubmit(newProduct11);
                context.products.InsertOnSubmit(newProduct12);
                context.products.InsertOnSubmit(newProduct13);
                context.products.InsertOnSubmit(newProduct14);
                context.products.InsertOnSubmit(newProduct15);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("products ok");

                #endregion

                #region catalog_item

                #region Create new record

                catalog_item newCatalogItem1 = new catalog_item();
                // newCatalogItem1.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem1.product_id = newProduct1.product_id;
                newCatalogItem1.catalog_id = newCatalog.catalog_id;
                newCatalogItem1.catalog_item_code = "74300";
                newCatalogItem1.catalog_item_name = "CHOCOLATE CHIP";
                newCatalogItem1.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem1.nb_units = 1;                       // Boxes per case
                newCatalogItem1.image_url = "";
                newCatalogItem1.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem1.catalog_item_export_name = "";
                newCatalogItem1.description = "";
                newCatalogItem1.deleted = false;
                newCatalogItem1.create_date = DateTime.Now;
                newCatalogItem1.create_user_id = CREATE_USER_ID;
                newCatalogItem1.update_date = DateTime.Now;
                newCatalogItem1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem2 = new catalog_item();
                // newCatalogItem2.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem2.product_id = newProduct2.product_id;
                newCatalogItem2.catalog_id = newCatalog.catalog_id;
                newCatalogItem2.catalog_item_code = "74304";
                newCatalogItem2.catalog_item_name = "BUTTER SUGAR";
                newCatalogItem2.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem2.nb_units = 1;                       // Boxes per case
                newCatalogItem2.image_url = "";
                newCatalogItem2.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem2.catalog_item_export_name = "";
                newCatalogItem2.description = "";
                newCatalogItem2.deleted = false;
                newCatalogItem2.create_date = DateTime.Now;
                newCatalogItem2.create_user_id = CREATE_USER_ID;
                newCatalogItem2.update_date = DateTime.Now;
                newCatalogItem2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem3 = new catalog_item();
                // newCatalogItem3.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem3.product_id = newProduct3.product_id;
                newCatalogItem3.catalog_id = newCatalog.catalog_id;
                newCatalogItem3.catalog_item_code = "74323";
                newCatalogItem3.catalog_item_name = "STRAWBERRY SHORTCAKE";
                newCatalogItem3.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem3.nb_units = 1;                       // Boxes per case
                newCatalogItem3.image_url = "";
                newCatalogItem3.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem3.catalog_item_export_name = "";
                newCatalogItem3.description = "";
                newCatalogItem3.deleted = false;
                newCatalogItem3.create_date = DateTime.Now;
                newCatalogItem3.create_user_id = CREATE_USER_ID;
                newCatalogItem3.update_date = DateTime.Now;
                newCatalogItem3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem4 = new catalog_item();
                // newCatalogItem4.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem4.product_id = newProduct4.product_id;
                newCatalogItem4.catalog_id = newCatalog.catalog_id;
                newCatalogItem4.catalog_item_code = "74308";
                newCatalogItem4.catalog_item_name = "CARNIVAL";
                newCatalogItem4.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem4.nb_units = 1;                       // Boxes per case
                newCatalogItem4.image_url = "";
                newCatalogItem4.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem4.catalog_item_export_name = "";
                newCatalogItem4.description = "";
                newCatalogItem4.deleted = false;
                newCatalogItem4.create_date = DateTime.Now;
                newCatalogItem4.create_user_id = CREATE_USER_ID;
                newCatalogItem4.update_date = DateTime.Now;
                newCatalogItem4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem5 = new catalog_item();
                // newCatalogItem5.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem5.product_id = newProduct5.product_id;
                newCatalogItem5.catalog_id = newCatalog.catalog_id;
                newCatalogItem5.catalog_item_code = "74319";
                newCatalogItem5.catalog_item_name = "CRANBERRY OATMEAL";
                newCatalogItem5.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem5.nb_units = 1;                       // Boxes per case
                newCatalogItem5.image_url = "";
                newCatalogItem5.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem5.catalog_item_export_name = "";
                newCatalogItem5.description = "";
                newCatalogItem5.deleted = false;
                newCatalogItem5.create_date = DateTime.Now;
                newCatalogItem5.create_user_id = CREATE_USER_ID;
                newCatalogItem5.update_date = DateTime.Now;
                newCatalogItem5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem6 = new catalog_item();
                // newCatalogItem6.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem6.product_id = newProduct6.product_id;
                newCatalogItem6.catalog_id = newCatalog.catalog_id;
                newCatalogItem6.catalog_item_code = "74314";
                newCatalogItem6.catalog_item_name = "TRIPLE CHOCOLATE CHUNK";
                newCatalogItem6.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem6.nb_units = 1;                       // Boxes per case
                newCatalogItem6.image_url = "";
                newCatalogItem6.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem6.catalog_item_export_name = "";
                newCatalogItem6.description = "";
                newCatalogItem6.deleted = false;
                newCatalogItem6.create_date = DateTime.Now;
                newCatalogItem6.create_user_id = CREATE_USER_ID;
                newCatalogItem6.update_date = DateTime.Now;
                newCatalogItem6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem7 = new catalog_item();
                // newCatalogItem7.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem7.product_id = newProduct7.product_id;
                newCatalogItem7.catalog_id = newCatalog.catalog_id;
                newCatalogItem7.catalog_item_code = "74307";
                newCatalogItem7.catalog_item_name = "WHITE CHOCOLATE MACADEMIA ";
                newCatalogItem7.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem7.nb_units = 1;                       // Boxes per case
                newCatalogItem7.image_url = "";
                newCatalogItem7.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem7.catalog_item_export_name = "";
                newCatalogItem7.description = "";
                newCatalogItem7.deleted = false;
                newCatalogItem7.create_date = DateTime.Now;
                newCatalogItem7.create_user_id = CREATE_USER_ID;
                newCatalogItem7.update_date = DateTime.Now;
                newCatalogItem7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem8 = new catalog_item();
                // newCatalogItem8.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem8.product_id = newProduct8.product_id;
                newCatalogItem8.catalog_id = newCatalog.catalog_id;
                newCatalogItem8.catalog_item_code = "74305";
                newCatalogItem8.catalog_item_name = "PEANUT BUTTER";
                newCatalogItem8.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem8.nb_units = 1;                       // Boxes per case
                newCatalogItem8.image_url = "";
                newCatalogItem8.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem8.catalog_item_export_name = "";
                newCatalogItem8.description = "";
                newCatalogItem8.deleted = false;
                newCatalogItem8.create_date = DateTime.Now;
                newCatalogItem8.create_user_id = CREATE_USER_ID;
                newCatalogItem8.update_date = DateTime.Now;
                newCatalogItem8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem9 = new catalog_item();
                // newCatalogItem9.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem9.product_id = newProduct9.product_id;
                newCatalogItem9.catalog_id = newCatalog.catalog_id;
                newCatalogItem9.catalog_item_code = "74389";
                newCatalogItem9.catalog_item_name = "REDUCED FAT CHOCOLATE CHIP";
                newCatalogItem9.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem9.nb_units = 1;                       // Boxes per case
                newCatalogItem9.image_url = "";
                newCatalogItem9.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem9.catalog_item_export_name = "";
                newCatalogItem9.description = "";
                newCatalogItem9.deleted = false;
                newCatalogItem9.create_date = DateTime.Now;
                newCatalogItem9.create_user_id = CREATE_USER_ID;
                newCatalogItem9.update_date = DateTime.Now;
                newCatalogItem9.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem10 = new catalog_item();
                // newCatalogItem10.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem10.product_id = newProduct10.product_id;
                newCatalogItem10.catalog_id = newCatalog.catalog_id;
                newCatalogItem10.catalog_item_code = "74303";
                newCatalogItem10.catalog_item_name = "OATMEAL RAISIN";
                newCatalogItem10.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem10.nb_units = 1;                       // Boxes per case
                newCatalogItem10.image_url = "";
                newCatalogItem10.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem10.catalog_item_export_name = "";
                newCatalogItem10.description = "";
                newCatalogItem10.deleted = false;
                newCatalogItem10.create_date = DateTime.Now;
                newCatalogItem10.create_user_id = CREATE_USER_ID;
                newCatalogItem10.update_date = DateTime.Now;
                newCatalogItem10.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem12 = new catalog_item();
                // newCatalogItem12.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem12.product_id = newProduct12.product_id;
                newCatalogItem12.catalog_id = newCatalog.catalog_id;
                newCatalogItem12.catalog_item_code = "74315";
                newCatalogItem12.catalog_item_name = "PINK COOKIE";
                newCatalogItem12.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem12.nb_units = 1;                       // Boxes per case
                newCatalogItem12.image_url = "";
                newCatalogItem12.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem12.catalog_item_export_name = "";
                newCatalogItem12.description = "";
                newCatalogItem12.deleted = false;
                newCatalogItem12.create_date = DateTime.Now;
                newCatalogItem12.create_user_id = CREATE_USER_ID;
                newCatalogItem12.update_date = DateTime.Now;
                newCatalogItem12.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem13 = new catalog_item();
                // newCatalogItem13.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem13.product_id = newProduct13.product_id;
                newCatalogItem13.catalog_id = newCatalog.catalog_id;
                newCatalogItem13.catalog_item_code = "70300";
                newCatalogItem13.catalog_item_name = "PRETZEL KIT";
                newCatalogItem13.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem13.nb_units = 1;                       // Boxes per case
                newCatalogItem13.image_url = "";
                newCatalogItem13.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem13.catalog_item_export_name = "";
                newCatalogItem13.description = "";
                newCatalogItem13.deleted = false;
                newCatalogItem13.create_date = DateTime.Now;
                newCatalogItem13.create_user_id = CREATE_USER_ID;
                newCatalogItem13.update_date = DateTime.Now;
                newCatalogItem13.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem14 = new catalog_item();
                // newCatalogItem14.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem14.product_id = newProduct14.product_id;
                newCatalogItem14.catalog_id = newCatalog.catalog_id;
                newCatalogItem14.catalog_item_code = "77145";
                newCatalogItem14.catalog_item_name = "APPLE CINNNAMON COFFEE CAKE";
                newCatalogItem14.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem14.nb_units = 1;                       // Boxes per case
                newCatalogItem14.image_url = "";
                newCatalogItem14.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem14.catalog_item_export_name = "";
                newCatalogItem14.description = "";
                newCatalogItem14.deleted = false;
                newCatalogItem14.create_date = DateTime.Now;
                newCatalogItem14.create_user_id = CREATE_USER_ID;
                newCatalogItem14.update_date = DateTime.Now;
                newCatalogItem14.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem15 = new catalog_item();
                // newCatalogItem15.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem15.product_id = newProduct15.product_id;
                newCatalogItem15.catalog_id = newCatalog.catalog_id;
                newCatalogItem15.catalog_item_code = "70370";
                newCatalogItem15.catalog_item_name = "DOUBLE CHOCOLATE CHIP BROWNIES";
                newCatalogItem15.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem15.nb_units = 1;                       // Boxes per case
                newCatalogItem15.image_url = "";
                newCatalogItem15.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem15.catalog_item_export_name = "";
                newCatalogItem15.description = "";
                newCatalogItem15.deleted = false;
                newCatalogItem15.create_date = DateTime.Now;
                newCatalogItem15.create_user_id = CREATE_USER_ID;
                newCatalogItem15.update_date = DateTime.Now;
                newCatalogItem15.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newCatalogItem1);
                context.catalog_items.InsertOnSubmit(newCatalogItem2);
                context.catalog_items.InsertOnSubmit(newCatalogItem3);
                context.catalog_items.InsertOnSubmit(newCatalogItem4);
                context.catalog_items.InsertOnSubmit(newCatalogItem5);
                context.catalog_items.InsertOnSubmit(newCatalogItem6);
                context.catalog_items.InsertOnSubmit(newCatalogItem7);
                context.catalog_items.InsertOnSubmit(newCatalogItem8);
                context.catalog_items.InsertOnSubmit(newCatalogItem9);
                context.catalog_items.InsertOnSubmit(newCatalogItem10);
                context.catalog_items.InsertOnSubmit(newCatalogItem12);
                context.catalog_items.InsertOnSubmit(newCatalogItem13);
                context.catalog_items.InsertOnSubmit(newCatalogItem14);
                context.catalog_items.InsertOnSubmit(newCatalogItem15);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region catalog_item_detail

                #region 40% profit

                catalog_item_detail newCatalogItemDetail_40_1 = new catalog_item_detail();
                // newCatalogItemDetail_40_1.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_1.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemDetail_40_1.catalog_item_detail_code = "74300";
                newCatalogItemDetail_40_1.catalog_item_detail_name = "Chocolate Chip";
                newCatalogItemDetail_40_1.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_1.nb_units = 1;
                newCatalogItemDetail_40_1.profit_rate = 0.40;
                newCatalogItemDetail_40_1.term = 1;
                newCatalogItemDetail_40_1.description = "";
                newCatalogItemDetail_40_1.is_default = false;
                newCatalogItemDetail_40_1.deleted = false;
                newCatalogItemDetail_40_1.create_date = DateTime.Now;
                newCatalogItemDetail_40_1.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_1.update_date = DateTime.Now;
                newCatalogItemDetail_40_1.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_2 = new catalog_item_detail();
                // newCatalogItemDetail_40_2.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_2.catalog_item_id = newCatalogItem2.catalog_item_id;
                newCatalogItemDetail_40_2.catalog_item_detail_code = "74304";
                newCatalogItemDetail_40_2.catalog_item_detail_name = "Butter Sugar";
                newCatalogItemDetail_40_2.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_2.nb_units = 1;
                newCatalogItemDetail_40_2.profit_rate = 0.40;
                newCatalogItemDetail_40_2.term = 1;
                newCatalogItemDetail_40_2.description = "";
                newCatalogItemDetail_40_2.is_default = false;
                newCatalogItemDetail_40_2.deleted = false;
                newCatalogItemDetail_40_2.create_date = DateTime.Now;
                newCatalogItemDetail_40_2.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_2.update_date = DateTime.Now;
                newCatalogItemDetail_40_2.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_3 = new catalog_item_detail();
                // newCatalogItemDetail_40_3.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_3.catalog_item_id = newCatalogItem3.catalog_item_id;
                newCatalogItemDetail_40_3.catalog_item_detail_code = "74323";
                newCatalogItemDetail_40_3.catalog_item_detail_name = "Strawberry Shortcake";
                newCatalogItemDetail_40_3.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_3.nb_units = 1;
                newCatalogItemDetail_40_3.profit_rate = 0.40;
                newCatalogItemDetail_40_3.term = 1;
                newCatalogItemDetail_40_3.description = "";
                newCatalogItemDetail_40_3.is_default = false;
                newCatalogItemDetail_40_3.deleted = false;
                newCatalogItemDetail_40_3.create_date = DateTime.Now;
                newCatalogItemDetail_40_3.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_3.update_date = DateTime.Now;
                newCatalogItemDetail_40_3.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_4 = new catalog_item_detail();
                // newCatalogItemDetail_40_4.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_4.catalog_item_id = newCatalogItem4.catalog_item_id;
                newCatalogItemDetail_40_4.catalog_item_detail_code = "74308";
                newCatalogItemDetail_40_4.catalog_item_detail_name = "Carnival";
                newCatalogItemDetail_40_4.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_4.nb_units = 1;
                newCatalogItemDetail_40_4.profit_rate = 0.40;
                newCatalogItemDetail_40_4.term = 1;
                newCatalogItemDetail_40_4.description = "";
                newCatalogItemDetail_40_4.is_default = false;
                newCatalogItemDetail_40_4.deleted = false;
                newCatalogItemDetail_40_4.create_date = DateTime.Now;
                newCatalogItemDetail_40_4.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_4.update_date = DateTime.Now;
                newCatalogItemDetail_40_4.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_5 = new catalog_item_detail();
                // newCatalogItemDetail_40_5.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_5.catalog_item_id = newCatalogItem5.catalog_item_id;
                newCatalogItemDetail_40_5.catalog_item_detail_code = "74319";
                newCatalogItemDetail_40_5.catalog_item_detail_name = "Cranberry Oatmeal";
                newCatalogItemDetail_40_5.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_5.nb_units = 1;
                newCatalogItemDetail_40_5.profit_rate = 0.40;
                newCatalogItemDetail_40_5.term = 1;
                newCatalogItemDetail_40_5.description = "";
                newCatalogItemDetail_40_5.is_default = false;
                newCatalogItemDetail_40_5.deleted = false;
                newCatalogItemDetail_40_5.create_date = DateTime.Now;
                newCatalogItemDetail_40_5.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_5.update_date = DateTime.Now;
                newCatalogItemDetail_40_5.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_6 = new catalog_item_detail();
                // newCatalogItemDetail_40_6.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_6.catalog_item_id = newCatalogItem6.catalog_item_id;
                newCatalogItemDetail_40_6.catalog_item_detail_code = "74314";
                newCatalogItemDetail_40_6.catalog_item_detail_name = "Triple Chocolate Chunk";
                newCatalogItemDetail_40_6.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_6.nb_units = 1;
                newCatalogItemDetail_40_6.profit_rate = 0.40;
                newCatalogItemDetail_40_6.term = 1;
                newCatalogItemDetail_40_6.description = "";
                newCatalogItemDetail_40_6.is_default = false;
                newCatalogItemDetail_40_6.deleted = false;
                newCatalogItemDetail_40_6.create_date = DateTime.Now;
                newCatalogItemDetail_40_6.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_6.update_date = DateTime.Now;
                newCatalogItemDetail_40_6.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_7 = new catalog_item_detail();
                // newCatalogItemDetail_40_7.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_7.catalog_item_id = newCatalogItem7.catalog_item_id;
                newCatalogItemDetail_40_7.catalog_item_detail_code = "74307";
                newCatalogItemDetail_40_7.catalog_item_detail_name = "White Chocolate Macademia";
                newCatalogItemDetail_40_7.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_7.nb_units = 1;
                newCatalogItemDetail_40_7.profit_rate = 0.40;
                newCatalogItemDetail_40_7.term = 1;
                newCatalogItemDetail_40_7.description = "";
                newCatalogItemDetail_40_7.is_default = false;
                newCatalogItemDetail_40_7.deleted = false;
                newCatalogItemDetail_40_7.create_date = DateTime.Now;
                newCatalogItemDetail_40_7.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_7.update_date = DateTime.Now;
                newCatalogItemDetail_40_7.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_8 = new catalog_item_detail();
                // newCatalogItemDetail_40_8.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_8.catalog_item_id = newCatalogItem8.catalog_item_id;
                newCatalogItemDetail_40_8.catalog_item_detail_code = "74305";
                newCatalogItemDetail_40_8.catalog_item_detail_name = "Penaut Butter";
                newCatalogItemDetail_40_8.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_8.nb_units = 1;
                newCatalogItemDetail_40_8.profit_rate = 0.40;
                newCatalogItemDetail_40_8.term = 1;
                newCatalogItemDetail_40_8.description = "";
                newCatalogItemDetail_40_8.is_default = false;
                newCatalogItemDetail_40_8.deleted = false;
                newCatalogItemDetail_40_8.create_date = DateTime.Now;
                newCatalogItemDetail_40_8.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_8.update_date = DateTime.Now;
                newCatalogItemDetail_40_8.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_9 = new catalog_item_detail();
                // newCatalogItemDetail_40_9.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_9.catalog_item_id = newCatalogItem9.catalog_item_id;
                newCatalogItemDetail_40_9.catalog_item_detail_code = "74389";
                newCatalogItemDetail_40_9.catalog_item_detail_name = "Reduced Fat Chocolate Chip";
                newCatalogItemDetail_40_9.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_9.nb_units = 1;
                newCatalogItemDetail_40_9.profit_rate = 0.40;
                newCatalogItemDetail_40_9.term = 1;
                newCatalogItemDetail_40_9.description = "";
                newCatalogItemDetail_40_9.is_default = false;
                newCatalogItemDetail_40_9.deleted = false;
                newCatalogItemDetail_40_9.create_date = DateTime.Now;
                newCatalogItemDetail_40_9.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_9.update_date = DateTime.Now;
                newCatalogItemDetail_40_9.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_10 = new catalog_item_detail();
                // newCatalogItemDetail_40_10.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_10.catalog_item_id = newCatalogItem10.catalog_item_id;
                newCatalogItemDetail_40_10.catalog_item_detail_code = "74303";
                newCatalogItemDetail_40_10.catalog_item_detail_name = "Oatmeal Raisin";
                newCatalogItemDetail_40_10.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_10.nb_units = 1;
                newCatalogItemDetail_40_10.profit_rate = 0.40;
                newCatalogItemDetail_40_10.term = 1;
                newCatalogItemDetail_40_10.description = "";
                newCatalogItemDetail_40_10.is_default = false;
                newCatalogItemDetail_40_10.deleted = false;
                newCatalogItemDetail_40_10.create_date = DateTime.Now;
                newCatalogItemDetail_40_10.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_10.update_date = DateTime.Now;
                newCatalogItemDetail_40_10.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_12 = new catalog_item_detail();
                // newCatalogItemDetail_40_12.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_12.catalog_item_id = newCatalogItem12.catalog_item_id;
                newCatalogItemDetail_40_12.catalog_item_detail_code = "74315";
                newCatalogItemDetail_40_12.catalog_item_detail_name = "The Pink Cookie";
                newCatalogItemDetail_40_12.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_12.nb_units = 1;
                newCatalogItemDetail_40_12.profit_rate = 0.40;
                newCatalogItemDetail_40_12.term = 1;
                newCatalogItemDetail_40_12.description = "";
                newCatalogItemDetail_40_12.is_default = false;
                newCatalogItemDetail_40_12.deleted = false;
                newCatalogItemDetail_40_12.create_date = DateTime.Now;
                newCatalogItemDetail_40_12.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_12.update_date = DateTime.Now;
                newCatalogItemDetail_40_12.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_13 = new catalog_item_detail();
                // newCatalogItemDetail_40_13.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_13.catalog_item_id = newCatalogItem13.catalog_item_id;
                newCatalogItemDetail_40_13.catalog_item_detail_code = "70300";
                newCatalogItemDetail_40_13.catalog_item_detail_name = "Pretzel Kit";
                newCatalogItemDetail_40_13.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_13.nb_units = 1;
                newCatalogItemDetail_40_13.profit_rate = 0.40;
                newCatalogItemDetail_40_13.term = 1;
                newCatalogItemDetail_40_13.description = "";
                newCatalogItemDetail_40_13.is_default = false;
                newCatalogItemDetail_40_13.deleted = false;
                newCatalogItemDetail_40_13.create_date = DateTime.Now;
                newCatalogItemDetail_40_13.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_13.update_date = DateTime.Now;
                newCatalogItemDetail_40_13.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_14 = new catalog_item_detail();
                // newCatalogItemDetail_40_14.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_14.catalog_item_id = newCatalogItem14.catalog_item_id;
                newCatalogItemDetail_40_14.catalog_item_detail_code = "77145";
                newCatalogItemDetail_40_14.catalog_item_detail_name = "Apple Cinnamon Coffee Cake";
                newCatalogItemDetail_40_14.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_14.nb_units = 1;
                newCatalogItemDetail_40_14.profit_rate = 0.40;
                newCatalogItemDetail_40_14.term = 1;
                newCatalogItemDetail_40_14.description = "";
                newCatalogItemDetail_40_14.is_default = false;
                newCatalogItemDetail_40_14.deleted = false;
                newCatalogItemDetail_40_14.create_date = DateTime.Now;
                newCatalogItemDetail_40_14.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_14.update_date = DateTime.Now;
                newCatalogItemDetail_40_14.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_15 = new catalog_item_detail();
                // newCatalogItemDetail_40_15.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_15.catalog_item_id = newCatalogItem15.catalog_item_id;
                newCatalogItemDetail_40_15.catalog_item_detail_code = "70370";
                newCatalogItemDetail_40_15.catalog_item_detail_name = "Double Chocolate Chip Brownies";
                newCatalogItemDetail_40_15.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_15.nb_units = 1;
                newCatalogItemDetail_40_15.profit_rate = 0.40;
                newCatalogItemDetail_40_15.term = 1;
                newCatalogItemDetail_40_15.description = "";
                newCatalogItemDetail_40_15.is_default = false;
                newCatalogItemDetail_40_15.deleted = false;
                newCatalogItemDetail_40_15.create_date = DateTime.Now;
                newCatalogItemDetail_40_15.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_15.update_date = DateTime.Now;
                newCatalogItemDetail_40_15.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_1);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_2);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_3);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_4);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_5);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_6);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_7);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_8);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_9);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_10);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_12);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_13);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_14);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_15);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region catalog_item_category_catalog_item

                #region Standard product

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem1 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem1.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem1.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemCategoryCatalogItem1.display_order = 1;
                newCatalogItemCategoryCatalogItem1.deleted = false;
                newCatalogItemCategoryCatalogItem1.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem1.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem1.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem2 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem2.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem2.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem2.catalog_item_id = newCatalogItem2.catalog_item_id;
                newCatalogItemCategoryCatalogItem2.display_order = 2;
                newCatalogItemCategoryCatalogItem2.deleted = false;
                newCatalogItemCategoryCatalogItem2.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem2.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem2.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem2.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem3 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem3.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem3.catalog_item_id = newCatalogItem3.catalog_item_id;
                newCatalogItemCategoryCatalogItem3.display_order = 3;
                newCatalogItemCategoryCatalogItem3.deleted = false;
                newCatalogItemCategoryCatalogItem3.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem3.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem3.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem3.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem4 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem4.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem4.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem4.catalog_item_id = newCatalogItem4.catalog_item_id;
                newCatalogItemCategoryCatalogItem4.display_order = 4;
                newCatalogItemCategoryCatalogItem4.deleted = false;
                newCatalogItemCategoryCatalogItem4.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem4.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem4.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem4.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem5 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem5.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem5.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem5.catalog_item_id = newCatalogItem5.catalog_item_id;
                newCatalogItemCategoryCatalogItem5.display_order = 5;
                newCatalogItemCategoryCatalogItem5.deleted = false;
                newCatalogItemCategoryCatalogItem5.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem5.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem5.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem5.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem6 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem6.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem6.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem6.catalog_item_id = newCatalogItem6.catalog_item_id;
                newCatalogItemCategoryCatalogItem6.display_order = 6;
                newCatalogItemCategoryCatalogItem6.deleted = false;
                newCatalogItemCategoryCatalogItem6.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem6.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem6.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem6.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem7 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem7.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem7.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem7.catalog_item_id = newCatalogItem7.catalog_item_id;
                newCatalogItemCategoryCatalogItem7.display_order = 7;
                newCatalogItemCategoryCatalogItem7.deleted = false;
                newCatalogItemCategoryCatalogItem7.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem7.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem7.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem7.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem8 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem8.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem8.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem8.catalog_item_id = newCatalogItem8.catalog_item_id;
                newCatalogItemCategoryCatalogItem8.display_order = 8;
                newCatalogItemCategoryCatalogItem8.deleted = false;
                newCatalogItemCategoryCatalogItem8.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem8.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem8.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem8.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem9 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem9.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem9.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem9.catalog_item_id = newCatalogItem9.catalog_item_id;
                newCatalogItemCategoryCatalogItem9.display_order = 9;
                newCatalogItemCategoryCatalogItem9.deleted = false;
                newCatalogItemCategoryCatalogItem9.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem9.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem9.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem9.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem10 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem10.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem10.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem10.catalog_item_id = newCatalogItem10.catalog_item_id;
                newCatalogItemCategoryCatalogItem10.display_order = 10;
                newCatalogItemCategoryCatalogItem10.deleted = false;
                newCatalogItemCategoryCatalogItem10.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem10.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem10.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem10.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem12 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem12.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem12.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem12.catalog_item_id = newCatalogItem12.catalog_item_id;
                newCatalogItemCategoryCatalogItem12.display_order = 12;
                newCatalogItemCategoryCatalogItem12.deleted = false;
                newCatalogItemCategoryCatalogItem12.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem12.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem12.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem12.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem13 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem13.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem13.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem13.catalog_item_id = newCatalogItem13.catalog_item_id;
                newCatalogItemCategoryCatalogItem13.display_order = 13;
                newCatalogItemCategoryCatalogItem13.deleted = false;
                newCatalogItemCategoryCatalogItem13.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem13.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem13.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem13.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem14 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem14.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem14.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem14.catalog_item_id = newCatalogItem14.catalog_item_id;
                newCatalogItemCategoryCatalogItem14.display_order = 14;
                newCatalogItemCategoryCatalogItem14.deleted = false;
                newCatalogItemCategoryCatalogItem14.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem14.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem14.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem14.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem15 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem15.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem15.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem15.catalog_item_id = newCatalogItem15.catalog_item_id;
                newCatalogItemCategoryCatalogItem15.display_order = 15;
                newCatalogItemCategoryCatalogItem15.deleted = false;
                newCatalogItemCategoryCatalogItem15.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem15.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem15.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem15.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem1);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem2);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem3);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem4);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem5);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem6);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem7);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem8);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem9);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem10);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem12);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem13);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem14);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem15);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #region business_rule

                #region Order Requirements

                #region For All Sections

                #region Standard lead time

                // Create new form group object
                business_rule newBusinessRule3 = new business_rule();

                // Fill in new record data
                // newBusinessRule3.business_rule_id;           // Created upon insert
                newBusinessRule3.form_id = newForm.form_id;
                newBusinessRule3.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule3.field_id = 35;                 // 35 = min_nb_day_lead_time
                newBusinessRule3.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule3.business_rule_name = "min_nb_day_lead_time";
                newBusinessRule3.value_to_compare = "15";
                // newBusinessRule3.form_section_number;        // null, not used
                // newBusinessRule3.description;                // null, not used
                // newBusinessRule3.message;                    // null, not used
                newBusinessRule3.deleted = false;
                newBusinessRule3.create_date = DateTime.Now;
                newBusinessRule3.create_user_id = CREATE_USER_ID;
                newBusinessRule3.update_date = DateTime.Now;
                newBusinessRule3.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule3);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Minimum Total Quantity

                // Create new form group object
                business_rule newBusinessRule4 = new business_rule();

                // Fill in new record data
                // newBusinessRule4.business_rule_id;           // Created upon insert
                newBusinessRule4.form_id = newForm.form_id;
                newBusinessRule4.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule4.field_id = 49;                 // 49 = min_total_quantity
                newBusinessRule4.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule4.business_rule_name = "min_total_quantity";
                newBusinessRule4.value_to_compare = "1";
                // newBusinessRule4.form_section_number;        // null, not used
                // newBusinessRule4.description;                // null, not used
                // newBusinessRule4.message;                    // null, not used
                newBusinessRule4.deleted = false;
                newBusinessRule4.create_date = DateTime.Now;
                newBusinessRule4.create_user_id = CREATE_USER_ID;
                newBusinessRule4.update_date = DateTime.Now;
                newBusinessRule4.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule4);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #endregion

                #endregion

                #endregion

                #region business_exception

                #region Standard product - minimum day lead time

                // Create new form group object
                business_exception newBusinessException2 = new business_exception();

                // Fill in new record data
                //newBusinessException2.business_exception_id;      // newBusinessException1
                newBusinessException2.form_id = newForm.form_id;
                newBusinessException2.business_exception_name = "Standard product - minimum day lead time";
                newBusinessException2.exception_type_id = 100;      // 100 = Note
                newBusinessException2.entity_type_id = 4;           // 4 = Order
                newBusinessException2.warning_message = "15 Business Days Lead-Time Required.<BR>If requested Lead-Time is <u>LESS</u> than 15 business Days, change the delivery date to meet this requirement.";
                newBusinessException2.app_item_id = 23;             // 23 = Order Form Step 3 - Information
                newBusinessException2.message = "For Product Orders by Common Carrier <u>LESS</u> than 15 Business Days cannot be guaranteed. ";
                newBusinessException2.exception_expression = "[nb_day_lead_time] <  [min_nb_day_lead_time]";
                newBusinessException2.fees_value_expression = "";
                newBusinessException2.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException2.form_section_number;       // null, not used
                // newBusinessException2.business_rule_id;          // null, not used
                newBusinessException2.deleted = false;
                newBusinessException2.create_date = DateTime.Now;
                newBusinessException2.create_user_id = CREATE_USER_ID;
                newBusinessException2.update_date = DateTime.Now;
                newBusinessException2.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #region Standard product - minimum total quantity

                // Create new form group object
                business_exception newBusinessException7 = new business_exception();

                // Fill in new record data
                //newBusinessException7.business_exception_id;      // newBusinessException1
                newBusinessException7.form_id = newForm.form_id;
                newBusinessException7.business_exception_name = "Standard product - minimum total quantity";
                newBusinessException7.exception_type_id = 100;      // 100 = note
                newBusinessException7.entity_type_id = 4;           // 4 = Order
                newBusinessException7.warning_message = "NOTE : If order quantity is <u>less</u> than 120, Shipping Charges ($100) applies.  If order quantity is <u>greater</u> than 120 - $100 Shipping Charges is waived.";
                newBusinessException7.app_item_id = 24;             // 24 = Order form step 4 - order detail
                newBusinessException7.message = "NOTE : If order quantity is <u>less</u> than 120, Shipping Charges ($100) applies.  If order quantity is <u>greater</u> than 120 - $100 Shipping Charges is waived.";
                newBusinessException7.exception_expression = "([total_quantity] <  [min_total_quantity])";
                newBusinessException7.fees_value_expression = "";
                newBusinessException7.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException7.form_section_number;       // null, not used
                // newBusinessException7.business_rule_id;          // null, not used
                newBusinessException7.deleted = false;
                newBusinessException7.create_date = DateTime.Now;
                newBusinessException7.create_user_id = CREATE_USER_ID;
                newBusinessException7.update_date = DateTime.Now;
                newBusinessException7.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException7);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #endregion

                #endregion

                #region PA form

                #region form

                // Create new form object
                form newPAForm = new form();

                // Fill in new form data
                // newPAForm.form_id;                             // Autonumber generated on insert
                newPAForm.form_group_id = newFormGroup.form_group_id;
                newPAForm.entity_type_id = 12;                     // Type 12 = program agreement
                newPAForm.form_code = "MC24";
                newPAForm.form_name = "Otis Spring 2011 Bulk 14 Item PA";
                newPAForm.description = "";
                newPAForm.order_terms_text = "";
                newPAForm.start_date = new DateTime(2010, 12, 8);;
                newPAForm.end_date = new DateTime(2011, 6, 30);;
                newPAForm.closing_time = new DateTime(2011, 6, 30);;
                newPAForm.image_url = "images/CatalogItem/chocochunk.gif";
                newPAForm.is_base_form = false;
                newPAForm.parent_form_id = 48;                     // Base form for orders in prod and dev
                newPAForm.is_product_price_updatable = false;
                newPAForm.is_quantity_adjustment_allowed = true;
                newPAForm.tax_postal_address_type_id = 2;
                newPAForm.enabled = true;
                newPAForm.deleted = false;
                newPAForm.version = 1;
                newPAForm.program_id = 3;                         // Otis = 3, PV = 2
                newPAForm.program_type_id = 7;
                newPAForm.program_basics_text = "";
                newPAForm.create_date = DateTime.Now;
                newPAForm.create_user_id = CREATE_USER_ID;
                newPAForm.update_date = DateTime.Now;
                newPAForm.update_user_id = CREATE_USER_ID;
                //newPAForm.warehouse_type_id;
                newPAForm.is_bulk = true;
                newPAForm.report_name = "OrderForm";
                newPAForm.is_warehouse_selectable = true;
                //newPAForm.default_warehouse_id;
                newPAForm.season_id = 8;

                // Add the new form to the db context
                context.forms.InsertOnSubmit(newPAForm);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form id = " + newPAForm.form_id.ToString());

                #endregion

                #region form_permission

                form_permission newPAFormPermission1 = new form_permission();
                //newPAFormPermission1.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission1.form_id = newPAForm.form_id;
                newPAFormPermission1.role_id = 0;                     // User
                newPAFormPermission1.allow_read = false;
                newPAFormPermission1.allow_write = false;
                newPAFormPermission1.create_date = DateTime.Now;
                newPAFormPermission1.create_user_id = CREATE_USER_ID;
                newPAFormPermission1.update_date = DateTime.Now;
                newPAFormPermission1.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission2 = new form_permission();
                //newPAFormPermission2.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission2.form_id = newPAForm.form_id;
                newPAFormPermission2.role_id = 1;                     // Field Sale Manager (FM)
                newPAFormPermission2.allow_read = true;
                newPAFormPermission2.allow_write = true;
                newPAFormPermission2.create_date = DateTime.Now;
                newPAFormPermission2.create_user_id = CREATE_USER_ID;
                newPAFormPermission2.update_date = DateTime.Now;
                newPAFormPermission2.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission3 = new form_permission();
                //newPAFormPermission3.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission3.form_id = newPAForm.form_id;
                newPAFormPermission3.role_id = 2;                     // Field Support
                newPAFormPermission3.allow_read = true;
                newPAFormPermission3.allow_write = true;
                newPAFormPermission3.create_date = DateTime.Now;
                newPAFormPermission3.create_user_id = CREATE_USER_ID;
                newPAFormPermission3.update_date = DateTime.Now;
                newPAFormPermission3.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission4 = new form_permission();
                //newPAFormPermission4.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission4.form_id = newPAForm.form_id;
                newPAFormPermission4.role_id = 3;                     // Accounting Manager
                newPAFormPermission4.allow_read = true;
                newPAFormPermission4.allow_write = true;
                newPAFormPermission4.create_date = DateTime.Now;
                newPAFormPermission4.create_user_id = CREATE_USER_ID;
                newPAFormPermission4.update_date = DateTime.Now;
                newPAFormPermission4.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission5 = new form_permission();
                //newPAFormPermission5.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission5.form_id = newPAForm.form_id;
                newPAFormPermission5.role_id = 4;                     // Admin
                newPAFormPermission5.allow_read = true;
                newPAFormPermission5.allow_write = true;
                newPAFormPermission5.create_date = DateTime.Now;
                newPAFormPermission5.create_user_id = CREATE_USER_ID;
                newPAFormPermission5.update_date = DateTime.Now;
                newPAFormPermission5.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission6 = new form_permission();
                //newPAFormPermission6.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission6.form_id = newPAForm.form_id;
                newPAFormPermission6.role_id = 5;                     // Super User
                newPAFormPermission6.allow_read = true;
                newPAFormPermission6.allow_write = true;
                newPAFormPermission6.create_date = DateTime.Now;
                newPAFormPermission6.create_user_id = CREATE_USER_ID;
                newPAFormPermission6.update_date = DateTime.Now;
                newPAFormPermission6.update_user_id = CREATE_USER_ID;

                // Add the new form permissions to the db context
                context.form_permissions.InsertOnSubmit(newPAFormPermission1);
                context.form_permissions.InsertOnSubmit(newPAFormPermission2);
                context.form_permissions.InsertOnSubmit(newPAFormPermission3);
                context.form_permissions.InsertOnSubmit(newPAFormPermission4);
                context.form_permissions.InsertOnSubmit(newPAFormPermission5);
                context.form_permissions.InsertOnSubmit(newPAFormPermission6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form permissions ok");

                #endregion

                #region form_profit_rate

                form_profit_rate newPAFormProfitRate1 = new form_profit_rate();
                //newPAFormProfitRate1.form_profit_rate_id;                // Autonumber generated on insert
                newPAFormProfitRate1.form_id = newPAForm.form_id;
                newPAFormProfitRate1.profit_rate_id = 1;         // 1 = 40%, 2 = 45%, 3 = 50%
                newPAFormProfitRate1.deleted = false;
                newPAFormProfitRate1.create_date = DateTime.Now;
                newPAFormProfitRate1.create_user_id = CREATE_USER_ID;
                newPAFormProfitRate1.update_date = DateTime.Now;
                newPAFormProfitRate1.update_user_id = CREATE_USER_ID;

                //form_profit_rate newPAFormProfitRate2 = new form_profit_rate();
                ////newPAFormProfitRate2.form_profit_rate_id;                // Autonumber generated on insert
                //newPAFormProfitRate2.form_id = newPAForm.form_id;
                //newPAFormProfitRate2.profit_rate_id = 2;         // 1 = 40%, 2 = 45%, 3 = 50%
                //newPAFormProfitRate2.deleted = false;
                //newPAFormProfitRate2.create_date = DateTime.Now;
                //newPAFormProfitRate2.create_user_id = CREATE_USER_ID;
                //newPAFormProfitRate2.update_date = DateTime.Now;
                //newPAFormProfitRate2.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_profit_rates.InsertOnSubmit(newPAFormProfitRate1);
                //context.form_profit_rates.InsertOnSubmit(newPAFormProfitRate2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form profit rate ok");

                #endregion

                #region form_section

                #region order catalog section

                // Create new record
                form_section newPAFormSection1 = new form_section();
                // newPAFormSection1.form_section_id;              // Autonumber generated on insert
                newPAFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newPAFormSection1.form_id = newPAForm.form_id;
                newPAFormSection1.form_section_type_id = 1;        // 1 = standard product, 2 = supply product, 3 = optional product
                newPAFormSection1.form_section_number = 1;
                newPAFormSection1.form_section_title = "Otis Spring 2011 Bulk 14 Item";
                newPAFormSection1.description = "";
                newPAFormSection1.deleted = false;
                newPAFormSection1.create_date = DateTime.Now;
                newPAFormSection1.create_user_id = CREATE_USER_ID;
                newPAFormSection1.update_date = DateTime.Now;
                newPAFormSection1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newPAFormSection1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newPAFormSection1.form_section_id.ToString());

                #endregion

                #endregion

                #region Supplies in PA

                #region catalog_group

                // Create new record
                catalog_group newSupplyCatalogGroup = new catalog_group();
                // newSupplyCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newSupplyCatalogGroup.catalog_group_code = "MC24";
                newSupplyCatalogGroup.catalog_group_name = "Otis Spring 2011 Bulk 14 Item Supplies";
                newSupplyCatalogGroup.description = "";
                newSupplyCatalogGroup.deleted = false;
                newSupplyCatalogGroup.create_date = DateTime.Now;
                newSupplyCatalogGroup.create_user_id = CREATE_USER_ID;
                newSupplyCatalogGroup.update_date = DateTime.Now;
                newSupplyCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newSupplyCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newSupplyCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region product

                #region Create new record

                product newSupplyProduct1 = new product();
                // newSupplyProduct1.product_id;                      // Autonumber generated on insert
                newSupplyProduct1.product_code = "CD2401";
                newSupplyProduct1.product_name = "Otis Bulk Brochure priced";
                newSupplyProduct1.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct1.nb_units = 1;                       // boxes per case
                newSupplyProduct1.nb_day_lead_time = 0;
                newSupplyProduct1.product_status_id = 101;            // 101 = Active
                newSupplyProduct1.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct1.business_division_id = 1;           // 1 = US
                newSupplyProduct1.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct1.coupon_id = null;
                newSupplyProduct1.description = "";
                newSupplyProduct1.image_url = "";
                newSupplyProduct1.is_free_sample = false;
                newSupplyProduct1.oracle_code = "";
                newSupplyProduct1.IVCOUP = "";
                newSupplyProduct1.IVITEM = "CD2401";
                newSupplyProduct1.unit_cost = null;
                newSupplyProduct1.vendor_id = 27;                     // 27 = prod
                newSupplyProduct1.vendor_item_code = "CD2401";
                newSupplyProduct1.deleted = false;
                newSupplyProduct1.create_date = DateTime.Now;
                newSupplyProduct1.create_user_id = CREATE_USER_ID;
                newSupplyProduct1.update_date = DateTime.Now;
                newSupplyProduct1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct2 = new product();
                // newSupplyProduct2.product_id;                      // Autonumber generated on insert
                newSupplyProduct2.product_code = "CD2402";
                newSupplyProduct2.product_name = "Otis Bulk Brochure Un-Priced";
                newSupplyProduct2.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct2.nb_units = 1;                       // boxes per case
                newSupplyProduct2.nb_day_lead_time = 0;
                newSupplyProduct2.product_status_id = 101;            // 101 = Active
                newSupplyProduct2.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct2.business_division_id = 1;           // 1 = US
                newSupplyProduct2.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct2.coupon_id = null;
                newSupplyProduct2.description = "";
                newSupplyProduct2.image_url = "";
                newSupplyProduct2.is_free_sample = false;
                newSupplyProduct2.oracle_code = "";
                newSupplyProduct2.IVCOUP = "";
                newSupplyProduct2.IVITEM = "CD2402";
                newSupplyProduct2.unit_cost = null;
                newSupplyProduct2.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct2.vendor_item_code = "CD2402";
                newSupplyProduct2.deleted = false;
                newSupplyProduct2.create_date = DateTime.Now;
                newSupplyProduct2.create_user_id = CREATE_USER_ID;
                newSupplyProduct2.update_date = DateTime.Now;
                newSupplyProduct2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct3 = new product();
                // newSupplyProduct3.product_id;                      // Autonumber generated on insert
                newSupplyProduct3.product_code = "CD2062";
                newSupplyProduct3.product_name = "Otis Mass Display";
                newSupplyProduct3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct3.nb_units = 1;                       // boxes per case
                newSupplyProduct3.nb_day_lead_time = 0;
                newSupplyProduct3.product_status_id = 101;            // 101 = Active
                newSupplyProduct3.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct3.business_division_id = 1;           // 1 = US
                newSupplyProduct3.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct3.coupon_id = null;
                newSupplyProduct3.description = "";
                newSupplyProduct3.image_url = "";
                newSupplyProduct3.is_free_sample = false;
                newSupplyProduct3.oracle_code = "";
                newSupplyProduct3.IVCOUP = "";
                newSupplyProduct3.IVITEM = "CD2062";
                newSupplyProduct3.unit_cost = null;
                newSupplyProduct3.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct3.vendor_item_code = "CD2062";
                newSupplyProduct3.deleted = false;
                newSupplyProduct3.create_date = DateTime.Now;
                newSupplyProduct3.create_user_id = CREATE_USER_ID;
                newSupplyProduct3.update_date = DateTime.Now;
                newSupplyProduct3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct4 = new product();
                // newSupplyProduct4.product_id;                      // Autonumber generated on insert
                newSupplyProduct4.product_code = "CD2098";
                newSupplyProduct4.product_name = "Generic Large Outer Envelope";
                newSupplyProduct4.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct4.nb_units = 1;                       // boxes per case
                newSupplyProduct4.nb_day_lead_time = 0;
                newSupplyProduct4.product_status_id = 101;            // 101 = Active
                newSupplyProduct4.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct4.business_division_id = 1;           // 1 = US
                newSupplyProduct4.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct4.coupon_id = null;
                newSupplyProduct4.description = "";
                newSupplyProduct4.image_url = "";
                newSupplyProduct4.is_free_sample = false;
                newSupplyProduct4.oracle_code = "";
                newSupplyProduct4.IVCOUP = "";
                newSupplyProduct4.IVITEM = "CD2098";
                newSupplyProduct4.unit_cost = null;
                newSupplyProduct4.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct4.vendor_item_code = "CD2098";
                newSupplyProduct4.deleted = false;
                newSupplyProduct4.create_date = DateTime.Now;
                newSupplyProduct4.create_user_id = CREATE_USER_ID;
                newSupplyProduct4.update_date = DateTime.Now;
                newSupplyProduct4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct5 = new product();
                // newSupplyProduct5.product_id;                      // Autonumber generated on insert
                newSupplyProduct5.product_code = "CD2086";
                newSupplyProduct5.product_name = "Otis Launch Poster";
                newSupplyProduct5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct5.nb_units = 1;                       // boxes per case
                newSupplyProduct5.nb_day_lead_time = 0;
                newSupplyProduct5.product_status_id = 101;            // 101 = Active
                newSupplyProduct5.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct5.business_division_id = 1;           // 1 = US
                newSupplyProduct5.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct5.coupon_id = null;
                newSupplyProduct5.description = "";
                newSupplyProduct5.image_url = "";
                newSupplyProduct5.is_free_sample = false;
                newSupplyProduct5.oracle_code = "";
                newSupplyProduct5.IVCOUP = "";
                newSupplyProduct5.IVITEM = "CD2086";
                newSupplyProduct5.unit_cost = null;
                newSupplyProduct5.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct5.vendor_item_code = "CD2086";
                newSupplyProduct5.deleted = false;
                newSupplyProduct5.create_date = DateTime.Now;
                newSupplyProduct5.create_user_id = CREATE_USER_ID;
                newSupplyProduct5.update_date = DateTime.Now;
                newSupplyProduct5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct6 = new product();
                // newSupplyProduct6.product_id;                      // Autonumber generated on insert
                newSupplyProduct6.product_code = "CDBULK2306";
                newSupplyProduct6.product_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyProduct6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct6.nb_units = 1;                       // boxes per case
                newSupplyProduct6.nb_day_lead_time = 0;
                newSupplyProduct6.product_status_id = 101;            // 101 = Active
                newSupplyProduct6.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct6.business_division_id = 1;           // 1 = US
                newSupplyProduct6.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct6.coupon_id = null;
                newSupplyProduct6.description = "";
                newSupplyProduct6.image_url = "";
                newSupplyProduct6.is_free_sample = false;
                newSupplyProduct6.oracle_code = "";
                newSupplyProduct6.IVCOUP = "";
                newSupplyProduct6.IVITEM = "CDBULK2306";
                newSupplyProduct6.unit_cost = null;
                newSupplyProduct6.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct6.vendor_item_code = "CDBULK2306";
                newSupplyProduct6.deleted = false;
                newSupplyProduct6.create_date = DateTime.Now;
                newSupplyProduct6.create_user_id = CREATE_USER_ID;
                newSupplyProduct6.update_date = DateTime.Now;
                newSupplyProduct6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct7 = new product();
                // newSupplyProduct7.product_id;                      // Autonumber generated on insert
                newSupplyProduct7.product_code = "CD2093";
                newSupplyProduct7.product_name = "QSP Plastic Food Bags";
                newSupplyProduct7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct7.nb_units = 1;                       // boxes per case
                newSupplyProduct7.nb_day_lead_time = 0;
                newSupplyProduct7.product_status_id = 101;            // 101 = Active
                newSupplyProduct7.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct7.business_division_id = 1;           // 1 = US
                newSupplyProduct7.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct7.coupon_id = null;
                newSupplyProduct7.description = "";
                newSupplyProduct7.image_url = "";
                newSupplyProduct7.is_free_sample = false;
                newSupplyProduct7.oracle_code = "";
                newSupplyProduct7.IVCOUP = "";
                newSupplyProduct7.IVITEM = "CD2093";
                newSupplyProduct7.unit_cost = null;
                newSupplyProduct7.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct7.vendor_item_code = "CD2093";
                newSupplyProduct7.deleted = false;
                newSupplyProduct7.create_date = DateTime.Now;
                newSupplyProduct7.create_user_id = CREATE_USER_ID;
                newSupplyProduct7.update_date = DateTime.Now;
                newSupplyProduct7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct8 = new product();
                // newSupplyProduct8.product_id;                      // Autonumber generated on insert
                newSupplyProduct8.product_code = "CD2106";
                newSupplyProduct8.product_name = "Generic Medium Collection Envelope";
                newSupplyProduct8.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct8.nb_units = 1;                       // boxes per case
                newSupplyProduct8.nb_day_lead_time = 0;
                newSupplyProduct8.product_status_id = 101;            // 101 = Active
                newSupplyProduct8.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct8.business_division_id = 1;           // 1 = US
                newSupplyProduct8.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct8.coupon_id = null;
                newSupplyProduct8.description = "";
                newSupplyProduct8.image_url = "";
                newSupplyProduct8.is_free_sample = false;
                newSupplyProduct8.oracle_code = "";
                newSupplyProduct8.IVCOUP = "";
                newSupplyProduct8.IVITEM = "CD2106";
                newSupplyProduct8.unit_cost = null;
                newSupplyProduct8.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct8.vendor_item_code = "CD2106";
                newSupplyProduct8.deleted = false;
                newSupplyProduct8.create_date = DateTime.Now;
                newSupplyProduct8.create_user_id = CREATE_USER_ID;
                newSupplyProduct8.update_date = DateTime.Now;
                newSupplyProduct8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct9 = new product();
                // newSupplyProduct9.product_id;                      // Autonumber generated on insert
                newSupplyProduct9.product_code = "CD2103";
                newSupplyProduct9.product_name = "Just Right Cookie (Baking Instructions)";
                newSupplyProduct9.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct9.nb_units = 1;                       // boxes per case
                newSupplyProduct9.nb_day_lead_time = 0;
                newSupplyProduct9.product_status_id = 101;            // 101 = Active
                newSupplyProduct9.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct9.business_division_id = 1;           // 1 = US
                newSupplyProduct9.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct9.coupon_id = null;
                newSupplyProduct9.description = "";
                newSupplyProduct9.image_url = "";
                newSupplyProduct9.is_free_sample = false;
                newSupplyProduct9.oracle_code = "";
                newSupplyProduct9.IVCOUP = "";
                newSupplyProduct9.IVITEM = "CD2103";
                newSupplyProduct9.unit_cost = null;
                newSupplyProduct9.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct9.vendor_item_code = "CD2103";
                newSupplyProduct9.deleted = false;
                newSupplyProduct9.create_date = DateTime.Now;
                newSupplyProduct9.create_user_id = CREATE_USER_ID;
                newSupplyProduct9.update_date = DateTime.Now;
                newSupplyProduct9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.products.InsertOnSubmit(newSupplyProduct1);
                context.products.InsertOnSubmit(newSupplyProduct2);
                context.products.InsertOnSubmit(newSupplyProduct3);
                context.products.InsertOnSubmit(newSupplyProduct4);
                context.products.InsertOnSubmit(newSupplyProduct5);
                context.products.InsertOnSubmit(newSupplyProduct6);
                context.products.InsertOnSubmit(newSupplyProduct7);
                context.products.InsertOnSubmit(newSupplyProduct8);
                context.products.InsertOnSubmit(newSupplyProduct9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("products ok");

                #endregion

                #region Priced catalog

                // Create new record
                catalog newSupplyCatalogPriced = new catalog();
                // newSupplyCatalogPriced.newCatalog_id;          // Autonumber generated on insert
                newSupplyCatalogPriced.catalog_group_id = newSupplyCatalogGroup.catalog_group_id;
                newSupplyCatalogPriced.catalog_code = "MC24";
                newSupplyCatalogPriced.catalog_name = "Otis Spring 2011 Bulk 14 Item Supplies Priced";
                newSupplyCatalogPriced.culture = "en-US";
                newSupplyCatalogPriced.description = "";
                newSupplyCatalogPriced.start_date = new DateTime(2010, 12, 8);
                newSupplyCatalogPriced.end_date = new DateTime(2011, 6, 30);
                newSupplyCatalogPriced.deleted = false;
                newSupplyCatalogPriced.create_date = DateTime.Now;
                newSupplyCatalogPriced.create_user_id = CREATE_USER_ID;
                newSupplyCatalogPriced.update_date = DateTime.Now;
                newSupplyCatalogPriced.update_user_id = CREATE_USER_ID;
                newSupplyCatalogPriced.is_priced = true;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newSupplyCatalogPriced);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newSupplyCatalogPriced.catalog_id.ToString());

                #endregion

                #region Priced catalog_item_category

                // Create new record
                catalog_item_category newSupplyCatalogItemCategoryPriced1 = new catalog_item_category();
                // newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryPriced1.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemCategoryPriced1.catalog_item_category_name = "Otis Spring 2011 Bulk 14 Item Supplies Priced";
                newSupplyCatalogItemCategoryPriced1.parent_catalog_item_category_id = 0;
                newSupplyCatalogItemCategoryPriced1.deleted = false;
                newSupplyCatalogItemCategoryPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newSupplyCatalogItemCategoryPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newSupplyCatalogItemCategoryPriced1.catalog_item_category_id.ToString());

                #endregion

                #region Priced form_section

                // Create new record
                form_section newSupplyPAFormSectionPriced1 = new form_section();
                // newSupplyPAFormSectionPriced1.form_section_id;              // Autonumber generated on insert
                newSupplyPAFormSectionPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyPAFormSectionPriced1.form_id = newPAForm.form_id;
                newSupplyPAFormSectionPriced1.form_section_type_id = 2;        // 1 = standard product, 2 = supply product, 3 = optional product
                newSupplyPAFormSectionPriced1.form_section_number = 2;
                newSupplyPAFormSectionPriced1.form_section_title = "Otis Spring 2011 Bulk 14 Item Supplies Priced";
                newSupplyPAFormSectionPriced1.description = "";
                newSupplyPAFormSectionPriced1.deleted = false;
                newSupplyPAFormSectionPriced1.create_date = DateTime.Now;
                newSupplyPAFormSectionPriced1.create_user_id = CREATE_USER_ID;
                newSupplyPAFormSectionPriced1.update_date = DateTime.Now;
                newSupplyPAFormSectionPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newSupplyPAFormSectionPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newSupplyPAFormSectionPriced1.form_section_id.ToString());

                #endregion

                #region Priced catalog_item

                #region Create new record

                catalog_item newSupplyCatalogItemPriced1 = new catalog_item();
                // newSupplyCatalogItemPriced1.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced1.product_id = newSupplyProduct1.product_id;
                newSupplyCatalogItemPriced1.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced1.catalog_item_code = "CD2401";
                newSupplyCatalogItemPriced1.catalog_item_name = "Otis Bulk Brochure Priced";
                newSupplyCatalogItemPriced1.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced1.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced1.image_url = "";
                newSupplyCatalogItemPriced1.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced1.catalog_item_export_name = "";
                newSupplyCatalogItemPriced1.description = "";
                newSupplyCatalogItemPriced1.deleted = false;
                newSupplyCatalogItemPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced3 = new catalog_item();
                // newSupplyCatalogItemPriced3.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced3.product_id = newSupplyProduct3.product_id;
                newSupplyCatalogItemPriced3.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced3.catalog_item_code = "CD2062";
                newSupplyCatalogItemPriced3.catalog_item_name = "Otis Mass Display";
                newSupplyCatalogItemPriced3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced3.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced3.image_url = "";
                newSupplyCatalogItemPriced3.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced3.catalog_item_export_name = "";
                newSupplyCatalogItemPriced3.description = "";
                newSupplyCatalogItemPriced3.deleted = false;
                newSupplyCatalogItemPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced4 = new catalog_item();
                // newSupplyCatalogItemPriced4.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced4.product_id = newSupplyProduct4.product_id;
                newSupplyCatalogItemPriced4.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced4.catalog_item_code = "CD2098";
                newSupplyCatalogItemPriced4.catalog_item_name = "Generic Large Outer Envelope";
                newSupplyCatalogItemPriced4.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced4.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced4.image_url = "";
                newSupplyCatalogItemPriced4.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced4.catalog_item_export_name = "";
                newSupplyCatalogItemPriced4.description = "";
                newSupplyCatalogItemPriced4.deleted = false;
                newSupplyCatalogItemPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced5 = new catalog_item();
                // newSupplyCatalogItemPriced5.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced5.product_id = newSupplyProduct5.product_id;
                newSupplyCatalogItemPriced5.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced5.catalog_item_code = "CD2086";
                newSupplyCatalogItemPriced5.catalog_item_name = "Otis Launch Poster";
                newSupplyCatalogItemPriced5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced5.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced5.image_url = "";
                newSupplyCatalogItemPriced5.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced5.catalog_item_export_name = "";
                newSupplyCatalogItemPriced5.description = "";
                newSupplyCatalogItemPriced5.deleted = false;
                newSupplyCatalogItemPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced6 = new catalog_item();
                // newSupplyCatalogItemPriced6.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced6.product_id = newSupplyProduct6.product_id;
                newSupplyCatalogItemPriced6.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced6.catalog_item_code = "CDBULK2306";
                newSupplyCatalogItemPriced6.catalog_item_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemPriced6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced6.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced6.image_url = "";
                newSupplyCatalogItemPriced6.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced6.catalog_item_export_name = "";
                newSupplyCatalogItemPriced6.description = "";
                newSupplyCatalogItemPriced6.deleted = false;
                newSupplyCatalogItemPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced7 = new catalog_item();
                // newSupplyCatalogItemPriced7.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced7.product_id = newSupplyProduct7.product_id;
                newSupplyCatalogItemPriced7.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced7.catalog_item_code = "CD2093";
                newSupplyCatalogItemPriced7.catalog_item_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemPriced7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced7.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced7.image_url = "";
                newSupplyCatalogItemPriced7.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced7.catalog_item_export_name = "";
                newSupplyCatalogItemPriced7.description = "";
                newSupplyCatalogItemPriced7.deleted = false;
                newSupplyCatalogItemPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced8 = new catalog_item();
                // newSupplyCatalogItemPriced8.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced8.product_id = newSupplyProduct8.product_id;
                newSupplyCatalogItemPriced8.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced8.catalog_item_code = "CD2106";
                newSupplyCatalogItemPriced8.catalog_item_name = "Generic Medium Collection Envelope";
                newSupplyCatalogItemPriced8.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced8.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced8.image_url = "";
                newSupplyCatalogItemPriced8.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced8.catalog_item_export_name = "";
                newSupplyCatalogItemPriced8.description = "";
                newSupplyCatalogItemPriced8.deleted = false;
                newSupplyCatalogItemPriced8.create_date = DateTime.Now;
                newSupplyCatalogItemPriced8.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced8.update_date = DateTime.Now;
                newSupplyCatalogItemPriced8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced9 = new catalog_item();
                // newSupplyCatalogItemPriced9.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced9.product_id = newSupplyProduct9.product_id;
                newSupplyCatalogItemPriced9.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced9.catalog_item_code = "CD2103";
                newSupplyCatalogItemPriced9.catalog_item_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemPriced9.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced9.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced9.image_url = "";
                newSupplyCatalogItemPriced9.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced9.catalog_item_export_name = "";
                newSupplyCatalogItemPriced9.description = "";
                newSupplyCatalogItemPriced9.deleted = false;
                newSupplyCatalogItemPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced1);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced3);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced4);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced5);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced6);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced7);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced8);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region Priced catalog_item_detail

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced1 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced1.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced1.catalog_item_id = newSupplyCatalogItemPriced1.catalog_item_id;
                newSupplyCatalogItemDetailPriced1.catalog_item_detail_code = "CD2401";
                newSupplyCatalogItemDetailPriced1.catalog_item_detail_name = "Otis Bulk Brochure Priced";
                newSupplyCatalogItemDetailPriced1.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced1.nb_units = 1;
                newSupplyCatalogItemDetailPriced1.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced1.term = 1;
                newSupplyCatalogItemDetailPriced1.description = "";
                newSupplyCatalogItemDetailPriced1.is_default = false;
                newSupplyCatalogItemDetailPriced1.deleted = false;
                newSupplyCatalogItemDetailPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced3 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced3.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced3.catalog_item_id = newSupplyCatalogItemPriced3.catalog_item_id;
                newSupplyCatalogItemDetailPriced3.catalog_item_detail_code = "CD2062";
                newSupplyCatalogItemDetailPriced3.catalog_item_detail_name = "Otis Mass Display";
                newSupplyCatalogItemDetailPriced3.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced3.nb_units = 1;
                newSupplyCatalogItemDetailPriced3.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced3.term = 1;
                newSupplyCatalogItemDetailPriced3.description = "";
                newSupplyCatalogItemDetailPriced3.is_default = false;
                newSupplyCatalogItemDetailPriced3.deleted = false;
                newSupplyCatalogItemDetailPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced4 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced4.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced4.catalog_item_id = newSupplyCatalogItemPriced4.catalog_item_id;
                newSupplyCatalogItemDetailPriced4.catalog_item_detail_code = "CD2098";
                newSupplyCatalogItemDetailPriced4.catalog_item_detail_name = "Generic Large Outer Envelope";
                newSupplyCatalogItemDetailPriced4.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced4.nb_units = 1;
                newSupplyCatalogItemDetailPriced4.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced4.term = 1;
                newSupplyCatalogItemDetailPriced4.description = "";
                newSupplyCatalogItemDetailPriced4.is_default = false;
                newSupplyCatalogItemDetailPriced4.deleted = false;
                newSupplyCatalogItemDetailPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced5 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced5.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced5.catalog_item_id = newSupplyCatalogItemPriced5.catalog_item_id;
                newSupplyCatalogItemDetailPriced5.catalog_item_detail_code = "CD2086";
                newSupplyCatalogItemDetailPriced5.catalog_item_detail_name = "Otis Launch Poster";
                newSupplyCatalogItemDetailPriced5.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced5.nb_units = 1;
                newSupplyCatalogItemDetailPriced5.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced5.term = 1;
                newSupplyCatalogItemDetailPriced5.description = "";
                newSupplyCatalogItemDetailPriced5.is_default = false;
                newSupplyCatalogItemDetailPriced5.deleted = false;
                newSupplyCatalogItemDetailPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced6 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced6.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced6.catalog_item_id = newSupplyCatalogItemPriced6.catalog_item_id;
                newSupplyCatalogItemDetailPriced6.catalog_item_detail_code = "CDBULK2306";
                newSupplyCatalogItemDetailPriced6.catalog_item_detail_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemDetailPriced6.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced6.nb_units = 1;
                newSupplyCatalogItemDetailPriced6.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced6.term = 1;
                newSupplyCatalogItemDetailPriced6.description = "";
                newSupplyCatalogItemDetailPriced6.is_default = false;
                newSupplyCatalogItemDetailPriced6.deleted = false;
                newSupplyCatalogItemDetailPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced7 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced7.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced7.catalog_item_id = newSupplyCatalogItemPriced7.catalog_item_id;
                newSupplyCatalogItemDetailPriced7.catalog_item_detail_code = "CD2093";
                newSupplyCatalogItemDetailPriced7.catalog_item_detail_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemDetailPriced7.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced7.nb_units = 1;
                newSupplyCatalogItemDetailPriced7.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced7.term = 1;
                newSupplyCatalogItemDetailPriced7.description = "";
                newSupplyCatalogItemDetailPriced7.is_default = false;
                newSupplyCatalogItemDetailPriced7.deleted = false;
                newSupplyCatalogItemDetailPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced8 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced8.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced8.catalog_item_id = newSupplyCatalogItemPriced8.catalog_item_id;
                newSupplyCatalogItemDetailPriced8.catalog_item_detail_code = "CD2106";
                newSupplyCatalogItemDetailPriced8.catalog_item_detail_name = "Generic Medium Collection Envelope";
                newSupplyCatalogItemDetailPriced8.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced8.nb_units = 1;
                newSupplyCatalogItemDetailPriced8.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced8.term = 1;
                newSupplyCatalogItemDetailPriced8.description = "";
                newSupplyCatalogItemDetailPriced8.is_default = false;
                newSupplyCatalogItemDetailPriced8.deleted = false;
                newSupplyCatalogItemDetailPriced8.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced8.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced8.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced9 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced9.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced9.catalog_item_id = newSupplyCatalogItemPriced9.catalog_item_id;
                newSupplyCatalogItemDetailPriced9.catalog_item_detail_code = "CD2103";
                newSupplyCatalogItemDetailPriced9.catalog_item_detail_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemDetailPriced9.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced9.nb_units = 1;
                newSupplyCatalogItemDetailPriced9.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced9.term = 1;
                newSupplyCatalogItemDetailPriced9.description = "";
                newSupplyCatalogItemDetailPriced9.is_default = false;
                newSupplyCatalogItemDetailPriced9.deleted = false;
                newSupplyCatalogItemDetailPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced9.update_user_id = CREATE_USER_ID;

                #endregion


                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced1);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced3);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced4);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced5);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced6);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced7);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced8);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region Priced catalog_item_category_catalog_item

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced1 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_id = newSupplyCatalogItemPriced1.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced1.display_order = 1;
                newSupplyCatalogItemCategoryCatalogItemPriced1.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced3 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_id = newSupplyCatalogItemPriced3.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced3.display_order = 5;
                newSupplyCatalogItemCategoryCatalogItemPriced3.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced4 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced4.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced4.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced4.catalog_item_id = newSupplyCatalogItemPriced4.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced4.display_order = 6;
                newSupplyCatalogItemCategoryCatalogItemPriced4.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced5 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_id = newSupplyCatalogItemPriced5.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced5.display_order = 2;
                newSupplyCatalogItemCategoryCatalogItemPriced5.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced6 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_id = newSupplyCatalogItemPriced6.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced6.display_order = 3;
                newSupplyCatalogItemCategoryCatalogItemPriced6.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced7 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_id = newSupplyCatalogItemPriced7.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced7.display_order = 4;
                newSupplyCatalogItemCategoryCatalogItemPriced7.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced8 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced8.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced8.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced8.catalog_item_id = newSupplyCatalogItemPriced8.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced8.display_order = 7;
                newSupplyCatalogItemCategoryCatalogItemPriced8.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced8.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced8.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced8.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced9 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced9.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced9.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced9.catalog_item_id = newSupplyCatalogItemPriced9.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced9.display_order = 8;
                newSupplyCatalogItemCategoryCatalogItemPriced9.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced9.update_user_id = CREATE_USER_ID;

                #endregion


                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced1);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced3);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced4);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced5);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced6);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced7);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced8);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced9);
                
                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #region UnPriced catalog

                // Create new record
                catalog newSupplyCatalogUnPriced = new catalog();
                // newSupplyCatalogUnPriced.newCatalog_id;          // Autonumber generated on insert
                newSupplyCatalogUnPriced.catalog_group_id = newSupplyCatalogGroup.catalog_group_id;
                newSupplyCatalogUnPriced.catalog_code = "MC24";
                newSupplyCatalogUnPriced.catalog_name = "Otis Spring 2011 Bulk 14 Item Supplies UnPriced";
                newSupplyCatalogUnPriced.culture = "en-US";
                newSupplyCatalogUnPriced.description = "";
                newSupplyCatalogUnPriced.start_date = new DateTime(2010, 12, 8);
                newSupplyCatalogUnPriced.end_date = new DateTime(2011, 6, 30);
                newSupplyCatalogUnPriced.deleted = false;
                newSupplyCatalogUnPriced.create_date = DateTime.Now;
                newSupplyCatalogUnPriced.create_user_id = CREATE_USER_ID;
                newSupplyCatalogUnPriced.update_date = DateTime.Now;
                newSupplyCatalogUnPriced.update_user_id = CREATE_USER_ID;
                newSupplyCatalogUnPriced.is_priced = false;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newSupplyCatalogUnPriced);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newSupplyCatalogUnPriced.catalog_id.ToString());

                #endregion

                #region UnPriced catalog_item_category

                // Create new record
                catalog_item_category newSupplyCatalogItemCategoryUnPriced1 = new catalog_item_category();
                // newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryUnPriced1.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_name = "Otis Spring 2011 Bulk 14 Item Supplies Unpriced";
                newSupplyCatalogItemCategoryUnPriced1.parent_catalog_item_category_id = 0;
                newSupplyCatalogItemCategoryUnPriced1.deleted = false;
                newSupplyCatalogItemCategoryUnPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryUnPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryUnPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryUnPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newSupplyCatalogItemCategoryUnPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id.ToString());

                #endregion

                #region UnPriced form_section

                // Create new record
                form_section newSupplyPAFormSectionUnPriced1 = new form_section();
                // newSupplyPAFormSectionUnPriced1.form_section_id;              // Autonumber generated on insert
                newSupplyPAFormSectionUnPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyPAFormSectionUnPriced1.form_id = newPAForm.form_id;
                newSupplyPAFormSectionUnPriced1.form_section_type_id = 2;        // 1 = standard product, 2 = supply product, 3 = optional product
                newSupplyPAFormSectionUnPriced1.form_section_number = 2;
                newSupplyPAFormSectionUnPriced1.form_section_title = "Otis Spring 2011 Bulk 14 Item Supplies UnPriced";
                newSupplyPAFormSectionUnPriced1.description = "";
                newSupplyPAFormSectionUnPriced1.deleted = false;
                newSupplyPAFormSectionUnPriced1.create_date = DateTime.Now;
                newSupplyPAFormSectionUnPriced1.create_user_id = CREATE_USER_ID;
                newSupplyPAFormSectionUnPriced1.update_date = DateTime.Now;
                newSupplyPAFormSectionUnPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newSupplyPAFormSectionUnPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newSupplyPAFormSectionUnPriced1.form_section_id.ToString());

                #endregion

                #region UnPriced catalog_item

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced2 = new catalog_item();
                // newSupplyCatalogItemUnPriced2.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced2.product_id = newSupplyProduct2.product_id;
                newSupplyCatalogItemUnPriced2.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced2.catalog_item_code = "CD2402";
                newSupplyCatalogItemUnPriced2.catalog_item_name = "Otis Bulk Brochure Un-priced";
                newSupplyCatalogItemUnPriced2.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced2.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced2.image_url = "";
                newSupplyCatalogItemUnPriced2.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced2.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced2.description = "";
                newSupplyCatalogItemUnPriced2.deleted = false;
                newSupplyCatalogItemUnPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced3 = new catalog_item();
                // newSupplyCatalogItemUnPriced3.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced3.product_id = newSupplyProduct3.product_id;
                newSupplyCatalogItemUnPriced3.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced3.catalog_item_code = "CD2062";
                newSupplyCatalogItemUnPriced3.catalog_item_name = "Otis Mass Display";
                newSupplyCatalogItemUnPriced3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced3.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced3.image_url = "";
                newSupplyCatalogItemUnPriced3.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced3.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced3.description = "";
                newSupplyCatalogItemUnPriced3.deleted = false;
                newSupplyCatalogItemUnPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced4 = new catalog_item();
                // newSupplyCatalogItemUnPriced4.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced4.product_id = newSupplyProduct4.product_id;
                newSupplyCatalogItemUnPriced4.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced4.catalog_item_code = "CD2098";
                newSupplyCatalogItemUnPriced4.catalog_item_name = "Generic Large Outer Envelope";
                newSupplyCatalogItemUnPriced4.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced4.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced4.image_url = "";
                newSupplyCatalogItemUnPriced4.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced4.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced4.description = "";
                newSupplyCatalogItemUnPriced4.deleted = false;
                newSupplyCatalogItemUnPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced5 = new catalog_item();
                // newSupplyCatalogItemUnPriced5.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced5.product_id = newSupplyProduct5.product_id;
                newSupplyCatalogItemUnPriced5.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced5.catalog_item_code = "CD2086";
                newSupplyCatalogItemUnPriced5.catalog_item_name = "Otis Launch Poster";
                newSupplyCatalogItemUnPriced5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced5.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced5.image_url = "";
                newSupplyCatalogItemUnPriced5.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced5.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced5.description = "";
                newSupplyCatalogItemUnPriced5.deleted = false;
                newSupplyCatalogItemUnPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced6 = new catalog_item();
                // newSupplyCatalogItemUnPriced6.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced6.product_id = newSupplyProduct6.product_id;
                newSupplyCatalogItemUnPriced6.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced6.catalog_item_code = "CDBULK2306";
                newSupplyCatalogItemUnPriced6.catalog_item_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemUnPriced6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced6.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced6.image_url = "";
                newSupplyCatalogItemUnPriced6.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced6.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced6.description = "";
                newSupplyCatalogItemUnPriced6.deleted = false;
                newSupplyCatalogItemUnPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced7 = new catalog_item();
                // newSupplyCatalogItemUnPriced7.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced7.product_id = newSupplyProduct7.product_id;
                newSupplyCatalogItemUnPriced7.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced7.catalog_item_code = "CD2093";
                newSupplyCatalogItemUnPriced7.catalog_item_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemUnPriced7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced7.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced7.image_url = "";
                newSupplyCatalogItemUnPriced7.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced7.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced7.description = "";
                newSupplyCatalogItemUnPriced7.deleted = false;
                newSupplyCatalogItemUnPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced8 = new catalog_item();
                // newSupplyCatalogItemUnPriced8.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced8.product_id = newSupplyProduct8.product_id;
                newSupplyCatalogItemUnPriced8.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced8.catalog_item_code = "CD2106";
                newSupplyCatalogItemUnPriced8.catalog_item_name = "Generic Medium Collection Envelope";
                newSupplyCatalogItemUnPriced8.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced8.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced8.image_url = "";
                newSupplyCatalogItemUnPriced8.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced8.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced8.description = "";
                newSupplyCatalogItemUnPriced8.deleted = false;
                newSupplyCatalogItemUnPriced8.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced8.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced8.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced9 = new catalog_item();
                // newSupplyCatalogItemUnPriced9.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced9.product_id = newSupplyProduct9.product_id;
                newSupplyCatalogItemUnPriced9.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced9.catalog_item_code = "CD2103";
                newSupplyCatalogItemUnPriced9.catalog_item_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemUnPriced9.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced9.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced9.image_url = "";
                newSupplyCatalogItemUnPriced9.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced9.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced9.description = "";
                newSupplyCatalogItemUnPriced9.deleted = false;
                newSupplyCatalogItemUnPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced2);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced3);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced4);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced5);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced6);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced7);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced8);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region UnPriced catalog_item_detail

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced2 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced2.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced2.catalog_item_id = newSupplyCatalogItemUnPriced2.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced2.catalog_item_detail_code = "CD2402";
                newSupplyCatalogItemDetailUnPriced2.catalog_item_detail_name = "Otis Bulk Brochure Un-priced";
                newSupplyCatalogItemDetailUnPriced2.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced2.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced2.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced2.term = 1;
                newSupplyCatalogItemDetailUnPriced2.description = "";
                newSupplyCatalogItemDetailUnPriced2.is_default = false;
                newSupplyCatalogItemDetailUnPriced2.deleted = false;
                newSupplyCatalogItemDetailUnPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced3 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced3.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced3.catalog_item_id = newSupplyCatalogItemUnPriced3.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced3.catalog_item_detail_code = "CD2062";
                newSupplyCatalogItemDetailUnPriced3.catalog_item_detail_name = "Otis Mass Display";
                newSupplyCatalogItemDetailUnPriced3.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced3.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced3.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced3.term = 1;
                newSupplyCatalogItemDetailUnPriced3.description = "";
                newSupplyCatalogItemDetailUnPriced3.is_default = false;
                newSupplyCatalogItemDetailUnPriced3.deleted = false;
                newSupplyCatalogItemDetailUnPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced4 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced4.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced4.catalog_item_id = newSupplyCatalogItemUnPriced4.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced4.catalog_item_detail_code = "CD2098";
                newSupplyCatalogItemDetailUnPriced4.catalog_item_detail_name = "Generic Large Outer Envelope";
                newSupplyCatalogItemDetailUnPriced4.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced4.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced4.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced4.term = 1;
                newSupplyCatalogItemDetailUnPriced4.description = "";
                newSupplyCatalogItemDetailUnPriced4.is_default = false;
                newSupplyCatalogItemDetailUnPriced4.deleted = false;
                newSupplyCatalogItemDetailUnPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced5 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced5.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced5.catalog_item_id = newSupplyCatalogItemUnPriced5.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced5.catalog_item_detail_code = "CD2086";
                newSupplyCatalogItemDetailUnPriced5.catalog_item_detail_name = "Otis Launch Poster";
                newSupplyCatalogItemDetailUnPriced5.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced5.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced5.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced5.term = 1;
                newSupplyCatalogItemDetailUnPriced5.description = "";
                newSupplyCatalogItemDetailUnPriced5.is_default = false;
                newSupplyCatalogItemDetailUnPriced5.deleted = false;
                newSupplyCatalogItemDetailUnPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced6 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced6.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced6.catalog_item_id = newSupplyCatalogItemUnPriced6.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced6.catalog_item_detail_code = "CDBULK2306";
                newSupplyCatalogItemDetailUnPriced6.catalog_item_detail_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemDetailUnPriced6.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced6.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced6.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced6.term = 1;
                newSupplyCatalogItemDetailUnPriced6.description = "";
                newSupplyCatalogItemDetailUnPriced6.is_default = false;
                newSupplyCatalogItemDetailUnPriced6.deleted = false;
                newSupplyCatalogItemDetailUnPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced7 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced7.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced7.catalog_item_id = newSupplyCatalogItemUnPriced7.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced7.catalog_item_detail_code = "CD2093";
                newSupplyCatalogItemDetailUnPriced7.catalog_item_detail_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemDetailUnPriced7.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced7.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced7.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced7.term = 1;
                newSupplyCatalogItemDetailUnPriced7.description = "";
                newSupplyCatalogItemDetailUnPriced7.is_default = false;
                newSupplyCatalogItemDetailUnPriced7.deleted = false;
                newSupplyCatalogItemDetailUnPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced8 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced8.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced8.catalog_item_id = newSupplyCatalogItemUnPriced8.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced8.catalog_item_detail_code = "CD2106";
                newSupplyCatalogItemDetailUnPriced8.catalog_item_detail_name = "Generic Medium Collection Envelope";
                newSupplyCatalogItemDetailUnPriced8.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced8.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced8.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced8.term = 1;
                newSupplyCatalogItemDetailUnPriced8.description = "";
                newSupplyCatalogItemDetailUnPriced8.is_default = false;
                newSupplyCatalogItemDetailUnPriced8.deleted = false;
                newSupplyCatalogItemDetailUnPriced8.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced8.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced8.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced9 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced9.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced9.catalog_item_id = newSupplyCatalogItemUnPriced9.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced9.catalog_item_detail_code = "CD2103";
                newSupplyCatalogItemDetailUnPriced9.catalog_item_detail_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemDetailUnPriced9.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced9.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced9.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced9.term = 1;
                newSupplyCatalogItemDetailUnPriced9.description = "";
                newSupplyCatalogItemDetailUnPriced9.is_default = false;
                newSupplyCatalogItemDetailUnPriced9.deleted = false;
                newSupplyCatalogItemDetailUnPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced2);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced3);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced4);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced5);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced6);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced7);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced8);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region UnPriced catalog_item_category_catalog_item

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced2 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced2.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.catalog_item_id = newSupplyCatalogItemUnPriced2.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.display_order = 1;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced3 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.catalog_item_id = newSupplyCatalogItemUnPriced3.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.display_order = 5;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced4 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced4.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.catalog_item_id = newSupplyCatalogItemUnPriced4.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.display_order = 6;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced5 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced5.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.catalog_item_id = newSupplyCatalogItemUnPriced5.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.display_order = 2;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced6 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced6.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.catalog_item_id = newSupplyCatalogItemUnPriced6.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.display_order = 3;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced7 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced7.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.catalog_item_id = newSupplyCatalogItemUnPriced7.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.display_order = 4;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced8 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced8.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.catalog_item_id = newSupplyCatalogItemUnPriced8.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.display_order = 7;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced9 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced9.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.catalog_item_id = newSupplyCatalogItemUnPriced9.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.display_order = 8;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.update_user_id = CREATE_USER_ID;

                #endregion


                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced2);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced3);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced4);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced5);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced6);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced7);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced8);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #endregion

                #endregion

                #region Order form requires PA form

                form_requires_form formRequiresForm = new form_requires_form();

                formRequiresForm.form_id = newForm.form_id;
                formRequiresForm.required_form_id = newPAForm.form_id; 
                formRequiresForm.deleted = false;
                formRequiresForm.create_date = DateTime.Now;
                formRequiresForm.create_user_id = CREATE_USER_ID;
                formRequiresForm.update_date = DateTime.Now;
                formRequiresForm.update_user_id = CREATE_USER_ID;

                context.form_requires_forms.InsertOnSubmit(formRequiresForm);
                context.SubmitChanges();

                otisBulk14OrderFormId = newForm.form_id; // Otis/Mag combo PA uses same Order form.

                sb.AppendLine("New form required form id = " + formRequiresForm.form_requires_form_id.ToString());

                #endregion

                sb.AppendLine("Otis Bulk 14 Spring 2011 - Success!");
            }
            catch (Exception ex)
            {
                sb.AppendLine("transaction rollback due to error");
                sb.AppendLine("Message: " + ex.Message);
                sb.AppendLine("Source: " + ex.Source);
                sb.AppendLine("");
                sb.AppendLine(ex.StackTrace);
            }
            finally
            {
                if (context.Connection != null)
                {
                    context.Connection.Close();
                }
            }

            sb.AppendLine("end");

            return sb.ToString();
        }
        public string Spring2011_OtisMagazineCombo()
        {
            #region Notes

            // To enforce the PA before an order can be made, we need to add code to:
            // QSPForm.WebApp.OrderStep_Selection.BindForm which is located in:
            // OrderExpress/UserControls/OrderStep_Selection.ascx

            // Supply catalogs in PA need one catalog for priced and one catalog for 
            // unpriced items

            #endregion

            #region Start

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("start");

            // Open db context
            QSPFulfillmentDataContext context = new QSPFulfillmentDataContext(CONNECTION_STRING);
            sb.AppendLine("db context open");

            // Open the connection
            context.Connection.Open();

            #endregion

            try
            {
                // Order form not required, It uses the Otis Bulk 14 item

                #region PA form

                #region form_group

                // Create new form group object
                form_group newFormGroup = new form_group();

                // Fill in new form group data
                // newFormGroup.form_group_id;              // Autonumber generated on insert
                newFormGroup.form_group_name = "Otis Spring 2011 Bulk 14 Item / Magazine Combo";
                newFormGroup.deleted = false;
                newFormGroup.create_date = DateTime.Now;
                newFormGroup.create_user_id = CREATE_USER_ID;
                newFormGroup.update_date = DateTime.Now;
                newFormGroup.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.form_groups.InsertOnSubmit(newFormGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form group id = " + newFormGroup.form_group_id.ToString());
                // MessageBox.Show("New form group id = " + newFormGroup.form_group_id.ToString());

                #endregion

                #region form

                // Create new form object
                form newPAForm = new form();

                // Fill in new form data
                // newPAForm.form_id;                             // Autonumber generated on insert
                newPAForm.form_group_id = newFormGroup.form_group_id;
                newPAForm.entity_type_id = 12;                     // Type 12 = program agreement
                newPAForm.form_code = "MC56";
                newPAForm.form_name = "Otis Spring 2011 Bulk 14 Item / Magazine Combo PA";
                newPAForm.description = "";
                newPAForm.order_terms_text = "";
                newPAForm.start_date = new DateTime(2010, 12, 8);
                newPAForm.end_date = new DateTime(2011, 6, 30);
                newPAForm.closing_time = new DateTime(2011, 6, 30);
                newPAForm.image_url = "images/CatalogItem/chocochunk.gif";
                newPAForm.is_base_form = false;
                newPAForm.parent_form_id = 48;                     // Base form for orders in prod and dev
                newPAForm.is_product_price_updatable = false;
                newPAForm.is_quantity_adjustment_allowed = true;
                newPAForm.tax_postal_address_type_id = 2;
                newPAForm.enabled = true;
                newPAForm.deleted = false;
                newPAForm.version = 1;
                newPAForm.program_id = 3;                         // Otis = 3, PV = 2, UP = 4, MP = 5
                newPAForm.program_type_id = 7;
                newPAForm.program_basics_text = "";
                newPAForm.create_date = DateTime.Now;
                newPAForm.create_user_id = CREATE_USER_ID;
                newPAForm.update_date = DateTime.Now;
                newPAForm.update_user_id = CREATE_USER_ID;
                //newPAForm.warehouse_type_id;
                newPAForm.is_bulk = true;
                newPAForm.report_name = "OrderForm";
                //newPAForm.is_warehouse_selectable = true;
                //newPAForm.default_warehouse_id;
                newPAForm.season_id = 8;

                // Add the new form to the db context
                context.forms.InsertOnSubmit(newPAForm);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form id = " + newPAForm.form_id.ToString());

                #endregion

                #region form_permission

                form_permission newPAFormPermission1 = new form_permission();
                //newPAFormPermission1.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission1.form_id = newPAForm.form_id;
                newPAFormPermission1.role_id = 0;                     // User
                newPAFormPermission1.allow_read = false;
                newPAFormPermission1.allow_write = false;
                newPAFormPermission1.create_date = DateTime.Now;
                newPAFormPermission1.create_user_id = CREATE_USER_ID;
                newPAFormPermission1.update_date = DateTime.Now;
                newPAFormPermission1.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission2 = new form_permission();
                //newPAFormPermission2.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission2.form_id = newPAForm.form_id;
                newPAFormPermission2.role_id = 1;                     // Field Sale Manager (FM)
                newPAFormPermission2.allow_read = true;
                newPAFormPermission2.allow_write = true;
                newPAFormPermission2.create_date = DateTime.Now;
                newPAFormPermission2.create_user_id = CREATE_USER_ID;
                newPAFormPermission2.update_date = DateTime.Now;
                newPAFormPermission2.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission3 = new form_permission();
                //newPAFormPermission3.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission3.form_id = newPAForm.form_id;
                newPAFormPermission3.role_id = 2;                     // Field Support
                newPAFormPermission3.allow_read = true;
                newPAFormPermission3.allow_write = true;
                newPAFormPermission3.create_date = DateTime.Now;
                newPAFormPermission3.create_user_id = CREATE_USER_ID;
                newPAFormPermission3.update_date = DateTime.Now;
                newPAFormPermission3.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission4 = new form_permission();
                //newPAFormPermission4.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission4.form_id = newPAForm.form_id;
                newPAFormPermission4.role_id = 3;                     // Accounting Manager
                newPAFormPermission4.allow_read = true;
                newPAFormPermission4.allow_write = true;
                newPAFormPermission4.create_date = DateTime.Now;
                newPAFormPermission4.create_user_id = CREATE_USER_ID;
                newPAFormPermission4.update_date = DateTime.Now;
                newPAFormPermission4.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission5 = new form_permission();
                //newPAFormPermission5.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission5.form_id = newPAForm.form_id;
                newPAFormPermission5.role_id = 4;                     // Admin
                newPAFormPermission5.allow_read = true;
                newPAFormPermission5.allow_write = true;
                newPAFormPermission5.create_date = DateTime.Now;
                newPAFormPermission5.create_user_id = CREATE_USER_ID;
                newPAFormPermission5.update_date = DateTime.Now;
                newPAFormPermission5.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission6 = new form_permission();
                //newPAFormPermission6.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission6.form_id = newPAForm.form_id;
                newPAFormPermission6.role_id = 5;                     // Super User
                newPAFormPermission6.allow_read = true;
                newPAFormPermission6.allow_write = true;
                newPAFormPermission6.create_date = DateTime.Now;
                newPAFormPermission6.create_user_id = CREATE_USER_ID;
                newPAFormPermission6.update_date = DateTime.Now;
                newPAFormPermission6.update_user_id = CREATE_USER_ID;

                // Add the new form permissions to the db context
                context.form_permissions.InsertOnSubmit(newPAFormPermission1);
                context.form_permissions.InsertOnSubmit(newPAFormPermission2);
                context.form_permissions.InsertOnSubmit(newPAFormPermission3);
                context.form_permissions.InsertOnSubmit(newPAFormPermission4);
                context.form_permissions.InsertOnSubmit(newPAFormPermission5);
                context.form_permissions.InsertOnSubmit(newPAFormPermission6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form permissions ok");

                #endregion

                #region form_profit_rate

                form_profit_rate newPAFormProfitRate1 = new form_profit_rate();
                //newPAFormProfitRate1.form_profit_rate_id;                // Autonumber generated on insert
                newPAFormProfitRate1.form_id = newPAForm.form_id;
                newPAFormProfitRate1.profit_rate_id = 1;         // 1 = 40%, 2 = 45%, 3 = 50%
                newPAFormProfitRate1.deleted = false;
                newPAFormProfitRate1.create_date = DateTime.Now;
                newPAFormProfitRate1.create_user_id = CREATE_USER_ID;
                newPAFormProfitRate1.update_date = DateTime.Now;
                newPAFormProfitRate1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_profit_rates.InsertOnSubmit(newPAFormProfitRate1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form profit rate ok");

                #endregion

                #region catalog_group

                // Create new record
                catalog_group newCatalogGroup = new catalog_group();
                // newCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newCatalogGroup.catalog_group_code = "MC56";
                newCatalogGroup.catalog_group_name = "Otis Spring 2011 Bulk 14/Mag Combo order";
                newCatalogGroup.description = "";
                newCatalogGroup.deleted = false;
                newCatalogGroup.create_date = DateTime.Now;
                newCatalogGroup.create_user_id = CREATE_USER_ID;
                newCatalogGroup.update_date = DateTime.Now;
                newCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region catalog

                // Create new record
                catalog newCatalog = new catalog();
                // newCatalog.newCatalog_id;          // Autonumber generated on insert
                newCatalog.catalog_group_id = newCatalogGroup.catalog_group_id;
                newCatalog.catalog_code = "MC56";
                newCatalog.catalog_name = "Otis Spring 2011 Bulk 14 Item/Magazine Combo order";
                newCatalog.culture = "en-US";
                newCatalog.description = "";
                newCatalog.start_date = new DateTime(2010, 12, 8);
                newCatalog.end_date = new DateTime(2011, 6, 30);
                newCatalog.deleted = false;
                newCatalog.create_date = DateTime.Now;
                newCatalog.create_user_id = CREATE_USER_ID;
                newCatalog.update_date = DateTime.Now;
                newCatalog.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newCatalog);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newCatalog.catalog_id.ToString());

                #endregion

                #region catalog_item_category

                // Create new record
                catalog_item_category newCatalogItemCategory1 = new catalog_item_category();
                // newCatalogItemCategory1.catalog_item_category_id;         // Autonumber generated on insert
                newCatalogItemCategory1.catalog_id = newCatalog.catalog_id;
                newCatalogItemCategory1.catalog_item_category_name = "Otis Spring 2011 Bulk 14/Mag Combo";
                newCatalogItemCategory1.parent_catalog_item_category_id = 0;
                newCatalogItemCategory1.deleted = false;
                newCatalogItemCategory1.create_date = DateTime.Now;
                newCatalogItemCategory1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategory1.update_date = DateTime.Now;
                newCatalogItemCategory1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newCatalogItemCategory1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newCatalogItemCategory1.catalog_item_category_id.ToString());

                #endregion
                
                #region form_section

                // Create new record
                form_section newPAFormSection1 = new form_section();
                // newPAFormSection1.form_section_id;              // Autonumber generated on insert
                newPAFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newPAFormSection1.form_id = newPAForm.form_id;
                newPAFormSection1.form_section_type_id = 1;        // 1 = standard product, 2 = supply product, 3 = optional product
                newPAFormSection1.form_section_number = 1;
                newPAFormSection1.form_section_title = "Otis Spring 2011 Bulk 14 / Magazine Combo";
                newPAFormSection1.description = "";
                newPAFormSection1.deleted = false;
                newPAFormSection1.create_date = DateTime.Now;
                newPAFormSection1.create_user_id = CREATE_USER_ID;
                newPAFormSection1.update_date = DateTime.Now;
                newPAFormSection1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newPAFormSection1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newPAFormSection1.form_section_id.ToString());

                #endregion

                #region Supplies in PA

                #region catalog_group

                // Create new record
                catalog_group newSupplyCatalogGroup = new catalog_group();
                // newSupplyCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newSupplyCatalogGroup.catalog_group_code = "MC56";
                newSupplyCatalogGroup.catalog_group_name = "Otis Spring 2011 CD/Mag Combo Supplies";
                newSupplyCatalogGroup.description = "";
                newSupplyCatalogGroup.deleted = false;
                newSupplyCatalogGroup.create_date = DateTime.Now;
                newSupplyCatalogGroup.create_user_id = CREATE_USER_ID;
                newSupplyCatalogGroup.update_date = DateTime.Now;
                newSupplyCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newSupplyCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newSupplyCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region product

                #region Create new record

                product newSupplyProduct1 = new product();
                // newSupplyProduct1.product_id;                      // Autonumber generated on insert
                newSupplyProduct1.product_code = "CDK2410";
                newSupplyProduct1.product_name = "Otis Priced - Magazine Tax Kit";
                newSupplyProduct1.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct1.nb_units = 1;                       // boxes per case
                newSupplyProduct1.nb_day_lead_time = 0;
                newSupplyProduct1.product_status_id = 101;            // 101 = Active
                newSupplyProduct1.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct1.business_division_id = 1;           // 1 = US
                newSupplyProduct1.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct1.coupon_id = null;
                newSupplyProduct1.description = "";
                newSupplyProduct1.image_url = "";
                newSupplyProduct1.is_free_sample = false;
                newSupplyProduct1.oracle_code = "";
                newSupplyProduct1.IVCOUP = "";
                newSupplyProduct1.IVITEM = "CDK2410";
                newSupplyProduct1.unit_cost = null;
                newSupplyProduct1.vendor_id = 27;                     // 27 = prod
                newSupplyProduct1.vendor_item_code = "CDK2410";
                newSupplyProduct1.deleted = false;
                newSupplyProduct1.create_date = DateTime.Now;
                newSupplyProduct1.create_user_id = CREATE_USER_ID;
                newSupplyProduct1.update_date = DateTime.Now;
                newSupplyProduct1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct2 = new product();
                // newSupplyProduct2.product_id;                      // Autonumber generated on insert
                newSupplyProduct2.product_code = "CDK2411";
                newSupplyProduct2.product_name = "Otis Priced - Magazine No Tax Kit";
                newSupplyProduct2.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct2.nb_units = 1;                       // boxes per case
                newSupplyProduct2.nb_day_lead_time = 0;
                newSupplyProduct2.product_status_id = 101;            // 101 = Active
                newSupplyProduct2.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct2.business_division_id = 1;           // 1 = US
                newSupplyProduct2.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct2.coupon_id = null;
                newSupplyProduct2.description = "";
                newSupplyProduct2.image_url = "";
                newSupplyProduct2.is_free_sample = false;
                newSupplyProduct2.oracle_code = "";
                newSupplyProduct2.IVCOUP = "";
                newSupplyProduct2.IVITEM = "CDK2411";
                newSupplyProduct2.unit_cost = null;
                newSupplyProduct2.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct2.vendor_item_code = "CDK2411";
                newSupplyProduct2.deleted = false;
                newSupplyProduct2.create_date = DateTime.Now;
                newSupplyProduct2.create_user_id = CREATE_USER_ID;
                newSupplyProduct2.update_date = DateTime.Now;
                newSupplyProduct2.update_user_id = CREATE_USER_ID;

                #endregion

                //CD2062
                #region Create new record

                product newSupplyProduct3 = new product();
                // newSupplyProduct3.product_id;                      // Autonumber generated on insert
                newSupplyProduct3.product_code = "CD2062";
                newSupplyProduct3.product_name = "Otis Mass Display";
                newSupplyProduct3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct3.nb_units = 1;                       // boxes per case
                newSupplyProduct3.nb_day_lead_time = 0;
                newSupplyProduct3.product_status_id = 101;            // 101 = Active
                newSupplyProduct3.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct3.business_division_id = 1;           // 1 = US
                newSupplyProduct3.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct3.coupon_id = null;
                newSupplyProduct3.description = "";
                newSupplyProduct3.image_url = "";
                newSupplyProduct3.is_free_sample = false;
                newSupplyProduct3.oracle_code = "";
                newSupplyProduct3.IVCOUP = "";
                newSupplyProduct3.IVITEM = "CD2062";
                newSupplyProduct3.unit_cost = null;
                newSupplyProduct3.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct3.vendor_item_code = "CD2062";
                newSupplyProduct3.deleted = false;
                newSupplyProduct3.create_date = DateTime.Now;
                newSupplyProduct3.create_user_id = CREATE_USER_ID;
                newSupplyProduct3.update_date = DateTime.Now;
                newSupplyProduct3.update_user_id = CREATE_USER_ID;

                #endregion

                //CD2086
                #region Create new record

                product newSupplyProduct5 = new product();
                // newSupplyProduct5.product_id;                      // Autonumber generated on insert
                newSupplyProduct5.product_code = "CD2086";
                newSupplyProduct5.product_name = "Otis Launch Poster";
                newSupplyProduct5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct5.nb_units = 1;                       // boxes per case
                newSupplyProduct5.nb_day_lead_time = 0;
                newSupplyProduct5.product_status_id = 101;            // 101 = Active
                newSupplyProduct5.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct5.business_division_id = 1;           // 1 = US
                newSupplyProduct5.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct5.coupon_id = null;
                newSupplyProduct5.description = "";
                newSupplyProduct5.image_url = "";
                newSupplyProduct5.is_free_sample = false;
                newSupplyProduct5.oracle_code = "";
                newSupplyProduct5.IVCOUP = "";
                newSupplyProduct5.IVITEM = "CD2086";
                newSupplyProduct5.unit_cost = null;
                newSupplyProduct5.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct5.vendor_item_code = "CD2086";
                newSupplyProduct5.deleted = false;
                newSupplyProduct5.create_date = DateTime.Now;
                newSupplyProduct5.create_user_id = CREATE_USER_ID;
                newSupplyProduct5.update_date = DateTime.Now;
                newSupplyProduct5.update_user_id = CREATE_USER_ID;

                #endregion

                //CDBULK2306
                #region Create new record

                product newSupplyProduct6 = new product();
                // newSupplyProduct6.product_id;                      // Autonumber generated on insert
                newSupplyProduct6.product_code = "CDBULK2306";
                newSupplyProduct6.product_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyProduct6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct6.nb_units = 1;                       // boxes per case
                newSupplyProduct6.nb_day_lead_time = 0;
                newSupplyProduct6.product_status_id = 101;            // 101 = Active
                newSupplyProduct6.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct6.business_division_id = 1;           // 1 = US
                newSupplyProduct6.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct6.coupon_id = null;
                newSupplyProduct6.description = "";
                newSupplyProduct6.image_url = "";
                newSupplyProduct6.is_free_sample = false;
                newSupplyProduct6.oracle_code = "";
                newSupplyProduct6.IVCOUP = "";
                newSupplyProduct6.IVITEM = "CDBULK2306";
                newSupplyProduct6.unit_cost = null;
                newSupplyProduct6.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct6.vendor_item_code = "CDBULK2306";
                newSupplyProduct6.deleted = false;
                newSupplyProduct6.create_date = DateTime.Now;
                newSupplyProduct6.create_user_id = CREATE_USER_ID;
                newSupplyProduct6.update_date = DateTime.Now;
                newSupplyProduct6.update_user_id = CREATE_USER_ID;

                #endregion

                //CD2093
                #region Create new record

                product newSupplyProduct7 = new product();
                // newSupplyProduct7.product_id;                      // Autonumber generated on insert
                newSupplyProduct7.product_code = "CD2093";
                newSupplyProduct7.product_name = "QSP Plastic Food Bags";
                newSupplyProduct7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct7.nb_units = 1;                       // boxes per case
                newSupplyProduct7.nb_day_lead_time = 0;
                newSupplyProduct7.product_status_id = 101;            // 101 = Active
                newSupplyProduct7.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct7.business_division_id = 1;           // 1 = US
                newSupplyProduct7.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct7.coupon_id = null;
                newSupplyProduct7.description = "";
                newSupplyProduct7.image_url = "";
                newSupplyProduct7.is_free_sample = false;
                newSupplyProduct7.oracle_code = "";
                newSupplyProduct7.IVCOUP = "";
                newSupplyProduct7.IVITEM = "CD2093";
                newSupplyProduct7.unit_cost = null;
                newSupplyProduct7.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct7.vendor_item_code = "CD2093";
                newSupplyProduct7.deleted = false;
                newSupplyProduct7.create_date = DateTime.Now;
                newSupplyProduct7.create_user_id = CREATE_USER_ID;
                newSupplyProduct7.update_date = DateTime.Now;
                newSupplyProduct7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct4 = new product();
                // newSupplyProduct4.product_id;                      // Autonumber generated on insert
                newSupplyProduct4.product_code = "CDK2412";
                newSupplyProduct4.product_name = "Otis Un-Priced - Magazine Tax Kit";
                newSupplyProduct4.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct4.nb_units = 1;                       // boxes per case
                newSupplyProduct4.nb_day_lead_time = 0;
                newSupplyProduct4.product_status_id = 101;            // 101 = Active
                newSupplyProduct4.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct4.business_division_id = 1;           // 1 = US
                newSupplyProduct4.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct4.coupon_id = null;
                newSupplyProduct4.description = "";
                newSupplyProduct4.image_url = "";
                newSupplyProduct4.is_free_sample = false;
                newSupplyProduct4.oracle_code = "";
                newSupplyProduct4.IVCOUP = "";
                newSupplyProduct4.IVITEM = "CDK2412";
                newSupplyProduct4.unit_cost = null;
                newSupplyProduct4.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct4.vendor_item_code = "CDK2412";
                newSupplyProduct4.deleted = false;
                newSupplyProduct4.create_date = DateTime.Now;
                newSupplyProduct4.create_user_id = CREATE_USER_ID;
                newSupplyProduct4.update_date = DateTime.Now;
                newSupplyProduct4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct8 = new product();
                // newSupplyProduct8.product_id;                      // Autonumber generated on insert
                newSupplyProduct8.product_code = "CDK2413";
                newSupplyProduct8.product_name = "Otis Un-Priced - Magazine No Tax Kit";
                newSupplyProduct8.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct8.nb_units = 1;                       // boxes per case
                newSupplyProduct8.nb_day_lead_time = 0;
                newSupplyProduct8.product_status_id = 101;            // 101 = Active
                newSupplyProduct8.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct8.business_division_id = 1;           // 1 = US
                newSupplyProduct8.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct8.coupon_id = null;
                newSupplyProduct8.description = "";
                newSupplyProduct8.image_url = "";
                newSupplyProduct8.is_free_sample = false;
                newSupplyProduct8.oracle_code = "";
                newSupplyProduct8.IVCOUP = "";
                newSupplyProduct8.IVITEM = "CDK2413";
                newSupplyProduct8.unit_cost = null;
                newSupplyProduct8.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct8.vendor_item_code = "CDK2413";
                newSupplyProduct8.deleted = false;
                newSupplyProduct8.create_date = DateTime.Now;
                newSupplyProduct8.create_user_id = CREATE_USER_ID;
                newSupplyProduct8.update_date = DateTime.Now;
                newSupplyProduct8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct9 = new product();
                // newSupplyProduct9.product_id;                      // Autonumber generated on insert
                newSupplyProduct9.product_code = "CD2103";
                newSupplyProduct9.product_name = "Just Right Cookie (Baking Instructions)";
                newSupplyProduct9.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct9.nb_units = 1;                       // boxes per case
                newSupplyProduct9.nb_day_lead_time = 0;
                newSupplyProduct9.product_status_id = 101;            // 101 = Active
                newSupplyProduct9.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct9.business_division_id = 1;           // 1 = US
                newSupplyProduct9.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct9.coupon_id = null;
                newSupplyProduct9.description = "";
                newSupplyProduct9.image_url = "";
                newSupplyProduct9.is_free_sample = false;
                newSupplyProduct9.oracle_code = "";
                newSupplyProduct9.IVCOUP = "";
                newSupplyProduct9.IVITEM = "CD2103";
                newSupplyProduct9.unit_cost = null;
                newSupplyProduct9.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct9.vendor_item_code = "CD2103";
                newSupplyProduct9.deleted = false;
                newSupplyProduct9.create_date = DateTime.Now;
                newSupplyProduct9.create_user_id = CREATE_USER_ID;
                newSupplyProduct9.update_date = DateTime.Now;
                newSupplyProduct9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.products.InsertOnSubmit(newSupplyProduct1);
                context.products.InsertOnSubmit(newSupplyProduct2);
                context.products.InsertOnSubmit(newSupplyProduct3);
                context.products.InsertOnSubmit(newSupplyProduct4);
                context.products.InsertOnSubmit(newSupplyProduct5);
                context.products.InsertOnSubmit(newSupplyProduct6);
                context.products.InsertOnSubmit(newSupplyProduct7);
                context.products.InsertOnSubmit(newSupplyProduct8);
                context.products.InsertOnSubmit(newSupplyProduct9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("products ok");

                #endregion

                #region Priced catalog

                // Create new record
                catalog newSupplyCatalogPriced = new catalog();
                // newSupplyCatalogPriced.newCatalog_id;          // Autonumber generated on insert
                newSupplyCatalogPriced.catalog_group_id = newSupplyCatalogGroup.catalog_group_id;
                newSupplyCatalogPriced.catalog_code = "MC56";
                newSupplyCatalogPriced.catalog_name = "Otis Spring 2011 CD/Mag Combo Supplies Priced";
                newSupplyCatalogPriced.culture = "en-US";
                newSupplyCatalogPriced.description = "";
                newSupplyCatalogPriced.start_date = new DateTime(2010, 12, 8);
                newSupplyCatalogPriced.end_date = new DateTime(2011, 6, 30);
                newSupplyCatalogPriced.deleted = false;
                newSupplyCatalogPriced.create_date = DateTime.Now;
                newSupplyCatalogPriced.create_user_id = CREATE_USER_ID;
                newSupplyCatalogPriced.update_date = DateTime.Now;
                newSupplyCatalogPriced.update_user_id = CREATE_USER_ID;
                newSupplyCatalogPriced.is_priced = true;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newSupplyCatalogPriced);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newSupplyCatalogPriced.catalog_id.ToString());

                #endregion

                #region Priced catalog_item_category

                // Create new record
                catalog_item_category newSupplyCatalogItemCategoryPriced1 = new catalog_item_category();
                // newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryPriced1.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemCategoryPriced1.catalog_item_category_name = "Otis Spring 2011 CD/Mag Combo Supplies Priced";
                newSupplyCatalogItemCategoryPriced1.parent_catalog_item_category_id = 0;
                newSupplyCatalogItemCategoryPriced1.deleted = false;
                newSupplyCatalogItemCategoryPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newSupplyCatalogItemCategoryPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newSupplyCatalogItemCategoryPriced1.catalog_item_category_id.ToString());

                #endregion

                #region Priced form_section

                // Create new record
                form_section newSupplyPAFormSectionPriced1 = new form_section();
                // newSupplyPAFormSectionPriced1.form_section_id;              // Autonumber generated on insert
                newSupplyPAFormSectionPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyPAFormSectionPriced1.form_id = newPAForm.form_id;
                newSupplyPAFormSectionPriced1.form_section_type_id = 2;        // 1 = standard product, 2 = supply product, 3 = optional product
                newSupplyPAFormSectionPriced1.form_section_number = 2;
                newSupplyPAFormSectionPriced1.form_section_title = "Otis Spring 2011 CD/Mag Combo Supplies Priced";
                newSupplyPAFormSectionPriced1.description = "";
                newSupplyPAFormSectionPriced1.deleted = false;
                newSupplyPAFormSectionPriced1.create_date = DateTime.Now;
                newSupplyPAFormSectionPriced1.create_user_id = CREATE_USER_ID;
                newSupplyPAFormSectionPriced1.update_date = DateTime.Now;
                newSupplyPAFormSectionPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newSupplyPAFormSectionPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newSupplyPAFormSectionPriced1.form_section_id.ToString());

                #endregion

                #region Priced catalog_item

                #region Create new record

                catalog_item newSupplyCatalogItemPriced1 = new catalog_item();
                // newSupplyCatalogItemPriced1.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced1.product_id = newSupplyProduct1.product_id;
                newSupplyCatalogItemPriced1.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced1.catalog_item_code = "CDK2410";
                newSupplyCatalogItemPriced1.catalog_item_name = "Otis Priced - Magazine Tax Kit";
                newSupplyCatalogItemPriced1.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced1.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced1.image_url = "";
                newSupplyCatalogItemPriced1.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced1.catalog_item_export_name = "";
                newSupplyCatalogItemPriced1.description = "";
                newSupplyCatalogItemPriced1.deleted = false;
                newSupplyCatalogItemPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced2 = new catalog_item();
                // newSupplyCatalogItemPriced2.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced2.product_id = newSupplyProduct2.product_id;
                newSupplyCatalogItemPriced2.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced2.catalog_item_code = "CDK2411";
                newSupplyCatalogItemPriced2.catalog_item_name = "Otis Priced - Magazine No Tax Kit";
                newSupplyCatalogItemPriced2.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced2.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced2.image_url = "";
                newSupplyCatalogItemPriced2.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced2.catalog_item_export_name = "";
                newSupplyCatalogItemPriced2.description = "";
                newSupplyCatalogItemPriced2.deleted = false;
                newSupplyCatalogItemPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced3 = new catalog_item();
                // newSupplyCatalogItemPriced3.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced3.product_id = newSupplyProduct3.product_id;
                newSupplyCatalogItemPriced3.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced3.catalog_item_code = "CD2062";
                newSupplyCatalogItemPriced3.catalog_item_name = "Otis Mass Display";
                newSupplyCatalogItemPriced3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced3.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced3.image_url = "";
                newSupplyCatalogItemPriced3.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced3.catalog_item_export_name = "";
                newSupplyCatalogItemPriced3.description = "";
                newSupplyCatalogItemPriced3.deleted = false;
                newSupplyCatalogItemPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced5 = new catalog_item();
                // newSupplyCatalogItemPriced5.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced5.product_id = newSupplyProduct5.product_id;
                newSupplyCatalogItemPriced5.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced5.catalog_item_code = "CD2086";
                newSupplyCatalogItemPriced5.catalog_item_name = "Otis Launch Poster";
                newSupplyCatalogItemPriced5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced5.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced5.image_url = "";
                newSupplyCatalogItemPriced5.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced5.catalog_item_export_name = "";
                newSupplyCatalogItemPriced5.description = "";
                newSupplyCatalogItemPriced5.deleted = false;
                newSupplyCatalogItemPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced6 = new catalog_item();
                // newSupplyCatalogItemPriced6.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced6.product_id = newSupplyProduct6.product_id;
                newSupplyCatalogItemPriced6.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced6.catalog_item_code = "CDBULK2306";
                newSupplyCatalogItemPriced6.catalog_item_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemPriced6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced6.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced6.image_url = "";
                newSupplyCatalogItemPriced6.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced6.catalog_item_export_name = "";
                newSupplyCatalogItemPriced6.description = "";
                newSupplyCatalogItemPriced6.deleted = false;
                newSupplyCatalogItemPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced7 = new catalog_item();
                // newSupplyCatalogItemPriced7.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced7.product_id = newSupplyProduct7.product_id;
                newSupplyCatalogItemPriced7.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced7.catalog_item_code = "CD2093";
                newSupplyCatalogItemPriced7.catalog_item_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemPriced7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced7.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced7.image_url = "";
                newSupplyCatalogItemPriced7.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced7.catalog_item_export_name = "";
                newSupplyCatalogItemPriced7.description = "";
                newSupplyCatalogItemPriced7.deleted = false;
                newSupplyCatalogItemPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced9 = new catalog_item();
                // newSupplyCatalogItemPriced9.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced9.product_id = newSupplyProduct9.product_id;
                newSupplyCatalogItemPriced9.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced9.catalog_item_code = "CD2103";
                newSupplyCatalogItemPriced9.catalog_item_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemPriced9.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced9.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced9.image_url = "";
                newSupplyCatalogItemPriced9.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced9.catalog_item_export_name = "";
                newSupplyCatalogItemPriced9.description = "";
                newSupplyCatalogItemPriced9.deleted = false;
                newSupplyCatalogItemPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced1);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced2); 
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced3);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced5);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced6);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced7);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region Priced catalog_item_detail

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced1 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced1.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced1.catalog_item_id = newSupplyCatalogItemPriced1.catalog_item_id;
                newSupplyCatalogItemDetailPriced1.catalog_item_detail_code = "CDK2410";
                newSupplyCatalogItemDetailPriced1.catalog_item_detail_name = "Otis Priced - Magazine Tax Kit";
                newSupplyCatalogItemDetailPriced1.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced1.nb_units = 1;
                newSupplyCatalogItemDetailPriced1.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced1.term = 1;
                newSupplyCatalogItemDetailPriced1.description = "";
                newSupplyCatalogItemDetailPriced1.is_default = false;
                newSupplyCatalogItemDetailPriced1.deleted = false;
                newSupplyCatalogItemDetailPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced2 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced2.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced2.catalog_item_id = newSupplyCatalogItemPriced2.catalog_item_id;
                newSupplyCatalogItemDetailPriced2.catalog_item_detail_code = "CDK2411";
                newSupplyCatalogItemDetailPriced2.catalog_item_detail_name = "Otis Priced - Magazine No Tax Kit";
                newSupplyCatalogItemDetailPriced2.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced2.nb_units = 1;
                newSupplyCatalogItemDetailPriced2.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced2.term = 1;
                newSupplyCatalogItemDetailPriced2.description = "";
                newSupplyCatalogItemDetailPriced2.is_default = false;
                newSupplyCatalogItemDetailPriced2.deleted = false;
                newSupplyCatalogItemDetailPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced3 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced3.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced3.catalog_item_id = newSupplyCatalogItemPriced3.catalog_item_id;
                newSupplyCatalogItemDetailPriced3.catalog_item_detail_code = "CD2062";
                newSupplyCatalogItemDetailPriced3.catalog_item_detail_name = "Otis Mass Display";
                newSupplyCatalogItemDetailPriced3.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced3.nb_units = 1;
                newSupplyCatalogItemDetailPriced3.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced3.term = 1;
                newSupplyCatalogItemDetailPriced3.description = "";
                newSupplyCatalogItemDetailPriced3.is_default = false;
                newSupplyCatalogItemDetailPriced3.deleted = false;
                newSupplyCatalogItemDetailPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced5 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced5.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced5.catalog_item_id = newSupplyCatalogItemPriced5.catalog_item_id;
                newSupplyCatalogItemDetailPriced5.catalog_item_detail_code = "CD2086";
                newSupplyCatalogItemDetailPriced5.catalog_item_detail_name = "Otis Launch Poster";
                newSupplyCatalogItemDetailPriced5.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced5.nb_units = 1;
                newSupplyCatalogItemDetailPriced5.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced5.term = 1;
                newSupplyCatalogItemDetailPriced5.description = "";
                newSupplyCatalogItemDetailPriced5.is_default = false;
                newSupplyCatalogItemDetailPriced5.deleted = false;
                newSupplyCatalogItemDetailPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced6 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced6.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced6.catalog_item_id = newSupplyCatalogItemPriced6.catalog_item_id;
                newSupplyCatalogItemDetailPriced6.catalog_item_detail_code = "CDBULK2306";
                newSupplyCatalogItemDetailPriced6.catalog_item_detail_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemDetailPriced6.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced6.nb_units = 1;
                newSupplyCatalogItemDetailPriced6.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced6.term = 1;
                newSupplyCatalogItemDetailPriced6.description = "";
                newSupplyCatalogItemDetailPriced6.is_default = false;
                newSupplyCatalogItemDetailPriced6.deleted = false;
                newSupplyCatalogItemDetailPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced7 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced7.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced7.catalog_item_id = newSupplyCatalogItemPriced7.catalog_item_id;
                newSupplyCatalogItemDetailPriced7.catalog_item_detail_code = "CD2093";
                newSupplyCatalogItemDetailPriced7.catalog_item_detail_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemDetailPriced7.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced7.nb_units = 1;
                newSupplyCatalogItemDetailPriced7.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced7.term = 1;
                newSupplyCatalogItemDetailPriced7.description = "";
                newSupplyCatalogItemDetailPriced7.is_default = false;
                newSupplyCatalogItemDetailPriced7.deleted = false;
                newSupplyCatalogItemDetailPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced9 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced9.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced9.catalog_item_id = newSupplyCatalogItemPriced9.catalog_item_id;
                newSupplyCatalogItemDetailPriced9.catalog_item_detail_code = "CD2103";
                newSupplyCatalogItemDetailPriced9.catalog_item_detail_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemDetailPriced9.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced9.nb_units = 1;
                newSupplyCatalogItemDetailPriced9.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced9.term = 1;
                newSupplyCatalogItemDetailPriced9.description = "";
                newSupplyCatalogItemDetailPriced9.is_default = false;
                newSupplyCatalogItemDetailPriced9.deleted = false;
                newSupplyCatalogItemDetailPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced1);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced2); 
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced3);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced5);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced6);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced7);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region Priced catalog_item_category_catalog_item

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced1 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_id = newSupplyCatalogItemPriced1.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced1.display_order = 1;
                newSupplyCatalogItemCategoryCatalogItemPriced1.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced2 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced2.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced2.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced2.catalog_item_id = newSupplyCatalogItemPriced2.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced2.display_order = 2;
                newSupplyCatalogItemCategoryCatalogItemPriced2.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced3 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_id = newSupplyCatalogItemPriced3.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced3.display_order = 3;
                newSupplyCatalogItemCategoryCatalogItemPriced3.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced5 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_id = newSupplyCatalogItemPriced5.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced5.display_order = 5;
                newSupplyCatalogItemCategoryCatalogItemPriced5.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced6 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_id = newSupplyCatalogItemPriced6.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced6.display_order = 6;
                newSupplyCatalogItemCategoryCatalogItemPriced6.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced7 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_id = newSupplyCatalogItemPriced7.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced7.display_order = 4;
                newSupplyCatalogItemCategoryCatalogItemPriced7.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced9 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced9.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced9.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced9.catalog_item_id = newSupplyCatalogItemPriced9.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced9.display_order = 7;
                newSupplyCatalogItemCategoryCatalogItemPriced9.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced1);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced2);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced3);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced5);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced6);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced7);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #region UnPriced catalog

                // Create new record
                catalog newSupplyCatalogUnPriced = new catalog();
                // newSupplyCatalogUnPriced.newCatalog_id;          // Autonumber generated on insert
                newSupplyCatalogUnPriced.catalog_group_id = newSupplyCatalogGroup.catalog_group_id;
                newSupplyCatalogUnPriced.catalog_code = "MC56";
                newSupplyCatalogUnPriced.catalog_name = "Otis Spring 2011 CD/Mag Combo Supplies UnPriced";
                newSupplyCatalogUnPriced.culture = "en-US";
                newSupplyCatalogUnPriced.description = "";
                newSupplyCatalogUnPriced.start_date = new DateTime(2010, 12, 8);
                newSupplyCatalogUnPriced.end_date = new DateTime(2011, 6, 30);
                newSupplyCatalogUnPriced.deleted = false;
                newSupplyCatalogUnPriced.create_date = DateTime.Now;
                newSupplyCatalogUnPriced.create_user_id = CREATE_USER_ID;
                newSupplyCatalogUnPriced.update_date = DateTime.Now;
                newSupplyCatalogUnPriced.update_user_id = CREATE_USER_ID;
                newSupplyCatalogUnPriced.is_priced = false;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newSupplyCatalogUnPriced);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newSupplyCatalogUnPriced.catalog_id.ToString());

                #endregion

                #region UnPriced catalog_item_category

                // Create new record
                catalog_item_category newSupplyCatalogItemCategoryUnPriced1 = new catalog_item_category();
                // newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryUnPriced1.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_name = "Otis Spring 2011 CD/Mag Combo Supplies Unpriced";
                newSupplyCatalogItemCategoryUnPriced1.parent_catalog_item_category_id = 0;
                newSupplyCatalogItemCategoryUnPriced1.deleted = false;
                newSupplyCatalogItemCategoryUnPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryUnPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryUnPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryUnPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newSupplyCatalogItemCategoryUnPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id.ToString());

                #endregion

                #region UnPriced form_section

                // Create new record
                form_section newSupplyPAFormSectionUnPriced1 = new form_section();
                // newSupplyPAFormSectionUnPriced1.form_section_id;              // Autonumber generated on insert
                newSupplyPAFormSectionUnPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyPAFormSectionUnPriced1.form_id = newPAForm.form_id;
                newSupplyPAFormSectionUnPriced1.form_section_type_id = 2;        // 1 = standard product, 2 = supply product, 3 = optional product
                newSupplyPAFormSectionUnPriced1.form_section_number = 2;
                newSupplyPAFormSectionUnPriced1.form_section_title = "Otis Spring 2011 CD/Mag Combo Supplies UnPriced";
                newSupplyPAFormSectionUnPriced1.description = "";
                newSupplyPAFormSectionUnPriced1.deleted = false;
                newSupplyPAFormSectionUnPriced1.create_date = DateTime.Now;
                newSupplyPAFormSectionUnPriced1.create_user_id = CREATE_USER_ID;
                newSupplyPAFormSectionUnPriced1.update_date = DateTime.Now;
                newSupplyPAFormSectionUnPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newSupplyPAFormSectionUnPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newSupplyPAFormSectionUnPriced1.form_section_id.ToString());

                #endregion

                #region UnPriced catalog_item

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced3 = new catalog_item();
                // newSupplyCatalogItemUnPriced3.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced3.product_id = newSupplyProduct3.product_id;
                newSupplyCatalogItemUnPriced3.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced3.catalog_item_code = "CD2062";
                newSupplyCatalogItemUnPriced3.catalog_item_name = "Otis Mass Display";
                newSupplyCatalogItemUnPriced3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced3.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced3.image_url = "";
                newSupplyCatalogItemUnPriced3.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced3.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced3.description = "";
                newSupplyCatalogItemUnPriced3.deleted = false;
                newSupplyCatalogItemUnPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced5 = new catalog_item();
                // newSupplyCatalogItemUnPriced5.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced5.product_id = newSupplyProduct5.product_id;
                newSupplyCatalogItemUnPriced5.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced5.catalog_item_code = "CD2086";
                newSupplyCatalogItemUnPriced5.catalog_item_name = "Otis Launch Poster";
                newSupplyCatalogItemUnPriced5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced5.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced5.image_url = "";
                newSupplyCatalogItemUnPriced5.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced5.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced5.description = "";
                newSupplyCatalogItemUnPriced5.deleted = false;
                newSupplyCatalogItemUnPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced6 = new catalog_item();
                // newSupplyCatalogItemUnPriced6.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced6.product_id = newSupplyProduct6.product_id;
                newSupplyCatalogItemUnPriced6.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced6.catalog_item_code = "CDBULK2306";
                newSupplyCatalogItemUnPriced6.catalog_item_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemUnPriced6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced6.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced6.image_url = "";
                newSupplyCatalogItemUnPriced6.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced6.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced6.description = "";
                newSupplyCatalogItemUnPriced6.deleted = false;
                newSupplyCatalogItemUnPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced7 = new catalog_item();
                // newSupplyCatalogItemUnPriced7.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced7.product_id = newSupplyProduct7.product_id;
                newSupplyCatalogItemUnPriced7.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced7.catalog_item_code = "CD2093";
                newSupplyCatalogItemUnPriced7.catalog_item_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemUnPriced7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced7.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced7.image_url = "";
                newSupplyCatalogItemUnPriced7.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced7.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced7.description = "";
                newSupplyCatalogItemUnPriced7.deleted = false;
                newSupplyCatalogItemUnPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced4 = new catalog_item();
                // newSupplyCatalogItemUnPriced4.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced4.product_id = newSupplyProduct4.product_id;
                newSupplyCatalogItemUnPriced4.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced4.catalog_item_code = "CDK2412";
                newSupplyCatalogItemUnPriced4.catalog_item_name = "Otis Un-Priced - Magazine Tax Kit";
                newSupplyCatalogItemUnPriced4.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced4.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced4.image_url = "";
                newSupplyCatalogItemUnPriced4.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced4.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced4.description = "";
                newSupplyCatalogItemUnPriced4.deleted = false;
                newSupplyCatalogItemUnPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced8 = new catalog_item();
                // newSupplyCatalogItemUnPriced8.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced8.product_id = newSupplyProduct8.product_id;
                newSupplyCatalogItemUnPriced8.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced8.catalog_item_code = "CDK2413";
                newSupplyCatalogItemUnPriced8.catalog_item_name = "Otis Un-Priced - Magazine No Tax Kit";
                newSupplyCatalogItemUnPriced8.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced8.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced8.image_url = "";
                newSupplyCatalogItemUnPriced8.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced8.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced8.description = "";
                newSupplyCatalogItemUnPriced8.deleted = false;
                newSupplyCatalogItemUnPriced8.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced8.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced8.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced9 = new catalog_item();
                // newSupplyCatalogItemUnPriced9.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced9.product_id = newSupplyProduct9.product_id;
                newSupplyCatalogItemUnPriced9.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced9.catalog_item_code = "CD2103";
                newSupplyCatalogItemUnPriced9.catalog_item_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemUnPriced9.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced9.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced9.image_url = "";
                newSupplyCatalogItemUnPriced9.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced9.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced9.description = "";
                newSupplyCatalogItemUnPriced9.deleted = false;
                newSupplyCatalogItemUnPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced9.update_user_id = CREATE_USER_ID;

                #endregion


                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced3);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced4);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced5);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced6);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced7);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced8);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region UnPriced catalog_item_detail


                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced3 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced3.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced3.catalog_item_id = newSupplyCatalogItemUnPriced3.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced3.catalog_item_detail_code = "CD2062";
                newSupplyCatalogItemDetailUnPriced3.catalog_item_detail_name = "Otis Mass Display";
                newSupplyCatalogItemDetailUnPriced3.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced3.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced3.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced3.term = 1;
                newSupplyCatalogItemDetailUnPriced3.description = "";
                newSupplyCatalogItemDetailUnPriced3.is_default = false;
                newSupplyCatalogItemDetailUnPriced3.deleted = false;
                newSupplyCatalogItemDetailUnPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced5 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced5.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced5.catalog_item_id = newSupplyCatalogItemUnPriced5.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced5.catalog_item_detail_code = "CD2086";
                newSupplyCatalogItemDetailUnPriced5.catalog_item_detail_name = "Otis Launch Poster";
                newSupplyCatalogItemDetailUnPriced5.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced5.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced5.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced5.term = 1;
                newSupplyCatalogItemDetailUnPriced5.description = "";
                newSupplyCatalogItemDetailUnPriced5.is_default = false;
                newSupplyCatalogItemDetailUnPriced5.deleted = false;
                newSupplyCatalogItemDetailUnPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced6 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced6.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced6.catalog_item_id = newSupplyCatalogItemUnPriced6.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced6.catalog_item_detail_code = "CDBULK2306";
                newSupplyCatalogItemDetailUnPriced6.catalog_item_detail_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemDetailUnPriced6.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced6.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced6.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced6.term = 1;
                newSupplyCatalogItemDetailUnPriced6.description = "";
                newSupplyCatalogItemDetailUnPriced6.is_default = false;
                newSupplyCatalogItemDetailUnPriced6.deleted = false;
                newSupplyCatalogItemDetailUnPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced7 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced7.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced7.catalog_item_id = newSupplyCatalogItemUnPriced7.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced7.catalog_item_detail_code = "CD2093";
                newSupplyCatalogItemDetailUnPriced7.catalog_item_detail_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemDetailUnPriced7.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced7.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced7.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced7.term = 1;
                newSupplyCatalogItemDetailUnPriced7.description = "";
                newSupplyCatalogItemDetailUnPriced7.is_default = false;
                newSupplyCatalogItemDetailUnPriced7.deleted = false;
                newSupplyCatalogItemDetailUnPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced4 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced4.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced4.catalog_item_id = newSupplyCatalogItemUnPriced4.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced4.catalog_item_detail_code = "CDK2412";
                newSupplyCatalogItemDetailUnPriced4.catalog_item_detail_name = "Otis Un-Priced - Magazine Tax Kit";
                newSupplyCatalogItemDetailUnPriced4.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced4.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced4.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced4.term = 1;
                newSupplyCatalogItemDetailUnPriced4.description = "";
                newSupplyCatalogItemDetailUnPriced4.is_default = false;
                newSupplyCatalogItemDetailUnPriced4.deleted = false;
                newSupplyCatalogItemDetailUnPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced8 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced8.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced8.catalog_item_id = newSupplyCatalogItemUnPriced8.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced8.catalog_item_detail_code = "CDK2413";
                newSupplyCatalogItemDetailUnPriced8.catalog_item_detail_name = "Otis Un-Priced - Magazine No Tax Kit";
                newSupplyCatalogItemDetailUnPriced8.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced8.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced8.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced8.term = 1;
                newSupplyCatalogItemDetailUnPriced8.description = "";
                newSupplyCatalogItemDetailUnPriced8.is_default = false;
                newSupplyCatalogItemDetailUnPriced8.deleted = false;
                newSupplyCatalogItemDetailUnPriced8.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced8.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced8.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced9 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced9.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced9.catalog_item_id = newSupplyCatalogItemUnPriced9.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced9.catalog_item_detail_code = "CD2103";
                newSupplyCatalogItemDetailUnPriced9.catalog_item_detail_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemDetailUnPriced9.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced9.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced9.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced9.term = 1;
                newSupplyCatalogItemDetailUnPriced9.description = "";
                newSupplyCatalogItemDetailUnPriced9.is_default = false;
                newSupplyCatalogItemDetailUnPriced9.deleted = false;
                newSupplyCatalogItemDetailUnPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced3);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced4);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced5);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced6);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced7);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced8);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region UnPriced catalog_item_category_catalog_item

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced3 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.catalog_item_id = newSupplyCatalogItemUnPriced3.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.display_order = 3;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced5 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced5.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.catalog_item_id = newSupplyCatalogItemUnPriced5.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.display_order = 5;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced6 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced6.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.catalog_item_id = newSupplyCatalogItemUnPriced6.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.display_order = 6;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced7 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced7.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.catalog_item_id = newSupplyCatalogItemUnPriced7.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.display_order = 4;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced4 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced4.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.catalog_item_id = newSupplyCatalogItemUnPriced4.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.display_order = 1;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced8 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced8.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.catalog_item_id = newSupplyCatalogItemUnPriced8.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.display_order = 2;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced9 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced9.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.catalog_item_id = newSupplyCatalogItemUnPriced9.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.display_order = 7;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced3);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced4);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced5);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced6);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced7);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced8);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #endregion

                #endregion

                #region Order form requires PA form

                form_requires_form formRequiresForm = new form_requires_form();

                formRequiresForm.form_id = otisBulk14OrderFormId; //Uses Otis 14 Bulk Item Order Form
                formRequiresForm.required_form_id = newPAForm.form_id;
                formRequiresForm.deleted = false;
                formRequiresForm.create_date = DateTime.Now;
                formRequiresForm.create_user_id = CREATE_USER_ID;
                formRequiresForm.update_date = DateTime.Now;
                formRequiresForm.update_user_id = CREATE_USER_ID;

                context.form_requires_forms.InsertOnSubmit(formRequiresForm);
                context.SubmitChanges();

                sb.AppendLine("New form required form id = " + formRequiresForm.form_requires_form_id.ToString());

                #endregion

                sb.AppendLine("Success!");
            }
            catch (Exception ex)
            {
                sb.AppendLine("transaction rollback due to error");
                sb.AppendLine("Message: " + ex.Message);
                sb.AppendLine("Source: " + ex.Source);
                sb.AppendLine("");
                sb.AppendLine(ex.StackTrace);
            }
            finally
            {
                if (context.Connection != null)
                {
                    context.Connection.Close();
                }
            }

            sb.AppendLine("end");

            return sb.ToString();
        }
        public string Spring2011_OtisMagVoucher()
        {
            #region Notes

            // Supply catalogs in PA need one catalog for priced and one catalog for 
            // unpriced items

            #endregion

            #region Start

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("start");

            // Open db context
            QSPFulfillmentDataContext context = new QSPFulfillmentDataContext(CONNECTION_STRING);
            sb.AppendLine("db context open");

            // Open the connection
            context.Connection.Open();

            #endregion

            try
            {
                #region Order form

                #region form_group

                // Create new form group object
                form_group newFormGroup = new form_group();

                // Fill in new form group data
                // newFormGroup.form_group_id;              // Autonumber generated on insert
                newFormGroup.form_group_name = "Otis / Mag Voucher Spring 2011 group";
                newFormGroup.deleted = false;
                newFormGroup.create_date = DateTime.Now;
                newFormGroup.create_user_id = CREATE_USER_ID;
                newFormGroup.update_date = DateTime.Now;
                newFormGroup.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.form_groups.InsertOnSubmit(newFormGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form group id = " + newFormGroup.form_group_id.ToString());
                // MessageBox.Show("New form group id = " + newFormGroup.form_group_id.ToString());

                #endregion

                #region form

                // Create new form object
                form newForm = new form();

                // Fill in new form data
                // newForm.form_id;                             // Autonumber generated on insert
                newForm.form_group_id = newFormGroup.form_group_id;
                newForm.entity_type_id = 4;                     // Type 4 = order
                newForm.form_code = "MC57";
                newForm.form_name = "Otis / Mag Voucher Spring 2011 order form";
                newForm.description = "";
                newForm.order_terms_text = "";
                newForm.start_date = new DateTime(2010, 12, 8);
                newForm.end_date = new DateTime(2011, 6, 30);
                newForm.closing_time = new DateTime(2011, 6, 30);
                newForm.image_url = "images/CatalogItem/chocochunk.gif";
                newForm.is_base_form = false;
                newForm.parent_form_id = 8;                     // Base form for orders in prod and dev
                newForm.is_product_price_updatable = false;
                newForm.is_quantity_adjustment_allowed = true;
                newForm.tax_postal_address_type_id = 2;
                newForm.enabled = true;
                newForm.deleted = false;
                newForm.version = 1;
                newForm.program_id = 3;                         // Otis = 3, PV = 2
                newForm.program_type_id = 7;
                newForm.program_basics_text = "";
                newForm.create_date = DateTime.Now;
                newForm.create_user_id = CREATE_USER_ID;
                newForm.update_date = DateTime.Now;
                newForm.update_user_id = CREATE_USER_ID;
                //newForm.warehouse_type_id;
                newForm.is_bulk = true;
                newForm.report_name = "OrderForm";              // Not used
                newForm.is_warehouse_selectable = false;
                newForm.default_warehouse_id = 27;
                newForm.season_id = 8;

                // Add the new form to the db context
                context.forms.InsertOnSubmit(newForm);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form id = " + newForm.form_id.ToString());
                // MessageBox.Show("New form id = " + newForm.form_id.ToString());

                #endregion

                #region form_permission by user role

                form_permission newFormPermission1 = new form_permission();
                //newFormPermission1.form_permission_id;            // Autonumber generated on insert
                newFormPermission1.form_id = newForm.form_id;
                newFormPermission1.role_id = 0;                     // User
                newFormPermission1.allow_read = false;
                newFormPermission1.allow_write = false;
                newFormPermission1.create_date = DateTime.Now;
                newFormPermission1.create_user_id = CREATE_USER_ID;
                newFormPermission1.update_date = DateTime.Now;
                newFormPermission1.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission2 = new form_permission();
                //newFormPermission2.form_permission_id;            // Autonumber generated on insert
                newFormPermission2.form_id = newForm.form_id;
                newFormPermission2.role_id = 1;                     // Field Sale Manager (FM)
                newFormPermission2.allow_read = true;
                newFormPermission2.allow_write = true;
                newFormPermission2.create_date = DateTime.Now;
                newFormPermission2.create_user_id = CREATE_USER_ID;
                newFormPermission2.update_date = DateTime.Now;
                newFormPermission2.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission3 = new form_permission();
                //newFormPermission3.form_permission_id;            // Autonumber generated on insert
                newFormPermission3.form_id = newForm.form_id;
                newFormPermission3.role_id = 2;                     // Field Support
                newFormPermission3.allow_read = true;
                newFormPermission3.allow_write = true;
                newFormPermission3.create_date = DateTime.Now;
                newFormPermission3.create_user_id = CREATE_USER_ID;
                newFormPermission3.update_date = DateTime.Now;
                newFormPermission3.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission4 = new form_permission();
                //newFormPermission4.form_permission_id;            // Autonumber generated on insert
                newFormPermission4.form_id = newForm.form_id;
                newFormPermission4.role_id = 3;                     // Accounting Manager
                newFormPermission4.allow_read = true;
                newFormPermission4.allow_write = true;
                newFormPermission4.create_date = DateTime.Now;
                newFormPermission4.create_user_id = CREATE_USER_ID;
                newFormPermission4.update_date = DateTime.Now;
                newFormPermission4.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission5 = new form_permission();
                //newFormPermission5.form_permission_id;            // Autonumber generated on insert
                newFormPermission5.form_id = newForm.form_id;
                newFormPermission5.role_id = 4;                     // Admin
                newFormPermission5.allow_read = true;
                newFormPermission5.allow_write = true;
                newFormPermission5.create_date = DateTime.Now;
                newFormPermission5.create_user_id = CREATE_USER_ID;
                newFormPermission5.update_date = DateTime.Now;
                newFormPermission5.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission6 = new form_permission();
                //newFormPermission6.form_permission_id;            // Autonumber generated on insert
                newFormPermission6.form_id = newForm.form_id;
                newFormPermission6.role_id = 5;                     // Super User
                newFormPermission6.allow_read = true;
                newFormPermission6.allow_write = true;
                newFormPermission6.create_date = DateTime.Now;
                newFormPermission6.create_user_id = CREATE_USER_ID;
                newFormPermission6.update_date = DateTime.Now;
                newFormPermission6.update_user_id = CREATE_USER_ID;

                // Add the new form permissions to the db context
                context.form_permissions.InsertOnSubmit(newFormPermission1);
                context.form_permissions.InsertOnSubmit(newFormPermission2);
                context.form_permissions.InsertOnSubmit(newFormPermission3);
                context.form_permissions.InsertOnSubmit(newFormPermission4);
                context.form_permissions.InsertOnSubmit(newFormPermission5);
                context.form_permissions.InsertOnSubmit(newFormPermission6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form permissions ok");

                #endregion

                #region form_delivery_date_type

                form_delivery_date_type newFormDeliveryDateType1 = new form_delivery_date_type();
                //newFormDeliveryDateType1.form_delivery_date_type_id;  // Autonumber generated on insert
                newFormDeliveryDateType1.form_id = newForm.form_id;
                newFormDeliveryDateType1.delivery_date_type_id = 5;     // Otis Spring 2011, week starting in Sunday, 3 to 4 weeks cutoff

                context.form_delivery_date_types.InsertOnSubmit(newFormDeliveryDateType1);
                context.SubmitChanges();

                sb.AppendLine("form delivery date type ok");

                #endregion

                #region form_profit_rate

                form_profit_rate newFormProfitRate1 = new form_profit_rate();
                //newFormProfitRate1.form_profit_rate_id;                // Autonumber generated on insert
                newFormProfitRate1.form_id = newForm.form_id;
                newFormProfitRate1.profit_rate_id = 1;         // 1 = 40%, 2 = 45%, 3 = 50%
                newFormProfitRate1.deleted = false;
                newFormProfitRate1.create_date = DateTime.Now;
                newFormProfitRate1.create_user_id = CREATE_USER_ID;
                newFormProfitRate1.update_date = DateTime.Now;
                newFormProfitRate1.update_user_id = CREATE_USER_ID;

                //form_profit_rate newFormProfitRate2 = new form_profit_rate();
                ////newFormProfitRate2.form_profit_rate_id;                // Autonumber generated on insert
                //newFormProfitRate2.form_id = newForm.form_id;
                //newFormProfitRate2.profit_rate_id = 2;         // 1 = 40%, 2 = 45%, 3 = 50%
                //newFormProfitRate2.deleted = false;
                //newFormProfitRate2.create_date = DateTime.Now;
                //newFormProfitRate2.create_user_id = CREATE_USER_ID;
                //newFormProfitRate2.update_date = DateTime.Now;
                //newFormProfitRate2.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_profit_rates.InsertOnSubmit(newFormProfitRate1);
                //context.form_profit_rates.InsertOnSubmit(newFormProfitRate2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form profit rate ok");

                #endregion

                #region form_order_type

                // Create new form order type object
                form_order_type newFormOrderType1 = new form_order_type();
                // newFormOrderType1.form_order_type_id;            // Autonumber generated on insert
                newFormOrderType1.form_id = newForm.form_id;
                newFormOrderType1.order_type_id = 1;                // 1 = standard order
                newFormOrderType1.deleted = false;
                newFormOrderType1.create_date = DateTime.Now;
                newFormOrderType1.create_user_id = CREATE_USER_ID;
                newFormOrderType1.update_date = DateTime.Now;
                newFormOrderType1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_order_types.InsertOnSubmit(newFormOrderType1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form order type ok");

                #endregion

                #region form_delivery_method

                // Create new record
                form_delivery_method newFormDeliveryMethod1 = new form_delivery_method();
                // newFormDeliveryMethod1.form_delivery_method_id;          // Autonumber generated on insert
                newFormDeliveryMethod1.delivery_method_id = 1;              // Common carrier
                newFormDeliveryMethod1.form_id = newForm.form_id;
                newFormDeliveryMethod1.deleted = false;
                newFormDeliveryMethod1.create_date = DateTime.Now;
                newFormDeliveryMethod1.create_user_id = CREATE_USER_ID;
                newFormDeliveryMethod1.update_date = DateTime.Now;
                newFormDeliveryMethod1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_delivery_methods.InsertOnSubmit(newFormDeliveryMethod1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form delivery methods ok");

                #endregion

                #region catalog_group

                // Create new record
                catalog_group newCatalogGroup = new catalog_group();
                // newCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newCatalogGroup.catalog_group_code = "MC57";
                newCatalogGroup.catalog_group_name = "Otis / Mag Voucher Spring 2011 order";
                newCatalogGroup.description = "";
                newCatalogGroup.deleted = false;
                newCatalogGroup.create_date = DateTime.Now;
                newCatalogGroup.create_user_id = CREATE_USER_ID;
                newCatalogGroup.update_date = DateTime.Now;
                newCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region catalog

                // Create new record
                catalog newCatalog = new catalog();
                // newCatalog.newCatalog_id;          // Autonumber generated on insert
                newCatalog.catalog_group_id = newCatalogGroup.catalog_group_id;
                newCatalog.catalog_code = "MC57";
                newCatalog.catalog_name = "Otis / Mag Voucher Spring 2011 order";
                newCatalog.culture = "en-US";
                newCatalog.description = "";
                newCatalog.start_date = new DateTime(2010, 12, 8);;      // the form begins in july, but for testing we allow it to begin in april (or before)
                newCatalog.end_date = new DateTime(2011, 6, 30);
                newCatalog.deleted = false;
                newCatalog.create_date = DateTime.Now;
                newCatalog.create_user_id = CREATE_USER_ID;
                newCatalog.update_date = DateTime.Now;
                newCatalog.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newCatalog);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newCatalog.catalog_id.ToString());

                #endregion

                #region catalog_item_category

                // Create new record
                catalog_item_category newCatalogItemCategory1 = new catalog_item_category();
                // newCatalogItemCategory1.catalog_item_category_id;         // Autonumber generated on insert
                newCatalogItemCategory1.catalog_id = newCatalog.catalog_id;
                newCatalogItemCategory1.catalog_item_category_name = "Otis / Mag Voucher Spring 2011";
                newCatalogItemCategory1.parent_catalog_item_category_id = 0;        // not used, do not like nulls
                newCatalogItemCategory1.deleted = false;
                newCatalogItemCategory1.create_date = DateTime.Now;
                newCatalogItemCategory1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategory1.update_date = DateTime.Now;
                newCatalogItemCategory1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newCatalogItemCategory1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newCatalogItemCategory1.catalog_item_category_id.ToString());

                #endregion

                #region form_section

                // Create new record
                form_section newFormSection1 = new form_section();
                // newFormSection1.form_section_id;              // Autonumber generated on insert
                newFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newFormSection1.form_id = newForm.form_id;
                newFormSection1.form_section_type_id = 1;        // 1 = standard product, 2 = supply product, 3 = optional product
                newFormSection1.form_section_number = 1;
                newFormSection1.form_section_title = "Otis / Mag Voucher Spring 2011";
                newFormSection1.description = "";
                newFormSection1.deleted = false;
                newFormSection1.create_date = DateTime.Now;
                newFormSection1.create_user_id = CREATE_USER_ID;
                newFormSection1.update_date = DateTime.Now;
                newFormSection1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newFormSection1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newFormSection1.form_section_id.ToString());

                #endregion

                #region product

                #region Create new record

                product newProduct1 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct1.product_code = "74300";
                newProduct1.product_name = "Chocolate Chip";
                newProduct1.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct1.nb_units = 1;                       // boxes per case
                newProduct1.nb_day_lead_time = 0;
                newProduct1.product_status_id = 101;            // 101 = Active
                newProduct1.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct1.business_division_id = 1;           // 1 = US
                newProduct1.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct1.coupon_id = null;
                newProduct1.description = "";
                newProduct1.image_url = "";
                newProduct1.is_free_sample = false;
                newProduct1.oracle_code = "";
                newProduct1.IVCOUP = "";
                newProduct1.IVITEM = "74300";
                newProduct1.unit_cost = null;
                newProduct1.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct1.vendor_item_code = "74300";
                newProduct1.deleted = false;
                newProduct1.create_date = DateTime.Now;
                newProduct1.create_user_id = CREATE_USER_ID;
                newProduct1.update_date = DateTime.Now;
                newProduct1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct2 = new product();
                // newProduct2.product_id;                      // Autonumber generated on insert
                newProduct2.product_code = "74304";
                newProduct2.product_name = "Butter Sugar";
                newProduct2.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct2.nb_units = 1;                       // boxes per case
                newProduct2.nb_day_lead_time = 0;
                newProduct2.product_status_id = 101;            // 101 = Active
                newProduct2.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct2.business_division_id = 1;           // 1 = US
                newProduct2.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct2.coupon_id = null;
                newProduct2.description = "";
                newProduct2.image_url = "";
                newProduct2.is_free_sample = false;
                newProduct2.oracle_code = "";
                newProduct2.IVCOUP = "";
                newProduct2.IVITEM = "74304";
                newProduct2.unit_cost = null;
                newProduct2.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct2.vendor_item_code = "74304";
                newProduct2.deleted = false;
                newProduct2.create_date = DateTime.Now;
                newProduct2.create_user_id = CREATE_USER_ID;
                newProduct2.update_date = DateTime.Now;
                newProduct2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct3 = new product();
                // newProduct3.product_id;                      // Autonumber generated on insert
                newProduct3.product_code = "74323";
                newProduct3.product_name = "Strawberry Shortcake";
                newProduct3.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct3.nb_units = 1;                       // boxes per case
                newProduct3.nb_day_lead_time = 0;
                newProduct3.product_status_id = 101;            // 101 = Active
                newProduct3.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct3.business_division_id = 1;           // 1 = US
                newProduct3.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct3.coupon_id = null;
                newProduct3.description = "";
                newProduct3.image_url = "";
                newProduct3.is_free_sample = false;
                newProduct3.oracle_code = "";
                newProduct3.IVCOUP = "";
                newProduct3.IVITEM = "74323";
                newProduct3.unit_cost = null;
                newProduct3.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct3.vendor_item_code = "74323";
                newProduct3.deleted = false;
                newProduct3.create_date = DateTime.Now;
                newProduct3.create_user_id = CREATE_USER_ID;
                newProduct3.update_date = DateTime.Now;
                newProduct3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct4 = new product();
                // newProduct4.product_id;                      // Autonumber generated on insert
                newProduct4.product_code = "74308";
                newProduct4.product_name = "Carnival";
                newProduct4.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct4.nb_units = 1;                       // boxes per case
                newProduct4.nb_day_lead_time = 0;
                newProduct4.product_status_id = 101;            // 101 = Active
                newProduct4.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct4.business_division_id = 1;           // 1 = US
                newProduct4.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct4.coupon_id = null;
                newProduct4.description = "";
                newProduct4.image_url = "";
                newProduct4.is_free_sample = false;
                newProduct4.oracle_code = "";
                newProduct4.IVCOUP = "";
                newProduct4.IVITEM = "74308";
                newProduct4.unit_cost = null;
                newProduct4.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct4.vendor_item_code = "74308";
                newProduct4.deleted = false;
                newProduct4.create_date = DateTime.Now;
                newProduct4.create_user_id = CREATE_USER_ID;
                newProduct4.update_date = DateTime.Now;
                newProduct4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct5 = new product();
                // newProduct5.product_id;                      // Autonumber generated on insert
                newProduct5.product_code = "74319";
                newProduct5.product_name = "Cranberry Oatmeal";
                newProduct5.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct5.nb_units = 1;                       // boxes per case
                newProduct5.nb_day_lead_time = 0;
                newProduct5.product_status_id = 101;            // 101 = Active
                newProduct5.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct5.business_division_id = 1;           // 1 = US
                newProduct5.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct5.coupon_id = null;
                newProduct5.description = "";
                newProduct5.image_url = "";
                newProduct5.is_free_sample = false;
                newProduct5.oracle_code = "";
                newProduct5.IVCOUP = "";
                newProduct5.IVITEM = "74319";
                newProduct5.unit_cost = null;
                newProduct5.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct5.vendor_item_code = "74319";
                newProduct5.deleted = false;
                newProduct5.create_date = DateTime.Now;
                newProduct5.create_user_id = CREATE_USER_ID;
                newProduct5.update_date = DateTime.Now;
                newProduct5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct6 = new product();
                // newProduct6.product_id;                      // Autonumber generated on insert
                newProduct6.product_code = "74314";
                newProduct6.product_name = "Triple Chocolate Chunk";
                newProduct6.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct6.nb_units = 1;                       // boxes per case
                newProduct6.nb_day_lead_time = 0;
                newProduct6.product_status_id = 101;            // 101 = Active
                newProduct6.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct6.business_division_id = 1;           // 1 = US
                newProduct6.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct6.coupon_id = null;
                newProduct6.description = "";
                newProduct6.image_url = "";
                newProduct6.is_free_sample = false;
                newProduct6.oracle_code = "";
                newProduct6.IVCOUP = "";
                newProduct6.IVITEM = "74314";
                newProduct6.unit_cost = null;
                newProduct6.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct6.vendor_item_code = "74314";
                newProduct6.deleted = false;
                newProduct6.create_date = DateTime.Now;
                newProduct6.create_user_id = CREATE_USER_ID;
                newProduct6.update_date = DateTime.Now;
                newProduct6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct7 = new product();
                // newProduct7.product_id;                      // Autonumber generated on insert
                newProduct7.product_code = "74307";
                newProduct7.product_name = "White Chocolate Macademia";
                newProduct7.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct7.nb_units = 1;                       // boxes per case
                newProduct7.nb_day_lead_time = 0;
                newProduct7.product_status_id = 101;            // 101 = Active
                newProduct7.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct7.business_division_id = 1;           // 1 = US
                newProduct7.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct7.coupon_id = null;
                newProduct7.description = "";
                newProduct7.image_url = "";
                newProduct7.is_free_sample = false;
                newProduct7.oracle_code = "";
                newProduct7.IVCOUP = "";
                newProduct7.IVITEM = "74307";
                newProduct7.unit_cost = null;
                newProduct7.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct7.vendor_item_code = "74307";
                newProduct7.deleted = false;
                newProduct7.create_date = DateTime.Now;
                newProduct7.create_user_id = CREATE_USER_ID;
                newProduct7.update_date = DateTime.Now;
                newProduct7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct8 = new product();
                // newProduct8.product_id;                      // Autonumber generated on insert
                newProduct8.product_code = "74305";
                newProduct8.product_name = "Peanut Butter";
                newProduct8.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct8.nb_units = 1;                       // boxes per case
                newProduct8.nb_day_lead_time = 0;
                newProduct8.product_status_id = 101;            // 101 = Active
                newProduct8.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct8.business_division_id = 1;           // 1 = US
                newProduct8.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct8.coupon_id = null;
                newProduct8.description = "";
                newProduct8.image_url = "";
                newProduct8.is_free_sample = false;
                newProduct8.oracle_code = "";
                newProduct8.IVCOUP = "";
                newProduct8.IVITEM = "74305";
                newProduct8.unit_cost = null;
                newProduct8.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct8.vendor_item_code = "74305";
                newProduct8.deleted = false;
                newProduct8.create_date = DateTime.Now;
                newProduct8.create_user_id = CREATE_USER_ID;
                newProduct8.update_date = DateTime.Now;
                newProduct8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct9 = new product();
                // newProduct9.product_id;                      // Autonumber generated on insert
                newProduct9.product_code = "74389";
                newProduct9.product_name = "Reduced Fat Chocolate Chip";
                newProduct9.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct9.nb_units = 1;                       // boxes per case
                newProduct9.nb_day_lead_time = 0;
                newProduct9.product_status_id = 101;            // 101 = Active
                newProduct9.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct9.business_division_id = 1;           // 1 = US
                newProduct9.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct9.coupon_id = null;
                newProduct9.description = "";
                newProduct9.image_url = "";
                newProduct9.is_free_sample = false;
                newProduct9.oracle_code = "";
                newProduct9.IVCOUP = "";
                newProduct9.IVITEM = "74389";
                newProduct9.unit_cost = null;
                newProduct9.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct9.vendor_item_code = "74389";
                newProduct9.deleted = false;
                newProduct9.create_date = DateTime.Now;
                newProduct9.create_user_id = CREATE_USER_ID;
                newProduct9.update_date = DateTime.Now;
                newProduct9.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct10 = new product();
                // newProduct10.product_id;                      // Autonumber generated on insert
                newProduct10.product_code = "74303";
                newProduct10.product_name = "Oatmeal Raisin";
                newProduct10.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct10.nb_units = 1;                       // boxes per case
                newProduct10.nb_day_lead_time = 0;
                newProduct10.product_status_id = 101;            // 101 = Active
                newProduct10.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct10.business_division_id = 1;           // 1 = US
                newProduct10.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct10.coupon_id = null;
                newProduct10.description = "";
                newProduct10.image_url = "";
                newProduct10.is_free_sample = false;
                newProduct10.oracle_code = "";
                newProduct10.IVCOUP = "";
                newProduct10.IVITEM = "74303";
                newProduct10.unit_cost = null;
                newProduct10.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct10.vendor_item_code = "74303";
                newProduct10.deleted = false;
                newProduct10.create_date = DateTime.Now;
                newProduct10.create_user_id = CREATE_USER_ID;
                newProduct10.update_date = DateTime.Now;
                newProduct10.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct12 = new product();
                // newProduct12.product_id;                      // Autonumber generated on insert
                newProduct12.product_code = "74315";
                newProduct12.product_name = "The Pink Cookie";
                newProduct12.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct12.nb_units = 1;                       // boxes per case
                newProduct12.nb_day_lead_time = 0;
                newProduct12.product_status_id = 101;            // 101 = Active
                newProduct12.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct12.business_division_id = 1;           // 1 = US
                newProduct12.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct12.coupon_id = null;
                newProduct12.description = "";
                newProduct12.image_url = "";
                newProduct12.is_free_sample = false;
                newProduct12.oracle_code = "";
                newProduct12.IVCOUP = "";
                newProduct12.IVITEM = "74315";
                newProduct12.unit_cost = null;
                newProduct12.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct12.vendor_item_code = "74315";
                newProduct12.deleted = false;
                newProduct12.create_date = DateTime.Now;
                newProduct12.create_user_id = CREATE_USER_ID;
                newProduct12.update_date = DateTime.Now;
                newProduct12.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct13 = new product();
                // newProduct13.product_id;                      // Autonumber generated on insert
                newProduct13.product_code = "70300";
                newProduct13.product_name = "Pretzel Kit";
                newProduct13.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct13.nb_units = 1;                       // boxes per case
                newProduct13.nb_day_lead_time = 0;
                newProduct13.product_status_id = 101;            // 101 = Active
                newProduct13.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct13.business_division_id = 1;           // 1 = US
                newProduct13.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct13.coupon_id = null;
                newProduct13.description = "";
                newProduct13.image_url = "";
                newProduct13.is_free_sample = false;
                newProduct13.oracle_code = "";
                newProduct13.IVCOUP = "";
                newProduct13.IVITEM = "70300";
                newProduct13.unit_cost = null;
                newProduct13.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct13.vendor_item_code = "70300";
                newProduct13.deleted = false;
                newProduct13.create_date = DateTime.Now;
                newProduct13.create_user_id = CREATE_USER_ID;
                newProduct13.update_date = DateTime.Now;
                newProduct13.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct14 = new product();
                // newProduct14.product_id;                      // Autonumber generated on insert
                newProduct14.product_code = "77145";
                newProduct14.product_name = "Apple Cinnamon Coffee Cake";
                newProduct14.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct14.nb_units = 1;                       // boxes per case
                newProduct14.nb_day_lead_time = 0;
                newProduct14.product_status_id = 101;            // 101 = Active
                newProduct14.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct14.business_division_id = 1;           // 1 = US
                newProduct14.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct14.coupon_id = null;
                newProduct14.description = "";
                newProduct14.image_url = "";
                newProduct14.is_free_sample = false;
                newProduct14.oracle_code = "";
                newProduct14.IVCOUP = "";
                newProduct14.IVITEM = "77145";
                newProduct14.unit_cost = null;
                newProduct14.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct14.vendor_item_code = "77145";
                newProduct14.deleted = false;
                newProduct14.create_date = DateTime.Now;
                newProduct14.create_user_id = CREATE_USER_ID;
                newProduct14.update_date = DateTime.Now;
                newProduct14.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct15 = new product();
                // newProduct15.product_id;                      // Autonumber generated on insert
                newProduct15.product_code = "70370";
                newProduct15.product_name = "Double Chocolate Chip Brownies";
                newProduct15.price = Convert.ToDecimal(9);      // Retail case cost
                newProduct15.nb_units = 1;                       // boxes per case
                newProduct15.nb_day_lead_time = 0;
                newProduct15.product_status_id = 101;            // 101 = Active
                newProduct15.product_type_id = 6;                // 6 = Cookies (cookie dough or frozen food)
                newProduct15.business_division_id = 1;           // 1 = US
                newProduct15.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct15.coupon_id = null;
                newProduct15.description = "";
                newProduct15.image_url = "";
                newProduct15.is_free_sample = false;
                newProduct15.oracle_code = "";
                newProduct15.IVCOUP = "";
                newProduct15.IVITEM = "70370";
                newProduct15.unit_cost = null;
                newProduct15.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct15.vendor_item_code = "70370";
                newProduct15.deleted = false;
                newProduct15.create_date = DateTime.Now;
                newProduct15.create_user_id = CREATE_USER_ID;
                newProduct15.update_date = DateTime.Now;
                newProduct15.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct16 = new product();
                // newProduct16.product_id;                      // Autonumber generated on insert
                newProduct16.product_code = "5510";
                newProduct16.product_name = "$10 Magazine voucher";
                newProduct16.price = Convert.ToDecimal(10);      // Retail case cost
                newProduct16.nb_units = 1;                       // boxes per case
                newProduct16.nb_day_lead_time = 0;
                newProduct16.product_status_id = 101;            // 101 = Active
                newProduct16.product_type_id = 17;                // 17 = Mag Voucher
                newProduct16.business_division_id = 1;           // 1 = US
                newProduct16.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct16.coupon_id = null;
                newProduct16.description = "";
                newProduct16.image_url = "";
                newProduct16.is_free_sample = false;
                newProduct16.oracle_code = "";
                newProduct16.IVCOUP = "";
                newProduct16.IVITEM = "5510";
                newProduct16.unit_cost = null;
                newProduct16.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct16.vendor_item_code = "5510";
                newProduct16.deleted = false;
                newProduct16.create_date = DateTime.Now;
                newProduct16.create_user_id = CREATE_USER_ID;
                newProduct16.update_date = DateTime.Now;
                newProduct16.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct17 = new product();
                // newProduct17.product_id;                      // Autonumber generated on insert
                newProduct17.product_code = "5520";
                newProduct17.product_name = "$20 Magazine voucher";
                newProduct17.price = Convert.ToDecimal(20);      // Retail case cost
                newProduct17.nb_units = 1;                       // boxes per case
                newProduct17.nb_day_lead_time = 0;
                newProduct17.product_status_id = 101;            // 101 = Active
                newProduct17.product_type_id = 17;                // 17 = Mag Voucher
                newProduct17.business_division_id = 1;           // 1 = US
                newProduct17.commission = Convert.ToDecimal(0);  // Calculated in as400
                newProduct17.coupon_id = null;
                newProduct17.description = "";
                newProduct17.image_url = "";
                newProduct17.is_free_sample = false;
                newProduct17.oracle_code = "";
                newProduct17.IVCOUP = "";
                newProduct17.IVITEM = "5520";
                newProduct17.unit_cost = null;
                newProduct17.vendor_id = 27;                     // 27 = prod, 28 = dev
                newProduct17.vendor_item_code = "5520";
                newProduct17.deleted = false;
                newProduct17.create_date = DateTime.Now;
                newProduct17.create_user_id = CREATE_USER_ID;
                newProduct17.update_date = DateTime.Now;
                newProduct17.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.products.InsertOnSubmit(newProduct1);
                context.products.InsertOnSubmit(newProduct2);
                context.products.InsertOnSubmit(newProduct3);
                context.products.InsertOnSubmit(newProduct4);
                context.products.InsertOnSubmit(newProduct5);
                context.products.InsertOnSubmit(newProduct6);
                context.products.InsertOnSubmit(newProduct7);
                context.products.InsertOnSubmit(newProduct8);
                context.products.InsertOnSubmit(newProduct9);
                context.products.InsertOnSubmit(newProduct10);
                context.products.InsertOnSubmit(newProduct12);
                context.products.InsertOnSubmit(newProduct13);
                context.products.InsertOnSubmit(newProduct14);
                context.products.InsertOnSubmit(newProduct15);
                context.products.InsertOnSubmit(newProduct16);
                context.products.InsertOnSubmit(newProduct17);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("products ok");

                #endregion

                #region catalog_item

                #region Create new record

                catalog_item newCatalogItem1 = new catalog_item();
                // newCatalogItem1.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem1.product_id = newProduct1.product_id;
                newCatalogItem1.catalog_id = newCatalog.catalog_id;
                newCatalogItem1.catalog_item_code = "74300";
                newCatalogItem1.catalog_item_name = "Chocolate Chip";
                newCatalogItem1.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem1.nb_units = 1;                       // Boxes per case
                newCatalogItem1.image_url = "";
                newCatalogItem1.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem1.catalog_item_export_name = "";
                newCatalogItem1.description = "";
                newCatalogItem1.deleted = false;
                newCatalogItem1.create_date = DateTime.Now;
                newCatalogItem1.create_user_id = CREATE_USER_ID;
                newCatalogItem1.update_date = DateTime.Now;
                newCatalogItem1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem2 = new catalog_item();
                // newCatalogItem2.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem2.product_id = newProduct2.product_id;
                newCatalogItem2.catalog_id = newCatalog.catalog_id;
                newCatalogItem2.catalog_item_code = "74304";
                newCatalogItem2.catalog_item_name = "Butter Sugar";
                newCatalogItem2.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem2.nb_units = 1;                       // Boxes per case
                newCatalogItem2.image_url = "";
                newCatalogItem2.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem2.catalog_item_export_name = "";
                newCatalogItem2.description = "";
                newCatalogItem2.deleted = false;
                newCatalogItem2.create_date = DateTime.Now;
                newCatalogItem2.create_user_id = CREATE_USER_ID;
                newCatalogItem2.update_date = DateTime.Now;
                newCatalogItem2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem3 = new catalog_item();
                // newCatalogItem3.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem3.product_id = newProduct3.product_id;
                newCatalogItem3.catalog_id = newCatalog.catalog_id;
                newCatalogItem3.catalog_item_code = "74323";
                newCatalogItem3.catalog_item_name = "Strawberry Shortcake";
                newCatalogItem3.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem3.nb_units = 1;                       // Boxes per case
                newCatalogItem3.image_url = "";
                newCatalogItem3.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem3.catalog_item_export_name = "";
                newCatalogItem3.description = "";
                newCatalogItem3.deleted = false;
                newCatalogItem3.create_date = DateTime.Now;
                newCatalogItem3.create_user_id = CREATE_USER_ID;
                newCatalogItem3.update_date = DateTime.Now;
                newCatalogItem3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem4 = new catalog_item();
                // newCatalogItem4.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem4.product_id = newProduct4.product_id;
                newCatalogItem4.catalog_id = newCatalog.catalog_id;
                newCatalogItem4.catalog_item_code = "74308";
                newCatalogItem4.catalog_item_name = "Carnival";
                newCatalogItem4.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem4.nb_units = 1;                       // Boxes per case
                newCatalogItem4.image_url = "";
                newCatalogItem4.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem4.catalog_item_export_name = "";
                newCatalogItem4.description = "";
                newCatalogItem4.deleted = false;
                newCatalogItem4.create_date = DateTime.Now;
                newCatalogItem4.create_user_id = CREATE_USER_ID;
                newCatalogItem4.update_date = DateTime.Now;
                newCatalogItem4.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem5 = new catalog_item();
                // newCatalogItem5.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem5.product_id = newProduct5.product_id;
                newCatalogItem5.catalog_id = newCatalog.catalog_id;
                newCatalogItem5.catalog_item_code = "74319";
                newCatalogItem5.catalog_item_name = "Cranberry Oatmeal";
                newCatalogItem5.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem5.nb_units = 1;                       // Boxes per case
                newCatalogItem5.image_url = "";
                newCatalogItem5.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem5.catalog_item_export_name = "";
                newCatalogItem5.description = "";
                newCatalogItem5.deleted = false;
                newCatalogItem5.create_date = DateTime.Now;
                newCatalogItem5.create_user_id = CREATE_USER_ID;
                newCatalogItem5.update_date = DateTime.Now;
                newCatalogItem5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem6 = new catalog_item();
                // newCatalogItem6.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem6.product_id = newProduct6.product_id;
                newCatalogItem6.catalog_id = newCatalog.catalog_id;
                newCatalogItem6.catalog_item_code = "74314";
                newCatalogItem6.catalog_item_name = "Triple Chocolate Chunk ";
                newCatalogItem6.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem6.nb_units = 1;                       // Boxes per case
                newCatalogItem6.image_url = "";
                newCatalogItem6.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem6.catalog_item_export_name = "";
                newCatalogItem6.description = "";
                newCatalogItem6.deleted = false;
                newCatalogItem6.create_date = DateTime.Now;
                newCatalogItem6.create_user_id = CREATE_USER_ID;
                newCatalogItem6.update_date = DateTime.Now;
                newCatalogItem6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem7 = new catalog_item();
                // newCatalogItem7.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem7.product_id = newProduct7.product_id;
                newCatalogItem7.catalog_id = newCatalog.catalog_id;
                newCatalogItem7.catalog_item_code = "74307";
                newCatalogItem7.catalog_item_name = "White Chocolate Macademia";
                newCatalogItem7.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem7.nb_units = 1;                       // Boxes per case
                newCatalogItem7.image_url = "";
                newCatalogItem7.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem7.catalog_item_export_name = "";
                newCatalogItem7.description = "";
                newCatalogItem7.deleted = false;
                newCatalogItem7.create_date = DateTime.Now;
                newCatalogItem7.create_user_id = CREATE_USER_ID;
                newCatalogItem7.update_date = DateTime.Now;
                newCatalogItem7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem8 = new catalog_item();
                // newCatalogItem8.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem8.product_id = newProduct8.product_id;
                newCatalogItem8.catalog_id = newCatalog.catalog_id;
                newCatalogItem8.catalog_item_code = "74305";
                newCatalogItem8.catalog_item_name = "Penaut Butter";
                newCatalogItem8.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem8.nb_units = 1;                       // Boxes per case
                newCatalogItem8.image_url = "";
                newCatalogItem8.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem8.catalog_item_export_name = "";
                newCatalogItem8.description = "";
                newCatalogItem8.deleted = false;
                newCatalogItem8.create_date = DateTime.Now;
                newCatalogItem8.create_user_id = CREATE_USER_ID;
                newCatalogItem8.update_date = DateTime.Now;
                newCatalogItem8.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem9 = new catalog_item();
                // newCatalogItem9.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem9.product_id = newProduct9.product_id;
                newCatalogItem9.catalog_id = newCatalog.catalog_id;
                newCatalogItem9.catalog_item_code = "74389";
                newCatalogItem9.catalog_item_name = "Reduced Fat Chocolate Chip";
                newCatalogItem9.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem9.nb_units = 1;                       // Boxes per case
                newCatalogItem9.image_url = "";
                newCatalogItem9.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem9.catalog_item_export_name = "";
                newCatalogItem9.description = "";
                newCatalogItem9.deleted = false;
                newCatalogItem9.create_date = DateTime.Now;
                newCatalogItem9.create_user_id = CREATE_USER_ID;
                newCatalogItem9.update_date = DateTime.Now;
                newCatalogItem9.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem10 = new catalog_item();
                // newCatalogItem10.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem10.product_id = newProduct10.product_id;
                newCatalogItem10.catalog_id = newCatalog.catalog_id;
                newCatalogItem10.catalog_item_code = "74303";
                newCatalogItem10.catalog_item_name = "Oatmeal Raisin";
                newCatalogItem10.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem10.nb_units = 1;                       // Boxes per case
                newCatalogItem10.image_url = "";
                newCatalogItem10.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem10.catalog_item_export_name = "";
                newCatalogItem10.description = "";
                newCatalogItem10.deleted = false;
                newCatalogItem10.create_date = DateTime.Now;
                newCatalogItem10.create_user_id = CREATE_USER_ID;
                newCatalogItem10.update_date = DateTime.Now;
                newCatalogItem10.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem12 = new catalog_item();
                // newCatalogItem12.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem12.product_id = newProduct12.product_id;
                newCatalogItem12.catalog_id = newCatalog.catalog_id;
                newCatalogItem12.catalog_item_code = "74315";
                newCatalogItem12.catalog_item_name = "The Pink Cookie";
                newCatalogItem12.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem12.nb_units = 1;                       // Boxes per case
                newCatalogItem12.image_url = "";
                newCatalogItem12.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem12.catalog_item_export_name = "";
                newCatalogItem12.description = "";
                newCatalogItem12.deleted = false;
                newCatalogItem12.create_date = DateTime.Now;
                newCatalogItem12.create_user_id = CREATE_USER_ID;
                newCatalogItem12.update_date = DateTime.Now;
                newCatalogItem12.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem13 = new catalog_item();
                // newCatalogItem13.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem13.product_id = newProduct13.product_id;
                newCatalogItem13.catalog_id = newCatalog.catalog_id;
                newCatalogItem13.catalog_item_code = "70300";
                newCatalogItem13.catalog_item_name = "Pretzel Kit";
                newCatalogItem13.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem13.nb_units = 1;                       // Boxes per case
                newCatalogItem13.image_url = "";
                newCatalogItem13.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem13.catalog_item_export_name = "";
                newCatalogItem13.description = "";
                newCatalogItem13.deleted = false;
                newCatalogItem13.create_date = DateTime.Now;
                newCatalogItem13.create_user_id = CREATE_USER_ID;
                newCatalogItem13.update_date = DateTime.Now;
                newCatalogItem13.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem14 = new catalog_item();
                // newCatalogItem14.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem14.product_id = newProduct14.product_id;
                newCatalogItem14.catalog_id = newCatalog.catalog_id;
                newCatalogItem14.catalog_item_code = "77145";
                newCatalogItem14.catalog_item_name = "Apple Cinnamon Coffee Cake";
                newCatalogItem14.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem14.nb_units = 1;                       // Boxes per case
                newCatalogItem14.image_url = "";
                newCatalogItem14.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem14.catalog_item_export_name = "";
                newCatalogItem14.description = "";
                newCatalogItem14.deleted = false;
                newCatalogItem14.create_date = DateTime.Now;
                newCatalogItem14.create_user_id = CREATE_USER_ID;
                newCatalogItem14.update_date = DateTime.Now;
                newCatalogItem14.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem15 = new catalog_item();
                // newCatalogItem15.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem15.product_id = newProduct15.product_id;
                newCatalogItem15.catalog_id = newCatalog.catalog_id;
                newCatalogItem15.catalog_item_code = "70370";
                newCatalogItem15.catalog_item_name = "Double Chocolate Chip Brownies";
                newCatalogItem15.price = Convert.ToDecimal(9);      // Retail case cost
                newCatalogItem15.nb_units = 1;                       // Boxes per case
                newCatalogItem15.image_url = "";
                newCatalogItem15.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem15.catalog_item_export_name = "";
                newCatalogItem15.description = "";
                newCatalogItem15.deleted = false;
                newCatalogItem15.create_date = DateTime.Now;
                newCatalogItem15.create_user_id = CREATE_USER_ID;
                newCatalogItem15.update_date = DateTime.Now;
                newCatalogItem15.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem16 = new catalog_item();
                // newCatalogItem16.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem16.product_id = newProduct16.product_id;
                newCatalogItem16.catalog_id = newCatalog.catalog_id;
                newCatalogItem16.catalog_item_code = "5510";
                newCatalogItem16.catalog_item_name = "$10 Magazine voucher";
                newCatalogItem16.price = Convert.ToDecimal(10);      // Retail case cost
                newCatalogItem16.nb_units = 1;                       // Boxes per case
                newCatalogItem16.image_url = "";
                newCatalogItem16.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem16.catalog_item_export_name = "";
                newCatalogItem16.description = "";
                newCatalogItem16.deleted = false;
                newCatalogItem16.create_date = DateTime.Now;
                newCatalogItem16.create_user_id = CREATE_USER_ID;
                newCatalogItem16.update_date = DateTime.Now;
                newCatalogItem16.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem17 = new catalog_item();
                // newCatalogItem17.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem17.product_id = newProduct17.product_id;
                newCatalogItem17.catalog_id = newCatalog.catalog_id;
                newCatalogItem17.catalog_item_code = "5520";
                newCatalogItem17.catalog_item_name = "$20 Magazine voucher";
                newCatalogItem17.price = Convert.ToDecimal(20);      // Retail case cost
                newCatalogItem17.nb_units = 1;                       // Boxes per case
                newCatalogItem17.image_url = "";
                newCatalogItem17.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem17.catalog_item_export_name = "";
                newCatalogItem17.description = "";
                newCatalogItem17.deleted = false;
                newCatalogItem17.create_date = DateTime.Now;
                newCatalogItem17.create_user_id = CREATE_USER_ID;
                newCatalogItem17.update_date = DateTime.Now;
                newCatalogItem17.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newCatalogItem1);
                context.catalog_items.InsertOnSubmit(newCatalogItem2);
                context.catalog_items.InsertOnSubmit(newCatalogItem3);
                context.catalog_items.InsertOnSubmit(newCatalogItem4);
                context.catalog_items.InsertOnSubmit(newCatalogItem5);
                context.catalog_items.InsertOnSubmit(newCatalogItem6);
                context.catalog_items.InsertOnSubmit(newCatalogItem7);
                context.catalog_items.InsertOnSubmit(newCatalogItem8);
                context.catalog_items.InsertOnSubmit(newCatalogItem9);
                context.catalog_items.InsertOnSubmit(newCatalogItem10);
                context.catalog_items.InsertOnSubmit(newCatalogItem12);
                context.catalog_items.InsertOnSubmit(newCatalogItem13);
                context.catalog_items.InsertOnSubmit(newCatalogItem14);
                context.catalog_items.InsertOnSubmit(newCatalogItem15);
                context.catalog_items.InsertOnSubmit(newCatalogItem16);
                context.catalog_items.InsertOnSubmit(newCatalogItem17);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region catalog_item_detail

                #region 40% profit

                catalog_item_detail newCatalogItemDetail_40_1 = new catalog_item_detail();
                // newCatalogItemDetail_40_1.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_1.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemDetail_40_1.catalog_item_detail_code = "74300";
                newCatalogItemDetail_40_1.catalog_item_detail_name = "Chocolate Chip";
                newCatalogItemDetail_40_1.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_1.nb_units = 1;
                newCatalogItemDetail_40_1.profit_rate = 0.40;
                newCatalogItemDetail_40_1.term = 1;
                newCatalogItemDetail_40_1.description = "";
                newCatalogItemDetail_40_1.is_default = false;
                newCatalogItemDetail_40_1.deleted = false;
                newCatalogItemDetail_40_1.create_date = DateTime.Now;
                newCatalogItemDetail_40_1.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_1.update_date = DateTime.Now;
                newCatalogItemDetail_40_1.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_2 = new catalog_item_detail();
                // newCatalogItemDetail_40_2.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_2.catalog_item_id = newCatalogItem2.catalog_item_id;
                newCatalogItemDetail_40_2.catalog_item_detail_code = "74304";
                newCatalogItemDetail_40_2.catalog_item_detail_name = "Butter Sugar";
                newCatalogItemDetail_40_2.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_2.nb_units = 1;
                newCatalogItemDetail_40_2.profit_rate = 0.40;
                newCatalogItemDetail_40_2.term = 1;
                newCatalogItemDetail_40_2.description = "";
                newCatalogItemDetail_40_2.is_default = false;
                newCatalogItemDetail_40_2.deleted = false;
                newCatalogItemDetail_40_2.create_date = DateTime.Now;
                newCatalogItemDetail_40_2.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_2.update_date = DateTime.Now;
                newCatalogItemDetail_40_2.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_3 = new catalog_item_detail();
                // newCatalogItemDetail_40_3.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_3.catalog_item_id = newCatalogItem3.catalog_item_id;
                newCatalogItemDetail_40_3.catalog_item_detail_code = "74323";
                newCatalogItemDetail_40_3.catalog_item_detail_name = "Strawberry Shortcake";
                newCatalogItemDetail_40_3.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_3.nb_units = 1;
                newCatalogItemDetail_40_3.profit_rate = 0.40;
                newCatalogItemDetail_40_3.term = 1;
                newCatalogItemDetail_40_3.description = "";
                newCatalogItemDetail_40_3.is_default = false;
                newCatalogItemDetail_40_3.deleted = false;
                newCatalogItemDetail_40_3.create_date = DateTime.Now;
                newCatalogItemDetail_40_3.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_3.update_date = DateTime.Now;
                newCatalogItemDetail_40_3.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_4 = new catalog_item_detail();
                // newCatalogItemDetail_40_4.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_4.catalog_item_id = newCatalogItem4.catalog_item_id;
                newCatalogItemDetail_40_4.catalog_item_detail_code = "74308";
                newCatalogItemDetail_40_4.catalog_item_detail_name = "Carnival";
                newCatalogItemDetail_40_4.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_4.nb_units = 1;
                newCatalogItemDetail_40_4.profit_rate = 0.40;
                newCatalogItemDetail_40_4.term = 1;
                newCatalogItemDetail_40_4.description = "";
                newCatalogItemDetail_40_4.is_default = false;
                newCatalogItemDetail_40_4.deleted = false;
                newCatalogItemDetail_40_4.create_date = DateTime.Now;
                newCatalogItemDetail_40_4.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_4.update_date = DateTime.Now;
                newCatalogItemDetail_40_4.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_5 = new catalog_item_detail();
                // newCatalogItemDetail_40_5.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_5.catalog_item_id = newCatalogItem5.catalog_item_id;
                newCatalogItemDetail_40_5.catalog_item_detail_code = "74319";
                newCatalogItemDetail_40_5.catalog_item_detail_name = "Cranberry Oatmeal";
                newCatalogItemDetail_40_5.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_5.nb_units = 1;
                newCatalogItemDetail_40_5.profit_rate = 0.40;
                newCatalogItemDetail_40_5.term = 1;
                newCatalogItemDetail_40_5.description = "";
                newCatalogItemDetail_40_5.is_default = false;
                newCatalogItemDetail_40_5.deleted = false;
                newCatalogItemDetail_40_5.create_date = DateTime.Now;
                newCatalogItemDetail_40_5.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_5.update_date = DateTime.Now;
                newCatalogItemDetail_40_5.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_6 = new catalog_item_detail();
                // newCatalogItemDetail_40_6.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_6.catalog_item_id = newCatalogItem6.catalog_item_id;
                newCatalogItemDetail_40_6.catalog_item_detail_code = "74314";
                newCatalogItemDetail_40_6.catalog_item_detail_name = "Triple Chocolate Chunk ";
                newCatalogItemDetail_40_6.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_6.nb_units = 1;
                newCatalogItemDetail_40_6.profit_rate = 0.40;
                newCatalogItemDetail_40_6.term = 1;
                newCatalogItemDetail_40_6.description = "";
                newCatalogItemDetail_40_6.is_default = false;
                newCatalogItemDetail_40_6.deleted = false;
                newCatalogItemDetail_40_6.create_date = DateTime.Now;
                newCatalogItemDetail_40_6.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_6.update_date = DateTime.Now;
                newCatalogItemDetail_40_6.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_7 = new catalog_item_detail();
                // newCatalogItemDetail_40_7.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_7.catalog_item_id = newCatalogItem7.catalog_item_id;
                newCatalogItemDetail_40_7.catalog_item_detail_code = "74307";
                newCatalogItemDetail_40_7.catalog_item_detail_name = "White Chocolate Macademia";
                newCatalogItemDetail_40_7.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_7.nb_units = 1;
                newCatalogItemDetail_40_7.profit_rate = 0.40;
                newCatalogItemDetail_40_7.term = 1;
                newCatalogItemDetail_40_7.description = "";
                newCatalogItemDetail_40_7.is_default = false;
                newCatalogItemDetail_40_7.deleted = false;
                newCatalogItemDetail_40_7.create_date = DateTime.Now;
                newCatalogItemDetail_40_7.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_7.update_date = DateTime.Now;
                newCatalogItemDetail_40_7.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_8 = new catalog_item_detail();
                // newCatalogItemDetail_40_8.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_8.catalog_item_id = newCatalogItem8.catalog_item_id;
                newCatalogItemDetail_40_8.catalog_item_detail_code = "74305";
                newCatalogItemDetail_40_8.catalog_item_detail_name = "Penaut Butter";
                newCatalogItemDetail_40_8.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_8.nb_units = 1;
                newCatalogItemDetail_40_8.profit_rate = 0.40;
                newCatalogItemDetail_40_8.term = 1;
                newCatalogItemDetail_40_8.description = "";
                newCatalogItemDetail_40_8.is_default = false;
                newCatalogItemDetail_40_8.deleted = false;
                newCatalogItemDetail_40_8.create_date = DateTime.Now;
                newCatalogItemDetail_40_8.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_8.update_date = DateTime.Now;
                newCatalogItemDetail_40_8.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_9 = new catalog_item_detail();
                // newCatalogItemDetail_40_9.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_9.catalog_item_id = newCatalogItem9.catalog_item_id;
                newCatalogItemDetail_40_9.catalog_item_detail_code = "74389";
                newCatalogItemDetail_40_9.catalog_item_detail_name = "Reduced Fat Chocolate Chip";
                newCatalogItemDetail_40_9.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_9.nb_units = 1;
                newCatalogItemDetail_40_9.profit_rate = 0.40;
                newCatalogItemDetail_40_9.term = 1;
                newCatalogItemDetail_40_9.description = "";
                newCatalogItemDetail_40_9.is_default = false;
                newCatalogItemDetail_40_9.deleted = false;
                newCatalogItemDetail_40_9.create_date = DateTime.Now;
                newCatalogItemDetail_40_9.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_9.update_date = DateTime.Now;
                newCatalogItemDetail_40_9.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_10 = new catalog_item_detail();
                // newCatalogItemDetail_40_10.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_10.catalog_item_id = newCatalogItem10.catalog_item_id;
                newCatalogItemDetail_40_10.catalog_item_detail_code = "74303";
                newCatalogItemDetail_40_10.catalog_item_detail_name = "Oatmeal Raisin";
                newCatalogItemDetail_40_10.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_10.nb_units = 1;
                newCatalogItemDetail_40_10.profit_rate = 0.40;
                newCatalogItemDetail_40_10.term = 1;
                newCatalogItemDetail_40_10.description = "";
                newCatalogItemDetail_40_10.is_default = false;
                newCatalogItemDetail_40_10.deleted = false;
                newCatalogItemDetail_40_10.create_date = DateTime.Now;
                newCatalogItemDetail_40_10.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_10.update_date = DateTime.Now;
                newCatalogItemDetail_40_10.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_12 = new catalog_item_detail();
                // newCatalogItemDetail_40_12.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_12.catalog_item_id = newCatalogItem12.catalog_item_id;
                newCatalogItemDetail_40_12.catalog_item_detail_code = "74315";
                newCatalogItemDetail_40_12.catalog_item_detail_name = "The Pink Cookie";
                newCatalogItemDetail_40_12.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_12.nb_units = 1;
                newCatalogItemDetail_40_12.profit_rate = 0.40;
                newCatalogItemDetail_40_12.term = 1;
                newCatalogItemDetail_40_12.description = "";
                newCatalogItemDetail_40_12.is_default = false;
                newCatalogItemDetail_40_12.deleted = false;
                newCatalogItemDetail_40_12.create_date = DateTime.Now;
                newCatalogItemDetail_40_12.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_12.update_date = DateTime.Now;
                newCatalogItemDetail_40_12.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_13 = new catalog_item_detail();
                // newCatalogItemDetail_40_13.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_13.catalog_item_id = newCatalogItem13.catalog_item_id;
                newCatalogItemDetail_40_13.catalog_item_detail_code = "70300";
                newCatalogItemDetail_40_13.catalog_item_detail_name = "Pretzel Kit";
                newCatalogItemDetail_40_13.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_13.nb_units = 1;
                newCatalogItemDetail_40_13.profit_rate = 0.40;
                newCatalogItemDetail_40_13.term = 1;
                newCatalogItemDetail_40_13.description = "";
                newCatalogItemDetail_40_13.is_default = false;
                newCatalogItemDetail_40_13.deleted = false;
                newCatalogItemDetail_40_13.create_date = DateTime.Now;
                newCatalogItemDetail_40_13.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_13.update_date = DateTime.Now;
                newCatalogItemDetail_40_13.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_14 = new catalog_item_detail();
                // newCatalogItemDetail_40_14.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_14.catalog_item_id = newCatalogItem14.catalog_item_id;
                newCatalogItemDetail_40_14.catalog_item_detail_code = "77145";
                newCatalogItemDetail_40_14.catalog_item_detail_name = "Apple Cinnamon Coffee Cake";
                newCatalogItemDetail_40_14.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_14.nb_units = 1;
                newCatalogItemDetail_40_14.profit_rate = 0.40;
                newCatalogItemDetail_40_14.term = 1;
                newCatalogItemDetail_40_14.description = "";
                newCatalogItemDetail_40_14.is_default = false;
                newCatalogItemDetail_40_14.deleted = false;
                newCatalogItemDetail_40_14.create_date = DateTime.Now;
                newCatalogItemDetail_40_14.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_14.update_date = DateTime.Now;
                newCatalogItemDetail_40_14.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_15 = new catalog_item_detail();
                // newCatalogItemDetail_40_15.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_15.catalog_item_id = newCatalogItem15.catalog_item_id;
                newCatalogItemDetail_40_15.catalog_item_detail_code = "70370";
                newCatalogItemDetail_40_15.catalog_item_detail_name = "Double Chocolate Chip Brownies";
                newCatalogItemDetail_40_15.price = Convert.ToDecimal(9);
                newCatalogItemDetail_40_15.nb_units = 1;
                newCatalogItemDetail_40_15.profit_rate = 0.40;
                newCatalogItemDetail_40_15.term = 1;
                newCatalogItemDetail_40_15.description = "";
                newCatalogItemDetail_40_15.is_default = false;
                newCatalogItemDetail_40_15.deleted = false;
                newCatalogItemDetail_40_15.create_date = DateTime.Now;
                newCatalogItemDetail_40_15.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_15.update_date = DateTime.Now;
                newCatalogItemDetail_40_15.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_16 = new catalog_item_detail();
                // newCatalogItemDetail_40_16.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_16.catalog_item_id = newCatalogItem16.catalog_item_id;
                newCatalogItemDetail_40_16.catalog_item_detail_code = "5510";
                newCatalogItemDetail_40_16.catalog_item_detail_name = "$10 Magazine voucher";
                newCatalogItemDetail_40_16.price = Convert.ToDecimal(6.00);
                newCatalogItemDetail_40_16.nb_units = 1;
                newCatalogItemDetail_40_16.profit_rate = 0.40;
                newCatalogItemDetail_40_16.term = 1;
                newCatalogItemDetail_40_16.description = "";
                newCatalogItemDetail_40_16.is_default = false;
                newCatalogItemDetail_40_16.deleted = false;
                newCatalogItemDetail_40_16.create_date = DateTime.Now;
                newCatalogItemDetail_40_16.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_16.update_date = DateTime.Now;
                newCatalogItemDetail_40_16.update_user_id = CREATE_USER_ID;

                catalog_item_detail newCatalogItemDetail_40_17 = new catalog_item_detail();
                // newCatalogItemDetail_40_17.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail_40_17.catalog_item_id = newCatalogItem17.catalog_item_id;
                newCatalogItemDetail_40_17.catalog_item_detail_code = "5520";
                newCatalogItemDetail_40_17.catalog_item_detail_name = "$20 Magazine voucher";
                newCatalogItemDetail_40_17.price = Convert.ToDecimal(12.00);
                newCatalogItemDetail_40_17.nb_units = 1;
                newCatalogItemDetail_40_17.profit_rate = 0.40;
                newCatalogItemDetail_40_17.term = 1;
                newCatalogItemDetail_40_17.description = "";
                newCatalogItemDetail_40_17.is_default = false;
                newCatalogItemDetail_40_17.deleted = false;
                newCatalogItemDetail_40_17.create_date = DateTime.Now;
                newCatalogItemDetail_40_17.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail_40_17.update_date = DateTime.Now;
                newCatalogItemDetail_40_17.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_1);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_2);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_3);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_4);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_5);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_6);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_7);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_8);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_9);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_10);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_12);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_13);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_14);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_15);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_16);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail_40_17);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region catalog_item_category_catalog_item

                #region Standard product

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem1 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem1.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem1.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemCategoryCatalogItem1.display_order = 1;
                newCatalogItemCategoryCatalogItem1.deleted = false;
                newCatalogItemCategoryCatalogItem1.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem1.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem1.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem2 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem2.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem2.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem2.catalog_item_id = newCatalogItem2.catalog_item_id;
                newCatalogItemCategoryCatalogItem2.display_order = 2;
                newCatalogItemCategoryCatalogItem2.deleted = false;
                newCatalogItemCategoryCatalogItem2.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem2.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem2.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem2.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem3 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem3.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem3.catalog_item_id = newCatalogItem3.catalog_item_id;
                newCatalogItemCategoryCatalogItem3.display_order = 3;
                newCatalogItemCategoryCatalogItem3.deleted = false;
                newCatalogItemCategoryCatalogItem3.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem3.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem3.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem3.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem4 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem4.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem4.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem4.catalog_item_id = newCatalogItem4.catalog_item_id;
                newCatalogItemCategoryCatalogItem4.display_order = 4;
                newCatalogItemCategoryCatalogItem4.deleted = false;
                newCatalogItemCategoryCatalogItem4.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem4.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem4.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem4.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem5 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem5.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem5.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem5.catalog_item_id = newCatalogItem5.catalog_item_id;
                newCatalogItemCategoryCatalogItem5.display_order = 5;
                newCatalogItemCategoryCatalogItem5.deleted = false;
                newCatalogItemCategoryCatalogItem5.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem5.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem5.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem5.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem6 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem6.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem6.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem6.catalog_item_id = newCatalogItem6.catalog_item_id;
                newCatalogItemCategoryCatalogItem6.display_order = 6;
                newCatalogItemCategoryCatalogItem6.deleted = false;
                newCatalogItemCategoryCatalogItem6.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem6.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem6.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem6.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem7 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem7.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem7.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem7.catalog_item_id = newCatalogItem7.catalog_item_id;
                newCatalogItemCategoryCatalogItem7.display_order = 7;
                newCatalogItemCategoryCatalogItem7.deleted = false;
                newCatalogItemCategoryCatalogItem7.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem7.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem7.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem7.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem8 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem8.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem8.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem8.catalog_item_id = newCatalogItem8.catalog_item_id;
                newCatalogItemCategoryCatalogItem8.display_order = 8;
                newCatalogItemCategoryCatalogItem8.deleted = false;
                newCatalogItemCategoryCatalogItem8.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem8.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem8.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem8.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem9 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem9.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem9.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem9.catalog_item_id = newCatalogItem9.catalog_item_id;
                newCatalogItemCategoryCatalogItem9.display_order = 9;
                newCatalogItemCategoryCatalogItem9.deleted = false;
                newCatalogItemCategoryCatalogItem9.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem9.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem9.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem9.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem10 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem10.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem10.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem10.catalog_item_id = newCatalogItem10.catalog_item_id;
                newCatalogItemCategoryCatalogItem10.display_order = 10;
                newCatalogItemCategoryCatalogItem10.deleted = false;
                newCatalogItemCategoryCatalogItem10.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem10.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem10.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem10.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem12 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem12.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem12.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem12.catalog_item_id = newCatalogItem12.catalog_item_id;
                newCatalogItemCategoryCatalogItem12.display_order = 12;
                newCatalogItemCategoryCatalogItem12.deleted = false;
                newCatalogItemCategoryCatalogItem12.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem12.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem12.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem12.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem13 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem13.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem13.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem13.catalog_item_id = newCatalogItem13.catalog_item_id;
                newCatalogItemCategoryCatalogItem13.display_order = 13;
                newCatalogItemCategoryCatalogItem13.deleted = false;
                newCatalogItemCategoryCatalogItem13.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem13.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem13.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem13.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem14 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem14.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem14.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem14.catalog_item_id = newCatalogItem14.catalog_item_id;
                newCatalogItemCategoryCatalogItem14.display_order = 14;
                newCatalogItemCategoryCatalogItem14.deleted = false;
                newCatalogItemCategoryCatalogItem14.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem14.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem14.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem14.update_user_id = CREATE_USER_ID;

                // Create new record
                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem15 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem15.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem15.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem15.catalog_item_id = newCatalogItem15.catalog_item_id;
                newCatalogItemCategoryCatalogItem15.display_order = 15;
                newCatalogItemCategoryCatalogItem15.deleted = false;
                newCatalogItemCategoryCatalogItem15.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem15.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem15.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem15.update_user_id = CREATE_USER_ID;

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem16 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem16.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem16.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem16.catalog_item_id = newCatalogItem16.catalog_item_id;
                newCatalogItemCategoryCatalogItem16.display_order = 16;
                newCatalogItemCategoryCatalogItem16.deleted = false;
                newCatalogItemCategoryCatalogItem16.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem16.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem16.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem16.update_user_id = CREATE_USER_ID;

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem17 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem17.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem17.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem17.catalog_item_id = newCatalogItem17.catalog_item_id;
                newCatalogItemCategoryCatalogItem17.display_order = 17;
                newCatalogItemCategoryCatalogItem17.deleted = false;
                newCatalogItemCategoryCatalogItem17.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem17.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem17.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem17.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem1);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem2);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem3);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem4);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem5);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem6);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem7);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem8);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem9);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem10);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem12);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem13);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem14);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem15);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem16);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem17);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #region business_rule

                #region Order Requirements

                #region For All Sections

                #region Standard lead time

                // Create new form group object
                business_rule newBusinessRule3 = new business_rule();

                // Fill in new record data
                // newBusinessRule3.business_rule_id;           // Created upon insert
                newBusinessRule3.form_id = newForm.form_id;
                newBusinessRule3.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule3.field_id = 35;                 // 35 = min_nb_day_lead_time
                newBusinessRule3.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule3.business_rule_name = "min_nb_day_lead_time";
                newBusinessRule3.value_to_compare = "15";
                // newBusinessRule3.form_section_number;        // null, not used
                // newBusinessRule3.description;                // null, not used
                // newBusinessRule3.message;                    // null, not used
                newBusinessRule3.deleted = false;
                newBusinessRule3.create_date = DateTime.Now;
                newBusinessRule3.create_user_id = CREATE_USER_ID;
                newBusinessRule3.update_date = DateTime.Now;
                newBusinessRule3.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule3);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Minimum Total Quantity

                // Create new form group object
                business_rule newBusinessRule4 = new business_rule();

                // Fill in new record data
                // newBusinessRule4.business_rule_id;           // Created upon insert
                newBusinessRule4.form_id = newForm.form_id;
                newBusinessRule4.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule4.field_id = 49;                 // 49 = min_total_quantity
                newBusinessRule4.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule4.business_rule_name = "min_total_quantity";
                newBusinessRule4.value_to_compare = "1";
                // newBusinessRule4.form_section_number;        // null, not used
                // newBusinessRule4.description;                // null, not used
                // newBusinessRule4.message;                    // null, not used
                newBusinessRule4.deleted = false;
                newBusinessRule4.create_date = DateTime.Now;
                newBusinessRule4.create_user_id = CREATE_USER_ID;
                newBusinessRule4.update_date = DateTime.Now;
                newBusinessRule4.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule4);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #endregion

                #endregion

                #endregion

                #region business_exception

                #region Standard product - minimum day lead time

                // Create new form group object
                business_exception newBusinessException2 = new business_exception();

                // Fill in new record data
                //newBusinessException2.business_exception_id;      // newBusinessException1
                newBusinessException2.form_id = newForm.form_id;
                newBusinessException2.business_exception_name = "Standard product - minimum day lead time";
                newBusinessException2.exception_type_id = 100;      // 100 = Note
                newBusinessException2.entity_type_id = 4;           // 4 = Order
                newBusinessException2.warning_message = "15 Business Days Lead-Time Required.<BR>If requested Lead-Time is <u>LESS</u> than 15 business Days, change the delivery date to meet this requirement.";
                newBusinessException2.app_item_id = 23;             // 23 = Order Form Step 3 - Information
                newBusinessException2.message = "For Product Orders by Common Carrier <u>LESS</u> than 15 Business Days cannot be guaranteed. ";
                newBusinessException2.exception_expression = "[nb_day_lead_time] <  [min_nb_day_lead_time]";
                newBusinessException2.fees_value_expression = "";
                newBusinessException2.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException2.form_section_number;       // null, not used
                // newBusinessException2.business_rule_id;          // null, not used
                newBusinessException2.deleted = false;
                newBusinessException2.create_date = DateTime.Now;
                newBusinessException2.create_user_id = CREATE_USER_ID;
                newBusinessException2.update_date = DateTime.Now;
                newBusinessException2.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #region Standard product - minimum total quantity

                // Create new form group object
                business_exception newBusinessException7 = new business_exception();

                // Fill in new record data
                //newBusinessException7.business_exception_id;      // newBusinessException1
                newBusinessException7.form_id = newForm.form_id;
                newBusinessException7.business_exception_name = "Standard product - minimum total quantity (cookie dough)";
                newBusinessException7.exception_type_id = 100;      // 100 = Note
                newBusinessException7.entity_type_id = 4;           // 4 = Order
                newBusinessException7.warning_message = "NOTE : If order quantity is <u>less</u> than 120, Shipping Charges ($100) applies.  If order quantity is <u>greater</u> than 120 - $100 Shipping Charges is waived.";
                newBusinessException7.app_item_id = 24;             // 24 = Order form step 4 - order detail
                newBusinessException7.message = "NOTE : If order quantity is <u>less</u> than 120, Shipping Charges ($100) applies.  If order quantity is <u>greater</u> than 120 - $100 Shipping Charges is waived.";
                newBusinessException7.exception_expression = "([total_quantity] <  [min_total_quantity])";
                newBusinessException7.fees_value_expression = "";
                newBusinessException7.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException7.form_section_number;       // null, not used
                // newBusinessException7.business_rule_id;          // null, not used
                newBusinessException7.deleted = false;
                newBusinessException7.create_date = DateTime.Now;
                newBusinessException7.create_user_id = CREATE_USER_ID;
                newBusinessException7.update_date = DateTime.Now;
                newBusinessException7.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException7);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion

                #endregion

                #endregion

                #region PA form

                #region form

                // Create new form object
                form newPAForm = new form();

                // Fill in new form data
                // newPAForm.form_id;                             // Autonumber generated on insert
                newPAForm.form_group_id = newFormGroup.form_group_id;
                newPAForm.entity_type_id = 12;                     // Type 12 = program agreement
                newPAForm.form_code = "MC57";
                newPAForm.form_name = "Otis / Mag Voucher Spring 2011 PA";
                newPAForm.description = "";
                newPAForm.order_terms_text = "";
                newPAForm.start_date = new DateTime(2010, 12, 8);;
                newPAForm.end_date = new DateTime(2011, 6, 30);;
                newPAForm.closing_time = new DateTime(2011, 6, 30);;
                newPAForm.image_url = "images/CatalogItem/chocochunk.gif";
                newPAForm.is_base_form = false;
                newPAForm.parent_form_id = 48;                     // Base form for orders in prod and dev
                newPAForm.is_product_price_updatable = false;
                newPAForm.is_quantity_adjustment_allowed = true;
                newPAForm.tax_postal_address_type_id = 2;
                newPAForm.enabled = true;
                newPAForm.deleted = false;
                newPAForm.version = 1;
                newPAForm.program_id = 3;                         // Otis = 3, PV = 2
                newPAForm.program_type_id = 7;
                newPAForm.program_basics_text = "";
                newPAForm.create_date = DateTime.Now;
                newPAForm.create_user_id = CREATE_USER_ID;
                newPAForm.update_date = DateTime.Now;
                newPAForm.update_user_id = CREATE_USER_ID;
                //newPAForm.warehouse_type_id;
                newPAForm.is_bulk = true;
                newPAForm.report_name = "OrderForm";
                newPAForm.is_warehouse_selectable = true;
                //newPAForm.default_warehouse_id;
                newPAForm.season_id = 8;

                // Add the new form to the db context
                context.forms.InsertOnSubmit(newPAForm);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form id = " + newPAForm.form_id.ToString());

                #endregion

                #region form_permission

                form_permission newPAFormPermission1 = new form_permission();
                //newPAFormPermission1.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission1.form_id = newPAForm.form_id;
                newPAFormPermission1.role_id = 0;                     // User
                newPAFormPermission1.allow_read = false;
                newPAFormPermission1.allow_write = false;
                newPAFormPermission1.create_date = DateTime.Now;
                newPAFormPermission1.create_user_id = CREATE_USER_ID;
                newPAFormPermission1.update_date = DateTime.Now;
                newPAFormPermission1.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission2 = new form_permission();
                //newPAFormPermission2.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission2.form_id = newPAForm.form_id;
                newPAFormPermission2.role_id = 1;                     // Field Sale Manager (FM)
                newPAFormPermission2.allow_read = true;
                newPAFormPermission2.allow_write = true;
                newPAFormPermission2.create_date = DateTime.Now;
                newPAFormPermission2.create_user_id = CREATE_USER_ID;
                newPAFormPermission2.update_date = DateTime.Now;
                newPAFormPermission2.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission3 = new form_permission();
                //newPAFormPermission3.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission3.form_id = newPAForm.form_id;
                newPAFormPermission3.role_id = 2;                     // Field Support
                newPAFormPermission3.allow_read = true;
                newPAFormPermission3.allow_write = true;
                newPAFormPermission3.create_date = DateTime.Now;
                newPAFormPermission3.create_user_id = CREATE_USER_ID;
                newPAFormPermission3.update_date = DateTime.Now;
                newPAFormPermission3.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission4 = new form_permission();
                //newPAFormPermission4.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission4.form_id = newPAForm.form_id;
                newPAFormPermission4.role_id = 3;                     // Accounting Manager
                newPAFormPermission4.allow_read = true;
                newPAFormPermission4.allow_write = true;
                newPAFormPermission4.create_date = DateTime.Now;
                newPAFormPermission4.create_user_id = CREATE_USER_ID;
                newPAFormPermission4.update_date = DateTime.Now;
                newPAFormPermission4.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission5 = new form_permission();
                //newPAFormPermission5.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission5.form_id = newPAForm.form_id;
                newPAFormPermission5.role_id = 4;                     // Admin
                newPAFormPermission5.allow_read = true;
                newPAFormPermission5.allow_write = true;
                newPAFormPermission5.create_date = DateTime.Now;
                newPAFormPermission5.create_user_id = CREATE_USER_ID;
                newPAFormPermission5.update_date = DateTime.Now;
                newPAFormPermission5.update_user_id = CREATE_USER_ID;

                form_permission newPAFormPermission6 = new form_permission();
                //newPAFormPermission6.form_permission_id;            // Autonumber generated on insert
                newPAFormPermission6.form_id = newPAForm.form_id;
                newPAFormPermission6.role_id = 5;                     // Super User
                newPAFormPermission6.allow_read = true;
                newPAFormPermission6.allow_write = true;
                newPAFormPermission6.create_date = DateTime.Now;
                newPAFormPermission6.create_user_id = CREATE_USER_ID;
                newPAFormPermission6.update_date = DateTime.Now;
                newPAFormPermission6.update_user_id = CREATE_USER_ID;

                // Add the new form permissions to the db context
                context.form_permissions.InsertOnSubmit(newPAFormPermission1);
                context.form_permissions.InsertOnSubmit(newPAFormPermission2);
                context.form_permissions.InsertOnSubmit(newPAFormPermission3);
                context.form_permissions.InsertOnSubmit(newPAFormPermission4);
                context.form_permissions.InsertOnSubmit(newPAFormPermission5);
                context.form_permissions.InsertOnSubmit(newPAFormPermission6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form permissions ok");

                #endregion

                #region form_profit_rate

                form_profit_rate newPAFormProfitRate1 = new form_profit_rate();
                //newPAFormProfitRate1.form_profit_rate_id;                // Autonumber generated on insert
                newPAFormProfitRate1.form_id = newPAForm.form_id;
                newPAFormProfitRate1.profit_rate_id = 1;         // 1 = 40%, 2 = 45%, 3 = 50%
                newPAFormProfitRate1.deleted = false;
                newPAFormProfitRate1.create_date = DateTime.Now;
                newPAFormProfitRate1.create_user_id = CREATE_USER_ID;
                newPAFormProfitRate1.update_date = DateTime.Now;
                newPAFormProfitRate1.update_user_id = CREATE_USER_ID;

                //form_profit_rate newPAFormProfitRate2 = new form_profit_rate();
                ////newPAFormProfitRate2.form_profit_rate_id;                // Autonumber generated on insert
                //newPAFormProfitRate2.form_id = newPAForm.form_id;
                //newPAFormProfitRate2.profit_rate_id = 2;         // 1 = 40%, 2 = 45%, 3 = 50%
                //newPAFormProfitRate2.deleted = false;
                //newPAFormProfitRate2.create_date = DateTime.Now;
                //newPAFormProfitRate2.create_user_id = CREATE_USER_ID;
                //newPAFormProfitRate2.update_date = DateTime.Now;
                //newPAFormProfitRate2.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_profit_rates.InsertOnSubmit(newPAFormProfitRate1);
                //context.form_profit_rates.InsertOnSubmit(newPAFormProfitRate2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form profit rate ok");

                #endregion

                #region form_section

                #region order catalog section

                // this is just because we need it to exist

                // Create new record
                form_section newPAFormSection1 = new form_section();
                // newPAFormSection1.form_section_id;              // Autonumber generated on insert
                newPAFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newPAFormSection1.form_id = newPAForm.form_id;
                newPAFormSection1.form_section_type_id = 1;        // 1 = standard product, 2 = supply product, 3 = optional product
                newPAFormSection1.form_section_number = 1;
                newPAFormSection1.form_section_title = "Otis / Mag Voucher Spring 2011";
                newPAFormSection1.description = "";
                newPAFormSection1.deleted = false;
                newPAFormSection1.create_date = DateTime.Now;
                newPAFormSection1.create_user_id = CREATE_USER_ID;
                newPAFormSection1.update_date = DateTime.Now;
                newPAFormSection1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newPAFormSection1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newPAFormSection1.form_section_id.ToString());

                #endregion

                #endregion

                #region Supplies in PA

                #region catalog_group

                // Create new record
                catalog_group newSupplyCatalogGroup = new catalog_group();
                // newSupplyCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newSupplyCatalogGroup.catalog_group_code = "MC57";
                newSupplyCatalogGroup.catalog_group_name = "Otis  / Mag Voucher Spring 2011 Supplies";
                newSupplyCatalogGroup.description = "";
                newSupplyCatalogGroup.deleted = false;
                newSupplyCatalogGroup.create_date = DateTime.Now;
                newSupplyCatalogGroup.create_user_id = CREATE_USER_ID;
                newSupplyCatalogGroup.update_date = DateTime.Now;
                newSupplyCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newSupplyCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newSupplyCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region product

                #region Create new record

                product newSupplyProduct1 = new product();
                // newSupplyProduct1.product_id;                      // Autonumber generated on insert
                newSupplyProduct1.product_code = "CDK2421";
                newSupplyProduct1.product_name = "Otis/Mag Bulk Brochure priced";
                newSupplyProduct1.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct1.nb_units = 1;                       // boxes per case
                newSupplyProduct1.nb_day_lead_time = 0;
                newSupplyProduct1.product_status_id = 101;            // 101 = Active
                newSupplyProduct1.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct1.business_division_id = 1;           // 1 = US
                newSupplyProduct1.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct1.coupon_id = null;
                newSupplyProduct1.description = "";
                newSupplyProduct1.image_url = "";
                newSupplyProduct1.is_free_sample = false;
                newSupplyProduct1.oracle_code = "";
                newSupplyProduct1.IVCOUP = "";
                newSupplyProduct1.IVITEM = "CDK2421";
                newSupplyProduct1.unit_cost = null;
                newSupplyProduct1.vendor_id = 27;                     // 27 = prod
                newSupplyProduct1.vendor_item_code = "CDK2421";
                newSupplyProduct1.deleted = false;
                newSupplyProduct1.create_date = DateTime.Now;
                newSupplyProduct1.create_user_id = CREATE_USER_ID;
                newSupplyProduct1.update_date = DateTime.Now;
                newSupplyProduct1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct2 = new product();
                // newSupplyProduct2.product_id;                      // Autonumber generated on insert
                newSupplyProduct2.product_code = "CDK2422";
                newSupplyProduct2.product_name = "Otis/Mag Bulk Brochure Un-Priced";
                newSupplyProduct2.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct2.nb_units = 1;                       // boxes per case
                newSupplyProduct2.nb_day_lead_time = 0;
                newSupplyProduct2.product_status_id = 101;            // 101 = Active
                newSupplyProduct2.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct2.business_division_id = 1;           // 1 = US
                newSupplyProduct2.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct2.coupon_id = null;
                newSupplyProduct2.description = "";
                newSupplyProduct2.image_url = "";
                newSupplyProduct2.is_free_sample = false;
                newSupplyProduct2.oracle_code = "";
                newSupplyProduct2.IVCOUP = "";
                newSupplyProduct2.IVITEM = "CDK2422";
                newSupplyProduct2.unit_cost = null;
                newSupplyProduct2.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct2.vendor_item_code = "CDK2422";
                newSupplyProduct2.deleted = false;
                newSupplyProduct2.create_date = DateTime.Now;
                newSupplyProduct2.create_user_id = CREATE_USER_ID;
                newSupplyProduct2.update_date = DateTime.Now;
                newSupplyProduct2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct3 = new product();
                // newSupplyProduct3.product_id;                      // Autonumber generated on insert
                newSupplyProduct3.product_code = "CD2062";
                newSupplyProduct3.product_name = "Otis Mass Display";
                newSupplyProduct3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct3.nb_units = 1;                       // boxes per case
                newSupplyProduct3.nb_day_lead_time = 0;
                newSupplyProduct3.product_status_id = 101;            // 101 = Active
                newSupplyProduct3.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct3.business_division_id = 1;           // 1 = US
                newSupplyProduct3.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct3.coupon_id = null;
                newSupplyProduct3.description = "";
                newSupplyProduct3.image_url = "";
                newSupplyProduct3.is_free_sample = false;
                newSupplyProduct3.oracle_code = "";
                newSupplyProduct3.IVCOUP = "";
                newSupplyProduct3.IVITEM = "CD2062";
                newSupplyProduct3.unit_cost = null;
                newSupplyProduct3.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct3.vendor_item_code = "CD2062";
                newSupplyProduct3.deleted = false;
                newSupplyProduct3.create_date = DateTime.Now;
                newSupplyProduct3.create_user_id = CREATE_USER_ID;
                newSupplyProduct3.update_date = DateTime.Now;
                newSupplyProduct3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct5 = new product();
                // newSupplyProduct5.product_id;                      // Autonumber generated on insert
                newSupplyProduct5.product_code = "CD2086";
                newSupplyProduct5.product_name = "Otis Launch Poster";
                newSupplyProduct5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct5.nb_units = 1;                       // boxes per case
                newSupplyProduct5.nb_day_lead_time = 0;
                newSupplyProduct5.product_status_id = 101;            // 101 = Active
                newSupplyProduct5.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct5.business_division_id = 1;           // 1 = US
                newSupplyProduct5.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct5.coupon_id = null;
                newSupplyProduct5.description = "";
                newSupplyProduct5.image_url = "";
                newSupplyProduct5.is_free_sample = false;
                newSupplyProduct5.oracle_code = "";
                newSupplyProduct5.IVCOUP = "";
                newSupplyProduct5.IVITEM = "CD2086";
                newSupplyProduct5.unit_cost = null;
                newSupplyProduct5.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct5.vendor_item_code = "CD2086";
                newSupplyProduct5.deleted = false;
                newSupplyProduct5.create_date = DateTime.Now;
                newSupplyProduct5.create_user_id = CREATE_USER_ID;
                newSupplyProduct5.update_date = DateTime.Now;
                newSupplyProduct5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct6 = new product();
                // newSupplyProduct6.product_id;                      // Autonumber generated on insert
                newSupplyProduct6.product_code = "CDBULK2306";
                newSupplyProduct6.product_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyProduct6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct6.nb_units = 1;                       // boxes per case
                newSupplyProduct6.nb_day_lead_time = 0;
                newSupplyProduct6.product_status_id = 101;            // 101 = Active
                newSupplyProduct6.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct6.business_division_id = 1;           // 1 = US
                newSupplyProduct6.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct6.coupon_id = null;
                newSupplyProduct6.description = "";
                newSupplyProduct6.image_url = "";
                newSupplyProduct6.is_free_sample = false;
                newSupplyProduct6.oracle_code = "";
                newSupplyProduct6.IVCOUP = "";
                newSupplyProduct6.IVITEM = "CDBULK2306";
                newSupplyProduct6.unit_cost = null;
                newSupplyProduct6.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct6.vendor_item_code = "CDBULK2306";
                newSupplyProduct6.deleted = false;
                newSupplyProduct6.create_date = DateTime.Now;
                newSupplyProduct6.create_user_id = CREATE_USER_ID;
                newSupplyProduct6.update_date = DateTime.Now;
                newSupplyProduct6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct7 = new product();
                // newSupplyProduct7.product_id;                      // Autonumber generated on insert
                newSupplyProduct7.product_code = "CD2093";
                newSupplyProduct7.product_name = "QSP Plastic Food Bags";
                newSupplyProduct7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct7.nb_units = 1;                       // boxes per case
                newSupplyProduct7.nb_day_lead_time = 0;
                newSupplyProduct7.product_status_id = 101;            // 101 = Active
                newSupplyProduct7.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct7.business_division_id = 1;           // 1 = US
                newSupplyProduct7.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct7.coupon_id = null;
                newSupplyProduct7.description = "";
                newSupplyProduct7.image_url = "";
                newSupplyProduct7.is_free_sample = false;
                newSupplyProduct7.oracle_code = "";
                newSupplyProduct7.IVCOUP = "";
                newSupplyProduct7.IVITEM = "CD2093";
                newSupplyProduct7.unit_cost = null;
                newSupplyProduct7.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct7.vendor_item_code = "CD2093";
                newSupplyProduct7.deleted = false;
                newSupplyProduct7.create_date = DateTime.Now;
                newSupplyProduct7.create_user_id = CREATE_USER_ID;
                newSupplyProduct7.update_date = DateTime.Now;
                newSupplyProduct7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newSupplyProduct9 = new product();
                // newSupplyProduct9.product_id;                      // Autonumber generated on insert
                newSupplyProduct9.product_code = "CD2103";
                newSupplyProduct9.product_name = "Just Right Cookie (Baking Instructions)";
                newSupplyProduct9.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyProduct9.nb_units = 1;                       // boxes per case
                newSupplyProduct9.nb_day_lead_time = 0;
                newSupplyProduct9.product_status_id = 101;            // 101 = Active
                newSupplyProduct9.product_type_id = 7;                // 6 = Cookies (cookie dough or frozen food)
                newSupplyProduct9.business_division_id = 1;           // 1 = US
                newSupplyProduct9.commission = Convert.ToDecimal(0);  // Calculated in as400
                newSupplyProduct9.coupon_id = null;
                newSupplyProduct9.description = "";
                newSupplyProduct9.image_url = "";
                newSupplyProduct9.is_free_sample = false;
                newSupplyProduct9.oracle_code = "";
                newSupplyProduct9.IVCOUP = "";
                newSupplyProduct9.IVITEM = "CD2103";
                newSupplyProduct9.unit_cost = null;
                newSupplyProduct9.vendor_id = 27;                     // 27 = prod, 28 = dev
                newSupplyProduct9.vendor_item_code = "CD2103";
                newSupplyProduct9.deleted = false;
                newSupplyProduct9.create_date = DateTime.Now;
                newSupplyProduct9.create_user_id = CREATE_USER_ID;
                newSupplyProduct9.update_date = DateTime.Now;
                newSupplyProduct9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.products.InsertOnSubmit(newSupplyProduct1);
                context.products.InsertOnSubmit(newSupplyProduct2);
                context.products.InsertOnSubmit(newSupplyProduct3);
                context.products.InsertOnSubmit(newSupplyProduct5);
                context.products.InsertOnSubmit(newSupplyProduct6);
                context.products.InsertOnSubmit(newSupplyProduct7);
                context.products.InsertOnSubmit(newSupplyProduct9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("products ok");

                #endregion

                #region Priced catalog

                // Create new record
                catalog newSupplyCatalogPriced = new catalog();
                // newSupplyCatalogPriced.newCatalog_id;          // Autonumber generated on insert
                newSupplyCatalogPriced.catalog_group_id = newSupplyCatalogGroup.catalog_group_id;
                newSupplyCatalogPriced.catalog_code = "MC57";
                newSupplyCatalogPriced.catalog_name = "Otis / Mag Voucher Spring 2011 Priced";
                newSupplyCatalogPriced.culture = "en-US";
                newSupplyCatalogPriced.description = "";
                newSupplyCatalogPriced.start_date = new DateTime(2010, 12, 8);;
                newSupplyCatalogPriced.end_date = new DateTime(2011, 6, 30);;
                newSupplyCatalogPriced.deleted = false;
                newSupplyCatalogPriced.create_date = DateTime.Now;
                newSupplyCatalogPriced.create_user_id = CREATE_USER_ID;
                newSupplyCatalogPriced.update_date = DateTime.Now;
                newSupplyCatalogPriced.update_user_id = CREATE_USER_ID;
                newSupplyCatalogPriced.is_priced = true;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newSupplyCatalogPriced);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newSupplyCatalogPriced.catalog_id.ToString());

                #endregion

                #region Priced catalog_item_category

                // Create new record
                catalog_item_category newSupplyCatalogItemCategoryPriced1 = new catalog_item_category();
                // newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryPriced1.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemCategoryPriced1.catalog_item_category_name = "Otis / Mag Voucher Spring 2011 Priced";
                newSupplyCatalogItemCategoryPriced1.parent_catalog_item_category_id = 0;
                newSupplyCatalogItemCategoryPriced1.deleted = false;
                newSupplyCatalogItemCategoryPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newSupplyCatalogItemCategoryPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newSupplyCatalogItemCategoryPriced1.catalog_item_category_id.ToString());

                #endregion

                #region Priced form_section

                // Create new record
                form_section newSupplyPAFormSectionPriced1 = new form_section();
                // newSupplyPAFormSectionPriced1.form_section_id;              // Autonumber generated on insert
                newSupplyPAFormSectionPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyPAFormSectionPriced1.form_id = newPAForm.form_id;
                newSupplyPAFormSectionPriced1.form_section_type_id = 2;        // 1 = standard product, 2 = supply product, 3 = optional product
                newSupplyPAFormSectionPriced1.form_section_number = 2;
                newSupplyPAFormSectionPriced1.form_section_title = "Otis / Mag Voucher Spring 2011 Priced";
                newSupplyPAFormSectionPriced1.description = "";
                newSupplyPAFormSectionPriced1.deleted = false;
                newSupplyPAFormSectionPriced1.create_date = DateTime.Now;
                newSupplyPAFormSectionPriced1.create_user_id = CREATE_USER_ID;
                newSupplyPAFormSectionPriced1.update_date = DateTime.Now;
                newSupplyPAFormSectionPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newSupplyPAFormSectionPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newSupplyPAFormSectionPriced1.form_section_id.ToString());

                #endregion

                #region Priced catalog_item

                #region Create new record

                catalog_item newSupplyCatalogItemPriced1 = new catalog_item();
                // newSupplyCatalogItemPriced1.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced1.product_id = newSupplyProduct1.product_id;
                newSupplyCatalogItemPriced1.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced1.catalog_item_code = "CDK2421";
                newSupplyCatalogItemPriced1.catalog_item_name = "Otis/Mag Bulk Brochure Priced";
                newSupplyCatalogItemPriced1.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced1.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced1.image_url = "";
                newSupplyCatalogItemPriced1.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced1.catalog_item_export_name = "";
                newSupplyCatalogItemPriced1.description = "";
                newSupplyCatalogItemPriced1.deleted = false;
                newSupplyCatalogItemPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced3 = new catalog_item();
                // newSupplyCatalogItemPriced3.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced3.product_id = newSupplyProduct3.product_id;
                newSupplyCatalogItemPriced3.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced3.catalog_item_code = "CD2062";
                newSupplyCatalogItemPriced3.catalog_item_name = "Otis Mass Display";
                newSupplyCatalogItemPriced3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced3.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced3.image_url = "";
                newSupplyCatalogItemPriced3.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced3.catalog_item_export_name = "";
                newSupplyCatalogItemPriced3.description = "";
                newSupplyCatalogItemPriced3.deleted = false;
                newSupplyCatalogItemPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced5 = new catalog_item();
                // newSupplyCatalogItemPriced5.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced5.product_id = newSupplyProduct5.product_id;
                newSupplyCatalogItemPriced5.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced5.catalog_item_code = "CD2086";
                newSupplyCatalogItemPriced5.catalog_item_name = "Otis Launch Poster";
                newSupplyCatalogItemPriced5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced5.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced5.image_url = "";
                newSupplyCatalogItemPriced5.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced5.catalog_item_export_name = "";
                newSupplyCatalogItemPriced5.description = "";
                newSupplyCatalogItemPriced5.deleted = false;
                newSupplyCatalogItemPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced6 = new catalog_item();
                // newSupplyCatalogItemPriced6.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced6.product_id = newSupplyProduct6.product_id;
                newSupplyCatalogItemPriced6.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced6.catalog_item_code = "CDBULK2306";
                newSupplyCatalogItemPriced6.catalog_item_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemPriced6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced6.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced6.image_url = "";
                newSupplyCatalogItemPriced6.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced6.catalog_item_export_name = "";
                newSupplyCatalogItemPriced6.description = "";
                newSupplyCatalogItemPriced6.deleted = false;
                newSupplyCatalogItemPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced7 = new catalog_item();
                // newSupplyCatalogItemPriced7.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced7.product_id = newSupplyProduct7.product_id;
                newSupplyCatalogItemPriced7.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced7.catalog_item_code = "CD2093";
                newSupplyCatalogItemPriced7.catalog_item_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemPriced7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced7.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced7.image_url = "";
                newSupplyCatalogItemPriced7.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced7.catalog_item_export_name = "";
                newSupplyCatalogItemPriced7.description = "";
                newSupplyCatalogItemPriced7.deleted = false;
                newSupplyCatalogItemPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemPriced9 = new catalog_item();
                // newSupplyCatalogItemPriced9.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemPriced9.product_id = newSupplyProduct9.product_id;
                newSupplyCatalogItemPriced9.catalog_id = newSupplyCatalogPriced.catalog_id;
                newSupplyCatalogItemPriced9.catalog_item_code = "CD2103";
                newSupplyCatalogItemPriced9.catalog_item_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemPriced9.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemPriced9.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemPriced9.image_url = "";
                newSupplyCatalogItemPriced9.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemPriced9.catalog_item_export_name = "";
                newSupplyCatalogItemPriced9.description = "";
                newSupplyCatalogItemPriced9.deleted = false;
                newSupplyCatalogItemPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced1);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced3);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced5);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced6);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced7);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region Priced catalog_item_detail

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced1 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced1.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced1.catalog_item_id = newSupplyCatalogItemPriced1.catalog_item_id;
                newSupplyCatalogItemDetailPriced1.catalog_item_detail_code = "CDK2421";
                newSupplyCatalogItemDetailPriced1.catalog_item_detail_name = "Otis/Mag Bulk Brochure Priced";
                newSupplyCatalogItemDetailPriced1.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced1.nb_units = 1;
                newSupplyCatalogItemDetailPriced1.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced1.term = 1;
                newSupplyCatalogItemDetailPriced1.description = "";
                newSupplyCatalogItemDetailPriced1.is_default = false;
                newSupplyCatalogItemDetailPriced1.deleted = false;
                newSupplyCatalogItemDetailPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced3 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced3.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced3.catalog_item_id = newSupplyCatalogItemPriced3.catalog_item_id;
                newSupplyCatalogItemDetailPriced3.catalog_item_detail_code = "CD2062";
                newSupplyCatalogItemDetailPriced3.catalog_item_detail_name = "Otis Mass Display";
                newSupplyCatalogItemDetailPriced3.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced3.nb_units = 1;
                newSupplyCatalogItemDetailPriced3.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced3.term = 1;
                newSupplyCatalogItemDetailPriced3.description = "";
                newSupplyCatalogItemDetailPriced3.is_default = false;
                newSupplyCatalogItemDetailPriced3.deleted = false;
                newSupplyCatalogItemDetailPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced5 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced5.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced5.catalog_item_id = newSupplyCatalogItemPriced5.catalog_item_id;
                newSupplyCatalogItemDetailPriced5.catalog_item_detail_code = "CD2086";
                newSupplyCatalogItemDetailPriced5.catalog_item_detail_name = "Otis Launch Poster";
                newSupplyCatalogItemDetailPriced5.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced5.nb_units = 1;
                newSupplyCatalogItemDetailPriced5.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced5.term = 1;
                newSupplyCatalogItemDetailPriced5.description = "";
                newSupplyCatalogItemDetailPriced5.is_default = false;
                newSupplyCatalogItemDetailPriced5.deleted = false;
                newSupplyCatalogItemDetailPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced6 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced6.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced6.catalog_item_id = newSupplyCatalogItemPriced6.catalog_item_id;
                newSupplyCatalogItemDetailPriced6.catalog_item_detail_code = "CDBULK2306";
                newSupplyCatalogItemDetailPriced6.catalog_item_detail_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemDetailPriced6.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced6.nb_units = 1;
                newSupplyCatalogItemDetailPriced6.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced6.term = 1;
                newSupplyCatalogItemDetailPriced6.description = "";
                newSupplyCatalogItemDetailPriced6.is_default = false;
                newSupplyCatalogItemDetailPriced6.deleted = false;
                newSupplyCatalogItemDetailPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced7 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced7.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced7.catalog_item_id = newSupplyCatalogItemPriced7.catalog_item_id;
                newSupplyCatalogItemDetailPriced7.catalog_item_detail_code = "CD2093";
                newSupplyCatalogItemDetailPriced7.catalog_item_detail_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemDetailPriced7.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced7.nb_units = 1;
                newSupplyCatalogItemDetailPriced7.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced7.term = 1;
                newSupplyCatalogItemDetailPriced7.description = "";
                newSupplyCatalogItemDetailPriced7.is_default = false;
                newSupplyCatalogItemDetailPriced7.deleted = false;
                newSupplyCatalogItemDetailPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailPriced9 = new catalog_item_detail();
                // newSupplyCatalogItemDetailPriced9.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailPriced9.catalog_item_id = newSupplyCatalogItemPriced9.catalog_item_id;
                newSupplyCatalogItemDetailPriced9.catalog_item_detail_code = "CD2103";
                newSupplyCatalogItemDetailPriced9.catalog_item_detail_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemDetailPriced9.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailPriced9.nb_units = 1;
                newSupplyCatalogItemDetailPriced9.profit_rate = 0.00;
                newSupplyCatalogItemDetailPriced9.term = 1;
                newSupplyCatalogItemDetailPriced9.description = "";
                newSupplyCatalogItemDetailPriced9.is_default = false;
                newSupplyCatalogItemDetailPriced9.deleted = false;
                newSupplyCatalogItemDetailPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemDetailPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced1);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced3);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced5);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced6);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced7);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region Priced catalog_item_category_catalog_item

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced1 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced1.catalog_item_id = newSupplyCatalogItemPriced1.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced1.display_order = 1;
                newSupplyCatalogItemCategoryCatalogItemPriced1.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced3 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced3.catalog_item_id = newSupplyCatalogItemPriced3.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced3.display_order = 4;
                newSupplyCatalogItemCategoryCatalogItemPriced3.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced5 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced5.catalog_item_id = newSupplyCatalogItemPriced5.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced5.display_order = 2;
                newSupplyCatalogItemCategoryCatalogItemPriced5.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced6 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced6.catalog_item_id = newSupplyCatalogItemPriced6.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced6.display_order = 3;
                newSupplyCatalogItemCategoryCatalogItemPriced6.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced7 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced7.catalog_item_id = newSupplyCatalogItemPriced7.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced7.display_order = 5;
                newSupplyCatalogItemCategoryCatalogItemPriced7.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemPriced9 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemPriced9.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemPriced9.catalog_item_category_id = newSupplyCatalogItemCategoryPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemPriced9.catalog_item_id = newSupplyCatalogItemPriced9.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemPriced9.display_order = 6;
                newSupplyCatalogItemCategoryCatalogItemPriced9.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced1);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced3);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced5);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced6);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced7);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #region UnPriced catalog

                // Create new record
                catalog newSupplyCatalogUnPriced = new catalog();
                // newSupplyCatalogUnPriced.newCatalog_id;          // Autonumber generated on insert
                newSupplyCatalogUnPriced.catalog_group_id = newSupplyCatalogGroup.catalog_group_id;
                newSupplyCatalogUnPriced.catalog_code = "MC57";
                newSupplyCatalogUnPriced.catalog_name = "Otis / Mag Voucher Spring 2011 UnPriced";
                newSupplyCatalogUnPriced.culture = "en-US";
                newSupplyCatalogUnPriced.description = "";
                newSupplyCatalogUnPriced.start_date = new DateTime(2010, 12, 8);;
                newSupplyCatalogUnPriced.end_date = new DateTime(2011, 6, 30);;
                newSupplyCatalogUnPriced.deleted = false;
                newSupplyCatalogUnPriced.create_date = DateTime.Now;
                newSupplyCatalogUnPriced.create_user_id = CREATE_USER_ID;
                newSupplyCatalogUnPriced.update_date = DateTime.Now;
                newSupplyCatalogUnPriced.update_user_id = CREATE_USER_ID;
                newSupplyCatalogUnPriced.is_priced = false;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newSupplyCatalogUnPriced);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newSupplyCatalogUnPriced.catalog_id.ToString());

                #endregion

                #region UnPriced catalog_item_category

                // Create new record
                catalog_item_category newSupplyCatalogItemCategoryUnPriced1 = new catalog_item_category();
                // newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryUnPriced1.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_name = "Otis / Mag Voucher Spring 2011 UnPriced";
                newSupplyCatalogItemCategoryUnPriced1.parent_catalog_item_category_id = 0;
                newSupplyCatalogItemCategoryUnPriced1.deleted = false;
                newSupplyCatalogItemCategoryUnPriced1.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryUnPriced1.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryUnPriced1.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryUnPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newSupplyCatalogItemCategoryUnPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id.ToString());

                #endregion

                #region UnPriced form_section

                // Create new record
                form_section newSupplyPAFormSectionUnPriced1 = new form_section();
                // newSupplyPAFormSectionUnPriced1.form_section_id;              // Autonumber generated on insert
                newSupplyPAFormSectionUnPriced1.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyPAFormSectionUnPriced1.form_id = newPAForm.form_id;
                newSupplyPAFormSectionUnPriced1.form_section_type_id = 2;        // 1 = standard product, 2 = supply product, 3 = optional product
                newSupplyPAFormSectionUnPriced1.form_section_number = 2;
                newSupplyPAFormSectionUnPriced1.form_section_title = "Otis / Mag Voucher Spring 2011 UnPriced";
                newSupplyPAFormSectionUnPriced1.description = "";
                newSupplyPAFormSectionUnPriced1.deleted = false;
                newSupplyPAFormSectionUnPriced1.create_date = DateTime.Now;
                newSupplyPAFormSectionUnPriced1.create_user_id = CREATE_USER_ID;
                newSupplyPAFormSectionUnPriced1.update_date = DateTime.Now;
                newSupplyPAFormSectionUnPriced1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newSupplyPAFormSectionUnPriced1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newSupplyPAFormSectionUnPriced1.form_section_id.ToString());

                #endregion

                #region UnPriced catalog_item

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced2 = new catalog_item();
                // newSupplyCatalogItemUnPriced2.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced2.product_id = newSupplyProduct2.product_id;
                newSupplyCatalogItemUnPriced2.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced2.catalog_item_code = "CDK2422";
                newSupplyCatalogItemUnPriced2.catalog_item_name = "Otis/Mag Bulk Brochure Un-priced";
                newSupplyCatalogItemUnPriced2.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced2.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced2.image_url = "";
                newSupplyCatalogItemUnPriced2.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced2.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced2.description = "";
                newSupplyCatalogItemUnPriced2.deleted = false;
                newSupplyCatalogItemUnPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced3 = new catalog_item();
                // newSupplyCatalogItemUnPriced3.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced3.product_id = newSupplyProduct3.product_id;
                newSupplyCatalogItemUnPriced3.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced3.catalog_item_code = "CD2062";
                newSupplyCatalogItemUnPriced3.catalog_item_name = "Otis Mass Display";
                newSupplyCatalogItemUnPriced3.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced3.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced3.image_url = "";
                newSupplyCatalogItemUnPriced3.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced3.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced3.description = "";
                newSupplyCatalogItemUnPriced3.deleted = false;
                newSupplyCatalogItemUnPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced5 = new catalog_item();
                // newSupplyCatalogItemUnPriced5.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced5.product_id = newSupplyProduct5.product_id;
                newSupplyCatalogItemUnPriced5.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced5.catalog_item_code = "CD2086";
                newSupplyCatalogItemUnPriced5.catalog_item_name = "Otis Launch Poster";
                newSupplyCatalogItemUnPriced5.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced5.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced5.image_url = "";
                newSupplyCatalogItemUnPriced5.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced5.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced5.description = "";
                newSupplyCatalogItemUnPriced5.deleted = false;
                newSupplyCatalogItemUnPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced6 = new catalog_item();
                // newSupplyCatalogItemUnPriced6.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced6.product_id = newSupplyProduct6.product_id;
                newSupplyCatalogItemUnPriced6.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced6.catalog_item_code = "CDBULK2306";
                newSupplyCatalogItemUnPriced6.catalog_item_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemUnPriced6.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced6.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced6.image_url = "";
                newSupplyCatalogItemUnPriced6.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced6.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced6.description = "";
                newSupplyCatalogItemUnPriced6.deleted = false;
                newSupplyCatalogItemUnPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced7 = new catalog_item();
                // newSupplyCatalogItemUnPriced7.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced7.product_id = newSupplyProduct7.product_id;
                newSupplyCatalogItemUnPriced7.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced7.catalog_item_code = "CD2093";
                newSupplyCatalogItemUnPriced7.catalog_item_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemUnPriced7.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced7.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced7.image_url = "";
                newSupplyCatalogItemUnPriced7.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced7.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced7.description = "";
                newSupplyCatalogItemUnPriced7.deleted = false;
                newSupplyCatalogItemUnPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newSupplyCatalogItemUnPriced9 = new catalog_item();
                // newSupplyCatalogItemUnPriced9.catalog_item_id;                  // Autonumber generated on insert
                newSupplyCatalogItemUnPriced9.product_id = newSupplyProduct9.product_id;
                newSupplyCatalogItemUnPriced9.catalog_id = newSupplyCatalogUnPriced.catalog_id;
                newSupplyCatalogItemUnPriced9.catalog_item_code = "CD2103";
                newSupplyCatalogItemUnPriced9.catalog_item_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemUnPriced9.price = Convert.ToDecimal(0);      // Retail case cost
                newSupplyCatalogItemUnPriced9.nb_units = 1;                       // Boxes per case
                newSupplyCatalogItemUnPriced9.image_url = "";
                newSupplyCatalogItemUnPriced9.catalog_item_status_id = 101;        // 101 = Active
                newSupplyCatalogItemUnPriced9.catalog_item_export_name = "";
                newSupplyCatalogItemUnPriced9.description = "";
                newSupplyCatalogItemUnPriced9.deleted = false;
                newSupplyCatalogItemUnPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemUnPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemUnPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemUnPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                //context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced1);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced2);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced3);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced5);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced6);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced7);
                context.catalog_items.InsertOnSubmit(newSupplyCatalogItemUnPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region UnPriced catalog_item_detail

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced2 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced2.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced2.catalog_item_id = newSupplyCatalogItemUnPriced2.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced2.catalog_item_detail_code = "CDK2422";
                newSupplyCatalogItemDetailUnPriced2.catalog_item_detail_name = "Otis/Mag Bulk Brochure Un-priced";
                newSupplyCatalogItemDetailUnPriced2.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced2.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced2.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced2.term = 1;
                newSupplyCatalogItemDetailUnPriced2.description = "";
                newSupplyCatalogItemDetailUnPriced2.is_default = false;
                newSupplyCatalogItemDetailUnPriced2.deleted = false;
                newSupplyCatalogItemDetailUnPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced3 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced3.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced3.catalog_item_id = newSupplyCatalogItemUnPriced3.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced3.catalog_item_detail_code = "CD2062";
                newSupplyCatalogItemDetailUnPriced3.catalog_item_detail_name = "Otis Mass Display";
                newSupplyCatalogItemDetailUnPriced3.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced3.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced3.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced3.term = 1;
                newSupplyCatalogItemDetailUnPriced3.description = "";
                newSupplyCatalogItemDetailUnPriced3.is_default = false;
                newSupplyCatalogItemDetailUnPriced3.deleted = false;
                newSupplyCatalogItemDetailUnPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced5 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced5.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced5.catalog_item_id = newSupplyCatalogItemUnPriced5.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced5.catalog_item_detail_code = "CD2086";
                newSupplyCatalogItemDetailUnPriced5.catalog_item_detail_name = "Otis Launch Poster";
                newSupplyCatalogItemDetailUnPriced5.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced5.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced5.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced5.term = 1;
                newSupplyCatalogItemDetailUnPriced5.description = "";
                newSupplyCatalogItemDetailUnPriced5.is_default = false;
                newSupplyCatalogItemDetailUnPriced5.deleted = false;
                newSupplyCatalogItemDetailUnPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced6 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced6.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced6.catalog_item_id = newSupplyCatalogItemUnPriced6.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced6.catalog_item_detail_code = "CDBULK2306";
                newSupplyCatalogItemDetailUnPriced6.catalog_item_detail_name = "Otis Bulk Sponsor Guide (new)";
                newSupplyCatalogItemDetailUnPriced6.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced6.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced6.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced6.term = 1;
                newSupplyCatalogItemDetailUnPriced6.description = "";
                newSupplyCatalogItemDetailUnPriced6.is_default = false;
                newSupplyCatalogItemDetailUnPriced6.deleted = false;
                newSupplyCatalogItemDetailUnPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced7 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced7.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced7.catalog_item_id = newSupplyCatalogItemUnPriced7.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced7.catalog_item_detail_code = "CD2093";
                newSupplyCatalogItemDetailUnPriced7.catalog_item_detail_name = "QSP Plastic Food Bags";
                newSupplyCatalogItemDetailUnPriced7.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced7.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced7.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced7.term = 1;
                newSupplyCatalogItemDetailUnPriced7.description = "";
                newSupplyCatalogItemDetailUnPriced7.is_default = false;
                newSupplyCatalogItemDetailUnPriced7.deleted = false;
                newSupplyCatalogItemDetailUnPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newSupplyCatalogItemDetailUnPriced9 = new catalog_item_detail();
                // newSupplyCatalogItemDetailUnPriced9.catalog_item_detail_id;                 // Autonumber generated on insert
                newSupplyCatalogItemDetailUnPriced9.catalog_item_id = newSupplyCatalogItemUnPriced9.catalog_item_id;
                newSupplyCatalogItemDetailUnPriced9.catalog_item_detail_code = "CD2103";
                newSupplyCatalogItemDetailUnPriced9.catalog_item_detail_name = "Just Right Cookie (Baking Instructions)";
                newSupplyCatalogItemDetailUnPriced9.price = Convert.ToDecimal(0);
                newSupplyCatalogItemDetailUnPriced9.nb_units = 1;
                newSupplyCatalogItemDetailUnPriced9.profit_rate = 0.00;
                newSupplyCatalogItemDetailUnPriced9.term = 1;
                newSupplyCatalogItemDetailUnPriced9.description = "";
                newSupplyCatalogItemDetailUnPriced9.is_default = false;
                newSupplyCatalogItemDetailUnPriced9.deleted = false;
                newSupplyCatalogItemDetailUnPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemDetailUnPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemDetailUnPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced2);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced3);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced5);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced6);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced7);
                context.catalog_item_details.InsertOnSubmit(newSupplyCatalogItemDetailUnPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region UnPriced catalog_item_category_catalog_item

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced2 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced2.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.catalog_item_id = newSupplyCatalogItemUnPriced2.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.display_order = 1;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced3 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.catalog_item_id = newSupplyCatalogItemUnPriced3.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.display_order = 4;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced3.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced5 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced5.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.catalog_item_id = newSupplyCatalogItemUnPriced5.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.display_order = 2;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced5.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced6 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced6.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.catalog_item_id = newSupplyCatalogItemUnPriced6.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.display_order = 3;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced6.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced7 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced7.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.catalog_item_id = newSupplyCatalogItemUnPriced7.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.display_order = 5;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced7.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newSupplyCatalogItemCategoryCatalogItemUnPriced9 = new catalog_item_category_catalog_item();
                // newSupplyCatalogItemCategoryCatalogItemUnPriced9.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.catalog_item_category_id = newSupplyCatalogItemCategoryUnPriced1.catalog_item_category_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.catalog_item_id = newSupplyCatalogItemUnPriced9.catalog_item_id;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.display_order = 6;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.deleted = false;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.create_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.create_user_id = CREATE_USER_ID;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.update_date = DateTime.Now;
                newSupplyCatalogItemCategoryCatalogItemUnPriced9.update_user_id = CREATE_USER_ID;

                #endregion

                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced2);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced3);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced5);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced6);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced7);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newSupplyCatalogItemCategoryCatalogItemUnPriced9);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #endregion

                #endregion

                #region Order form requires PA form

                form_requires_form formRequiresForm = new form_requires_form();

                formRequiresForm.form_id = newForm.form_id;
                formRequiresForm.required_form_id = newPAForm.form_id; 
                formRequiresForm.deleted = false;
                formRequiresForm.create_date = DateTime.Now;
                formRequiresForm.create_user_id = CREATE_USER_ID;
                formRequiresForm.update_date = DateTime.Now;
                formRequiresForm.update_user_id = CREATE_USER_ID;

                context.form_requires_forms.InsertOnSubmit(formRequiresForm);
                context.SubmitChanges();

                sb.AppendLine("New form required form id = " + formRequiresForm.form_requires_form_id.ToString());

                #endregion

                sb.AppendLine("Otis / Mag Voucher Spring 2011 - Success!");
            }
            catch (Exception ex)
            {
                sb.AppendLine("transaction rollback due to error");
                sb.AppendLine("Message: " + ex.Message);
                sb.AppendLine("Source: " + ex.Source);
                sb.AppendLine("");
                sb.AppendLine(ex.StackTrace);
            }
            finally
            {
                if (context.Connection != null)
                {
                    context.Connection.Close();
                }
            }

            sb.AppendLine("end");

            return sb.ToString();
        }

        #endregion

        #region New Voucher Program

        public string AddNewVoucherProgramSpring2011Form()
        {
            #region Start

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("start");

            // Open db context
            QSPFulfillmentDataContext context = new QSPFulfillmentDataContext(CONNECTION_STRING);
            sb.AppendLine("db context open");

            // Open the connection
            context.Connection.Open();

            #endregion

            try
            {
                #region Order form

                #region form_group

                // Create new form group object
                form_group newFormGroup = new form_group();

                // Fill in new form group data
                // newFormGroup.form_group_id;              // Autonumber generated on insert
                newFormGroup.form_group_name = "New Voucher Program Spring 2011 stock order form group";
                newFormGroup.deleted = false;
                newFormGroup.create_date = DateTime.Now;
                newFormGroup.create_user_id = CREATE_USER_ID;
                newFormGroup.update_date = DateTime.Now;
                newFormGroup.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.form_groups.InsertOnSubmit(newFormGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form group id = " + newFormGroup.form_group_id.ToString());
                // MessageBox.Show("New form group id = " + newFormGroup.form_group_id.ToString());

                #endregion

                #region form

                // Create new form object
                form newForm = new form();

                // Fill in new form data
                // newForm.form_id;                             // Autonumber generated on insert
                newForm.form_group_id = newFormGroup.form_group_id;
                newForm.entity_type_id = 4;                     // Type 4 = order
                newForm.form_code = "NewVoucherProgramSpring2011";
                newForm.form_name = "New Voucher Program Spring 2011";
                newForm.description = "";
                newForm.order_terms_text = "";
                newForm.start_date = new DateTime(2010, 11, 17);
                newForm.end_date = new DateTime(2011, 7, 30);
                newForm.closing_time = new DateTime(2011, 7, 30);
                newForm.image_url = "images/CatalogItem/NewVoucherProgram.jpg";
                newForm.is_base_form = false;
                newForm.parent_form_id = 8;                     // Base form for orders in prod and dev
                newForm.is_product_price_updatable = false;
                newForm.is_quantity_adjustment_allowed = true;
                newForm.tax_postal_address_type_id = 2;
                newForm.enabled = true;
                newForm.deleted = false;
                newForm.version = 1;
                //newForm.program_id = null;                    // Does not belong to Otis / Pine Valley
                newForm.program_type_id = 7;
                newForm.program_basics_text = "";
                newForm.create_date = DateTime.Now;
                newForm.create_user_id = CREATE_USER_ID;
                newForm.update_date = DateTime.Now;
                newForm.update_user_id = CREATE_USER_ID;
                //newForm.warehouse_type_id;
                newForm.is_bulk = false;
                newForm.report_name = "OrderForm";
                newForm.is_warehouse_selectable = true;
                newForm.default_warehouse_id = 9;
                newForm.season_id = 8;

                // Add the new form to the db context
                context.forms.InsertOnSubmit(newForm);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New form id = " + newForm.form_id.ToString());
                // MessageBox.Show("New form id = " + newForm.form_id.ToString());

                #endregion

                #region form_permission

                form_permission newFormPermission1 = new form_permission();
                //newFormPermission1.form_permission_id;            // Autonumber generated on insert
                newFormPermission1.form_id = newForm.form_id;
                newFormPermission1.role_id = 0;                     // User
                newFormPermission1.allow_read = true;
                newFormPermission1.allow_write = true;
                newFormPermission1.create_date = DateTime.Now;
                newFormPermission1.create_user_id = CREATE_USER_ID;
                newFormPermission1.update_date = DateTime.Now;
                newFormPermission1.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission2 = new form_permission();
                //newFormPermission2.form_permission_id;            // Autonumber generated on insert
                newFormPermission2.form_id = newForm.form_id;
                newFormPermission2.role_id = 1;                     // Field Sale Manager (FM)
                newFormPermission2.allow_read = true;
                newFormPermission2.allow_write = true;
                newFormPermission2.create_date = DateTime.Now;
                newFormPermission2.create_user_id = CREATE_USER_ID;
                newFormPermission2.update_date = DateTime.Now;
                newFormPermission2.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission3 = new form_permission();
                //newFormPermission3.form_permission_id;            // Autonumber generated on insert
                newFormPermission3.form_id = newForm.form_id;
                newFormPermission3.role_id = 2;                     // Field Support
                newFormPermission3.allow_read = true;
                newFormPermission3.allow_write = true;
                newFormPermission3.create_date = DateTime.Now;
                newFormPermission3.create_user_id = CREATE_USER_ID;
                newFormPermission3.update_date = DateTime.Now;
                newFormPermission3.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission4 = new form_permission();
                //newFormPermission4.form_permission_id;            // Autonumber generated on insert
                newFormPermission4.form_id = newForm.form_id;
                newFormPermission4.role_id = 3;                     // Accounting Manager
                newFormPermission4.allow_read = true;
                newFormPermission4.allow_write = true;
                newFormPermission4.create_date = DateTime.Now;
                newFormPermission4.create_user_id = CREATE_USER_ID;
                newFormPermission4.update_date = DateTime.Now;
                newFormPermission4.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission5 = new form_permission();
                //newFormPermission5.form_permission_id;            // Autonumber generated on insert
                newFormPermission5.form_id = newForm.form_id;
                newFormPermission5.role_id = 4;                     // Admin
                newFormPermission5.allow_read = true;
                newFormPermission5.allow_write = true;
                newFormPermission5.create_date = DateTime.Now;
                newFormPermission5.create_user_id = CREATE_USER_ID;
                newFormPermission5.update_date = DateTime.Now;
                newFormPermission5.update_user_id = CREATE_USER_ID;

                form_permission newFormPermission6 = new form_permission();
                //newFormPermission6.form_permission_id;            // Autonumber generated on insert
                newFormPermission6.form_id = newForm.form_id;
                newFormPermission6.role_id = 5;                     // Super User
                newFormPermission6.allow_read = true;
                newFormPermission6.allow_write = true;
                newFormPermission6.create_date = DateTime.Now;
                newFormPermission6.create_user_id = CREATE_USER_ID;
                newFormPermission6.update_date = DateTime.Now;
                newFormPermission6.update_user_id = CREATE_USER_ID;

                // Add the new form permissions to the db context
                context.form_permissions.InsertOnSubmit(newFormPermission1);
                context.form_permissions.InsertOnSubmit(newFormPermission2);
                context.form_permissions.InsertOnSubmit(newFormPermission3);
                context.form_permissions.InsertOnSubmit(newFormPermission4);
                context.form_permissions.InsertOnSubmit(newFormPermission5);
                context.form_permissions.InsertOnSubmit(newFormPermission6);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form permissions ok");

                #endregion

                #region form_delivery_date_type

                form_delivery_date_type newFormDeliveryDateType1 = new form_delivery_date_type();
                //newFormDeliveryDateType1.form_delivery_date_type_id;  // Autonumber generated on insert
                newFormDeliveryDateType1.form_id = newForm.form_id;
                newFormDeliveryDateType1.delivery_date_type_id = 1;     // Choose a date

                context.form_delivery_date_types.InsertOnSubmit(newFormDeliveryDateType1);
                context.SubmitChanges();

                sb.AppendLine("form delivery date type ok");

                #endregion

                #region form_profit_rate

                form_profit_rate newFormProfitRate1 = new form_profit_rate();
                //newFormProfitRate1.form_profit_rate_id;                // Autonumber generated on insert
                newFormProfitRate1.form_id = newForm.form_id;
                newFormProfitRate1.profit_rate_id = 1;         // 1 = 40%, 2 = 45%, 3 = 50%
                newFormProfitRate1.deleted = false;
                newFormProfitRate1.create_date = DateTime.Now;
                newFormProfitRate1.create_user_id = CREATE_USER_ID;
                newFormProfitRate1.update_date = DateTime.Now;
                newFormProfitRate1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_profit_rates.InsertOnSubmit(newFormProfitRate1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form profit rate ok");

                #endregion

                #region form_order_type

                // Create new form order type object
                form_order_type newFormOrderType1 = new form_order_type();
                // newFormOrderType1.form_order_type_id;            // Autonumber generated on insert
                newFormOrderType1.form_id = newForm.form_id;
                newFormOrderType1.order_type_id = 1;                // 1 = standard order
                newFormOrderType1.deleted = false;
                newFormOrderType1.create_date = DateTime.Now;
                newFormOrderType1.create_user_id = CREATE_USER_ID;
                newFormOrderType1.update_date = DateTime.Now;
                newFormOrderType1.update_user_id = CREATE_USER_ID;

                // Add the new form order types to the db context
                context.form_order_types.InsertOnSubmit(newFormOrderType1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form order type ok");

                #endregion

                #region form_delivery_method

                // Create new record
                form_delivery_method newFormDeliveryMethod1 = new form_delivery_method();
                // newFormDeliveryMethod1.form_delivery_method_id;          // Autonumber generated on insert
                newFormDeliveryMethod1.delivery_method_id = 1;              // Common carrier
                newFormDeliveryMethod1.form_id = newForm.form_id;
                newFormDeliveryMethod1.deleted = false;
                newFormDeliveryMethod1.create_date = DateTime.Now;
                newFormDeliveryMethod1.create_user_id = CREATE_USER_ID;
                newFormDeliveryMethod1.update_date = DateTime.Now;
                newFormDeliveryMethod1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_delivery_methods.InsertOnSubmit(newFormDeliveryMethod1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form delivery methods ok");

                #endregion

                #region catalog_group

                // Create new record
                catalog_group newCatalogGroup = new catalog_group();
                // newCatalogGroup.catalog_group_id;                // Autonumber generated on insert
                newCatalogGroup.catalog_group_code = "NewVoucherProgramSpring2011";
                newCatalogGroup.catalog_group_name = "New Voucher Program Spring 2011 order form";
                newCatalogGroup.description = "";
                newCatalogGroup.deleted = false;
                newCatalogGroup.create_date = DateTime.Now;
                newCatalogGroup.create_user_id = CREATE_USER_ID;
                newCatalogGroup.update_date = DateTime.Now;
                newCatalogGroup.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_groups.InsertOnSubmit(newCatalogGroup);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog group id = " + newCatalogGroup.catalog_group_id.ToString());

                #endregion

                #region catalog

                // Create new record
                catalog newCatalog = new catalog();
                // newCatalog.newCatalog_id;          // Autonumber generated on insert
                newCatalog.catalog_group_id = newCatalogGroup.catalog_group_id;
                newCatalog.catalog_code = "NewVoucherProgramSpring2011";
                newCatalog.catalog_name = "New Voucher Program Spring 2011 order form";
                newCatalog.culture = "en-US";
                newCatalog.description = "";
                newCatalog.start_date = new DateTime(2010, 11, 17);
                newCatalog.end_date = new DateTime(2011, 6, 30);
                newCatalog.deleted = false;
                newCatalog.create_date = DateTime.Now;
                newCatalog.create_user_id = CREATE_USER_ID;
                newCatalog.update_date = DateTime.Now;
                newCatalog.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalogs.InsertOnSubmit(newCatalog);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog id = " + newCatalog.catalog_id.ToString());

                #endregion

                #region catalog_item_category

                // Create new record
                catalog_item_category newCatalogItemCategory1 = new catalog_item_category();
                // newCatalogItemCategory1.catalog_item_category_id;         // Autonumber generated on insert
                newCatalogItemCategory1.catalog_id = newCatalog.catalog_id;
                newCatalogItemCategory1.catalog_item_category_name = "New Voucher Program Spring 2011";
                newCatalogItemCategory1.parent_catalog_item_category_id = 0;
                newCatalogItemCategory1.deleted = false;
                newCatalogItemCategory1.create_date = DateTime.Now;
                newCatalogItemCategory1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategory1.update_date = DateTime.Now;
                newCatalogItemCategory1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.catalog_item_categories.InsertOnSubmit(newCatalogItemCategory1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("new catalog item category id = " + newCatalogItemCategory1.catalog_item_category_id.ToString());

                #endregion

                #region form_section

                // Create new record
                form_section newFormSection1 = new form_section();
                // newFormSection1.form_section_id;              // Autonumber generated on insert
                newFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newFormSection1.form_id = newForm.form_id;
                newFormSection1.form_section_type_id = 1;        // 1 = standard product, 2 = supply product, 3 = optional product
                newFormSection1.form_section_number = 1;
                newFormSection1.form_section_title = "New Voucher Program Spring 2011";
                newFormSection1.description = "";
                newFormSection1.deleted = false;
                newFormSection1.create_date = DateTime.Now;
                newFormSection1.create_user_id = CREATE_USER_ID;
                newFormSection1.update_date = DateTime.Now;
                newFormSection1.update_user_id = CREATE_USER_ID;

                // Add the new recotds to the db context
                context.form_sections.InsertOnSubmit(newFormSection1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("form section id = " + newFormSection1.form_section_id.ToString());

                #endregion

                #region product

                #region Create new record

                product newProduct1 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct1.product_code = "52510";
                newProduct1.product_name = "WFC MAGAZINE VOUCHER $10";
                newProduct1.price = Convert.ToDecimal(0.00);
                newProduct1.nb_units = 1;
                newProduct1.nb_day_lead_time = 10;
                newProduct1.product_status_id = 101;            // 101 = Active
                newProduct1.product_type_id = 17;               // 17 = Mag Voucher
                newProduct1.business_division_id = 1;           // 1 = US
                newProduct1.commission = Convert.ToDecimal(0.00);                  
                newProduct1.coupon_id = null;
                newProduct1.description = "";
                newProduct1.image_url = "";
                newProduct1.is_free_sample = false;
                newProduct1.oracle_code = "";
                newProduct1.IVCOUP = "";
                newProduct1.IVITEM = "52510";
                newProduct1.unit_cost = Convert.ToDecimal(0.00);
                newProduct1.vendor_id = 46;
                newProduct1.vendor_item_code = "52510";
                newProduct1.deleted = false;
                newProduct1.create_date = DateTime.Now;
                newProduct1.create_user_id = CREATE_USER_ID;
                newProduct1.update_date = DateTime.Now;
                newProduct1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct2 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct2.product_code = "52520";
                newProduct2.product_name = "WFC MAGAZINE VOUCHER $20";
                newProduct2.price = Convert.ToDecimal(0.00);
                newProduct2.nb_units = 1;
                newProduct2.nb_day_lead_time = 10;
                newProduct2.product_status_id = 101;            // 101 = Active
                newProduct2.product_type_id = 17;               // 17 = Mag Voucher
                newProduct2.business_division_id = 1;           // 1 = US
                newProduct2.commission = Convert.ToDecimal(0.00);                  
                newProduct2.coupon_id = null;
                newProduct2.description = "";
                newProduct2.image_url = "";
                newProduct2.is_free_sample = false;
                newProduct2.oracle_code = "";
                newProduct2.IVCOUP = "";
                newProduct2.IVITEM = "52520";
                newProduct2.unit_cost = Convert.ToDecimal(0.00);
                newProduct2.vendor_id = 46;
                newProduct2.vendor_item_code = "52520";
                newProduct2.deleted = false;
                newProduct2.create_date = DateTime.Now;
                newProduct2.create_user_id = CREATE_USER_ID;
                newProduct2.update_date = DateTime.Now;
                newProduct2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                product newProduct3 = new product();
                // newProduct1.product_id;                      // Autonumber generated on insert
                newProduct3.product_code = "52530";
                newProduct3.product_name = "WFC MAGAZINE VOUCHER $30";
                newProduct3.price = Convert.ToDecimal(0.00);
                newProduct3.nb_units = 1;
                newProduct3.nb_day_lead_time = 10;
                newProduct3.product_status_id = 101;            // 101 = Active
                newProduct3.product_type_id = 17;               // 17 = Mag Voucher
                newProduct3.business_division_id = 1;           // 1 = US
                newProduct3.commission = Convert.ToDecimal(0.00);                   
                newProduct3.coupon_id = null;
                newProduct3.description = "";
                newProduct3.image_url = "";
                newProduct3.is_free_sample = false;
                newProduct3.oracle_code = "";
                newProduct3.IVCOUP = "";
                newProduct3.IVITEM = "52530";
                newProduct3.unit_cost = Convert.ToDecimal(0.00);
                newProduct3.vendor_id = 46;
                newProduct3.vendor_item_code = "52530";
                newProduct3.deleted = false;
                newProduct3.create_date = DateTime.Now;
                newProduct3.create_user_id = CREATE_USER_ID;
                newProduct3.update_date = DateTime.Now;
                newProduct3.update_user_id = CREATE_USER_ID;

                #endregion

                

                // Add the new recotds to the db context
                context.products.InsertOnSubmit(newProduct1);
                context.products.InsertOnSubmit(newProduct2);
                context.products.InsertOnSubmit(newProduct3);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("products ok");

                #endregion

                #region catalog_item

                #region Create new record

                catalog_item newCatalogItem1 = new catalog_item();
                // newCatalogItem1.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem1.product_id = newProduct1.product_id;
                newCatalogItem1.catalog_id = newCatalog.catalog_id;
                newCatalogItem1.catalog_item_code = "52510";
                newCatalogItem1.catalog_item_name = "WFC MAGAZINE VOUCHER $10";
                newCatalogItem1.price = Convert.ToDecimal(0.00);
                newCatalogItem1.nb_units = 1;
                newCatalogItem1.image_url = "1";
                newCatalogItem1.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem1.catalog_item_export_name = "";
                newCatalogItem1.description = "";
                newCatalogItem1.deleted = false;
                newCatalogItem1.create_date = DateTime.Now;
                newCatalogItem1.create_user_id = CREATE_USER_ID;
                newCatalogItem1.update_date = DateTime.Now;
                newCatalogItem1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem2 = new catalog_item();
                // newCatalogItem2.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem2.product_id = newProduct2.product_id;
                newCatalogItem2.catalog_id = newCatalog.catalog_id;
                newCatalogItem2.catalog_item_code = "52520";
                newCatalogItem2.catalog_item_name = "WFC MAGAZINE VOUCHER $20";
                newCatalogItem2.price = Convert.ToDecimal(0.00);
                newCatalogItem2.nb_units = 1;
                newCatalogItem2.image_url = "1";
                newCatalogItem2.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem2.catalog_item_export_name = "";
                newCatalogItem2.description = "";
                newCatalogItem2.deleted = false;
                newCatalogItem2.create_date = DateTime.Now;
                newCatalogItem2.create_user_id = CREATE_USER_ID;
                newCatalogItem2.update_date = DateTime.Now;
                newCatalogItem2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item newCatalogItem3 = new catalog_item();
                // newCatalogItem3.catalog_item_id;                  // Autonumber generated on insert
                newCatalogItem3.product_id = newProduct3.product_id;
                newCatalogItem3.catalog_id = newCatalog.catalog_id;
                newCatalogItem3.catalog_item_code = "52530";
                newCatalogItem3.catalog_item_name = "WFC MAGAZINE VOUCHER $30";
                newCatalogItem3.price = Convert.ToDecimal(0.00);
                newCatalogItem3.nb_units = 1;
                newCatalogItem3.image_url = "1";
                newCatalogItem3.catalog_item_status_id = 101;        // 101 = Active
                newCatalogItem3.catalog_item_export_name = "";
                newCatalogItem3.description = "";
                newCatalogItem3.deleted = false;
                newCatalogItem3.create_date = DateTime.Now;
                newCatalogItem3.create_user_id = CREATE_USER_ID;
                newCatalogItem3.update_date = DateTime.Now;
                newCatalogItem3.update_user_id = CREATE_USER_ID;

                #endregion

                
                // Add the new recotds to the db context
                context.catalog_items.InsertOnSubmit(newCatalogItem1);
                context.catalog_items.InsertOnSubmit(newCatalogItem2);
                context.catalog_items.InsertOnSubmit(newCatalogItem3);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog items ok");

                #endregion

                #region catalog_item_detail - 40%

                #region Create new record

                catalog_item_detail newCatalogItemDetail1 = new catalog_item_detail();
                // newCatalogItemDetail1.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail1.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemDetail1.catalog_item_detail_code = "52510";
                newCatalogItemDetail1.catalog_item_detail_name = "WFC MAGAZINE VOUCHER $10";
                newCatalogItemDetail1.price = Convert.ToDecimal(6.00);
                newCatalogItemDetail1.nb_units = 1;
                newCatalogItemDetail1.profit_rate = 0.40;
                newCatalogItemDetail1.term = 0;
                newCatalogItemDetail1.description = "";
                newCatalogItemDetail1.is_default = false;
                newCatalogItemDetail1.deleted = false;
                newCatalogItemDetail1.create_date = DateTime.Now;
                newCatalogItemDetail1.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail1.update_date = DateTime.Now;
                newCatalogItemDetail1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail2 = new catalog_item_detail();
                // newCatalogItemDetail2.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail2.catalog_item_id = newCatalogItem2.catalog_item_id;
                newCatalogItemDetail2.catalog_item_detail_code = "52520";
                newCatalogItemDetail2.catalog_item_detail_name = "WFC MAGAZINE VOUCHER $20";
                newCatalogItemDetail2.price = Convert.ToDecimal(12.00);
                newCatalogItemDetail2.nb_units = 1;
                newCatalogItemDetail2.profit_rate = 0.40;
                newCatalogItemDetail2.term = 0;
                newCatalogItemDetail2.description = "";
                newCatalogItemDetail2.is_default = false;
                newCatalogItemDetail2.deleted = false;
                newCatalogItemDetail2.create_date = DateTime.Now;
                newCatalogItemDetail2.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail2.update_date = DateTime.Now;
                newCatalogItemDetail2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_detail newCatalogItemDetail3 = new catalog_item_detail();
                // newCatalogItemDetail3.catalog_item_detail_id;                 // Autonumber generated on insert
                newCatalogItemDetail3.catalog_item_id = newCatalogItem3.catalog_item_id;
                newCatalogItemDetail3.catalog_item_detail_code = "52530";
                newCatalogItemDetail3.catalog_item_detail_name = "WFC MAGAZINE VOUCHER $30";
                newCatalogItemDetail3.price = Convert.ToDecimal(18.00);
                newCatalogItemDetail3.nb_units = 1;
                newCatalogItemDetail3.profit_rate = 0.40;
                newCatalogItemDetail3.term = 0;
                newCatalogItemDetail3.description = "";
                newCatalogItemDetail3.is_default = false;
                newCatalogItemDetail3.deleted = false;
                newCatalogItemDetail3.create_date = DateTime.Now;
                newCatalogItemDetail3.create_user_id = CREATE_USER_ID;
                newCatalogItemDetail3.update_date = DateTime.Now;
                newCatalogItemDetail3.update_user_id = CREATE_USER_ID;

                #endregion

                
                // Add the new recotds to the db context
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail1);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail2);
                context.catalog_item_details.InsertOnSubmit(newCatalogItemDetail3);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Catalog item detail ok");

                #endregion

                #region catalog_item_category_catalog_item

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem1 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem1.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem1.catalog_item_id = newCatalogItem1.catalog_item_id;
                newCatalogItemCategoryCatalogItem1.display_order = 1;
                newCatalogItemCategoryCatalogItem1.deleted = false;
                newCatalogItemCategoryCatalogItem1.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem1.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem1.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem1.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem2 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem2.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem2.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem2.catalog_item_id = newCatalogItem2.catalog_item_id;
                newCatalogItemCategoryCatalogItem2.display_order = 2;
                newCatalogItemCategoryCatalogItem2.deleted = false;
                newCatalogItemCategoryCatalogItem2.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem2.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem2.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem2.update_user_id = CREATE_USER_ID;

                #endregion

                #region Create new record

                catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem3 = new catalog_item_category_catalog_item();
                // newCatalogItemCategoryCatalogItem3.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
                newCatalogItemCategoryCatalogItem3.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
                newCatalogItemCategoryCatalogItem3.catalog_item_id = newCatalogItem3.catalog_item_id;
                newCatalogItemCategoryCatalogItem3.display_order = 3;
                newCatalogItemCategoryCatalogItem3.deleted = false;
                newCatalogItemCategoryCatalogItem3.create_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem3.create_user_id = CREATE_USER_ID;
                newCatalogItemCategoryCatalogItem3.update_date = DateTime.Now;
                newCatalogItemCategoryCatalogItem3.update_user_id = CREATE_USER_ID;

                #endregion


                // Add the new recotds to the db context
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem1);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem2);
                context.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem3);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("catalog item category catalog item ok");

                #endregion

                #region business_rule

                #region Predefined Business rules

                #region Sales History Interval # Day

                // Create new form group object
                business_rule newBusinessRule1 = new business_rule();

                // Fill in new record data
                // newBusinessRule1.business_rule_id;           // Created upon insert
                newBusinessRule1.form_id = newForm.form_id;
                newBusinessRule1.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule1.field_id = 29;                 // 29 = [Interval_NbDay]
                newBusinessRule1.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule1.business_rule_name = "[Interval_NbDay]";
                newBusinessRule1.value_to_compare = "45";
                // newBusinessRule1.form_section_number;        // null, not used
                // newBusinessRule1.description;                // null, not used
                // newBusinessRule1.message;                    // null, not used
                newBusinessRule1.deleted = false;
                newBusinessRule1.create_date = DateTime.Now;
                newBusinessRule1.create_user_id = CREATE_USER_ID;
                newBusinessRule1.update_date = DateTime.Now;
                newBusinessRule1.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule1);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Sales History Minimum Total Amount:

                // Create new form group object
                business_rule newBusinessRule2 = new business_rule();

                // Fill in new record data
                // newBusinessRule2.business_rule_id;           // Created upon insert
                newBusinessRule2.form_id = newForm.form_id;
                newBusinessRule2.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule2.field_id = 105;                 // 105 = account_history_min_total_amount
                newBusinessRule2.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule2.business_rule_name = "account_history_min_total_amount";
                newBusinessRule2.value_to_compare = "2000";
                // newBusinessRule2.form_section_number;        // null, not used
                // newBusinessRule2.description;                // null, not used
                // newBusinessRule2.message;                    // null, not used
                newBusinessRule2.deleted = false;
                newBusinessRule2.create_date = DateTime.Now;
                newBusinessRule2.create_user_id = CREATE_USER_ID;
                newBusinessRule2.update_date = DateTime.Now;
                newBusinessRule2.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #endregion

                #region Order Requirements

                #region For All Sections

                #region Standard lead time

                // Create new form group object
                business_rule newBusinessRule3 = new business_rule();

                // Fill in new record data
                // newBusinessRule3.business_rule_id;           // Created upon insert
                newBusinessRule3.form_id = newForm.form_id;
                newBusinessRule3.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule3.field_id = 35;                 // 35 = min_nb_day_lead_time
                newBusinessRule3.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule3.business_rule_name = "min_nb_day_lead_time";
                newBusinessRule3.value_to_compare = "10";
                // newBusinessRule3.form_section_number;        // null, not used
                // newBusinessRule3.description;                // null, not used
                // newBusinessRule3.message;                    // null, not used
                newBusinessRule3.deleted = false;
                newBusinessRule3.create_date = DateTime.Now;
                newBusinessRule3.create_user_id = CREATE_USER_ID;
                newBusinessRule3.update_date = DateTime.Now;
                newBusinessRule3.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule3);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Minimum Total Quantity

                // Create new form group object
                business_rule newBusinessRule4 = new business_rule();

                // Fill in new record data
                // newBusinessRule4.business_rule_id;           // Created upon insert
                newBusinessRule4.form_id = newForm.form_id;
                newBusinessRule4.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule4.field_id = 49;                 // 49 = min_total_quantity
                newBusinessRule4.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule4.business_rule_name = "min_total_quantity";
                newBusinessRule4.value_to_compare = "1";
                // newBusinessRule4.form_section_number;        // null, not used
                // newBusinessRule4.description;                // null, not used
                // newBusinessRule4.message;                    // null, not used
                newBusinessRule4.deleted = false;
                newBusinessRule4.create_date = DateTime.Now;
                newBusinessRule4.create_user_id = CREATE_USER_ID;
                newBusinessRule4.update_date = DateTime.Now;
                newBusinessRule4.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule4);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Minimum Total Amount

                // Create new form group object
                business_rule newBusinessRule5 = new business_rule();

                // Fill in new record data
                // newBusinessRule5.business_rule_id;           // Created upon insert
                newBusinessRule5.form_id = newForm.form_id;
                newBusinessRule5.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                newBusinessRule5.field_id = 45;                 // 45 = min_total_amount
                newBusinessRule5.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                newBusinessRule5.business_rule_name = "min_total_amount";
                newBusinessRule5.value_to_compare = "1";
                // newBusinessRule5.form_section_number;        // null, not used
                // newBusinessRule5.description;                // null, not used
                // newBusinessRule5.message;                    // null, not used
                newBusinessRule5.deleted = false;
                newBusinessRule5.create_date = DateTime.Now;
                newBusinessRule5.create_user_id = CREATE_USER_ID;
                newBusinessRule5.update_date = DateTime.Now;
                newBusinessRule5.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_rules.InsertOnSubmit(newBusinessRule5);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("Business rule submitted");

                #endregion

                #region Standard Maximum Total Amount

                //// Create new form group object
                //business_rule newBusinessRule6 = new business_rule();

                //// Fill in new record data
                //// newBusinessRule6.business_rule_id;           // Created upon insert
                //newBusinessRule6.form_id = newForm.form_id;
                //newBusinessRule6.form_section_type_id = 1;      // 1 = standard order, 2 = supply order, 3 = optional product
                //newBusinessRule6.field_id = 108;                 // 108 = max_total_amount
                //newBusinessRule6.logical_operator_id = 1;       // 1 = equal, 2 = not equal, 3 = greater than, 4 = greater than equal, 5 = less than, 6 = less than equal
                //newBusinessRule6.business_rule_name = "max_total_amount";
                //newBusinessRule6.value_to_compare = "50000";
                //// newBusinessRule6.form_section_number;        // null, not used
                //// newBusinessRule6.description;                // null, not used
                //// newBusinessRule6.message;                    // null, not used
                //newBusinessRule6.deleted = false;
                //newBusinessRule6.create_date = DateTime.Now;
                //newBusinessRule6.create_user_id = CREATE_USER_ID;
                //newBusinessRule6.update_date = DateTime.Now;
                //newBusinessRule6.update_user_id = CREATE_USER_ID;

                //// Add the new form group to the db context
                //context.business_rules.InsertOnSubmit(newBusinessRule6);

                //// Submit changes to the db
                //context.SubmitChanges();

                //sb.AppendLine("Business rule submitted");

                #endregion

                #endregion

                #endregion

                #endregion

                #region business_exception



                #region Standard product - minimum day lead time

                // Create new form group object
                business_exception newBusinessException2 = new business_exception();

                // Fill in new record data
                //newBusinessException2.business_exception_id;      // newBusinessException1
                newBusinessException2.form_id = newForm.form_id;
                newBusinessException2.business_exception_name = "Standard product - minimum day lead time";
                newBusinessException2.exception_type_id = 100;      // 100 = Note
                newBusinessException2.entity_type_id = 4;           // 4 = Order
                newBusinessException2.warning_message = "10 Business Days Lead-Time Required.<BR>If requested Lead-Time is <u>LESS</u> than 10 business Days, change the delivery date to meet this requirement.";
                newBusinessException2.app_item_id = 23;             // 23 = Order Form Step 3 - Information
                newBusinessException2.message = "For Product Orders by Common Carrier <u>LESS</u> than 10 Business Days cannot be guaranteed. ";
                newBusinessException2.exception_expression = "[nb_day_lead_time] <  [min_nb_day_lead_time]";
                newBusinessException2.fees_value_expression = "";
                newBusinessException2.form_section_type_id = 1;     // 1 = Standard product
                // newBusinessException2.form_section_number;       // null, not used
                // newBusinessException2.business_rule_id;          // null, not used
                newBusinessException2.deleted = false;
                newBusinessException2.create_date = DateTime.Now;
                newBusinessException2.create_user_id = CREATE_USER_ID;
                newBusinessException2.update_date = DateTime.Now;
                newBusinessException2.update_user_id = CREATE_USER_ID;

                // Add the new form group to the db context
                context.business_exceptions.InsertOnSubmit(newBusinessException2);

                // Submit changes to the db
                context.SubmitChanges();

                sb.AppendLine("New business exception");

                #endregion


                #region Standard product - expedited freight charges

                //// Create new form group object
                //business_exception newBusinessException5 = new business_exception();

                //// Fill in new record data
                ////newBusinessException5.business_exception_id;      // newBusinessException1
                //newBusinessException5.form_id = newForm.form_id;
                //newBusinessException5.business_exception_name = "Standard product - expedited freight charges";
                //newBusinessException5.exception_type_id = 103;      // 103 = Expedited freight charges
                //newBusinessException5.entity_type_id = 4;           // 4 = Order
                //newBusinessException5.warning_message = "";
                //// newBusinessException5.app_item_id;               // null, not used
                //newBusinessException5.message = "For Product Orders by Common Carrier, <u>LESS</u> than 4 Business Days, if there are any expedited freight charges relating to this order they will be recovered from the employee's 12-pay.";
                //newBusinessException5.exception_expression = "([nb_day_lead_time] <  [min_nb_day_lead_time]) AND  ([delivery_method_id] = [common_carrier])";
                //newBusinessException5.fees_value_expression = "";
                //newBusinessException5.form_section_type_id = 1;     // 1 = Standard product
                //// newBusinessException5.form_section_number;       // null, not used
                //// newBusinessException5.business_rule_id;          // null, not used
                //newBusinessException5.deleted = false;
                //newBusinessException5.create_date = DateTime.Now;
                //newBusinessException5.create_user_id = CREATE_USER_ID;
                //newBusinessException5.update_date = DateTime.Now;
                //newBusinessException5.update_user_id = CREATE_USER_ID;

                //// Add the new form group to the db context
                //context.business_exceptions.InsertOnSubmit(newBusinessException5);

                //// Submit changes to the db
                //context.SubmitChanges();

                //sb.AppendLine("New business exception");

                #endregion

                #region Standard product - minimum total quantity

                //// Create new form group object
                //business_exception newBusinessException7 = new business_exception();

                //// Fill in new record data
                ////newBusinessException7.business_exception_id;      // newBusinessException1
                //newBusinessException7.form_id = newForm.form_id;
                //newBusinessException7.business_exception_name = "Standard product - minimum total quantity";
                //newBusinessException7.exception_type_id = 900;      // 900 = Mandatory
                //newBusinessException7.entity_type_id = 4;           // 4 = Order
                //newBusinessException7.warning_message = "";
                //newBusinessException7.app_item_id = 24;             // 24 = Order form step 4 - order detail
                //newBusinessException7.message = "";
                //newBusinessException7.exception_expression = "([total_quantity] <  [min_total_quantity])";
                //newBusinessException7.fees_value_expression = "";
                //newBusinessException7.form_section_type_id = 1;     // 1 = Standard product
                //// newBusinessException7.form_section_number;       // null, not used
                //// newBusinessException7.business_rule_id;          // null, not used
                //newBusinessException7.deleted = false;
                //newBusinessException7.create_date = DateTime.Now;
                //newBusinessException7.create_user_id = CREATE_USER_ID;
                //newBusinessException7.update_date = DateTime.Now;
                //newBusinessException7.update_user_id = CREATE_USER_ID;

                //// Add the new form group to the db context
                //context.business_exceptions.InsertOnSubmit(newBusinessException7);

                //// Submit changes to the db
                //context.SubmitChanges();

                //sb.AppendLine("New business exception");

                #endregion

                #endregion

                #endregion

                sb.AppendLine("Success!");
            }
            catch (Exception ex)
            {
                //transaction.Rollback();

                sb.AppendLine("transaction rollback due to error");
                sb.AppendLine("Message: " + ex.Message);
                sb.AppendLine("Source: " + ex.Source);
                sb.AppendLine("");
                sb.AppendLine(ex.StackTrace);
            }
            finally
            {
                if (context.Connection != null)
                {
                    context.Connection.Close();
                }
            }

            sb.AppendLine("end");

            return sb.ToString();
        }

        #endregion
    }
}
