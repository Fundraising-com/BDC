USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[PreImportValidate]    Script Date: 06/07/2017 09:20:44 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[PreImportValidate]
	@CampaignID int
AS

	
	--Make sure we have a good campaign - if we have an order make sure it's approved
	Declare @status int
	select  @status=Status From QSPCanadaCommon..Campaign where id = @CampaignID

	Declare @count int

	if(@status <> 37002)
	begin

		Update QSPCanadaCommon..Campaign  set Status=37002 where id = @CampaignID

	end

-- For now :  KT messed this up earlier
delete   from QSPCanadaCommon..CampaignToContentCatalog where campaignid=@campaignid

	-- Must have the campaign to content set up 
	select @count = count(*)  from QSPCanadaCommon..CampaignToContentCatalog where campaignid=@CampaignID

	if(@count = 0)
	begin
		
		exec QSPCanadaCommon..pr_CreateCampaignToContentCatalog @CampaignID
	end

	Select @CampaignID
GO
