USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GLAccount_GetAccountNumber]    Script Date: 06/07/2017 09:17:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_GLAccount_GetAccountNumber]
(
	@GLAccountID	INT
)

RETURNS VARCHAR(100)

AS

BEGIN
	
	RETURN
	(
		SELECT	CASE GLAccountSystemID
					WHEN 1 THEN	ISNULL(CASE BusinessUnitID WHEN 2 THEN '173' ELSE '172' END, 'NULL') + '.' + ISNULL(Entity, '000') + '.' + ISNULL(Account, '0000') + '.' + ISNULL(Division, '0000') + '.' + ISNULL(Product, '0000') + '.' + ISNULL(LangMarket, '00') + '.' + ISNULL(DistChannel, '00') + '.' + ISNULL(Segment, '00')
					ELSE		ISNULL(CASE BusinessUnitID WHEN 2 THEN '173' ELSE '172' END, 'NULL') + '.' + ISNULL(Account, 'NULL') + '.' + ISNULL(Division, 'NULL') + '.' + ISNULL(Product, 'NULL') + '.' + ISNULL(Department, 'NULL') + '.' + ISNULL(Project, 'NULL') + '.' + ISNULL(Source, 'NULL') + '.' + ISNULL(Geographic, 'NULL') + '.' + ISNULL(Other, 'NULL') + '.' + ISNULL(Affiliate, 'NULL')
				END
		FROM	GLAccount
		WHERE	GLAccountID = @GLAccountID
	)

END
GO
