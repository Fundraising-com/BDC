USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_Kanata_Catalog_SelectAll]    Script Date: 06/07/2017 09:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Kanata_Catalog_SelectAll] 

@iCampaignID		int

AS

DECLARE	@bIsUSCampaign bit
SELECT	@bIsUSCampaign = CASE addr.Country WHEN 'US' THEN 1 ELSE 0 END
FROM	QSPCanadaCommon..Campaign c
JOIN	QSPCanadaCommon..CAccount acc
			ON	acc.ID = c.BillToAccountID
JOIN	QSPCanadaCommon..Address addr
			ON	addr.AddressListID = acc.AddressListID
			AND	addr.Address_Type = 54001
WHERE	c.ID = @iCampaignID

IF @bIsUSCampaign = 1
BEGIN
	SELECT	Code,
			Program_Type,
			Status AS StatusID
	FROM	QSPcanadaProduct..vw_Kanata_Catalog_SelectAll
	WHERE	(Program_Type LIKE '%Prize Zone%' OR Program_Type LIKE '%Kanata Pick%')
	AND		Program_Type LIKE '% US %'
END
ELSE
BEGIN
	SELECT	Code,
			Program_Type,
			Status AS StatusID
	FROM	QSPcanadaProduct..vw_Kanata_Catalog_SelectAll
	WHERE	(Program_Type NOT LIKE '%Prize Zone%' AND Program_Type NOT LIKE '%Kanata Pick%')
	OR		Program_Type NOT LIKE '% US %'
END
GO
