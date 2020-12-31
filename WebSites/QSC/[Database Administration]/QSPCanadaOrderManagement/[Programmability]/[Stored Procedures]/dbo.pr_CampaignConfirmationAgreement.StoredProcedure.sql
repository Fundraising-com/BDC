USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CampaignConfirmationAgreement]    Script Date: 06/07/2017 09:19:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CampaignConfirmationAgreement]
@FMID int, @StartDate Datetime, @EndDate Datetime, @AccountID int, @CampaignID int
AS
SET NOCOUNT ON

-- Saqib Shah, May, 2005
-- CA Confirmation Report - 

 IF (@AccountID IS NULL AND  @CampaignID IS NULL AND @FMID IS NULL) 

   Begin
    
     PRINT 'Error, user should throw atleast one parameter from one of these'
   
   End

 ELSE

   BEGIN

  declare @TempContact int
 select top 1 @TempContact = id  from QSPCanadaCommon.dbo.Contact   --  @TempContact is filled with one random contact and used in equi join cluase in order to make sure that query never fails



 Select   ca.ID as CampaignId,
	Convert(varchar(20),  ca.StartDate,106)  as CampaignStartDate,
	Convert(varchar(20), ca.EndDate , 106)  as CampaignEndDate,
	ca.BillToAccountID 	as BillToAccountID,
	billac.Name 		as BillToAccountName,
	BillPhone.PhoneNumber 	as AccountBillToPhone,
	BillFax.PhoneNumber 	as AccountBillToFax,
	billadd.Street1		as BillToAddress1,
	billadd.Street2		as BillToAddress2,
	billadd.City		as BillToCity,
	billadd.StateProvince	as BillToState,
	billadd.Postal_Code	as BillToPostalCode,
	ca.ShipToAccountId	as ShipToAccountId,
	shipac.Name 		as ShipToAccountName,
	ShipPhone.PhoneNumber 	as AccountShipToPhone,
	ShipFax.PhoneNumber 	as AccountShipToFax,
	shipadd.Street1		as ShipToAddress1,
	shipadd.Street2		as ShipToAddress2,
	shipadd.City		as ShipToCity,
	shipadd.StateProvince	as ShipToState,
	shipadd.Postal_Code	as ShipToPostalCode,
	ca.Lang,
	ca.Country,
	ca.BillToCampaignContactID,
 	ConBill.FirstName+' '+ConBill.LastName 	as BillToContactName,
	ConBill.Email 			as BillToContactEmail,
	BillContactPhone.PhoneNumber 	as BillContactPhone,
	ca.ShipToCampaignContactID,
 	ConShip.FirstName+' '+ConShip.LastName as ShipToContactName,
	ConShip.Email 				as ShipToContactEmail,
	ShipContactPhone.PhoneNumber 	as ShipContactPhone,
	ca.FMID				as FMID,
       	fm.FirstName+' '+fm.LastName 		as FM_Name,
	FMPhone.PhoneNumber 		as FMPhone,
	fm.Email 				as FMEmail,
	ca.EstimatedGross			as EstimatedSales,
	ca.NumberofParticipants,			
	ca.NumberOfClassRoooms,
	ca.NumberofStaff,
	case ca.IsStaffOrder when 0 then case ca.lang when 'EN' then 'NO' else 'Non' end 
			       when 1 then case ca.lang when 'EN' then 'YES' else 'Oui' end 
			       else 'NO' end as IsStaffOrder,

	case  when ca.FSRequired <> 1 then case ca.lang when 'EN' then 'NO' else 'Non' end 
	          when ca.FSRequired = 1 then  case ca.lang when 'EN' then 'YES' else 'Oui' end 
	          else case ca.lang when 'EN' then 'NO' else 'Non' end 
	          end as IsFSRequired,

	case when ca.SuppliesDeliveryDate = '1995-01-01' or ca.FSRequired <> 1 then null else Convert(varchar(20), ca.SuppliesDeliveryDate , 106) end as SuppliesDeliveryDate,

        	case when SuppliesShipToCampaignContactID = 63001 and ca.FSRequired = 1 then case ca.lang when 'EN' then 'FM' else 'REP' end 
	      when  SuppliesShipToCampaignContactID = 63002 and ca.FSRequired = 1 then  case ca.lang when 'EN' then 'SCHOOL' else 'ÉCOLE' end 
	      when  SuppliesShipToCampaignContactID = 63003 and ca.FSRequired = 1 then  case ca.lang when 'EN' then 'OTHER' else 'AUTRE' end 
	      end as SuppliesShipToContactEntity,

      	case  IncentivesDistributionID when 62001 then case ca.lang when 'EN' then 'Campaign' else 'Campagne' end 
	      when  62002 then  case ca.lang when 'EN' then 'Class' else 'Classe' end 
	      when  62003 then  case ca.lang when 'EN' then 'Student' else 'Étudiant' end 
	      end as IncentivesDistributionEntity,

	CASE ca.Lang WHEN 'FR' THEN COALESCE(Prog.FrenchName, COALESCE(Prog.Name, '')) ELSE COALESCE(Prog.Name, '') END as ProgramName,
            cast(cp.GroupProfit as varchar) + '%' as GroupProfit,
            Case IsPreCollect
            when 'Y' then 
		CASE ca.Lang	WHEN 'FR' THEN	'Pré-collecte'
				ELSE			'Pre-collect'
		END
            when 'N' then
		CASE ca.Lang	WHEN	'FR' THEN	'Post-collecte'
				ELSE			'Post-collect'
		END
            else 'N/A'
	    end as 'IsPreCollect',

	SuppliesShipToCampaignContactID,
	SuppliesCampaignContactID,
	DeliverContact.FirstName+' '+DeliverContact.LastName 	as DeliverContactName,
	DeliverAddress.Street1		as DeliverAddress1,
	DeliverAddress.Street2		as DeliverAddress2,
	DeliverAddress.City		as DeliverCity,
	DeliverAddress.StateProvince	as DeliverState,
	DeliverAddress.Postal_Code	as DeliverPostalCode,
	ca.SpecialInstructions
  	
  From 	 QSPCanadaCommon.dbo.Campaign 	as ca,
	 QSPCanadaCommon.dbo.CAccount 	as billac,
	 QSPCanadaCommon.dbo.Address 	as billadd,
       	 QSPCanadaCommon.dbo.Phone	as BillPhone,
       	 QSPCanadaCommon.dbo.Phone	as BillFax,
	 QSPCanadaCommon.dbo.CAccount 	as shipac,
	 QSPCanadaCommon.dbo.Address 	as shipadd,
	 QSPCanadaCommon.dbo.Phone 	as ShipPhone,
       	 QSPCanadaCommon.dbo.Phone	as ShipFax,
	 QSPCanadaCommon.dbo.Contact 	as ConBill,
	 QSPCanadaCommon.dbo.Contact 	as ConShip,
	 QSPCanadaCommon.dbo.Contact 	as DeliverContact,
	 QSPCanadaCommon.dbo.Address 	as DeliverAddress,
       	 QSPCanadaCommon.dbo.Phone		as BillContactPhone,
       	 QSPCanadaCommon.dbo.Phone		as ShipContactPhone,
	 QSPCanadaCommon.dbo.FieldManager  	as fm,
       	 QSPCanadaCommon.dbo.Phone		as FMPhone,
	 QspCanadaCommon.dbo.CampaignProgram 	as cp,
	 QspCanadaCommon.dbo.Program 		as prog

  where   ca.BillToAccountID 		= billac.id
      	 and billac.AddressListId	*= billadd.AddressListId
	 and billadd.Address_Type 	= 54002 -- billing address
         	 and billac.PhoneListID 	*= BillPhone.PhoneListID 
  	 and BillPhone.Type 		= 30505 --main fone
         	 and billac.PhoneListID 	*= BillFax.PhoneListID 
  	 and BillFax.Type 		= 30503 --main fone

	 and ca.ShipToAccountID  	= shipac.id
       	 and shipac.AddressListId	*= shipadd.AddressListId
       	 and shipadd.Address_Type 	= 54001 --shipping address
	 and shipac.PhoneListID 	*= ShipPhone.PhoneListID
  	 and ShipPhone.Type 		= 30505 --main phone
	 and shipac.PhoneListID 	*= ShipFax.PhoneListID
  	 and ShipFax.Type 		= 30503 --main phone

	 and ca.BillToCampaignContactID = ConBill.ID
	 and ca.ShipToCampaignContactID = ConShip.ID
	 and ConBill.PhoneListID 		*= BillContactPhone.PhoneListID
  	 and BillContactPhone.Type 		= 30501 --work phone
	 and ConShip.PhoneListID 		*= ShipContactPhone.PhoneListID
  	 and ShipContactPhone.Type 		= 30501 --work phone
	 and ca.FMID 				= fm.FMID
	 and fm.PhoneListID 			*= FMPhone.PhoneListID
  	 and FMPhone.Type 			= 30505  --main phone	
         	 and Isnull(ca.SuppliesCampaignContactID,IsNull(ca.ShipToCampaignContactID,@TempContact)) = DeliverContact.id
	 and DeliverContact.AddressID  *= DeliverAddress.Address_ID
	 and  cp.ProgramID  = prog.id 
	 and  cp.CampaignID  =  ca.id
	 and cp.DeletedTF = 0
	 AND cp.OnlineOnly = 0
  	 and isnull(ca.ShipToAccountId,'') 	= isnull(@AccountID, isnull(ca.ShipToAccountId,'')   )
  	 and ca.ID 				= isnull(@CampaignID,ca.ID )
	 and ca.StartDate >=  IsNull(@StartDate,ca.StartDate) and ca.EndDate <= IsNull(@EndDate,ca.EndDate)
	 and ca.FMID =    IsNull(@FMID,ca.FMID)

 Order by ca.ID

END
GO
