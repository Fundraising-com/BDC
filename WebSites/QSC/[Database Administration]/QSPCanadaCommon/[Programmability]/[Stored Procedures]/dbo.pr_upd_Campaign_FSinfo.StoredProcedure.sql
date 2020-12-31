USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_Campaign_FSinfo]    Script Date: 06/07/2017 09:33:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_upd_Campaign_FSinfo]
  @CampaignID int,
  @SuppliesDeliveryDate datetime,
  @SuppliesShipToCampaignContactID int,
  @UserIDModified UserID_UDDT
AS

UPDATE
	Campaign
SET
	SuppliesDeliveryDate = @SuppliesDeliveryDate,
	SuppliesShipToCampaignContactID = @SuppliesShipToCampaignContactID,
	UserIDModified = @UserIDModified,
	DateModified = getdate()
WHERE
	Campaign.[ID] = @CampaignID
GO
