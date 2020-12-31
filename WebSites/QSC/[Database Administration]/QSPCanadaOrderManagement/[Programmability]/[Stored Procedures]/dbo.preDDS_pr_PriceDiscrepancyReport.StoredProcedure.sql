USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[preDDS_pr_PriceDiscrepancyReport]    Script Date: 06/07/2017 09:20:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[preDDS_pr_PriceDiscrepancyReport]
@AccountID int, @CampaignID int, @OrderID int,@FMID int, @InvoiceID int
AS
SET NOCOUNT ON

-- SSS- Sep, 2004
-- used in .net Price Descrepancy Report
-- all columns in this query may not be used in report

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
	ShipPhone.PhoneNumber 	as AccountShipToPhone,
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
	batch.OrderTypeCode,
	batch.MagnetCardsMailed,
	batch.MagnetPostage,
	batch.MagnetMailDate,
	ca.Lang,
	ca.Country,
	ca.magnetStatementDate,	
	ca.BillToCampaignContactID,
 	ConBill.FirstName+' '+ConBill.LastName 	as BillToContactName,
	ca.ShipToCampaignContactID,
 	ConShip.FirstName+' '+ConShip.LastName as ShipToContactName,
	ca.FMID				 as FMID,
        	fm.FirstName+' '+fm.LastName 		as FM_Name,
	inv.Invoice_ID,
	Convert(varchar(10),inv.invoice_Date,101) as Invoice_Date
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

        QSPCanadaCommon..Contact		as ConBill,
	QSPCanadaCommon..Contact		as ConShip,
	QSPCanadaCommon..Campaign 		as ca,
	QSPCanadaCommon..FieldManager 		as fm
  where batch.campaignID 			= ca.ID
  	and ca.FMID 				= fm.FMID
  	and ca.BillToCampaignContactID 	= ConBill.ID 
  	and ca.ShipToCampaignContactID 	= ConShip.ID 
  	and isnull(batch.ShipToAccountId,'') 	= isnull(@AccountID, isnull(batch.ShipToAccountId,'')   )
  	and batch.CampaignId 			= isnull(@CampaignID,batch.CampaignId )
  	and batch.OrderID 			= isnull(@OrderID,batch.OrderID)
  	and ca.FMID 				= isnull(@FMID,ca.FMID )
  	and IsNull(inv.Invoice_ID,'') 		= isnull(@InvoiceID, IsNull(inv.Invoice_ID,''))
Order By batch.OrderID,Batch.Date
GO
