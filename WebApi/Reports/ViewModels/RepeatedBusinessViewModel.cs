using System;
using System.ComponentModel.DataAnnotations;

namespace GA.BDC.WebApi.Reports.ViewModels
{
    public class RepeatedBusinessViewModel
    {
        [Required]
        public DateTime Date1Start { get; set; }
        [Required]
        public DateTime Date1End { get; set; }
        [Required]
        public DateTime Date2Start { get; set; }
        [Required]
        public DateTime Date2End { get; set; }
        [Required]
        public Boolean ShowFCs { get; set; }
    }
}