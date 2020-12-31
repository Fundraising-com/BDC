USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetQSPCAReportByAccountID]    Script Date: 06/07/2017 09:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[pr_GetQSPCAReportByAccountID]
	@AccountID int,
	@FMID varchar(4) = '9999'
as


	select 	MenuTitle,
		'http://www.qsp.ca/EmailCollection/Login.aspx?AccountNumber='+cast(@AccountID as varchar(50))+
			'&NoReport='+cast(id as varchar(20)) as Link
			from [USPVL2K12].QSPStore.dbo.TemplateMenu where
		role=2 and templatesiteid=12  and isdeleted=0
		order by MenuTitle
GO
