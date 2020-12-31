USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Statement_GetHeader]    Script Date: 06/07/2017 09:17:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Statement_GetHeader]
(
	@CampaignID	INT
)

RETURNS TABLE

AS

RETURN
(

SELECT		TOP 1 
			acc.ID AS AccountID,
			camp.ID AS CampaignID,
			ISNULL(camp.IsStaffOrder, 0) AS IsStaffCampaign,
			camp.Lang,
			CASE camp.IsStaffOrder
				WHEN 1 THEN	CASE camp.Lang
								WHEN 'FR' THEN	'Employés - Magasin'
								ELSE			'Staff - Magazine'
							END
				ELSE ISNULL(QSPCanadaCommon.dbo.FNC_GetProgramsbyCampaign(camp.ID), '')
			END AS CampaignPrograms,
			camp.FMID,
			fm.FirstName AS FMFirstName,
			fm.LastName AS FMLastName,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN	'Net 30 jours'
				ELSE			'Net 30 days'
			END AS PaymentTerms,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN	'QSP' 
				ELSE			'QSP' 	
			END AS CorpAttn,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN	'33 Prince Street Suite 200' 
				ELSE			'33 Prince Street Suite 200' 	
			END AS CorpAddress1,
			NULL AS CorpAddress2,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN	'Montreal' 
				ELSE			'Montreal' 
			END AS CorpCity,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN	'QC'
				ELSE			'QC'
			END AS CorpProvince,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN	'H3C 2M7' 
				ELSE			'H3C 2M7' 
			END AS CorpPostalCode,
			CASE ISNULL(camp.Lang, 'EN') 
				WHEN 'FR' THEN  '1-800-667-2536' 
				ELSE			'1-800-667-2536' 
			END AS CorpPhoneNumber,	
			'10435-8759' AS CorpGSTNumber,
			CASE addBill.StateProvince
				WHEN 'QC' THEN  '1006332729'
				ELSE			NULL
			END AS CorpQSTNumber,
			acc.Name AS AccountName,
			cont.FirstName AS AccountContactFirstName,
			cont.LastName AS AccountContactLastName,
			addBill.Street1 AS AccountAddress1,
			addBill.Street2 AS AccountAddress2,
			addBill.City AS AccountCity,
			addBill.StateProvince AS AccountProvince,
			addBill.Postal_Code AS AccountPostalCode,
			addBill.Zip4 AS AccountZip4,
			ISNULL(phone.PhoneNumber, '') AS AccountPhoneNumber
FROM		QSPCanadaCommon..Campaign camp       
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
LEFT JOIN	QSPCanadaCommon.dbo.Phone phone
				ON	phone.PhoneListID = acc.PhoneListID
				AND	phone.Type = 30505 --30505: Main
LEFT JOIN	QSPCanadaCommon..Address addBill
				ON	addBill.AddressListID = acc.AddressListID
				AND	addBill.Address_Type = 54002 --54002: BillTo
LEFT JOIN	QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
LEFT JOIN	QSPCanadaCommon..Contact cont
				ON	cont.ID = camp.BillToCampaignContactID
WHERE		camp.ID = @CampaignID
)
GO
