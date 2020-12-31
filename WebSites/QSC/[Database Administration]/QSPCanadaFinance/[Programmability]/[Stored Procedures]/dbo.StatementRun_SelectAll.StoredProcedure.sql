USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementRun_SelectAll]    Script Date: 06/07/2017 09:17:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementRun_SelectAll]

AS

SELECT	StatementRunID,
		StatementRunDate,
		FiscalYearEnd
FROM	StatementRun
GO
