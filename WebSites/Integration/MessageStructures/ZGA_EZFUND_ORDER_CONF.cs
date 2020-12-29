
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
   ///   GA BDC Order Confirmation:
   ///
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_ORDER_CONF
   ///      This is an auto-generated structure of the ZGA_BDC_ORDER_CONF IDoc
   /// </summary>
#pragma warning disable 282
   public partial struct ZGA_EZFUND_ORDER_CONF : IInboundIDocStruct
   {
      /// <summary>
      ///   GA BDC Order Confirmation Segment:
      ///
      ///   ZGA_BDC_ORDER_CONF_SEG segment (required)
      /// </summary>
      public ZGA_EZFUND_ORDER_CONF_SEG ZgaEZFUNDOrderConfSeg;
      
      #region initialization
      
      /// <summary>
      ///   Constructor w/optional member segment initialization
      /// </summary>
      public ZGA_EZFUND_ORDER_CONF(bool isInitializationRequired) : this()
      {
            ZgaEZFUNDOrderConfSeg = new ZGA_EZFUND_ORDER_CONF_SEG(isInitializationRequired);
      }
      
      internal const string IDocTypeAsDefined = "ZGA_BDC_ORDER_CONF";
      internal const string IDocMessageTypeAsDefined = "ZGA_EZFUND_ORDER_CONF";
      
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
      
      #region explicit implementation of SWCorporate.SAP.Shared.Interfaces.IInboundIDocStruct interface
      
      string IInboundIDocStruct.ReceiverPartner
      {
         get { return "GABDC"; }
      }
      
      string IInboundIDocStruct.ReceiverPartnerType
      {
         get { return "LS"; }
      }
      
      string IInboundIDocStruct.ReceiverPort
      {
         get { return "GABDC"; }
      }
      
      #endregion
   }
#pragma warning restore 282
}