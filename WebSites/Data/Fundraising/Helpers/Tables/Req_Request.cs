namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Req_Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Request_Id { get; set; }

        public int? Language_Id { get; set; }

        public int? Request_Type_ID { get; set; }

        public int? Project_Type_ID { get; set; }

        public int? Priority_Id { get; set; }

        [StringLength(60)]
        public string Project_Name { get; set; }

        [Column(TypeName = "text")]
        public string Summary_Description { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Request_Date { get; set; }

        public DateTime? Decision_Required_Date { get; set; }

        [Column(TypeName = "text")]
        public string Impact_Description { get; set; }

        [Column(TypeName = "text")]
        public string Mis_Impact_Description { get; set; }

        [Column(TypeName = "text")]
        public string Decision_Description { get; set; }

        public int? Decision_Id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Decision_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Project_Sheduled_Start_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Project_Sheduled_End_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Project_Start_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Project_End_Date { get; set; }

        public int? Manager_ID { get; set; }

        public int? Employee_Id { get; set; }

        public int? MIS_ID { get; set; }

        public virtual Req_Decision Req_Decision { get; set; }

        public virtual Req_Priority Req_Priority { get; set; }

        public virtual Req_Project_Type Req_Project_Type { get; set; }

        public virtual Req_Request_Type Req_Request_Type { get; set; }
    }
}
