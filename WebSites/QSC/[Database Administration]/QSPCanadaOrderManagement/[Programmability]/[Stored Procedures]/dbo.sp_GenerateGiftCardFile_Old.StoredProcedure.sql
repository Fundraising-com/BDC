USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerateGiftCardFile_Old]    Script Date: 06/07/2017 09:20:50 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_GenerateGiftCardFile_Old](@file_id INT = 0) AS

SET NOCOUNT ON

DECLARE @filename		VARCHAR(50),	--The filename is which the gift card detail is dumped 
	@remit_batch_id	INT,		--Remit Batch ID
	@coh_instance		INT,		--Customer Order Header Instance
	@trans_id		INT,		--Trans ID
	@gco_id		INT,		--Gift Card Output ID
	@now			DATETIME,	--The present dat/time 
	@nbr_rows		INT,		--Number of subs to send gift cards
	@sqlcommand 		NVARCHAR(1024),
	@textline 		VARCHAR(1024),
	@ptrval 		VARBINARY(16),
	@i 			INT,
	@pos_crlf 		INT,
	@text_length 		INT,
	@tempstr 		VARCHAR(1024),
	@sql_bcp		VARCHAR(512),
	@next_instance		INT,
	@temptable 		nvarchar(255)


SET @now	= getDate()
SET @filename = 'Gift_Cards_' + 
		CAST(DATEPART(YEAR,	@now) 	AS VARCHAR) +
		CAST(DATEPART(MONTH,	@now) 	AS VARCHAR) +
		CAST(DATEPART(DAY,	@now) 	AS VARCHAR) + 
		CAST(DATEPART(HOUR,	@now) 	AS VARCHAR) + 
		CAST(DATEPART(MINUTE,	@now) 	AS VARCHAR) + 
		CAST(DATEPART(SECOND,	@now) 	AS VARCHAR) + 
		'.txt'

-- If a file_id is provided then generate from the blub
IF @file_id <> 0
BEGIN
	set @temptable='##t1'
	create table ##t1 (col2 varchar(1024))
	
	select @filename = FileName, @text_length = datalength(filecontent)
	  from GiftCardOutput
	 where ID = @file_id
	
	set @i = 0
	
	while @i < @text_length
	begin
	    select @tempstr  = substring(filecontent, @i, @i + 1024)
	      from GiftCardOutput
	     where ID = @file_id -- take the next row
	
	     set @pos_crlf = charindex(char(13) + char(10), @tempstr,0)
	     set @textline = substring(@tempstr, 0, @pos_crlf)

	     set @sqlcommand = 'insert into ' + @temptable + ' values(''' + replace(@textline, '''', '''''') + ''')'
	     exec sp_executesql @sqlcommand

	     set @i = @i + @pos_crlf + 1
	end

	set @sqlcommand = 'bcp "##t1" out "e:\projects\paylater\nightly\' + @filename + '" -c -q -T -w'
	exec master..xp_cmdshell @sqlcommand
--select * from ##t1
	drop table ##t1
END

-- Else generate a gift card file for remitted orders that a gift card is needed to be sent and not already sent
ELSE
BEGIN
	SELECT @nbr_rows=count(*) FROM vw_GiftCardDetail
	
	IF @nbr_rows <> 0
	BEGIN

		--Create new file output entry
		INSERT INTO GiftCardOutput (FileName, Date, FileContent) VALUES (@filename, @now, '')
		SELECT @gco_id = @@identity

		SELECT  v.RemitBatchID ,
			v.CustomerOrderHeaderInstance , 
			v.TransID , 
			v.CampaignId ,  
			v.OrderID ,
			v.TitleCode ,
			v.MagazineTitle ,
			v.Lang ,
			v.NumberOfIssues , 
			v.SupporterName , 
			v.FirstName , 
			v.LastName , 
			v.Address1 , 
			v.Address2 ,
			v.City , 
			v.State ,
			v.Zip,
			v.GroupName,
			v.RunID,
			case v.IsStaffOrder when 1 then 'S' else ' ' end as IsStaffOrder 
		      INTO #t
		    FROM vw_GiftCardDetail v, RemitBatch rb
	                WHERE rb.ID = v.RemitBatchID AND
			    rb.status='42001'
			--    and rb.runid >=1124

		
		-- BCP the temporary table in the gift card file according to the specific format
		DECLARE @runid int
		SELECT @runid = max(runid) from remitbatch	

			SELECT  right('0000000000' + convert(varchar, coalesce(@runid,'')), 10) +
				right ('0000000000' +  convert(varchar, coalesce(CampaignId,'')), 10) +
				right ('0000000000' +  convert(varchar, coalesce(OrderID,'')), 10) +
				right ('0000' + convert(varchar, coalesce(TitleCode,'')), 4) +
				left (convert(varchar, coalesce(MagazineTitle,'')) + space(50), 50) +
				left (convert(varchar, coalesce(Lang,'')) + space(2), 2) +
				right ('00000' + convert(varchar, coalesce(NumberOfIssues,'')), 5) +
				left (convert(varchar, coalesce(SupporterName,'')) + space(80), 80) +
				left (convert(varchar, coalesce(FirstName,'QSP')) + space(50), 50) +
				left (convert(varchar, coalesce(LastName,'Customer')) + space(50), 50) +
				left (convert(varchar, coalesce(Address1,'')) + space(50), 50) +
				left (convert(varchar, coalesce(Address2,'')) + space(50), 50) +
				left (convert(varchar, coalesce(City,'')) + space(50), 50) +
				left (convert(varchar, coalesce(State,'')) + space(10), 10) +
				left (convert(varchar, coalesce(Zip,'')) + space(20), 20) +
				left (convert(varchar, coalesce(GroupName,'')) + space(50), 50) +
				IsStaffOrder AS TextLine
			INTO ##t_bcp
			FROM #t
		
		set @sqlcommand = 'bcp "##t_bcp" out "e:\projects\paylater\nightly\' + @filename + '" -c -q -T -w'
		exec master..xp_cmdshell @sqlcommand

		--Write in blub
		    select @PtrVal = textptr(FileContent)
		      from GiftCardOutput
		     where ID = @gco_id
		
		    -- Fills the blob
		    declare c2 cursor for select TextLine from ##t_bcp
		    open c2
		    fetch next from c2 into @TextLine
		    while @@fetch_status = 0
		    begin

		        set @TextLine = @TextLine + char(13) + char(10)
		
		        select @i = i from (select i = datalength(FileContent) from GiftCardOutput where ID = @gco_id) xyx
		        updatetext GiftCardOutput.FileContent @PtrVal @i 0 @TextLine
		
		        fetch next from c2 into @TextLine
		    end
		    close c2
		    deallocate c2		
	
		--Record which RemitBatch has been processed
		DECLARE c CURSOR FOR 
			SELECT DISTINCT(RemitBatchID) FROM #t
		
			OPEN c
				FETCH c INTO @remit_batch_id
	
				WHILE @@FETCH_STATUS = 0
				BEGIN
					SELECT @next_instance=COALESCE(MAX(instance),0) + 1 FROM GiftCardRemitBatch
					INSERT INTO GiftCardRemitBatch (Instance, GiftCardOutputID, RemitBatchID) VALUES (@next_instance, @gco_id, @remit_batch_id)				
					FETCH c INTO @remit_batch_id
				END
			CLOSE c
		DEALLOCATE c
	
		--Update each sub to say that the gift card was sent and when
		UPDATE CustomerOrderDetail SET IsGiftCardSent = 1
		  WHERE EXISTS (SELECT * FROM #t 
				     WHERE CustomerOrderDetail.CustomerOrderHeaderInstance = #t.CustomerOrderHeaderInstance AND
					       CustomerOrderDetail.TransID = #t.TransID)
	
	
		DROP TABLE #t
		DROP TABLE ##t_bcp
	END
	ELSE
		PRINT 'No gift cards need to be sent'
END
GO
