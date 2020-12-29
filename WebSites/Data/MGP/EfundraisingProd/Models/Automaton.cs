namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Automaton")]
    public partial class Automaton
    {
        public Automaton()
        {
            Automaton_State = new HashSet<Automaton_State>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Automaton_Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Automaton_State> Automaton_State { get; set; }
    }
}
