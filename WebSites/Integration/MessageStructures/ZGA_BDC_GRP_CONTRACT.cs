
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
   ///   GA BDC Group Contract:
   ///
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_GRP_CONTRACT
   ///      This is an auto-generated structure of the ZGA_BDC_GRP_CONTRACT IDoc
   /// </summary>
#pragma warning disable 282
   public partial struct ZGA_BDC_GRP_CONTRACT : IOutboundIDocStruct
   {
      /// <summary>
      ///   GA  BDC Group Contract:
      ///
      ///   ZGA_BDC_GRP_CONTRACT_SEG segment (required)
      /// </summary>
      public ZGA_BDC_GRP_CONTRACT_SEG ZgaBdcGrpContractSeg;
      
      #region initialization
      
      /// <summary>
      ///   Constructor w/optional member segment initialization
      /// </summary>
      public ZGA_BDC_GRP_CONTRACT(bool isInitializationRequired) : this()
      {
         ZgaBdcGrpContractSeg = new ZGA_BDC_GRP_CONTRACT_SEG(isInitializationRequired);
      }
      
      internal const string IDocTypeAsDefined = "ZGA_BDC_GRP_CONTRACT";
      internal const string IDocMessageTypeAsDefined = "ZGA_BDC_GRP_CONTRACT";
      
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
         get { return "FOCUS"; }
      }

      string IOutboundIDocStruct.SenderPartnerType
      {
         get { return "LS"; }
      }
      
      #endregion
   }
#pragma warning restore 282
}