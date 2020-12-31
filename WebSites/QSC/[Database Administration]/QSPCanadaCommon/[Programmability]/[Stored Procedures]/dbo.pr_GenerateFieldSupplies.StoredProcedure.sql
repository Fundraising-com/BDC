USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateFieldSupplies]    Script Date: 06/07/2017 09:33:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GenerateFieldSupplies] 

	@fiscal int = 0,
	@id int =0,
	@shipdatefrom varchar(50)='',
	@shipdateto varchar(50)='',
	@province varchar(4)='',
	@sFMID varchar(4)=''

AS

IF (ISNULL(@shipdatefrom,'') = '' OR ISNULL(@shipdateto,'') = '')
BEGIN
	RAISERROR("Missing Ship From or Ship To Date", 10, 1)
	RETURN
END

DECLARE 	@Select	Varchar(8000),
		@Where	Varchar(8000),
		@GroupBy	Varchar(8000),
		 @needand int

set @needand = 0
set @Select=
'declare @campaignid int
declare @programid int
declare @dd datetime
declare @count int
declare @fsForFiscal int
select @count = 0 
set nocount on
declare aUpdate cursor for 
select distinct   campaignid, SuppliesDeliveryDate
	from campaign,campaignprogram where status = 37002 and FSOrderRecCreated <> 1 and  FSRequired=1  and suppliesdeliverydate <>  ''1/1/95'' and  campaign.id=campaignprogram.campaignid '


-- Set up where clase
if @id <> 0
begin 
	set @Select = @Select + ' and Campaign.id =  ' + CAST(@ID AS Varchar(200)) + ' '
	select @needand=1
end

if @shipdatefrom <> ''
begin
	set @Select = @Select + 'and SuppliesDeliveryDate between '''+@shipdatefrom+'''' +' and '+  ''''+ @shipdateto+''''
end

if @province <> ''
begin
	set @Select = @Select + ' and QSPCanadaCommon.dbo.FNC_GetCampaignShipToProvince( Campaign.ID ) = '''+@province+''''
end


if @sFMID <> ''
begin
	set @Select = @Select + ' and FMID = '''+@sFMID+''''
end

set @Select = @Select + ' order by  SuppliesDeliveryDate'



set @Select = @Select +' 
open aUpdate

fetch next from aUpdate into @campaignid, @dd
while(@@fetch_status <> -1)
begin

	select @fsForFiscal = count(*)	
		from qspcanadaproduct..program_master pm,qspcanadacommon..season season,qspcanadaproduct..programsection ps,
		qspcanadacommon..campaign c 
		where pm.season = season.id
		and type = 3
		and Code= CatalogCode
		and c.id  = @campaignid
		and c.StartDate BETWEEN season.StartDate AND season.EndDate
		--and datepart(year, c.StartDate) = Season.FiscalYear
		--and QSPCanadaCommon.dbo.UDF_GetSeason(c.StartDate)=Season.Season

	if(@fsForFiscal<>0)
	begin
		delete   from CampaignToContentCatalog where campaignid=@campaignid
		exec pr_CreateCampaignToContentCatalog @campaignid
	
		exec QSPCanadaOrderManagement..GenerateFieldSupplyOrder_V6 @campaignid, ''-1'', 0,1
		select @count = @count + 1
	end
	fetch next from aUpdate into @campaignid, @dd

end

close aUpdate
deallocate aUpdate

'

--print @Select
exec(@Select)

--exec qspcanadaordermanagement..pr_closeorders
GO
