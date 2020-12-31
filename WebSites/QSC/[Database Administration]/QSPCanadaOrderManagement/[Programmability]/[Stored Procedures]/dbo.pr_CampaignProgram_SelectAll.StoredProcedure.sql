USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CampaignProgram_SelectAll]    Script Date: 06/07/2017 09:19:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CampaignProgram'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CampaignProgram_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[CampaignID],
	[ProgramID],
	[IsPreCollect],
	[GroupProfit],
	[DeletedTF]
FROM qspcanadacommon..[CampaignProgram]
ORDER BY 
	[CampaignID] ASC
	, [ProgramID] ASC
GO
