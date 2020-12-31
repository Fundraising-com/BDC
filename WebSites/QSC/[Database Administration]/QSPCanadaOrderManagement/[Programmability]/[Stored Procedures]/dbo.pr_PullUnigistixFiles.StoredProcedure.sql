USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_PullUnigistixFiles]    Script Date: 06/07/2017 09:20:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_PullUnigistixFiles] AS

DECLARE @RunID INT,
	@output		VARCHAR(8000)
	
	SELECT @RunID = coalesce(max(RunID),0) + 1 FROM FTPOutputUnigistix
	
	INSERT INTO FTPOutputUnigistix (Line) EXEC MASTER..XP_CMDSHELL 'E:\projects\paylater\apps\QSPCanadaFTP.exe --config E:\projects\paylater\apps\QSPCanadaFTP.ini --host 10.100.106.25 --mode pull --user qsp --pass qspp@y '
	
	UPDATE FTPOutputUnigistix SET RunID = @RunID WHERE RunID IS NULL
	
	declare @line varchar(1024)
	set @output = ''
	declare  c1 cursor for select line from FTPOutputUnigistix WHERE RunID = @RunID order by id
	open c1
	fetch next from c1 into @line
	while @@fetch_status <> -1
	begin
		select @output = @output + coalesce(@line,'') + '
	'
		fetch next from c1 into @line
	end
	close c1
	deallocate c1
	
	
	exec MASTER..XP_SMTP_SENDMAIL 
		    @FROM 	= 'ftp_output@qsp.com',
		    @TO 	= 'qsp-qspfulfillment-dev@qsp.com',
		    @priority 	= 'NORMAL',
		    @subject 	= 'QSP Canada FTP Output - Unigistix',
		    @type 	= 'text/plain',
		    @message  	= @output,
		    @server 	= 'nasmtp.us.rdigest.com'
GO
