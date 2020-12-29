namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataProvider : DbContext
    {
        public DataProvider()
            : base("name=EFRECommerce")
        {
        }

        public virtual DbSet<catalog> catalogs { get; set; }
        public virtual DbSet<country> countries { get; set; }
        public virtual DbSet<credit_card> credit_card { get; set; }
        public virtual DbSet<credit_card_authorization> credit_card_authorization { get; set; }
        public virtual DbSet<credit_card_type> credit_card_type { get; set; }
        public virtual DbSet<email> emails { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<order_comment> order_comment { get; set; }
        public virtual DbSet<order_detail> order_detail { get; set; }
        public virtual DbSet<phone_number> phone_number { get; set; }
        public virtual DbSet<postal_address> postal_address { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<receipt> receipts { get; set; }
        public virtual DbSet<recurrence_order_detail> recurrence_order_detail { get; set; }
        public virtual DbSet<recurrence_plan> recurrence_plan { get; set; }
        public virtual DbSet<shipment_group> shipment_group { get; set; }
        public virtual DbSet<source> sources { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<subdivision> subdivisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<catalog>()
                .Property(e => e.catalog_name)
                .IsUnicode(false);

            modelBuilder.Entity<catalog>()
                .Property(e => e.culture_code)
                .IsUnicode(false);

            modelBuilder.Entity<catalog>()
                .HasMany(e => e.products)
                .WithRequired(e => e.catalog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<country>()
                .HasMany(e => e.subdivisions)
                .WithRequired(e => e.country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<credit_card>()
                .Property(e => e.credit_card_number)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card>()
                .Property(e => e.credit_card_name)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card>()
                .Property(e => e.last_cc_digits)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card>()
                .Property(e => e.GUID)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_authorization>()
                .Property(e => e.transaction_type)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_authorization>()
                .Property(e => e.amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<credit_card_authorization>()
                .Property(e => e.response_code)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_authorization>()
                .Property(e => e.response_auth_code)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_authorization>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_authorization>()
                .Property(e => e.response_message)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_type>()
                .Property(e => e.credit_card_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<credit_card_type>()
                .HasMany(e => e.credit_card)
                .WithRequired(e => e.credit_card_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<email>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<email>()
                .Property(e => e.recipient_name)
                .IsUnicode(false);

            modelBuilder.Entity<email>()
                .HasMany(e => e.orders)
                .WithOptional(e => e.email)
                .HasForeignKey(e => e.billing_email_id);

            modelBuilder.Entity<email>()
                .HasMany(e => e.shipment_group)
                .WithOptional(e => e.email)
                .HasForeignKey(e => e.shipping_email_id);

            modelBuilder.Entity<order>()
                .Property(e => e.adjustment_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<order>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.ext_order_tracking)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.GUID)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .HasMany(e => e.order_detail)
                .WithRequired(e => e.order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<order>()
                .HasMany(e => e.receipts)
                .WithRequired(e => e.order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<order_comment>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<order_comment>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<order_comment>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<order_detail>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<order_detail>()
                .HasMany(e => e.recurrence_order_detail)
                .WithRequired(e => e.order_detail)
                .HasForeignKey(e => e.order_detail_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<order_detail>()
                .HasMany(e => e.recurrence_order_detail1)
                .WithRequired(e => e.order_detail1)
                .HasForeignKey(e => e.recurrence_order_detail_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<phone_number>()
                .Property(e => e.phone_number1)
                .IsUnicode(false);

            modelBuilder.Entity<phone_number>()
                .HasMany(e => e.orders)
                .WithOptional(e => e.phone_number)
                .HasForeignKey(e => e.billing_phone_number_id);

            modelBuilder.Entity<phone_number>()
                .HasMany(e => e.shipment_group)
                .WithOptional(e => e.phone_number)
                .HasForeignKey(e => e.shipping_phone_number_id);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.address1)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.address2)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.zip)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .Property(e => e.zip4)
                .IsUnicode(false);

            modelBuilder.Entity<postal_address>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.postal_address)
                .HasForeignKey(e => e.billing_postal_address_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<postal_address>()
                .HasMany(e => e.shipment_group)
                .WithOptional(e => e.postal_address)
                .HasForeignKey(e => e.shipping_postal_address_id);

            modelBuilder.Entity<product>()
                .Property(e => e.product_name)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.product_code)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.remit_code)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.culture_code)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.order_detail)
                .WithRequired(e => e.product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<receipt>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<recurrence_plan>()
                .Property(e => e.recurrence_plan_name)
                .IsUnicode(false);

            modelBuilder.Entity<recurrence_plan>()
                .Property(e => e.frequency)
                .IsUnicode(false);

            modelBuilder.Entity<recurrence_plan>()
                .HasMany(e => e.order_detail)
                .WithOptional(e => e.recurrence_plan)
                .HasForeignKey(e => e.reoccurance_plan_id);

            modelBuilder.Entity<shipment_group>()
                .Property(e => e.shipping_charges)
                .HasPrecision(19, 4);

            modelBuilder.Entity<shipment_group>()
                .Property(e => e.fulf_shipment_tracking)
                .IsUnicode(false);

            modelBuilder.Entity<source>()
                .Property(e => e.source_name)
                .IsUnicode(false);

            modelBuilder.Entity<source>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.source)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<status>()
                .Property(e => e.status_name)
                .IsUnicode(false);

            modelBuilder.Entity<status>()
                .Property(e => e.short_description)
                .IsUnicode(false);

            modelBuilder.Entity<status>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<status>()
                .HasMany(e => e.order_detail)
                .WithOptional(e => e.status)
                .HasForeignKey(e => e.reoccurance_status_id);

            modelBuilder.Entity<status>()
                .HasMany(e => e.order_detail1)
                .WithRequired(e => e.status1)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);
        }
    }
}
