USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[PrintStatementHeaderDetails]    Script Date: 06/07/2017 09:17:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[PrintStatementHeaderDetails]

	@AccountID	INT,
	@StartDate	DATETIME,
	@EndDate	DATETIME

AS

SET NOCOUNT ON

SELECT	TOP 1
			Acct.ID AS AcctID,
			camp.FMID,
			Acct.Lang,
			fm.FirstName + ' ' + fm.LastName AS FMName,
			ISNULL(ct.FirstName, '') + ' ' + ISNULL(ct.LastName, '') AS ContactName,
			acct.Name AS AcctName,
			adBill.Street1 AS BillingAddress,
			adBill.Street2 AS BillingAddress2,
			adBill.City AS BillingCity,
			adBill.StateProvince AS BillingState,
			adBill.Postal_Code AS BillingZip,
			adBill.Zip4	AS BillingZip4,
			p.PhoneNumber,
			CASE ISNULL(camp.Lang,'EN')
				WHEN 'FR' THEN '6600, route Transcanadienne - bureau 750' 
				ELSE '695 Riddell Road' 	
			END AS QSPAddress1Label,
			CASE ISNULL(camp.Lang,'EN')
				WHEN 'FR' THEN 'Pointe-Claire, QC   H9R 4S2' 
				ELSE 'Orangeville, ON   L9W 4Z5 ' 
			END AS QSPAddress2Label,
			CASE ISNULL(camp.Lang,'EN')
				WHEN 'FR' THEN  '1-866-342-3863'
				ELSE '1-866-342-3863'
			END AS QSPPhoneLabel
FROM		QSPCanadaCommon..CAccount acct
LEFT JOIN	QSPCanadaCommon..Phone p (NOLOCK)
				ON	p.PhoneListID = acct.PhoneListID
				AND	p.Type=30505  --Main
LEFT JOIN	QSPCanadaCommon..Address adBill
				ON	adBill.AddressListID = acct.AddressListID
				AND	adBill.Address_Type = 54002 --BillTo
LEFT JOIN	QSPCanadaCommon..Campaign camp       
				ON	camp.BillToAccountID = acct.ID
LEFT JOIN	QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
LEFT JOIN	QSPCanadaOrderManagement..Batch b
				ON	b.AccountID = acct.ID
				AND	b.CampaignID = camp.ID
LEFT JOIN	QSPCanadaCommon..Contact ct
				ON	ct.ID = camp.BillToCampaignContactID
WHERE 	acct.ID = @AccountID

SET NOCOUNT OFF
GO
