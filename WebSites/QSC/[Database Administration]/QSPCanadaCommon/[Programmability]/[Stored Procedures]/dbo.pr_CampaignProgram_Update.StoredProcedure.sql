USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CampaignProgram_Update]    Script Date: 06/07/2017 09:33:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will update an existing row in the table 'CampaignProgram'
-- Gets: @iCampaignID int
-- Gets: @iProgramID int
-- Gets: @sIsPreCollect varchar(1)
-- Gets: @dcGroupProfit numeric(10, 2)
-- Gets: @bDeletedTF bit
-- Gets: @bIsBlackboardPacket bit
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CampaignProgram_Update]
	@iCampaignID int,
	@iProgramID int,
	@sIsPreCollect varchar(1),
	@dcGroupProfit numeric(10, 2),
	@bDeletedTF bit,
	@bIsPersonalize bit,
	@bBlackboardPacket bit,
	@bFieldSupplyPacket bit,
	@bOnlineOnly bit,
	@bAllowOnlineAccountDelivery bit

AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[CampaignProgram]
SET 
	[IsPreCollect] = ISNULL(@sIsPreCollect, 0),
	[GroupProfit] = @dcGroupProfit,
	[DeletedTF] = ISNULL(@bDeletedTF, 0),
	[IsPersonalize]= ISNULL(@bIsPersonalize, 0),
	[BlackboardPacket] = ISNULL(@bBlackboardPacket, 0),
	[FieldSupplyPacket] = ISNULL(@bFieldSupplyPacket, 0),
	[OnlineOnly] = CASE WHEN @iProgramID IN (44) THEN CONVERT(BIT, 0) ELSE @bOnlineOnly END,
	[AllowOnlineAccountDelivery] = CASE WHEN @iProgramID IN (44, 53, 54, 55, 56, 57, 58, 59, 62) THEN CONVERT(BIT, 1) ELSE ISNULL(@bAllowOnlineAccountDelivery, (0)) END
WHERE
	[CampaignID] = @iCampaignID
	AND [ProgramID] = @iProgramID
GO
