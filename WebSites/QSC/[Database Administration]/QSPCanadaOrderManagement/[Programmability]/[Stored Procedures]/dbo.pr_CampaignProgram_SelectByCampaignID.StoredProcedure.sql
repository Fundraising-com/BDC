USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CampaignProgram_SelectByCampaignID]    Script Date: 06/07/2017 09:19:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'CampaignProgram'
-- based on the Primary Key.
-- Gets: @iCampaignID int
-- Gets: @iProgramID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CampaignProgram_SelectByCampaignID]
	@iCampaignID int
	
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	cp.[CampaignID],
	cp.[ProgramID],
	cp.[IsPreCollect],
	cp.[GroupProfit],
	cp.[DeletedTF],
	p.name as ProgramDescription
FROM qspcanadacommon..[CampaignProgram] cp ,qspcanadacommon..program p
WHERE
	cp.[CampaignID] = @iCampaignID and 
	cp.programid = p.id and
	cp.DeletedTF = 0
GO
