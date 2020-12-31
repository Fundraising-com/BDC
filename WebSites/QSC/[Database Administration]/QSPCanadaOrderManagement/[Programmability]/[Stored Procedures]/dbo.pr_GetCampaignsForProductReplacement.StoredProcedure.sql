USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetCampaignsForProductReplacement]    Script Date: 06/07/2017 09:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_GetCampaignsForProductReplacement] 

@iGroupID int = 0,
@zGroupName varchar(250) = '',
@iCampaignID int = 0,
@zFMID varchar(4) = '',
@zFMFirstName varchar(100) = '',
@zFMLastName varchar(100) = '',
@zCity varchar(50) = '',
@zProvince varchar(25) = '',
@zPostalCode varchar(10) = '',
@zProgramName varchar(50) = 'Gift'

AS

	CREATE TABLE #List(
		[GroupID] [int] NOT NULL,
		[GroupName] [varchar](50),
		[CampaignID] [int] NOT NULL,
		[Programs] [varchar](200),
		[StartDate] [datetime] NULL,
		[EndDate] [datetime] NULL,
		[FMID] [varchar](4),
		[lastname] [varchar](50),
		[firstname] [varchar](50),
		[city] [varchar](50),
		[stateprovince] [varchar](10),
		[postal_code] [varchar](7)
	) ON [PRIMARY]

	/*
	--Issue# 2957
	DECLARE @SeasonStart DateTime
	DECLARE @SeasonEnd DateTime

	Select  @SeasonStart=  Startdate,  @SeasonEnd=  DateAdd(Month,+1,EndDate)
	From QSPCanadaCommon.dbo.Season
	Where Getdate() between StartDate and EndDate
	And Season ='Y'
	*/

	DECLARE @sqlStatement nvarchar(4000)

	SET @sqlStatement = 'INSERT INTO #LIST
					SELECT 	a.ID as GroupID,
					a.Name as GroupName,
					c.Id as CampaignID,
					cast('' '' as varchar(200)) as Programs,
					c.StartDate,
					c.EndDate,
					c.FMID as FMID, 
					fm.lastname,
					fm.firstname,
					addr.city,
					addr.stateprovince,
					addr.postal_code

			FROM		QSPCanadaCommon..CAccount a,
					QSPCanadaCommon..Campaign c,
					QSPCanadaCommon..FieldManager fm,
					QSPCanadaCommon..Address addr
			
			WHERE	a.id = c.ShipToAccountID
					and a.addresslistid = addr.addresslistid
					and addr.address_type = 54001
					and c.Status = 37002
					and c.FMID = fm.FMID'
					--and Convert(DateTime,Convert(Varchar(10),c.StartDate,101) ,101) >= '''+Convert(Varchar(10),@SeasonStart,101)+'''
					--and Convert(DateTime,Convert(Varchar(10),c.EndDate,101) ,101) <= '''+Convert(Varchar(10),@SeasonEnd,101)+'''

	if(@iGroupID<> 0)
		BEGIN
			set @sqlStatement = @sqlStatement + ' and a.id =  '''+ cast(@iGroupID as varchar) + ''''
		END
	
	if(@zGroupName <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and a.Name LIKE  '''+ @zGroupName + '%'''
		END

	if(@iCampaignID <> 0)
		BEGIN
			set @sqlStatement = @sqlStatement + ' and c.id =  '''+ cast(@iCampaignID as varchar) + ''''
		END
	if(@zFMID<> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and c.FMID =  '''+ @zFMID + ''''
		END
	if(@zFMFirstName <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and fm.firstname LIKE  '''+ @zFMFirstName + '%'''
		END
	if(@zFMLastName <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and fm.lastname LIKE  '''+ @zFMLastName + '%'''
		END
	if(@zCity <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and addr.city LIKE  '''+ @zCity + '%'''
		END
	if(@zProvince <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and addr.stateprovince LIKE  '''+ @zProvince + '%'''
		END
	if(@zPostalCode <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and addr.postal_code LIKE  '''+ @zPostalCode + '%'''
		END

	SET @sqlStatement = @sqlStatement +	'
					AND EXISTS
					(SELECT	cp.CampaignID
					FROM		QSPCanadaCommon..CampaignProgram cp
					WHERE	cp.CampaignID = c.ID
					AND		cp.DeletedTF = 0)
					'
	
/*
--Issue# 2957
if(@zProgramName = 'Gift')
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND cp.ProgramID IN (4, 19, 20)) '
	END
	else if(@zProgramName = 'Kanata')
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND cp.ProgramID IN (9, 11, 12, 18, 22, 23)) '
	END
*/

set @sqlStatement = @sqlStatement + 
'
declare @id int
declare @progid int
declare aSetProg  cursor for select #List.campaignid, programid from QSPCanadaCommon..campaignprogram,#List where QSPCanadaCommon..campaignprogram.campaignid =#List.CampaignID and DeletedTF=0
			
open aSetProg
fetch next from aSetProg into @id, @progid
WHILE(@@fetch_status <> -1)
begin
		
	update #List  set Programs = Programs + '' '' + Abr from #List,QSPCanadaCommon..CampaignProgram, QSPCanadaCommon..Program where 
			#List.CampaignID = @id and QSPCanadaCommon..CampaignProgram.ProgramID=@ProgID and QSPCanadaCommon..Program.ID = @ProgID
			and QSPCanadaCommon..campaignprogram.campaignid=@id

	fetch next from aSetProg into @id, @progid

end
close aSetProg
deallocate aSetProg


select * from #list order by StartDate Desc
drop table #list
'

exec (@sqlStatement)
GO
