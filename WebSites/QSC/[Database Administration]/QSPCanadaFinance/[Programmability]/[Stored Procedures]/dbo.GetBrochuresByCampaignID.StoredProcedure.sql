USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetBrochuresByCampaignID]    Script Date: 06/07/2017 09:17:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetBrochuresByCampaignID] 
        @CampaignID        int 
AS 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
--   MTC 8/31/2004 
--   Get Brochures By Campaign ID For Canada Finance System 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
SET NOCOUNT ON 

SELECT Program_Type as BrochureName 
FROM QSPCanadaCommon..CampaignProgram CP 
INNER JOIN QSPCanadaProduct..Program_Master PM ON CP.ProgramID = PM.Program_ID 
WHERE CampaignID = @CampaignID 
AND CP. ProgramID <> 3 -- filter out magnet 
AND CP.DeletedTF = 0

SET NOCOUNT OFF
GO
