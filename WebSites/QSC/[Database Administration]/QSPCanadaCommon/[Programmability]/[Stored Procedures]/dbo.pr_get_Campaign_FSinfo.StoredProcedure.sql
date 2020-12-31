USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_Campaign_FSinfo]    Script Date: 06/07/2017 09:33:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_get_Campaign_FSinfo]
  @CampaignID int 
AS

SELECT
	[ID] AS CampaignID,
	ISNULL(SuppliesCampaignContactID, 0) 		AS SuppliesCampaignContactID,
	ISNULL(SuppliesShipToCampaignContactID, 0) 	AS SuppliesShipToCampaignContactID,
	ISNULL(SuppliesDeliveryDate, CAST('1995-01-01 00:00:00.000' as datetime)) AS SuppliesDeliveryDate,
	ISNULL(FSOrderRecCreated,0)			AS FSOrderRecCreated,
	CAST('1995-01-01 00:00:00.000' as datetime) 	AS DateChanged,
	CAST(' ' AS varchar(1)) 			AS UserIDChanged,
	CAST(0 AS int)	 				AS UserIDModified,
	SuppliesAddressID

FROM 
	Campaign
WHERE
	Campaign.[ID] = @CampaignID
GO
