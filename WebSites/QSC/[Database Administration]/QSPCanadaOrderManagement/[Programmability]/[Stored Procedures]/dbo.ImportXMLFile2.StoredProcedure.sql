USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ImportXMLFile2]    Script Date: 06/07/2017 09:19:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE    PROCEDURE [dbo].[ImportXMLFile2] (@filename varchar(255), @tablename varchar(100), @rowpattern nvarchar(255),  @with varchar(2000)) AS


	DECLARE  	@TextLine 	VARCHAR(8000),
			@command 	VARCHAR(255),
			@i		INT,
			@PtrVal		VARBINARY(16),
			@xmlDoc 	INT,
			@sqlStatement nvarchar(4000)

	
	SELECT @command = 'TYPE ' + @filename

	create table #tResults  (line_id int identity, line_text varchar(8000) )

	insert into #tResults
       		exec master..xp_cmdshell @command

	CREATE TABLE #X (	ID INT PRIMARY KEY,
					F TEXT)

    	INSERT INTO #X VALUES (1, '')


    	SELECT @PtrVal = TEXTPTR(F)
      		FROM #X
     		WHERE ID = 1
  
	


    	DECLARE c CURSOR FOR
    	SELECT line_text FROM #tResults ORDER BY line_id

    	OPEN c

    	FETCH NEXT FROM c INTO @TextLine

    	WHILE @@fetch_status = 0
    	BEGIN
	        SET @TextLine = @TextLine
	        SELECT @i = i FROM (SELECT i = DATALENGTH(F) FROM #X) xyx
	        UPDATETEXT #X.F @PtrVal @i 0 @TextLine
        	        FETCH NEXT FROM c INTO @TextLine
    	END
 
    	CLOSE c

	DEALLOCATE c

	DECLARE @hdl int
	EXEC sp_xml_concat_V2 @xmlDoc OUT, '(SELECT F FROM #X)a', 'F'

	SET @sqlStatement = ' insert into '+@tablename+' SELECT * FROM OPENXML (@xmlDoc, @rowpattern,0)
    				WITH
    				(' + @with + ')'

	EXECUTE  	sp_executesql @sqlStatement, 
			@params = N'@xmlDoc INT, @rowpattern nvarchar(255), @with varchar(2000)',
			@xmlDoc=@xmlDoc, @rowpattern=@rowpattern, @with=@with



	 EXEC sp_xml_removedocument @xmlDoc

	DROP TABLE #tResults
	DROP TABLE #X
GO
