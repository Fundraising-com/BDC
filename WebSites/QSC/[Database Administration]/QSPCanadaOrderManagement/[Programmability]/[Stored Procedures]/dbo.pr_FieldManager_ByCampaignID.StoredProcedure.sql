USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_FieldManager_ByCampaignID]    Script Date: 06/07/2017 09:19:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_FieldManager_ByCampaignID] 
	@iCampaignID int =0
AS
if(@iCampaignID <>0)
BEGIN
	select * from qspcanadacommon..FieldManager fm, qspcanadacommon..Campaign cp where fm.FMID = cp.FMID and cp.ID= @iCampaignID
END
else
BEGIN
	SELECT	fm.*
	FROM		QSPCanadaCommon..FieldManager fm
	WHERE	((fm.Status = 1
	AND		fm.DeletedTF = 0
	AND		COALESCE(fm.DMIndicator, '') <> '')
	OR		fm.DMIndicator = 'Y')
	ORDER BY	fm.LastName,
			fm.FirstName
END
GO
