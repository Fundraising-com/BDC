using System;
using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
    public class Session
    {
        public Session()
        {
            Properties = new Dictionary<string, string>();
        }
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Anonymous Id
        /// </summary>
        public string AnonymousId { get; set; }
        /// <summary>
        /// Created
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Properties
        /// </summary>
        public IDictionary<string, string> Properties { get; set; }
    }
}
