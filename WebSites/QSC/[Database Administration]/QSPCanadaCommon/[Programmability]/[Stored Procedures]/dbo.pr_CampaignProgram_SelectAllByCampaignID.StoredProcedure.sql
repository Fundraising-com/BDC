USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CampaignProgram_SelectAllByCampaignID]    Script Date: 06/07/2017 09:33:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all existing row from the table 'CampaignProgram'
-- based on the Campaign ID.
-- Gets: @iCampaignID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CampaignProgram_SelectAllByCampaignID]
	@iCampaignID int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[CampaignID],
	[ProgramID],
	[IsPreCollect],
	[GroupProfit],
	[DeletedTF],
	[IsPersonalize],
	ISNULL([BlackboardPacket], CONVERT(BIT, 0)) BlackboardPacket,
	ISNULL([FieldSupplyPacket], CONVERT(BIT, 0)) FieldSupplyPacket,
	ISNULL([OnlineOnly], CONVERT(BIT, 0)) OnlineOnly,
	CASE WHEN ProgramID IN (44, 53, 54, 55, 56, 57, 58, 59, 62) THEN CONVERT(BIT, 1) ELSE ISNULL([AllowOnlineAccountDelivery], CONVERT(BIT, 0)) END AllowOnlineAccountDelivery
FROM [dbo].[CampaignProgram]
WHERE	[CampaignID] = @iCampaignID
AND		[DeletedTF] = 0
AND		EXISTS
		(SELECT	[ID]
		FROM		[Program]
		WHERE	[Program].[ID] = [CampaignProgram].[ProgramID]
		AND		[Program].[ActiveForFiscal_TF] = 1)
GO
