USE [QSPCanadaFinance]
GO
/****** Object:  View [dbo].[Campaign_Program_Multiplier_vw]    Script Date: 06/07/2017 09:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Campaign_Program_Multiplier_vw]

AS

	SELECT		c.ID AS CampaignId,
				CASE  
					WHEN (cp.CampaignID IS NOT NULL AND ISNULL(c.OnlineOnlyPrograms, 0) = 0) THEN 1	
					ELSE 0.5
				END	AS ProgramMultiplier
	FROM		QSPCanadaCommon..Campaign c
	LEFT JOIN	QSPCanadaCommon..CampaignProgram cp ON cp.CampaignID = c.ID AND cp.DeletedTF = 0 AND cp.ProgramID IN (1, 2)
GO
