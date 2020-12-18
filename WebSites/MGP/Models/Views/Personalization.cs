using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class Personalization
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// URL Redirection
        /// </summary>
        [Required, MaxLength(255)]
        public string Redirect { get; set; }
        /// <summary>
        /// Group Message
        /// </summary>
        [MaxLength(2000)]
        public string Body { get; set; }
        /// <summary>
        /// Goal
        /// </summary>
        [Required, Range(0.0, double.MaxValue, ErrorMessage = "The Goal should be a positive number.")]
        public double Goal { get; set; }
        /// <summary>
        /// PersonalizationImages
        /// </summary>
        public PersonalizationImage[] PersonalizationImages { get; set; }
        /// <summary>
        /// PersonalizationImages JSON
        /// </summary>
        public string PersoImgsJSON { get; set; }
        /// <summary>
        /// Default Image
        /// </summary>
        public string DefaultImage { get; set; }
    }
}