USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerateGiftCardFile]    Script Date: 06/07/2017 09:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GenerateGiftCardFile]

@file_id	int = 0,
@fileName	varchar(50)

AS

SET NOCOUNT ON

DECLARE 
	@filename_suffix	VARCHAR(50),   -- The filename suffix which indicates whether it is regular or holiday
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
	@temptable 		nvarchar(255),
	@type			VARCHAR(1),	-- R for regular, X for holiday, N for none
	@runid			INT

SET @now	= getDate()


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

	set @sqlcommand = 'bcp "tempdb.##t1" out "q:\projects\paylater\GiftCards\' + @filename + '" -c -q -T -w'
	exec master..xp_cmdshell @sqlcommand
--select * from ##t1
	drop table ##t1
END

-- Else generate a gift card file for remitted orders that a gift card is needed to be sent and not already sent
ELSE
BEGIN
	SET @type = 'R'

	IF @filename is null
	BEGIN
		SET @filename = 'Gift_Cards_' + 
			CAST(DATEPART(YEAR,		@now) 	AS VARCHAR) +
			CAST(DATEPART(MONTH,	@now) 	AS VARCHAR) +
			CAST(DATEPART(DAY,		@now) 	AS VARCHAR) + 
			CAST(DATEPART(HOUR,		@now) 	AS VARCHAR) + 
			CAST(DATEPART(MINUTE,	@now) 	AS VARCHAR) + 
			CAST(DATEPART(SECOND,	@now) 	AS VARCHAR)
	END

	WHILE(@type <> 'N')
	BEGIN
		IF(@type = 'R')
		BEGIN
			SET @filename_suffix = '_Regular.txt'
			SELECT @nbr_rows=count(*) FROM vw_GiftCardDetailRegular
		END
		ELSE IF(@type = 'X')
		BEGIN
			SET @filename_suffix = '_Holiday.txt'
			SELECT @nbr_rows=count(*) FROM vw_GiftCardDetailHoliday
		END
		ELSE
		BEGIN
			SET @filename_suffix = ''
			SELECT @nbr_rows=0
		END
		
		IF @nbr_rows <> 0
		BEGIN
	
			--Create new file output entry
			INSERT INTO GiftCardOutput (FileName, Date, FileContent) VALUES (@fileName + @filename_suffix, @now, '')
			SELECT @gco_id = @@identity

			--It won't compile without this tweak
			SELECT	TOP 1
					RemitBatchID ,
					CustomerOrderHeaderInstance , 
					TransID , 
					CampaignId ,  
					OrderID ,
					TitleCode ,
					MagazineTitle ,
					Lang ,
					NumberOfIssues , 
					SupporterName , 
					FirstName , 
					LastName , 
					Address1 , 
					Address2 ,
					City , 
					State ,
					Zip,
					GroupName,
					RunID,
					case IsStaffOrder when 1 then 'S' else ' ' end as IsStaffOrder
			INTO		#t
			FROM		vw_GiftCardDetailRegular

			TRUNCATE TABLE #t

			IF(@type = 'R')
			BEGIN
				INSERT INTO #t
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
				    FROM vw_GiftCardDetailRegular v/*, RemitBatch rb
			                WHERE rb.ID = v.RemitBatchID AND
					    rb.status='42001'*/

				--Add Karen Philips to gift cards so she receives one each batch
				--INSERT INTO #t
				--VALUES (1206, 0, 1, 41200, 1111111, '3186', 'Reader''s Digest', 'EN', 12, 'KAREN PHILLIPS', 'KAREN', 'PHILLIPS', '442 PASSMORE AVENUE', NULL, 'SCARBOROUGH', 'ON','M1V5M7','QSP',0, ' ')

			END
			ELSE IF(@type = 'X')
			BEGIN
				INSERT INTO #t
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
				    FROM vw_GiftCardDetailHoliday v/*, RemitBatch rb
			                WHERE rb.ID = v.RemitBatchID AND
					    rb.status='42001'*/
					--    and rb.runid >=1124
			END
			
			-- BCP the temporary table in the gift card file according to the specific format
			SELECT @runid = max(runid) from remitbatch where status = 42001
	
				SELECT  right('0000000000' + convert(varchar(10), coalesce(@runid,'')), 10) +
					right ('0000000000' +  convert(varchar(10), coalesce(CampaignId,'')), 10) +
					right ('0000000000' +  convert(varchar(10), coalesce(OrderID,'')), 10) +
					right ('0000' + convert(varchar(4), coalesce(TitleCode,'')), 4) +
					left (convert(varchar(50), coalesce(MagazineTitle,'')) + space(50), 50) +
					left (convert(varchar(2), coalesce(Lang,'')) + space(2), 2) +
					right ('00000' + convert(varchar(5), coalesce(NumberOfIssues,'')), 5) +
					left (convert(varchar(80), coalesce(SupporterName,'')) + space(80), 80) +
					left (convert(varchar(50), coalesce(FirstName,'QSP')) + space(50), 50) +
					left (convert(varchar(50), coalesce(LastName,'Customer')) + space(50), 50) +
					left (convert(varchar(50), coalesce(Address1,'')) + space(50), 50) +
					left (convert(varchar(50), coalesce(Address2,'')) + space(50), 50) +
					left (convert(varchar(50), coalesce(City,'')) + space(50), 50) +	
					left (convert(varchar(10), coalesce(State,'')) + space(10), 10) +
					left (convert(varchar(20), coalesce(Zip,'')) + space(20), 20) +
					left (convert(varchar(50), coalesce(GroupName,'')) + space(50), 50) +
					IsStaffOrder AS TextLine
				INTO ##t_bcp
				FROM #t
			
			set @sqlcommand = 'bcp "tempdb.##t_bcp" out "Q:\projects\paylater\GiftCards\' + @filename + @filename_suffix + '" -c -q -T -w'
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
			PRINT 'No ' + CASE @type WHEN 'R' THEN 'regular' WHEN 'X' THEN 'holiday' END + ' gift cards need to be sent'

		IF(@type = 'R')
			SET @type = 'X'
		ELSE IF(@type = 'X')
			SET @type = 'N'
	END
END

IF @@error <> 0
	SELECT 1
ELSE 
	SELECT 0
GO
