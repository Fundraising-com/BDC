namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Temp_Lead
    {
        public Temp_Lead()
        {
            Web_Visit = new HashSet<Web_Visit>();
        }

        public int? Division_ID { get; set; }

        public int? Promotion_ID { get; set; }

        [Key]
        public int Temp_Lead_ID { get; set; }

        [StringLength(4)]
        public string Channel_Code { get; set; }

        public int? Lead_Status_ID { get; set; }

        public int? Consultant_ID { get; set; }

        public DateTime? Lead_Entry_Date { get; set; }

        [StringLength(10)]
        public string Salutation { get; set; }

        [StringLength(50)]
        public string First_Name { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }

        [StringLength(100)]
        public string Organization { get; set; }

        [StringLength(100)]
        public string Street_Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(10)]
        public string State_Code { get; set; }

        [StringLength(10)]
        public string Country_Code { get; set; }

        [StringLength(10)]
        public string Zip_Code { get; set; }

        [StringLength(20)]
        public string Day_Phone { get; set; }

        [StringLength(20)]
        public string Day_Time_Call { get; set; }

        [StringLength(20)]
        public string Evening_Phone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? Group_Type_ID { get; set; }

        public int? Participant_Count { get; set; }

        public int? Fund_Raising_Goal { get; set; }

        public DateTime? Decision_Date { get; set; }

        public bool Decision_Maker { get; set; }

        public DateTime? Fund_Raiser_Start_Date { get; set; }

        public bool OnEmailList { get; set; }

        [StringLength(2000)]
        public string Comments { get; set; }

        public int? Hear_ID { get; set; }

        public bool kit_to_send { get; set; }

        public bool kit_sent { get; set; }

        public DateTime? kit_sent_date { get; set; }

        [StringLength(10)]
        public string Day_Phone_Ext { get; set; }

        [StringLength(10)]
        public string Evening_Phone_Ext { get; set; }

        [StringLength(2000)]
        public string Rejection_reason { get; set; }

        [StringLength(20)]
        public string Other_Phone { get; set; }

        [StringLength(255)]
        public string Cookie_Content { get; set; }

        [StringLength(50)]
        public string Group_Web_Site { get; set; }

        public int? Organization_Type_ID { get; set; }

        public int? Title_ID { get; set; }

        public int? Campaign_Reason_ID { get; set; }

        public int? Web_Site_ID { get; set; }

        [StringLength(10)]
        public string Other_Phone_Ext { get; set; }

        public bool IsNew { get; set; }

        public bool? Opt_In_Sweepstakes { get; set; }

        public int? Group_ID { get; set; }

        [StringLength(128)]
        public string create_login { get; set; }

        [StringLength(128)]
        public string create_appname { get; set; }

        [StringLength(128)]
        public string create_hostname { get; set; }

        public DateTime? create_date { get; set; }

        public virtual ICollection<Web_Visit> Web_Visit { get; set; }
    }
}
