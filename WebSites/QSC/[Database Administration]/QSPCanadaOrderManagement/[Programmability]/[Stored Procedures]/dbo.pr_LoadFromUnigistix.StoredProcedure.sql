USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LoadFromUnigistix]    Script Date: 06/07/2017 09:20:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE       PROCEDURE [dbo].[pr_LoadFromUnigistix] AS

set nocount on
DECLARE  	@TextLine 	VARCHAR(8000),
			@command 	VARCHAR(255),
			@i		INT,
			@PtrVal		VARBINARY(16),
			@xmlDoc 	INT,
			@sqlStatement nvarchar(4000),
			@date varchar(20),
			@filename varchar(200),
			@archivedirectory varchar(200),
			@fsize varchar(20),
			@sDate varchar(20),
			@smallfilename varchar(200)

-- First create archive directory
declare @today datetime
select @today=GetDate()

select @date = datepart(year,@today)
Select @sDate = Convert(varchar(10),@today,101)

if(datepart(month, @today) < 10)
begin
	select @date = @date + '_0' +  cast(datepart(month,@today) as varchar(2))
end
else
begin
	select @date = @date + '_' +  cast(datepart(month,@today) as varchar(2))
end


if(datepart(day, @today) < 10)
begin
	select @date = @date + '_0' +  cast(datepart(day,@today) as varchar(2))
end
else
begin
	select @date = @date + '_' +  cast(datepart(day,@today) as varchar(2))
end

Exec pr_CreateArchiveDirectory 'FromUnigistix'

SELECT @ArchiveDirectory='E:\Projects\PayLater\PayLaterArchives\'		


SELECT @command = 'Dir E:\Projects\PayLater\Nightly\FromUnigistix\*.xml '

create table #tResults  (line_id int identity, line_text varchar(8000) )

insert into #tResults
		exec master..xp_cmdshell @command

CREATE TABLE #X (	ID INT PRIMARY KEY,
				F TEXT)

	INSERT INTO #X VALUES (1, '')


	SELECT @PtrVal = TEXTPTR(F)
		FROM #X
		WHERE ID = 1




	DECLARE cBHE CURSOR FOR
		SELECT line_text FROM #tResults  
			where line_text like '%BHE%'
				 ORDER BY line_id

	OPEN cBHE

	FETCH NEXT FROM cBHE INTO @TextLine

	WHILE @@fetch_status = 0
	BEGIN
	        SELECT @filename = 'E:\Projects\PayLater\Nightly\FromUnigistix\'+substring(@TextLine,40, len(@textline)-39)
		SELECT @smallfilename = substring(@TextLine,40, len(@textline)-39)

print @filename		
		SELECT @fsize = replace(ltrim(substring(@TextLine,21, 18)),',','')

		exec dbo.pr_CreateUnigistixShipmentInformationFileRecvdACKFile @smallfilename, @fsize,	@sDate

		exec spLoadUnigistixBHEFile_V2 @filename



	        FETCH NEXT FROM cBHE INTO @TextLine
	END

	CLOSE cBHE

	DEALLOCATE cBHE



	DECLARE cNONEBHE CURSOR FOR
		SELECT line_text FROM #tResults  
			where line_text like '%Batch%' and line_text not like '%BHE%'
				 ORDER BY line_id

	OPEN cNONEBHE

	FETCH NEXT FROM cNONEBHE INTO @TextLine

	WHILE @@fetch_status = 0
	BEGIN
	        SELECT @filename = 'E:\Projects\PayLater\Nightly\FromUnigistix\'+substring(@TextLine,40, len(@textline)-39)
		SELECT @smallfilename = substring(@TextLine,40, len(@textline)-39)
print @filename		
		SELECT @fsize = replace(ltrim(substring(@TextLine,21, 18)),',','')
		exec spLoadUnigistixNonBHEFile_V2 @filename
		
		exec dbo.pr_CreateUnigistixShipmentInformationFileRecvdACKFile @smallfilename, @fsize,	@sDate

	        FETCH NEXT FROM cNONEBHE INTO @TextLine
	END

	CLOSE cNONEBHE

	DEALLOCATE cNONEBHE

	-----Nov 21, 2007 MS
	Create Table #SO ( orderid int, statusinstance int, Cnt int)

	Insert #SO
	select orderid, customerorderdetail.statusinstance,count(*) as Cnt 
	from batch ,customerorderheader,customerorderdetail
	where orderbatchdate=date and orderbatchid=id 
	and customerorderheaderinstance=instance
	and producttype <> 46001
	and batch.statusinstance=40012
	and batch.orderid in (select orderid from shipmentorder)
	group by orderid,customerorderdetail.statusinstance
	order by orderid,customerorderdetail.statusinstance


	update Batch set StatusInstance=40013 where orderid in
	( 
	select orderid from #SO 
	--where statusinstance=508
	group by orderid 
	having count(*)=1
	)

	Set @archivedirectory =@archivedirectory+@date+'\FromUnigistix\'

	-- move all the files into the archive
	SELECT @command = 'Move /y E:\Projects\PayLater\Nightly\FromUnigistix\*.xml  '+@archivedirectory

	exec master..xp_cmdshell @command

	exec dbo.pr_PushUnigistixTrackingFiles

	drop table #tResults
	drop table #X
	drop table #SO
GO
