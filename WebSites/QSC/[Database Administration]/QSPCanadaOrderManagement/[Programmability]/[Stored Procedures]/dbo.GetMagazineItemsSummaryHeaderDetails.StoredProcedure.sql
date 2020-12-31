USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetMagazineItemsSummaryHeaderDetails]    Script Date: 06/07/2017 09:19:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
	QSPCanadaFinance.dbo.UDF_CleanPhoneNumber(Phone.PhoneNumber,'-') PhoneNumber,
	CONVERT(datetime, CONVERT(varchar,DateReceived,112))  as DateReceived,
	CONVERT(datetime, CONVERT(varchar,DateBatchCompleted,112))  as DateBatchCompleted,
	c.Lang
    , qsp.CompanyName 	as QSPCompanyName
    , qsp.ShipAddress1 	as QSPShipAddress1
    , qsp.ShipAddress2 	as QSPShipAddress2 
    , qsp.ShipCity   	as QSPShipCity
    , qsp.ShipProvince    	as QSPShipProvince
    , qsp.ShipPostalCode  as QSPShipPostalCode
    , qsp.ShipPhone2      	as QSPShipPhone1

    , QspFR.CompanyName 	as QspFRCompanyName
    , QspFR.ShipAddress1 	as QspFRShipAddress1
    , QspFR.ShipAddress2 	as QspFRShipAddress2 
    , QspFR.ShipCity   	  	as QspFRShipCity
    , QspFR.ShipProvince    	as QspFRShipProvince
    , QspFR.ShipPostalCode  	as QspFRShipPostalCode
    , QspFR.ShipPhone1      	as QspFRShipPhone1
	
FROM  QSPCanadaOrderManagement..Batch B  
	INNER JOIN QSPCanadaCommon..CAccount A on A.ID = B.AccountID
	LEFT JOIN QSPCanadaCommon..AddressList AL on A.AddressListID = AL.ID
	LEFT JOIN QSPCanadaCommon..Address AdShip on AL.ID = AdShip.AddressListID AND AdShip.Address_Type = 54001 --Ship To
	LEFT JOIN QSPCanadaCommon..Address AdBill   on AL.ID = AdBill.AddressListID   AND AdBill.Address_Type = 54001 --Use ship to for both.  Bill To=2
	INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
	LEFT JOIN QSPCanadaCommon..Phone Phone on  A.PhoneListID = Phone.PhoneListID AND Phone.Type = 30505 --Main
	LEFT  JOIN qspcanadacommon.dbo.CompanyInfo Qsp on qsp.CompanyID = 1 -- qsp toronto
	LEFT  JOIN qspcanadacommon.dbo.CompanyInfo QspFR on QspFR.CompanyID = 4 -- qsp quebec new address

WHERE OrderID = @OrderID

SET NOCOUNT OFF
GO
