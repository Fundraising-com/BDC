USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateFilesForResolve]    Script Date: 06/07/2017 09:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_GenerateFilesForResolve]
as

	DECLARE	@sqlcommand 	  	NVARCHAR(4000)
	
	Set @sqlcommand  = ' bcp  "select ID,Name from QSPCanadaCommon..Program"  queryout "e:\projects\paylater\nightly\ToResolve_test\Program.xls " -c -q -t"\t" -T'   
	Exec master..xp_cmdshell @sqlcommand


	Set @sqlcommand  = ' bcp  "select CampaignID,ProgramID from QSPCanadaCommon..CampaignProgram"  queryout "e:\projects\paylater\nightly\ToResolve_test\CampaignProgram.xls " -c -q -t"\t" -T'   
	Exec master..xp_cmdshell @sqlcommand


	Set @sqlcommand  = ' bcp  "Exec QSPCanadaProduct.dbo.pr_GenerateResolveProductList"  queryout "e:\projects\paylater\nightly\ToResolve_test\Product.xls " -w -c -q -t"\t" -T'   
	Exec master..xp_cmdshell @sqlcommand


	Set @sqlcommand  = ' bcp  "Exec QSPCanadaCommon.dbo.pr_GenerateResolveAccountList"  queryout "e:\projects\paylater\nightly\ToResolve_test\Account.xls " -w -c -q -t"\t" -T'   
	Exec master..xp_cmdshell @sqlcommand


	Set @sqlcommand  = ' bcp  "Exec QSPCanadaCommon.dbo.pr_GenerateCampaignListForResolve"  queryout "e:\projects\paylater\nightly\ToResolve_test\Campaign.xls " -w -c -q -t"\t" -T'   
	Exec master..xp_cmdshell @sqlcommand

	Set @sqlcommand  = ' bcp  "Exec QSPCanadaCommon.dbo.pr_GenerateResolveFMList"  queryout "e:\projects\paylater\nightly\ToResolve_test\FieldManager.xls " -w -c -q -t"\t" -T'   
	Exec master..xp_cmdshell @sqlcommand
GO
