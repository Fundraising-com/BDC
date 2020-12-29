using System;
using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
    /// <summary>
    /// Fundraising Parnter and its custom attributes
    /// </summary>
    public class Partner
    {
        public Partner()
        {
            Attributes = new Dictionary<string, string>();
        }
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Has Collection Site
        /// </summary>
        public bool HasCollectionSite { get; set; }
        /// <summary>
        /// Created
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Custom Attributes
        /// </summary>
        public IDictionary<string, string> Attributes { get; set; }
    }
}
