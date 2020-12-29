
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
   ///   GA BDC Payment:
   ///
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_PAYMENT
   ///      This is an auto-generated structure of the ZGA_BDC_PAYMENT IDoc
   /// </summary>
#pragma warning disable 282
   public partial struct ZGA_BDC_PAYMENT : IOutboundIDocStruct
   {
      /// <summary>
      ///   GA BDC Payment Segment:
      ///
      ///   ZGA_BDC_PAYMENT_SEG segment (required)
      /// </summary>
      public ZGA_BDC_PAYMENT_SEG ZgaBdcPaymentSeg;
      
      #region initialization
      
      /// <summary>
      ///   Constructor w/optional member segment initialization
      /// </summary>
      public ZGA_BDC_PAYMENT(bool isInitializationRequired) : this()
      {
         ZgaBdcPaymentSeg = new ZGA_BDC_PAYMENT_SEG(isInitializationRequired);
      }
      
      internal const string IDocTypeAsDefined = "ZGA_BDC_PAYMENT";
      internal const string IDocMessageTypeAsDefined = "ZGA_BDC_PAYMENT";
      
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