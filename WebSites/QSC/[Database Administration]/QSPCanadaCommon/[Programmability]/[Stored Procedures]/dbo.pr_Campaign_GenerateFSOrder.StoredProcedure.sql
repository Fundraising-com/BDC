USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_GenerateFSOrder]    Script Date: 06/07/2017 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[pr_Campaign_GenerateFSOrder]
  @CampaignID int,
  @UserID UserID_UDDT
AS

------------------------------------------------------------------
--  Exec the Order Management Proc to actually generate the order 
------------------------------------------------------------------
EXEC QSPCanadaOrderManagement.dbo.GenerateFieldSupplyOrder_V2
	@CampaignId = @CampaignID,
	@UserId = @UserID


------------------------------------------------------------------
--  Make the necessary changes to the Campaign table 
------------------------------------------------------------------
UPDATE
	QSPCanadaCommon.dbo.Campaign
SET
	FSOrderRecCreated = 1,
	DateModified      = getdate(),
	UserIDModified    = @UserID
WHERE
	Campaign.[ID] = @CampaignID
GO
