USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetMagazineItemsSummaryHeaderDetails]    Script Date: 06/07/2017 09:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--drop PROCEDURE dbo.GetMagazineItemsSummaryHeaderDetails

CREATE PROCEDURE [dbo].[GetMagazineItemsSummaryHeaderDetails]
	@OrderID	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 8/18/2004 
--   Get Magazine Items Summary Header Details For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON


SELECT A.ID as AcctID, CampaignID, OrderId,  
	ContactFirstName + ' ' + ContactLastname as ContactName,
	A.Name as AcctName,
	AdShip.Street1 		as ShippingAddress,
	AdShip.Street2 		as ShippingAddress2,
	AdShip.City      		as ShippingCity,
	AdShip.StateProvince	as ShippingState,
	AdShip.Postal_Code      	as ShippingZip,
	AdShip.Zip4		as ShippingZip4,
	AdBill.Street1 		as BillingAddress,
	AdBill.Street2 		as BillingAddress2,
	AdBill.City      		as BillingCity,
	AdBill.StateProvince	as BillingState,
	AdBill.Postal_Code      	as BillingZip,
	AdBill.Zip4		as BillingZip4,
	PhoneNumber,
	CONVERT(datetime, CONVERT(varchar,DateReceived,112))  as DateReceived,
	CONVERT(datetime, CONVERT(varchar,DateBatchCompleted,112))  as DateBatchCompleted	
FROM  QSPCanadaOrderManagement..Batch B  
	INNER JOIN QSPCanadaCommon..CAccount A on A.ID = B.AccountID
	LEFT JOIN QSPCanadaCommon..AddressList AL on A.AddressListID = AL.ID
	LEFT JOIN QSPCanadaCommon..Address AdShip on AL.ID = AdShip.AddressListID AND AdShip.Address_Type = 54001 --Ship To
	LEFT JOIN QSPCanadaCommon..Address AdBill   on AL.ID = AdBill.AddressListID   AND AdBill.Address_Type = 54001 --Use ship to for both.  Bill To=2
	INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
	LEFT JOIN QSPCanadaCommon..Phone Phone on  A.PhoneListID = Phone.PhoneListID AND Phone.Type = 30501
WHERE OrderID = @OrderID

SET NOCOUNT OFF
GO
