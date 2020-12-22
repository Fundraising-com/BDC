namespace GA.BDC.Web.MGP.Helpers
{
   // ReSharper disable InconsistentNaming
   public static class Constants
   {
      /// <summary>
      /// Default USA Profit setting
      /// </summary>
      public const string DEFAULT_PROFIT_PERCENTAGE_USA = "40";
      public const double DEFAULT_PROFIT_PERCENTAGE_USA_RATE = 40D;
      /// <summary>
      /// Default CANADA Profit setting
      /// </summary>
      public const string DEFAULT_PROFIT_PERCENTAGE_CAN = "37";
      public const double DEFAULT_PROFIT_PERCENTAGE_CAN_RATE = 37D;
      /// <summary>
      /// Session key for the partner Id
      /// </summary>
      public const string SESSION_KEY_PARTNER_ID = "partner_id";
      /// <summary>
      /// Session key for the event Id
      /// </summary>
      public const string SESSION_KEY_EVENT_ID = "event_id";
      /// <summary>
      /// Session key for the event participation Id
      /// </summary>
      public const string SESSION_KEY_EVENT_PARTICIPATION_ID = "event_participation_id";
      /// <summary>
      /// Session key for the participant home Id
      /// </summary>
      public const string SESSION_KEY_PARTICIPANT_HOME_ID = "ph_id";
      /// <summary>
      /// Session key for the touch Id
      /// </summary>
      public const string SESSION_KEY_TOUCH_ID = "touch_id";
      /// <summary>
      /// Session key for the lead Id
      /// </summary>
      public const string SESSION_KEY_LEAD_ID = "lead_id";
      /// <summary>
      /// Session key for the promotion Id
      /// </summary>
      public const string SESSION_KEY_PROMOTION_ID = "promotion_id";
      /// <summary>
      /// Session key for the promotion Id
      /// </summary>
      public const string SESSION_KEY_FCEXTERNAL_ID = "fc_external_id";
      /// <summary>
      /// Session key for the participant home Id
      /// </summary>
      public const string SESSION_KEY_PERSONALIZATION_ID = "personalization_id";
      /// <summary>
      /// Session key for the creation channel Id
      /// </summary>
      public const string SESSION_KEY_CREATION_CHANNEL_ID = "creation_channel_id";
      /// <summary>
      /// Session key for the GA external supporter Id
      /// </summary>
      public const string SESSION_KEY_GA_EXTERNAL_SUPPORTER_ID = "ga_external_supporter_id";
      /// <summary>
      /// Session key for disabling header section
      /// </summary>
      public const string SESSION_KEY_DISABLE_HEADER = "disable_header";
      /// <summary>
      /// Session key for disabling footer section
      /// </summary>
      public const string SESSION_KEY_DISABLE_FOOTER = "disable_footer";
      /// <summary>
      /// Session key for user inputted password
      /// </summary>
      public const string SESSION_KEY_PASSWORD = "password";
      /// <summary>
      /// Query parameter key for the partner Id as integer
      /// </summary>
      public const string QUERY_PARAMETER_P = "p";
      /// <summary>
      /// Query parameter key for the partner Id as Guid
      /// </summary>
      public const string QUERY_PARAMETER_GID = "gid";
      /// <summary>
      /// Query parameter key for the event Id
      /// </summary>
      public const string QUERY_PARAMETER_EVENT_ID = "eventId";
      /// <summary>
      /// Query parameter key for the event Id
      /// </summary>
      public const string QUERY_PARAMETER_EVENT_ID_2 = "e";
      /// <summary>
      /// Query parameter key for the participant Id
      /// </summary>
      public const string QUERY_PARAMETER_PARTICIPANT_ID = "participantId";
      /// <summary>
      /// Query parameter key for the event participationId
      /// </summary>
      public const string QUERY_PARAMETER_EVENT_PARTICIPATION_ID = "ep";
      /// <summary>
      /// Query parameter key for the participant home Id
      /// </summary>
      public const string QUERY_PARAMETER_PARTICIPANT_HOME_ID = "ph";
      /// <summary>
      /// Query parameter key for the touchId
      /// </summary>
      public const string QUERY_PARAMETER_TOUCH_ID = "touch_id";
      /// <summary>
      /// Query parameter key for the touchId
      /// </summary>
      public const string QUERY_PARAMETER_TOUCH_ID_2 = "t";
      /// <summary>
      /// Query parameter key for the lead Id as integer
      /// </summary>
      public const string QUERY_PARAMETER_LEADID = "lID";
      /// <summary>
      /// Query parameter key for the promotion Id as integer
      /// </summary>
      public const string QUERY_PARAMETER_PROMOTIONID = "pr_id";
      /// <summary>
      /// Query parameter key for the personalization Id
      /// </summary>
      public const string QUERY_PARAMETER_PERSONALIZATION_ID = "pers";
      /// <summary>
      /// Query parameter key for the FC External Id as integer
      /// </summary>
      public const string QUERY_PARAMETER_FCEXTERNALID = "FCExtID";
      /// <summary>
      /// Query parameter key for the creation channel id override
      /// </summary>
      public const string QUERY_PARAMETER_CREATIONCHANNEL = "cc";
      /// <summary>
      /// Query parameter key for the order id (Donation)
      /// </summary>
      public const string QUERY_PARAMETER_ORDER_ID = "orId";
      /// <summary>
      /// Query parameter key for external organization id
      /// </summary>
      public const string QUERY_PARAMETER_EXTERNAL_ORGANIZATION_ID = "oID";
      /// <summary>
      /// Query parameter key for external group id
      /// </summary>
      public const string QUERY_PARAMETER_EXTERNAL_GROUP_ID = "grID";
      /// <summary>
      /// Query parameter key for Group name
      /// </summary>
      public const string QUERY_PARAMETER_GROUP_NAME = "gName";
      /// <summary>
      /// Query parameter key for organizer name
      /// </summary>
      public const string QUERY_PARAMETER_ORGANIZER_NAME = "oName";
      /// <summary>
      /// Query parameter key for organizer email
      /// </summary>
      public const string QUERY_PARAMETER_ORGANIZER_EMAIL = "oEmail";
      /// <summary>
      /// Query parameter key for GA external supporter id
      /// </summary>
      public const string QUERY_PARAMETER_GA_EXTERNAL_SUPPORTER_ID = "ext_supp";
      /// <summary>
      /// Query parameter key for AutoCreation Redirect URL
      /// </summary>
      public const string QUERY_PARAMETER_REDIRECT_URL = "rURL";
      /// <summary>
      /// Query parameter key for Header section
      /// </summary>
      public const string QUERY_PARAMETER_HEADER = "head";
      /// <summary>
      /// Query parameter key for Footer section
      /// </summary>
      public const string QUERY_PARAMETER_FOOTER = "foot";
      /// <summary>
      /// Custom attribute value to show only the partner logo
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_SHOW_ONLY_PARTNER_LOGO = "esubs_show_only_partner_logo";
      /// <summary>
      /// Custom attribute value to hide the main menu at the top of the page
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_HIDE_MAIN_MENU = "esubs_hide_main_menu";
      /// <summary>
      /// Custom attribute value to show the popular items at the group pages
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_GROUP_PAGE_SHOW_POPULAR_ITEMS = "esubs_group_page_show_popular_items";
      /// <summary>
      /// Show the popular item banner
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_GROUP_PAGE_SHOW_POPULAR_ITEMS_BANNER = "esubs_group_page_show_popular_items_banner";
      /// <summary>
      /// Hides any references to register as canadian
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_HIDE_CANADIAN_REGISTRATION_LINK = "esubs_hide_canadian_registration_link";
      /// <summary>
      /// Overrides the Demo Page Image
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_DEMO_PAGE_IMAGE = "esubs_demo_page_image";
      /// <summary>
      /// Custom attribute value to get image partner path
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_PARTNER_PATH = "partner_path";
      /// <summary>
      /// PAP Affiliate id
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_PAP_A_AID = "pap_a_aid";
      /// <summary>
      /// Custom attribute value to insert leads for the partner
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_INSERT_AS_LEAD = "esubs_insert_as_lead";
      /// <summary>
      /// Custom attribute value to allow prize program
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_PRIZE_PROGRAM = "esubs_prize_program";
      /// <summary>
      /// Custom attribute value to hide profit
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_HIDE_PROFIT = "esubs_hide_profit";
      /// <summary>
      /// Custom attribute value to hide the goal
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_HIDE_GOAL = "esubs_hide_goal";
      /// <summary>
      /// Custom attribute value to hide the how it works links
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_HIDE_HOWITWORKS = "esubs_hide_howitworks";
      /// <summary>
      /// Custom attribute value to hide the browse participants link in group page
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_HIDE_BROWSE_PARTICIPANTS = "esubs_hide_browse_participants";
      /// <summary>
      /// Custom attribute value to customize the Shop for out cause message
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_CUSTOMIZE_SHOP_NOW_FOR_OUR_CAUSE = "esubs_customize_shopnowforourcause";
      /// <summary>
      /// Custom attribute value to customize the participant register message
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_CUSTOMIZE_PARTICIPANT_REGISTER_MESSAGE = "esubs_customize_participant_register_message";
      /// <summary>
      /// Custom attribute value to hide the link to the FAQ page
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_GROUP_PAGE_HIDE_FAQ = "esubs_hide_faq";
      /// <summary>
      /// Custom attribute value to customize to Hide all the Reports
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_GROUP_PAGE_HIDE_ALL_REPORTS = "esubs_hide_all_reports";
      /// <summary>
      /// Custom attribute value to customize to Hide the Promote your Page menu
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_HIDE_PROMOTION_PAGE = "esubs_hide_promotion";
      /// <summary>
      /// Custom attribute value to customize to Hide the Promote your Page menu
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_ESUBS_CAN_NOT_CREATE_EVENT = "esubs_can_not_create_event";
      /// <summary>
      /// Overrides some CSS style rules
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_CSS_OVERRIDE_FILE = "esubs_css_override_file";
      /// <summary>
      /// Custom attribute value to hide check report
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_HIDE_CHECK_REPORT = "esubs_hide_check_report";
      /// <summary>
      /// Custom attribute value to redirect to landing page
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_REDIRECT_TO_LANDING_PAGE = "esubs_redirect_to_landing_page";
      /// <summary>
      /// Custom attribute value to redirect to landing page
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_SHOW_RECEIPT_LINK = "esubs_show_receipt_link";
      /// <summary>
      /// Custom attribute value to hide the JOIN button per events
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_HIDE_JOIN_BUTTON_BY_EVENTS = "esubs_hide_join_button_by_events";
      /// <summary>
      /// Custom attribute value to show total amount in Amount Raised Thermometer (as opposed to Total Profit)
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_SHOW_TOTAL_AMOUNT_IN_THERMOMETER = "esubs_show_total_amount_in_thermometer";
      /// <summary>
      /// Custom attribute value for esubs url
      /// </summary>
      public const string CUSTOM_ATTRIBUTE_ESUBS_URL = "esubs_url";
      /// <summary>
      /// Default group photo placeholder filename
      /// </summary>
      public const string GROUP_PHOTO_PLACEHOLDER_FILENAME = "groupphotoplaceholder.gif";
      /// <summary>
      /// Participant Registration page name
      /// </summary>
      public static string PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME = "participant_default.gif";
      /// <summary>
      /// Legacy Image Path StartsWith
      /// </summary>
      public const string LEGACY_IMAGE_PATH_STARTSWITH = "Resources/Images";
      /// <summary>
      /// Legacy Image Path sponsor
      /// </summary>
      public const string LEGACY_IMAGE_PATH_SPONSOR = "sponsor";
      /// <summary>
      /// Legacy Image Path efund classic
      /// </summary>
      public const string LEGACY_IMAGE_PATH_EFUNDCLASSIC = "_efund_/_classic_";
      /// <summary>
      /// Legacy Image Path personalized big_photos
      /// </summary>
      public const string LEGACY_IMAGE_PATH_PERSONALIZED_BIG_PHOTOS = "Personalized/big_photos";
      /// <summary>
      /// Legacy Image Path personalized folders
      /// </summary>
      public const string LEGACY_IMAGE_PATH_PERSONALIZED_FOLDERS = "sports|school|community|general";
      /// <summary>
      /// Legacy Image Path personalized folders
      /// </summary>
      public const string LEGACY_IMAGE_PARTNER_PERSONALIZATION = "Personalized/PartnerPersonalization";
      /// <summary>
      /// Legacy Domain Host
      /// </summary>
      public const string LEGACY_DOMAIN_HOST = "my.fundraising.com";
      /// <summary>
      /// New Domain Host
      /// </summary>
      public const string NEW_DOMAIN_HOST = "efundraising.com";
      /// <summary>
      /// Redirect to store page name
      /// </summary>
      public const string STORE_REDIRECT_PAGE = "RedirectToStore";
      /// <summary>
      /// Participant Registration page name
      /// </summary>
      public const string PARTICIPANT_REGISTER_PAGE = "ParticipantRegister";
      /// <summary>
      /// Default Group Size
      /// </summary>
      public const string DEFAULT_GROUP_SIZE = "15";
      /// <summary>
      /// USA Culture Code
      /// </summary>
      public const string USA_CULTURE_CODE = "en-US";
      /// <summary>
      /// USA Country Code
      /// </summary>
      public const string USA_COUNTRY_CODE = "US";
      /// <summary>
      /// CANADA Culture Code
      /// </summary>
      public const string CANADA_CULTURE_CODE = "en-CA";
      /// <summary>
      /// CANADA Country Code
      /// </summary>
      public const string CANADA_COUNTRY_CODE = "CA";
      /// <summary>
      /// Default USA Subdivision Code
      /// </summary>
      public const string DEFAULT_US_SUBDIVISIONCODE = "US-CA";
      /// <summary>
      /// Default CANADA Subdivision Code
      /// </summary>
      public const string DEFAULT_CA_SUBDIVISIONCODE = "CA-AB";
   }
   // ReSharper restore InconsistentNaming
}