namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("consultant")]
    public partial class consultant
    {
        public consultant()
        {
            AR_Activity = new HashSet<AR_Activity>();
            Associate_Mentor = new HashSet<Associate_Mentor>();
            Associate_Mentor1 = new HashSet<Associate_Mentor>();
            clients = new HashSet<client>();
            Commission_Paid = new HashSet<Commission_Paid>();
            Commission_Rate = new HashSet<Commission_Rate>();
            consultant_address = new HashSet<consultant_address>();
            Default_Consultant_Rate = new HashSet<Default_Consultant_Rate>();
            leads = new HashSet<lead>();
            leads1 = new HashSet<lead>();
            sale_to_add = new HashSet<sale_to_add>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int consultant_id { get; set; }

        public byte division_id { get; set; }

        public int? client_id { get; set; }

        [StringLength(4)]
        public string client_sequence_code { get; set; }

        public int? department_id { get; set; }

        public int? partner_id { get; set; }

        public byte consultant_transfer_status_id { get; set; }

        public short? territory_id { get; set; }

        public int? ext_consultant_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public bool is_agent { get; set; }

        public bool is_active { get; set; }

        [StringLength(50)]
        public string nt_login { get; set; }

        [StringLength(50)]
        public string phone_extension { get; set; }

        [StringLength(50)]
        public string email_address { get; set; }

        [StringLength(15)]
        public string home_phone { get; set; }

        [StringLength(15)]
        public string work_phone { get; set; }

        [StringLength(15)]
        public string fax_number { get; set; }

        [StringLength(15)]
        public string toll_free_phone { get; set; }

        [StringLength(15)]
        public string mobile_phone { get; set; }

        [StringLength(15)]
        public string pager_phone { get; set; }

        [Column(TypeName = "text")]
        public string default_proposal_text { get; set; }

        public bool csr_consultant { get; set; }

        public double? objectives { get; set; }

        public bool is_available { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        public bool kit_paid { get; set; }

        public bool? is_fm { get; set; }

        public DateTime create_date { get; set; }

        public long? wfc_id { get; set; }

        public virtual ICollection<AR_Activity> AR_Activity { get; set; }

        public virtual ICollection<Associate_Mentor> Associate_Mentor { get; set; }

        public virtual ICollection<Associate_Mentor> Associate_Mentor1 { get; set; }

        public virtual ICollection<client> clients { get; set; }

        public virtual ICollection<Commission_Paid> Commission_Paid { get; set; }

        public virtual ICollection<Commission_Rate> Commission_Rate { get; set; }

        public virtual ICollection<consultant_address> consultant_address { get; set; }

        public virtual consultant_transfer_status consultant_transfer_status { get; set; }

        public virtual Department Department { get; set; }

        public virtual division division { get; set; }

        public virtual territory territory { get; set; }

        public virtual ICollection<Default_Consultant_Rate> Default_Consultant_Rate { get; set; }

        public virtual ICollection<lead> leads { get; set; }

        public virtual ICollection<lead> leads1 { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add { get; set; }
    }
}
