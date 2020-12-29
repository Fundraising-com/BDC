namespace GA.BDC.Data.Fundraising.FastFundraising.Tables
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataProvider : DbContext
    {
        public DataProvider()
            : base("name=FastFundraising")
        {
        }

        public virtual DbSet<fundraiser_category> fundraising_categories { get; set; }
        public virtual DbSet<fundraiser_product> fundraising_products { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<FC> FCs { get; set; }
        public virtual DbSet<fc_testimonial> fc_testimonial { get; set; }
        public virtual DbSet<ffitemprice> ffitemprices { get; set; }
        public virtual DbSet<ffitem> ffitems { get; set; }
        public virtual DbSet<shipping_fee> shipping_fee { get; set; }
        public virtual DbSet<shipping_group> shipping_group { get; set; }
        public virtual DbSet<fffeedback> fffeedbacks { get; set; }
        public virtual DbSet<ffnewslettersignup> ffnewslettersignups { get; set; }
        public virtual DbSet<ffquestion> ffquestions { get; set; }
        public virtual DbSet<fftechsupport> fftechsupports { get; set; }
        public virtual DbSet<saleitem> saleitems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<category>()
                .Property(e => e.categoryname)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .Property(e => e.displayitemimagepath)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.property)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<FC>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<FC>()
                .Property(e => e.email_address)
                .IsUnicode(false);

            modelBuilder.Entity<FC>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<FC>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<FC>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<FC>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<FC>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<FC>()
                .Property(e => e.profit_raised)
                .HasPrecision(12, 2);

            modelBuilder.Entity<FC>()
                .HasMany(e => e.fc_testimonial)
                .WithOptional(e => e.FC)
                .HasForeignKey(e => e.fc_id);

            modelBuilder.Entity<fc_testimonial>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<fc_testimonial>()
                .Property(e => e.commentor)
                .IsUnicode(false);

            modelBuilder.Entity<fc_testimonial>()
                .Property(e => e.account)
                .IsUnicode(false);

            modelBuilder.Entity<ffitem>()
                .Property(e => e.itemname)
                .IsUnicode(false);

            modelBuilder.Entity<ffitem>()
                .Property(e => e.itemnmbr)
                .IsUnicode(false);

            modelBuilder.Entity<ffitem>()
                .Property(e => e.imagepath)
                .IsUnicode(false);

            modelBuilder.Entity<ffitem>()
                .Property(e => e.descriptionpath)
                .IsUnicode(false);

            modelBuilder.Entity<ffitem>()
                .Property(e => e.itemuom)
                .IsUnicode(false);

            modelBuilder.Entity<ffitem>()
                .Property(e => e.peruom)
                .IsUnicode(false);

            modelBuilder.Entity<ffitem>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<ffitem>()
                .Property(e => e.flavors)
                .IsUnicode(false);

            modelBuilder.Entity<ffitem>()
                .Property(e => e.packaging)
                .IsUnicode(false);

            modelBuilder.Entity<shipping_fee>()
                .Property(e => e.shipping_fee1)
                .HasPrecision(19, 4);

            modelBuilder.Entity<shipping_fee>()
                .HasMany(e => e.shipping_group)
                .WithRequired(e => e.shipping_fee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<fffeedback>()
                .Property(e => e.fbcustname)
                .IsUnicode(false);

            modelBuilder.Entity<fffeedback>()
                .Property(e => e.fbemail)
                .IsUnicode(false);

            modelBuilder.Entity<fffeedback>()
                .Property(e => e.fbmessage)
                .IsUnicode(false);

            modelBuilder.Entity<ffnewslettersignup>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<ffquestion>()
                .Property(e => e.fname)
                .IsUnicode(false);

            modelBuilder.Entity<ffquestion>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<ffquestion>()
                .Property(e => e.question)
                .IsUnicode(false);

            modelBuilder.Entity<fftechsupport>()
                .Property(e => e.tcustname)
                .IsUnicode(false);

            modelBuilder.Entity<fftechsupport>()
                .Property(e => e.temail)
                .IsUnicode(false);

            modelBuilder.Entity<fftechsupport>()
                .Property(e => e.tmessage)
                .IsUnicode(false);

            modelBuilder.Entity<saleitem>()
                .Property(e => e.imagepath)
                .IsUnicode(false);

            modelBuilder.Entity<saleitem>()
                .Property(e => e.descriptionpath)
                .IsUnicode(false);
        }
    }
}
