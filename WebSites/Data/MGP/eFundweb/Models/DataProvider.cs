namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataProvider : DbContext
    {
        public DataProvider()
            : base("name=eFundweb")
        {
        }

        public virtual DbSet<C_tbd_partner> C_tbd_partner { get; set; }
        public virtual DbSet<C_tbd_promotion> C_tbd_promotion { get; set; }
        public virtual DbSet<C_tbd_promotion_type> C_tbd_promotion_type { get; set; }
        public virtual DbSet<Accounting_Class> Accounting_Class { get; set; }
        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<Advertiser> Advertisers { get; set; }
        public virtual DbSet<Advertiser_Partner> Advertiser_Partner { get; set; }
        public virtual DbSet<Advertising_Support> Advertising_Support { get; set; }
        public virtual DbSet<Advertising_Support_Type> Advertising_Support_Type { get; set; }
        public virtual DbSet<Advertisment_Type> Advertisment_Type { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Agent_Address> Agent_Address { get; set; }
        public virtual DbSet<Agent_Comment> Agent_Comment { get; set; }
        public virtual DbSet<Agent_Domain> Agent_Domain { get; set; }
        public virtual DbSet<Agent_Email> Agent_Email { get; set; }
        public virtual DbSet<Agent_Link> Agent_Link { get; set; }
        public virtual DbSet<Agent_Reach_Number> Agent_Reach_Number { get; set; }
        public virtual DbSet<Agent_Reach_Type> Agent_Reach_Type { get; set; }
        public virtual DbSet<Best_Time_Call> Best_Time_Call { get; set; }
        public virtual DbSet<Best_Time_Call_Desc> Best_Time_Call_Desc { get; set; }
        public virtual DbSet<Brochures_Images> Brochures_Images { get; set; }
        public virtual DbSet<Campaign_Reason> Campaign_Reason { get; set; }
        public virtual DbSet<Campaign_Reason_Desc> Campaign_Reason_Desc { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Category_Desc> Category_Desc { get; set; }
        public virtual DbSet<Category_Package> Category_Package { get; set; }
        public virtual DbSet<Category_Partner> Category_Partner { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Client_Address> Client_Address { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer_Questions> Customer_Questions { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<Entry_Form> Entry_Form { get; set; }
        public virtual DbSet<Form_Type> Form_Type { get; set; }
        public virtual DbSet<Form_Type_Desc> Form_Type_Desc { get; set; }
        public virtual DbSet<Grabber> Grabbers { get; set; }
        public virtual DbSet<Group_Type> Group_Type { get; set; }
        public virtual DbSet<Group_Type_Desc> Group_Type_Desc { get; set; }
        public virtual DbSet<Hear_About_Us> Hear_About_Us { get; set; }
        public virtual DbSet<Hear_About_Us_Desc> Hear_About_Us_Desc { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Input_Select> Input_Select { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<launch> launches { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<Organization_Type> Organization_Type { get; set; }
        public virtual DbSet<Organization_Type_Desc> Organization_Type_Desc { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Package_Desc> Package_Desc { get; set; }
        public virtual DbSet<package_product> package_product { get; set; }
        public virtual DbSet<Partners_Forms> Partners_Forms { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_Class> Product_Class { get; set; }
        public virtual DbSet<Product_Desc> Product_Desc { get; set; }
        public virtual DbSet<Product_Interested_In> Product_Interested_In { get; set; }
        public virtual DbSet<Product_Interested_In_Desc> Product_Interested_In_Desc { get; set; }
        public virtual DbSet<Product_Interested_Partner> Product_Interested_Partner { get; set; }
        public virtual DbSet<promotion_default> promotion_default { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Questions_Desc> Questions_Desc { get; set; }
        public virtual DbSet<Questions_Entry_Form> Questions_Entry_Form { get; set; }
        public virtual DbSet<Replication_Monitoring> Replication_Monitoring { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Sales_Item> Sales_Item { get; set; }
        public virtual DbSet<Salutation> Salutations { get; set; }
        public virtual DbSet<Salutation_Desc> Salutation_Desc { get; set; }
        public virtual DbSet<Scratch_Book> Scratch_Book { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Sweepstakes_Registration> Sweepstakes_Registration { get; set; }
        public virtual DbSet<Sweepstakes_Registration_AT> Sweepstakes_Registration_AT { get; set; }
        public virtual DbSet<Sweepstakes_Registration_USAWrestling> Sweepstakes_Registration_USAWrestling { get; set; }
        public virtual DbSet<Targeted_Market> Targeted_Market { get; set; }
        public virtual DbSet<Targeted_Market_Type> Targeted_Market_Type { get; set; }
        public virtual DbSet<TBD_Sweepstakes_Repli> TBD_Sweepstakes_Repli { get; set; }
        public virtual DbSet<Temp_Lead> Temp_Lead { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<Title_Desc> Title_Desc { get; set; }
        public virtual DbSet<Unsubscribe> Unsubscribes { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<Web_Site> Web_Site { get; set; }
        public virtual DbSet<Web_Visit> Web_Visit { get; set; }
        public virtual DbSet<leadgen_lead> leadgen_lead { get; set; }
        public virtual DbSet<partners_site_list> partners_site_list { get; set; }
        public virtual DbSet<Sweepstakes_Registration_NSG> Sweepstakes_Registration_NSG { get; set; }
        public virtual DbSet<tbd_partner> tbd_partner { get; set; }
        public virtual DbSet<tell_a_friend> tell_a_friend { get; set; }
        public virtual DbSet<Unassigned_Consultant> Unassigned_Consultant { get; set; }
        public virtual DbSet<unsubscribe_puffy> unsubscribe_puffy { get; set; }
        public virtual DbSet<partner> partners { get; set; }
        public virtual DbSet<promotion> promotions { get; set; }
        public virtual DbSet<Promotion_Type> Promotion_Type { get; set; }

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
                .Property(e => e.partner_folder)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .Property(e => e.partner_password)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_partner>()
                .HasMany(e => e.C_tbd_promotion)
                .WithRequired(e => e.C_tbd_partner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.Promotion_Type_Code)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.Visibility)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.Contact_Name)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.Tracking_Serial)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.Cookie_Content)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.Keyword)
                .IsUnicode(false);

            modelBuilder.Entity<C_tbd_promotion>()
                .Property(e => e.Script_Name)
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

            modelBuilder.Entity<Accounting_Class>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Advertisement>()
                .Property(e => e.Description)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Advertisement>()
                .Property(e => e.Size)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Advertisement>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Advertiser>()
                .Property(e => e.Advertiser_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Advertiser_Partner>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support>()
                .Property(e => e.Title)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support>()
                .Property(e => e.Web_Site)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support>()
                .Property(e => e.Ordering_Phone_Number)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support>()
                .Property(e => e.Magazine_Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Advertising_Support>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support_Type>()
                .Property(e => e.Description)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Advertising_Support_Type>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Advertisment_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.Agent_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.Logo)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.Add_Timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Agent>()
                .Property(e => e.Add_By_User)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Address>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Address>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Address>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Address>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Address>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Comment>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Comment>()
                .Property(e => e.Added_By)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Domain>()
                .Property(e => e.Domain_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Email>()
                .Property(e => e.Email_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Link>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Link>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Reach_Number>()
                .Property(e => e.Reach_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Agent_Reach_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Best_Time_Call>()
                .Property(e => e.Best_Time_Call_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Best_Time_Call>()
                .Property(e => e.Best_Time_Call_Desc_Fr)
                .IsUnicode(false);

            modelBuilder.Entity<Best_Time_Call_Desc>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Brochures_Images>()
                .Property(e => e.Base_Filename)
                .IsUnicode(false);

            modelBuilder.Entity<Brochures_Images>()
                .Property(e => e.File_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Campaign_Reason>()
                .Property(e => e.Campaign_Reason_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Campaign_Reason>()
                .Property(e => e.Campaign_Reason_Desc_Fr)
                .IsUnicode(false);

            modelBuilder.Entity<Campaign_Reason_Desc>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Category_Key)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Scratchcard_Image)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Category_Desc)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Category_Package)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category_Desc>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Category_Desc>()
                .Property(e => e.Category_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Client_Sequence_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Organization_Class_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Channel_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Salutation)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Last_name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Organization)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Day_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Day_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Evening_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Evening_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Extra_Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Day_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Evening_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Other_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Other_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Client_Address>()
                .Property(e => e.Client_Sequence_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Client_Address>()
                .Property(e => e.Address_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Client_Address>()
                .Property(e => e.Street_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Client_Address>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Client_Address>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Client_Address>()
                .Property(e => e.Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Client_Address>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Client_Address>()
                .Property(e => e.Attention_of)
                .IsUnicode(false);

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

            modelBuilder.Entity<Country>()
                .Property(e => e.Country_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Currency_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.C_tbd_partner)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer_Questions>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Questions>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Questions>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Questions>()
                .Property(e => e.Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Questions>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Questions>()
                .Property(e => e.Question)
                .IsUnicode(false);

            modelBuilder.Entity<Destination>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.property)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<Entry_Form>()
                .Property(e => e.Entry_Form_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Entry_Form>()
                .HasMany(e => e.Partners_Forms)
                .WithRequired(e => e.Entry_Form)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Entry_Form>()
                .HasMany(e => e.Questions_Entry_Form)
                .WithRequired(e => e.Entry_Form)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Form_Type>()
                .Property(e => e.Form_Type_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Form_Type_Desc>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Grabber>()
                .Property(e => e.Grabber_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Group_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Group_Type>()
                .Property(e => e.Description_Fr)
                .IsUnicode(false);

            modelBuilder.Entity<Group_Type_Desc>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Hear_About_Us>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Hear_About_Us>()
                .Property(e => e.Name_Fr)
                .IsUnicode(false);

            modelBuilder.Entity<Hear_About_Us_Desc>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Image>()
                .Property(e => e.Images_Path)
                .IsUnicode(false);

            modelBuilder.Entity<Input_Select>()
                .Property(e => e.Table_Source)
                .IsUnicode(false);

            modelBuilder.Entity<Input_Select>()
                .Property(e => e.Column_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Input_Select>()
                .Property(e => e.Column_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Input_Select>()
                .Property(e => e.Clauses)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .Property(e => e.Language1)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .Property(e => e.Language_Path)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Category_Desc)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Package_Desc)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Product_Desc)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Questions_Desc)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Templates)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Title_Desc)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<launch>()
                .Property(e => e.redirect_to)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Channel_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Salutation)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Organization)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Street_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Day_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Day_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Evening_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Evening_Time_Call)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Interests)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Day_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Evening_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Other_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Group_Web_Site)
                .IsUnicode(false);

            modelBuilder.Entity<Lead>()
                .Property(e => e.Cookie_Content)
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

            modelBuilder.Entity<Organization_Type>()
                .Property(e => e.Organization_Type_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Organization_Type>()
                .Property(e => e.Organization_Type_Desc_Fr)
                .IsUnicode(false);

            modelBuilder.Entity<Organization_Type_Desc>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.Image_Path)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .HasMany(e => e.Category_Package)
                .WithRequired(e => e.Package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Package>()
                .HasMany(e => e.Package_Desc)
                .WithRequired(e => e.Package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Package>()
                .HasMany(e => e.package_product)
                .WithRequired(e => e.Package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Package_Desc>()
                .Property(e => e.Package_Desc1)
                .IsUnicode(false);

            modelBuilder.Entity<Package_Desc>()
                .Property(e => e.Package_Title)
                .IsUnicode(false);

            modelBuilder.Entity<Partners_Forms>()
                .Property(e => e.Recipients)
                .IsUnicode(false);

            modelBuilder.Entity<Price>()
                .Property(e => e.Price1)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Product_Short_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image_Path)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.package_product)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Prices)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_Desc)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_Class>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Class>()
                .Property(e => e.Product_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Class>()
                .Property(e => e.Display_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Class>()
                .Property(e => e.Product_Class_Web_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Class>()
                .Property(e => e.Product_Class_Web_Profit)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Class>()
                .Property(e => e.Product_Class_Image)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Class>()
                .Property(e => e.Product_Class_Title_Image)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Desc>()
                .Property(e => e.Long_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Desc>()
                .Property(e => e.Product_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Interested_In>()
                .Property(e => e.Product_Interested_In_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Interested_In>()
                .HasMany(e => e.Product_Interested_In_Desc1)
                .WithRequired(e => e.Product_Interested_In)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_Interested_In>()
                .HasMany(e => e.Product_Interested_Partner)
                .WithRequired(e => e.Product_Interested_In)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_Interested_In_Desc>()
                .Property(e => e.Product_Interested_In_Description)
                .IsUnicode(false);

            modelBuilder.Entity<promotion_default>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Questions_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Questions_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Questions_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Validation_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Questions_Desc)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Questions_Entry_Form)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questions_Desc>()
                .Property(e => e.Questions_Display)
                .IsUnicode(false);

            modelBuilder.Entity<Questions_Desc>()
                .Property(e => e.Error_Message)
                .IsUnicode(false);

            modelBuilder.Entity<Questions_Entry_Form>()
                .Property(e => e.Insert_Table)
                .IsUnicode(false);

            modelBuilder.Entity<Questions_Entry_Form>()
                .Property(e => e.Insert_Column)
                .IsUnicode(false);

            modelBuilder.Entity<Questions_Entry_Form>()
                .Property(e => e.Default_Value)
                .IsUnicode(false);

            modelBuilder.Entity<Replication_Monitoring>()
                .Property(e => e.Msg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.Client_Sequence_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.PO_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.Shipping_Fees)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sale>()
                .Property(e => e.Shipping_Fees_Discount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sale>()
                .Property(e => e.Waybill_No)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.Total_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sale>()
                .Property(e => e.UpFront_Payment_Required)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Sale>()
                .Property(e => e.Accounting_Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.SSN_Number)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.SSN_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.SSN_City)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.SSN_State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.SSN_Country_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.SSN_Zip_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.Group_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.Unit_Price_Sold)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.Suggested_Coupons)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.Sales_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.Paid_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.Adjusted_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.Discount_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.Sales_Commission_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.Sponsor_Commission_Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.Nb_units_sold)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Sales_Item>()
                .Property(e => e.manual_product_description)
                .IsUnicode(false);

            modelBuilder.Entity<Salutation>()
                .Property(e => e.Salutation_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Salutation>()
                .Property(e => e.Salutation_Fr)
                .IsUnicode(false);

            modelBuilder.Entity<Salutation_Desc>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Scratch_Book>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Scratch_Book>()
                .Property(e => e.Raising_Potential)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Scratch_Book>()
                .Property(e => e.Product_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Scratch_Book>()
                .Property(e => e.Current_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Scratch_Book>()
                .Property(e => e.Small_Image)
                .IsUnicode(false);

            modelBuilder.Entity<Scratch_Book>()
                .Property(e => e.Front_Image)
                .IsUnicode(false);

            modelBuilder.Entity<Scratch_Book>()
                .Property(e => e.Back_Image)
                .IsUnicode(false);

            modelBuilder.Entity<Scratch_Book>()
                .Property(e => e.Scratch_Booh_Web_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .Property(e => e.State_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration>()
                .Property(e => e.Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_AT>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_AT>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_AT>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_AT>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_AT>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_AT>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_AT>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_AT>()
                .Property(e => e.Phone_Ext)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_AT>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_USAWrestling>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_USAWrestling>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_USAWrestling>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_USAWrestling>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_USAWrestling>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_USAWrestling>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_USAWrestling>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_USAWrestling>()
                .Property(e => e.Phone_Ext)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_USAWrestling>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Targeted_Market>()
                .Property(e => e.Age_Range)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Targeted_Market>()
                .Property(e => e.Education_Level)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Targeted_Market>()
                .Property(e => e.Description)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Targeted_Market>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Targeted_Market_Type>()
                .Property(e => e.Description)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Targeted_Market_Type>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<TBD_Sweepstakes_Repli>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<TBD_Sweepstakes_Repli>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<TBD_Sweepstakes_Repli>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<TBD_Sweepstakes_Repli>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<TBD_Sweepstakes_Repli>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<TBD_Sweepstakes_Repli>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<TBD_Sweepstakes_Repli>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<TBD_Sweepstakes_Repli>()
                .Property(e => e.Phone_Ext)
                .IsUnicode(false);

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
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Comments)
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
                .Property(e => e.Cookie_Content)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Group_Web_Site)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.Other_Phone_Ext)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.create_login)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.create_appname)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Lead>()
                .Property(e => e.create_hostname)
                .IsUnicode(false);

            modelBuilder.Entity<Template>()
                .Property(e => e.Template_Path)
                .IsUnicode(false);

            modelBuilder.Entity<Template>()
                .HasMany(e => e.Images)
                .WithRequired(e => e.Template)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Testimonial>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Testimonial>()
                .Property(e => e.Organism)
                .IsUnicode(false);

            modelBuilder.Entity<Testimonial>()
                .Property(e => e.Responsible)
                .IsUnicode(false);

            modelBuilder.Entity<Title>()
                .Property(e => e.Title_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Title>()
                .Property(e => e.Title_Desc_Fr)
                .IsUnicode(false);

            modelBuilder.Entity<Title>()
                .HasMany(e => e.Title_Desc1)
                .WithRequired(e => e.Title)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Title_Desc>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Unsubscribe>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.Session_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.Ads_Script)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.Referer)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.Cookie_Content)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.Host)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.Query_String)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.Script_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.GTSE)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.Keyword)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.PID)
                .IsUnicode(false);

            modelBuilder.Entity<Visit>()
                .Property(e => e.Elapsed_Completion_Time)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Visit>()
                .Property(e => e.Elapsed_Visit_Time)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Visit>()
                .Property(e => e.IP_Address)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Web_Site>()
                .Property(e => e.Web_Site_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Web_Visit>()
                .Property(e => e.Referrer)
                .IsUnicode(false);

            modelBuilder.Entity<Web_Visit>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<Web_Visit>()
                .Property(e => e.Query_String)
                .IsUnicode(false);

            modelBuilder.Entity<Web_Visit>()
                .Property(e => e.Host)
                .IsUnicode(false);

            modelBuilder.Entity<partners_site_list>()
                .Property(e => e.partner_name)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_NSG>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_NSG>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_NSG>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_NSG>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_NSG>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_NSG>()
                .Property(e => e.State_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_NSG>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_NSG>()
                .Property(e => e.Phone_Ext)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sweepstakes_Registration_NSG>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbd_partner>()
                .Property(e => e.Partner_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tell_a_friend>()
                .Property(e => e.culture_code)
                .IsUnicode(false);

            modelBuilder.Entity<tell_a_friend>()
                .Property(e => e.from_name)
                .IsUnicode(false);

            modelBuilder.Entity<tell_a_friend>()
                .Property(e => e.from_email)
                .IsUnicode(false);

            modelBuilder.Entity<tell_a_friend>()
                .Property(e => e.to_name)
                .IsUnicode(false);

            modelBuilder.Entity<tell_a_friend>()
                .Property(e => e.to_email)
                .IsUnicode(false);

            modelBuilder.Entity<tell_a_friend>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<tell_a_friend>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.country_id)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.partner_name)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.partner_path)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.esubs_url)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.estore_url)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.free_kit_url)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.logo)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.phone_number)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.email_ext)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<partner>()
                .Property(e => e.partner_folder)
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.promotion_type_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.visibility)
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.contact_name)
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.cookie_content)
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.keyword)
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.script_name)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_Type>()
                .Property(e => e.Promotion_Type_Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Promotion_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
