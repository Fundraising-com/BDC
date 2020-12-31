USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_RptGetConfirmationAgreementInfo]    Script Date: 06/07/2017 09:33:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_RptGetConfirmationAgreementInfo]
	@ICampaignID	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Get Summary info for reports
--  KET  8/13/2004  - Inital Revision
--  JLC 01/20/2005  - Noticed this proc was same identical to dbo.pr_RptGetSummaryInfo (which I updated today)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- UNTIL a difference is needed with dbo.pr_RptGetSummaryInfo, just have this one call that one.
EXEC dbo.pr_RptGetSummaryInfo @ICampaignID = @ICampaignID
GO
