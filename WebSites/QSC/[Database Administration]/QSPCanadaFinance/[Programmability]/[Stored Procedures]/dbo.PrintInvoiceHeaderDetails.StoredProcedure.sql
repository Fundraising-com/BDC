USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[PrintInvoiceHeaderDetails]    Script Date: 06/07/2017 09:17:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PrintInvoiceHeaderDetails]
	@InvoiceID	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/25/2004 
--   Get Invoice Header Details For Canada Finance System
--   MS 03/15/2005
--  Get staff order flag from campaign table instead
--   MS 04/27/2005
--  Get latest contact name from contact table if contact not exists in batch

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

DECLARE @Cnt INT

SELECT TOP 1 C.IsStaffOrder,  			
	OrderQualifierID, OrderTypeCode, Invoice_ID, A.ID as AcctID, CampaignID, Order_Id, C.FMID, FM.FirstName + ' ' + FM.LastName as FMName, 
	Cont.FirstName + ' ' + Cont.Lastname as ContactName,
	--ISNULL(B.ContactFirstName + ' ' + B.ContactLastName, Cont.FirstName + ' ' + Cont.LastName)  AS ContactName,
	CONVERT(varchar, Invoice_Date,101) as Invoice_Date, 
	CONVERT(varchar, Invoice_Due_Date,101) as Invoice_Due_Date, 
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
	CASE Is_Printed	
		WHEN 'Y' THEN 1 
		WHEN 'N' THEN 0
		ELSE 0
	END AS Is_Printed,
	MAX(Cont.Id) ContactId
/*FROM INVOICE I
	LEFT JOIN QSPCanadaOrderManagement..Batch B  on B.OrderID = I.Order_ID	--Disabled MS Apr 26 
	LEFT JOIN QSPCanadaCommon..CAccount A on A.ID = B.AccountID
	LEFT JOIN QSPCanadaCommon..AddressList AL on A.AddressListID = AL.ID
	LEFT JOIN QSPCanadaCommon..Address AdShip on AL.ID = AdShip.AddressListID AND AdShip.Address_Type = 54001 --Ship To
	LEFT JOIN QSPCanadaCommon..Address AdBill   on AL.ID = AdBill.AddressListID   AND AdBill.Address_Type = 54001 --Use ship to for both.  Bill To=2
	LEFT JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
	LEFT JOIN QSPCanadaCommon..FieldManager FM on FM.FMID = C.FMID
*/
INTO #TEMP
FROM    QSPCanadaCommon.dbo.Campaign C INNER JOIN
              QSPCanadaOrderManagement.dbo.Batch B ON C.ID = B.CampaignID INNER JOIN
              QSPCanadaCommon.dbo.FieldManager FM ON FM.FMID = C.FMID INNER JOIN
              QSPCanadaFinance.dbo.INVOICE I ON B.OrderID = I.ORDER_ID INNER JOIN
              QSPCanadaCommon.dbo.CAccount A ON A.Id = B.AccountID LEFT OUTER JOIN
             -- QSPCanadaCommon.dbo.Contact Cont ON Cont.CAccountID = A.Id LEFT OUTER JOIN 	
	QSPCanadaCommon.dbo.Contact Cont ON Cont.ID = C.ShipToCampaignContactID LEFT OUTER JOIN --MS Feb 14, 2006
              QSPCanadaCommon.dbo.AddressList AL ON A.AddressListID = AL.ID LEFT OUTER JOIN
              QSPCanadaCommon.dbo.Address AdShip ON AL.ID = AdShip.AddressListID AND AdShip.address_type = 54001 LEFT OUTER JOIN
              QSPCanadaCommon.dbo.Address AdBill ON AL.ID = AdBill.AddressListID AND AdBill.address_type = 54001
WHERE Invoice_ID = @InvoiceID
AND cont.DeletedTF =0		--MS Oct12, 2005
GROUP BY C.IsStaffOrder,
	OrderQualifierID, OrderTypeCode, Invoice_ID, A.ID , CampaignID, Order_Id, C.FMID, FM.FirstName + ' ' + FM.LastName, 
	--ISNULL(B.ContactFirstName + ' ' + B.ContactLastName, cont.FirstName + ' ' + cont.LastName) ,
	cont.FirstName , cont.LastName,
	CONVERT(varchar, Invoice_Date,101) ,
	CONVERT(varchar, Invoice_Due_Date,101) ,
	A.Name , AdShip.Street1,AdShip.Street2, AdShip.City,
	AdShip.StateProvince,AdShip.Postal_Code,AdShip.Zip4,AdBill.Street1,
	AdBill.Street2 		,
	AdBill.City      	,
	AdBill.StateProvince	,
	AdBill.Postal_Code      	,
	AdBill.Zip4		,
	Is_Printed	
ORDER BY ContactId Desc --Latest Contact First

--If the Campaign contact does not exists Get the group contact or default contact
-- to avoid invoices without header MS June 29, 2006 Issue #566
SELECT @Cnt = COUNT(*) FROM  #TEMP
IF @Cnt  = 0
BEGIN
	SELECT TOP 1 C.IsStaffOrder,  			
			OrderQualifierID, OrderTypeCode, Invoice_ID, A.ID as AcctID, CampaignID, Order_Id, C.FMID, FM.FirstName + ' ' + FM.LastName as FMName, 
			Cont.FirstName + ' ' + Cont.Lastname as ContactName,
			--ISNULL(B.ContactFirstName + ' ' + B.ContactLastName, Cont.FirstName + ' ' + Cont.LastName)  AS ContactName,
			CONVERT(varchar, Invoice_Date,101) as Invoice_Date, 
			CONVERT(varchar, Invoice_Due_Date,101) as Invoice_Due_Date, 
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
			CASE Is_Printed	
				WHEN 'Y' THEN 1 
				WHEN 'N' THEN 0
			ELSE 0
			END AS Is_Printed,
			MAX(Cont.Id) ContactId
	FROM    QSPCanadaCommon.dbo.Campaign C INNER JOIN
        	QSPCanadaOrderManagement.dbo.Batch B ON C.ID = B.CampaignID INNER JOIN
        	QSPCanadaCommon.dbo.FieldManager FM ON FM.FMID = C.FMID INNER JOIN
        	QSPCanadaFinance.dbo.INVOICE I ON B.OrderID = I.ORDER_ID INNER JOIN
        	QSPCanadaCommon.dbo.CAccount A ON A.Id = B.AccountID LEFT OUTER JOIN
        	QSPCanadaCommon.dbo.Contact Cont ON Cont.CaccountID = A.ID LEFT OUTER JOIN --MS Feb 14, 2006
        	QSPCanadaCommon.dbo.AddressList AL ON A.AddressListID = AL.ID LEFT OUTER JOIN
        	QSPCanadaCommon.dbo.Address AdShip ON AL.ID = AdShip.AddressListID AND AdShip.address_type = 54001 LEFT OUTER JOIN
        	QSPCanadaCommon.dbo.Address AdBill ON AL.ID = AdBill.AddressListID AND AdBill.address_type = 54001
	WHERE Invoice_ID = @InvoiceID
	AND cont.DeletedTF =0		
	GROUP BY C.IsStaffOrder,
		OrderQualifierID, OrderTypeCode, Invoice_ID, A.ID , CampaignID, Order_Id, C.FMID, FM.FirstName + ' ' + FM.LastName, 
		--ISNULL(B.ContactFirstName + ' ' + B.ContactLastName, cont.FirstName + ' ' + cont.LastName) ,
		cont.FirstName , cont.LastName,
		CONVERT(varchar, Invoice_Date,101) ,
		CONVERT(varchar, Invoice_Due_Date,101) ,
		A.Name , AdShip.Street1,AdShip.Street2, AdShip.City,
		AdShip.StateProvince,AdShip.Postal_Code,AdShip.Zip4,AdBill.Street1,
		AdBill.Street2 		,
		AdBill.City      	,
		AdBill.StateProvince	,
		AdBill.Postal_Code      	,
		AdBill.Zip4		,
		Is_Printed	
	ORDER BY ContactId Desc --Latest Contact First
End
ELSE
BEGIN
       SELECT * FROM #TEMP
END
DROP TABLE #TEMP
SET NOCOUNT OFF
GO
