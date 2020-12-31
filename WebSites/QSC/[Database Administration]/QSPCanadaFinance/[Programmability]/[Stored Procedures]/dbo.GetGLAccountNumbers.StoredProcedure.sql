USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetGLAccountNumbers]    Script Date: 06/07/2017 09:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetGLAccountNumbers]
	
AS

SELECT		dbo.UDF_GLAccount_GetAccountNumber(GLAccountID) AS GLAccountNumber,
			[Description] AS GLAccountDescription,
			GLAccountID
FROM		GLAccount
WHERE		GLAccountStatusID = 1 --1:Active
ORDER BY	[Description]
GO
