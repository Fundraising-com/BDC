using System;
using GA.BDC.Integration.MessageStructures;
using GA.Core.Shared.Contracts;
using SWCorporate.SAP.Shared;

namespace GA.BDC.Integration.MessageFormatters
{
   public sealed class GrpContractIDocMessageFormatter : IDocMessageFormatter<ZGA_BDC_GRP_CONTRACT>
   {
      #region Private Members

      private const string __BLANK_SAP_VALUE = "/";
      private readonly Contract _grpContract = null;

      #endregion

      #region Constructors

      public GrpContractIDocMessageFormatter(Contract grpContract)
      {
         this._grpContract = grpContract;
      }

      #endregion

      protected override IDoc FormatMessage()
      {
         if(_grpContract == null)
         {
            throw new Exception(
               "The BDC Group Contract cannot be updated Through the GA.BDC.Integration.MessageFormatters.GRPContractMessageFormatter for IDoc Template: ZGA_BDC_GRP_CONTRACT.  The Contract source has to be provided.");
         }

         ZGA_BDC_GRP_CONTRACT GrpContractMessage = new ZGA_BDC_GRP_CONTRACT(true);
         IDoc iDoc = this.CreateIDoc(GrpContractMessage, 1);
         //We currently only Update Contracts
         // Contract.SAPTransmittedDate is null we have to do a 'CRE'
         //Currently we should never create and have SAPTransmittedDate of null
         GrpContractMessage.ZgaBdcGrpContractSeg.TransType = "CHG";

         //Set defaults for empty fields so SAP doesn't barf if they aren't set in the address loop
         GrpContractMessage.ZgaBdcGrpContractSeg.Address1 =
            GrpContractMessage.ZgaBdcGrpContractSeg.Address2 =
            GrpContractMessage.ZgaBdcGrpContractSeg.City =
            GrpContractMessage.ZgaBdcGrpContractSeg.Country =
            GrpContractMessage.ZgaBdcGrpContractSeg.Email =
            GrpContractMessage.ZgaBdcGrpContractSeg.GroupName =
            GrpContractMessage.ZgaBdcGrpContractSeg.SalespAcct =
            GrpContractMessage.ZgaBdcGrpContractSeg.SponsorName =
            GrpContractMessage.ZgaBdcGrpContractSeg.State =
            GrpContractMessage.ZgaBdcGrpContractSeg.Telephone =
            GrpContractMessage.ZgaBdcGrpContractSeg.Zip = __BLANK_SAP_VALUE;

         //Determine ContractAddress Entities
         foreach(ContractAddress ca in _grpContract.ContractAddresses)
         {
            if(ca.IsPartner)
            {
               GrpContractMessage.ZgaBdcGrpContractSeg.BdcPartnerId = ca.ExternalIdentifier.ToString();
            }

            if(ca.IsSoldTo)
            {
               GrpContractMessage.ZgaBdcGrpContractSeg.BdcGroupId = ca.ExternalIdentifier.ToString();
               GrpContractMessage.ZgaBdcGrpContractSeg.Email = ca.EmailAddress;
               GrpContractMessage.ZgaBdcGrpContractSeg.GroupName = ca.Name1;
            }

            if(ca.IsSalesPerson)
            {
               GrpContractMessage.ZgaBdcGrpContractSeg.SalespAcct = ca.SAPAcctNo.ToString();
            }

            if(ca.IsPayer)
            {
               GrpContractMessage.ZgaBdcGrpContractSeg.SponsorName = ca.Name1;
               GrpContractMessage.ZgaBdcGrpContractSeg.Address1 = ca.Address;
               GrpContractMessage.ZgaBdcGrpContractSeg.Address2 = string.Empty;
               GrpContractMessage.ZgaBdcGrpContractSeg.City = ca.City;
               GrpContractMessage.ZgaBdcGrpContractSeg.State = ca.StateProvinceAbbr;
               GrpContractMessage.ZgaBdcGrpContractSeg.Zip = ca.PostalCode;
               GrpContractMessage.ZgaBdcGrpContractSeg.Country = ca.CountryAbbr;
               GrpContractMessage.ZgaBdcGrpContractSeg.Telephone = ca.PhoneNumber;
            }
         }

         GrpContractMessage.ZgaBdcGrpContractSeg.FocusContractId = _grpContract.ContractIDWithCheckDigit;
         GrpContractMessage.ZgaBdcGrpContractSeg.Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
         iDoc.AddSegment(GrpContractMessage.ZgaBdcGrpContractSeg);
         return iDoc;
      }
   }
}