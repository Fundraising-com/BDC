namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataProvider : DbContext
    {
        public DataProvider()
            : base("name=esubs_global_v2")
        {
        }

      public virtual DbSet<touch_email_process_queue> touch_emails_process_queue { get; set; }
      public virtual DbSet<text_group> text_group { get; set; }
        public virtual DbSet<partner_text_group> partner_text_group { get; set; }
        public virtual DbSet<text_asset_replace> text_asset_replace { get; set; }
        public virtual DbSet<page_route_mapper> page_route_mapper { get; set; }
        public virtual DbSet<popular_item> popular_item { get; set; }
        public virtual DbSet<gallery_image> gallery_image { get; set; }
        public virtual DbSet<email_template_selector> email_template_selector { get; set; }
        public virtual DbSet<email_flow> email_flows { get; set; }
        public virtual DbSet<email_flow_template> email_flow_template { get; set; }
        public virtual DbSet<C_tbd_partner> C_tbd_partner { get; set; }
        public virtual DbSet<landingPage> landingPages { get; set; }
        public virtual DbSet<default_personalization> default_personalizations { get; set; }
        public virtual DbSet<offensive_content> offensive_contents { get; set; }
        public virtual DbSet<C_tbd_partner_attribute> C_tbd_partner_attribute { get; set; }
        public virtual DbSet<C_tbd_partner_attribute_value> C_tbd_partner_attribute_value { get; set; }
        public virtual DbSet<C_tbd_partner_promotion> C_tbd_partner_promotion { get; set; }
        public virtual DbSet<C_tbd_partner_type> C_tbd_partner_type { get; set; }
        public virtual DbSet<C_tbd_partner_type_culture> C_tbd_partner_type_culture { get; set; }
        public virtual DbSet<C_tbd_promotion> C_tbd_promotion { get; set; }
        public virtual DbSet<C_tbd_promotion_destination> C_tbd_promotion_destination { get; set; }
        public virtual DbSet<C_tbd_promotion_type> C_tbd_promotion_type { get; set; }
        public virtual DbSet<action> actions { get; set; }
        public virtual DbSet<audit_member> audit_member { get; set; }
        public virtual DbSet<audit_member_hierarchy> audit_member_hierarchy { get; set; }
        public virtual DbSet<audit_personalization_image> audit_personalization_image { get; set; }
        public virtual DbSet<audit_users> audit_users { get; set; }
        public virtual DbSet<business_rule> business_rule { get; set; }
        public virtual DbSet<country> countries { get; set; }
        public virtual DbSet<creation_channel> creation_channel { get; set; }
        public virtual DbSet<culture> cultures { get; set; }
        public virtual DbSet<culture_country_name> culture_country_name { get; set; }
        public virtual DbSet<culture_subdivision_name> culture_subdivision_name { get; set; }
        public virtual DbSet<custcare_comments> custcare_comments { get; set; }
        public virtual DbSet<custom_email_template> custom_email_template { get; set; }
        public virtual DbSet<default_email_template> default_email_template { get; set; }
        public virtual DbSet<display_product_type> display_product_type { get; set; }
        public virtual DbSet<dm_image_approval_status> dm_image_approval_status { get; set; }
        public virtual DbSet<dm_personalization_image> dm_personalization_image { get; set; }
        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<earned_prize> earned_prize { get; set; }
        public virtual DbSet<email_import_test> email_import_test { get; set; }
        public virtual DbSet<email_template> email_template { get; set; }
        public virtual DbSet<email_template_culture> email_template_culture { get; set; }
        public virtual DbSet<email_template_field> email_template_field { get; set; }
        public virtual DbSet<email_template_tag> email_template_tag { get; set; }
        public virtual DbSet<email_template_type> email_template_type { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<es_valid_orders_items> es_valid_orders_items { get; set; }
        public virtual DbSet<es_valid_orders_items_staging> es_valid_orders_items_staging { get; set; }
        public virtual DbSet<_event> events { get; set; }
        public virtual DbSet<event_facebook_visitor> event_facebook_visitor { get; set; }
        public virtual DbSet<event_gasavingcard> event_gasavingcard { get; set; }
        public virtual DbSet<event_group> event_group { get; set; }
        public virtual DbSet<event_participation> event_participation { get; set; }
        public virtual DbSet<event_profit_range> event_profit_range { get; set; }
        public virtual DbSet<event_status> event_status { get; set; }
        public virtual DbSet<event_total_amount> event_total_amount { get; set; }
        public virtual DbSet<event_type> event_type { get; set; }
        public virtual DbSet<exception_type> exception_type { get; set; }
        public virtual DbSet<external_account> external_account { get; set; }
        public virtual DbSet<external_account_action> external_account_action { get; set; }
        public virtual DbSet<group> groups { get; set; }
        public virtual DbSet<group_group_status> group_group_status { get; set; }
        public virtual DbSet<group_status> group_status { get; set; }
        public virtual DbSet<group_type> group_type { get; set; }
        public virtual DbSet<image_approval_status> image_approval_status { get; set; }
        public virtual DbSet<kd_alumni> kd_alumni { get; set; }
        public virtual DbSet<kiwanisgroup> kiwanisgroups { get; set; }
        public virtual DbSet<kiwanismember> kiwanismembers { get; set; }
        public virtual DbSet<language> languages { get; set; }
        public virtual DbSet<member> members { get; set; }
        public virtual DbSet<member_hierarchy> member_hierarchy { get; set; }
        public virtual DbSet<member_phone_number> member_phone_number { get; set; }
        public virtual DbSet<member_postal_address> member_postal_address { get; set; }
        public virtual DbSet<member_type> member_type { get; set; }
        public virtual DbSet<event_participation_action> event_participation_actions { get; set; }
        public virtual DbSet<opt_status> opt_status { get; set; }
        public virtual DbSet<order_profit> order_profit { get; set; }
        public virtual DbSet<participant_total_amount> participant_total_amount { get; set; }
        public virtual DbSet<participation_channel> participation_channel { get; set; }
        public virtual DbSet<Partner_Activation_Commission> Partner_Activation_Commission { get; set; }
        public virtual DbSet<Partner_Commission_Range> Partner_Commission_Range { get; set; }
        public virtual DbSet<partner_culture_link> partner_culture_link { get; set; }
        public virtual DbSet<partner_email_template> partner_email_template { get; set; }
        public virtual DbSet<partner_payment_config> partner_payment_config { get; set; }
        public virtual DbSet<partner_payment_info> partner_payment_info { get; set; }
        public virtual DbSet<partner_product_offer> partner_product_offer { get; set; }
        public virtual DbSet<Partner_Sales_Commission> Partner_Sales_Commission { get; set; }
        public virtual DbSet<partner_store_template> partner_store_template { get; set; }
        public virtual DbSet<payment> payments { get; set; }
        public virtual DbSet<payment_batch> payment_batch { get; set; }
        public virtual DbSet<payment_comment> payment_comment { get; set; }
        public virtual DbSet<payment_exception_type> payment_exception_type { get; set; }
        public virtual DbSet<payment_info> payment_info { get; set; }
        public virtual DbSet<payment_item> payment_item { get; set; }
        public virtual DbSet<payment_payment_status> payment_payment_status { get; set; }
        public virtual DbSet<payment_period> payment_period { get; set; }
        public virtual DbSet<payment_status> payment_status { get; set; }
        public virtual DbSet<payment_type> payment_type { get; set; }
        public virtual DbSet<personalization> personalizations { get; set; }
        public virtual DbSet<personalization_image> personalization_image { get; set; }
        public virtual DbSet<phone_number> phone_number { get; set; }
        public virtual DbSet<phone_number_type> phone_number_type { get; set; }
        public virtual DbSet<postal_address> postal_address { get; set; }
        public virtual DbSet<postal_address_validation> postal_address_validation { get; set; }
        public virtual DbSet<postal_address_type> postal_address_type { get; set; }
        public virtual DbSet<precalculatedvalue> precalculatedvalues { get; set; }
        public virtual DbSet<prize> prizes { get; set; }
        public virtual DbSet<prize_item> prize_item { get; set; }
        public virtual DbSet<prize_type> prize_type { get; set; }
        public virtual DbSet<product_offer> product_offer { get; set; }
        public virtual DbSet<program> programs { get; set; }
        public virtual DbSet<program_partner> program_partner { get; set; }
        public virtual DbSet<program_partner_prize> program_partner_prize { get; set; }
        public virtual DbSet<qsp_matching_code> qsp_matching_code { get; set; }
        public virtual DbSet<stats_personalization> stats_personalization { get; set; }
        public virtual DbSet<stats_personalization_item> stats_personalization_item { get; set; }
        public virtual DbSet<stats_personalization_section> stats_personalization_section { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<store_template> store_template { get; set; }
        public virtual DbSet<subdivision> subdivisions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tag> tags { get; set; }
        public virtual DbSet<tellafriend> tellafriends { get; set; }
        public virtual DbSet<tellafriend_recipient> tellafriend_recipient { get; set; }
        public virtual DbSet<test_table> test_table { get; set; }
        public virtual DbSet<theme> themes { get; set; }
        public virtual DbSet<touch> touches { get; set; }
        public virtual DbSet<touch_action> touch_action { get; set; }
        public virtual DbSet<touch_change_log> touch_change_log { get; set; }
        public virtual DbSet<touch_change_log_details> touch_change_log_details { get; set; }
        public virtual DbSet<touch_info> touch_info { get; set; }
        public virtual DbSet<touch_info_archive> touch_info_archive { get; set; }
        public virtual DbSet<touch_not_to_send_20071212> touch_not_to_send_20071212 { get; set; }
        public virtual DbSet<Trace_leads> Trace_leads { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<user_oauthmembership> user_oauthmemberships { get; set; }
        public virtual DbSet<xfactor_member> xfactor_member { get; set; }
        public virtual DbSet<aoii_chapters> aoii_chapters { get; set; }
        public virtual DbSet<aoii_chapters_2006> aoii_chapters_2006 { get; set; }
        public virtual DbSet<bounce_to_transfer> bounce_to_transfer { get; set; }
        public virtual DbSet<campaign_merges> campaign_merges { get; set; }
        public virtual DbSet<direct_mail> direct_mail { get; set; }
        public virtual DbSet<direct_mail_info> direct_mail_info { get; set; }
        public virtual DbSet<direct_mail_letter> direct_mail_letter { get; set; }
        public virtual DbSet<direct_mail_template> direct_mail_template { get; set; }
        public virtual DbSet<efundraingtransaction_suppID_tranlation> efundraingtransaction_suppID_tranlation { get; set; }
        public virtual DbSet<event_change> event_change { get; set; }
        public virtual DbSet<ewnchapter> ewnchapters { get; set; }
        public virtual DbSet<ewnparticipant> ewnparticipants { get; set; }
        public virtual DbSet<ewnpaticipant_possibleduplicate> ewnpaticipant_possibleduplicate { get; set; }
        public virtual DbSet<featured_event> featured_event { get; set; }
        public virtual DbSet<featured_event2> featured_event2 { get; set; }
        public virtual DbSet<generated_pass> generated_pass { get; set; }
        public virtual DbSet<KAT> KATs { get; set; }
        public virtual DbSet<kd_alumni_2006> kd_alumni_2006 { get; set; }
        public virtual DbSet<kd_fy08> kd_fy08 { get; set; }
        public virtual DbSet<Kiwani> Kiwanis { get; set; }
        public virtual DbSet<kiwanis_phone_to_add> kiwanis_phone_to_add { get; set; }
        public virtual DbSet<kiwanischange> kiwanischanges { get; set; }
        public virtual DbSet<MD08> MD08 { get; set; }
        public virtual DbSet<MDImport> MDImports { get; set; }
        public virtual DbSet<member_20110621> member_20110621 { get; set; }
        public virtual DbSet<order_profit_backup> order_profit_backup { get; set; }
        public virtual DbSet<payment_info_corr_20080908> payment_info_corr_20080908 { get; set; }
        public virtual DbSet<payment_info_corr_20080918> payment_info_corr_20080918 { get; set; }
        public virtual DbSet<payment_info_corr_20080918_2> payment_info_corr_20080918_2 { get; set; }
        public virtual DbSet<prize_to_be_removed> prize_to_be_removed { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<temp_customer> temp_customer { get; set; }
        public virtual DbSet<temp_group_member> temp_group_member { get; set; }
        public virtual DbSet<temp_member> temp_member { get; set; }
        public virtual DbSet<temp_member_bak> temp_member_bak { get; set; }
        public virtual DbSet<temp_member_hierarchy> temp_member_hierarchy { get; set; }
        public virtual DbSet<temp_payment_info> temp_payment_info { get; set; }
        public virtual DbSet<temp_phone_number> temp_phone_number { get; set; }
        public virtual DbSet<temp_postal_address> temp_postal_address { get; set; }
        public virtual DbSet<temp_touch_info> temp_touch_info { get; set; }
        public virtual DbSet<touch_archive> touch_archive { get; set; }
        public virtual DbSet<tpa_fy09> tpa_fy09 { get; set; }
        public virtual DbSet<unused_account_number> unused_account_number { get; set; }
        public virtual DbSet<web_action> web_action { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.partner_name)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner_attribute>()
                .Property(e => e.partner_attribute_name)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner_attribute_value>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner_type>()
                .Property(e => e.partner_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner_type_culture>()
                .Property(e => e.partner_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.promotion_type_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.promotion_name)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.script_name)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion_destination>()
                .Property(e => e.promotion_destination_url)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion_type>()
                .Property(e => e.promotion_type_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion_type>()
                .Property(e => e.promotion_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<action>()
                .Property(e => e.action_desc)
                .IsUnicode(false);

            modelBuilder.Entity<action>()
                .HasMany(e => e.touch_action)
                .WithRequired(e => e.action)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.audit_user_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.audit_opcode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.audit_modifier)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.middle_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.external_member_id)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.parent_first_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member>()
                .Property(e => e.parent_last_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member_hierarchy>()
                .Property(e => e.audit_user_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_member_hierarchy>()
                .Property(e => e.audit_opcode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<audit_member_hierarchy>()
                .Property(e => e.audit_modifier)
                .IsUnicode(false);

            modelBuilder.Entity<audit_personalization_image>()
                .Property(e => e.audit_user_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_personalization_image>()
                .Property(e => e.audit_opcode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<audit_personalization_image>()
                .Property(e => e.audit_modifier)
                .IsUnicode(false);

            modelBuilder.Entity<audit_personalization_image>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<audit_personalization_image>()
                .Property(e => e.approver_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_users>()
                .Property(e => e.audit_user_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_users>()
                .Property(e => e.audit_opcode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<audit_users>()
                .Property(e => e.audit_modifier)
                .IsUnicode(false);

            modelBuilder.Entity<audit_users>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_users>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<audit_users>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<audit_users>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<audit_users>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<business_rule>()
                .Property(e => e.business_rule_name)
                .IsUnicode(false);

            modelBuilder.Entity<business_rule>()
                .Property(e => e.stored_procedure_call)
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .HasMany(e => e.cultures)
                .WithRequired(e => e.country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<country>()
                .HasMany(e => e.culture_country_name)
                .WithRequired(e => e.country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<country>()
                .HasMany(e => e.subdivisions)
                .WithRequired(e => e.country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<creation_channel>()
                .Property(e => e.creation_channel_name)
                .IsUnicode(false);

            modelBuilder.Entity<creation_channel>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<culture>()
                .Property(e => e.culture_name)
                .IsUnicode(false);

            modelBuilder.Entity<culture>()
                .HasMany(e => e.culture_country_name)
                .WithRequired(e => e.culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<culture>()
                .HasMany(e => e.culture_subdivision_name)
                .WithRequired(e => e.culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<culture>()
                .HasMany(e => e.email_template_culture)
                .WithRequired(e => e.culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<culture>()
                .HasMany(e => e.events)
                .WithRequired(e => e.culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<culture>()
                .HasMany(e => e.store_template)
                .WithRequired(e => e.culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<custcare_comments>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<custom_email_template>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<custom_email_template>()
                .Property(e => e.body_txt)
                .IsUnicode(false);

            modelBuilder.Entity<custom_email_template>()
                .Property(e => e.body_html)
                .IsUnicode(false);

            modelBuilder.Entity<display_product_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<dm_image_approval_status>()
                .Property(e => e.image_approval_status_description)
                .IsUnicode(false);

            modelBuilder.Entity<dm_personalization_image>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<dm_personalization_image>()
                .Property(e => e.approver_name)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.property)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<email_import_test>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<email_import_test>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<email_import_test>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<email_template>()
                .Property(e => e.email_template_name)
                .IsUnicode(false);

            modelBuilder.Entity<email_template>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<email_template>()
                .Property(e => e.param_procedure_call)
                .IsUnicode(false);

            modelBuilder.Entity<email_template>()
                .Property(e => e.from_name)
                .IsUnicode(false);

            modelBuilder.Entity<email_template>()
                .Property(e => e.from_email_address)
                .IsUnicode(false);

            modelBuilder.Entity<email_template>()
                .Property(e => e.reply_to_name)
                .IsUnicode(false);

            modelBuilder.Entity<email_template>()
                .Property(e => e.reply_to_email_address)
                .IsUnicode(false);

            modelBuilder.Entity<email_template>()
                .Property(e => e.bounce_name)
                .IsUnicode(false);

            modelBuilder.Entity<email_template>()
                .Property(e => e.bounce_email_address)
                .IsUnicode(false);

            modelBuilder.Entity<email_template>()
                .HasMany(e => e.email_template_culture)
                .WithRequired(e => e.email_template)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<email_template>()
                .HasMany(e => e.email_template_tag)
                .WithRequired(e => e.email_template)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<email_template>()
                .HasMany(e => e.partner_payment_config)
                .WithOptional(e => e.email_template)
                .HasForeignKey(e => e.email_template_id);

            modelBuilder.Entity<email_template>()
                .HasMany(e => e.partner_payment_config1)
                .WithOptional(e => e.email_template1)
                .HasForeignKey(e => e.first_email_template_id);

            modelBuilder.Entity<email_template>()
                .HasMany(e => e.touch_change_log)
                .WithRequired(e => e.email_template)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<email_template_culture>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<email_template_culture>()
                .Property(e => e.body_html)
                .IsUnicode(false);

            modelBuilder.Entity<email_template_culture>()
                .Property(e => e.body_text)
                .IsUnicode(false);

            modelBuilder.Entity<email_template_culture>()
                .Property(e => e.footer_text)
                .IsUnicode(false);

            modelBuilder.Entity<email_template_culture>()
                .Property(e => e.footer_html)
                .IsUnicode(false);

            modelBuilder.Entity<email_template_field>()
                .Property(e => e.field_name)
                .IsUnicode(false);

            modelBuilder.Entity<email_template_field>()
                .Property(e => e.table_name)
                .IsUnicode(false);

            modelBuilder.Entity<email_template_field>()
                .Property(e => e.type_name)
                .IsUnicode(false);

            modelBuilder.Entity<email_template_field>()
                .HasMany(e => e.touch_change_log_details)
                .WithRequired(e => e.email_template_field)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<email_template_type>()
                .Property(e => e.email_template_name)
                .IsUnicode(false);

            modelBuilder.Entity<email_template_type>()
                .HasMany(e => e.email_template)
                .WithRequired(e => e.email_template_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.rda_tag)
                .IsUnicode(false);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.total_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.sub_total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.tax)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.freight)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.redeemed_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.supp_name)
                .IsUnicode(false);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.product_desc)
                .IsUnicode(false);

            modelBuilder.Entity<es_valid_orders_items>()
                .Property(e => e.product_type_desc)
                .IsUnicode(false);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.total_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.sub_total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.tax)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.freight)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.redeemed_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.supp_name)
                .IsUnicode(false);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.product_desc)
                .IsUnicode(false);

            modelBuilder.Entity<es_valid_orders_items_staging>()
                .Property(e => e.product_type_desc)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.event_name)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.redirect)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.humeur_representative)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.event_gasavingcard)
                .WithRequired(e => e._event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.event_group)
                .WithRequired(e => e._event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.event_participation)
                .WithRequired(e => e._event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<event_facebook_visitor>()
                .Property(e => e.facebook_id)
                .IsUnicode(false);

            modelBuilder.Entity<event_facebook_visitor>()
                .Property(e => e.facebook_image_url)
                .IsUnicode(false);

            modelBuilder.Entity<event_facebook_visitor>()
                .Property(e => e.facebook_firstname)
                .IsUnicode(false);

            modelBuilder.Entity<event_facebook_visitor>()
                .Property(e => e.facebook_lastname)
                .IsUnicode(false);

            modelBuilder.Entity<event_participation>()
                .Property(e => e.salutation)
                .IsUnicode(false);

            modelBuilder.Entity<event_participation>()
                .HasMany(e => e.personalizations)
                .WithRequired(e => e.event_participation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<event_participation>()
                .HasMany(e => e.stats_personalization)
                .WithRequired(e => e.event_participation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<event_status>()
                .Property(e => e.event_status_name)
                .IsUnicode(false);

            modelBuilder.Entity<event_status>()
                .HasMany(e => e.events)
                .WithRequired(e => e.event_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<event_total_amount>()
                .Property(e => e.total_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<event_total_amount>()
                .Property(e => e.total_donation_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<event_total_amount>()
                .Property(e => e.total_profit)
                .HasPrecision(19, 4);

            modelBuilder.Entity<event_type>()
                .Property(e => e.event_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<event_type>()
                .HasMany(e => e.events)
                .WithRequired(e => e.event_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<exception_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<exception_type>()
                .HasMany(e => e.payment_exception_type)
                .WithRequired(e => e.exception_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<external_account>()
                .Property(e => e.fsm_id)
                .IsUnicode(false);

            modelBuilder.Entity<group>()
                .Property(e => e.external_group_id)
                .IsUnicode(false);

            modelBuilder.Entity<group>()
                .Property(e => e.group_name)
                .IsUnicode(false);

            modelBuilder.Entity<group>()
                .Property(e => e.group_url)
                .IsUnicode(false);

            modelBuilder.Entity<group>()
                .Property(e => e.redirect)
                .IsUnicode(false);

            modelBuilder.Entity<group>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<group>()
                .HasMany(e => e.event_group)
                .WithRequired(e => e.group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<group>()
                .HasMany(e => e.group1)
                .WithOptional(e => e.group2)
                .HasForeignKey(e => e.parent_group_id);

            modelBuilder.Entity<group>()
                .HasMany(e => e.group_group_status)
                .WithRequired(e => e.group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<group_status>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<group_status>()
                .HasMany(e => e.group_group_status)
                .WithRequired(e => e.group_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<group_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<group_type>()
                .HasMany(e => e.events)
                .WithRequired(e => e.group_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<image_approval_status>()
                .Property(e => e.image_approval_status_description)
                .IsUnicode(false);

            modelBuilder.Entity<image_approval_status>()
                .HasMany(e => e.personalization_image)
                .WithRequired(e => e.image_approval_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<kd_alumni>()
                .Property(e => e.Chapter)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni>()
                .Property(e => e.Alumni_Name)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni>()
                .Property(e => e.redirect)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni>()
                .Property(e => e.EMAIL_ID)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni>()
                .Property(e => e.PW)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanismember>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanismember>()
                .Property(e => e.middle_name)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanismember>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanismember>()
                .Property(e => e.gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<kiwanismember>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanismember>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanismember>()
                .Property(e => e.external_member_id)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanismember>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanismember>()
                .Property(e => e.parent_first_name)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanismember>()
                .Property(e => e.parent_last_name)
                .IsUnicode(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.cultures)
                .WithRequired(e => e.language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<member>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.middle_name)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.external_member_id)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.parent_first_name)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.parent_last_name)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.greeting)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.email_validation_response_message)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .HasMany(e => e.member_hierarchy)
                .WithRequired(e => e.member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<member>()
                .HasMany(e => e.member_phone_number)
                .WithRequired(e => e.member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<member>()
                .HasMany(e => e.member_postal_address)
                .WithRequired(e => e.member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<member_hierarchy>()
                .HasMany(e => e.event_participation)
                .WithRequired(e => e.member_hierarchy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<member_hierarchy>()
                .HasMany(e => e.groups)
                .WithRequired(e => e.member_hierarchy)
                .HasForeignKey(e => e.sponsor_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<member_hierarchy>()
                .HasMany(e => e.member_hierarchy1)
                .WithOptional(e => e.member_hierarchy2)
                .HasForeignKey(e => e.parent_member_hierarchy_id);

            modelBuilder.Entity<member_type>()
                .Property(e => e.member_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<member_type>()
                .Property(e => e.email_description)
                .IsUnicode(false);

            modelBuilder.Entity<opt_status>()
                .Property(e => e.opt_status_name)
                .IsUnicode(false);

            modelBuilder.Entity<participant_total_amount>()
                .Property(e => e.participant_name)
                .IsUnicode(false);

            modelBuilder.Entity<participant_total_amount>()
                .Property(e => e.total_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<participant_total_amount>()
                .Property(e => e.total_supporters)
                .HasPrecision(19, 4);

            modelBuilder.Entity<participant_total_amount>()
                .Property(e => e.total_donation_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<participant_total_amount>()
                .Property(e => e.total_donors)
                .HasPrecision(19, 4);

            modelBuilder.Entity<participant_total_amount>()
                .Property(e => e.total_profit)
                .HasPrecision(19, 4);

            modelBuilder.Entity<participation_channel>()
                .Property(e => e.participation_channel_name)
                .IsUnicode(false);

            modelBuilder.Entity<Partner_Activation_Commission>()
                .Property(e => e.Fixed_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Partner_Commission_Range>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Partner_Sales_Commission>()
                .Property(e => e.Fixed_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Partner_Sales_Commission>()
                .Property(e => e.Variable_Rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Partner_Sales_Commission>()
                .Property(e => e.pure_variable_rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<payment>()
                .Property(e => e.paid_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<payment>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.phone_number)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.address_1)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.address_2)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .HasMany(e => e.payment_exception_type)
                .WithRequired(e => e.payment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<payment>()
                .HasMany(e => e.payment_item)
                .WithRequired(e => e.payment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<payment>()
                .HasMany(e => e.payment_payment_status)
                .WithRequired(e => e.payment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<payment_batch>()
                .Property(e => e.filename)
                .IsUnicode(false);

            modelBuilder.Entity<payment_comment>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<payment_comment>()
                .Property(e => e.nt_login)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info>()
                .Property(e => e.payment_name)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info>()
                .Property(e => e.on_behalf_of_name)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info>()
                .Property(e => e.ship_to_name)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info>()
                .Property(e => e.ssn)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info>()
                .HasMany(e => e.payments)
                .WithRequired(e => e.payment_info)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<payment_item>()
                .Property(e => e.order_detail_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<payment_item>()
                .Property(e => e.profit_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<payment_period>()
                .HasMany(e => e.payments)
                .WithRequired(e => e.payment_period)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<payment_status>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<payment_status>()
                .HasMany(e => e.payment_payment_status)
                .WithRequired(e => e.payment_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<payment_type>()
                .Property(e => e.payment_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<payment_type>()
                .HasMany(e => e.payments)
                .WithRequired(e => e.payment_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<personalization>()
                .Property(e => e.header_title1)
                .IsUnicode(false);

            modelBuilder.Entity<personalization>()
                .Property(e => e.header_title2)
                .IsUnicode(false);

            modelBuilder.Entity<personalization>()
                .Property(e => e.body)
                .IsUnicode(false);

            modelBuilder.Entity<personalization>()
                .Property(e => e.fundraising_goal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<personalization>()
                .Property(e => e.site_bgcolor)
                .IsUnicode(false);

            modelBuilder.Entity<personalization>()
                .Property(e => e.header_bgcolor)
                .IsUnicode(false);

            modelBuilder.Entity<personalization>()
                .Property(e => e.header_color)
                .IsUnicode(false);

            modelBuilder.Entity<personalization>()
                .Property(e => e.group_url)
                .IsUnicode(false);

            modelBuilder.Entity<personalization>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<personalization>()
                .Property(e => e.redirect)
                .IsUnicode(false);

            modelBuilder.Entity<personalization>()
                .HasMany(e => e.personalization_image)
                .WithRequired(e => e.personalization)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<personalization_image>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<personalization_image>()
                .Property(e => e.approver_name)
                .IsUnicode(false);

            modelBuilder.Entity<personalization_image>()
                .Property(e => e.high_image_url)
                .IsUnicode(false);

            modelBuilder.Entity<phone_number>()
                .Property(e => e.phone_number1)
                .IsUnicode(false);

            modelBuilder.Entity<phone_number>()
                .HasMany(e => e.member_phone_number)
                .WithRequired(e => e.phone_number)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<phone_number_type>()
                .Property(e => e.phone_number_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<phone_number_type>()
                .HasMany(e => e.member_phone_number)
                .WithRequired(e => e.phone_number_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.address_1)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.address_2)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.matching_code)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .HasMany(e => e.member_postal_address)
                .WithRequired(e => e.postal_address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<postal_address_type>()
                .Property(e => e.postal_address_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address_type>()
                .HasMany(e => e.member_postal_address)
                .WithRequired(e => e.postal_address_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<precalculatedvalue>()
                .Property(e => e.sales_amount_grand_total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<prize>()
                .Property(e => e.prize_name)
                .IsUnicode(false);

            modelBuilder.Entity<prize>()
                .HasMany(e => e.prize_item)
                .WithRequired(e => e.prize)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<prize>()
                .HasMany(e => e.prize1)
                .WithOptional(e => e.prize2)
                .HasForeignKey(e => e.parent_prize_id);

            modelBuilder.Entity<prize>()
                .HasMany(e => e.program_partner_prize)
                .WithRequired(e => e.prize)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<prize_item>()
                .Property(e => e.prize_item_code)
                .IsUnicode(false);

            modelBuilder.Entity<prize_item>()
                .HasOptional(e => e.earned_prize)
                .WithRequired(e => e.prize_item);

            modelBuilder.Entity<prize_type>()
                .Property(e => e.prize_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<prize_type>()
                .HasMany(e => e.prizes)
                .WithRequired(e => e.prize_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product_offer>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<product_offer>()
                .HasMany(e => e.email_template_tag)
                .WithRequired(e => e.product_offer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<program>()
                .Property(e => e.program_name)
                .IsUnicode(false);

            modelBuilder.Entity<program>()
                .HasMany(e => e.program_partner)
                .WithRequired(e => e.program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<program_partner>()
                .Property(e => e.program_url)
                .IsUnicode(false);

            modelBuilder.Entity<program_partner>()
                .HasMany(e => e.program_partner_prize)
                .WithRequired(e => e.program_partner)
                .HasForeignKey(e => new { e.program_id, e.partner_id })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<qsp_matching_code>()
                .Property(e => e.cust_billing_matching_code)
                .IsUnicode(false);

            modelBuilder.Entity<qsp_matching_code>()
                .Property(e => e.cust_shipping_matching_code)
                .IsUnicode(false);

            modelBuilder.Entity<qsp_matching_code>()
                .Property(e => e.account_matching_code)
                .IsUnicode(false);

            modelBuilder.Entity<stats_personalization_item>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<stats_personalization_item>()
                .HasMany(e => e.stats_personalization)
                .WithRequired(e => e.stats_personalization_item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<stats_personalization_section>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Partner_Sales_Commission)
                .WithRequired(e => e.Store)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<store_template>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<store_template>()
                .HasMany(e => e.partner_store_template)
                .WithRequired(e => e.store_template)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<subdivision>()
                .HasMany(e => e.culture_subdivision_name)
                .WithRequired(e => e.subdivision)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tag>()
                .Property(e => e.start_tag_name)
                .IsUnicode(false);

            modelBuilder.Entity<tag>()
                .Property(e => e.end_tag_name)
                .IsUnicode(false);

            modelBuilder.Entity<tag>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<tag>()
                .HasMany(e => e.email_template_tag)
                .WithRequired(e => e.tag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tellafriend>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<tellafriend>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<tellafriend>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<tellafriend>()
                .Property(e => e.body_html)
                .IsUnicode(false);

            modelBuilder.Entity<tellafriend>()
                .Property(e => e.body_txt)
                .IsUnicode(false);

            modelBuilder.Entity<tellafriend>()
                .HasMany(e => e.tellafriend_recipient)
                .WithRequired(e => e.tellafriend)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tellafriend_recipient>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<tellafriend_recipient>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<test_table>()
                .Property(e => e.test_varchar)
                .IsUnicode(false);

            modelBuilder.Entity<theme>()
                .Property(e => e.theme_name)
                .IsUnicode(false);

            modelBuilder.Entity<theme>()
                .HasMany(e => e.default_email_template)
                .WithRequired(e => e.theme)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<touch_action>()
                .Property(e => e.action_desc)
                .IsUnicode(false);

            modelBuilder.Entity<touch_change_log>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<touch_change_log>()
                .HasMany(e => e.touch_change_log_details)
                .WithRequired(e => e.touch_change_log)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<touch_change_log_details>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<touch_change_log_details>()
                .Property(e => e.refreshed_by)
                .IsUnicode(false);

            modelBuilder.Entity<touch_info>()
                .HasMany(e => e.custom_email_template)
                .WithRequired(e => e.touch_info)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<touch_info>()
                .HasMany(e => e.touches)
                .WithRequired(e => e.touch_info)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user_oauthmembership>()
                .Property(e => e.provider)
                .IsUnicode(false);

            modelBuilder.Entity<user_oauthmembership>()
                .Property(e => e.providerUserId)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<xfactor_member>()
                .Property(e => e.external_member_id)
                .IsUnicode(false);

            modelBuilder.Entity<xfactor_member>()
                .Property(e => e.external_group_id)
                .IsUnicode(false);

            modelBuilder.Entity<xfactor_member>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<xfactor_member>()
                .Property(e => e.middle_name)
                .IsUnicode(false);

            modelBuilder.Entity<xfactor_member>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<xfactor_member>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<xfactor_member>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<xfactor_member>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<aoii_chapters>()
                .Property(e => e.Group_Name)
                .IsUnicode(false);

            modelBuilder.Entity<aoii_chapters>()
                .Property(e => e.F2)
                .IsUnicode(false);

            modelBuilder.Entity<aoii_chapters>()
                .Property(e => e.Email_ID)
                .IsUnicode(false);

            modelBuilder.Entity<aoii_chapters>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<aoii_chapters_2006>()
                .Property(e => e.chapter)
                .IsUnicode(false);

            modelBuilder.Entity<bounce_to_transfer>()
                .Property(e => e.email_address)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<campaign_merges>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<direct_mail_info>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<direct_mail_info>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<direct_mail_info>()
                .Property(e => e.document_path)
                .IsUnicode(false);

            modelBuilder.Entity<direct_mail_info>()
                .Property(e => e.member_hierarchy_id)
                .IsFixedLength();

            modelBuilder.Entity<direct_mail_letter>()
                .Property(e => e.letter_bar_code_1)
                .IsUnicode(false);

            modelBuilder.Entity<direct_mail_letter>()
                .Property(e => e.letter_bar_code_2)
                .IsUnicode(false);

            modelBuilder.Entity<direct_mail_template>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<direct_mail_template>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<direct_mail_template>()
                .Property(e => e.document_path)
                .IsUnicode(false);

            modelBuilder.Entity<event_change>()
                .Property(e => e.user)
                .IsUnicode(false);

            modelBuilder.Entity<ewnchapter>()
                .Property(e => e.Chapter)
                .IsUnicode(false);

            modelBuilder.Entity<ewnchapter>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ewnchapter>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ewnchapter>()
                .Property(e => e.Email_address)
                .IsUnicode(false);

            modelBuilder.Entity<ewnchapter>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<ewnchapter>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<ewnparticipant>()
                .Property(e => e.Name_Last)
                .IsUnicode(false);

            modelBuilder.Entity<ewnparticipant>()
                .Property(e => e.Name_First)
                .IsUnicode(false);

            modelBuilder.Entity<ewnparticipant>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<ewnparticipant>()
                .Property(e => e.Current_Chapter_City)
                .IsUnicode(false);

            modelBuilder.Entity<ewnparticipant>()
                .Property(e => e.eMail_Address)
                .IsUnicode(false);

            modelBuilder.Entity<ewnpaticipant_possibleduplicate>()
                .Property(e => e.Name_Last)
                .IsUnicode(false);

            modelBuilder.Entity<ewnpaticipant_possibleduplicate>()
                .Property(e => e.Name_First)
                .IsUnicode(false);

            modelBuilder.Entity<ewnpaticipant_possibleduplicate>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<ewnpaticipant_possibleduplicate>()
                .Property(e => e.Current_Chapter_City)
                .IsUnicode(false);

            modelBuilder.Entity<ewnpaticipant_possibleduplicate>()
                .Property(e => e.eMail_Address)
                .IsUnicode(false);

            modelBuilder.Entity<generated_pass>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<KAT>()
                .Property(e => e.short_name)
                .IsUnicode(false);

            modelBuilder.Entity<KAT>()
                .Property(e => e.univ_name)
                .IsUnicode(false);

            modelBuilder.Entity<KAT>()
                .Property(e => e.redirect)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni_2006>()
                .Property(e => e.Chapter)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni_2006>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni_2006>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<kd_alumni_2006>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanis_phone_to_add>()
                .Property(e => e.phone_number)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanischange>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanischange>()
                .Property(e => e.middle_name)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanischange>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanischange>()
                .Property(e => e.gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<kiwanischange>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanischange>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanischange>()
                .Property(e => e.external_member_id)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanischange>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanischange>()
                .Property(e => e.parent_first_name)
                .IsUnicode(false);

            modelBuilder.Entity<kiwanischange>()
                .Property(e => e.parent_last_name)
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.middle_name)
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.external_member_id)
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.parent_first_name)
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.parent_last_name)
                .IsUnicode(false);

            modelBuilder.Entity<member_20110621>()
                .Property(e => e.greeting)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info_corr_20080918>()
                .Property(e => e.payment_name)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info_corr_20080918>()
                .Property(e => e.on_behalf_of_name)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info_corr_20080918>()
                .Property(e => e.ship_to_name)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info_corr_20080918>()
                .Property(e => e.ssn)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info_corr_20080918_2>()
                .Property(e => e.payment_name)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info_corr_20080918_2>()
                .Property(e => e.on_behalf_of_name)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info_corr_20080918_2>()
                .Property(e => e.ship_to_name)
                .IsUnicode(false);

            modelBuilder.Entity<payment_info_corr_20080918_2>()
                .Property(e => e.ssn)
                .IsUnicode(false);

            modelBuilder.Entity<prize_to_be_removed>()
                .Property(e => e.prize_item_code)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.email_template_name)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.param_procedure_call)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.from_name)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.from_email_address)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.reply_to_name)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.reply_to_email_address)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.bounce_name)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.bounce_email_address)
                .IsUnicode(false);

            modelBuilder.Entity<temp_customer>()
                .Property(e => e.lead_email)
                .IsUnicode(false);

            modelBuilder.Entity<temp_customer>()
                .Property(e => e.org_email)
                .IsUnicode(false);

            modelBuilder.Entity<temp_customer>()
                .Property(e => e.org_name)
                .IsUnicode(false);

            modelBuilder.Entity<temp_customer>()
                .Property(e => e.group_name)
                .IsUnicode(false);

            modelBuilder.Entity<temp_customer>()
                .Property(e => e.part_email)
                .IsUnicode(false);

            modelBuilder.Entity<temp_customer>()
                .Property(e => e.part_name)
                .IsUnicode(false);

            modelBuilder.Entity<temp_customer>()
                .Property(e => e.supp_email)
                .IsUnicode(false);

            modelBuilder.Entity<temp_customer>()
                .Property(e => e.supp_name)
                .IsUnicode(false);

            modelBuilder.Entity<temp_group_member>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<temp_member>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<temp_member>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<temp_member_bak>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<temp_member_bak>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<temp_phone_number>()
                .Property(e => e.phone_number)
                .IsUnicode(false);

            modelBuilder.Entity<temp_postal_address>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<temp_postal_address>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<temp_postal_address>()
                .Property(e => e.zip)
                .IsUnicode(false);

            modelBuilder.Entity<web_action>()
                .Property(e => e.value)
                .IsUnicode(false);
        }
    }
}
