USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GrossSalesPerformanceReport3]    Script Date: 06/07/2017 09:20:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GrossSalesPerformanceReport3] 

@FMID varchar(6) = '',
@DateFrom datetime = '01/01/1955',
@DateTo datetime = '01/01/1955',
@ProvinceCode varchar(2) = '',
@City varchar(50) = '',
@PostalCode varchar(7) = '',
@GroupClassCode varchar(7) = '',
@GroupCodeName varchar(7) = '',
@StaffIndicator int = 2,
@CampaignLanguage varchar(2) = '',
@ProgramsFromCampaign int = 0,
@IncentivesPrograms int = 0,
@CatalogCode varchar(50) = '',
@InternetOrders int=0

AS

DECLARE	@sqlStatement nvarchar(4000),
		@sqlStatement2 nvarchar(4000),
		@DateCampaignApproved datetime,
		@DateFirstOrder datetime,
		@GroupID int,
		@GroupName varchar(50),
		@CampaignID int,
		@TempFMID varchar(4),
		@FMName varchar(101),
		@EstimatedAmountFromCampaign numeric(10,2),
		@ActualUnits int,
		@ActualAmount numeric(10,2),
		@ActualMagUnits int,
		@ActualMagAmount numeric(10,2),
		@ActualGiftUnits int,
		@ActualGiftAmount numeric(10,2),
		@VarianceAmount numeric(10,2),
		@VariancePercentage numeric(10,2),
		@CampaignEnrollment int,
		@AvgEnrollment numeric(10,2),
		@ActualNbrParticipants int,
		@AvgAmountParticipant numeric(10,2),
		@ParticipationPercentage numeric(10,2),
		@batchtypelist varchar (1024)


SET @batchtypelist = '39001, 39002, 39004, 39005, 39006, 39007, 39008'

SET @sqlStatement =   	'SELECT 	distinct c.ApprovedStatusDate as DateCampaignApproved, 
		a.ID as GroupID,
		a.Name as GroupName,
		c.Id as CampaignID,
		c.FMID as FMID, 
		fm.LastName + '', '' + fm.FirstName as FMName,
		c.EstimatedGross as EstimatedAmountFromCampaign,
		c.NumberOfParticipants as CampaignEnrollment
	

FROM		QSPCanadaCommon..CAccount a,
		QSPCanadaCommon..Campaign c,
		QSPCanadaCommon..Campaigntocontentcatalog ccc,
		QSPCanadaCommon..FieldManager fm,
		QSPCanadaCommon..CampaignProgram cp,
		QSPCanadaCommon..Program p,
		QSPCanadaCommon..Address addr,
		QSPCanadaOrderManagement..Batch b


WHERE	a.id = c.ShipToAccountID
		and a.addresslistid = addr.addresslistid
		and addr.address_type = 54001
		and c.FMID = fm.FMID
		and c.ID = cp.CampaignID
		and p.Id = cp.ProgramID
		and cp.DeletedTF = 0
		and b.CampaignID = c.ID
		and b.status in (40004, 40013)
		and ccc.campaignid = c.id '
		
                       		

if(@FMID <> 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' and fm.FMID = '''+ @FMID + ''''
END

if(@DateFrom <> '01/01/1955' and @DateTo <> '01/01/1955')
BEGIN
	set @sqlStatement = @sqlStatement + ' and c.StartDate between '''+ convert(nvarchar,@DateFrom) + ''' and ''' + convert(nvarchar,@DateTo) +''''
END

if(@ProvinceCode <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' and addr.stateProvince = '''+ @ProvinceCode +''''
END

if(@City <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' and addr.city = '''+ @City  +''''
END

if(@PostalCode <>'')
BEGIN
	set @sqlStatement = @sqlStatement + ' and addr.postal_code = '''+ @PostalCode +''''
END

if(@GroupClassCode <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' and a.CAccountCodeClass = '''+ @GroupClassCode +''''
END

if(@GroupCodeName <>'')
BEGIN
	set @sqlStatement = @sqlStatement + ' and a.CAccountCodeGroup = '''+ @GroupCodeName +''''
END

if(@StaffIndicator = 1)
BEGIN
	set @sqlStatement = @sqlStatement + ' and b.OrderQualifierID = ''39003'''
	SET @batchtypelist = '39003'
END

if(@StaffIndicator = 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' and b.OrderQualifierID <> ''39003'''
END

if(@StaffIndicator = 2)
BEGIN
	SET @batchtypelist = @batchtypelist + ',39003'
END

if(@CampaignLanguage <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' and c.Lang = '''+ @CampaignLanguage  +''''
END

/*
if(@ProgramsFromCampaign <> 0)
BEGIN
	if(@IncentivesPrograms <> 0)
	begin
		set @sqlStatement = @sqlStatement + '  and (cp.ProgramID = '+ convert(nvarchar,@ProgramsFromCampaign) 
	end
	else
	begin
		set @sqlStatement = @sqlStatement + ' and cp.ProgramID = '+ convert(nvarchar,@ProgramsFromCampaign)
	end
END

if(@IncentivesPrograms <> 0)
BEGIN
	if(@ProgramsFromCampaign <> 0)
	begin
		set @sqlStatement = @sqlStatement + ' or cp.ProgramID = '+ convert(nvarchar,@IncentivesPrograms) + ')'
	end
	else
	begin
		set @sqlStatement = @sqlStatement + ' and cp.ProgramID = '+ convert(nvarchar,@IncentivesPrograms)
	end
END
*/

if(@InternetOrders = 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' and b.orderqualifierid <> ''39009'' '
END
ELSE
BEGIN
	SET @batchtypelist = @batchtypelist + ',39009'
END



if(@CatalogCode <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' and Content_Catalog_Code = '''+ @CatalogCode +''''
END

SET @sqlStatement =  @sqlStatement + ' ORDER BY FMID, DateCampaignApproved, GroupID, CampaignID'


declare @Table table (
	DateCampaignApproved datetime,
	DateFirstOrder datetime,
	GroupID int,
	GroupName varchar(50),
	CampaignID int,
	TempFMID varchar(4),
	FMName varchar(101),
	EstimatedAmountFromCampaign numeric(10,2),
	ActualMagUnits int,
	ActualMagAmount numeric(10,2),
	ActualGiftUnits int,
	ActualGiftAmount numeric(10,2),
	ActualUnits int,
	ActualAmount numeric(10,2),
	VarianceAmount numeric(10,2),
	VariancePercentage numeric(10,2),
	CampaignEnrollment int,
	AvgEnrollment numeric(10,2),
	ActualNbrParticipants int,
	AvgAmountParticipant numeric(10,2),
	ParticipationPercentage numeric(10,2)
)

exec ('declare c1 cursor for ' + @sqlStatement)
open c1
fetch next from c1 into @DateCampaignApproved, @GroupID, @GroupName, @CampaignID, @TempFMID, @FMName, @EstimatedAmountFromCampaign, @CampaignEnrollment
while @@fetch_status = 0
begin

	create table #t1  (
		units int,
		amount numeric(10,2),
		producttype varchar(25)
	)

	SET @sqlStatement2 = 'select count(cod.Price),  coalesce(sum(cod.Price), 0), cod.ProductType
	from CustomerOrderDetail cod, CustomerOrderHeader coh, batch b
	where coh.Instance = cod.CustomerOrderHeaderInstance
	and coh.CampaignID = '+cast(@CampaignID as varchar)+ ' 
	and cod.ProductType in (46001, 46002) 
	and coh.orderbatchid = b.id
	and coh.orderbatchdate = b.date 
	and b.orderqualifierid in('+@batchtypelist+') Group By cod.ProductType'
	
	insert into #t1  execute sp_executesql @sqlStatement2

	SELECT @ActualMagUnits = units, @ActualMagAmount = amount From #t1 where ProductType = '46001'
	SELECT @ActualGiftUnits = units,@ActualGiftAmount = amount  From #t1 where ProductType = '46002'


	DROP TABLE #t1

	CREATE TABLE #total  (
		Units int,
		Amount numeric(10,2),
		NbrParticipant int,
		DateFirstOrder datetime
	)

	SET @sqlStatement2 = 'select  count(cod.Price),  coalesce(sum(cod.Price), 0),  count(distinct studentinstance), min(coh.CreationDate)
	from CustomerOrderDetail cod, CustomerOrderHeader coh, batch b
	where coh.Instance = cod.CustomerOrderHeaderInstance
	and coh.CampaignID = ' + cast(@CampaignID as varchar) + 'AND cod.ProductType in (46001,46002)
	and coh.orderbatchid = b.id
	and coh.orderbatchdate = b.date 
	and b.orderqualifierid in(' + @batchtypelist +')'
	
	insert into #total  execute sp_executesql @sqlStatement2

	SELECT @ActualUnits = units, @ActualAmount = amount, @ActualNbrParticipants = nbrparticipant, @DateFirstOrder = datefirstorder FROM #total

	DROP TABLE #total

	set @VarianceAmount = @ActualAmount - @EstimatedAmountFromCampaign
	set @VariancePercentage = @VarianceAmount / case @EstimatedAmountFromCampaign when 0 then 1 else @EstimatedAmountFromCampaign end
	set @AvgEnrollment = @ActualAmount / case @CampaignEnrollment when 0 then 1 else @CampaignEnrollment end
	set @AvgAmountParticipant = @ActualAmount /case @ActualNbrParticipants when 0 then 1 else @ActualNbrParticipants end
	set @ParticipationPercentage = cast((@ActualNbrParticipants / case @CampaignEnrollment when 0 then 1 else cast(@CampaignEnrollment as float) end) as float)

	insert into @Table select @DateCampaignApproved, @DateFirstOrder, @GroupID, @GroupName, @CampaignID, @TempFMID, @FMName, @EstimatedAmountFromCampaign, @ActualMagUnits, @ActualMagAmount, @ActualGiftUnits, @ActualGiftAmount, @ActualUnits, @ActualAmount, @VarianceAmount, @VariancePercentage, @CampaignEnrollment, @AvgEnrollment, @ActualNbrParticipants, @AvgAmountParticipant, @ParticipationPercentage

	fetch next from c1 into @DateCampaignApproved, @GroupID, @GroupName, @CampaignID, @TempFMID, @FMName, @EstimatedAmountFromCampaign, @CampaignEnrollment
end
close c1
deallocate c1

select * from @Table where ActualUnits > 0 order by FMName, GroupName
GO
