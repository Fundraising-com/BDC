USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CampaignProgram_Insert]    Script Date: 06/07/2017 09:33:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'CampaignProgram'
-- Gets: @iCampaignID int
-- Gets: @iProgramID int
-- Gets: @sIsPreCollect varchar(1)
-- Gets: @dcGroupProfit numeric(10, 2)
-- Gets: @bDeletedTF bit
-- Gets: @bIsPersonalize bit
-- Gets: @bIsBlackboardPacket bit
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CampaignProgram_Insert]
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

DECLARE @iCount	int

SELECT	@iCount = count(CampaignID)
FROM		CampaignProgram
WHERE	CampaignID = @iCampaignID
AND		ProgramID = @iProgramID
AND		DeletedTF = 1

IF(@iCount = 0)
BEGIN
	-- INSERT a new row in the table.
	INSERT [dbo].[CampaignProgram]
	(
		[CampaignID],
		[ProgramID],
		[IsPreCollect],
		[GroupProfit],
		[DeletedTF],
		[IsPersonalize],
		[BlackboardPacket],
		[FieldSupplyPacket],
		[OnlineOnly],
		[AllowOnlineAccountDelivery]
	)
	VALUES
	(
		@iCampaignID,
		@iProgramID,
		ISNULL(@sIsPreCollect, (0)),
		@dcGroupProfit,
		ISNULL(@bDeletedTF, (0)),
		ISNULL(@bIsPersonalize, (0)),
		ISNULL(@bBlackboardPacket, (0)),
		ISNULL(@bFieldSupplyPacket, (0)),
		CASE WHEN @iProgramID IN (44) THEN CONVERT(BIT, 0) ELSE ISNULL(@bOnlineOnly, (0)) END,
		CASE WHEN @iProgramID IN (44, 53, 54, 55, 56, 57, 58, 59, 62) THEN CONVERT(BIT, 1) ELSE ISNULL(@bAllowOnlineAccountDelivery, (0)) END
	)
END
ELSE
BEGIN
	UPDATE	CampaignProgram
	SET		IsPreCollect = ISNULL(@sIsPreCollect, (0)),
			GroupProfit = @dcGroupProfit,
			DeletedTF = ISNULL(@bDeletedTF, (0)),
			IsPersonalize= ISNULL(@bIsPersonalize, (0)),
			BlackboardPacket = ISNULL(@bBlackboardPacket, (0)),
			FieldSupplyPacket = ISNULL(@bFieldSupplyPacket, (0)),
			OnlineOnly = CASE WHEN @iProgramID IN (44) THEN CONVERT(BIT, 0) ELSE @bOnlineOnly END,
			AllowOnlineAccountDelivery = CASE WHEN @iProgramID IN (44, 53, 54, 55, 56, 57, 58, 59, 62) THEN CONVERT(BIT, 1) ELSE ISNULL(@bAllowOnlineAccountDelivery, (0)) END
	WHERE	CampaignID = @iCampaignID
	AND		ProgramID = @iProgramID
	AND		DeletedTF = 1
END
GO
