USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[ShowErrors]    Script Date: 02/14/2014 13:08:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ShowErrors]
(
	@Table			sysname,
	@Top			int 		= NULL,
	@Owner			sysname		= NULL,
	@LoginName		nvarchar(256)	= NULL,
	@HostName		nvarchar(256) 	= NULL,
	@ApplicationName	nvarchar(256) 	= NULL,
	@NTUserName 		nvarchar(256)	= NULL,
	@StartTime		datetime	= NULL,
	@Debug			bit		= 0
)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @SELECT nvarchar(25), @ColumnList nvarchar(250), @FROM nvarchar(150), @WHERE nvarchar(250), @ORDERBY nvarchar(250)

	SET @SELECT = 'SELECT ' + CASE WHEN @Top IS NULL OR @Top < 0 THEN 'TOP 150' ELSE 'TOP ' + LTRIM(CAST(@Top AS varchar)) END
	SET @ColumnList = ' CAST(SUBSTRING(TextData, 8, CHARINDEX('','', TextData)-8) AS int) AS Error, e.Description, TextData, LoginName, NTUserName, HostName, ApplicationName, SPID, StartTime'
	SET @FROM = ' FROM ' + QUOTENAME(COALESCE(@Owner, 'dbo')) + '.' + QUOTENAME(@Table) + ' JOIN master..sysmessages AS e ON CAST(SUBSTRING(TextData, 8, CHARINDEX('','', TextData)-8) AS int) = e.error'

	SET @WHERE = ' WHERE EventClass = 33'

	IF @LoginName IS NOT NULL
	BEGIN
		SET @WHERE = @WHERE + ' AND LoginName LIKE ' + QUOTENAME(@LoginName, '''')
	END

	IF @HostName IS NOT NULL
	BEGIN
		SET @WHERE = @WHERE + ' AND HostName LIKE ' + QUOTENAME(@HostName, '''')
	END

	IF @ApplicationName IS NOT NULL
	BEGIN
		SET @WHERE = @WHERE + ' AND ApplicationName LIKE ' + QUOTENAME(@ApplicationName, '''')
	END	

	IF @NTUserName IS NOT NULL
	BEGIN
		SET @WHERE = @WHERE + ' AND NTUserName LIKE ' + QUOTENAME(@NTUserName, '''')
	END	

	IF @StartTime IS NOT NULL
	BEGIN
		SET @WHERE = @WHERE + ' AND StartTime >= ' + QUOTENAME(CONVERT(varchar, @StartTime, 109), '''') 
	END
	
	SET @ORDERBY = 'ORDER BY CAST(SUBSTRING(TextData, 8, CHARINDEX('','', TextData)-8) AS int)'

	IF @Debug = 1
	BEGIN
		SELECT @SELECT + char(13) + @ColumnList + char(13) + @FROM + char(13) + @WHERE + char(13) + @ORDERBY
	END
	
	EXEC(@SELECT + @ColumnList + @FROM + @WHERE + @ORDERBY)
	
END
GO
