USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetMainBatchForThisCampaign]    Script Date: 06/07/2017 09:19:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec dbo.prc_gen_CreateGrants

--drop PROCEDURE dbo.GetMainBatchForThisCampaign

CREATE PROCEDURE [dbo].[GetMainBatchForThisCampaign] 
	@CampaignID int
AS

--SELECT Date, ID FROM batch 
--WHERE campaignid = @CampaignID and OrderQualifierID = 39001


select 	AccountID,
	'',
	ShipToAccountID,
	0,
	'',
	'',
	'',
	'',
	'',
	'',
	'',
	CampaignPrograms   = 'test',
	0.0,
	0

FROM batch 
WHERE campaignid = @CampaignID and OrderQualifierID = 39001
GO
