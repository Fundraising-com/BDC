USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CampaignProgram_Delete]    Script Date: 06/07/2017 09:33:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will delete an existing row from the table 'CampaignProgram'
-- using the Primary Key. 
-- Gets: @iCampaignID int
-- Gets: @iProgramID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CampaignProgram_Delete]
	@iCampaignID int,
	@iProgramID int
AS
SET NOCOUNT ON
-- DELETE an existing row from the table.
UPDATE	[dbo].[CampaignProgram]
SET		[DeletedTF] = 1
WHERE
	[CampaignID] = @iCampaignID
	AND [ProgramID] = @iProgramID
GO
