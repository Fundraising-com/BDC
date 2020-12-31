USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllFulfillmentHouses]    Script Date: 06/07/2017 09:18:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllFulfillmentHouses] AS
SELECT	fh.Ful_Nbr,
		fh.Ful_Status,
		fh.Ful_Name,
		QSPCanadaOrderManagement.dbo.UDF_ReplaceAccents(fh.Ful_Name) AS Ful_Name_WithoutAccents,
		fh.Ful_Addr_1,
		fh.Ful_Addr_2,
		fh.Ful_City,
		fh.Ful_State,
		fh.Ful_Zip + CASE COALESCE(fh.Ful_Zip_Four, '') WHEN '' THEN '' ELSE '-' + fh.Ful_Zip_Four END AS Ful_Zip,
		fh.CountryCode,
		coalesce(fh.InterfaceMediaID, 0) AS InterfaceMediaID,
		coalesce(cdim.Description, '') AS InterfaceMediaDescription,
		coalesce(fh.InterfaceLayoutID, 0) AS InterfaceLayoutID,
		coalesce(cdil.Description, '') AS InterfaceLayoutDescription,
		coalesce(fh.TransmissionMethodID, 0) AS TransmissionMethodID,
		coalesce(fh.HardCopy, 0) AS HardCopy,
		coalesce(cdtm.Description, '') AS TransmissionMethodDescription,
		coalesce(fh.QSPAgencyCode, '') AS QSPAgencyCode,
		coalesce(fh.IsEffortKey, 'N') AS IsEffortKeyRequired,
		CASE fh.Ful_Nbr
			WHEN 6 THEN		'TIME'
			WHEN 7 THEN		'CDS'
			WHEN 9 THEN		'PALM'
			WHEN 11 THEN	'INDAS'
			WHEN 12 THEN	'EDS'
			WHEN 25 THEN	'KABLE'
			ELSE			'OTHER'
		END AS PayGroupLookUpCode
FROM		Fulfillment_House fh LEFT OUTER JOIN
		QSPCanadaCommon..CodeDetail cdim ON fh.InterfaceMediaID = cdim.Instance LEFT OUTER JOIN
		QSPCanadaCommon..CodeDetail cdil ON fh.InterfaceLayoutID = cdil.Instance LEFT OUTER JOIN
		QSPCanadaCommon..CodeDetail cdtm ON fh.TransmissionMethodID = cdtm.Instance
ORDER BY	fh.Ful_Name
GO
