USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_payment_from_qspexport]    Script Date: 06/07/2017 09:17:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Philippe Girard
-- Create date: July 17, 2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[pr_get_payment_from_qspexport]
	@download_path varchar(1024) = 'E:\Projects\Paylater\QSPCAFinance\ChequePaymentArchive'
	, @decrypted_folder_path varchar(1024) = 'E:\Projects\Paylater\QSPCAFinance\ResolveChequePayments\'
	, @ftp_server varchar(255) = '205.150.214.234'
	, @ftp_user varchar(255) = 'qspftp'
	, @ftp_pass varchar(255) = 'qsp1002'
	, @ftp_path varchar(255) =  'ChequePayments' -- 'QSPExport'
	, @ftp_work_dir varchar(255) = 'C:\WINNT\Temp'
	, @decrypt_cmd varchar(255) = 'C:\gnupg\gpg -o'
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @commandout varchar(2000)
    DECLARE @file_id int
    DECLARE @filename varchar(255)
    DECLARE @shortfilename varchar(255)
    DECLARE @cmd varchar(255)
	DECLARE @ret_code int

    CREATE TABLE #file (
        file_id int IDENTITY(1,1)
        , filename varchar(255)
    )

    CREATE TABLE #log (
        line_id int IDENTITY(1,1)
        , line varchar(255)
    )
    
    --INSERT INTO #log
    exec [pr_ftp_getfile] @FTPServer = @ftp_server
        , @FTPUser = @ftp_user
        , @FTPPWD = @ftp_pass
        , @FTPPath = @ftp_path
        , @TargetPath = @download_path
        , @workdir = @ftp_work_dir
        , @commandout = @commandout OUT

    SET @cmd = 'dir ' + @download_path + '\*.pgp /B /A-D'

    INSERT INTO #file
    EXEC master..xp_cmdshell @cmd

    delete from #file where filename is null or filename = 'File Not Found'

    WHILE EXISTS(SELECT * FROM #file)
    BEGIN
        SELECT TOP 1 @file_id = file_id
                , @filename = filename
        FROM #file
        
        DELETE FROM #file WHERE file_id = @file_id
        
        SET @shortfilename = SUBSTRING(@filename, 1, LEN(@filename) - PATINDEX('%.%', REVERSE(@filename)))
		
        SET @cmd = @decrypt_cmd + ' ' + @decrypted_folder_path + @shortfilename + '.xml ' + @download_path + '\' + @filename

        PRINT @cmd

        INSERT INTO #log
        EXEC @ret_code = master..xp_cmdshell @cmd
        
		

    END

    SELECT line FROM #log ORDER BY line_id
END
GO
