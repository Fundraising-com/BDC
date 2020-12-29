namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Automaton_Function
    {
        public Automaton_Function()
        {
            Automaton_Transition_Function = new HashSet<Automaton_Transition_Function>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Automaton_Function_Id { get; set; }

        [StringLength(50)]
        public string Function_Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Automaton_Transition_Function> Automaton_Transition_Function { get; set; }
    }
}
