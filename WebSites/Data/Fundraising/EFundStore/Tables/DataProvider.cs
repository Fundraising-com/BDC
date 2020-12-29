namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataProvider : DbContext
    {
        public DataProvider()
            : base("name=EFundStore")
        {
        }

      public virtual DbSet<review> reviews { get; set; }
      public virtual DbSet<user> users { get; set; }
        public virtual DbSet<user_profile> user_profiles { get; set; }
        public virtual DbSet<product_image> product_images { get; set; }
        public virtual DbSet<shipping_fee> shipping_fees { get; set; }
        public virtual DbSet<shipping_fee_detail> shipping_fee_details { get; set; }
        public virtual DbSet<product_profit> product_profits { get; set; }
        public virtual DbSet<banner> banners { get; set; }
        public virtual DbSet<notification> notifications { get; set; }
        public virtual DbSet<package_exclusion> package_exclusions { get; set; }
        public virtual DbSet<view_port> view_ports { get; set; }
        public virtual DbSet<banner_view_port> banners_view_ports { get; set; }
        public virtual DbSet<page_route_mapper> page_route_mappers { get; set; }
        public virtual DbSet<accounting_class> accounting_class { get; set; }
        public virtual DbSet<best_time_call> best_time_call { get; set; }
        public virtual DbSet<best_time_call_desc> best_time_call_desc { get; set; }
        public virtual DbSet<brochure_image> brochure_image { get; set; }
        public virtual DbSet<campaign_reason> campaign_reason { get; set; }
        public virtual DbSet<campaign_reason_desc> campaign_reason_desc { get; set; }
        public virtual DbSet<carrier> carriers { get; set; }
        public virtual DbSet<carrier_shipping_option> carrier_shipping_option { get; set; }
        public virtual DbSet<choice> choices { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<client_address> client_address { get; set; }
        public virtual DbSet<client_address_type> client_address_type { get; set; }
        public virtual DbSet<client_sequence> client_sequence { get; set; }
        public virtual DbSet<control_type> control_type { get; set; }
        public virtual DbSet<country> countries { get; set; }
        public virtual DbSet<credit_card> credit_card { get; set; }
        public virtual DbSet<credit_card_type> credit_card_type { get; set; }
        public virtual DbSet<culture> cultures { get; set; }
        public virtual DbSet<culture_country> culture_country { get; set; }
        public virtual DbSet<culture_subdivision> culture_subdivision { get; set; }
        public virtual DbSet<division> divisions { get; set; }
        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<group_type> group_type { get; set; }
        public virtual DbSet<group_type_desc> group_type_desc { get; set; }
        public virtual DbSet<hear_about_us> hear_about_us { get; set; }
        public virtual DbSet<hear_about_us_desc> hear_about_us_desc { get; set; }
        public virtual DbSet<language> languages { get; set; }
        public virtual DbSet<newsletter> newsletters { get; set; }
        public virtual DbSet<follow_up_email_suggested_products> follow_up_products { get; set; }
        public virtual DbSet<newsletter_subscription> newsletter_subscription { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<order_coupon> order_coupon { get; set; }
        public virtual DbSet<order_sale> order_sale { get; set; }
        public virtual DbSet<organization_type> organization_type { get; set; }
        public virtual DbSet<organization_type_desc> organization_type_desc { get; set; }
        public virtual DbSet<package> packages { get; set; }
        public virtual DbSet<package_category> package_category { get; set; }
        public virtual DbSet<package_desc> package_desc { get; set; }
        public virtual DbSet<package_interest> package_interest { get; set; }
        public virtual DbSet<package_package_category> package_package_category { get; set; }
        public virtual DbSet<partner_contact> partner_contact { get; set; }
        public virtual DbSet<partner_package> partner_package { get; set; }
        public virtual DbSet<partner_web_detail> partner_web_detail { get; set; }
        public virtual DbSet<partner_web_form> partner_web_form { get; set; }
        public virtual DbSet<blog_post> blog_post { get; set; }
        public virtual DbSet<blog_categories> blog_categories { get; set; }
        public virtual DbSet<blog_tags> blog_tags { get; set; }
        public virtual DbSet<post_tag> post_tag { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<currency> currencies { get; set; }
        public virtual DbSet<product_class> product_class { get; set; }
        public virtual DbSet<product_class_desc> product_class_desc { get; set; }
        public virtual DbSet<product_culture> product_culture { get; set; }
        public virtual DbSet<product_desc> product_desc { get; set; }
        public virtual DbSet<product_package> product_package { get; set; }
        public virtual DbSet<product_price_info> product_price_info { get; set; }
        public virtual DbSet<program> programs { get; set; }
        public virtual DbSet<program_partner> program_partner { get; set; }
        public virtual DbSet<question> questions { get; set; }
        public virtual DbSet<question_param_target> question_param_target { get; set; }
        public virtual DbSet<salutation> salutations { get; set; }
        public virtual DbSet<salutation_desc> salutation_desc { get; set; }
        public virtual DbSet<session> sessions { get; set; }
        public virtual DbSet<session_item> session_item { get; set; }
        public virtual DbSet<shopping_cart> shopping_cart { get; set; }
        public virtual DbSet<shopping_cart_item> shopping_cart_item { get; set; }
        public virtual DbSet<story> stories { get; set; }
        public virtual DbSet<story_type> story_type { get; set; }
        public virtual DbSet<subdivision> subdivisions { get; set; }
        public virtual DbSet<supplier> suppliers { get; set; }
        public virtual DbSet<survey> surveys { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<temp_lead> temp_lead { get; set; }
        public virtual DbSet<template> templates { get; set; }
        public virtual DbSet<title> titles { get; set; }
        public virtual DbSet<title_desc> title_desc { get; set; }
        public virtual DbSet<unsubscribe> unsubscribes { get; set; }
        public virtual DbSet<user_vote> user_vote { get; set; }
        public virtual DbSet<web_form> web_form { get; set; }
        public virtual DbSet<web_form_question> web_form_question { get; set; }
        public virtual DbSet<web_form_type_desc> web_form_type_desc { get; set; }
        public virtual DbSet<website> websites { get; set; }
        public virtual DbSet<accounting_class_shipping_fee> accounting_class_shipping_fee { get; set; }
        public virtual DbSet<questions_entry_form> questions_entry_form { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<accounting_class>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<best_time_call>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<best_time_call_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<brochure_image>()
                .Property(e => e.base_filename)
                .IsUnicode(false);

            modelBuilder.Entity<brochure_image>()
                .Property(e => e.file_ext)
                .IsUnicode(false);

            modelBuilder.Entity<campaign_reason>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<campaign_reason_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<carrier>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<carrier_shipping_option>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<choice>()
                .Property(e => e.choice_desc)
                .IsUnicode(false);

            modelBuilder.Entity<choice>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<choice>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<choice>()
                .HasMany(e => e.user_vote)
                .WithRequired(e => e.choice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<choice>()
                .HasMany(e => e.surveys)
                .WithMany(e => e.choices)
                .Map(m => m.ToTable("survey_choice").MapLeftKey("choice_id").MapRightKey("survey_id"));

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
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.country_code)
                .IsUnicode(false);

            modelBuilder.Entity<client_address>()
                .Property(e => e.attention_of)
                .IsUnicode(false);

            modelBuilder.Entity<client_address_type>()
                .Property(e => e.client_address_type_id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<client_address_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<client_sequence>()
                .Property(e => e.client_sequence_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<client_sequence>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<control_type>()
                .Property(e => e.assembly_name)
                .IsUnicode(false);

            modelBuilder.Entity<control_type>()
                .Property(e => e._namespace)
                .IsUnicode(false);

            modelBuilder.Entity<control_type>()
                .Property(e => e.class_name)
                .IsUnicode(false);

            modelBuilder.Entity<control_type>()
                .Property(e => e.display_attribute)
                .IsUnicode(false);

            modelBuilder.Entity<control_type>()
                .Property(e => e.binding_name)
                .IsUnicode(false);

            modelBuilder.Entity<control_type>()
                .Property(e => e.event_handler_name)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card>()
                .Property(e => e.last_digits)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_type>()
                .Property(e => e.credit_card_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_type>()
                .Property(e => e.credit_card_image)
                .IsUnicode(false);

            modelBuilder.Entity<culture>()
                .Property(e => e.culture_name)
                .IsUnicode(false);

            modelBuilder.Entity<division>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<division>()
                .Property(e => e.logo)
                .IsUnicode(false);

            modelBuilder.Entity<division>()
                .Property(e => e.short_name)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.property)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<group_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<group_type_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<hear_about_us>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<hear_about_us_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<newsletter_subscription>()
                .Property(e => e.referrer)
                .IsUnicode(false);

            modelBuilder.Entity<newsletter_subscription>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<newsletter_subscription>()
                .Property(e => e.fullname)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.order_number)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.order_total)
                .HasPrecision(9, 2);

            modelBuilder.Entity<order>()
                .Property(e => e.shipping_total)
                .HasPrecision(9, 2);

            modelBuilder.Entity<order>()
                .Property(e => e.tax_total)
                .HasPrecision(9, 2);

            modelBuilder.Entity<organization_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<organization_type_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<package>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<package>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<package_category>()
                .Property(e => e.package_category_title)
                .IsUnicode(false);

            modelBuilder.Entity<package_category>()
                .Property(e => e.package_category_desc)
                .IsUnicode(false);

            modelBuilder.Entity<package_category>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<package_category>()
                .Property(e => e.product_url)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.short_desc)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.long_desc)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.extra_desc)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.page_name)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.image_name)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.page_title)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.image_alt_text)
                .IsUnicode(false);

            modelBuilder.Entity<package_desc>()
                .Property(e => e.configuration)
                .IsUnicode(false);

            modelBuilder.Entity<package_package_category>()
                .Property(e => e.package_category_id)
                .IsFixedLength();

            modelBuilder.Entity<partner_contact>()
                .Property(e => e.section_name)
                .IsUnicode(false);

            modelBuilder.Entity<partner_contact>()
                .Property(e => e.section_value)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_detail>()
                .Property(e => e.top_menu)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_detail>()
                .Property(e => e.left_menu)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_detail>()
                .Property(e => e.right_menu)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_detail>()
                .Property(e => e.images_path)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_detail>()
                .Property(e => e.default_color)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_detail>()
                .Property(e => e.short_cut_menu)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_detail>()
                .Property(e => e.product_image_map)
                .IsUnicode(false);

            modelBuilder.Entity<partner_web_form>()
                .Property(e => e.recipient)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.raising_potential)
                .HasPrecision(15, 4);

            modelBuilder.Entity<product>()
                .Property(e => e.product_code)
                .IsUnicode(false);

            modelBuilder.Entity<product_class>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<product_class_desc>()
                .Property(e => e.product_class_desc1)
                .IsUnicode(false);

            modelBuilder.Entity<product_class_desc>()
                .Property(e => e.min_requirement)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.short_desc)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.long_desc)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.page_name)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.image_name)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.extra_desc)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.page_title)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.image_alt_text)
                .IsUnicode(false);

            modelBuilder.Entity<product_desc>()
                .Property(e => e.configuration)
                .IsUnicode(false);

            modelBuilder.Entity<product_price_info>()
                .Property(e => e.country_code)
                .IsUnicode(false);

            modelBuilder.Entity<product_price_info>()
                .Property(e => e.unit_price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<program>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<program_partner>()
                .Property(e => e.program_url)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.field_name)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.default_value)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.stored_proc_to_call)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.answer_type)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.field_value)
                .IsUnicode(false);

            modelBuilder.Entity<question_param_target>()
                .Property(e => e.parameter_target)
                .IsUnicode(false);

            modelBuilder.Entity<salutation>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<salutation_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<session_item>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<session_item>()
                .Property(e => e.value)
                .IsUnicode(false);            

            modelBuilder.Entity<story>()
                .Property(e => e.story_text)
                .IsUnicode(false);

            modelBuilder.Entity<story_type>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.name)
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
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<survey>()
                .Property(e => e.page_name)
                .IsUnicode(false);

            modelBuilder.Entity<survey>()
                .HasMany(e => e.user_vote)
                .WithRequired(e => e.survey)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.channel_code)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.salutation)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.organization)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.street_address)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.state_code)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.country_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.day_phone)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.day_time_call)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.evening_phone)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.fax)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.day_phone_ext)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.evening_phone_ext)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.other_phone)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.cookie_content)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.group_web_site)
                .IsUnicode(false);

            modelBuilder.Entity<temp_lead>()
                .Property(e => e.other_phone_ext)
                .IsUnicode(false);

            modelBuilder.Entity<template>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<template>()
                .Property(e => e.path)
                .IsUnicode(false);

            modelBuilder.Entity<title>()
                .Property(e => e.title_desc)
                .IsUnicode(false);

            modelBuilder.Entity<title_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<unsubscribe>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user_vote>()
                .Property(e => e.session_id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<web_form>()
                .Property(e => e.web_form_desc)
                .IsUnicode(false);

            modelBuilder.Entity<web_form>()
                .Property(e => e.stored_proc_to_call)
                .IsUnicode(false);

            modelBuilder.Entity<web_form_type_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<website>()
                .Property(e => e.website_dns)
                .IsUnicode(false);

            modelBuilder.Entity<accounting_class_shipping_fee>()
                .Property(e => e.min_amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<accounting_class_shipping_fee>()
                .Property(e => e.max_amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<questions_entry_form>()
                .Property(e => e.insert_table)
                .IsUnicode(false);

            modelBuilder.Entity<questions_entry_form>()
                .Property(e => e.insert_column)
                .IsUnicode(false);

            modelBuilder.Entity<questions_entry_form>()
                .Property(e => e.default_value)
                .IsUnicode(false);
        }
    }
}
