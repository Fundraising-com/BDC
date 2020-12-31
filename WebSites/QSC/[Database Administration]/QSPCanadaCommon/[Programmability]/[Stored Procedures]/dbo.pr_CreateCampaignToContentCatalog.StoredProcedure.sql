USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateCampaignToContentCatalog]    Script Date: 06/07/2017 09:33:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE    PROCEDURE [dbo].[pr_CreateCampaignToContentCatalog]
	 @campaignid int

 AS
declare @programid int
declare @dd datetime

DECLARE @IsCombo int
Declare @count int 

EXEC QSPCanadaCommon..Campaign_IsCombo_Check @CampaignId, @IsCombo OUTPUT

declare aUpdateCC cursor for 
select   distinct  --campaignid, SuppliesDeliveryDate
     ID,programid 
	from 
campaign,campaignprogram
 where --startdate between '7/1/05' and '12/31/05'  
--status = 37002 
--and suppliesdeliverydate <> '1/1/95'
--and FSRequired=1
 campaign.id=campaignprogram.campaignid
and campaign.id = @campaignid
and programid not in (9)
and CampaignProgram.DeletedTF=0
order by id

open aUpdateCC

fetch next from aUpdateCC into @campaignid, @programid
while(@@fetch_status <> -1)
begin


	select @count=count(*) from CampaignToContentCatalog where campaignid=@campaignid and
		programid=@programid
	if(@count = 0)
	begin

		exec pr_upd_CampaignToContentCatalog @campaignid,@programid,'',1,@IsCombo,-1
	end
	fetch next from aUpdateCC into @campaignid, @programid

end

close aUpdateCC
deallocate aUpdateCC
GO
