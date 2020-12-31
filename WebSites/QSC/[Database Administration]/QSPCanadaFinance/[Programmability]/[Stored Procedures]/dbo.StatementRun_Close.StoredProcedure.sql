USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementRun_Close]    Script Date: 06/07/2017 09:17:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementRun_Close]

	@StatementRunID	INT

AS

UPDATE	StatementRun
SET		StatementRunClosed = 1
WHERE	StatementRunID = @StatementRunID
GO
