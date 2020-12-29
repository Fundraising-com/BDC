namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataProvider : DbContext
    {
        public DataProvider()
            : base("name=EFundraisingProd")
        {
        }

        public virtual DbSet<C_tbd_partner> C_tbd_partner { get; set; }
        public virtual DbSet<C_tbd_promotion> C_tbd_promotion { get; set; }
        public virtual DbSet<C_tbd_promotion_type> C_tbd_promotion_type { get; set; }
        public virtual DbSet<accounting_class> accounting_class { get; set; }
        public virtual DbSet<accounting_class_shipping_fees> accounting_class_shipping_fees { get; set; }
        public virtual DbSet<Accounting_Period> Accounting_Period { get; set; }
        public virtual DbSet<accounting_period_result> accounting_period_result { get; set; }
        public virtual DbSet<address_zone> address_zone { get; set; }
        public virtual DbSet<Adjustment> Adjustments { get; set; }
        public virtual DbSet<Adjustment_Audit> Adjustment_Audit { get; set; }
        public virtual DbSet<advertisement> advertisements { get; set; }
        public virtual DbSet<Advertiser> Advertisers { get; set; }
        public virtual DbSet<Advertiser_Partner> Advertiser_Partner { get; set; }
        public virtual DbSet<Advertising_Support> Advertising_Support { get; set; }
        public virtual DbSet<Advertising_Support_Contact> Advertising_Support_Contact { get; set; }
        public virtual DbSet<Advertising_Support_Type> Advertising_Support_Type { get; set; }
        public virtual DbSet<Advertisment_Type> Advertisment_Type { get; set; }
        public virtual DbSet<Alias_Country_Code> Alias_Country_Code { get; set; }
        public virtual DbSet<Alias_Promotion> Alias_Promotion { get; set; }
        public virtual DbSet<Alias_State> Alias_State { get; set; }
        public virtual DbSet<Applicable_Adjustment_Tax> Applicable_Adjustment_Tax { get; set; }
        public virtual DbSet<Applicable_Tax> Applicable_Tax { get; set; }
        public virtual DbSet<Applicable_Tax_To_Add> Applicable_Tax_To_Add { get; set; }
        public virtual DbSet<application> applications { get; set; }
        public virtual DbSet<AR_Activity> AR_Activity { get; set; }
        public virtual DbSet<AR_Activity_Type> AR_Activity_Type { get; set; }
        public virtual DbSet<AR_Consultant> AR_Consultant { get; set; }
        public virtual DbSet<AR_Status> AR_Status { get; set; }
        public virtual DbSet<Area_Manager> Area_Manager { get; set; }
        public virtual DbSet<Associate_Mentor> Associate_Mentor { get; set; }
        public virtual DbSet<Associate_Mentor_Comment> Associate_Mentor_Comment { get; set; }
        public virtual DbSet<associate_mentor_commission> associate_mentor_commission { get; set; }
        public virtual DbSet<Automaton> Automata { get; set; }
        public virtual DbSet<Automaton_Function> Automaton_Function { get; set; }
        public virtual DbSet<Automaton_Shipping_Status> Automaton_Shipping_Status { get; set; }
        public virtual DbSet<Automaton_State> Automaton_State { get; set; }
        public virtual DbSet<Automaton_Transition> Automaton_Transition { get; set; }
        public virtual DbSet<Automaton_Transition_Function> Automaton_Transition_Function { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Bank_Account> Bank_Account { get; set; }
        public virtual DbSet<best_time_call> best_time_call { get; set; }
        public virtual DbSet<best_time_call_desc> best_time_call_desc { get; set; }
        public virtual DbSet<Billing_Company> Billing_Company { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Brand_Coupon_Sheet> Brand_Coupon_Sheet { get; set; }
        public virtual DbSet<brochures_images> brochures_images { get; set; }
        public virtual DbSet<business_calendar> business_calendar { get; set; }
        public virtual DbSet<Business_Rule> Business_Rule { get; set; }
        public virtual DbSet<campaign_reason> campaign_reason { get; set; }
        public virtual DbSet<campaign_reason_desc> campaign_reason_desc { get; set; }
        public virtual DbSet<Cancelation_Reason> Cancelation_Reason { get; set; }
        public virtual DbSet<carrier> carriers { get; set; }
        public virtual DbSet<carrier_shipping_option> carrier_shipping_option { get; set; }
        public virtual DbSet<carrier_shipping_status> carrier_shipping_status { get; set; }
        public virtual DbSet<charge> charges { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<client_activity> client_activity { get; set; }
        public virtual DbSet<client_activity_type> client_activity_type { get; set; }
        public virtual DbSet<client_address> client_address { get; set; }
        public virtual DbSet<client_address_type> client_address_type { get; set; }
        public virtual DbSet<client_sequence> client_sequence { get; set; }
        public virtual DbSet<client_status> client_status { get; set; }
        public virtual DbSet<Collection_Status> Collection_Status { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Commission_Earning> Commission_Earning { get; set; }
        public virtual DbSet<Commission_Outstanding> Commission_Outstanding { get; set; }
        public virtual DbSet<Commission_Outstanding_History> Commission_Outstanding_History { get; set; }
        public virtual DbSet<Commission_Paid> Commission_Paid { get; set; }
        public virtual DbSet<Commission_Rate> Commission_Rate { get; set; }
        public virtual DbSet<Commission_Table> Commission_Table { get; set; }
        public virtual DbSet<Competitor> Competitors { get; set; }
        public virtual DbSet<Competitor_Advertising> Competitor_Advertising { get; set; }
        public virtual DbSet<conciliation> conciliations { get; set; }
        public virtual DbSet<Confirmation_Method> Confirmation_Method { get; set; }
        public virtual DbSet<consultant> consultants { get; set; }
        public virtual DbSet<consultant_address> consultant_address { get; set; }
        public virtual DbSet<consultant_transfer_status> consultant_transfer_status { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Conversion_Rate_Table> Conversion_Rate_Table { get; set; }
        public virtual DbSet<cost_range> cost_range { get; set; }
        public virtual DbSet<country> countries { get; set; }
        public virtual DbSet<Country1> Countries1 { get; set; }
        public virtual DbSet<country_names> country_names { get; set; }
        public virtual DbSet<Coupon_Sheet> Coupon_Sheet { get; set; }
        public virtual DbSet<Credit_Approval_Method> Credit_Approval_Method { get; set; }
        public virtual DbSet<credit_card_refund_request> credit_card_refund_request { get; set; }
        public virtual DbSet<credit_card_refund_request_status> credit_card_refund_request_status { get; set; }
        public virtual DbSet<credit_card_types> credit_card_types { get; set; }
        public virtual DbSet<credit_check_request> credit_check_request { get; set; }
        public virtual DbSet<credit_check_status> credit_check_status { get; set; }
        public virtual DbSet<crm_static_past3seasons_new> crm_static_past3seasons_new { get; set; }
        public virtual DbSet<culture> cultures { get; set; }
        public virtual DbSet<culture1> cultures1 { get; set; }
        public virtual DbSet<customer_status> customer_status { get; set; }
        public virtual DbSet<Default_Consultant_Rate> Default_Consultant_Rate { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<deposit> deposits { get; set; }
        public virtual DbSet<Deposit_Item> Deposit_Item { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<Destinations2> Destinations2 { get; set; }
        public virtual DbSet<Detailed_Promotion> Detailed_Promotion { get; set; }
        public virtual DbSet<division> divisions { get; set; }
        public virtual DbSet<Double_Lead> Double_Lead { get; set; }
        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<EFO_Admin> EFO_Admin { get; set; }
        public virtual DbSet<EFO_Campaign> EFO_Campaign { get; set; }
        public virtual DbSet<EFO_Campaign_Image> EFO_Campaign_Image { get; set; }
        public virtual DbSet<EFO_Campaign_Status> EFO_Campaign_Status { get; set; }
        public virtual DbSet<EFO_Catalog_Category> EFO_Catalog_Category { get; set; }
        public virtual DbSet<EFO_Email_Type> EFO_Email_Type { get; set; }
        public virtual DbSet<EFO_Group_Type> EFO_Group_Type { get; set; }
        public virtual DbSet<EFO_Item> EFO_Item { get; set; }
        public virtual DbSet<EFO_Message> EFO_Message { get; set; }
        public virtual DbSet<EFO_Organizer> EFO_Organizer { get; set; }
        public virtual DbSet<EFO_Participant> EFO_Participant { get; set; }
        public virtual DbSet<EFO_Sale> EFO_Sale { get; set; }
        public virtual DbSet<EFO_Sale_Item> EFO_Sale_Item { get; set; }
        public virtual DbSet<EFO_Status> EFO_Status { get; set; }
        public virtual DbSet<EFO_Supporter> EFO_Supporter { get; set; }
        public virtual DbSet<EFO_Supporter_Email_Sent> EFO_Supporter_Email_Sent { get; set; }
        public virtual DbSet<EFO_Tag> EFO_Tag { get; set; }
        public virtual DbSet<eFR_Lead> eFR_Lead { get; set; }
        public virtual DbSet<Efr_Lead_Activity> Efr_Lead_Activity { get; set; }
        public virtual DbSet<EMail_iwon1> EMail_iwon1 { get; set; }
        public virtual DbSet<EMail_iwon2> EMail_iwon2 { get; set; }
        public virtual DbSet<EMail_PaulStanton> EMail_PaulStanton { get; set; }
        public virtual DbSet<Entry_Form> Entry_Form { get; set; }
        public virtual DbSet<Ext_sales_status> Ext_sales_status { get; set; }
        public virtual DbSet<fedex> fedexes { get; set; }
        public virtual DbSet<Field_Sales_Manager> Field_Sales_Manager { get; set; }
        public virtual DbSet<Flag_Pole> Flag_Pole { get; set; }
        public virtual DbSet<FSM_Address> FSM_Address { get; set; }
        public virtual DbSet<General_Comment> General_Comment { get; set; }
        public virtual DbSet<GL_Table> GL_Table { get; set; }
        public virtual DbSet<Grabber> Grabbers { get; set; }
        public virtual DbSet<group_type> group_type { get; set; }
        public virtual DbSet<group_type_desc> group_type_desc { get; set; }
        public virtual DbSet<harmony_list_transfer> harmony_list_transfer { get; set; }
        public virtual DbSet<hear_about_us> hear_about_us { get; set; }
        public virtual DbSet<hear_about_us_desc> hear_about_us_desc { get; set; }
        public virtual DbSet<IDGen_Table> IDGen_Table { get; set; }
        public virtual DbSet<Inventory_Adjustment> Inventory_Adjustment { get; set; }
        public virtual DbSet<Inventory_Adjustment_Type> Inventory_Adjustment_Type { get; set; }
        public virtual DbSet<Java_Errors> Java_Errors { get; set; }
        public virtual DbSet<Kit_Type> Kit_Type { get; set; }
        public virtual DbSet<language_desc> language_desc { get; set; }
        public virtual DbSet<language> languages { get; set; }
        public virtual DbSet<lead> leads { get; set; }
        public virtual DbSet<lead_activity> lead_activity { get; set; }
        public virtual DbSet<Lead_Activity_Closing_Reason> Lead_Activity_Closing_Reason { get; set; }
        public virtual DbSet<Lead_Activity_Type> Lead_Activity_Type { get; set; }
        public virtual DbSet<Lead_Channel> Lead_Channel { get; set; }
        public virtual DbSet<Lead_Combinaisons> Lead_Combinaisons { get; set; }
        public virtual DbSet<Lead_Conditions> Lead_Conditions { get; set; }
        public virtual DbSet<Lead_Duplicates_Login> Lead_Duplicates_Login { get; set; }
        public virtual DbSet<lead_email_tracking> lead_email_tracking { get; set; }
        public virtual DbSet<lead_history> lead_history { get; set; }
        public virtual DbSet<Lead_Personalized_050403> Lead_Personalized_050403 { get; set; }
        public virtual DbSet<Lead_Priority> Lead_Priority { get; set; }
        public virtual DbSet<Lead_Promotion> Lead_Promotion { get; set; }
        public virtual DbSet<Lead_Qualification_Type> Lead_Qualification_Type { get; set; }
        public virtual DbSet<Lead_Status> Lead_Status { get; set; }
        public virtual DbSet<Lead_Visit> Lead_Visit { get; set; }
        public virtual DbSet<Local_Sponsor> Local_Sponsor { get; set; }
        public virtual DbSet<Local_Sponsor_Activity> Local_Sponsor_Activity { get; set; }
        public virtual DbSet<Local_Sponsor_Activity_Type> Local_Sponsor_Activity_Type { get; set; }
        public virtual DbSet<Local_Sponsor_Sales_Item> Local_Sponsor_Sales_Item { get; set; }
        public virtual DbSet<Local_Sponsor_Steps> Local_Sponsor_Steps { get; set; }
        public virtual DbSet<log_harmony_transfer_leads> log_harmony_transfer_leads { get; set; }
        public virtual DbSet<Mailing_Code> Mailing_Code { get; set; }
        public virtual DbSet<Mailing_Name> Mailing_Name { get; set; }
        public virtual DbSet<mke_replication_test> mke_replication_test { get; set; }
        public virtual DbSet<MSysConf> MSysConfs { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<orders_sale> orders_sale { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Organization_Class> Organization_Class { get; set; }
        public virtual DbSet<Organization_Status> Organization_Status { get; set; }
        public virtual DbSet<organization_type> organization_type { get; set; }
        public virtual DbSet<organization_type_desc> organization_type_desc { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<package_desc> package_desc { get; set; }
        public virtual DbSet<package_templates> package_templates { get; set; }
        public virtual DbSet<package1> packages1 { get; set; }
        public virtual DbSet<pap_client_type> pap_client_type { get; set; }
        public virtual DbSet<pap_product_category> pap_product_category { get; set; }
        public virtual DbSet<pap_product_type> pap_product_type { get; set; }
        public virtual DbSet<pap_suppressed_product_type> pap_suppressed_product_type { get; set; }
        public virtual DbSet<pap_transaction> pap_transaction { get; set; }
        public virtual DbSet<participant> participants { get; set; }
        public virtual DbSet<partner_commission> partner_commission { get; set; }
        public virtual DbSet<Partner_Commission_Range> Partner_Commission_Range { get; set; }
        public virtual DbSet<partner_contacts> partner_contacts { get; set; }
        public virtual DbSet<Partner_Fixed_Cost> Partner_Fixed_Cost { get; set; }
        public virtual DbSet<partner_group_types> partner_group_types { get; set; }
        public virtual DbSet<Partner_Lead_Commission> Partner_Lead_Commission { get; set; }
        public virtual DbSet<partner_packages> partner_packages { get; set; }
        public virtual DbSet<Partner_Sales_Commission> Partner_Sales_Commission { get; set; }
        public virtual DbSet<partner_web_details> partner_web_details { get; set; }
        public virtual DbSet<party_type> party_type { get; set; }
        public virtual DbSet<payment> payments { get; set; }
        public virtual DbSet<Payment_Audit> Payment_Audit { get; set; }
        public virtual DbSet<Payment_Entry_Stop_Date> Payment_Entry_Stop_Date { get; set; }
        public virtual DbSet<payment_method> payment_method { get; set; }
        public virtual DbSet<Payment_status> Payment_status { get; set; }
        public virtual DbSet<payment_term> payment_term { get; set; }
        public virtual DbSet<perfom_trace> perfom_trace { get; set; }
        public virtual DbSet<phone_number_tracking> phone_number_tracking { get; set; }
        public virtual DbSet<po_status> po_status { get; set; }
        public virtual DbSet<postal_address> postal_address { get; set; }
        public virtual DbSet<Postponed_Sale> Postponed_Sale { get; set; }
        public virtual DbSet<Postponed_Status> Postponed_Status { get; set; }
        public virtual DbSet<Price_Range> Price_Range { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<product_business_rule> product_business_rule { get; set; }
        public virtual DbSet<product_business_rule_profit_range> product_business_rule_profit_range { get; set; }
        public virtual DbSet<product_business_rule_shipping_fee> product_business_rule_shipping_fee { get; set; }
        public virtual DbSet<product_class> product_class { get; set; }
        public virtual DbSet<product_class_desc> product_class_desc { get; set; }
        public virtual DbSet<product_desc> product_desc { get; set; }
        public virtual DbSet<Product_Quantity> Product_Quantity { get; set; }
        public virtual DbSet<Production_Status> Production_Status { get; set; }
        public virtual DbSet<products_packages> products_packages { get; set; }
        public virtual DbSet<profit_range> profit_range { get; set; }
        public virtual DbSet<promokit> promokits { get; set; }
        public virtual DbSet<Promotion_Code> Promotion_Code { get; set; }
        public virtual DbSet<Promotion_Cost> Promotion_Cost { get; set; }
        public virtual DbSet<Promotion_Group> Promotion_Group { get; set; }
        public virtual DbSet<Promotion_Group_Promotion> Promotion_Group_Promotion { get; set; }
        public virtual DbSet<promotional_kit> promotional_kit { get; set; }
        public virtual DbSet<Proposal> Proposals { get; set; }
        public virtual DbSet<QSP_Program> QSP_Program { get; set; }
        public virtual DbSet<Question_Entry_Form> Question_Entry_Form { get; set; }
        public virtual DbSet<Reason> Reasons { get; set; }
        public virtual DbSet<Referee> Referees { get; set; }
        public virtual DbSet<Replication_Monitoring> Replication_Monitoring { get; set; }
        public virtual DbSet<Req_Decision> Req_Decision { get; set; }
        public virtual DbSet<Req_Employees> Req_Employees { get; set; }
        public virtual DbSet<Req_Language> Req_Language { get; set; }
        public virtual DbSet<Req_Priority> Req_Priority { get; set; }
        public virtual DbSet<Req_Project_Type> Req_Project_Type { get; set; }
        public virtual DbSet<Req_Request> Req_Request { get; set; }
        public virtual DbSet<Req_Request_Type> Req_Request_Type { get; set; }
        public virtual DbSet<sale> sales { get; set; }
        public virtual DbSet<Sale_Audit> Sale_Audit { get; set; }
        public virtual DbSet<sale_carrier_shipping_status> sale_carrier_shipping_status { get; set; }
        public virtual DbSet<sale_to_add> sale_to_add { get; set; }
        public virtual DbSet<Sale_To_Local_Sponsor> Sale_To_Local_Sponsor { get; set; }
        public virtual DbSet<Sale_Zip_Code> Sale_Zip_Code { get; set; }
        public virtual DbSet<Sales_Change_Log> Sales_Change_Log { get; set; }
        public virtual DbSet<sales_constraints> sales_constraints { get; set; }
        public virtual DbSet<sales_item> sales_item { get; set; }
        public virtual DbSet<Sales_Item_Coupon_Sheet> Sales_Item_Coupon_Sheet { get; set; }
        public virtual DbSet<sales_item_to_add> sales_item_to_add { get; set; }
        public virtual DbSet<Sales_Status> Sales_Status { get; set; }
        public virtual DbSet<salutation> salutations { get; set; }
        public virtual DbSet<salutation_desc> salutation_desc { get; set; }
        public virtual DbSet<Sample> Samples { get; set; }
        public virtual DbSet<SC_SECTION> SC_SECTION { get; set; }
        public virtual DbSet<scratch_book> scratch_book { get; set; }
        public virtual DbSet<scratch_book_commission> scratch_book_commission { get; set; }
        public virtual DbSet<scratch_book_price_info> scratch_book_price_info { get; set; }
        public virtual DbSet<service_type> service_type { get; set; }
        public virtual DbSet<shipping_fee> shipping_fee { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<special_offer> special_offer { get; set; }
        public virtual DbSet<Sponsor_Consultant> Sponsor_Consultant { get; set; }
        public virtual DbSet<Sport_Association> Sport_Association { get; set; }
        public virtual DbSet<SS_Drop_Box> SS_Drop_Box { get; set; }
        public virtual DbSet<SS_Drop_Box_Package> SS_Drop_Box_Package { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<State_Tax> State_Tax { get; set; }
        public virtual DbSet<subdivision> subdivisions { get; set; }
        public virtual DbSet<supplier> suppliers { get; set; }
        public virtual DbSet<sync_service_log> sync_service_log { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Targeted_Market> Targeted_Market { get; set; }
        public virtual DbSet<targeted_market_type> targeted_market_type { get; set; }
        public virtual DbSet<Tax_Table> Tax_Table { get; set; }
        public virtual DbSet<Temp_Lead> Temp_Lead { get; set; }
        public virtual DbSet<Temp_Sale_Zip_Code> Temp_Sale_Zip_Code { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<Template_Set> Template_Set { get; set; }
        public virtual DbSet<territory> territories { get; set; }
        public virtual DbSet<Territory_Def> Territory_Def { get; set; }
        public virtual DbSet<title> titles { get; set; }
        public virtual DbSet<title_desc> title_desc { get; set; }
        public virtual DbSet<transfer_status> transfer_status { get; set; }
        public virtual DbSet<Unassigned_Consultant> Unassigned_Consultant { get; set; }
        public virtual DbSet<Unassigned_Consultant_Sale> Unassigned_Consultant_Sale { get; set; }
        public virtual DbSet<UnAssignLogin> UnAssignLogins { get; set; }
        public virtual DbSet<Web_Site> Web_Site { get; set; }
        public virtual DbSet<WFC_Import> WFC_Import { get; set; }
        public virtual DbSet<WFC_Import_Payments> WFC_Import_Payments { get; set; }
        public virtual DbSet<WFC_Logs> WFC_Logs { get; set; }
        public virtual DbSet<WFC_Payment_Logs> WFC_Payment_Logs { get; set; }
        public virtual DbSet<ARDHISP> ARDHISPs { get; set; }
        public virtual DbSet<BeFree> BeFrees { get; set; }
        public virtual DbSet<BeFree_History> BeFree_History { get; set; }
        public virtual DbSet<crm_users> crm_users { get; set; }
        public virtual DbSet<fr_pendingleads> fr_pendingleads { get; set; }
        public virtual DbSet<Lead_Activity_copy> Lead_Activity_copy { get; set; }
        public virtual DbSet<lead_interest_product_class> lead_interest_product_class { get; set; }
        public virtual DbSet<mag_lead> mag_lead { get; set; }
        public virtual DbSet<pap_partner_product_type_overide> pap_partner_product_type_overide { get; set; }
        public virtual DbSet<partner_from_esubs_20080414> partner_from_esubs_20080414 { get; set; }
        public virtual DbSet<Promotion_old> Promotion_old { get; set; }
        public virtual DbSet<rs_lastcommit> rs_lastcommit { get; set; }
        public virtual DbSet<rs_threads> rs_threads { get; set; }
        public virtual DbSet<Sponsor_Found_Stool> Sponsor_Found_Stool { get; set; }
        public virtual DbSet<temp_bounce> temp_bounce { get; set; }
        public virtual DbSet<temp_unsub> temp_unsub { get; set; }
        public virtual DbSet<tmp_total_adjustment> tmp_total_adjustment { get; set; }
        public virtual DbSet<tmp_total_deposit> tmp_total_deposit { get; set; }
        public virtual DbSet<xavier> xaviers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.partner_name)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.partner_path)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.esubs_url)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.estore_url)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.free_kit_url)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.logo)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.phone_number)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.email_ext)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .HasMany(e => e.C_tbd_promotion)
                .WithRequired(e => e.C_tbd_partner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.promotion_type_code)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.script_name)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.contact_name)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.visibility)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.tracking_serial)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.cookie_content)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.keyword)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion_type>()
                .Property(e => e.Promotion_Type_Code)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion_type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion_type>()
                .Property(e => e.Default_Commission_Rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<C_tbd_promotion_type>()
                .HasMany(e => e.C_tbd_promotion)
                .WithRequired(e => e.C_tbd_promotion_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<C_tbd_promotion_type>()
                .HasMany(e => e.Default_Consultant_Rate)
                .WithRequired(e => e.C_tbd_promotion_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<accounting_class>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<accounting_class>()
                .HasMany(e => e.accounting_class_shipping_fees)
                .WithRequired(e => e.accounting_class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<accounting_class_shipping_fees>()
                .Property(e => e.min_amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<accounting_class_shipping_fees>()
                .Property(e => e.max_amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<accounting_period_result>()
                .Property(e => e.amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<accounting_period_result>()
                .Property(e => e.budgeted_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<address_zone>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Adjustment>()
                .Property(e => e.Adjustment_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Adjustment>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Adjustment>()
                .Property(e => e.Adjustment_On_Shipping)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Adjustment>()
                .Property(e => e.Adjustment_On_Taxes)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Adjustment>()
                .Property(e => e.Adjustment_On_Sale_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Adjustment_Audit>()
                .Property(e => e.AUDIT_OPERATION)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Adjustment_Audit>()
                .Property(e => e.HOST)
                .IsUnicode(false);

            modelBuilder.Entity<Adjustment_Audit>()
                .Property(e => e.AUDIT_USERID)
                .IsUnicode(false);

            modelBuilder.Entity<Adjustment_Audit>()
                .Property(e => e.Adjustment_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Adjustment_Audit>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Adjustment_Audit>()
                .Property(e => e.Adjustment_On_Shipping)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Adjustment_Audit>()
                .Property(e => e.Adjustment_On_Taxes)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Adjustment_Audit>()
                .Property(e => e.Adjustment_On_Sale_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<advertisement>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<advertisement>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<Advertiser>()
                .Property(e => e.Advertiser_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Advertiser_Partner>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support>()
                .Property(e => e.Web_Site)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support>()
                .Property(e => e.Ordering_Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support>()
                .Property(e => e.Magazine_Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Advertising_Support>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support>()
                .HasMany(e => e.Advertising_Support_Contact)
                .WithRequired(e => e.Advertising_Support)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Advertising_Support>()
                .HasMany(e => e.Targeted_Market)
                .WithRequired(e => e.Advertising_Support)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Advertising_Support>()
                .HasMany(e => e.Competitor_Advertising)
                .WithMany(e => e.Advertising_Support)
                .Map(m => m.ToTable("Competitor_Advertising_Support").MapLeftKey("Advertising_Support_ID").MapRightKey("Competitor_Advertising_ID"));

            modelBuilder.Entity<Advertising_Support_Contact>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support_Contact>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support_Contact>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support_Contact>()
                .Property(e => e.Fax_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support_Contact>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support_Type>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support_Type>()
                .HasMany(e => e.Advertising_Support)
                .WithRequired(e => e.Advertising_Support_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Advertisment_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Alias_Country_Code>()
                .Property(e => e.Input_Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Alias_Country_Code>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Alias_Promotion>()
                .Property(e => e.Cookie_Content)
                .IsUnicode(false);

            modelBuilder.Entity<Alias_State>()
                .Property(e => e.Input_State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Alias_State>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Applicable_Adjustment_Tax>()
                .Property(e => e.Tax_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Applicable_Adjustment_Tax>()
                .Property(e => e.Tax_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Applicable_Tax>()
                .Property(e => e.Tax_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Applicable_Tax>()
                .Property(e => e.Tax_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Applicable_Tax_To_Add>()
                .Property(e => e.Tax_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Applicable_Tax_To_Add>()
                .Property(e => e.Tax_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<application>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<application>()
                .HasMany(e => e.pap_transaction)
                .WithRequired(e => e.application)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AR_Activity>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<AR_Activity_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AR_Activity_Type>()
                .HasMany(e => e.AR_Activity)
                .WithRequired(e => e.AR_Activity_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AR_Consultant>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AR_Consultant>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<AR_Consultant>()
                .Property(e => e.Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<AR_Consultant>()
                .Property(e => e.Nt_Login)
                .IsUnicode(false);

            modelBuilder.Entity<AR_Status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Area_Manager>()
                .Property(e => e.Area_Manager_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Area_Manager>()
                .HasMany(e => e.Field_Sales_Manager)
                .WithOptional(e => e.Area_Manager)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Associate_Mentor>()
                .HasMany(e => e.Associate_Mentor_Comment)
                .WithRequired(e => e.Associate_Mentor)
                .HasForeignKey(e => new { e.Associate_ID, e.Mentor_ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Associate_Mentor>()
                .HasMany(e => e.associate_mentor_commission)
                .WithRequired(e => e.Associate_Mentor)
                .HasForeignKey(e => new { e.associate_id, e.mentor_id })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Associate_Mentor_Comment>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<associate_mentor_commission>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<Automaton>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Automaton>()
                .HasMany(e => e.Automaton_State)
                .WithRequired(e => e.Automaton)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Automaton_Function>()
                .Property(e => e.Function_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Automaton_Function>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Automaton_Function>()
                .HasMany(e => e.Automaton_Transition_Function)
                .WithRequired(e => e.Automaton_Function)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Automaton_State>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Automaton_State>()
                .HasMany(e => e.Automaton_Transition)
                .WithRequired(e => e.Automaton_State)
                .HasForeignKey(e => new { e.Automaton_Id, e.State_To_Id })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Automaton_Transition>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Automaton_Transition>()
                .HasMany(e => e.Automaton_Transition_Function)
                .WithRequired(e => e.Automaton_Transition)
                .HasForeignKey(e => new { e.Automaton_Id, e.State_To_Id, e.State_From_Id })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Street_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.Bank_Account)
                .WithRequired(e => e.Bank)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bank_Account>()
                .Property(e => e.Bank_Account_No)
                .IsUnicode(false);

            modelBuilder.Entity<Bank_Account>()
                .Property(e => e.Currency_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Bank_Account>()
                .Property(e => e.GL_Account_No)
                .IsUnicode(false);

            modelBuilder.Entity<Bank_Account>()
                .HasMany(e => e.deposits)
                .WithRequired(e => e.Bank_Account)
                .HasForeignKey(e => new { e.bank_id, e.bank_account_no })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<best_time_call>()
                .Property(e => e.best_time_call_desc)
                .IsUnicode(false);

            modelBuilder.Entity<best_time_call>()
                .HasMany(e => e.best_time_call_desc1)
                .WithRequired(e => e.best_time_call)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<best_time_call_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.Billing_Company_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.Billing_Company_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.Street_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.City_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.Telephone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.Web)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.Logo)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.Invoice_Title)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.Invoice_Footer)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.fax_number)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Company>()
                .Property(e => e.logo_path)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.Promotion)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Brand_Coupon_Sheet)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Local_Sponsor)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.special_offer)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<brochures_images>()
                .Property(e => e.base_filename)
                .IsUnicode(false);

            modelBuilder.Entity<brochures_images>()
                .Property(e => e.file_ext)
                .IsUnicode(false);

            modelBuilder.Entity<Business_Rule>()
                .Property(e => e.Rule_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Business_Rule>()
                .Property(e => e.Module_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Business_Rule>()
                .Property(e => e.Form_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Business_Rule>()
                .Property(e => e.Access_Sub_Name)
                .IsUnicode(false);

            modelBuilder.Entity<campaign_reason>()
                .Property(e => e.campaign_reason_desc)
                .IsUnicode(false);

            modelBuilder.Entity<campaign_reason>()
                .HasMany(e => e.campaign_reason_desc1)
                .WithRequired(e => e.campaign_reason)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<campaign_reason_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Cancelation_Reason>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<carrier>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<carrier>()
                .Property(e => e.SCAC)
                .IsUnicode(false);

            modelBuilder.Entity<carrier>()
                .HasMany(e => e.accounting_class)
                .WithRequired(e => e.carrier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<carrier_shipping_option>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<carrier_shipping_option>()
                .HasMany(e => e.accounting_class)
                .WithRequired(e => e.carrier_shipping_option)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<carrier_shipping_status>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<carrier_shipping_status>()
                .HasMany(e => e.sale_carrier_shipping_status)
                .WithRequired(e => e.carrier_shipping_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<charge>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<charge>()
                .Property(e => e.fulf_charge_id)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.client_sequence_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.organization_class_code)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.channel_code)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.salutation)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.organization)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.day_phone)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.day_time_call)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.evening_phone)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.evening_time_call)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.fax)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.extra_comment)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.day_phone_ext)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.evening_phone_ext)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.other_phone)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.other_phone_ext)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .HasMany(e => e.client_activity)
                .WithRequired(e => e.client)
                .HasForeignKey(e => new { e.client_sequence_code, e.client_id })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<client>()
                .HasMany(e => e.client_address)
                .WithRequired(e => e.client)
                .HasForeignKey(e => new { e.client_sequence_code, e.client_id })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<client_activity>()
                .Property(e => e.client_sequence_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<client_activity>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<client_activity_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<client_activity_type>()
                .HasMany(e => e.client_activity)
                .WithRequired(e => e.client_activity_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.client_sequence_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.address_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.street_address)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.state_code)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.country_code)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.attention_of)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.matching_code)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.phone_1)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.phone_2)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<client_address_type>()
                .Property(e => e.address_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<client_address_type>()
                .Property(e => e.address_type_desc)
                .IsUnicode(false);

            modelBuilder.Entity<client_address_type>()
                .HasMany(e => e.client_address)
                .WithRequired(e => e.client_address_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<client_sequence>()
                .Property(e => e.client_sequence_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<client_sequence>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<client_sequence>()
                .HasMany(e => e.clients)
                .WithRequired(e => e.client_sequence)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Collection_Status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Earning>()
                .Property(e => e.Product_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Earning>()
                .Property(e => e.Payment_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Commission_Earning>()
                .Property(e => e.Commission_Amount)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Earning>()
                .Property(e => e.Commission_Rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Commission_Earning>()
                .Property(e => e.Sales_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Commission_Earning>()
                .Property(e => e.Currency_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Earning>()
                .Property(e => e.Exchange_Rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Commission_Earning>()
                .Property(e => e.Commission_Amount_Ca)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding>()
                .Property(e => e.Payment_Term)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding>()
                .Property(e => e.Organization)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding>()
                .Property(e => e.Day_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding>()
                .Property(e => e.Outstanding_Amount)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding>()
                .Property(e => e.Currency_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding>()
                .Property(e => e.Outstanding_Commission)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding_History>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding_History>()
                .Property(e => e.Payment_Term)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding_History>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding_History>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding_History>()
                .Property(e => e.Organization)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding_History>()
                .Property(e => e.Day_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding_History>()
                .Property(e => e.Outstanding_Amount)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding_History>()
                .Property(e => e.Currency_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Outstanding_History>()
                .Property(e => e.Outstanding_Commission)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Paid>()
                .Property(e => e.Sales_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Commission_Paid>()
                .Property(e => e.Consultant_Commission_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Commission_Rate>()
                .Property(e => e.Commission_Rate_Free)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Commission_Rate>()
                .Property(e => e.Commission_Rate_No_Free)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Commission_Table>()
                .Property(e => e.Promotion_Type_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Table>()
                .Property(e => e.Channel_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Commission_Table>()
                .Property(e => e.Commission_Rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Competitor>()
                .Property(e => e.Business_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Competitor>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Competitor>()
                .HasMany(e => e.Competitor_Advertising)
                .WithRequired(e => e.Competitor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Competitor_Advertising>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Competitor_Advertising>()
                .Property(e => e.Publicity_Duration)
                .IsUnicode(false);

            modelBuilder.Entity<Competitor_Advertising>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<conciliation>()
                .Property(e => e.invoice_number)
                .IsUnicode(false);

            modelBuilder.Entity<Confirmation_Method>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.client_sequence_code)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.nt_login)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.phone_extension)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.home_phone)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.work_phone)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.fax_number)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.toll_free_phone)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.mobile_phone)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.pager_phone)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.default_proposal_text)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<consultant>()
                .HasMany(e => e.AR_Activity)
                .WithRequired(e => e.consultant)
                .HasForeignKey(e => e.AR_Consultant_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<consultant>()
                .HasMany(e => e.Associate_Mentor)
                .WithRequired(e => e.consultant)
                .HasForeignKey(e => e.Associate_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<consultant>()
                .HasMany(e => e.Associate_Mentor1)
                .WithRequired(e => e.consultant1)
                .HasForeignKey(e => e.Mentor_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<consultant>()
                .HasMany(e => e.clients)
                .WithOptional(e => e.consultant)
                .HasForeignKey(e => e.csr_consultant_id);

            modelBuilder.Entity<consultant>()
                .HasMany(e => e.Commission_Paid)
                .WithRequired(e => e.consultant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<consultant>()
                .HasMany(e => e.Commission_Rate)
                .WithRequired(e => e.consultant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<consultant>()
                .HasMany(e => e.consultant_address)
                .WithRequired(e => e.consultant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<consultant>()
                .HasMany(e => e.Default_Consultant_Rate)
                .WithRequired(e => e.consultant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<consultant>()
                .HasMany(e => e.leads)
                .WithOptional(e => e.consultant)
                .HasForeignKey(e => e.assigner_id);

            modelBuilder.Entity<consultant>()
                .HasMany(e => e.leads1)
                .WithRequired(e => e.consultant1)
                .HasForeignKey(e => e.consultant_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<consultant_address>()
                .Property(e => e.country_code)
                .IsUnicode(false);

            modelBuilder.Entity<consultant_address>()
                .Property(e => e.state_code)
                .IsUnicode(false);

            modelBuilder.Entity<consultant_address>()
                .Property(e => e.street_address)
                .IsUnicode(false);

            modelBuilder.Entity<consultant_address>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<consultant_address>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<consultant_transfer_status>()
                .Property(e => e.consultant_transfer_status_desc)
                .IsUnicode(false);

            modelBuilder.Entity<consultant_transfer_status>()
                .HasMany(e => e.consultants)
                .WithRequired(e => e.consultant_transfer_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Street_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Conversion_Rate_Table>()
                .Property(e => e.Currency_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Conversion_Rate_Table>()
                .Property(e => e.Conversion_Rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<cost_range>()
                .Property(e => e.margin_plan)
                .HasPrecision(15, 4);

            modelBuilder.Entity<country>()
                .Property(e => e.country_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .Property(e => e.country_name)
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .Property(e => e.long_country_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .Property(e => e.numeric_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .Property(e => e.currency_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .HasMany(e => e.country_names)
                .WithRequired(e => e.country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country1>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Country1>()
                .Property(e => e.Country_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Country1>()
                .Property(e => e.Currency_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Country1>()
                .HasMany(e => e.Alias_Country_Code)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country1>()
                .HasMany(e => e.Billing_Company)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country1>()
                .HasMany(e => e.client_address)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country1>()
                .HasMany(e => e.FSM_Address)
                .WithOptional(e => e.Country)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Country1>()
                .HasMany(e => e.Organizations)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country1>()
                .HasMany(e => e.sale_to_add)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.ssn_country_code);

            modelBuilder.Entity<Country1>()
                .HasMany(e => e.scratch_book_price_info)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<country_names>()
                .Property(e => e.country_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<country_names>()
                .Property(e => e.country_name)
                .IsUnicode(false);

            modelBuilder.Entity<Coupon_Sheet>()
                .Property(e => e.Product_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Coupon_Sheet>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Coupon_Sheet>()
                .HasMany(e => e.Brand_Coupon_Sheet)
                .WithRequired(e => e.Coupon_Sheet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coupon_Sheet>()
                .HasMany(e => e.Sales_Item_Coupon_Sheet)
                .WithRequired(e => e.Coupon_Sheet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Credit_Approval_Method>()
                .Property(e => e.Description)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_refund_request>()
                .Property(e => e.status_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_refund_request>()
                .Property(e => e.refund_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<credit_card_refund_request_status>()
                .Property(e => e.status_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_refund_request_status>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_types>()
                .Property(e => e.credit_card_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_types>()
                .Property(e => e.credit_card_image)
                .IsUnicode(false);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.amount_requested)
                .HasPrecision(19, 4);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.amount_approved)
                .HasPrecision(19, 4);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.mid_init)
                .IsUnicode(false);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.zip)
                .IsUnicode(false);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.ssn)
                .IsUnicode(false);

            modelBuilder.Entity<credit_check_request>()
                .Property(e => e.credit_report)
                .IsUnicode(false);

            modelBuilder.Entity<credit_check_status>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.account_name)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.total_sold)
                .HasPrecision(18, 0);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.qsp_cust_billing_matching_code)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.qsp_cust_shipping_matching_code)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.qsp_account_matching_code)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.fm_id)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.home_phone)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.work_phone)
                .IsUnicode(false);

            modelBuilder.Entity<crm_static_past3seasons_new>()
                .Property(e => e.mobile_phone)
                .IsUnicode(false);

            modelBuilder.Entity<culture>()
                .Property(e => e.culture_name)
                .IsUnicode(false);

            modelBuilder.Entity<culture1>()
                .Property(e => e.country_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<culture1>()
                .Property(e => e.culture_name)
                .IsUnicode(false);

            modelBuilder.Entity<culture1>()
                .Property(e => e.display_name)
                .IsUnicode(false);

            modelBuilder.Entity<culture1>()
                .Property(e => e.culture_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<culture1>()
                .Property(e => e.iso_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<culture1>()
                .HasMany(e => e.scratch_book)
                .WithMany(e => e.cultures)
                .Map(m => m.ToTable("products_cultures").MapLeftKey("culture_id").MapRightKey("product_id"));

            modelBuilder.Entity<customer_status>()
                .Property(e => e.customer_status_desc)
                .IsUnicode(false);

            modelBuilder.Entity<Default_Consultant_Rate>()
                .Property(e => e.Promotion_Type_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Default_Consultant_Rate>()
                .Property(e => e.Default_Commission_Rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Department>()
                .Property(e => e.Department_name)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.General_Comment)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<deposit>()
                .Property(e => e.bank_account_no)
                .IsUnicode(false);

            modelBuilder.Entity<deposit>()
                .HasMany(e => e.Deposit_Item)
                .WithRequired(e => e.deposit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Destination>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<Destinations2>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<Detailed_Promotion>()
                .Property(e => e.Promotion_Type_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Detailed_Promotion>()
                .Property(e => e.Target_Age_Group_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Detailed_Promotion>()
                .Property(e => e.Target_Gender_Group_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Detailed_Promotion>()
                .Property(e => e.Target_Group_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Detailed_Promotion>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<division>()
                .Property(e => e.division_name)
                .IsUnicode(false);

            modelBuilder.Entity<division>()
                .Property(e => e.logo)
                .IsUnicode(false);

            modelBuilder.Entity<division>()
                .Property(e => e.short_name)
                .IsUnicode(false);

            modelBuilder.Entity<division>()
                .HasMany(e => e.advertisements)
                .WithRequired(e => e.division)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<division>()
                .HasMany(e => e.clients)
                .WithRequired(e => e.division)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<division>()
                .HasMany(e => e.consultants)
                .WithRequired(e => e.division)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<division>()
                .HasMany(e => e.leads)
                .WithRequired(e => e.division)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Channel_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Salutation)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Organization)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Street_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Day_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Day_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Evening_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Day_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Evening_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Rejection_reason)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Other_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Cookie_Content)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Group_Web_Site)
                .IsUnicode(false);

            modelBuilder.Entity<Double_Lead>()
                .Property(e => e.Other_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.property)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Admin>()
                .Property(e => e.UID)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Admin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Campaign>()
                .Property(e => e.Group_Name)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Campaign>()
                .Property(e => e.Financial_Goal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<EFO_Campaign>()
                .Property(e => e.Fund_Raising_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Campaign>()
                .Property(e => e.Background_Info)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Campaign>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Campaign>()
                .Property(e => e.Account_Number)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Campaign>()
                .HasMany(e => e.EFO_Campaign_Status)
                .WithRequired(e => e.EFO_Campaign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFO_Campaign>()
                .HasMany(e => e.EFO_Participant)
                .WithRequired(e => e.EFO_Campaign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFO_Campaign_Image>()
                .Property(e => e.Image_Catalog_Path)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Campaign_Image>()
                .Property(e => e.Image_Catalog_Path_Sel)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Catalog_Category>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Catalog_Category>()
                .HasMany(e => e.EFO_Campaign_Image)
                .WithRequired(e => e.EFO_Catalog_Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFO_Email_Type>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Email_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Email_Type>()
                .HasMany(e => e.EFO_Tag)
                .WithRequired(e => e.EFO_Email_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFO_Email_Type>()
                .HasMany(e => e.EFO_Status)
                .WithMany(e => e.EFO_Email_Type)
                .Map(m => m.ToTable("EFO_Status_Email").MapLeftKey("Email_Type_ID").MapRightKey("Status_ID"));

            modelBuilder.Entity<EFO_Group_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Item>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Item>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<EFO_Item>()
                .Property(e => e.Amount2Supplier)
                .HasPrecision(10, 2);

            modelBuilder.Entity<EFO_Item>()
                .Property(e => e.Amount2Group)
                .HasPrecision(10, 2);

            modelBuilder.Entity<EFO_Item>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Item>()
                .HasMany(e => e.EFO_Sale_Item)
                .WithRequired(e => e.EFO_Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFO_Message>()
                .Property(e => e.From_Name)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Message>()
                .Property(e => e.From_Email)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Message>()
                .Property(e => e.To_Name)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Message>()
                .Property(e => e.To_Email)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Message>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Message>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Message>()
                .Property(e => e.Content_Type)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Organizer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Organizer>()
                .Property(e => e.User_Name)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Organizer>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Organizer>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Organizer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Organizer>()
                .Property(e => e.Best_Time_To_Call)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Organizer>()
                .Property(e => e.Evening_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Organizer>()
                .Property(e => e.Day_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Organizer>()
                .Property(e => e.Fax_Number)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Organizer>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Participant>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Participant>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Participant>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Participant>()
                .HasMany(e => e.EFO_Message)
                .WithRequired(e => e.EFO_Participant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFO_Participant>()
                .HasMany(e => e.EFO_Supporter)
                .WithRequired(e => e.EFO_Participant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.Amount_To_Group)
                .HasPrecision(10, 4);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.Amount_To_Supplier)
                .HasPrecision(10, 4);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.Amount)
                .HasPrecision(10, 4);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.Delivery_Address)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.Delivery_City)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.Delivery_Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.Card_Name)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.Card_Address)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Sale>()
                .Property(e => e.Transaction_ID)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Sale>()
                .HasMany(e => e.EFO_Sale_Item)
                .WithRequired(e => e.EFO_Sale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFO_Sale_Item>()
                .Property(e => e.Quantity)
                .HasPrecision(4, 0);

            modelBuilder.Entity<EFO_Status>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Status>()
                .HasMany(e => e.EFO_Campaign_Status)
                .WithRequired(e => e.EFO_Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFO_Supporter>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Supporter>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Supporter>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Supporter>()
                .Property(e => e.Relation)
                .IsUnicode(false);

            modelBuilder.Entity<EFO_Supporter>()
                .HasMany(e => e.EFO_Sale)
                .WithRequired(e => e.EFO_Supporter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFO_Tag>()
                .Property(e => e.Tag_Name)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.Organization_Name)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.Promotion_Description)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.Lead_Activity_Detail)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.Lead_Comment)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.phone_extension)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.Promotion_Type)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.C2ndPhone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<eFR_Lead>()
                .Property(e => e.C2ndPhone_Extension)
                .IsUnicode(false);

            modelBuilder.Entity<Efr_Lead_Activity>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<EMail_iwon1>()
                .Property(e => e.GoodEmail)
                .IsUnicode(false);

            modelBuilder.Entity<EMail_iwon2>()
                .Property(e => e.GoodEmail)
                .IsUnicode(false);

            modelBuilder.Entity<EMail_PaulStanton>()
                .Property(e => e.GoodEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Entry_Form>()
                .Property(e => e.Entry_Form_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Entry_Form>()
                .Property(e => e.Master_Template)
                .IsUnicode(false);

            modelBuilder.Entity<Entry_Form>()
                .Property(e => e.Content_Template)
                .IsUnicode(false);

            modelBuilder.Entity<Entry_Form>()
                .HasMany(e => e.Question_Entry_Form)
                .WithRequired(e => e.Entry_Form)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ext_sales_status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.fedex_uid)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.company_name)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.contact_name)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.address_line_1)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.address_line_2)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.province_state)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.zip_postal_code)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.extention)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.tax_id_ssn)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.shipalert_email_address)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.shipalert_email_message)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.sevice_level)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.inter_part_description)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.inter_unit_value)
                .HasPrecision(15, 4);

            modelBuilder.Entity<fedex>()
                .Property(e => e.inter_currency)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.inter_unit_of_measure)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.inter_country_of_manufacture)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.inter_part_number)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.inter_marks_number)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.inter_sku_upc_item)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.inter_tracking_number)
                .IsUnicode(false);

            modelBuilder.Entity<fedex>()
                .Property(e => e.inter_shipping_quote)
                .HasPrecision(15, 4);

            modelBuilder.Entity<fedex>()
                .Property(e => e.cod_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.QSP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.Home_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.Work_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.Fax_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.Toll_Free_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.Mobile_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.Pager_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .Property(e => e.Region)
                .IsUnicode(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .HasMany(e => e.FSM_Address)
                .WithRequired(e => e.Field_Sales_Manager)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Field_Sales_Manager>()
                .HasMany(e => e.Organizations)
                .WithOptional(e => e.Field_Sales_Manager)
                .HasForeignKey(e => e.Agent_ID);

            modelBuilder.Entity<Flag_Pole>()
                .Property(e => e.MDR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Flag_Pole>()
                .HasMany(e => e.Organizations)
                .WithOptional(e => e.Flag_Pole)
                .WillCascadeOnDelete();

            modelBuilder.Entity<FSM_Address>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<FSM_Address>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<FSM_Address>()
                .Property(e => e.FSM_Address_Type)
                .IsUnicode(false);

            modelBuilder.Entity<FSM_Address>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<FSM_Address>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<FSM_Address>()
                .Property(e => e.Street_Address)
                .IsUnicode(false);

            modelBuilder.Entity<General_Comment>()
                .Property(e => e.General_Comment1)
                .IsUnicode(false);

            modelBuilder.Entity<General_Comment>()
                .Property(e => e.User_Name)
                .IsUnicode(false);

            modelBuilder.Entity<GL_Table>()
                .Property(e => e.GL_Code)
                .IsUnicode(false);

            modelBuilder.Entity<GL_Table>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<GL_Table>()
                .Property(e => e.GL_Account_No)
                .IsUnicode(false);

            modelBuilder.Entity<GL_Table>()
                .Property(e => e.Debit_Credit)
                .IsUnicode(false);

            modelBuilder.Entity<Grabber>()
                .Property(e => e.Grabber_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<group_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<group_type>()
                .HasMany(e => e.group_type_desc)
                .WithRequired(e => e.group_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<group_type>()
                .HasMany(e => e.targeted_market_type)
                .WithRequired(e => e.group_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<group_type_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<harmony_list_transfer>()
                .Property(e => e.list_name)
                .IsUnicode(false);

            modelBuilder.Entity<harmony_list_transfer>()
                .Property(e => e.list_desc)
                .IsUnicode(false);

            modelBuilder.Entity<hear_about_us>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<hear_about_us>()
                .HasMany(e => e.hear_about_us_desc)
                .WithRequired(e => e.hear_about_us)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<hear_about_us>()
                .HasMany(e => e.leads)
                .WithRequired(e => e.hear_about_us)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<hear_about_us_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<IDGen_Table>()
                .Property(e => e.Context)
                .IsUnicode(false);

            modelBuilder.Entity<IDGen_Table>()
                .Property(e => e.ID_Name)
                .IsUnicode(false);

            modelBuilder.Entity<IDGen_Table>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory_Adjustment_Type>()
                .Property(e => e.Inventory_Adjustment_Type_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory_Adjustment_Type>()
                .HasMany(e => e.Inventory_Adjustment)
                .WithRequired(e => e.Inventory_Adjustment_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Java_Errors>()
                .Property(e => e.Class_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Java_Errors>()
                .Property(e => e.Error_Message)
                .IsUnicode(false);

            modelBuilder.Entity<Kit_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Kit_Type>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Kit_Type>()
                .HasMany(e => e.promotional_kit)
                .WithRequired(e => e.Kit_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language_desc>()
                .Property(e => e.language_name)
                .IsUnicode(false);

            modelBuilder.Entity<language>()
                .Property(e => e.language_name)
                .IsUnicode(false);

            modelBuilder.Entity<language>()
                .Property(e => e.long_language_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<language>()
                .Property(e => e.short_language_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.best_time_call_desc)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.campaign_reason_desc)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.country_names)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.group_type_desc)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.hear_about_us_desc)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.language_desc)
                .WithRequired(e => e.language)
                .HasForeignKey(e => e.display_language_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.language_desc1)
                .WithRequired(e => e.language1)
                .HasForeignKey(e => e.language_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.organization_type_desc)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.package_desc)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.partner_contacts)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.product_desc)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.salutation_desc)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.title_desc)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.channel_code)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.salutation)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.organization)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.street_address)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.state_code)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.country_code)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.day_phone)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.day_time_call)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.evening_phone)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.evening_time_call)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.fax)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.interests)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.day_phone_ext)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.evening_phone_ext)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.other_phone)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.group_web_site)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.cookie_content)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.other_closing_activity_reason)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.matching_code)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .Property(e => e.vif)
                .IsUnicode(false);

            modelBuilder.Entity<lead>()
                .HasMany(e => e.Efr_Lead_Activity)
                .WithRequired(e => e.lead)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lead>()
                .HasMany(e => e.lead_activity)
                .WithRequired(e => e.lead)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lead>()
                .HasMany(e => e.Lead_Promotion)
                .WithRequired(e => e.lead)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lead>()
                .HasMany(e => e.promotional_kit)
                .WithRequired(e => e.lead)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lead>()
                .HasMany(e => e.Referees)
                .WithRequired(e => e.lead)
                .HasForeignKey(e => e.Lead_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lead>()
                .HasMany(e => e.sale_to_add)
                .WithRequired(e => e.lead)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lead_activity>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Activity_Closing_Reason>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Activity_Closing_Reason>()
                .HasMany(e => e.leads)
                .WithRequired(e => e.Lead_Activity_Closing_Reason)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lead_Activity_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Activity_Type>()
                .HasMany(e => e.Efr_Lead_Activity)
                .WithRequired(e => e.Lead_Activity_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lead_Activity_Type>()
                .HasMany(e => e.lead_activity)
                .WithRequired(e => e.Lead_Activity_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lead_Channel>()
                .Property(e => e.Channel_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Channel>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Channel>()
                .HasMany(e => e.clients)
                .WithRequired(e => e.Lead_Channel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lead_Channel>()
                .HasMany(e => e.leads)
                .WithRequired(e => e.Lead_Channel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lead_Conditions>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Conditions>()
                .HasMany(e => e.Lead_Combinaisons)
                .WithRequired(e => e.Lead_Conditions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lead_Duplicates_Login>()
                .Property(e => e.DUPLICATES_FOUND)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Duplicates_Login>()
                .Property(e => e.RELATED_TABLE)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Duplicates_Login>()
                .Property(e => e.DETECTED_FIELDS)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Duplicates_Login>()
                .Property(e => e.FIELDS_VALUES)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Duplicates_Login>()
                .Property(e => e.NT_LOGIN)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Duplicates_Login>()
                .Property(e => e.MACHINE)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.channel_code)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.organization)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.street_address)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.state_code)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.country_code)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.day_phone)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.evening_phone)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.fax)
                .IsUnicode(false);

            modelBuilder.Entity<lead_history>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Personalized_050403>()
                .Property(e => e.GoodEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Priority>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Qualification_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Qualification_Type>()
                .HasMany(e => e.Lead_Combinaisons)
                .WithRequired(e => e.Lead_Qualification_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lead_Status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Status>()
                .HasMany(e => e.leads)
                .WithRequired(e => e.Lead_Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lead_Visit>()
                .Property(e => e.Channel_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Visit>()
                .HasMany(e => e.promotional_kit)
                .WithRequired(e => e.Lead_Visit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Salutation)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Middle_Initial)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Street_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.City_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Day_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Day_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Evening_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Evening_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Alternate_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Fax_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor_Activity>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor_Activity_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor_Activity_Type>()
                .HasMany(e => e.Local_Sponsor_Activity)
                .WithRequired(e => e.Local_Sponsor_Activity_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Local_Sponsor_Steps>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Local_Sponsor_Steps>()
                .HasMany(e => e.Local_Sponsor)
                .WithOptional(e => e.Local_Sponsor_Steps)
                .HasForeignKey(e => e.Local_Sponsor_Steps_Id);

            modelBuilder.Entity<log_harmony_transfer_leads>()
                .Property(e => e.list_name)
                .IsUnicode(false);

            modelBuilder.Entity<log_harmony_transfer_leads>()
                .Property(e => e.list_desc)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Code>()
                .Property(e => e.List_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Code>()
                .Property(e => e.Flyer_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Code>()
                .Property(e => e.Mailing_Code_Label)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.List_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.Contact_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.School_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.School_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.Fax_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Mailing_Name>()
                .Property(e => e.School_Type)
                .IsUnicode(false);

            modelBuilder.Entity<mke_replication_test>()
                .Property(e => e.test_value)
                .IsUnicode(false);

            modelBuilder.Entity<MSysConf>()
                .Property(e => e.CHValue)
                .IsUnicode(false);

            modelBuilder.Entity<MSysConf>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Newsletter>()
                .Property(e => e.Referrer)
                .IsUnicode(false);

            modelBuilder.Entity<Newsletter>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Newsletter>()
                .Property(e => e.Fullname)
                .IsUnicode(false);

            modelBuilder.Entity<Organization>()
                .Property(e => e.Organization_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Organization>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Organization>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Organization>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Organization>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Organization>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Organization_Class>()
                .Property(e => e.Organization_Class_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Organization_Class>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Organization_Class>()
                .HasMany(e => e.clients)
                .WithRequired(e => e.Organization_Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organization_Status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<organization_type>()
                .Property(e => e.organization_type_desc)
                .IsUnicode(false);

            modelBuilder.Entity<organization_type>()
                .HasMany(e => e.organization_type_desc1)
                .WithRequired(e => e.organization_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<organization_type_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.Package_Image)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.Package_Profit)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.Package_Web_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.Package_Title_Image)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.profit_min)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Package>()
                .Property(e => e.profit_max)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Package>()
                .Property(e => e.profit_default)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Package>()
                .HasMany(e => e.Price_Range)
                .WithRequired(e => e.Package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Package>()
                .HasMany(e => e.scratch_book)
                .WithRequired(e => e.Package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Package>()
                .HasMany(e => e.SS_Drop_Box_Package)
                .WithRequired(e => e.Package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.package_name)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.package_short_desc)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.package_long_desc)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.package_extra_desc)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.package_small_img)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.package_large_img)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.page_url)
                .IsUnicode(false);

            modelBuilder.Entity<package_templates>()
                .Property(e => e.package_template_desc)
                .IsUnicode(false);

            modelBuilder.Entity<package1>()
                .Property(e => e.package_name)
                .IsUnicode(false);

            modelBuilder.Entity<package1>()
                .HasMany(e => e.package_desc)
                .WithRequired(e => e.package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<package1>()
                .HasMany(e => e.packages1)
                .WithOptional(e => e.package)
                .HasForeignKey(e => e.parent_package_id);

            modelBuilder.Entity<package1>()
                .HasMany(e => e.partner_packages)
                .WithRequired(e => e.package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<package1>()
                .HasMany(e => e.products_packages)
                .WithRequired(e => e.package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pap_client_type>()
                .Property(e => e.ext_client_type_id)
                .IsUnicode(false);

            modelBuilder.Entity<pap_product_category>()
                .Property(e => e.product_category_code)
                .IsUnicode(false);

            modelBuilder.Entity<pap_product_category>()
                .Property(e => e.product_category_desc)
                .IsUnicode(false);

            modelBuilder.Entity<pap_product_category>()
                .HasMany(e => e.pap_partner_product_type_overide)
                .WithRequired(e => e.pap_product_category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pap_product_type>()
                .Property(e => e.ext_product_type_id)
                .IsUnicode(false);

            modelBuilder.Entity<pap_product_type>()
                .HasMany(e => e.pap_partner_product_type_overide)
                .WithRequired(e => e.pap_product_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pap_suppressed_product_type>()
                .Property(e => e.ext_product_type_id)
                .IsUnicode(false);

            modelBuilder.Entity<pap_transaction>()
                .Property(e => e.total_cost)
                .HasPrecision(9, 2);

            modelBuilder.Entity<pap_transaction>()
                .Property(e => e.action_code)
                .IsUnicode(false);

            modelBuilder.Entity<pap_transaction>()
                .Property(e => e.ext_transaction_id)
                .IsUnicode(false);

            modelBuilder.Entity<pap_transaction>()
                .Property(e => e.ext_status_id)
                .IsUnicode(false);

            modelBuilder.Entity<participant>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<participant>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<partner_commission>()
                .Property(e => e.commission_rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Partner_Commission_Range>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<partner_contacts>()
                .Property(e => e.section_name)
                .IsUnicode(false);

            modelBuilder.Entity<partner_contacts>()
                .Property(e => e.section_value)
                .IsUnicode(false);

            modelBuilder.Entity<Partner_Fixed_Cost>()
                .Property(e => e.Cost_By_Lead)
                .HasPrecision(10, 4);

            modelBuilder.Entity<partner_group_types>()
                .Property(e => e.partner_group_type_desc)
                .IsUnicode(false);

            modelBuilder.Entity<partner_group_types>()
                .HasMany(e => e.C_tbd_partner)
                .WithRequired(e => e.partner_group_types)
                .HasForeignKey(e => e.partner_group_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<partner_group_types>()
                .HasMany(e => e.C_tbd_partner1)
                .WithRequired(e => e.partner_group_types1)
                .HasForeignKey(e => e.partner_subgroup_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Partner_Lead_Commission>()
                .Property(e => e.Channel_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Partner_Lead_Commission>()
                .Property(e => e.Fixed_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Partner_Sales_Commission>()
                .Property(e => e.Variable_Rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<partner_web_details>()
                .Property(e => e.top_menu)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_details>()
                .Property(e => e.left_menu)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_details>()
                .Property(e => e.right_menu)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_details>()
                .Property(e => e.images_path)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_details>()
                .Property(e => e.default_color)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_details>()
                .Property(e => e.short_cut_menu)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_details>()
                .Property(e => e.product_image_map)
                .IsUnicode(false);

            modelBuilder.Entity<party_type>()
                .Property(e => e.party_type_desc)
                .IsUnicode(false);

            modelBuilder.Entity<party_type>()
                .HasMany(e => e.campaign_reason)
                .WithRequired(e => e.party_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<party_type>()
                .HasMany(e => e.hear_about_us)
                .WithRequired(e => e.party_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<party_type>()
                .HasMany(e => e.organization_type)
                .WithRequired(e => e.party_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<party_type>()
                .HasMany(e => e.titles)
                .WithRequired(e => e.party_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.credit_card_no)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.expiry_date)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.name_on_card)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.authorization_number)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.payment_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Payment_Audit>()
                .Property(e => e.AUDIT_OPERATION)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Payment_Audit>()
                .Property(e => e.HOST)
                .IsUnicode(false);

            modelBuilder.Entity<Payment_Audit>()
                .Property(e => e.AUDIT_USERID)
                .IsUnicode(false);

            modelBuilder.Entity<Payment_Audit>()
                .Property(e => e.credit_card_no)
                .IsUnicode(false);

            modelBuilder.Entity<Payment_Audit>()
                .Property(e => e.expiry_date)
                .IsUnicode(false);

            modelBuilder.Entity<Payment_Audit>()
                .Property(e => e.name_on_card)
                .IsUnicode(false);

            modelBuilder.Entity<Payment_Audit>()
                .Property(e => e.authorization_number)
                .IsUnicode(false);

            modelBuilder.Entity<Payment_Audit>()
                .Property(e => e.payment_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<payment_method>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<payment_method>()
                .Property(e => e.discount_percentage)
                .HasPrecision(15, 4);

            modelBuilder.Entity<payment_method>()
                .HasMany(e => e.credit_card_types)
                .WithRequired(e => e.payment_method)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<payment_method>()
                .HasMany(e => e.payments)
                .WithRequired(e => e.payment_method)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<payment_method>()
                .HasMany(e => e.sale_to_add)
                .WithOptional(e => e.payment_method)
                .HasForeignKey(e => e.payment_method_id);

            modelBuilder.Entity<payment_method>()
                .HasMany(e => e.sale_to_add1)
                .WithOptional(e => e.payment_method1)
                .HasForeignKey(e => e.upfront_payment_method_id);

            modelBuilder.Entity<Payment_status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<payment_term>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<payment_term>()
                .Property(e => e.discount_percent)
                .HasPrecision(15, 4);

            modelBuilder.Entity<phone_number_tracking>()
                .Property(e => e.phone_number_tracking_desc)
                .IsUnicode(false);

            modelBuilder.Entity<po_status>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<Postponed_Sale>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Postponed_Status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Postponed_Status>()
                .HasMany(e => e.Postponed_Sale)
                .WithRequired(e => e.Postponed_Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Price_Range>()
                .Property(e => e.Unit_Price_Sold)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Priority>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<product_class>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<product_class>()
                .Property(e => e.product_code)
                .IsUnicode(false);

            modelBuilder.Entity<product_class>()
                .Property(e => e.display_name)
                .IsUnicode(false);

            modelBuilder.Entity<product_class>()
                .HasMany(e => e.product_business_rule)
                .WithRequired(e => e.product_class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product_class_desc>()
                .Property(e => e.product_class_desc1)
                .IsUnicode(false);

            modelBuilder.Entity<product_class_desc>()
                .Property(e => e.min_requirements)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.product_name)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.product_short_desc)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.product_long_desc)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.product_small_img)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.product_large_img)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Quantity>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Production_Status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<profit_range>()
                .HasMany(e => e.product_business_rule_profit_range)
                .WithRequired(e => e.profit_range)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<promokit>()
                .Property(e => e.street_address)
                .IsUnicode(false);

            modelBuilder.Entity<promokit>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<promokit>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<promokit>()
                .Property(e => e.country_code)
                .IsUnicode(false);

            modelBuilder.Entity<promokit>()
                .Property(e => e.state_code)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_Code>()
                .Property(e => e.Promotion_Code_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_Cost>()
                .Property(e => e.Cost)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Proposal>()
                .Property(e => e.Fax_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Proposal>()
                .Property(e => e.Email_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Proposal>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Proposal>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Proposals)
                .Map(m => m.ToTable("Proposal_Tags").MapLeftKey("Proposal_ID").MapRightKey("Tags_ID"));

            modelBuilder.Entity<QSP_Program>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<QSP_Program>()
                .Property(e => e.Base_Directory)
                .IsUnicode(false);

            modelBuilder.Entity<QSP_Program>()
                .HasMany(e => e.Template_Set)
                .WithRequired(e => e.QSP_Program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reason>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Referee>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Referee>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Referee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Referee>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Referee>()
                .HasMany(e => e.leads)
                .WithOptional(e => e.Referee)
                .HasForeignKey(e => e.referee_id);

            modelBuilder.Entity<Replication_Monitoring>()
                .Property(e => e.Msg)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Decision>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Decision>()
                .HasMany(e => e.Req_Request)
                .WithOptional(e => e.Req_Decision)
                .HasForeignKey(e => new { e.Decision_Id, e.Language_Id })
                .WillCascadeOnDelete();

            modelBuilder.Entity<Req_Employees>()
                .Property(e => e.Employee_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Employees>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Employees>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Language>()
                .Property(e => e.Language)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Language>()
                .HasMany(e => e.Req_Decision)
                .WithRequired(e => e.Req_Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Req_Language>()
                .HasMany(e => e.Req_Priority)
                .WithRequired(e => e.Req_Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Req_Language>()
                .HasMany(e => e.Req_Project_Type)
                .WithRequired(e => e.Req_Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Req_Language>()
                .HasMany(e => e.Req_Request_Type)
                .WithRequired(e => e.Req_Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Req_Priority>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Priority>()
                .HasMany(e => e.Req_Request)
                .WithOptional(e => e.Req_Priority)
                .HasForeignKey(e => new { e.Priority_Id, e.Language_Id })
                .WillCascadeOnDelete();

            modelBuilder.Entity<Req_Project_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Project_Type>()
                .HasMany(e => e.Req_Request)
                .WithOptional(e => e.Req_Project_Type)
                .HasForeignKey(e => new { e.Project_Type_ID, e.Language_Id })
                .WillCascadeOnDelete();

            modelBuilder.Entity<Req_Request>()
                .Property(e => e.Project_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Request>()
                .Property(e => e.Summary_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Request>()
                .Property(e => e.Impact_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Request>()
                .Property(e => e.Mis_Impact_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Request>()
                .Property(e => e.Decision_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Request_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Req_Request_Type>()
                .HasMany(e => e.Req_Request)
                .WithOptional(e => e.Req_Request_Type)
                .HasForeignKey(e => new { e.Request_Type_ID, e.Language_Id })
                .WillCascadeOnDelete();

            modelBuilder.Entity<sale>()
                .Property(e => e.client_sequence_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.po_number)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.expiry_date)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.shipping_fees)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sale>()
                .Property(e => e.shipping_fees_discount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sale>()
                .Property(e => e.waybill_no)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.total_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sale>()
                .Property(e => e.upfront_payment_required)
                .HasPrecision(10, 2);

            modelBuilder.Entity<sale>()
                .Property(e => e.accounting_comments)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.ssn_number)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.ssn_address)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.ssn_city)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.ssn_state_code)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.ssn_country_code)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.ssn_zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.cvv2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.ext_shipping_account_id)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .Property(e => e.ext_billing_account_id)
                .IsUnicode(false);

            modelBuilder.Entity<sale>()
                .HasMany(e => e.AR_Activity)
                .WithRequired(e => e.sale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sale>()
                .HasMany(e => e.orders_sale)
                .WithRequired(e => e.sale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sale>()
                .HasMany(e => e.sale_carrier_shipping_status)
                .WithRequired(e => e.sale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sale>()
                .HasMany(e => e.sales_item)
                .WithRequired(e => e.sale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.AUDIT_OPERATION)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.HOST)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.AUDIT_USERID)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.client_sequence_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.po_number)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.expiry_date)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.shipping_fees)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.shipping_fees_discount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.waybill_no)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.total_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.upfront_payment_required)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.accounting_comments)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.ssn_number)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.ssn_address)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.ssn_city)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.ssn_state_code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.ssn_country_code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.ssn_zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.cvv2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.ext_shipping_account_id)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Audit>()
                .Property(e => e.ext_billing_account_id)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.po_number)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.credit_card_no)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.expiry_date)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.shipping_fees)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.shipping_fees_discount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.total_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.upfront_payment_required)
                .HasPrecision(10, 2);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.ssn_number)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.ssn_address)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.ssn_city)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.ssn_state_code)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.ssn_country_code)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .Property(e => e.ssn_zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<sale_to_add>()
                .HasMany(e => e.Applicable_Tax_To_Add)
                .WithRequired(e => e.sale_to_add)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sale_to_add>()
                .HasMany(e => e.sales_item_to_add)
                .WithRequired(e => e.sale_to_add)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale_To_Local_Sponsor>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Zip_Code>()
                .Property(e => e.Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Change_Log>()
                .Property(e => e.Table_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Change_Log>()
                .Property(e => e.Column_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Change_Log>()
                .Property(e => e.User_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Change_Log>()
                .Property(e => e.From_Value)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Change_Log>()
                .Property(e => e.To_Value)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Change_Log>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Change_Log>()
                .Property(e => e.Computer_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Change_Log>()
                .Property(e => e.Other_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<sales_constraints>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.group_name)
                .IsUnicode(false);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.unit_price_sold)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.suggested_coupons)
                .IsUnicode(false);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.sales_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.paid_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.adjusted_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.discount_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.sales_commission_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.sponsor_commission_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.nb_units_sold)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.manual_product_description)
                .IsUnicode(false);

            modelBuilder.Entity<sales_item>()
                .Property(e => e.profit_margin)
                .HasPrecision(18, 0);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.group_name)
                .IsUnicode(false);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.unit_price_sold)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.suggested_coupons)
                .IsUnicode(false);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.sales_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.paid_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.adjusted_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.discount_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.sales_commission_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.sponsor_commission_amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.nb_units_sold)
                .HasPrecision(15, 4);

            modelBuilder.Entity<sales_item_to_add>()
                .Property(e => e.manual_product_description)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<salutation>()
                .Property(e => e.salutation_desc)
                .IsUnicode(false);

            modelBuilder.Entity<salutation>()
                .HasMany(e => e.salutation_desc1)
                .WithRequired(e => e.salutation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<salutation_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Sample>()
                .HasMany(e => e.promotional_kit)
                .WithOptional(e => e.Sample)
                .HasForeignKey(e => e.sample_id);

            modelBuilder.Entity<SC_SECTION>()
                .Property(e => e.Section_Title)
                .IsUnicode(false);

            modelBuilder.Entity<SC_SECTION>()
                .Property(e => e.Section_Image)
                .IsUnicode(false);

            modelBuilder.Entity<SC_SECTION>()
                .Property(e => e.Section_Text)
                .IsUnicode(false);

            modelBuilder.Entity<SC_SECTION>()
                .Property(e => e.Section_Template)
                .IsUnicode(false);

            modelBuilder.Entity<SC_SECTION>()
                .Property(e => e.Section_Sub_Title)
                .IsUnicode(false);

            modelBuilder.Entity<scratch_book>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<scratch_book>()
                .Property(e => e.raising_potential)
                .HasPrecision(15, 4);

            modelBuilder.Entity<scratch_book>()
                .Property(e => e.product_code)
                .IsUnicode(false);

            modelBuilder.Entity<scratch_book>()
                .Property(e => e.current_description)
                .IsUnicode(false);

            modelBuilder.Entity<scratch_book>()
                .Property(e => e.fixed_profit)
                .HasPrecision(18, 0);

            modelBuilder.Entity<scratch_book>()
                .HasMany(e => e.Commission_Rate)
                .WithRequired(e => e.scratch_book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<scratch_book>()
                .HasMany(e => e.Inventory_Adjustment)
                .WithRequired(e => e.scratch_book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<scratch_book>()
                .HasMany(e => e.product_business_rule)
                .WithOptional(e => e.scratch_book)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<scratch_book>()
                .HasMany(e => e.product_desc)
                .WithRequired(e => e.scratch_book)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<scratch_book>()
                .HasMany(e => e.Product_Quantity)
                .WithRequired(e => e.scratch_book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<scratch_book>()
                .HasMany(e => e.products_packages)
                .WithRequired(e => e.scratch_book)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<scratch_book>()
                .HasMany(e => e.sales_item)
                .WithRequired(e => e.scratch_book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<scratch_book>()
                .HasMany(e => e.sales_item_to_add)
                .WithRequired(e => e.scratch_book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<scratch_book>()
                .HasMany(e => e.scratch_book_price_info)
                .WithRequired(e => e.scratch_book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<scratch_book_commission>()
                .Property(e => e.commission_rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<scratch_book_commission>()
                .Property(e => e.commission_rate_ca)
                .HasPrecision(15, 4);

            modelBuilder.Entity<scratch_book_price_info>()
                .Property(e => e.country_code)
                .IsUnicode(false);

            modelBuilder.Entity<scratch_book_price_info>()
                .Property(e => e.unit_price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<service_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<shipping_fee>()
                .Property(e => e.shipping_fee1)
                .HasPrecision(19, 4);

            modelBuilder.Entity<shipping_fee>()
                .HasMany(e => e.product_business_rule_shipping_fee)
                .WithRequired(e => e.shipping_fee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.Site_Title)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.Site_Content)
                .IsUnicode(false);

            modelBuilder.Entity<special_offer>()
                .Property(e => e.special_offer_text)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Middle_Initial)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Day_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Day_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Evening_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Evenig_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Alternate_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .Property(e => e.Nt_Login)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .HasMany(e => e.Local_Sponsor)
                .WithRequired(e => e.Sponsor_Consultant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sponsor_Consultant>()
                .HasMany(e => e.Local_Sponsor_Activity)
                .WithRequired(e => e.Sponsor_Consultant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sport_Association>()
                .Property(e => e.Sport_Ass_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<SS_Drop_Box>()
                .Property(e => e.SS_Drop_Box_Name)
                .IsUnicode(false);

            modelBuilder.Entity<SS_Drop_Box>()
                .HasMany(e => e.SS_Drop_Box_Package)
                .WithRequired(e => e.SS_Drop_Box)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .Property(e => e.State_Name)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .Property(e => e.SAP_State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.Alias_State)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.Billing_Company)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.client_address)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.FSM_Address)
                .WithOptional(e => e.State)
                .WillCascadeOnDelete();

            modelBuilder.Entity<State>()
                .HasMany(e => e.Local_Sponsor)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.Organizations)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.sale_to_add)
                .WithOptional(e => e.State)
                .HasForeignKey(e => e.ssn_state_code);

            modelBuilder.Entity<State>()
                .HasMany(e => e.State_Tax)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State_Tax>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<State_Tax>()
                .Property(e => e.Tax_Code)
                .IsUnicode(false);

            modelBuilder.Entity<State_Tax>()
                .Property(e => e.Tax_Rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<supplier>()
                .Property(e => e.supplier_name)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.street_adress)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.zip)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.contact_name)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.fax)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.account_no)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.credit_margin)
                .HasPrecision(15, 4);

            modelBuilder.Entity<supplier>()
                .Property(e => e.state_code)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.country_code)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Label)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Control_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Targeted_Market>()
                .Property(e => e.Age_Range)
                .IsUnicode(false);

            modelBuilder.Entity<Targeted_Market>()
                .Property(e => e.Education_Level)
                .IsUnicode(false);

            modelBuilder.Entity<Targeted_Market>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Targeted_Market>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<targeted_market_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<targeted_market_type>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<targeted_market_type>()
                .HasMany(e => e.Targeted_Market)
                .WithRequired(e => e.targeted_market_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tax_Table>()
                .Property(e => e.Tax_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Tax_Table>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Tax_Table>()
                .Property(e => e.Tax_Account_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Tax_Table>()
                .Property(e => e.Description_francaise)
                .IsUnicode(false);

            modelBuilder.Entity<Tax_Table>()
                .HasMany(e => e.Applicable_Tax)
                .WithRequired(e => e.Tax_Table)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tax_Table>()
                .HasMany(e => e.Applicable_Tax_To_Add)
                .WithRequired(e => e.Tax_Table)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tax_Table>()
                .HasMany(e => e.State_Tax)
                .WithRequired(e => e.Tax_Table)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Channel_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Salutation)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Organization)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Street_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Day_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Day_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Evening_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Evening_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Interests)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Day_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Evening_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Rejection_reason)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Other_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Other_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Group_Web_Site)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Cookie_Content)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Campaign_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Sale_Zip_Code>()
                .Property(e => e.Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Template>()
                .Property(e => e.Template_Path)
                .IsUnicode(false);

            modelBuilder.Entity<Template>()
                .Property(e => e.ReportCenterPasswd)
                .IsUnicode(false);

            modelBuilder.Entity<Template_Set>()
                .Property(e => e.Supporter_Path)
                .IsUnicode(false);

            modelBuilder.Entity<Template_Set>()
                .Property(e => e.Generic_Path)
                .IsUnicode(false);

            modelBuilder.Entity<Template_Set>()
                .Property(e => e.Edit_Path)
                .IsUnicode(false);

            modelBuilder.Entity<territory>()
                .Property(e => e.territory_name)
                .IsUnicode(false);

            modelBuilder.Entity<Territory_Def>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<title>()
                .Property(e => e.title_desc)
                .IsUnicode(false);

            modelBuilder.Entity<title>()
                .HasMany(e => e.leads)
                .WithRequired(e => e.title)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<title>()
                .HasMany(e => e.title_desc1)
                .WithRequired(e => e.title)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<title_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<transfer_status>()
                .Property(e => e.transfer_status_desc)
                .IsUnicode(false);

            modelBuilder.Entity<transfer_status>()
                .HasMany(e => e.C_tbd_promotion)
                .WithRequired(e => e.transfer_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UnAssignLogin>()
                .Property(e => e.User_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Web_Site>()
                .Property(e => e.Web_Site_Name)
                .IsUnicode(false);

            modelBuilder.Entity<WFC_Logs>()
                .Property(e => e.customer_number)
                .IsFixedLength();

            modelBuilder.Entity<WFC_Logs>()
                .Property(e => e.order_number)
                .IsFixedLength();

            modelBuilder.Entity<WFC_Logs>()
                .Property(e => e.sale_id)
                .IsFixedLength();

            modelBuilder.Entity<WFC_Logs>()
                .Property(e => e.product_code)
                .IsFixedLength();

            modelBuilder.Entity<WFC_Logs>()
                .Property(e => e.freight)
                .IsFixedLength();

            modelBuilder.Entity<WFC_Logs>()
                .Property(e => e.result_message)
                .IsFixedLength();

            modelBuilder.Entity<WFC_Logs>()
                .Property(e => e.address)
                .IsFixedLength();

            modelBuilder.Entity<WFC_Payment_Logs>()
                .Property(e => e.invoice_number)
                .IsFixedLength();

            modelBuilder.Entity<WFC_Payment_Logs>()
                .Property(e => e.message)
                .IsFixedLength();

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARCUST)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.AR_SEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARCNTR)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARYRTR)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARMOTR)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARDYTR)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARAMTR)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.AR_ORD)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.AR_IFM)
                .HasPrecision(6, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARAMBL)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARAMCR)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARAMCL)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARAMNC)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.AR_CHK)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARCNIV)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARYRIV)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARMOIV)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.ARDYIV)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.AR_ISQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.AR_BAT)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.AR_BSQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARDHISP>()
                .Property(e => e.AR_SSQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<BeFree>()
                .Property(e => e.Merchant_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree>()
                .Property(e => e.Record_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree>()
                .Property(e => e.Source_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree>()
                .Property(e => e.Transaction_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree>()
                .Property(e => e.Product_Key)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree>()
                .Property(e => e.Qty_Product)
                .HasPrecision(5, 0);

            modelBuilder.Entity<BeFree>()
                .Property(e => e.Unit_Price)
                .HasPrecision(11, 2);

            modelBuilder.Entity<BeFree>()
                .Property(e => e.Currency_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree>()
                .Property(e => e.Merchandise_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree_History>()
                .Property(e => e.Merchant_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree_History>()
                .Property(e => e.Record_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree_History>()
                .Property(e => e.Source_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree_History>()
                .Property(e => e.Transaction_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree_History>()
                .Property(e => e.Product_Key)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree_History>()
                .Property(e => e.Qty_Product)
                .HasPrecision(5, 0);

            modelBuilder.Entity<BeFree_History>()
                .Property(e => e.Unit_Price)
                .HasPrecision(11, 2);

            modelBuilder.Entity<BeFree_History>()
                .Property(e => e.Currency_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BeFree_History>()
                .Property(e => e.Merchandise_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<crm_users>()
                .Property(e => e.consultant_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<crm_users>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<crm_users>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Lead_Activity_copy>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.BATCHNO)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.SEQNO)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.ISS_MM)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.ISS_DD)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.ISS_YY)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.PUB_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.CARD_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.FIRST)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.MIDDLE)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.LAST)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.COMPANY)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.ADDRESS1)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.ADDRESS2)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.CITY)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.STATE)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.ZIP)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.PLUS4)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.COUNTRY)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.RS_NO)
                .IsUnicode(false);

            modelBuilder.Entity<mag_lead>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<partner_from_esubs_20080414>()
                .Property(e => e.partner_name)
                .IsUnicode(false);

            modelBuilder.Entity<partner_from_esubs_20080414>()
                .Property(e => e.partner_path)
                .IsUnicode(false);

            modelBuilder.Entity<partner_from_esubs_20080414>()
                .Property(e => e.esubs_url)
                .IsUnicode(false);

            modelBuilder.Entity<partner_from_esubs_20080414>()
                .Property(e => e.Partner_Name1)
                .IsUnicode(false);

            modelBuilder.Entity<partner_from_esubs_20080414>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_old>()
                .Property(e => e.Promotion_Type_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_old>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_old>()
                .Property(e => e.Visibility)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_old>()
                .Property(e => e.Contact_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_old>()
                .Property(e => e.Tracking_Serial)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_old>()
                .Property(e => e.Cookie_Content)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_old>()
                .Property(e => e.Keyword)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_old>()
                .Property(e => e.Script_Name)
                .IsUnicode(false);

            modelBuilder.Entity<rs_lastcommit>()
                .Property(e => e.origin_qid)
                .IsFixedLength();

            modelBuilder.Entity<rs_lastcommit>()
                .Property(e => e.secondary_qid)
                .IsFixedLength();

            modelBuilder.Entity<Sponsor_Found_Stool>()
                .Property(e => e.User_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sponsor_Found_Stool>()
                .Property(e => e.Modif_Date)
                .IsUnicode(false);

            modelBuilder.Entity<temp_bounce>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<temp_unsub>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<tmp_total_adjustment>()
                .Property(e => e.Adjustment_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tmp_total_deposit>()
                .Property(e => e.Total_Deposit)
                .HasPrecision(38, 4);
        }
    }
}
