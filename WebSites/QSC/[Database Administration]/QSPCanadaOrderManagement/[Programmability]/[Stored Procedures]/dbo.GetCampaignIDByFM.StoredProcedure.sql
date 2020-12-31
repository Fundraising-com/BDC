USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetCampaignIDByFM]    Script Date: 06/07/2017 09:19:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
cREATE  PROCEDURE [dbo].[GetCampaignIDByFM] 
	@FMID varchar(4)
AS

SELECT	CampaignID 
FROM		QSPCanadaCommon.dbo.FMCurrentCampaign	FMCC
WHERE	FMCC.FMID = @FMID
GO
