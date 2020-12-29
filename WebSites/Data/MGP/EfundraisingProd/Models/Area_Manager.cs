namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Area_Manager
    {
        public Area_Manager()
        {
            Field_Sales_Manager = new HashSet<Field_Sales_Manager>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Area_Manager_ID { get; set; }

        [StringLength(25)]
        public string Area_Manager_Name { get; set; }

        public virtual ICollection<Field_Sales_Manager> Field_Sales_Manager { get; set; }
    }
}
