using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderExpress.DataGeneration {
    class FormCreator {

        private const int CREATE_USER_ID = 100010;    // system user
        private const string CONNECTION_STRING = "Data Source=USPVL3K09-dev;Initial Catalog=QSPFulfillment;Integrated Security=True";
        private QSPFulfillmentDataContext db;
        private StringBuilder sb = new StringBuilder();

        public string Execute() {
            db = new QSPFulfillmentDataContext(CONNECTION_STRING);

            CreateOtisBulkSupplies();
            CreateOtisPfsSupplies();
            CreatePineValleyBulkSupplies();
            CreatePineValleyPfsSupplies();

            return sb.ToString();
        }

        private void CreateOtisBulkSupplies() {
            int FormId = 70;
            catalog_group NewCatalogGroup = CreateCatalogGroup("OtisBulkSupplies09", "Otis Spring 2009 Bulk Supplies");
            catalog NewCatalog = CreateCatalog(NewCatalogGroup);
            catalog_item_category NewCatalogItemCategory = CreateCatalogItemCategory(NewCatalog);
            form_section NewFormSection = CreateFormSection(NewCatalogItemCategory, FormId, 2);
            form_delivery_method NewFormDeliveryMethod = CreateFormDeliveryMethod(FormId, 1);
            form_catalog_group NewFormCatalogGroup = CreateFormCatalogGroup(FormId, NewCatalogGroup.catalog_group_id);

            product NewProduct1 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2058", "Otis Bulk Brochure Priced", 1, 27, 1);
            product NewProduct2 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2059", "Otis Bulk Brochure Un-priced", 1, 27, 2);
            product NewProduct3 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2063", "Otis Launch Poster", 1, 27, 3);
            product NewProduct4 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2062", "Otis Mass Display", 1, 27, 4);
            product NewProduct5 = CreateProduct(NewCatalog, NewCatalogItemCategory, "GE2000", "Large Outer Envelope", 1, 27, 5);
            product NewProduct6 = CreateProduct(NewCatalog, NewCatalogItemCategory, "GE1003B", "Medium Collection Envelope", 1, 27, 6);
        }

        private void CreateOtisPfsSupplies() {
            int FormId = 72;
            catalog_group NewCatalogGroup = CreateCatalogGroup("OtisPfsSupplies09", "Otis Spring 2009 PFS Supplies");
            catalog NewCatalog = CreateCatalog(NewCatalogGroup);
            catalog_item_category NewCatalogItemCategory = CreateCatalogItemCategory(NewCatalog);
            form_section NewFormSection = CreateFormSection(NewCatalogItemCategory, FormId, 2);
            form_delivery_method NewFormDeliveryMethod = CreateFormDeliveryMethod(FormId, 1);
            form_catalog_group NewFormCatalogGroup = CreateFormCatalogGroup(FormId, NewCatalogGroup.catalog_group_id);

            product NewProduct1 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2067", "Otis PFS Brochure Priced", 1, 27, 1);
            product NewProduct2 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2072", "Otis PFS Brochure Un-priced", 1, 27, 2);
            product NewProduct3 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CDOS2066", "Otis 3-part Order Form", 1, 27, 3);
            product NewProduct4 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2063", "Otis Launch Poster", 1, 27, 4);
            product NewProduct5 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2062", "Otis Mass Display", 1, 27, 5);
            product NewProduct6 = CreateProduct(NewCatalog, NewCatalogItemCategory, "GE2000", "Large Outer Envelope", 1, 27, 6);
            product NewProduct7 = CreateProduct(NewCatalog, NewCatalogItemCategory, "GE1003B", "Medium Collection Envelope", 1, 27, 7);
            product NewProduct8 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2023", "PFS Homeroom Envelope (1) per class", 1, 27, 8);
        }

        private void CreatePineValleyBulkSupplies() {
            int FormId = 66;
            catalog_group NewCatalogGroup = CreateCatalogGroup("PVBulkSupplies09", "Pine Valley 2009 Bulk Supplies");
            catalog NewCatalog = CreateCatalog(NewCatalogGroup);
            catalog_item_category NewCatalogItemCategory = CreateCatalogItemCategory(NewCatalog);
            form_section NewFormSection = CreateFormSection(NewCatalogItemCategory, FormId, 2);
            form_delivery_method NewFormDeliveryMethod = CreateFormDeliveryMethod(FormId, 1);
            form_catalog_group NewFormCatalogGroup = CreateFormCatalogGroup(FormId, NewCatalogGroup.catalog_group_id);

            product NewProduct1 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2068", "PV Spring Bulk Brochure Priced", 1, 5, 1);
            product NewProduct2 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2073", "PV Spring Bulk Brochure Unpriced", 1, 5, 2);
            product NewProduct3 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2071", "PV Launch Poster", 1, 5, 3);
            product NewProduct4 = CreateProduct(NewCatalog, NewCatalogItemCategory, "GE2000", "Large Brochure Envelope", 1, 5, 4);
            product NewProduct5 = CreateProduct(NewCatalog, NewCatalogItemCategory, "GE1003B", "Medium Collection Envelope", 1, 5, 5);
        }

        private void CreatePineValleyPfsSupplies() {
            int FormId = 68;
            catalog_group NewCatalogGroup = CreateCatalogGroup("PVPfsSupplies09", "Pine Valley 2009 PFS Supplies");
            catalog NewCatalog = CreateCatalog(NewCatalogGroup);
            catalog_item_category NewCatalogItemCategory = CreateCatalogItemCategory(NewCatalog);
            form_section NewFormSection = CreateFormSection(NewCatalogItemCategory, FormId, 2);
            form_delivery_method NewFormDeliveryMethod = CreateFormDeliveryMethod(FormId, 1);
            form_catalog_group NewFormCatalogGroup = CreateFormCatalogGroup(FormId, NewCatalogGroup.catalog_group_id);

            product NewProduct1 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2069", "PV Spring PFS Brochure Priced", 1, 5, 1);
            product NewProduct2 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2074", "PV Spring PFS Brochure Unpriced", 1, 5, 2);
            product NewProduct3 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CDPV2070", "PV 3-part Order Form", 1, 5, 3);
            product NewProduct4 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2071", "PV Launch Poster", 1, 5, 4);
            product NewProduct5 = CreateProduct(NewCatalog, NewCatalogItemCategory, "GE2000", "Large Brochure Envelope", 1, 5, 5);
            product NewProduct6 = CreateProduct(NewCatalog, NewCatalogItemCategory, "GE1003B", "Medium Collection Envelope", 1, 5, 6);
            product NewProduct7 = CreateProduct(NewCatalog, NewCatalogItemCategory, "CD2023", "PFS Homeroom Envelope (1) per class", 1, 5, 7);
        }

        private product CreateProduct(catalog NewCatalog, catalog_item_category NewCatalogItemCategory, string productCode, string productName, int nbUnit, int vendorId, int displayOrder) {
            product NewProduct1 = CreateProductRow(productCode, productName, nbUnit, vendorId);
            catalog_item NewCatalogItem1 = CreateCatalogItem(NewProduct1, NewCatalog);
            catalog_item_detail NewCatalogItemDetail1 = CreateCatalogItemDetail(NewCatalogItem1);
            catalog_item_category_catalog_item NewCatalogItemCategoryCatalogItem1 = CreateCatalogItemCategoryCatalogItem(NewCatalogItemCategory, NewCatalogItem1, displayOrder);
            return NewProduct1;
        }

        private catalog_group CreateCatalogGroup(string catalogCode, string catalogName) {
            // Create new record
            catalog_group newCatalogGroup = new catalog_group();
            // newCatalogGroup.catalog_group_id;                // Autonumber generated on insert
            newCatalogGroup.catalog_group_code = catalogCode;
            newCatalogGroup.catalog_group_name = catalogName;
            newCatalogGroup.description = "";
            newCatalogGroup.deleted = false;
            newCatalogGroup.create_date = DateTime.Now;
            newCatalogGroup.create_user_id = CREATE_USER_ID;
            newCatalogGroup.update_date = DateTime.Now;
            newCatalogGroup.update_user_id = CREATE_USER_ID;

            db.catalog_groups.InsertOnSubmit(newCatalogGroup);
            db.SubmitChanges();
            sb.AppendLine("catalog group id = " + newCatalogGroup.catalog_group_id.ToString());
            return newCatalogGroup;
        }

        private catalog CreateCatalog(catalog_group newCatalogGroup) {
            // Create new record
            catalog newCatalog = new catalog();
            // newCatalog.newCatalog_id;          // Autonumber generated on insert
            newCatalog.catalog_group_id = newCatalogGroup.catalog_group_id;
            newCatalog.catalog_code = newCatalogGroup.catalog_group_code;
            newCatalog.catalog_name = newCatalogGroup.catalog_group_name;
            newCatalog.culture = "en-US";
            newCatalog.description = "";
            newCatalog.start_date = new DateTime(2009, 1, 1);
            newCatalog.end_date = new DateTime(2009, 6, 30);
            newCatalog.deleted = false;
            newCatalog.create_date = DateTime.Now;
            newCatalog.create_user_id = CREATE_USER_ID;
            newCatalog.update_date = DateTime.Now;
            newCatalog.update_user_id = CREATE_USER_ID;

            db.catalogs.InsertOnSubmit(newCatalog);
            db.SubmitChanges();
            sb.AppendLine("new catalog id = " + newCatalog.catalog_id.ToString());
            return newCatalog;
        }

        private catalog_item_category CreateCatalogItemCategory(catalog newCatalog) {
            // Create new record
            catalog_item_category newCatalogItemCategory1 = new catalog_item_category();
            // newCatalogItemCategory1.catalog_item_category_id;         // Autonumber generated on insert
            newCatalogItemCategory1.catalog_id = newCatalog.catalog_id;
            newCatalogItemCategory1.catalog_item_category_name = newCatalog.catalog_name;
            newCatalogItemCategory1.parent_catalog_item_category_id = 0;
            newCatalogItemCategory1.deleted = false;
            newCatalogItemCategory1.create_date = DateTime.Now;
            newCatalogItemCategory1.create_user_id = CREATE_USER_ID;
            newCatalogItemCategory1.update_date = DateTime.Now;
            newCatalogItemCategory1.update_user_id = CREATE_USER_ID;

            db.catalog_item_categories.InsertOnSubmit(newCatalogItemCategory1);
            db.SubmitChanges();
            sb.AppendLine("new catalog item category id = " + newCatalogItemCategory1.catalog_item_category_id.ToString());
            return newCatalogItemCategory1;
        }

        private form_section CreateFormSection(catalog_item_category newCatalogItemCategory1, int formId, int formSectionNumber) {
            // Create new record
            form_section newPAFormSection1 = new form_section();
            // newPAFormSection1.form_section_id;              // Autonumber generated on insert
            newPAFormSection1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
            newPAFormSection1.form_id = formId;
            newPAFormSection1.form_section_type_id = 2;        // 1 = standard product, 2 = supply product, 3 = optional product
            newPAFormSection1.form_section_number = formSectionNumber;
            newPAFormSection1.form_section_title = newCatalogItemCategory1.catalog_item_category_name;
            newPAFormSection1.description = "";
            newPAFormSection1.deleted = false;
            newPAFormSection1.create_date = DateTime.Now;
            newPAFormSection1.create_user_id = CREATE_USER_ID;
            newPAFormSection1.update_date = DateTime.Now;
            newPAFormSection1.update_user_id = CREATE_USER_ID;

            db.form_sections.InsertOnSubmit(newPAFormSection1);
            db.SubmitChanges();
            sb.AppendLine("form section id = " + newPAFormSection1.form_section_id.ToString());
            return newPAFormSection1;
        }

        private product CreateProductRow(string productCode, string productName, int nbUnit, int vendorId) {
            product newProduct1 = new product();
            // newProduct1.product_id;                      // Autonumber generated on insert
            newProduct1.product_code = productCode;
            newProduct1.product_name = productName;
            newProduct1.price = 0;      // Retail case cost
            newProduct1.nb_units = nbUnit;                       // boxes per case
            newProduct1.nb_day_lead_time = 0;
            newProduct1.product_status_id = 101;            // 101 = Active
            newProduct1.product_type_id = 7;                // 7 = Supply
            newProduct1.business_division_id = 1;           // 1 = US
            newProduct1.commission = Convert.ToDecimal(0);  // Calculated in as400
            newProduct1.coupon_id = null;
            newProduct1.description = "";
            newProduct1.image_url = "";
            newProduct1.is_free_sample = false;
            newProduct1.oracle_code = "";
            newProduct1.IVCOUP = "";
            newProduct1.IVITEM = productCode;
            newProduct1.unit_cost = null;
            newProduct1.vendor_id = vendorId;                     // 27 = Otis; 5 = Pine Valley
            newProduct1.vendor_item_code = productCode;
            newProduct1.deleted = false;
            newProduct1.create_date = DateTime.Now;
            newProduct1.create_user_id = CREATE_USER_ID;
            newProduct1.update_date = DateTime.Now;
            newProduct1.update_user_id = CREATE_USER_ID;

            db.products.InsertOnSubmit(newProduct1);
            db.SubmitChanges();
            sb.AppendLine("product id = " + newProduct1.product_id.ToString());
            return newProduct1;
        }

        public catalog_item CreateCatalogItem(product newProduct1, catalog newCatalog) {
            catalog_item newCatalogItem1 = new catalog_item();
            // newCatalogItem1.catalog_item_id;                  // Autonumber generated on insert
            newCatalogItem1.product_id = newProduct1.product_id;
            newCatalogItem1.catalog_id = newCatalog.catalog_id;
            newCatalogItem1.catalog_item_code = newProduct1.product_code;
            newCatalogItem1.catalog_item_name = newProduct1.product_name;
            newCatalogItem1.price = 0;      // Retail case cost
            newCatalogItem1.nb_units = newProduct1.nb_units;                       // Boxes per case
            newCatalogItem1.image_url = "";
            newCatalogItem1.catalog_item_status_id = 101;        // 101 = Active
            newCatalogItem1.catalog_item_export_name = "";
            newCatalogItem1.description = "";
            newCatalogItem1.deleted = false;
            newCatalogItem1.create_date = DateTime.Now;
            newCatalogItem1.create_user_id = CREATE_USER_ID;
            newCatalogItem1.update_date = DateTime.Now;
            newCatalogItem1.update_user_id = CREATE_USER_ID;

            db.catalog_items.InsertOnSubmit(newCatalogItem1);
            db.SubmitChanges();
            sb.AppendLine("catalog item id = " + newCatalogItem1.catalog_item_id.ToString());
            return newCatalogItem1;
        }

        public catalog_item_detail CreateCatalogItemDetail(catalog_item newCatalogItem1) {
            catalog_item_detail newCatalogItemDetail1 = new catalog_item_detail();
            // newCatalogItemDetail1.catalog_item_detail_id;                 // Autonumber generated on insert
            newCatalogItemDetail1.catalog_item_id = newCatalogItem1.catalog_item_id;
            newCatalogItemDetail1.catalog_item_detail_code = newCatalogItem1.catalog_item_code;
            newCatalogItemDetail1.catalog_item_detail_name = newCatalogItem1.catalog_item_name;
            newCatalogItemDetail1.price = 0;
            newCatalogItemDetail1.nb_units = newCatalogItem1.nb_units.Value;
            newCatalogItemDetail1.profit_rate = 0;
            newCatalogItemDetail1.term = 1;
            newCatalogItemDetail1.description = "";
            newCatalogItemDetail1.is_default = false;
            newCatalogItemDetail1.deleted = false;
            newCatalogItemDetail1.create_date = DateTime.Now;
            newCatalogItemDetail1.create_user_id = CREATE_USER_ID;
            newCatalogItemDetail1.update_date = DateTime.Now;
            newCatalogItemDetail1.update_user_id = CREATE_USER_ID;

            db.catalog_item_details.InsertOnSubmit(newCatalogItemDetail1);
            db.SubmitChanges();
            sb.AppendLine("catalog item detail id = " + newCatalogItemDetail1.catalog_item_detail_id.ToString());
            return newCatalogItemDetail1;
        }

        public catalog_item_category_catalog_item CreateCatalogItemCategoryCatalogItem(catalog_item_category newCatalogItemCategory1, catalog_item newCatalogItem1, int displayOrder) {
            // Create new record
            catalog_item_category_catalog_item newCatalogItemCategoryCatalogItem1 = new catalog_item_category_catalog_item();
            // newCatalogItemCategoryCatalogItem1.catalog_item_category_catalog_item_id;         // Autonumber generated on insert
            newCatalogItemCategoryCatalogItem1.catalog_item_category_id = newCatalogItemCategory1.catalog_item_category_id;
            newCatalogItemCategoryCatalogItem1.catalog_item_id = newCatalogItem1.catalog_item_id;
            newCatalogItemCategoryCatalogItem1.display_order = displayOrder;
            newCatalogItemCategoryCatalogItem1.deleted = false;
            newCatalogItemCategoryCatalogItem1.create_date = DateTime.Now;
            newCatalogItemCategoryCatalogItem1.create_user_id = CREATE_USER_ID;
            newCatalogItemCategoryCatalogItem1.update_date = DateTime.Now;
            newCatalogItemCategoryCatalogItem1.update_user_id = CREATE_USER_ID;

            db.catalog_item_category_catalog_items.InsertOnSubmit(newCatalogItemCategoryCatalogItem1);
            db.SubmitChanges();
            sb.AppendLine("catalog item category catalog item id = " + newCatalogItemCategoryCatalogItem1.catalog_item_category_catalog_item_id.ToString());
            return newCatalogItemCategoryCatalogItem1;
        }

        public form_catalog_group CreateFormCatalogGroup(int formId, int catalogGroupId) {
            form_catalog_group NewRow = new form_catalog_group();
            NewRow.form_id = formId;
            NewRow.catalog_group_id = catalogGroupId;
            NewRow.product_catalog_item_category_id = db.form_sections.Where(a => a.form_id == formId && a.form_section_number == 1).First().catalog_item_category_id;
            NewRow.supply_catalog_item_category_id = db.form_sections.Where(a => a.form_id == formId && a.form_section_number == 2).First().catalog_item_category_id.ToString();
            NewRow.deleted = false;
            NewRow.create_date = DateTime.Now;
            NewRow.create_user_id = CREATE_USER_ID;
            NewRow.update_date = DateTime.Now;
            NewRow.update_user_id = CREATE_USER_ID;

            db.form_catalog_groups.InsertOnSubmit(NewRow);
            db.SubmitChanges();
            sb.AppendLine("form catalog group id = " + NewRow.form_catalog_group_id.ToString());
            return NewRow;
        }

        public form_delivery_method CreateFormDeliveryMethod(int formId, int deliveryMethodId) {
            form_delivery_method NewRow = new form_delivery_method();
            NewRow.form_id = formId;
            NewRow.delivery_method_id = deliveryMethodId;
            NewRow.deleted = false;
            NewRow.create_date = DateTime.Now;
            NewRow.create_user_id = CREATE_USER_ID;
            NewRow.update_date = DateTime.Now;
            NewRow.update_user_id = CREATE_USER_ID;

            db.form_delivery_methods.InsertOnSubmit(NewRow);
            db.SubmitChanges();
            sb.AppendLine("form delivery method id = " + NewRow.form_delivery_method_id.ToString());
            return NewRow;
        }
    }
}
