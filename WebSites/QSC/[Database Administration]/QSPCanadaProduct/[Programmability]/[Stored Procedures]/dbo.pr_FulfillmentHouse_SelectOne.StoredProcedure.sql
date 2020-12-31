USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_FulfillmentHouse_SelectOne]    Script Date: 06/07/2017 09:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_FulfillmentHouse_SelectOne]

	@iFulfillmentHouseID int

AS

SELECT	fh.Ful_Nbr,
		fh.Ful_Status,
		fh.Ful_Name,
		coalesce(fh.InterfaceMediaID, 0) AS InterfaceMediaID,
		coalesce(cdim.Description, '') AS InterfaceMedia,
		coalesce(fh.InterfaceLayoutID, 0) AS InterfaceLayoutID,
		coalesce(cdil.Description, '') AS InterfaceLayout,
		coalesce(fh.TransmissionMethodID, 0) AS TransmissionMethodID,
		coalesce(fh.HardCopy, 0) AS HardCopy,
		coalesce(cdtm.Description, '') AS TransmissionMethod,
		coalesce(fh.IsEffortKey, 'N') as IsEffortKey,
		coalesce(fh.QSPAgencyCode, '') AS QSPAgencyCode,
		fh.Ful_Addr_1,
		coalesce(fh.Ful_Addr_2, '') AS Ful_Addr_2,
		fh.Ful_City,
		fh.Ful_State,
		fh.Ful_Zip,
		fh.CountryCode,
		coalesce(fhc.FirstName, '') as ContactFirstName,
		coalesce(fhc.LastName, '') as ContactLastName,
		coalesce(fhc.Title, '') as ContactTitle,
		coalesce(fhc.Email, '') as ContactEmail,
		coalesce(fhc.WorkPhone, '') as ContactWorkPhone,
		coalesce(fhc.Fax, '') as ContactFax,
		coalesce(fhc.CustSvcContactQSPFirstName, '') as QSPContactFirstName,
		coalesce(fhc.CustSvcContactQSPLastName, '') as QSPContactLastName,
		coalesce(fhc.CustSvcContactQSPEmail, '') as QSPContactEmail,
		coalesce(fhc.CustSvcContactQSPPhone, '') as QSPContactWorkPhone,
		CASE fh.Ful_Nbr
			WHEN 6 THEN		'TIME'
			WHEN 7 THEN		'CDS'
			WHEN 9 THEN		'PALM'
			WHEN 11 THEN	'INDAS'
			WHEN 12 THEN	'KABLE'
			WHEN 207 THEN	'INDAS'
			WHEN 227 THEN	'INDAS'
			WHEN 232 THEN	'CDS'
			ELSE			'OTHER'
		END AS PayGroupLookUpCode
		

FROM		Fulfillment_House fh LEFT OUTER JOIN
		Fulfillment_House_Contacts fhc ON fh.Ful_Nbr = fhc.Ful_Nbr LEFT OUTER JOIN
		QSPCanadaCommon..CodeDetail cdim ON fh.InterfaceMediaID = cdim.Instance LEFT OUTER JOIN
		QSPCanadaCommon..CodeDetail cdil ON fh.InterfaceLayoutID = cdil.Instance LEFT OUTER JOIN
		QSPCanadaCommon..CodeDetail cdtm ON fh.TransmissionMethodID = cdtm.Instance
WHERE 	fh.Ful_Nbr = @iFulfillmentHouseID
GO
