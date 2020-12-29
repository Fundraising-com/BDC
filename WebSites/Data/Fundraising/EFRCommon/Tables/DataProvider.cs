namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataProvider : DbContext
    {
        public DataProvider()
            : base("name=EFRCommon")
        {
        }

        public virtual DbSet<advertising> advertisings { get; set; }
        public virtual DbSet<advertising_listing> advertising_listing { get; set; }
        public virtual DbSet<advertising_type> advertising_type { get; set; }
        public virtual DbSet<listing> listings { get; set; }
        public virtual DbSet<package> packages { get; set; }
        public virtual DbSet<package_desc> package_desc { get; set; }
        public virtual DbSet<partner> partners { get; set; }
        public virtual DbSet<partner_attribute> partner_attribute { get; set; }
        public virtual DbSet<partner_attribute_value> partner_attribute_value { get; set; }
        public virtual DbSet<partner_profit> partner_profit { get; set; }
        public virtual DbSet<partner_promotion> partner_promotion { get; set; }
        public virtual DbSet<partner_type> partner_type { get; set; }
        public virtual DbSet<partner_type_culture> partner_type_culture { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<product_desc> product_desc { get; set; }
        public virtual DbSet<product_package> product_package { get; set; }
        public virtual DbSet<profit> profits { get; set; }
        public virtual DbSet<profit_group> profit_group { get; set; }
        public virtual DbSet<profit_range> profit_range { get; set; }
        public virtual DbSet<promotion> promotions { get; set; }
        public virtual DbSet<promotion_destination> promotion_destination { get; set; }
        public virtual DbSet<promotion_type> promotion_type { get; set; }
        public virtual DbSet<subdivision> subdivisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<advertising>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<advertising>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<advertising>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<advertising>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<advertising>()
                .Property(e => e.compagnie_name)
                .IsUnicode(false);

            modelBuilder.Entity<advertising>()
                .Property(e => e.compagnie_url)
                .IsUnicode(false);

            modelBuilder.Entity<advertising>()
                .Property(e => e.display_url)
                .IsUnicode(false);

            modelBuilder.Entity<advertising>()
                .Property(e => e.listing_text)
                .IsUnicode(false);

            modelBuilder.Entity<advertising>()
                .Property(e => e.image_type)
                .IsUnicode(false);

            modelBuilder.Entity<advertising>()
                .Property(e => e.is_visible)
                .IsUnicode(false);

            modelBuilder.Entity<advertising_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<listing>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<package>()
                .Property(e => e.name)
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

            modelBuilder.Entity<partner>()
                .Property(e => e.partner_name)
                .IsUnicode(false);

            modelBuilder.Entity<partner_attribute>()
                .Property(e => e.partner_attribute_name)
                .IsUnicode(false);

            modelBuilder.Entity<partner_attribute_value>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<partner_type>()
                .Property(e => e.partner_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<partner_type_culture>()
                .Property(e => e.partner_type_name)
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

            modelBuilder.Entity<profit>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<profit>()
                .Property(e => e.disclaimer)
                .IsUnicode(false);

            modelBuilder.Entity<profit>()
                .Property(e => e.alt_disclaimer)
                .IsUnicode(false);

            modelBuilder.Entity<profit_group>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<profit_group>()
                .Property(e => e.disclaimer)
                .IsUnicode(false);

            modelBuilder.Entity<profit_group>()
                .Property(e => e.alt_disclaimer)
                .IsUnicode(false);

            modelBuilder.Entity<profit_range>()
                .Property(e => e._operator)
                .IsUnicode(false);

            modelBuilder.Entity<profit_range>()
                .Property(e => e.disclaimer)
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.promotion_type_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.promotion_name)
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.script_name)
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.cookie_content)
                .IsUnicode(false);

            modelBuilder.Entity<promotion>()
                .Property(e => e.keyword)
                .IsUnicode(false);

            modelBuilder.Entity<promotion_destination>()
                .Property(e => e.promotion_destination_url)
                .IsUnicode(false);

            modelBuilder.Entity<promotion_type>()
                .Property(e => e.promotion_type_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<promotion_type>()
                .Property(e => e.promotion_type_name)
                .IsUnicode(false);
        }
    }
}
