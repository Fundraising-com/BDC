USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CampaignProgram_SelectOne]    Script Date: 06/07/2017 09:19:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'CampaignProgram'
-- based on the Primary Key.
-- Gets: @iCampaignID int
-- Gets: @iProgramID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CampaignProgram_SelectOne]
	@iCampaignID int,
	@iProgramID int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[CampaignID],
	[ProgramID],
	[IsPreCollect],
	[GroupProfit],
	[DeletedTF]
FROM qspcanadacommon..[CampaignProgram]
WHERE
	[CampaignID] = @iCampaignID
	AND [ProgramID] = @iProgramID
GO
