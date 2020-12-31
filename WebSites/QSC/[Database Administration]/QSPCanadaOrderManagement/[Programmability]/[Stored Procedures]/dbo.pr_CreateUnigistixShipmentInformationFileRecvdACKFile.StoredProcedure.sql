USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateUnigistixShipmentInformationFileRecvdACKFile]    Script Date: 06/07/2017 09:19:49 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[pr_CreateUnigistixShipmentInformationFileRecvdACKFile]
	@filename varchar(200),
	@size varchar(20),
	@date varchar(20)
as

	DECLARE	@sqlcommand 	  	NVARCHAR(4000),
		@BatchProc 		NVARCHAR(1000),
		@Outfilename            varchar(200)

 	Set @BatchProc  	= 'EXEC QSPCanadaOrderManagement.dbo.pr_CreateUnigistixShipmentInformationFileRecvdACKFileContent '''+@filename+''','''+@size+''','''+@date+''''

	Select  @Outfilename = 'e:\projects\paylater\nightly\ToUnigistixTracking\ACK-'+@filename+'.xml'
 
	set 	@sqlcommand = 	' bcp  "'+@BatchProc+'"  queryout "'+@Outfilename+' " -c -q -T -r '  

	Exec master..xp_cmdshell @sqlcommand
GO
