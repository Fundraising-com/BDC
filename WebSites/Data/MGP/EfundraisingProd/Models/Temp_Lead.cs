namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Temp_Lead
    {
        public int? Division_ID { get; set; }

        public int? Promotion_ID { get; set; }

        [Key]
        public int Temp_Lead_Id { get; set; }

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

        [StringLength(50)]
        public string Title { get; set; }

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
        public string Evening_Time_Call { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? Group_Type_ID { get; set; }

        public int? Member_Count { get; set; }

        public int? Participant_Count { get; set; }

        public int? Fund_Raising_Goal { get; set; }

        public DateTime? Decision_Date { get; set; }

        public bool Decision_Maker { get; set; }

        public bool Committee_Meeting_Required { get; set; }

        public DateTime? Committee_Meeting_Date { get; set; }

        public DateTime? Fund_Raiser_Start_Date { get; set; }

        public bool OnEmailList { get; set; }

        public bool FaxKit { get; set; }

        public bool EmailKit { get; set; }

        [Column(TypeName = "text")]
        public string Comments { get; set; }

        public int? Hear_Id { get; set; }

        public bool kit_to_send { get; set; }

        public bool kit_sent { get; set; }

        public DateTime? kit_sent_date { get; set; }

        public int? Old_Lead_Id { get; set; }

        public DateTime? Lead_Assignment_Date { get; set; }

        [Column(TypeName = "text")]
        public string Interests { get; set; }

        public bool Has_Been_Contacted { get; set; }

        public int? fk_Kit_Type_ID { get; set; }

        public int? Lead_Priority_Id { get; set; }

        [StringLength(10)]
        public string Day_Phone_Ext { get; set; }

        [StringLength(10)]
        public string Evening_Phone_Ext { get; set; }

        [Column(TypeName = "text")]
        public string Rejection_reason { get; set; }

        [StringLength(20)]
        public string Other_Phone { get; set; }

        [StringLength(10)]
        public string Other_Phone_Ext { get; set; }

        [StringLength(50)]
        public string Group_Web_Site { get; set; }

        public int? Organization_Type_Id { get; set; }

        public int? Campaign_Reason_Id { get; set; }

        public int? Title_Id { get; set; }

        [StringLength(255)]
        public string Cookie_Content { get; set; }

        [StringLength(50)]
        public string Campaign_Reason { get; set; }

        public int? Web_Site_Id { get; set; }

        public bool IsNew { get; set; }
    }
}
