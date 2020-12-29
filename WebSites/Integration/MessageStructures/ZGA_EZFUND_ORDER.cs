using System;
using System.Collections.Generic;

using SWCorporate.SAP.Shared.Interfaces;

namespace GA.BDC.Integration.MessageStructures
{
    /// <summary>
    ///   GA EZFUND Order:
    ///
    ///   GA.EZFUND.Integration.MessageStructures.ZGA_EZFUND_ORDER
    ///      This is an auto-generated structure of the ZGA_EZFUND_ORDER IDoc
    /// </summary>
#pragma warning disable 282
    public partial struct ZGA_EZFUND_ORDER : IOutboundIDocStruct
    {
        /// <summary>
        ///   GA EZFUND Order Header Segment:
        ///
        ///   ZGA_EZFUND_ORDER_HDR_SEG segment (required)
        /// </summary>
        public ZGA_EZFUND_ORDER_HDR_SEG ZgaEZFUNDOrderHdrSeg;

        /// <summary>
        ///   GA EZFUND Order Account Segment List:
        ///
        ///   ZGA_EZFUND_ORDER_ACCT_SEG segments (list of 2 to 2, required)
        /// </summary>
        public List<ZGA_EZFUND_ORDER_ACCT_SEG> ZgaEZFUNDOrderAcctSegList;

        /// <summary>
        ///   GA EZFUND Order Item Segment List:
        ///
        ///   ZGA_EZFUND_ORDER_ITEM_SEG segments (list of 1 to 999, required)
        /// </summary>
        public List<ZGA_EZFUND_ORDER_ITEM_SEG> ZgaEZFUNDOrderItemSegList;

        #region initialization

        /// <summary>
        ///   Constructor w/optional member segment initialization
        /// </summary>
        public ZGA_EZFUND_ORDER(bool isInitializationRequired) : this()
        {
            ZgaEZFUNDOrderAcctSegList = new List<ZGA_EZFUND_ORDER_ACCT_SEG>();
            ZgaEZFUNDOrderItemSegList = new List<ZGA_EZFUND_ORDER_ITEM_SEG>();

            ZgaEZFUNDOrderHdrSeg = new ZGA_EZFUND_ORDER_HDR_SEG(isInitializationRequired);
        }

        internal const string IDocTypeAsDefined = "ZGA_BDC_ORDER";
        internal const string IDocMessageTypeAsDefined = "ZGA_EZFUND_ORDER";

        #endregion

        #region implicit implementation of SWCorporate.SAP.Shared.Interfaces.IIDocStruct interface

        /// <summary>
        ///   IDoc Type Name
        /// </summary>
        public string IDocTypeName
        {
            get { return IDocTypeAsDefined; }
        }

        /// <summary>
        ///   IDoc Message Type Name
        /// </summary>
        public string IDocMessageTypeName
        {
            get { return IDocMessageTypeAsDefined; }
        }

        #endregion

        #region explicit implementation of SWCorporate.SAP.Shared.Interfaces.IOutboundIDocStruct interface

        string IOutboundIDocStruct.SenderPartner
        {
            get { return "GABDC"; }
        }

        string IOutboundIDocStruct.SenderPartnerType
        {
            get { return "LS"; }
        }

        #endregion
    }
#pragma warning restore 282
}