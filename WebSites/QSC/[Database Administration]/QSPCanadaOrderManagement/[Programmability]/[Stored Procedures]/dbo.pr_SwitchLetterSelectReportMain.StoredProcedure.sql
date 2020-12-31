USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterSelectReportMain]    Script Date: 06/07/2017 09:20:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SwitchLetterSelectReportMain]

@sReport varchar(100),
@sTitleCode varchar(10) = '0',
@iRemitBatchID  int =0,
@dFrom datetime = '01/01/1955',
@dTo datetime = '01/01/1955',
@iSwitchLetterBatchID int = 0,
@iCustomerOrderHeaderInstance int =0,
@iTransID int =0

as


declare @sqlstatement varchar(400)



if(@sReport = 'pr_SwitchLetterSelectReport')
BEGIN
	set @sqlstatement =  'pr_SwitchLetterSelectReport '+convert(nvarchar,@iSwitchLetterBatchID)
END
else if(@sReport = 'pr_SwitchLetterSelectReportPreview')
BEGIN
	set @sqlstatement =  'pr_SwitchLetterSelectReportPreview '''+@sTitleCode +''''+',' +convert(nvarchar,@iRemitBatchID) +',''' +convert(nvarchar,@dFrom) +''','''+ convert(nvarchar,@dTo)+''''
END
else if(@sReport = 'pr_SwitchLetterSelectReportPreviewSub')
BEGIN
	set @sqlstatement =  'pr_SwitchLetterSelectReportPreviewSub ' +convert(nvarchar,@iCustomerOrderHeaderInstance) +',' +convert(nvarchar,@iTransID)
END
else if(@sReport = 'pr_SwitchLetterSelectReportSub')
BEGIN
	set @sqlstatement = 'pr_SwitchLetterSelectReportSub ' + convert(nvarchar,@iCustomerOrderHeaderInstance) +',' +convert(nvarchar,@iTransID)
END


exec(@sqlstatement)
GO
