
/*****************************************************************************************************************
-- Generated using CodeSmith v4.1.2.2729
-- 
-- DO NOT ALTER!
-- 
-- Template Authors: Leonard Lewis
/****************************************************************************************************************/
using System;
using System.Collections.Generic;

using SWCorporate.SAP.Shared.Interfaces;

namespace GA.BDC.Integration.MessageStructures
{
   /// <summary>
   ///   GA BDC Order:
   ///
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_ORDER
   ///      This is an auto-generated structure of the ZGA_BDC_ORDER IDoc
   /// </summary>
#pragma warning disable 282
   public partial struct ZGA_BDC_ORDER : IOutboundIDocStruct
   {
      /// <summary>
      ///   GA BDC Order Header Segment:
      ///
      ///   ZGA_BDC_ORDER_HDR_SEG segment (required)
      /// </summary>
      public ZGA_BDC_ORDER_HDR_SEG ZgaBdcOrderHdrSeg;
      
      /// <summary>
      ///   GA BDC Order Account Segment List:
      ///
      ///   ZGA_BDC_ORDER_ACCT_SEG segments (list of 2 to 2, required)
      /// </summary>
      public List<ZGA_BDC_ORDER_ACCT_SEG> ZgaBdcOrderAcctSegList;
      
      /// <summary>
      ///   GA BDC Order Item Segment List:
      ///
      ///   ZGA_BDC_ORDER_ITEM_SEG segments (list of 1 to 999, required)
      /// </summary>
      public List<ZGA_BDC_ORDER_ITEM_SEG> ZgaBdcOrderItemSegList;
      
      #region initialization
      
      /// <summary>
      ///   Constructor w/optional member segment initialization
      /// </summary>
      public ZGA_BDC_ORDER(bool isInitializationRequired) : this()
      {
         ZgaBdcOrderAcctSegList = new List<ZGA_BDC_ORDER_ACCT_SEG>();
         ZgaBdcOrderItemSegList = new List<ZGA_BDC_ORDER_ITEM_SEG>();

         ZgaBdcOrderHdrSeg = new ZGA_BDC_ORDER_HDR_SEG(isInitializationRequired);
      }
      
      internal const string IDocTypeAsDefined = "ZGA_BDC_ORDER";
      internal const string IDocMessageTypeAsDefined = "ZGA_BDC_ORDER";
      
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