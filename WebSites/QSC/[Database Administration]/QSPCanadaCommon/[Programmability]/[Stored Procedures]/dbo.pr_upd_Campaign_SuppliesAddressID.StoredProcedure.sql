USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_Campaign_SuppliesAddressID]    Script Date: 06/07/2017 09:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_upd_Campaign_SuppliesAddressID]
 @CampaignID int,
 @SuppliesAddressID int
AS

UPDATE 
	dbo.Campaign
   SET 
	[SuppliesAddressID] 			= @SuppliesAddressID,
	[SuppliesShipToCampaignContactID] 	= 3 --SupplyAddress
	
 WHERE [Id] = @CampaignID
GO
