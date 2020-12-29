namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Automaton_Transition
    {
        public Automaton_Transition()
        {
            Automaton_Transition_Function = new HashSet<Automaton_Transition_Function>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Automaton_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int State_To_Id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int State_From_Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual Automaton_State Automaton_State { get; set; }

        public virtual ICollection<Automaton_Transition_Function> Automaton_Transition_Function { get; set; }
    }
}
