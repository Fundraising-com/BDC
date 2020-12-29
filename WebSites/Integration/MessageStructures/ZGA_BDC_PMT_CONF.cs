
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
   ///   GA BDC Payment Confirmation:
   ///
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_PMT_CONF
   ///      This is an auto-generated structure of the ZGA_BDC_PMT_CONF IDoc
   /// </summary>
#pragma warning disable 282
   public partial struct ZGA_BDC_PMT_CONF : IInboundIDocStruct
   {
      /// <summary>
      ///   GA BDC Payment Confirmation Segment:
      ///
      ///   ZGA_BDC_PMT_CONF_SEG segment (required)
      /// </summary>
      public ZGA_BDC_PMT_CONF_SEG ZgaBdcPmtConfSeg;
      
      #region initialization
      
      /// <summary>
      ///   Constructor w/optional member segment initialization
      /// </summary>
      public ZGA_BDC_PMT_CONF(bool isInitializationRequired) : this()
      {
         ZgaBdcPmtConfSeg = new ZGA_BDC_PMT_CONF_SEG(isInitializationRequired);
      }
      
      internal const string IDocTypeAsDefined = "ZGA_BDC_PMT_CONF";
      internal const string IDocMessageTypeAsDefined = "ZGA_BDC_PMT_CONF";
      
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