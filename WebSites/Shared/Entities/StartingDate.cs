using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class StartingDate
    {
        /// <summary>
        /// Sales Start Code
        /// </summary>
        public int StartCode { get; set; }
        /// <summary>
        /// Sales Start Code String
        /// </summary>
        public string StartCodeTxt { get; set; }
        /// <summary>
        /// Sales Start Sequence Number
        /// </summary>
        public int StartSequenceNumber { get; set; }
        /// <summary>
        /// Sales Start Message
        /// </summary>
        public string StartMessage { get; set; }
    }
}
