USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateResolveAccountList]    Script Date: 06/07/2017 09:33:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_GenerateResolveAccountList]
as

SELECT A.ID as AcctID, 
	A.Name as AcctName,
	AdShip.Street1 		as ShippingAddress,
	AdShip.Street2 		as ShippingAddress2,
	AdShip.City      		as ShippingCity,
	AdShip.StateProvince	as ShippingState,
	AdShip.Postal_Code      	as ShippingZip,
	AdBill.Street1 		as BillingAddress,
	AdBill.Street2 		as BillingAddress2,
	AdBill.City      		as BillingCity,
	AdBill.StateProvince	as BillingState,
	AdBill.Postal_Code      	as BillingZip,
	PhoneNumber

FROM  
	QSPCanadaCommon..CAccount A 
	LEFT JOIN QSPCanadaCommon..AddressList AL on A.AddressListID = AL.ID
	LEFT JOIN QSPCanadaCommon..Address AdShip on AL.ID = AdShip.AddressListID AND AdShip.Address_Type = 54001 --Ship To
	LEFT JOIN QSPCanadaCommon..Address AdBill   on AL.ID = AdBill.AddressListID   AND AdBill.Address_Type = 54002 --Use ship to for both.  Bill To=2
	LEFT JOIN QSPCanadaCommon..Phone Phone on  A.PhoneListID = Phone.PhoneListID AND Phone.Type = 30501
GO
