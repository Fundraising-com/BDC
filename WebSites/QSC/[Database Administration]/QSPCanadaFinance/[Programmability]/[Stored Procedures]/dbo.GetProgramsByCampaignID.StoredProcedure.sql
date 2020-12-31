USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetProgramsByCampaignID]    Script Date: 06/07/2017 09:17:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetProgramsByCampaignID] 
        @CampaignID        int 
AS 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
--   MTC 8/20/2004 
--   Get Programs By Campaign ID For Canada Finance System 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
SET NOCOUNT ON 

SELECT Name 
FROM QSPCanadaCommon..CampaignProgram CP 
INNER JOIN QSPCanadaCommon..Program P ON CP.ProgramID = P.ID 
WHERE CampaignID = @CampaignID 
AND CP. ProgramID <> 3 -- filter out magnet 
AND CP.DeletedTF = 0

SET NOCOUNT OFF
GO
