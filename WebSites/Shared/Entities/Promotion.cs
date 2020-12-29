using System;
using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
    /// <summary>
    /// Fundraising Promotion and its custom attributes
    /// </summary>
    public class Promotion
    {
        public Promotion()
        {
            Attributes = new Dictionary<string, string>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public int  Id { get; set; }
        
        /// <summary>
        /// promotion_type_code
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// promotion_destination_id
        /// </summary>
        public int? DestinationId { get; set; }
        
        /// <summary>
        /// promotion_name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// script_name
        /// </summary>
        public string ScriptName { get; set; }
        
        /// <summary>
        /// active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// create_date
        /// </summary>
        public DateTime CreateDate { get; set; }
        
        /// <summary>
        /// cookie_content
        /// </summary>
        public string CookieContent { get; set; }
        
        /// <summary>
        /// keyword
        /// </summary>
        public string Keyword { get; set; }
        
        /// <summary>
        /// is_displayable
        /// </summary>
        public bool? IsDsplayable { get; set; }
        
        /// <summary>
        /// Custom Attributes
        /// </summary>
        public IDictionary<string, string> Attributes { get; set; }

        public int PartnerId { get; set; }
    }
}
