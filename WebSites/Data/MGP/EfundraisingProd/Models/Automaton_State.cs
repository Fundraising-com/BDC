namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Automaton_State
    {
        public Automaton_State()
        {
            Automaton_Transition = new HashSet<Automaton_Transition>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Automaton_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int State_Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual Automaton Automaton { get; set; }

        public virtual ICollection<Automaton_Transition> Automaton_Transition { get; set; }
    }
}
