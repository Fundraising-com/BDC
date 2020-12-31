USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetPaymentXMLandDecrypt]    Script Date: 06/07/2017 09:17:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetPaymentXMLandDecrypt]
AS
BEGIN
	Declare @decrypted_folder_path varchar(1024) 
	Declare @decrypt_cmd 		varchar(255) 
	Declare @PgpFilePath 		Varchar(200)
	Declare @ArchivePath 		Varchar(200)
	Declare	@command 		varchar(500)
	Declare @CurrentYear		varchar(10)
	Declare @FiletoDecrypt  		varchar(40)

	Create Table #tFileName  (Name Varchar(100) )	

	--Get payment encrypted file
	PRINT 'Get payment encrypted file'
	EXEC MASTER..XP_CMDSHELL 'E:\projects\paylater\apps\QSPCanadaFTP.exe --config E:\projects\paylater\apps\QSPCanadaResolveChequePaymentFTP.ini --host 205.150.214.234 --mode pull --user qspftp --pass qsp1002'

	--Get current Year
	PRINT 'Get current Year'
	Select @CurrentYear = datepart(year,getdate())
	Set @CurrentYear = @CurrentYear+'_'


	Set @decrypted_folder_path = 'E:\projects\paylater\QSPCAFinance\ResolveChequePayments\'
	Set @PgpFilePath   = 'E:\projects\paylater\QSPCAFinance\ResolveChequePayments\' 
	Set @ArchivePath   ='E:\projects\paylater\QSPCAFinance\ChequePaymentArchive\'
	
	Set @decrypt_cmd   = 'gpg -o '
	
	PRINT @command
	Select @command       = 'DIR ' + @PgpFilePath
	
	INSERT #tFileName EXEC master..xp_cmdshell @command

	Select * From #tFileName Where Name like '%.pgp'

	If @@Rowcount > 0
	Begin
		
		DECLARE cPgpFiles CURSOR FOR

		Select Rtrim(Substring(Name,(CHARINDEX( @CurrentYear,Name)),28))from #tFileName Where Name like '%.pgp'
	
		Open cPgpFiles

		FETCH NEXT FROM cPgpFiles INTO @FiletoDecrypt
		WHILE @@Fetch_Status = 0
		BEGIN
			Select @command = @decrypt_cmd+@PgpFilePath+@FiletoDecrypt+'xml  '+ @PgpFilePath+@FiletoDecrypt+'pgp'
			PRINT @command
			EXEC master..xp_cmdshell @command
			--Select @command

			 
			Select @command = 'MOVE ' + @PgpFilePath+@FiletoDecrypt+'pgp  ' + '  '+ @ArchivePath+@FiletoDecrypt+'pgp  '
			PRINT @command
			EXEC master..xp_cmdshell @command
			--Select @command

		FETCH NEXT FROM cPgpFiles INTO @FiletoDecrypt
		END

		Close cPgpFiles
		Deallocate cPgpFiles
				
	End
	
	Drop Table #tFileName
END
GO
