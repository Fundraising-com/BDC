USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_OrderEntryFollowupReport]    Script Date: 06/07/2017 09:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_OrderEntryFollowupReport]
@ReportRequestID int,  @OrderID int

--@AccountID int, @CampaignID int,

AS
SET NOCOUNT ON

-- Saqib Shah, August, 2004
-- used in .net Order Entry Followup Report

 Select batch.id,batch.date,batch.OrderID,
	batch.AccountID 	as BillToAccountID,
	billac.Name 		as BillToAccountName,
	BillPhone.PhoneNumber 	as AccountBillToPhone,
	billadd.Street1		as BillToAddress1,
	billadd.Street2		as BillToAddress2,
	billadd.City		as BillToCity,
	billadd.StateProvince	as BillToState,
	billadd.Postal_Code	as BillToPostalCode,
	batch.ShipToAccountId	as ShipToAccountId,
	shipac.Name 		as ShipToAccountName,
	QspCanadaFinance.dbo.UDF_CleanPhoneNumber(ShipPhone.PhoneNumber   ,'-')  as AccountShipToPhone,
	shipadd.Street1		as ShipToAddress1,
	shipadd.Street2		as ShipToAddress2,
	shipadd.City		as ShipToCity,
	shipadd.StateProvince	as ShipToState,
	shipadd.Postal_Code	as ShipToPostalCode,
	batch.OrderIDIncentive,
	Convert(varchar(10),batch.DateCreated,101) 	as DateCreated,
	Convert(varchar(10),batch.DateReceived,101) 	as DateReceived,
	batch.CampaignId,
	batch.StatusInstance,
	ca.Lang,
             ca.isStaffOrder ,        
	ca.BillToCampaignContactID,
 	ConBill.FirstName+' '+ConBill.LastName 	as BillToContactName,
	ca.ShipToCampaignContactID,
 	ConShip.FirstName+' '+ConShip.LastName as ShipToContactName,
	ca.FMID				 as FMID,
        	fm.FirstName+' '+fm.LastName 		as FM_Name,
	inv.Invoice_ID,
	Convert(varchar(10),inv.invoice_Date,101) as Invoice_Date

	    , qsp.CompanyName 	as QSPCompanyName
	    , qsp.ShipAddress1 	as QSPShipAddress1
	    , qsp.ShipAddress2 	as QSPShipAddress2 
	    , qsp.ShipCity   	as QSPShipCity
	    , qsp.ShipProvince    	as QSPShipProvince
	    , qsp.ShipPostalCode  as QSPShipPostalCode
	    , qsp.ShipPhone2        as QSPShipPhone
	 --   , QspCanadaFinance.dbo.UDF_CleanPhoneNumber(qsp.ShipPhone2  ,'-')  as QSPShipPhone
	
	    , QspFR.CompanyName 	as QspFRCompanyName
	    , QspFR.ShipAddress1 	as QspFRShipAddress1
	    , QspFR.ShipAddress2 	as QspFRShipAddress2 
	    , QspFR.ShipCity   	  	as QspFRShipCity
	    , QspFR.ShipProvince    	as QspFRShipProvince
	    , QspFR.ShipPostalCode  	as QspFRShipPostalCode
	    , QspFR.ShipPhone1                as QspFRShipPhone1
	--    , QspCanadaFinance.dbo.UDF_CleanPhoneNumber(QspFR.ShipPhone1    ,'-')  as QspFRShipPhone1
  From  QSPCanadaOrderManagement..Batch as batch
	Left Outer join QSPCanadaFinance..Invoice as inv
	ON  batch.orderID = inv.Order_ID

	Left Outer join QSPCanadaCommon..CAccount as billac
	ON   batch.AccountId = billac.id
	Left outer Join QSPCanadaCommon..Address as billadd
        	ON billac.AddressListId	= billadd.AddressListId
           and  billadd.Address_Type = 54002 -- billing address
        	Left outer join QSPCanadaCommon..Phone	as BillPhone
        	ON billac.PhoneListID = BillPhone.PhoneListID 
  	   and BillPhone.Type = 30505 --main fone

	Left Outer join QSPCanadaCommon..CAccount as shipac
	ON   batch.ShipToAccountId = shipac.id
	Left outer Join QSPCanadaCommon..Address as shipadd
        	ON shipac.AddressListId	= shipadd.AddressListId
           	and shipadd.Address_Type = 54001 --shipping address
	Left outer join QSPCanadaCommon..Phone as ShipPhone
	ON  shipac.PhoneListID 		= ShipPhone.PhoneListID
  	    and ShipPhone.Type 		= 30505, --main phone

	QSPCanadaCommon..Campaign 		as ca
	Left outer join QSPCanadaCommon..Contact as ConBill
	ON  ca.BillToCampaignContactID 	= ConBill.ID
	Left outer join QSPCanadaCommon..Contact as ConShip
	ON  ca.ShipToCampaignContactID 	= ConShip.ID
	LEFT  JOIN qspcanadacommon.dbo.CompanyInfo Qsp on qsp.CompanyID = 1 -- qsp toronto
	LEFT  JOIN qspcanadacommon.dbo.CompanyInfo QspFR on QspFR.CompanyID = 4 -- qsp quebec new address
	,
	QSPCanadaCommon..FieldManager 	as fm
  Where batch.campaignID 			= ca.ID
  	and ca.FMID 				= fm.FMID
             and batch.orderqualifierid not in (39014) -- should not be CC-Reprocess
  	and batch.OrderID 			= @OrderID
 Order by batch.OrderID,batch.date

--following lines are written by saqib on 13-Apr-2005 to update data driven subscription support tables
IF @ReportRequestID <> 0  -- if the value is not zero it means the report is called from a data driven subscription
BEGIN
     
   UPDATE Qspcanadaordermanagement.dbo.ReportRequestBatch_OrderEntryFollowupReport
   set  RunDateStart = getdate()
   where [id]  = @ReportRequestID

END
GO
