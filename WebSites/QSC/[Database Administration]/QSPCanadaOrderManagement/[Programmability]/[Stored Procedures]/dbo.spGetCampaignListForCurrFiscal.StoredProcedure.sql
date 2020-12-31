USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spGetCampaignListForCurrFiscal]    Script Date: 06/07/2017 09:20:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetCampaignListForCurrFiscal]
	@accountID int
	
 AS
	declare @startdate datetime
	declare @enddate datetime

	exec QSPCanadaCommon..GetCurrentFiscalStartAndEnd @startdate output, @enddate output
	-- for current fiscal year fetch all the campaigns
	select pr.id, pr.name from qspcanadacommon..campaign,
		qspcanadacommon..campaignprogram,
		qspcanadacommon..program pr  where billtoaccountid=@accountID and
			campaignid =qspcanadacommon..campaign.id
			and pr.id=programid
			and StartDate between @startdate and @enddate
		order by StartDate
GO
